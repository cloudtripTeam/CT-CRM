using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MoreLinq;
using System.IO;

public partial class Admin_flight_result : System.Web.UI.Page
{
    public string searchSummary { set; get; }
    public string airlinesFilter { set; get; }
    public string stopOversFilter { set; get; }
    public string fares { set; get; }
    string DynUrl = string.Empty;
    string filePath = string.Empty;
    string filePathFlaxi = string.Empty;
    Guid searchID;
    SearchDetails oSearchDetails = null;
    Common com = new Common();
    Dictionary<int, double> stops = new Dictionary<int, double>();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                oSearchDetails = BLL.SearchDetails.Current(Request.QueryString.Get("id"));
                bingResult(Request.QueryString.Get("id"));
                _hfFrom.Value = oSearchDetails.flightFareSearchRQ.Segments[0].Origin.AirportCode;
                _hfTo.Value = oSearchDetails.flightFareSearchRQ.Segments[0].Destination.AirportCode;
                _hfDepart.Value = oSearchDetails.flightFareSearchRQ.Segments[0].Date;
                if (oSearchDetails.flightFareSearchRQ.JourneyType == EL.Trip_Type.Return_Trip)
                {
                    _hfReturn.Value = oSearchDetails.flightFareSearchRQ.Segments[1].Date;
                    _hfjtype.Value = "R";

                }
                else
                {
                    _hfjtype.Value = "O";
                }

                _hfAdult.Value = oSearchDetails.flightFareSearchRQ.Adults.ToString();
                _hfChild.Value = oSearchDetails.flightFareSearchRQ.Children.ToString();
                _hfInfant.Value = oSearchDetails.flightFareSearchRQ.Infants.ToString();
                _hfClass.Value = oSearchDetails.flightFareSearchRQ.Get_CabinClass(oSearchDetails.flightFareSearchRQ.CabinClass);
                _hfAirline.Value = oSearchDetails.flightFareSearchRQ.Airline.AirlineCode;
                _hfFlexible.Value = Convert.ToString(oSearchDetails.flightFareSearchRQ.FlexiSearch == 0 ? false : true);
                _hfDirect.Value = oSearchDetails.flightFareSearchRQ.DirectFlight.ToString();

            }
            catch (NullReferenceException nx)
            {
                fares = nx.StackTrace;
                //ErrorSignal.FromCurrentContext().Raise(nx);
            }
            catch (Exception ex)
            {
                fares = ex.StackTrace;
                // ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");

    }

    public string bingResult(string Guid)
    {
        try
        {
            bindSearchSummary();

            string fileUrl = HttpContext.Current.Server.MapPath("~/App_Data/Result/" + (Guid) + ".txt");
            //HttpContext.Current.Server.MapPath("~/App_Data/Result/result.txt");//HttpContext.Current.Server.MapPath("~/App_Data/Result/" + (HttpContext.Current.Session.SessionID + Guid) + ".txt");
            if (File.Exists(fileUrl))
            {
                string result = File.ReadAllText(fileUrl);
                System.Web.Script.Serialization.JavaScriptSerializer jsSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                jsSerializer.MaxJsonLength = Int32.MaxValue;
                EL.Flight.Itineraries itin = jsSerializer.Deserialize<EL.Flight.Itineraries>(result);
                if (itin.Items.Count > 0)
                {
                    if (Request.QueryString.Get("medium") != null)
                    {
                        CheckChoosenFare(itin, Request.QueryString.Get("price"), Request.QueryString.Get("valcar"), Request.QueryString.Get("index"));
                    }
                    bindFares(itin);
                    bindFlightFilter(itin);
                }
                else {

                    return fares = "Fares not availble please chnage your search criteria.";
                }
            }
            else
            {
                return fares = "file does not exist.";
            }

        }
        catch (NullReferenceException nx)
        {
            fares += nx.StackTrace;
            // ErrorSignal.FromCurrentContext().Raise(nx);
        }
        catch (Exception ex)
        {
            fares += ex.StackTrace;
            fares += ex.Source;
            //ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return "";
    }

    private void bindSearchSummary()
    {

        searchSummary = @"<ul>" +
            "<li class='logo hover' style='width: 0%;'></li>" +
            "<li class='route hover'>" +
                "<span class='origin'><span class='iata'>" + oSearchDetails.flightFareSearchRQ.Segments[0].Origin.AirportCityCode + "</span><span class='name'>" + oSearchDetails.flightFareSearchRQ.Segments[0].Origin.AirportCityName + "</span></span>" +
                "<span class='indicators'>" +
                    "<i class='fa fa-arrow-left' aria-hidden='true'></i>" +
                    "<i class='fa fa-arrow-right' aria-hidden='true'></i>" +
                "</span>" +
                "<span class='destination'><span class='iata'>" + oSearchDetails.flightFareSearchRQ.Segments[0].Destination.AirportCityCode + "</span><span class='name'>" + oSearchDetails.flightFareSearchRQ.Segments[0].Destination.AirportCityName + "</span></span>" +
            "</li>" +
            "<li class='allDates hover'>" +
                "<span>" + oSearchDetails.flightFareSearchRQ.Segments[0].Date + "</span> - <span>" +
                    "<i class='fa fa-calendar' aria-hidden='true'></i>" +

                "</span>" +
            "</li>" +
            "<li class='outboundDate hover'>" +
                "<label>Out</label><span>" +
                    "<i class='fa fa-calendar' aria-hidden='true'></i>" +
                      oSearchDetails.flightFareSearchRQ.Segments[0].Date +
                "</span>" +
            "</li>";
        if (oSearchDetails.flightFareSearchRQ.Segments.Count() == 2)
        {
            searchSummary += @"<li class='returnDate hover'>" +
                "<label>Rtn</label><span>" +
                    "<i class='fa fa-calendar' aria-hidden='true'></i>" +
                     oSearchDetails.flightFareSearchRQ.Segments[1].Date +
                "</span>" +
            "</li>";
        }
        searchSummary += @"<li class='actions hover collapsed' data-toggle='collapse' data-target='#demo'>" +
                "<label><i class='fa fa-pencil' aria-hidden='true'></i>Modify search</label>" +
            "</li>" +
        "</ul>";

    }
    private void bindFares(EL.Flight.Itineraries itins)
    {
        List<EL.Flight.Itinerary> distinctItins = itins.Items.DistinctBy(m => new { m.ValCarrier, m.GrandTotal }).ToList();

        foreach (EL.Flight.Itinerary itin in distinctItins)
        {
            string PCCs = string.Empty;
            int AlternateTiming = itins.Items.Count(c => (c.ValCarrier == itin.ValCarrier) && (c.GrandTotal == itin.GrandTotal));
           IEnumerable<EL.Flight.Itinerary> iii =  itins.Items.DistinctBy(c => (c.ValCarrier == itin.ValCarrier) && (c.GrandTotal == itin.GrandTotal)).DistinctBy(d=> (d.PCC));
            if (iii.Count() == 1)
            { PCCs = itin.PCC; }
            else
            {
                foreach (EL.Flight.Itinerary pcc in iii.ToList())
                {
                    PCCs += pcc.PCC + "/";
                }
                PCCs = PCCs.Remove(PCCs.Length - 1);
            }
            fares += bindFares(itin, AlternateTiming,PCCs);
        }
    }
    private string bindFares(EL.Flight.Itinerary itin, int AlternateTiming,string PCCs)
    {

        string journeyType = string.Empty;

        EL.Flight.Itinerary_Sector sectRet = itin.Sectors.Find(
              delegate(EL.Flight.Itinerary_Sector sect)
              {
                  return sect.Group == "1";
              }
              );

        if (sectRet != null)
        { journeyType = "Round Trip"; }
        else { journeyType = "One way"; };
        string str = string.Empty; ;
        str = com.GetStopOvers(itin, "Outbound", ref stops);
        str = "clas" + str.Replace(" ", "");
        str = (str == "clas2stops") ? "clas2stop" : str;
        DynUrl = "";
        string fare = "<div class='catalog-row cls" + itin.Sectors[0].AirV + " " + str + " ' id='dv" + itin.IndexNumber + itin.Provider + "'>" +

                            "<!-- // flight-item // -->" +
                            "<div class='flight-item fly-in'>" +
                                "<div class='flt-i-a'>" +
                                    "<div class='flt-i-b'>";
        fare += flightDetails(itin, "outbound");
        
        if (sectRet != null) { fare += flightDetails(itin, "inbound"); }

        fare += "</div>" +
                                "</div>" +
                                "<div class='flt-i-c'>" +
                                    //"<p class='flt-i-padding'><strong>"+ itin.PCC+ "</strong></p>" +
                                    "<p class='flt-i-padding'><strong>" + PCCs + "</strong></p>" +
                                    "<div class='flt-i-padding'>" +
                                        "<div class='flt-i-price'>" + itin.CurrencySymble + Math.Round(itin.GrandTotal / (itin.AdultInfo.NoAdult + itin.ChildInfo.NoChild + itin.InfantInfo.NoInfant), 2) + "</div>" +
                                        "<div class='flt-i-price-b'>avg/person</div>" +
                                        "<div class='flt-i-price-b'></div>" +
                                        "ADT -" + (itin.AdultInfo.AdtBFare + itin.AdultInfo.AdTax + itin.AdultInfo.MarkUp + itin.AdultInfo.Commission) + "<br />" +
                                        "CHD -" + (itin.ChildInfo.ChdBFare + itin.ChildInfo.CHTax + itin.ChildInfo.MarkUp + itin.ChildInfo.Commission) + "<br />" +
                                        "INF -" + (itin.InfantInfo.InfBFare + itin.InfantInfo.InTax + itin.InfantInfo.MarkUp + itin.InfantInfo.Commission) + "<br />" +
                                    "</div>" +
                                "</div>" +
                                "<div class='clear'></div>" +
                            "</div>" +
                            "<!-- \\ flight-item \\ -->" +
                        "</div>";

       
        return fare;

    }
    private string outboundSummary(EL.Flight.Itinerary itin)
    {
        try
        {
            EL.Flight.Itinerary_Sector sectOut1 = itin.Sectors.Find(
              delegate(EL.Flight.Itinerary_Sector sect)
              {
                  return sect.IsReturn != true;
              }
              );

            EL.Flight.Itinerary_Sector sectOut2 = itin.Sectors.FindLast(
                delegate(EL.Flight.Itinerary_Sector sect)
                {
                    return sect.IsReturn != true;
                }
                );


            #region Outbound Summary
            if (sectOut1 != null && sectOut2 != null)
            {

                return "<div class='ng-scope'><div class='col-md-12 col-xs-12 p-0'><div class='col-xs-6 p-0'><div class='pull-left stripes txt-orange ng-isolate-scope'>" +
                "<img src='//www.traveljunction.co.uk/images/outbound.png' width='16' height='16' alt='outbound'>DEPARTURE</div></div>" +
          "<div class='col-xs-6 p-0'><div class='pull-right'><ul class='list-inline'>" +
                "<li class='ng-scope'>" +
                "<small>" + sectOut1.AirlineName + "</small><img class='ng-isolate-scope' src='//www.flightxpertuk.com/images/airlinelogo/" + sectOut1.AirV + "s.gif'>" +
                    "</li></ul></div></div></div><div class='results_item_row'>" +
          "<div style='width: 15%;' class='pull-left text-right red  strong'> <span class='big ng-binding mr-10 txt-orange'>" + sectOut1.Departure.Time + "</span>" +
            "<div class='small text-gray ellipsis ng-binding mr-10'>" + Convert.ToDateTime(sectOut1.Departure.Date).ToString("ddd, dd MMM") + "</div>" +
          "</div>" +
          "<div  style='width: 25%;' class='pull-left ellipsis p-l5'> <span class='big strong ng-isolate-scope'><span class='bigger ng-binding ng-scope'>" + sectOut1.Departure.AirportCode + "</span></span>" +
            "<div class='small text-gray ellipsis ng-binding'>" + sectOut1.Departure.AirportCityName + "</div>" +
          "</div>" +
          "<div style='width: 20%;' class='pull-left text-center dotted-bdr-left dotted-bdr-right'><span>" + com.GetTotalJourneyTime(itin, "Outbound") + "</span>" +
          "<span><img src='//www.traveljunction.co.uk/images/" + com.GetStopOvers(itin, "Outbound", ref stops).Replace(" ", "-") + ".png' width='48' height='10'></span> <br>" +

           "<small>" + com.GetStopOvers(itin, "Outbound", ref stops) + "</small></div>" +
          "<div style='width: 15%' class='pull-left text-right red  strong'> <span class='big ng-binding mr-10 txt-orange'>" + sectOut2.Arrival.Time + "</span>" +
            "<div class='small text-gray ellipsis ng-binding mr-10'>" + Convert.ToDateTime(sectOut2.Arrival.Date).ToString("ddd, dd MMM") + "</div>" +
          "</div>" +
          "<div style='width: 25%;' class='pull-left ellipsis p-l5'> <span class='big strong ng-isolate-scope'><span class='bigger ng-binding ng-scope'>" + sectOut2.Arrival.AirportCode + "</span></span>" +
            "<div class='small text-gray ellipsis ng-binding'>" + sectOut2.Arrival.AirportCityName + "</div>" +
          "</div>" +
          "<div class='clear'></div></div></div>";
            }
            #endregion

            else
            { return ""; }
        }
        catch (Exception ex)
        {
            fares = ex.StackTrace;
            return "";
        }

    }
    private string inboundSummary(EL.Flight.Itinerary itin, int AlternateTiming)
    {
        try
        {
            string fare = string.Empty;
            EL.Flight.Itinerary_Sector sectIn1 = itin.Sectors.Find(
               delegate(EL.Flight.Itinerary_Sector sect)
               {

                   return sect.IsReturn == true;
               }
               );

            EL.Flight.Itinerary_Sector sectIn2 = itin.Sectors.FindLast(
                delegate(EL.Flight.Itinerary_Sector sect)
                {

                    return sect.IsReturn == true;
                }
                );
            if (sectIn1 != null && sectIn2 != null)
            {
                fare = "<div class='ng-scope border_top'><div class='col-md-12 col-xs-12 p-0'><div class='col-xs-6 p-0'><div class='pull-left stripes txt-orange ng-isolate-scope'>" +
                "<img src='//www.traveljunction.co.uk/images/return.png' width='16' height='16' alt='inbound'>RETURN</div></div>" +
          "<div class='col-xs-6 p-0'><div class='pull-right'><ul class='list-inline'>" +
                "<li class='ng-scope'>" +
                "<small>" + sectIn1.AirlineName + "</small><img class='ng-isolate-scope' src='//www.flightxpertuk.com/images/airlinelogo/" + sectIn1.AirV + "s.gif'>" +
                    "</li></ul></div></div></div><div class='results_item_row'>" +
          "<div style='width: 15%;' class='pull-left text-right red  strong'> <span class='big ng-binding mr-10 txt-orange'>" + sectIn1.Departure.Time + "</span>" +
            "<div class='small text-gray ellipsis ng-binding mr-10'>" + Convert.ToDateTime(sectIn1.Departure.Date).ToString("ddd, dd MMM") + "</div>" +
          "</div>" +
          "<div  style='width: 25%;' class='pull-left ellipsis p-l5'> <span class='big strong ng-isolate-scope'><span class='bigger ng-binding ng-scope'>" + sectIn1.Departure.AirportCode + "</span></span>" +
            "<div class='small text-gray ellipsis ng-binding'>" + sectIn1.Departure.AirportCityName + "</div>" +
          "</div>" +
          "<div style='width: 20%;' class='pull-left text-center dotted-bdr-left dotted-bdr-right'><span>" + com.GetTotalJourneyTime(itin, "Inbound") + "</span>" +
          "<span><img src='//www.traveljunction.co.uk/images/" + com.GetStopOvers(itin, "Inbound", ref stops).Replace(" ", "-") + ".png' width='48' height='10'></span> <br>" +

           "<small>" + com.GetStopOvers(itin, "Inbound", ref stops) + "</small></div>" +
          "<div style='width: 15%' class='pull-left text-right red  strong'> <span class='big ng-binding mr-10 txt-orange'>" + sectIn2.Arrival.Time + "</span>" +
            "<div class='small text-gray ellipsis ng-binding mr-10'>" + Convert.ToDateTime(sectIn2.Arrival.Date).ToString("ddd, dd MMM") + "</div>" +
          "</div>" +
          "<div style='width: 25%;' class='pull-left ellipsis p-l5'> <span class='big strong ng-isolate-scope'><span class='bigger ng-binding ng-scope'>" + sectIn2.Arrival.AirportCode + "</span></span>" +
            "<div class='small text-gray ellipsis ng-binding'>" + sectIn2.Arrival.AirportCityName + "</div>" +
          "</div>" +
          "<div class='clear'></div></div>" +
          "<div class='border_top'>" +
          "<ul class='list-inline'>" +
            "<li class='ng-scope'> <a class='txt-dark' data-toggle='collapse' data-target='#flight-details-info" + itin.IndexNumber + "'><i class='fa fa-info-circle'></i> Flight Detail</a> </li>";
                if (AlternateTiming > 1)
                {
                    fare += "<li class='ng-scope'> <a href='alternateflights.aspx?id=" + Request.QueryString.Get("id") + "&airv=" + itin.ValCarrier + "&amt=" + itin.GrandTotal + "' class='txt-dark'> Alternate Timing(" + AlternateTiming + ") </a></li>";
                }


                fare += "<li class='pull-right ng-binding ng-scope'> <i class='fa fa-briefcase' aria-hidden='true'></i> Baggage " + itin.Sectors[0].BaggageInfo + " Included </li>" +
          "</ul></div></div>";

                return fare;
            }
            else
            {
                //no inbound sectors found
                fare = "<div class='border_top'>" +
          "<ul class='list-inline'>" +
            "<li class='ng-scope'> <a class='txt-dark' data-toggle='collapse' data-target='#flight-details-info" + itin.IndexNumber + "'><i class='fa fa-info-circle'></i> Flight Detail</a> </li>";
                if (AlternateTiming > 1)
                {
                    fare += "<li class='ng-scope'> <a href='alternateflights.aspx' class='txt-dark' > Alternate Timing(" + AlternateTiming + ") </a></li>";
                }
                fare += "<li class='pull-right ng-binding ng-scope'> <i class='fa fa-briefcase' aria-hidden='true'></i> Baggage " + itin.Sectors[0].BaggageInfo + " Included </li>" +
         "</ul></div>";
                return fare;

            }
        }
        catch (Exception ex)
        {
            fares = ex.StackTrace;
            return "";
        }
    }


    private string flightDetails(EL.Flight.Itinerary itin, string type)
    {
        string SectorsDetails = string.Empty;
        List<EL.Flight.Itinerary_Sector> sects = null;
        string strDet = string.Empty;
        sects = com.GetSectores(itin, type, ref stops);
        EL.Flight.Itinerary_Sector first = com.getFirstOUT_IN_Sectors(itin, type);
        EL.Flight.Itinerary_Sector last = com.getLastOUT_IN_Sectors(itin, type);

        if (type == "outbound")
        {
           
            strDet = "<div class='flt-i-bb'>" +
                                            "<div class='flt-l-a'>" +

                                                "<div class='flt-l-c'>" +
                                                    "<div class='flt-l-cb'>" +
                                                        "<div class='flt-l-c-padding'>";

            strDet += "<div class='flyght-info-head'>" +
                                                               "<div class='way-lbl'>Departure</div>" +
                                                               Convert.ToDateTime(first.Departure.Date).ToString("ddd, dd MMM") + " &nbsp;&nbsp;&nbsp;" + first.Departure.AirportCode + " - " + last.Arrival.AirportCode + " &nbsp;&nbsp;&nbsp; Journey Time - " + com.GetTotalJourneyTime(itin, "outbound") + "</div>";
            
            //strDet = "<div class='col-md-12 col-xs-12 p-0'><div class='col-xs-12 stripes-dtl'><div class='pull-left ng-isolate-scope txt-orange'><img src='//www.traveljunction.co.uk/images/outbound.png' width='16' height='16' alt='outbound'> OUTBOUND FLIGHT DETAIL</div><div class='pull-right ng-isolate-scope txt-orange'>Total Journey : " +

            //                            com.GetTotalJourneyTime(itin, "outbound") +
            //                        "</div></div></div><div class='clear'></div>";
            int a = sects.Count;
        }
        else if (type == "inbound")
        {
            strDet = "<div class='flt-i-bb flight-return'>" +
                                            "<div class='flt-l-a'>" +

                                                "<div class='flt-l-c'>" +
                                                    "<div class='flt-l-cb'>" +
                                                        "<div class='flt-l-c-padding'>";

            strDet += "<div class='flyght-info-head'>" +
                                                               "<div class='way-lbl'>Return</div>" +
                                                                Convert.ToDateTime(first.Departure.Date).ToString("ddd, dd MMM") + " &nbsp;&nbsp;&nbsp;" + first.Departure.AirportCode + " - " + last.Arrival.AirportCode + " &nbsp;&nbsp;&nbsp; Journey Time - " + com.GetTotalJourneyTime(itin, "inbound") + "</div>";

            
        }
        foreach (EL.Flight.Itinerary_Sector sect in sects)
        {

            SectorsDetails += "<div class='flight-line'>" +
                                                                "<div class='flight-line-a'>" +
                                                                    "<span class='flight-line-a'><b>" + sect.AirV + "&nbsp;" + sect.FltNum + "</b></span>" +
                                                                "</div>" +
                                                                "<div class='flight-line-d'></div>" +
                                                                "<div class='flight-line-a'>" +
                                                                    "<b>" + sect.Departure.Time + "&nbsp;&nbsp;" + sect.Departure.AirportCode + "</b>" +

                                                                    "<span>" + Convert.ToDateTime(sect.Departure.Date).ToString("ddd, dd MMM yyyy") + "</span>" +
                                                                "</div>" +
                                                                "<div class='flight-line-d'></div>" +
                                                                "<div class='flight-line-a'>" +
                                                                    "<b>" + sect.Arrival.Time + "&nbsp;&nbsp;" + sect.Arrival.AirportCode + "</b>" +

                                                                    "<span>" + Convert.ToDateTime(sect.Arrival.Date).ToString("ddd, dd MMM yyyy") + "</span>" +
                                                                "</div>" +
                                                                "<div class='flight-line-d'></div>" +
                                                                "<div class='flight-line-a'>" +
                                                                    "<b>time</b>" +
                                                                    "<span>" + sect.ElapsedTime + "</span>" +
                                                                "</div>" +
                                                                "<div class='flight-line-d'></div>" +
                                                                "<div class='flight-line-a'>" +
                                                                    "<span></span>" +
                                                                "</div>" +


                                                            "</div>";

            
        }

                               SectorsDetails += "</div>" +
                                                    "</div>"+
                                                    "<br class='clear' />" +
                                                "</div>" +
                                            "</div>" +
                                            "<div class='clear'></div>" +
                                        "</div>" +
                                        "<br class='clear' />";





        
        return strDet + SectorsDetails;
    }
    private void bindFlightFilter(EL.Flight.Itineraries itins)
    {
        var obj = itins.Items.DistinctBy(x => x.Sectors[0].AirV);

        foreach (var iti in obj.ToList())
        {

            airlinesFilter += "<div class='checkbox'>" +
                "<label style='width:100% !important;'>" +
                  "<div class='pull-left'><input type='checkbox' checked='true' class='mr-10' onclick='filterAirlin(this.id)' id='" + iti.Sectors[0].AirV + "' value='" + iti.Sectors[0].AirV + "'>" +
            iti.Sectors[0].AirlineName + "</div><div class='pull-right'>" + iti.CurrencySymble + Math.Round((iti.GrandTotal / (iti.AdultInfo.NoAdult + iti.ChildInfo.NoChild + iti.InfantInfo.NoInfant)), 2) + "</div>"+

            "</label>" +
          "</div>";

           
        }
        bindStops(itins);

    }
    private void bindStops(EL.Flight.Itineraries itins)
    {
        string currencySymble = itins.Items[0].CurrencySymble;
        string str = string.Empty;
        foreach (KeyValuePair<int, double> stp in stops.OrderBy(x => x.Key))
        {
            str = (stp.Key == 0) ? "nonstop" : stp.Key + "stop";

            stopOversFilter += "<div class='checkbox'>" +
                "<label style='width:100% !important;'>" +
                  "<div class='pull-left'><input type='checkbox' checked='true' onclick='filterStop(this.id)' class='mr-10' id='" + str + "' value='" + stp.Key + "'></div>";
            stopOversFilter += (stp.Key == 0) ? "non stop" : (stp.Key == 1 ? stp.Key + " stop" : stp.Key + " stops");
            stopOversFilter += "<div class='pull-right'>" + currencySymble + stp.Value + "</div>"+
                "</label>" +
              "</div>";
            //stopOversFilter += "<div class='radio-btn-wrapper'>" +
            //                   "<input type='checkbox' checked='true' onclick='filterStop(this.id)' class='mr-10' id='" + str + "' value='" + stp.Key + "'>" +
            //                   "<label for='bungalow' style='font-weight: normal !important;'>";
            //stopOversFilter += (stp.Key == 0) ? "non stop" : stp.Key + " stop";
            //stopOversFilter += " </label>" +
            //                   "<span class='count'>" + currencySymble + stp.Value + "</span>" +
            //               "</div>";
        }
    }
    //check campaign media
    private void CheckChoosenFare(EL.Flight.Itineraries items, string totalPrice, string valCarrier, string index)
    {
        List<EL.Flight.Itinerary> iten = (from EL.Flight.Itinerary i in items.Items
                                          where Convert.ToDouble(i.GrandTotal).ToString("f2") == Convert.ToDouble(totalPrice).ToString("f2") && i.ValCarrier.ToUpper() == valCarrier.ToUpper() && i.IndexNumber == Convert.ToInt16(index)
                                          select i).ToList();
        if (iten == null || iten.Count == 0)
        {

            iten = (from EL.Flight.Itinerary i in items.Items
                    where Convert.ToDouble(i.GrandTotal).ToString("f2") == Convert.ToDouble(totalPrice).ToString("f2") && i.ValCarrier.ToUpper() == valCarrier.ToUpper()
                    select i).ToList();
        }
        if (iten.Count > 0)
        {
            Response.Redirect("~/passengerDetails.aspx?id=" + Request.QueryString.Get("id") + "&inx=" + iten[0].IndexNumber + "&sup=" + iten[0].Provider, false);
        }

    }
    public void SearchLowFares(object someParameter)
    {
        EL.Trip_Type TT = EL.Trip_Type.Return_Trip;

        if (_hfjtype.Value == "O")
        { TT = EL.Trip_Type.OneWay_Trip; }
        else if (_hfjtype.Value == "R")
        { TT = EL.Trip_Type.Return_Trip; }
        // EL.Trip_Type TT = journeyType.Checked ? EL.Trip_Type.Return_Trip : EL.Trip_Type.OneWay_Trip;
        //DefaultCredential cred = new DefaultCredential();

        //BLL.FlightsBL OLowfareSearch = new BLL.FlightsBL();
        //searchID = Guid.NewGuid();

        //OLowfareSearch.LowFareSearch(_hfFrom.Value, _hfTo.Value, _hfDepart.Value, _hfReturn.Value,
        //       TT, _hfClass.Value, Convert.ToInt32(_hfChild.Value), Convert.ToInt32(_hfInfant.Value),
        //       Convert.ToInt32(_hfInfant.Value), Convert.ToBoolean(_hfFlexible.Value), false ? 3 : 0, _hfAirline.Value, cred.Default_Credential.Company_ID,
        //       cred.Default_Credential.Hap, cred.Default_Credential.Hap_Password, cred.Default_Credential.Hap_Type, searchID.ToString(), someParameter);

    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            SearchLowFares(HttpContext.Current);

            SecureQueryString qs = new SecureQueryString();

            filePath = HttpContext.Current.Server.MapPath("~/App_Data/Result/" + searchID.ToString() + ".txt");
            SearchAsynch osysnch = new SearchAsynch();
            IAsyncResult ar = osysnch.FlightSearchAsync(SearchDetails.Current(searchID.ToString()), filePath);
            Session["fltResult"] = ar;
            //if (chkIsFlexi.Checked)                
            // qs["redirectPage"] = "resultflx.aspx";
            // else
            qs["redirectPage"] = "flight-result.aspx";

            qs["searchID"] = searchID.ToString();
            SearchDetails oSearch = SearchDetails.Current(searchID.ToString());
            qs["origin"] = oSearch.flightFareSearchRQ.Segments[0].Origin.AirportCityName;
            qs["destination"] = oSearch.flightFareSearchRQ.Segments[0].Destination.AirportCityName;
            qs["originCode"] = oSearch.flightFareSearchRQ.Segments[0].Origin.AirportCode;
            qs["destinationCode"] = oSearch.flightFareSearchRQ.Segments[0].Destination.AirportCode;
            qs["DepartDate"] = oSearch.flightFareSearchRQ.Segments[0].Date;
            if (oSearch.flightFareSearchRQ.Segments.Count == 2)
            { qs["ReturnDate"] = oSearch.flightFareSearchRQ.Segments[1].Date; ; }
            else
            { qs["ReturnDate"] = ""; }

            Response.Redirect("~/wait.aspx?q=" + qs.ToString());
        }
    }
}