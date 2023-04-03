using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI;
using BLL;
using FandHServices;
using mailServices;

public partial class Admin_FlightQuote2 : System.Web.UI.Page
{
    readonly FandHServicesClient obj = new FandHServicesClient();
    readonly GetSetDatabase objGetSetDatabase = new GetSetDatabase();
    public static DataTable dtFetchPassengerRequest = new DataTable();
    readonly WeatherInfo Weather_Info = new WeatherInfo();
    public bool Btn_save_pref_Status = false;
    DataTable dtPax = new DataTable();
    DataTable dtSector = new DataTable();
    DataTable dtComp = new DataTable();
    DataTable dtContact = new DataTable();
    public string XPDetails { get; set; }
    public string Terms { get; set; }

    private double Price_Payable_Amount = 0;
    readonly string https_URL = "https://";
    readonly Layout lo = new Layout();
    UserDetail objUserDetail;
    public string Success_Message = "Thank you for confirming your reservation.<br>If you have any questions please feel free to call us on<a href='tel:0203 745 3778'><i class='fa fa-phone-square'></i>&nbsp;0203 745 3778</a>.";



    protected void Page_Load(object sender, EventArgs e)
    {
        objUserDetail = Session["UserDetails"] as UserDetail;
        if (Session["UserDetails"] != null)
        {

            DataTable dt = objGetSetDatabase.GET_Amount_Charges_Detail(Common.DecryptString(Request.QueryString.Get("BID"), ""), "001", "", "", "", "", "", "", "");
            Price_Payable_Amount = Math.Round(Convert.ToDouble(dt.Compute("Sum(SellPrice)", string.Empty)), 2);

            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Common.DecryptString(Request.QueryString.Get("BID"), "")))
                {
                    if (Check_Passenger_Status(Common.DecryptString(Request.QueryString.Get("BID"), "")))
                    {
                        Btn_save_pref_Status = true;
                    }
                    else
                    {
                        Btn_save_pref_Status = false;
                    }

                    string xp = Common.DecryptString(Request.QueryString.Get("BID"), "");
                    XPDetails = BookingDetails(xp);
                }
            }
        }
        else
        {
            //Response.Redirect("~/Login.aspx", false);
        }
    }

    private string BookingDetails(string XP)
    {
        dtPax = obj.GET_Passenger_Detail(XP, "001");
        dtSector = objGetSetDatabase.GET_Sector_Detail(XP, "001");
        dtComp = obj.GET_Booking_Master(XP);
        dtContact = obj.GET_Contact_Detail(XP, "001");

        WeatherDetails(dtSector.AsEnumerable().Where(s => s.Field<string>("TripID") == "0").Select(s => s.Field<string>("ToCityName")).LastOrDefault());

        string htmlpageurl = "~/Admin/FlightQuote2.aspx";

        if (txtXPTo.Text.Trim() == "")
        { txtXPTo.Text = dtContact.Rows[0]["EmailID"].ToString(); }

        //FLTUK
        string authorizeTravelPerformance = string.Empty;

        if (dtComp.Rows[0]["BookingByCompany"].ToString() == "FLTUK")
        {
            authorizeTravelPerformance = "<tr ><td><input type='checkbox' checked='checked' /> I authorize Travel-Performance to change the flights on my behalf.</td></tr><tr style='border-bottom: 1pt solid black;'><td></td> </tr>";
        }

        StreamReader reader = new StreamReader(System.Web.Hosting.HostingEnvironment.MapPath(htmlpageurl));
        string readFile = reader.ReadToEnd();
        string myString = readFile;

        #region Company Logo on main Head
        StringBuilder Company_Logo_URL = new StringBuilder();
        Company_Logo_URL.Append(WebsiteStaticData.WebsiteUrl + "images/logoes/" + ((dtComp != null && dtComp.Rows.Count > 0) ? lo.SetCompanyDetail(dtComp.Rows[0]["BookingByCompany"].ToString()).Comp_logo : "") + ".png");
        #endregion

        #region Company Address
        StringBuilder Company_Address = new StringBuilder();
        Company_Address.Append(dtComp != null && dtComp.Rows.Count > 0 ? lo.SetCompanyDetail(dtComp.Rows[0]["BookingByCompany"].ToString()).Comp_Address : "");
        #endregion

        #region Reservation Tab
        StringBuilder Itin_Div = new StringBuilder();
        if (dtComp.Rows[0]["CurrencyType"].ToString() != "CAD")
        {
            foreach (DataRow drFlight in dtSector.Rows)
            {
                Itin_Div.Append("<div class='itinerary-info'>"
                    + "<div class='col-md-3 col-sm-3 col-xs-12 itinerary-info-sub'>"
                    + "<h3>"
                    + drFlight["FromDestName"].ToString()
                    + " to "
                    + drFlight["ToDestName"].ToString()
                    + "</h3> "
                    + "<span>"
                    + drFlight["AirlineName"].ToString()
                    + "</span> "
                    + "</div> "
                    + "<div class='col-md-3 col-sm-3 col-xs-12 itinerary-info-sub'>"
                    + "<h4>Flight</h4>"
                    + "<span>"
                    + Convert.ToDateTime(drFlight["FromDateTime"]).ToString("ddd, dd MMM yyyy")
                    + "</span><span class='dep-time-mob'>"
                    + "<b>Departure</b><br>"
                    + Convert.ToDateTime(drFlight["ToDateTime"]).ToString("H:mm")
                    + "</span>"
                    + "</div>"
                    + "<div class='col-md-3 col-sm-3 hidden-xs itinerary-info-sub'>"
                    + "<h4>Departs</h4>"
                    + "<span>"
                    + Convert.ToDateTime(drFlight["FromDateTime"]).ToString("H:mm")
                    + "</span>"
                    + "</div>"
                    + "<div class='col-md-3 col-sm-3 hidden-xs itinerary-info-sub'>"
                    + "<h4>Arrival</h4>"
                    + "<span>"
                    + Convert.ToDateTime(drFlight["ToDateTime"]).ToString("H:mm")
                    + "</span>"
                    + "</div>"
                    + "<div class='clearfix'></div></div>");
            }
            #endregion
        }
        else
        {
            DataTable dtSector1 = new DataTable();
            dtSector1 = objGetSetDatabase.GET_Sector_Detail_NO_XP(XP, "Select");
            if (dtSector1 != null)
            {
                if (dtSector1.Rows.Count > 0)
                {
                    foreach (DataRow drFlight in dtSector1.Rows)
                    {
                        Itin_Div.Append(Convert.ToString(drFlight["BNP_BOOKING_NO_XP"]));
                    }
                }
            }
        }

        #region Itinerary Tab
        StringBuilder Itin_Pax_detail = new StringBuilder();
        StringBuilder Itin_section = new StringBuilder();

        Itin_Pax_detail.Append("<span class='text12'></span>");


        if(dtPax != null)
        {
            if(dtPax.Rows.Count > 0)
            {
                foreach (DataRow drPax in dtPax.Rows)
                {
                    if (dtPax.Rows.IndexOf(drPax) == 0)
                    {
                        Itin_Pax_detail.Append("<span class='text12'> " + Convert.ToString(drPax["Title"]) + " " + Convert.ToString(drPax["FName"]) + " " + Convert.ToString(drPax["MName"]) + " " + Convert.ToString(drPax["LName"]) + "</span>");
                    }
                    else
                    {
                        Itin_Pax_detail.Append(",<span class='text12'> " + Convert.ToString(drPax["Title"]) + " " + Convert.ToString(drPax["FName"]) + " " + Convert.ToString(drPax["MName"]) + " " + Convert.ToString(drPax["LName"]) + "</span>");
                    }
                }
            }
        }

        



        if (dtComp.Rows[0]["CurrencyType"].ToString() == "GBP")
        {
            foreach (DataRow drFlight in dtSector.Rows)
            {
                if (drFlight["TripID"].ToString() == "0") // outbound
                {
                    Itin_section.Append("<div class='col-md-12 col-sm-12 col-xs-12' style='position:relative'>" +
                        "<div class='col-xs-12'>" +
                            "<span class='fligh-bound'>" +
                                "<span class='icon icon-air'></span>" +
                                "Departure" +
                            "</span>" +
                        "</div>" +
                        "<div class='col-md-2 col-sm-2'>" +
                            "<span class='dep-date'>" + drFlight["AirlineName"].ToString() + "</span>" +
                        "</div>" +
                        "<div class='col-xs-3 air-info text-center pt15 hidden'>" +
                            "<span class='block fs10 mt5'></span>" +
                        "</div>" +
                        "<div class='col-md-3 col-sm-3 col-xs-6 offset-m-0 dep-info flight-desc text-right'>" +
                            "<span class='city'></span>" +
                            "<span class='dep-time'>" + drFlight["FromDest"] + " | " + Convert.ToDateTime(drFlight["FromDateTime"]).ToString("H:mm") + "</span>" +
                            "<span class='dep-date'>" + Convert.ToDateTime(drFlight["FromDateTime"]).ToString("ddd, dd MMM yyyy") + "</span>" +
                            "<span class='airport'>" + drFlight["FromDestName"] + ", " + drFlight["TerminalFrom"] + "</span>" +
                            "<span class='airport'>" + drFlight["FromCityName"] + "</span>" +
                        "</div>" +
                        "<div class='col-md-4 col-sm-4 text-center pt15 cabin-class'>" +
                            "<span>"+ Common.GetCabinClassChangeValue(Convert.ToString(drFlight["CabinClass"])) + "</span>" +  
                    "<div class='line-flight'></div>" +
                            "<span></span>" +
                        "</div>" +
                        "<div class='col-md-3 col-sm-3 col-xs-6 offset-m-0 arr-info flight-desc text-left'>" +
                            "<span class='city'></span>" +
                            "<span class='dep-time'>" + drFlight["ToDest"] + " | " + Convert.ToDateTime(drFlight["ToDateTime"]).ToString("H:mm") + "</span>" +
                            "<span class='dep-date'>" + Convert.ToDateTime(drFlight["ToDateTime"]).ToString("ddd, dd MMM yyyy") + "</span>" +
                            "<span class='airport'>" + drFlight["ToDestName"] + ", " + drFlight["TerminalTo"] + "</span>" +
                            "<span class='airport'>" + drFlight["ToCityName"] + "</span>" +
                        "</div>" +
                        "<div class='clearfix'></div>" +
                    "</div>");
                }
                if (drFlight["TripID"].ToString() == "1") // inbound
                {
                    Itin_section.Append("<div class='col-md-12 col-sm-12 col-xs-12' style='position:relative'>" +
                                "<div class='col-xs-12'>" +
                                    "<span class='fligh-bound'>" +
                                        "<span class='icon icon-air icon-return'></span>Return" +
                                    "</span>" +
                                "</div>" +
                                "<div class='col-md-2 col-sm-2'>" +
                                    "<span class='dep-date'>" + drFlight["AirlineName"].ToString() + "</span> " +
                                "</div> " +
                                "<div class='col-xs-3 air-info text-center pt15 hidden'>" +
                                "</div>" +
                                "<div class='col-md-3 col-sm-3 col-xs-6 offset-m-0 dep-info flight-desc text-right'>" +
                                    "<span class='city'></span>" +
                                         "<span class='dep-time'>" + drFlight["FromDest"] + " | " + Convert.ToDateTime(drFlight["FromDateTime"]).ToString("H:mm") + "</span>" +
                                        "<span class='dep-date'>" + Convert.ToDateTime(drFlight["FromDateTime"]).ToString("ddd, dd MMM yyyy") + "</span>" +
                                        "<span class='airport'>" + drFlight["FromDestName"] + ", " + drFlight["TerminalFrom"] + "</span>" +
                                        "<span class='airport'>" + drFlight["FromCityName"] + "</span>" +
                                "</div>" +
                                "<div class='col-md-4 col-sm-4 text-center pt15 cabin-class'>" +
                                    "<span>" + Common.GetCabinClassChangeValue(Convert.ToString(drFlight["CabinClass"])) + "</span>" +
                                    "<div class='line-flight'></div>" +
                                    "<span></span>" +
                                "</div>" +
                                "<div class='col-md-3 col-sm-3 col-xs-6 offset-m-0 arr-info flight-desc text-left'>" +
                                    "<span class='city'></span>" +
                                        "<span class='dep-time'>" + drFlight["ToDest"] + " | " + Convert.ToDateTime(drFlight["ToDateTime"]).ToString("H:mm") + "</span>" +
                                        "<span class='dep-date'>" + Convert.ToDateTime(drFlight["ToDateTime"]).ToString("ddd, dd MMM yyyy") + "</span>" +
                                        "<span class='airport'>" + drFlight["ToDestName"] + ", " + drFlight["TerminalTo"] + "</span>" +
                                        "<span class='airport'>" + drFlight["ToCityName"] + "</span>" +
                                "</div>" +
                                "<div class='clearfix'></div>" +
                            "</div>");
                }
            }
        }
        else
        {
            DataTable dtItinDetail = new DataTable();
            dtItinDetail = objGetSetDatabase.GET_Sector_Detail_NO_XP(XP, "Select");

            if (dtItinDetail != null && dtItinDetail.Rows.Count > 0)
            {
                Itin_section.Append(Convert.ToString(dtItinDetail.Rows[0]["BNP_Booking_NO_XP"]));
            }
        }
        #endregion

        #region Baggage Details inside Itineary Tab

        StringBuilder Baggage_Deatil = new StringBuilder();
        string _baggage = string.Empty;
        if (dtSector != null  && dtSector.Rows.Count > 0)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(dtSector.Rows[0]["BaggageAllownce"])))
            {
                _baggage = (Convert.ToString(dtSector.Rows[0]["BaggageAllownce"]) + " check-in baggage included in the fare");
            }
            else
            {
                _baggage = " No baggage included in the fare";
            }
        }
        else
        {
            _baggage = " No baggage included in the fare";
        }


        Baggage_Deatil.Append("<div class='col-md-12 col-sm-12 col-xs-12 offset-m-0 p-0'>" +
                    "<div class='widget stacked widget-table action-table'>" +
                        "<div class='widget-content'>" +
                            "<div class='scroll-x'>" +
                                "<table class='table table-striped table-bordered price font13'>" +
                                    "<tbody style = 'text-align:justify;'> " +
                                        "<tr> " +
                                            "<td style='width:50%;'>Change Before Depart</td>" +
                                            "<td> Not Permitted</td>" +
                                        "</tr>" +
                                        "<tr>" +
                                            "<td>Change After Depart</td>" +
                                            "<td> Not Permitted </td>" +
                                        "</tr>" +
                                        "<tr>" +
                                            "<td>Cancel Before Depart</td>" +
                                            "<td> No Refunds </td>" +
                                        "</tr>" +
                                        "<tr>" +
                                            "<td>Cancel After Depart</td>" +
                                            "<td> No Refunds </td>" +
                                        "</tr>" +
                                        "<tr>" +
                                            "<td>Baggage</td>" +
                                            "<td> " + _baggage + "</td>" +
                                        "</tr>" +
                                    "</tbody>" +
                                "</table>" +
                            "</div>" +
                        "</div>" +
                    "</div>" +
                "</div>");
        #endregion

        #region Passenger Deatils Tab


        StringBuilder Passenger_Details = new StringBuilder();
        StringBuilder Add_Pax = new StringBuilder();

        foreach (DataRow drPax in dtPax.Rows)
        {
            var DOB = Convert.ToString(drPax["DOB"]);
            if (!string.IsNullOrEmpty(DOB))
            {
                DOB = Convert.ToDateTime(drPax["DOB"]).ToString("dd MMM yyyy");
            }
            Add_Pax.Append("<tr>" +
                "<td>" + Convert.ToString(drPax["PaxID"].ToString()) + "</td>" +
                "<td>" + PassengerType(Convert.ToString(drPax["PaxType"].ToString())) + "</td>" +
                "<td>" + Convert.ToString(drPax["Title"]) + " " + Convert.ToString(drPax["FName"]) + " " + Convert.ToString(drPax["MName"]) + " " + Convert.ToString(drPax["LName"]) + "</td>" +
                "<td>" + DOB + "</td>" +
            "</td>");
        }

        Passenger_Details.Append("<table class='table table-striped table-bordered font13 mb-0'>" +
            "<thead class='fligh-bound'>" +
            "<tr>" +
                "<th style='width: 5%;' >#</th>" +
                "<th style='width: 15%;'>Pax Type</th>" +
                "<th>Passenger Name</th>" +
                "<th style='width: 15%;'>Date of Birth</th>" +
            "</tr> " +
            "</thead> " +
            "<tbody>" + Add_Pax.ToString() + "</tbody>" +
            "</table>");
        #endregion

        #region Price Tab

        StringBuilder Payment_Details = new StringBuilder();
        Payment_Details.Append("<div class='col-md-12 col-sm-12 col-xs-12 offset-m-0'>" +
        "<div class='widget stacked widget-table action-table'>" +
        "<div class='widget-content'>" +
        "<div class='scroll-x'>" +
            "<table class='table table-striped table-bordered price font13'>" +
                " <tbody style='text-align:justify;'> " +
                    "<tr> " +
                        "<td style='width:50%;'>Payable Amount</td>" +
                            "<td>£  &nbsp; " + Price_Payable_Amount + "</td>" +
                        "</tr>" +
                        "<td colspan='3'>*** Payment received is subject to card being successfully charged / Cheque clearance or Amount being reflected in our account in case of Bank Transfer." +
                            "</td>" +
                        "</tr>" +
                    "</tbody>" +
                "</table>" +
            "</div>" +
            "</div>" +
            "</div>" +
            "</div>");
        #endregion

        #region Special Request Tab

        StringBuilder special_Request = new StringBuilder();

        if (Btn_save_pref_Status)
        {
            foreach (DataRow dtPSR in dtFetchPassengerRequest.Rows)
            {
                special_Request.Append("<div class='col-md-12 col-sm-12 col-xs-12 list-border paxSR' Id=PAX_" + dtPSR["Passenger_perference_Id"] + ">" +
                "<h3><i class='fa fa-user' aria-hidden='true'></i>&nbsp;" + dtPSR["Passenger_perference_Passenger_Name"] + "</h3>" +
                "<hr />" +
                "<h5>MEAL PREFERENCE</h5>" +
                "<ul class='rdoList grid7 meal'>" +
                "<li>" +

                "<label title='VEGETARIAN HINDU MEAL'> " +
                "<input type='checkbox' value='AVML' disabled " + (Convert.ToString(dtPSR["Passenger_perference_Meal_perference"]) == "AVML" ? AutoFilledPassengerDetails(Convert.ToString(dtPSR["Passenger_perference_Meal_perference"]), "MEAL_PREFERENCE") : "") + " name='meal_pax_" + dtPSR["Passenger_perference_Id"] + "'/>" +
                "AVML" +
                "</label>" +
                "</li>" +
                "<li>" +
                "<label title='BABY MEAL'> " +
                "<input type='checkbox' value='BBML' disabled " + (Convert.ToString(dtPSR["Passenger_perference_Meal_perference"]) == "BBML" ? AutoFilledPassengerDetails(Convert.ToString(dtPSR["Passenger_perference_Meal_perference"]), "MEAL_PREFERENCE") : "") + "  name='meal_pax_" + dtPSR["Passenger_perference_Id"] + "'/>" +
                "BBML" +
                "</label>" +
                "</li>" +
                "<li>" +
                "<label title='BLAND MEAL'> " +
                "<input type='checkbox' value='BLML' disabled " + (Convert.ToString(dtPSR["Passenger_perference_Meal_perference"]) == "BLML" ? AutoFilledPassengerDetails(Convert.ToString(dtPSR["Passenger_perference_Meal_perference"]), "MEAL_PREFERENCE") : "") + " name='meal_pax_" + dtPSR["Passenger_perference_Id"] + "' />" +
                "BLML" +
                "</label>" +
                "</li>" +
                "<li>" +
                "<label title='CHILD MEAL'> " +
                "<input type='checkbox' value='CHML' disabled " + (Convert.ToString(dtPSR["Passenger_perference_Meal_perference"]) == "CHML" ? AutoFilledPassengerDetails(Convert.ToString(dtPSR["Passenger_perference_Meal_perference"]), "MEAL_PREFERENCE") : "") + " name='meal_pax_" + dtPSR["Passenger_perference_Id"] + "' />" +
                "CHML" +
                "</label" +
                "</li>" +
                "<li>" +
                "<label title='DIABETIC MEAL'> " +
                "<input type='checkbox' value='DBML' disabled " + (Convert.ToString(dtPSR["Passenger_perference_Meal_perference"]) == "DBML" ? AutoFilledPassengerDetails(Convert.ToString(dtPSR["Passenger_perference_Meal_perference"]), "MEAL_PREFERENCE") : "") + " name='meal_pax_" + dtPSR["Passenger_perference_Id"] + "' />" +
                "DBML" +
                "</label>" +
                "</li>" +
                "<li>" +
                "<label title='FRUIT PLATTER MEAL'> " +
                "<input type='checkbox' value='FPML' disabled " + (Convert.ToString(dtPSR["Passenger_perference_Meal_perference"]) == "FPML" ? AutoFilledPassengerDetails(Convert.ToString(dtPSR["Passenger_perference_Meal_perference"]), "MEAL_PREFERENCE") : "") + " name='meal_pax_" + dtPSR["Passenger_perference_Id"] + "' />" +
                "FPML" +
                "</label>" +
                "</li>" +
                "<li>" +
                "<label title='GLUTEN INTOLERANT MEAL'> " +
                "<input type='checkbox' value='GFML' disabled " + (Convert.ToString(dtPSR["Passenger_perference_Meal_perference"]) == "GFML" ? AutoFilledPassengerDetails(Convert.ToString(dtPSR["Passenger_perference_Meal_perference"]), "MEAL_PREFERENCE") : "") + " name='meal_pax_" + dtPSR["Passenger_perference_Id"] + "' />" +
                "GFML" +
                "</label>" +
                "</li>" +
                "<li>" +
                "<label title='HINDU - NON VEGETARIAN MEAL'> " +
                "<input type='checkbox' value='HNML' disabled " + (Convert.ToString(dtPSR["Passenger_perference_Meal_perference"]) == "HNML" ? AutoFilledPassengerDetails(Convert.ToString(dtPSR["Passenger_perference_Meal_perference"]), "MEAL_PREFERENCE") : "") + " name='meal_pax_" + dtPSR["Passenger_perference_Id"] + "' />" +
                "HNML" +
                "</label>" +
                "</li>" +
                "<li>" +
                "<label title='KOSHER MEAL'> " +
                "<input type='checkbox' value='KSML' disabled " + (Convert.ToString(dtPSR["Passenger_perference_Meal_perference"]) == "KSML" ? AutoFilledPassengerDetails(Convert.ToString(dtPSR["Passenger_perference_Meal_perference"]), "MEAL_PREFERENCE") : "") + " name='meal_pax_" + dtPSR["Passenger_perference_Id"] + "' />" +
                "KSML" +
                "</label>" +
                "</li>" +
                "<li>" +
                "<label title='MUSLIM MEAL'> " +
                "<input type='checkbox' value='MOML' disabled " + (Convert.ToString(dtPSR["Passenger_perference_Meal_perference"]) == "MOML" ? AutoFilledPassengerDetails(Convert.ToString(dtPSR["Passenger_perference_Meal_perference"]), "MEAL_PREFERENCE") : "") + " name='meal_pax_" + dtPSR["Passenger_perference_Id"] + "' />" +
                "MOML" +
                "</label>" +
                "</li>" +
                "<li>" +
                "<label title='RAW VEGETARIAN MEAL'> " +
                "<input type='checkbox' value='RVML' disabled " + (Convert.ToString(dtPSR["Passenger_perference_Meal_perference"]) == "RVML" ? AutoFilledPassengerDetails(Convert.ToString(dtPSR["Passenger_perference_Meal_perference"]), "MEAL_PREFERENCE") : "") + " name='meal_pax_" + dtPSR["Passenger_perference_Id"] + "' />" +
                "RVML" +
                "</label>" +
                "</li>" +
                "<li>" +
                "<label title='SEAFOOD MEAL'> " +
                "<input type='checkbox' value='SFML' disabled " + (Convert.ToString(dtPSR["Passenger_perference_Meal_perference"]) == "SFML" ? AutoFilledPassengerDetails(Convert.ToString(dtPSR["Passenger_perference_Meal_perference"]), "MEAL_PREFERENCE") : "") + " name='meal_pax_" + dtPSR["Passenger_perference_Id"] + "' />" +
                "SFML" +
                "</label>" +
                "</li>" +
                "<li>" +
                "<label title='SPECIAL MEAL'> " +
                "<input type='checkbox' value='SPML' disabled " + (Convert.ToString(dtPSR["Passenger_perference_Meal_perference"]) == "SPML" ? AutoFilledPassengerDetails(Convert.ToString(dtPSR["Passenger_perference_Meal_perference"]), "MEAL_PREFERENCE") : "") + "  name='meal_pax_" + dtPSR["Passenger_perference_Id"] + "' />" +
                "SPML" +
                "</label>" +
                "</li>" +
                "<li>" +
                "<label title='VEGETARIAN VEGAN MEAL'> " +
                "<input type='checkbox' value='VGML' disabled  " + (Convert.ToString(dtPSR["Passenger_perference_Meal_perference"]) == "VGML" ? AutoFilledPassengerDetails(Convert.ToString(dtPSR["Passenger_perference_Meal_perference"]), "MEAL_PREFERENCE") : "") + " name='meal_pax_" + dtPSR["Passenger_perference_Id"] + "' />" +
                "VGML" +
                "</label>" +
                "</li>" +
                "<li>" +
                "<label title='OTHER MEAL'> " +
                "<input type='checkbox' id='chkOTML' value='OTML' disabled  " + (Convert.ToString(dtPSR["Passenger_perference_Meal_perference"]) == "OTML" ? AutoFilledPassengerDetails(Convert.ToString(dtPSR["Passenger_perference_Meal_perference"]), "MEAL_PREFERENCE") : "") + " name='meal_pax_" + dtPSR["Passenger_perference_Id"] + "'  />" +
                "OTML" +
                "</label>" +
                "</li>" +
                "<li>" +
                "<input type='text' name='otherMeal'   disabled " + CreateStyle(Convert.ToString(dtPSR["Passenger_perference_Meal_perference"])) + "    value='" + Convert.ToString(dtPSR["Passenger_perference_Meal_perference_actual_value"]) + "' id='otherMeal' style='width: 200px;height: 25px;'/>" +
                "</li>" +
                "</ul>" +
                "<h5>SEAT PREFERENCE </h5>" +
                "<ul class='rdoList grid7 seat'>" +
                "<li>" +
                "<label>" +
                "<input type='checkbox' value='WINDOW' disabled  " + (Convert.ToString(dtPSR["Passenger_perference_Seat_perference"]) == "WINDOW" ? AutoFilledPassengerDetails(Convert.ToString(dtPSR["Passenger_perference_Seat_perference"]), "SEAT_PREFERENCE") : "") + " name='seat_pax_" + dtPSR["Passenger_perference_Id"] + "' />" +
                "WINDOW" +
                "</label>" +
                "</li>" +
                "<li>" +
                "<label>" +
                "<input type='checkbox' value='AISLE' disabled  " + (Convert.ToString(dtPSR["Passenger_perference_Seat_perference"]) == "AISLE" ? AutoFilledPassengerDetails(Convert.ToString(dtPSR["Passenger_perference_Seat_perference"]), "SEAT_PREFERENCE") : "") + " name='seat_pax_" + dtPSR["Passenger_perference_Id"] + "' />" +
                "AISLE" +
                "</label>" +
                "</li>" +
                "</ul>" +
                "<h5>WHEELCHAIR PREFERENCE </h5>" +
                "<ul class='rdoList listbox wheelchair'>" +
                "<li>" +
                "<label>" +
                "<input type='checkbox' value='WCHR' disabled  " + (Convert.ToString(dtPSR["Passenger_perference_Wheel_Chair_perference"]) == "NEEDS HELP FOR LONG DISTANCE CAN ASCEND/DESCEND STAIRS" ? AutoFilledPassengerDetails(Convert.ToString(dtPSR["Passenger_perference_Wheel_Chair_perference"]), "WHEELCHAIR_PREFERENCE") : "") + " name='wheel_pax_" + dtPSR["Passenger_perference_Id"] + "' />" +
                "NEEDS HELP FOR LONG DISTANCE CAN ASCEND/DESCEND STAIRS" +
                "</label>" +
                "</li>" +
                "<li>" +
                "<label>" +
                "<input type='checkbox' value='WCHS' disabled " + (Convert.ToString(dtPSR["Passenger_perference_Wheel_Chair_perference"]) == "NEEDS HELP FOR LONG DISTANCE CANNOT ASCEND/DESCEND STAIRS" ? AutoFilledPassengerDetails(Convert.ToString(dtPSR["Passenger_perference_Wheel_Chair_perference"]), "WHEELCHAIR_PREFERENCE") : "") + " name='wheel_pax_" + dtPSR["Passenger_perference_Id"] + "' />" +
                " NEEDS HELP FOR LONG DISTANCE CANNOT ASCEND/DESCEND STAIRS" +
                "</label>" +
                "</li>" +
                "<li>" +
                "<label>" +
                "<input type='checkbox' value='WCHC' disabled " + (Convert.ToString(dtPSR["Passenger_perference_Wheel_Chair_perference"]) == "COMPLETELY IMMOBILE" ? AutoFilledPassengerDetails(Convert.ToString(dtPSR["Passenger_perference_Wheel_Chair_perference"]), "WHEELCHAIR_PREFERENCE") : "") + " name ='wheel_pax_" + dtPSR["Passenger_perference_Id"] + "' /> " +
                "COMPLETELY IMMOBILE" +
                "</label>" +
                "</li>" +
                "</ul>" +
                "<h5>FREQUENT FLYER NUMBER </h5>" +
                "<input type='text' disabled value='" + Convert.ToString(dtPSR["Passenger_perference_Frequent_Flyer_Number"]) + "'   class='freqflyer' autocomplete='off' Id='freqflyer_pax_" + dtPSR["Passenger_perference_Id"] + "' maxlength='25' style='width: 200px;' />" +
                "</div>");
            }
        }
        else
        {
            foreach (DataRow drPax in dtPax.Rows)
            {
                special_Request.Append("<div class='col-md-12 col-sm-12 col-xs-12 list-border' Id=PAX_" + drPax["PaxId"] + ">" +
                                        "<h3><i class='fa fa-user' aria-hidden='true'></i>&nbsp;" + Convert.ToString(drPax["Title"]) + " " + Convert.ToString(drPax["FName"]) + " " + Convert.ToString(drPax["MName"]) + " " + Convert.ToString(drPax["LName"]) + "</h3>" +
                                        "<hr />" +
                                        "<h5>MEAL PREFERENCE</h5>" +
                                        "<ul class='rdoList grid7 meal'>" +
                                        "<li>" +
                                        "<label title='VEGETARIAN HINDU MEAL'> " +
                                        "<input type='checkbox' value='AVML' name='meal_pax_" + drPax["PaxId"] + "'/>" +
                                        "AVML" +
                                        "</label>" +
                                        "</li>" +
                                        "<li>" +
                                        "<label title='BABY MEAL'> " +
                                        "<input type='checkbox' value='BBML' name='meal_pax_" + drPax["PaxId"] + "'/>" +
                                        "BBML" +
                                        "</label>" +
                                        "</li>" +
                                        "<li>" +
                                        "<label title='BLAND MEAL'> " +
                                        "<input type='checkbox' value='BLML' name='meal_pax_" + drPax["PaxId"] + "' />" +
                                        "BLML" +
                                        "</label>" +
                                        "</li>" +
                                        "<li>" +
                                        "<label title='CHILD MEAL'> " +
                                        "<input type='checkbox' value='CHML' name='meal_pax_" + drPax["PaxId"] + "' />" +
                                        "CHML" +
                                        "</label" +
                                        "</li>" +
                                        "<li>" +
                                        "<label title='DIABETIC MEAL'> " +
                                        "<input type='checkbox' value='DBML' name='meal_pax_" + drPax["PaxId"] + "' />" +
                                        "DBML" +
                                        "</label>" +
                                        "</li>" +
                                        "<li>" +
                                        "<label title='FRUIT PLATTER MEAL'> " +
                                        "<input type='checkbox' value='FPML' name='meal_pax_" + drPax["PaxId"] + "' />" +
                                        "FPML" +
                                        "</label>" +
                                        "</li>" +
                                        "<li>" +
                                        "<label title='GLUTEN INTOLERANT MEAL'> " +
                                        "<input type='checkbox' value='GFML' name='meal_pax_" + drPax["PaxId"] + "' />" +
                                        "GFML" +
                                        "</label>" +
                                        "</li>" +
                                        "<li>" +
                                        "<label title='HINDU - NON VEGETARIAN MEAL'> " +
                                        "<input type='checkbox' value='HNML' name='meal_pax_" + drPax["PaxId"] + "' />" +
                                        "HNML" +
                                        "</label>" +
                                        "</li>" +
                                        "<li>" +
                                        "<label title='KOSHER MEAL'> " +
                                        "<input type='checkbox' value='KSML' name='meal_pax_" + drPax["PaxId"] + "' />" +
                                        "KSML" +
                                        "</label>" +
                                        "</li>" +
                                        "<li>" +
                                        "<label title='MUSLIM MEAL'> " +
                                        "<input type='checkbox' value='MOML' name='meal_pax_" + drPax["PaxId"] + "' />" +
                                        "MOML" +
                                        "</label>" +
                                        "</li>" +
                                        "<li>" +
                                        "<label title='RAW VEGETARIAN MEAL'> " +
                                        "<input type='checkbox' value='RVML' name='meal_pax_" + drPax["PaxId"] + "' />" +
                                        "RVML" +
                                        "</label>" +
                                        "</li>" +
                                        "<li>" +
                                        "<label title='SEAFOOD MEAL'> " +
                                        "<input type='checkbox' value='SFML' name='meal_pax_" + drPax["PaxId"] + "' />" +
                                        "SFML" +
                                        "</label>" +
                                        "</li>" +
                                        "<li>" +
                                        "<label title='SPECIAL MEAL'> " +
                                        "<input type='checkbox' value='SPML' name='meal_pax_" + drPax["PaxId"] + "' />" +
                                        "SPML" +
                                        "</label>" +
                                        "</li>" +
                                        "<li>" +
                                        "<label title='VEGETARIAN VEGAN MEAL'> " +
                                        "<input type='checkbox' value='VGML' name='meal_pax_" + drPax["PaxId"] + "' />" +
                                        "VGML" +
                                        "</label>" +
                                        "</li>" +
                                        "</ul>" +
                                        "<h5>SEAT PREFERENCE </h5>" +
                                        "<ul class='rdoList grid7 seat'>" +
                                        "<li>" +
                                        "<label>" +
                                        "<input type='checkbox' value='WINDOW' name='seat_pax_" + drPax["PaxId"] + "' />" +
                                        "WINDOW" +
                                        "</label>" +
                                        "</li>" +
                                        "<li>" +
                                        "<label>" +
                                        "<input type='checkbox' value='AISLE' name='seat_pax_" + drPax["PaxId"] + "' />" +
                                        "AISLE" +
                                        "</label>" +
                                        "</li>" +
                                        "</ul>" +
                                        "<h5>WHEELCHAIR PREFERENCE </h5>" +
                                        "<ul class='rdoList listbox wheelchair'>" +
                                        "<li>" +
                                        "<label>" +
                                        "<input type='checkbox' value='WCHR' name='wheel_pax_" + drPax["PaxId"] + "' />" +
                                        "NEEDS HELP FOR LONG DISTANCE CAN ASCEND/DESCEND STAIRS" +
                                        "</label>" +
                                        "</li>" +
                                        "<li>" +
                                        "<label>" +
                                        "<input type='checkbox' value='WCHS' name='wheel_pax_" + drPax["PaxId"] + "' />" +
                                        " NEEDS HELP FOR LONG DISTANCE CANNOT ASCEND/DESCEND STAIRS" +
                                        "</label>" +
                                        "</li>" +
                                        "<li>" +
                                        "<label>" +
                                        "<input type='checkbox' value='WCHC' name='wheel_pax_" + drPax["PaxId"] + "' /> " +
                                        "COMPLETELY IMMOBILE" +
                                        "</label>" +
                                        "</li>" +
                                        "</ul>" +
                                        "<h5>FREQUENT FLYER NUMBER </h5>" +
                                        "<input type='text' class='freqflyer' name='freqflyer_pax_" + drPax["PaxId"] + "' maxlength='25' style='width: 200px;' />" +
                                        "</div>");
            }
        }
        #endregion

        #region Right Side Div Quote Detatils and Travel Expert Tab

        StringBuilder Pax_Main_div = new StringBuilder();
        StringBuilder Pax_Detail_div = new StringBuilder();
        foreach (DataRow drPax in dtPax.Rows)
        {
            if (Convert.ToString(drPax["PaxId"]) == "1") //only Lead passagner show in this Tab
            {
                Pax_Detail_div.Append("<label><span class='blueText'>" + Convert.ToString(drPax["Title"]) + " " + Convert.ToString(drPax["FName"]) + " " + Convert.ToString(drPax["MName"]) + " " + Convert.ToString(drPax["LName"]) + "</span></label>");
            }
        }

        Pax_Main_div.Append("<i class='vertical-text'>Quote Details</i>" +
            "<span class='quote-ref mob'><b>" +
                "<span class='hidden-xs'>Quote </span>Reference </b>" +
                "<span class='ref'>" + XP + "</span></span>" +
            "<hr>" +
            "<div class='clearfix'></div>" +
            "<span class='quote-ref'><b>Contact Details</b></span>" +
            "<div class='clearfix'></div>" +
            "<div class='client-info'>" +
                    Pax_Detail_div.ToString() +
                "<label class='mt20 address'>" + CheckAddress(dtContact) + "</label>" +
                "<label class='mt10'>" + CheckEmailId(dtContact) + "</label>" +
                "<label>" + CheckPhoneNumber(dtContact) + "</label>" +
            "</div>");


        StringBuilder Pax_Main_Div_Agent = new StringBuilder();
        Pax_Main_Div_Agent.Append("<label class='mt10 hidden-xs'></label>" +
            "<i class='vertical-text'>Travel Expert</i>" +
            "<div class='clearfix'></div>" +
            "<div class='clearfix'></div>" +
            "<img alt = 'Travel Expert' title='Travel Expert' src=" + BLL.WebsiteStaticData.WebsiteUrl + "images/logoes/" + lo.SetCompanyDetail(dtComp.Rows[0]["BookingByCompany"].ToString()).Comp_logo + ".png" + ">" +
            "<div class='clearfix'></div>" +
            "<span class='quote-ref mt10'>" +
            "<a href='mailto:" + lo.SetCompanyDetail(dtComp.Rows[0]["BookingByCompany"].ToString()).Comp_Emailid + "'><i class='fa fa-envelope'></i>" + (dtComp != null && dtComp.Rows.Count > 0 ? lo.SetCompanyDetail(dtComp.Rows[0]["BookingByCompany"].ToString()).Comp_Emailid : "") + "</a><br>" +
            "<a href='tel:" + lo.SetCompanyDetail(dtComp.Rows[0]["BookingByCompany"].ToString()).Comp_contact + "'><i class='fa fa-phone'></i>" + (dtComp != null && dtComp.Rows.Count > 0 ? lo.SetCompanyDetail(dtComp.Rows[0]["BookingByCompany"].ToString()).Comp_contact : "") + "</a>" +
            "</span>");

        #endregion


        #region Terms Conditions

        StringBuilder terms_Conditions = new StringBuilder();

        if (dtComp.Rows[0]["CurrencyType"].ToString() == "CAD")
        {
            terms_Conditions.Append(Term_Condition_CA());
        }
        if (dtComp.Rows[0]["CurrencyType"].ToString() == "USD")
        {
            terms_Conditions.Append(Term_Condition_USA_NEW());
        }
        else
        {
            terms_Conditions.Append(Term_Condition());
        }


        #endregion


        #region Hide / Show Success Message Tab

        if (Btn_save_pref_Status)
        {
            lblSuccessMessage.Text = Success_Message;
            divSuccessMsg.Visible = true;
        }
        else
        {
            lblSuccessMessage.Text = string.Empty;
            divSuccessMsg.Visible = false;
        }


        #endregion



        #region Footer Tab

        StringBuilder Footer_Layout = new StringBuilder();
        Footer_Layout.Append("<div class='container'><p class='text-center'>©Copyrights " + DateTime.Now.Year + " | All rights reserved | <a  target='_blank' href=" + https_URL + lo.SetCompanyDetail(dtComp.Rows[0]["BookingByCompany"].ToString()).Comp_Emailid.Split('@')[1] + ">" + lo.SetCompanyDetail(dtComp.Rows[0]["BookingByCompany"].ToString()).Comp_Emailid.Split('@')[1] + "</a></p></div>");

        #endregion





        Company_Logo_Tab.Src = Company_Logo_URL.ToString();
        Company_Address_Tab.Text = Company_Address.ToString();

        Itin_Res_Tab.Text = Itin_Div.ToString();
        Itin_Section_Tab.Text = Itin_section.ToString();

        Itin_Pax_Detail_Tab.Text = Itin_Pax_detail.ToString();

        Baggage_Deatil_Tab.Text = Baggage_Deatil.ToString();
        Baggage_Deatil_Tab2.Text = Baggage_Deatil.ToString();
        //Payment_Details_Tab.Text = Payment_Details.ToString();
        Payment_Details_Tab2.Text = Payment_Details.ToString();
        Pax_Details_Tab.Text = Passenger_Details.ToString();
        Footer_Layout_Tab.Text = Footer_Layout.ToString();
        Pax_Main_Div_Agent_Tab.Text = Pax_Main_Div_Agent.ToString();
        Pax_Main_Div_Tab.Text = Pax_Main_div.ToString();
        Special_Request_Tab.Text = special_Request.ToString();
        lblTerms_Condition_Tab.Text = terms_Conditions.ToString();


        return myString.ToString();
    }

    public string CreateStyle(string value)
    {
        string result = string.Empty;

        if (!string.IsNullOrEmpty(value))
        {
            if (value == "OTML")
            {
                result = "style='display: block;width: auto;'";
            }
            else
            {
                result = "style='display: none'";
            }
        }
        return result;
    }

     



    public string CheckAddress(DataTable dtContact)
    {
        string PAddress = Convert.ToString(dtContact.Rows[0]["PAddress"]);
        string PCity = Convert.ToString(dtContact.Rows[0]["City"]);
        string PState = Convert.ToString(dtContact.Rows[0]["PState"]);
        string Country = Convert.ToString(dtContact.Rows[0]["Country"]);

        StringBuilder SAddress = new StringBuilder();

        if (!string.IsNullOrEmpty(PAddress) && PAddress != "." && PAddress != ";" && PAddress != ":")
        {
            SAddress.Append(Convert.ToString(dtContact.Rows[0]["PAddress"]));
        }
        if (!string.IsNullOrEmpty(PCity) && PCity != "." && PCity != ";" && PCity != ":")
        {
            SAddress.Append("," + Convert.ToString(dtContact.Rows[0]["City"]));
        }
        if (!string.IsNullOrEmpty(PState) && PState != "." && PState != ";" && PState != ":")
        {
            SAddress.Append("," + Convert.ToString(dtContact.Rows[0]["PState"]));
        }

        if (!string.IsNullOrEmpty(Country) && Country != "." && Country != ";" && Country != ":")
        {
            SAddress.Append("," + Convert.ToString(dtContact.Rows[0]["Country"]));
        }

        return SAddress.ToString();
    }

    public string CheckPhoneNumber(DataTable dtContact)
    {
        string resultPhone = string.Empty;
        string PhoneNumber = Convert.ToString(dtContact.Rows[0]["PhoneNo"]);
        if (!string.IsNullOrEmpty(PhoneNumber))
        {
            resultPhone = "<i class='fa fa-phone'></i>" + Convert.ToString(dtContact.Rows[0]["PhoneNo"]) + "";
        }
        else
        {
            resultPhone = string.Empty;
        }
        return resultPhone;
    }
    public string CheckEmailId(DataTable dtContact)
    {
        string resultEmail = string.Empty;
        string PhoneNumber = Convert.ToString(dtContact.Rows[0]["EmailID"]);
        if (!string.IsNullOrEmpty(PhoneNumber))
        {
            resultEmail = "<i class='fa fa-envelope'></i>&nbsp;" + Convert.ToString(dtContact.Rows[0]["EmailID"]) + "";
        }
        else
        {
            resultEmail = string.Empty;
        }
        return resultEmail;
    }

    protected void btnSend_Click(object sender, EventArgs e)
    {
        string Decrypt_BID = Common.DecryptString(Request.QueryString.Get("BID"), "");

        DataTable _PassengerDetail = new DataTable();
        _PassengerDetail.Clear();
        _PassengerDetail.Columns.Add("BookingID");
        _PassengerDetail.Columns.Add("NO_XP");
        DataRow dr = _PassengerDetail.NewRow();
        dr["BookingID"] = Decrypt_BID;
        dr["NO_XP"] = txtEditor.Text;
        _PassengerDetail.Rows.Add(dr);

        if (txtEditor.Text.Length > 500)
        {
            GET_SET_Booking_NO_XP(_PassengerDetail);
        }

        XPDetails = BookingDetails_NewFormatEmail(Decrypt_BID); ;
        DataServiceSoapClient objmail = new DataServiceSoapClient();
        if (objmail.Sendcustomermail(txtXPFrom.Text.Trim(), txtXPTo.Text.Trim(), "Flight Confirmation", XPDetails, txtXPFrom.Text.Trim(), "dinesh@dial4travel.co.uk") == true)
        {
            //ltrMsg.Text = "Quotation Sent Successfully.";
            GetSetDatabase objGetSetDatabase = new GetSetDatabase();
            objGetSetDatabase.SET_Booking_Detail(txtInvoice.Text.Trim(), "001", "", "", "", "", "", "", "", "Flight Quotation sent to " + txtXPTo.Text.Trim() + " with total price =" + txtPrice.Text, "", "", "", "", "", objUserDetail.userID, "", "", "Update", "", "");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "<script language='javascript'>alertMessage();</script>", false);
        }
        txtNote.Text = "";
        txtPrice.Text = "";
        txtXPFrom.Text = "";
        txtXPTo.Text = "";
    }

    private string BookingDetails_NewFormatEmail(string XP)
    {
        StringBuilder sbEmail = new StringBuilder();
        dtPax = obj.GET_Passenger_Detail(XP, "001");
        GetSetDatabase gs = new GetSetDatabase();
        dtSector = gs.GET_Sector_Detail(XP, "001");
        dtComp = obj.GET_Booking_Master(XP);
        dtContact = obj.GET_Contact_Detail(XP, "001");

        string PaxName = string.Empty;
        string CompanyLogo = BLL.WebsiteStaticData.WebsiteUrl + "images/logoes/" + lo.SetCompanyDetail(dtComp.Rows[0]["BookingByCompany"].ToString()).Comp_logo + ".png";
        string BrandName = lo.SetCompanyDetail(dtComp.Rows[0]["BookingByCompany"].ToString()).Brand_Name;
        string Company_URL = https_URL + lo.SetCompanyDetail(dtComp.Rows[0]["BookingByCompany"].ToString()).Comp_Emailid.Split('@')[1];

        string client_Image_URL = BLL.WebsiteStaticData.WebsiteUrl + @"images/flight-info-email-icon.png";


        double sendAmttoClient = 0;
        double sendtxtPrice = 0;

        if (!string.IsNullOrEmpty(txtPrice.Text))
        {
            sendtxtPrice = Convert.ToDouble(txtPrice.Text);
        }

        if (sendtxtPrice == 0)
        {
            sendAmttoClient = Price_Payable_Amount;
        }
        else
        {
            if (Price_Payable_Amount == sendtxtPrice)
            {
                sendAmttoClient = Price_Payable_Amount;
            }
            else if (Price_Payable_Amount < sendtxtPrice)
            {
                sendAmttoClient = sendtxtPrice;
            }
            else if (Price_Payable_Amount > sendtxtPrice)
            {
                sendAmttoClient = sendtxtPrice;
            }
        }
        //local
       // string Client_URL = "http://localhost:55580/FlightQuote.aspx?BID=" + Request.QueryString.Get("BID") + "&PID=001" + "&amt=" + sendAmttoClient;

        //test 
        //string Client_URL = "http://test.traveljunction.co.uk/FlightQuote.aspx?BID=" + Request.QueryString.Get("BID") + "&PID=001" + "&amt=" + sendAmttoClient;

        //Live
         string Client_URL = Company_URL + "/FlightQuote.aspx?BID=" + Request.QueryString.Get("BID") + "&PID=001" + "&amt=" + sendAmttoClient;


        foreach (DataRow drPax in dtPax.Rows)
        {
            if (Convert.ToString(drPax["PaxId"]) == "1")
            {
                PaxName = Convert.ToString(drPax["Title"]) + " " + Convert.ToString(drPax["FName"]) + " " + Convert.ToString(drPax["MName"]) + " " + Convert.ToString(drPax["LName"]);
            }
        }

        sbEmail.Append("<p>" +
                "Hi, " + PaxName + " </p> <p>" +
                "Below is the link to the flight information received from <a href='" + Company_URL + "'>" + BrandName + "</a></p>" +
                "<p>Note : " + txtNote.Text + "</p>" +
                "<a href='" + Client_URL + "' target='_blank'><img style='width:20%!important' src='" + client_Image_URL + "'/></a><br />" +

                "<img src='" + CompanyLogo + "' alt='" + BrandName + "'/>");

        return sbEmail.ToString();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        XPDetails = BookingDetails(txtInvoice.Text.Trim());
    }

    #region Terms and Conditions
     
    public string Term_Condition()
    {
        Terms = @"<p>&nbsp;</p>
        <p><strong><strong><u>Brief Terms &amp; Conditions-</u></strong></strong></p>
        <ul>
        <li>The fare is subjected to the availability &amp; it cannot be guaranteed until the tickets are issued.</li>
        <li>All the tickets are non-changeable &amp; non-refundable unless specified. Please contact your travel agent to get the detailed description about your ticket conditions.</li>
        <li>The local authorities in certain countries may impose additional taxes (tourist tax, etc.), which generally have to be paid locally. The Customer is exclusively responsible for paying such additional taxes. The amount of taxes can change between booking and stay dates. In the event that taxes have increased as at your stay date, you will be liable to pay taxes at the new higher rate.</li>
        <li>It is your responsibility to ensure that your&nbsp;travel documentation&nbsp;is in order. Please note that if you are travelling on a&nbsp;one-way ticket, most countries will not allow you to enter without&nbsp;relevant visas&nbsp;or documentation. If you have a connecting flight&nbsp;via a third country, you may also require a transit visa. For further information, please contact the consulate of the country you are travelling through. For the most up to date information on visas, passports, health and&nbsp;travel advice&nbsp;worldwide, please see the following web sites for UK Visas, Home Office and Foreign and Commonwealth Office.&nbsp;</li>
        </ul>
        <p><strong><strong>The official British Government website for visa service,&nbsp;<a href= 'https://www.gov.uk/apply-to-come-to-the-uk'><u>https://www.gov.uk/apply-to-come-to-the-uk</u></a></strong></strong></p>
        <p>Travel advice by country for Travel and living abroad,&nbsp;<a href= 'http://www.fco.gov.uk/en/travel-and-living-abroad/travel-advice-by-country/'><u>http://www.fco.gov.uk/en/travel-and-living-abroad/travel-advice-by-country/</u></a>&nbsp;or call on 0207 008 02320207 008 0232/0233</p>
        <p>&nbsp;</p>
        <table width= '100% '>
        <tbody>
        <tr>
        <td>
        <p><strong><strong>We advise you to&nbsp;reconfirm your flight 72 Hours prior to departure</strong></strong>&nbsp;</p>
        </td>
        </tr>
        </tbody>
        </table>
        <p>According to European law, we strongly recommend you to buy insurance before travelling.</p>
        <p>Kindly&nbsp;reconfirm&nbsp;all the terms and conditions regarding your reservation before confirming with our&nbsp;travel consultant.</p>
        <p>The tickets will be issued as electronically. Please get your VISA if required before travelling.&nbsp;</p>
        <table>
        <tbody>
        <tr>
        <td>
        <p>An&nbsp;electronic ticket&nbsp;(e-ticket) is a paperless ticketing method. Because your electronic ticket is held in the airlines computer, you cannot forget it or lose it. More importantly, your electronic ticket cannot be stolen, saving you the cost of a replacement ticket. When you arrive at the airline check in desk, you will be required to present the following to receive your boarding pass, an official form of identification i.e. your passport, a print out of your confirmation email to show to the airline.&nbsp;</p>
        </td>
        </tr>
        </tbody>
        </table>
        <p><strong><strong>Please note that you must have a valid Passport with a minimum of 6 months on it or you will not be able to travel. Your passport must also be in excellent condition - the presentation of damaged passports may mean you are unable to travel. It is mandatory to carry a machine readable passport or valid visa for travel to USA; otherwise you will be denied boarding.</strong></strong><br /><br /><strong><strong><u>Check in Times</u>:&nbsp;</strong></strong>Due to security measures we currently recommend 3 hours for intercontinental flights and 2 hours for European and Domestic flights.</p>
        <p>Check-in will normally close 1 Hour before scheduled departure so please allow sufficient time to get to the airport in time.</p>
        <p>Seats where possible, we will send your seat request to the airline, please note that not all airlines will pre-assign seats and actual seat allocation is entirely at the airlines discretion and an early check-in will help you the most in getting the seat you want. Exit row and extra leg room seats cannot normally be requested in advance and are usually only assigned at check-in.</p>
        <p>Meal Requests if you haven&rsquo;t already done so please let us know and we will send the forward the request to the airline but remember meal requests are not guaranteed but the airline will make every effort to meet your request.&nbsp;&nbsp;</p>
        <p><strong><strong><em>BOOKING CONDITIONS</em></strong></strong></p>
        <p>&nbsp;</p>
        <p><strong><strong><u>CONDITIONS A&nbsp;</u></strong></strong></p>
        <ol>
        <li><u> RESERVING YOUR FLIGHT OR HOLIDAY</u></li>
        </ol>
        <p>On receipt of your request and deposit we will confirm your booking and from that point cancellation charges will apply and we&rsquo;ll send you a confirmation with details of your arrangements.</p>
        <p>Please note that a telephone booking confirmation is a firm booking as if it were made or confirmed in writing.</p>
        <ol start= '2 '>
        <li><u> PRICE GUARANTEE</u></li>
        </ol>
        <p>CHARTER FLIGHT ARRANGEMENTS: - The price shown on this confirmation invoice will not be subject to any surcharges.</p>
        <p>SCHEDULED FLIGHT ARRANGEMENTS: - As scheduled airlines reserve the right to increase prices at any time the price shown on this confirmation invoice will ONLY be guaranteed once full payment is received. The payment of a deposit guarantees your seat, not the price.</p>
        <p>GOVERNMENT ACTION: - Our Price Guarantee cannot cover increases due to direct Government action. E.g. the imposition of VAT or Passenger Levy.</p>
        <ol start= '3 '>
        <li><u> MINOR CHANGES TO YOUR HOLIDAY</u></li>
        </ol>
        <p>If we are obliged to make any minor change in the arrangements for your holiday, we will inform you as soon as possible.</p>
        <ol start= '4 '>
        <li><u> MAJOR CHANGES TO YOUR HOLIDAY</u></li>
        </ol>
        <p>If we are obliged to make any major changes to your holiday arrangements prior to the departure e.g. change of departure time of more than 12 hours, change of airport (but excluding changes between airports in the London region, aircraft type or airline) it will only be because we are forced to do so by circumstances usually beyond our control. In such an unlikely event we will inform you immediately and our objective will be to minimize your inconvenience. We will wherever possible offer you alternative arrangements as close as possible to your original choice.</p>
        <p>You will then have a choice of accepting, taking another available holiday of similar price or canceling. Should you choose to cancel you will be reimbursed all monies paid to us.</p>
        <ol start= '5 '>
        <li><u> GROUP HOLIDAYS</u></li>
        </ol>
        <p>Some of our holidays are based on minimum number of participants and in the unlikely event that these numbers are not reached we reserve the right to cancel the tour and refund all payments made, Prices are subject to increase if the group size is reduced.</p>
        <ol start= '6 '>
        <li><u> FLIGHTS</u></li>
        </ol>
        <p>Details of airlines, flight numbers/schedules and destination airport will be shown on your invoice/confirmation. Please note that a flight described as quote &lsquo;direct&rsquo; will not necessarily be non-stop. Flight schedules may change at any time and we will advise you of any changes prior to departure. However, we strongly suggest you to reconfirm your reservation on any flights during your journey at least 72 hours prior to departure. We cannot accept any responsibility for delays or missed flights.&nbsp;</p>
        <ol start= '7'>
        <li><u> INSURANCE</u></li>
        </ol>
        <p>The Company strongly recommends that the Client takes out adequate insurance. The Client is herewith recommended to read the terms of any insurance to satisfy them as to the fitness of cover. The Company will be pleased to quote you for insurance.</p>
        <ol start= '8'>
        <li><u> MAKING A BOOKING</u></li>
        </ol>
        <p>The person, who makes the booking, must be aged 18 years or over, accepts these conditions on behalf of all members of the party and is responsible for all payments due from the party.</p>
        <ol start= '9'>
        <li><u> DEPOSIT</u></li>
        </ol>
        <p>No booking will be confirmed unless the required deposit has been received by The Company. Deposits do not guarantee price only confirmation of seats. Deposits are non-refundable.</p>
        <ol start= '10'>
        <li><u> CHANGING YOUR ARRANGEMENTS</u></li>
        </ol>
        <p>If you wish to change any item - other than increasing the number of persons in your party - and providing we can accommodate the change, you will have to confirm the change in writing and pay amendment fee of &pound;35 per booking + the airline/supplier charges depending upon the terms &amp; conditions of your ticket. From time to time we are required to collect additional taxes. You will be informed of any additional taxes prior to ticket issuance. Once the ticket is issued, most airlines do not allow changes.</p>
        <ol start= '11'>
        <li><u> CANCELLATION BEFORE TICKET ISSUE</u></li>
        </ol>
        <p>Should you or any member of your party be forced to cancel your flight or holiday, we must be notified, in writing, by the person who made the booking and who is therefore, responsible for the payment of the cancellation charges.</p>
        <p>CANCELLATION AFTER TICKET ISSUE: Cancellation will result in loss of 100% of total cost of all travel arrangements in most cases. Please consult your travel consultant. Charter flights carry a 100% cancellation fee both before and after ticket issuance.</p>
        <ol start= '12'>
        <li><u> NAME CHANGE/CORRECTION</u></li>
        </ol>
        <p>In any case, the complete name change is not possible which means we cannot transfer the ticket to someone else. However, name correction can be done depending upon the supplier/airline&rsquo;s conditions.</p>
        <ol start='13'>
        <li><u> LEGAL JURISDICTION</u></li>
        </ol>
        <p>We accept the jurisdiction of the Courts in part of the UK in which the client is domiciled. For clients not domiciled in the UK the Courts of England shall have sole jurisdiction.</p>
        <p><strong><strong><u>CONDITIONS B</u></strong></strong></p>
        <p>&nbsp;</p>
        <p>Please read the following terms and conditions carefully as they apply to all bookings made. No variations shall be valid unless agreed and confirmed in writing by a Director of The Company. A verbal variation will not be valid.</p>
        <p>The Company act as agents only in transactions relating to flights, car hire, accommodation, package holidays etc. and book those facilities for you (the client) on behalf of the Supplier or Operator (the Principal). The Company are not the Principal and do not act as the Principal nor shall they be construed as being such&nbsp;<em>by&nbsp;</em>inference or otherwise. This confirmation does not constitute a contract. Your contract is with the Principal named overleaf. The Company is not liable for the Principal&rsquo;s actions, failures or omissions.</p>
        <p>No booking will be confirmed unless the required deposit has been received by The Company. Principals reserve the right to increase prices up to the date on which they receive the balance. Payment of a deposit guarantees your seat, not the price.</p>
        <p>Bookings made will be immediately subject to the Principal's terms and conditions and The Company have no authority to vary them in the Client's favor.</p>
        <p>All amendments/cancellations will incur charges.</p>
        <p>Please note that a telephone booking confirmation is as firmly confirmed as if it were made/confirmed in writing at that time.</p>
        <p>The Company will attempt to fulfill Client&rsquo;s requirements to the best of its ability and in the event of a complaint, will pass such complaints to the Principal concerned on the Client&rsquo;s behalf, as agent only. The Company will not be able to commit the Principal as to their correct course of actions.</p>
        <p>The Company strongly recommends that the Client takes out adequate insurance whether or not it is a Principal's condition of booking. The Client is herewith recommended to read the terms of any insurance to satisfy them as to the fitness of cover. The Company will be pleased to quote you for insurance. Should insurance be declined you will be asked to sign our indemnity form.</p>
        <p><strong><strong><u>CONDITIONS APPLYING TO A AND B</u></strong></strong></p>
        <p><em>Please remember that the person making the booking accepts ALL the booking Conditions and is liable for any amendment fees, payments or cancellation charges that arise on behalf of ALL the passengers in their party. In addition they are also responsible for checking this and all future documentation and for advising us immediately if anything is missing&nbsp;</em>or&nbsp;<em>incorrect.&nbsp;</em>The details overleaf are given in good faith based on information from the Principal at the time of booking. Should it transpire that any of these details differ you will be advised immediately.</p>
        <p><strong><strong><u>PAYMENT</u></strong></strong></p>
        <p>You must pay the balance by the due date shown on the confirmation. Please note that for some telephone bookings full payment may be required IMMEDIATELY i.e. before you receive confirmation. If this applies, you will be advised when the booking is made. It is very important that you pay balance when due as failure to do so may lead to the cancellation of your holiday and still leave you liable to the cancellation charges. Where an extra  'booking charge ' is applicable, you will be advised at the time of booking. Credit card payments are subject to a minimum of 3% extra charge. Payment by Debit cards are accepted without any additional surcharges. However, a late payment fee of &pound;20 will be applied to your balance where cancellation can be avoided by the Principal.</p>
        <p><strong><strong><u>PASSPORTS, VISA AND HEALTH REQUIREMENTS</u></strong></strong>&nbsp;&ndash; Though we are happy to provide general information about the passport and visa requirements related to your trip, it is entirely your responsibility to get them confirmed from the relevant Embassies and/or Consulates. Please take special note that for all air travel within the British Isles, airlines require photographic identification of a specific type.</p>
        <p><em>Passport and Visa:&nbsp;</em>Passports normally need to be valid for at least 6 months beyond the period of your stay. If you are traveling to the USA you must have a machine readable passport. You must insure that your passport and visas are valid for all destinations on your journey, this includes transiting airports. Please consult the relevant Embassy or Consulate for this information. We regret that neither we nor the principal(s) or supplier(s) accept any responsibility if you are denied to board any flight or to enter any country due to failure on your part to carry the correct passport, visa or other documents required by any airline, authority or country.</p>
        <p><em>Health:&nbsp;</em>Recommended inoculations for travel may change at any time. It is your responsibility to ensure that you obtain all recommended inoculations, take all recommended medication and follow all medical advice in relation to your trip.</p>
        <p><strong><strong><u>SPECIAL REQUESTS AND MEDICAL PROBLEMS</u></strong></strong></p>
        <p>If you have any special requests, please advise us at time of booking. Although we will endeavor topass any such request on to the relevant supplier, we regret we cannot guarantee any request will be met. Infant meals must be requested by the client at the time of booking. Failure to meet any special request will not be a breach of contract on our part. If you have any medical problem or disability which may affect your booked arrangements, you must advise us in writing at the time of booking with full details. If we are unable to properly accommodate your particular need, we reserve the right to decline/cancel your booking.</p>
        <p><strong><strong><u>FORCE MAJEURE</u></strong></strong></p>
        <p>We accept no responsibility for and shall not be liable in respect of any loss or damage or alterations, delays or changes arising from unusual and unforeseen circumstances beyond our control, such as war or threat of war, riot, civil strife, industrial dispute including air traffic control disputes, terrorist activity, natural and nuclear disaster, fire or adverse weather conditions, technical problems with transport, closure or congestion of airports or ports, cancellations of schedules by scheduled airlines. You can check the current position on any country by contacting the Foreign and Commonwealth Office.</p>
        <table width='100%'>
        <tbody>
        <tr>
        <td>
        <p><strong><strong><u>CANCELLATIONS / AMENDMENTS BY THE TRAVEL SUPPLIER</u></strong></strong></p>
        </td>
        </tr>
        <tr>
        <td>
        <p>Airlines reserve the right to make time changes, or in rare cases, to cancel flights, for operational reasons. Whilst we are not responsible for, and has no control over, such changes, we will do our best to assist when such situations arise.</p>
        <p><br />In the unlikely event that your flight is cancelled by the airline or tour operator, your rights and remedies will be governed by the supplier's conditions/ airline's conditions of carriage. As a result you may be entitled to:</p>
        <p><br />(a) carriage on another flight with the same airline;<br />(b) re-routing to your destination with another carrier;<br />(c) <br /> some other right or remedy (like credit note or vouchers etc.).</p>
        <p><br />In the event of schedule changes made prior to commencement of your journey, it is not always necessary to have your tickets reissued or revalidated, but we will advise you should this be necessary. we take no responsibility for any flight rescheduling en route.</p>
        </td>
        </tr>
        </tbody>
        </table>
        <p><strong><strong><u>RECONFIRMING RETURN/ONWARD FLIGHTS</u></strong></strong></p>
        <p>It is your responsibility to ensure that you follow ALL RECONFIRMATION INSTRUCTIONS which will be shown EITHER on the FRONT of this invoice or on your travel documents. However, we strongly recommend you to reconfirm all the flights 72hrs prior to the departure. Should there be any changes to the schedule, the company will not be liable for any additional costs due to your failure to reconfirm your flights.</p>
        <p><strong><strong><u>DOCUMENT DISPATCH</u></strong></strong></p>
        <p>Most airline tickets are now &lsquo;e-tickets&rsquo; and as such will normally be e-mailed, or a copy posted, within 48hrs of receipt of full payment. &lsquo;E-tickets&rsquo; can also be collected at the departure airport. All other documentation, payment receipts, hotel vouchers, transfers etc. will be sent at the same time either by e-mail or to the address of the person paying for tickets. It is the customer&rsquo;s responsibility for any documents lost in the post; therefore we strongly suggest that all documents are sent by recorded delivery. Prices can be obtained while booking.</p>
        <p>Have a great trip, if you need any other travel arrangements. Let us know as it&rsquo;s our pleasure to assist you.</p>";
        return Terms;
    }

    public string Term_Condition_USA_NEW()
    {
        Terms = @"<p>Introduction</p>
            <p>There are some terms and conditions that are especially designed for the users of company. The company requests its users to go through each and every terms and conditions before utilizing its services. If a user utilizes the services then, it is considered as the person consent to the Terms of Use. On the other hand, if user doesn&rsquo;t want to give consent over the terms and conditions mentioned over the website, then don&rsquo;t utilize the website.</p>
            <h3>Price</h3>
            <p>All prices displayed on our website are subject to change at any time without prior notice. Airfare is only guaranteed once the purchase has been completed and the tickets have been issued. Airlines and other travel suppliers may change their prices without notice.<br /> If a price increase occurs after you have made a reservation that affects your travel package, we will notify you of the price increase before taking any further steps. However, no price increases will affect your travel package once your reservation has been finalized.<br /> All reservations are non-refundable unless otherwise stated. If you find that you must cancel a reservation for any reason, please contact us. We will do all we can to assist you in this process. However, please be aware that even if your cancellation is allowed and your reservation is thus refundable, it may be subject to an administrative cancellation fee of USD 200.00 per passenger for international flights, USD 300.00 for trans-border flights between USA and the Canada and USD 150.00 for domestic flights.</p>
            <h3>For US bookings, even if your ticket is nonrefundable:</h3>
            <ul>
            <li>Within same day midnight you may cancel your booking, &ldquo;subject to our cancellation fees&rdquo;.<br /> &bull; All Airline Basic Economy tickets and Promotion tickets cancellation are not permitted Non-refundable tickets<br /> &bull; All reservations are also non-changeable and non-transferable unless otherwise stated. If you need to make a change to your reservation and that change is allowed, please be aware that such change is subject to a fee of $150.00 per passenger for domestic flights, $200.00 for trans-border flights and $300.00 for all other flights. There may also be fees or differences in price charged by any third-party suppliers (e.g., airlines, hotels, cruise lines, etc.) included in your reservation.<br /> <br /> Please be aware that once you have made a reservation, name changes are not allowed. If you find you need to change or correct the spelling of a name after you&rsquo;ve made a reservation, you will have to cancel your original reservation&mdash;if allowed&mdash;and then make a new reservation with a new flight at the then-current rate using the correct spelling of the name. This will likely incur fees and penalties. Therefore, it is imperative&mdash;and your responsibility&mdash;to verify the spelling of the names of all passengers before making your reservation.<br /> The rate applied on the date of issuance of the ticket is only valid for a ticket fully utilized and in the sequential order of flight segments on the dates indicated. Improper use may void the ticket and result in cancellation of the entire trip.<br /> Pricing is displayed in US currency.</li>
            </ul>
            <h3>PAYMENT AND FLIGHT INFORMATION AND CONFIRMATION</h3>
            <p>Some banks and credit card companies charge a fee for international transactions. They will appear on your credit or bank card statement as a foreign or international transaction fee. For example, if you make a travel reservation through our website from outside the United States using a U.S. credit card, your bank may convert the payment amount to your local currency and may charge you a fee for the conversion. The amount of the charge appearing on your credit or bank card statement may be in your local currency and different than the purchase amount shown on the billing summary page for the reservation.</p>
            <p>In addition, a foreign transaction fee may be assessed if the bank that issued your credit card is located outside the United States.</p>
            <p>Booking international travel through our website may be considered an international transaction by the bank or credit card company since company may pass your payment on to an international travel supplier.</p>
            <p>Your bank or credit card company determines the currency exchange rate and the amount of the foreign transaction fee on the day it processes the transaction. Please contact your bank or credit card company should you have any questions about these fees or the exchange rate applied to your transaction.</p>
            <p>Booking notification: Once your purchase is complete, you should receive an email titled &ldquo;Booking Notification.&rdquo; Your booking may provide you with a confirmation number before a ticket has been issued. If this is the case, the booking process is not complete and the fare is subject to change until a ticket is issued.</p>
            <p>Once your ticket has been issued, you should receive your electronic ticket.</p>
            <p>We strongly recommend that you re-confirm your flight reservation with the airline 24 hours prior to departure for domestic flights, and 72 hours prior to departure for international flights.</p>
            <h3>Booking Amendments and Charges</h3>
            <p>Booking made on us may have some charges that are being charged as per the directive of airline. The charges may vary by flight and booking class. The amendment fee may also vary by airline to airline, flight and booking class. All changes must be made at least 72 hours prior to the departure date. Time duration can be different from airline to airline, therefore it is advised to cross check it with customer support for accurate information.</p>
            <h3>Cancel &amp; Exchange</h3>
            <p>Most of the aircraft tickets are non-refundable. In some situations where the carrier allows cancellation, then a credit might be considerable against future ticket by same traveler on the same airlines. The airlines have their own policy of credit termination date, which can&rsquo;t be utilized after its expiry. So, in case of cancellation, we request customers to examine the limitations with customer service specialist. Cancellation of tickets must be done before flight takes off else, we don&rsquo;t assure or promise for any cancellation. If you are already prepare to make a new reservation and wish to utilize the airline credit, then you will be required to bear the difference in the fare if applicable. Such kind of strategies and policies are made by the management of carriers, which are not in our control.</p>
            <p>We can accept refund requests only if the following conditions have been met:</p>
            <ul>
            <li>You have applied for a cancellation and refund with us and if the fare rules provide for cancellation and refunds;</li>
            <li>You are not a 'no show' (most 'no show' bookings are in-eligible for any waiver from suppliers for refund processing); and</li>
            <li>We are able to secure waivers from suppliers to process this requested cancellation and refund.</li>
            </ul>
            <p>We are unable to provide a specific time line for how long it may take for this requested refund to be processed. All refund requests are processed in a sequential format. Once you have provided our customer service agent with your cancellation request, we will then send you an email notification that your request has been received. This notification does not automatically qualify you for a refund. This only provides you with an acknowledgement of your request and provides you with a tracking number. Upon receipt of your request we will work with the suppliers such as airlines, hotels, car-rental companies to generate a waiver based on airline and other supplier rules and notify you of the supplier decision. Our services fees associated with the original travel reservation or booking are not refundable. Please note that we are dependent on the suppliers for receiving the requested refunds. Once the refund has been approved by the supplier it may take additional time for this to appear on your credit card statement. Generally, all suppliers will charge a penalty for refund. This entire process may take 60-90 days from receipt of your request to receiving credit on your statement. Apart from the airlines and other suppliers refund penalties, Company will charge a post-ticketing services fee, as applicable. All refund fees are charged on per-passenger, per-ticket basis. These fees will only be assessed if a refund has been authorized by the supplier or a waiver has been received and when the airline/supplier rules permit such refunds. If such refund is not processed by the supplier, we will refund you our post-ticketing service fees applicable to your agent assisted refund request , but not our booking fees for the original travel reservation or booking.</p>
            <h3>The company accept request under some of the guidelines such as follows:</h3>
            <ul>
            <li>If the ticketing fare rules will allow the cancellation on a particular booking, then only request will be accepted.</li>
            <li>You should not be a NO SHOW passenger. In case, you are a NO SHOW and you are not allowed to board the airline, then you are not eligible for refund.</li>
            <li>If refund is processed, it maximum takes up to 21 working days.</li>
            </ul>
            <h3>Payment Policy</h3>
            <ul>
            <li>We accept credit cards and debit cards of major countries including US, Canada etc.</li>
            <li>All costs appear in U.S. dollars.</li>
            <li>We may accept payment into two separate transactions which includes Airline Base Fare and Taxes. However, aggregate sum will be same as advised to people.</li>
            <li>Once your payment gets through, the ticket purchased is guarantee. In case we didn&rsquo;t get payment, we will notify you within 24 hours.</li>
            <li>If the credit card details are not approved by our verification department, we won&rsquo;t process any booking.</li>
            </ul>
            <h3>VISA AND ENTRY REQUIREMENTS</h3>
            <p>All customers are advised to verify travel documents (transit visa/entry visa) for the country through which they are transiting and/or entering. Reliable information regarding international travel can be found at govt. website and also with the consulate/embassy of the country(s) you are visiting or transiting through. We will not be responsible if proper travel documents are not available and you are denied entry or transit into a Country.<br /> Your transaction with us does not guarantee entrance to the country of destination. Traveler understands that we accepts no responsibility for determining passenger's eligibility to enter or transit through any specific country. Information, if any, given by company's employees must be verified with government authorities. Such information does not imply responsibility company behalf.</p>
            <h3>ETA FOR CANADA</h3>
            <p>Travelers who fly to or transit through Canada may need an Electronic Travel Authorization (ETA), this is an automated system that allows Canadian authorities to screen passengers before their arrival in Canada and determine the eligibility of visitors to enter Canada and whether such travel involves any security risk.</p>
            <h3>CREDIT CARD DECLINES</h3>
            <p>At the time of processing the transaction, user's credit card declines for various reasons. We endeavour to notify you by e-mail within 72 hours. Please note in any case if your credit card has been declined, the transaction will not be processed. We do not guarantee for the fare change and any other booking details. If there is a fare change from the TSP, we provide user(s) with the alternative options either accept or cancel the booking.</p>
            <p>&nbsp;</p>";
        return Terms;
    }

    public string Term_Condition_CA()
    {
        Terms = @"<div>
        <p>
            <strong>
                <strong>
                    <u>Brief Terms &amp; Conditions-</u>
                </strong>
            </strong>
        </p>
        <ul>
            <li>The fare is subjected to the availability &amp; it cannot be guaranteed until the tickets are issued.</li>
            <li>All the tickets are non-changeable &amp; non-refundable unless specified. Please contact your travel agent to get the detailed description about your ticket conditions.</li>
            <li>The local authorities in certain countries may impose additional taxes (tourist tax, etc.), which generally have to be paid locally. The Customer is exclusively responsible for paying such additional taxes. The amount of taxes can change between booking and stay dates. In the event that taxes have increased as at your stay date, you will be liable to pay taxes at the new higher rate.</li>
            <span>
                <li>It is your responsibility to ensure that your&nbsp;travel documentation&nbsp;is in order. Please note that if you are travelling on a&nbsp;one-way ticket, most countries will not allow you to enter without&nbsp;relevant visas&nbsp;or documentation. If you have a connecting flight&nbsp;via a third country, you may also require a transit visa. For further information, please contact the consulate of the country you are travelling through. For the most up to date information on visas,contact your local embassy of the country you are planning to travel to.</li>
            </span>
        </ul>
    </div>
    <div>
        <table width='100%'>
            <tbody>
                <tr>
                    <td>
                        <p>
                            <strong>
                                <strong>We advise you to&nbsp;reconfirm your flight 72 Hours prior to departure</strong>
                            </strong>&nbsp;

                        </p>
                        <p>The tickets will be issued as electronically. Please get your VISA if required before travelling.&nbsp;</p>
                        <table>
                            <tbody>
                                <tr>
                                    <td>
                                        <p>An&nbsp;electronic ticket&nbsp;(e-ticket) is a paperless ticketing method. Because your electronic ticket is held in the airlines computer, you cannot forget it or lose it. More importantly, your electronic ticket cannot be stolen, saving you the cost of a replacement ticket. When you arrive at the airline check in desk, you will be required to present the following to receive your boarding pass, an official form of identification i.e. your passport, a print out of your confirmation email to show to the airline.&nbsp;</p>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <p>
                            <strong>
                                <strong>Please note that you must have a valid Passport with a minimum of 6 months on it or you will not be able to travel. Your passport must also be in excellent condition - the presentation of damaged passports may mean you are unable to travel. It is mandatory to carry a machine readable passport or valid visa for travel to USA; otherwise you will be denied boarding.</strong>
                            </strong>
                            <br>
                            <br>
                            <strong>
                                <strong>
                                    <u>Check in Times</u>:&nbsp;

                                </strong>
                            </strong>Due to security measures we currently recommend 3 hours for intercontinental flights and 2 hours for European and Domestic flights.

                        </p>
                        <p>Check-in will normally close 1 Hour before scheduled departure so please allow sufficient time to get to the airport in time.</p>
                        <p>Seats where possible, we will send your seat request to the airline, please note that not all airlines will pre-assign seats and actual seat allocation is entirely at the airlines discretion and an early check-in will help you the most in getting the seat you want. Exit row and extra leg room seats cannot normally be requested in advance and are usually only assigned at check-in.</p>
                        <p>Meal Requests if you haven’t already done so please let us know and we will send the forward the request to the airline but remember meal requests are not guaranteed but the airline will make every effort to meet your request.</p>
                        <br>
                        <p>This website was designed to be as user-friendly, informative and secure as possible. Please read these terms and conditions to learn more about the website as well as our responsibilities and yours in using it. If you are not willing to agree to these provisions, please do not use the website.</p>
                        <p>This website is intended to provide services primarily for residents of North America and Canada.</p>
                        <p>
                            ******************************

                            <wbr>******************************

                            <wbr>******************************

                            <wbr>******************************

                            <wbr>******************************

                            <wbr>******************************

                            <wbr>**************************

                        </p>
                        <p>
                            <span>
                                <b>PRICES , Refund and Cancellation Policies</b>
                            </span>
                            <br>
                        </p>
                        <p>
                            All prices displayed on our website are subject to change at any time without prior notice. Airfare is only guaranteed once the purchase has been completed and the tickets have been issued. Airlines and other travel suppliers may change their prices without notice.

                            <br />
                            <br />If a price increase occurs after you have made a reservation that affects your travel package, we will notify you of the price increase before taking any further steps. However, no price increases will affect your travel package once your reservation has been finalized.

                            <br />
                            <br />All reservations are non-refundable unless otherwise stated. If you find that you must cancel a reservation for any reason, please contact us. We will do all we can to assist you in this process. However, please be aware that even if your cancellation is allowed and your reservation is thus refundable, it may be subject to an administrative cancellation fee of CAD150.00 per passenger for international flights, CAD125 for trans-border flights between Canada and the USA and CAD 75 for domestic flights.

                            <br />
                            <br>For U.S. and Canada bookings, even if your ticket is nonrefundable:

                        </p>
                        <ul>
                            <li>Within same day midnight you may cancel your booking and receive a full refund, subject to our cancellation fees.</li>
                            <li>All reservations are also non-changeable and non-transferable unless otherwise stated. If you need to make a change to your reservation and that change is allowed, please be aware that such change is subject to a fee of $75 per passenger for domestic flights, $125.00 for trans-border flights and $150.00 for all other flights. There may also be fees or differences in price charged by any third-party suppliers (e.g., airlines, hotels, cruise lines, etc.) included in your reservation.</li>
                        </ul>
                        <p>
                           
                            <span>Please be aware that once you have made a reservation, name changes are not allowed. If you find you need to change or correct the spelling of a name after you’ve made a reservation, you will have to cancel your original reservation—if allowed—and then make a new reservation with a new flight at the then-current rate using the correct spelling of the name. This will likely incur fees and penalties. Therefore, it is imperative—and your responsibility—to verify the spelling of the names of all passengers before making your reservation.</span>
                            <br />
                            <br />
                            <span>The rate applied on the date of issuance of the ticket is only valid for a ticket fully utilized and in the sequential order of flight segments on the dates indicated. Improper use may void the ticket and result in cancellation of the entire trip.</span>
                            <br />
                            <br />
                            <span>Pricing is displayed in US and/or Canadian currency.</span>
                        </p>
                       
                        <h3>
                            FORCE MAJEURE
                        </h3>
                        <p>
                            <span></span>
                        </p>
                        <p,Helvetica,Arial,sans-serif;outline:0px'>
                            We accept no responsibility for and shall not be liable in respect of any loss or damage or alterations, delays or changes arising from unusual and unforeseen circumstances beyond our control, such as war or threat of war, riot, civil strife, industrial dispute including air traffic control disputes, terrorist activity, natural and nuclear disaster, fire or adverse weather conditions, technical problems with transport, closure or congestion of airports or ports, cancellations of schedules by scheduled airlines. You can check the current position on any country by contacting the Foreign and Commonwealth Office.
                            </p>
                            <p>
                                <br>
                            </p>
                            <h3>
                                CANCELLATIONS / AMENDMENTS BY THE TRAVEL SUPPLIER
                            </h3>
                            <p>
                                Airlines reserve the right to make time changes, or in rare cases, to cancel flights, for operational reasons. Whilst we are&nbsp;not responsible for, and has no control over, such changes, we will do our best to assist when such situations arise.

                                <br />
                                <br />In the unlikely event that your flight is cancelled by the airline or tour operator, your rights and remedies will be governed by the supplier's conditions/ airline's conditions of carriage. As a result you may be entitled to:

                                <br />
                                <br />(a) carriage on another flight with the same airline without additional costs;

                                <br />(b) re-routing to your destination with another carrier without additional costs;

                                <br />(d) some other right or remedy, for example a future credit note, voucher, etc

                                <br />
                                <br>In the event of schedule changes made prior to commencement of your journey, it is not always necessary to have your tickets reissued or revalidated, but we will advise you should this be necessary. we&nbsp;take no responsibility for any flight rescheduling en route.

                            </p>
                            <h3>
                            </h3>
                            <p>
                                ******************************

                                <wbr>******************************

                                <wbr>******************************

                                <wbr>******************************

                                <wbr>******************************

                                <wbr>******************************

                                <wbr>******************************

                                <wbr>*

                            </p>
                            <h3>
                                TRAVEL INFORMATION
                            </h3>
                            <p>
                                Our website provides extensive information related to travel for you, our customer. It contains information about vacation destinations, tour packages and travel providers as well as airfares, flight schedules and cruise details. It also contains information for travelers about insurance and foreign currencies.

                                <br />
                                <br />We receive this information from third-party sources such as airlines, hotels, tour operators and transportation providers. We always take reasonable care to make sure this information is accurate and up-to-date. However, we cannot guarantee the accuracy of this information or that it is the most current information available.

                                <br />
                                <br />As a traveler, you must know and understand the applicable legal requirements related to travel, including passport, visa and health requirements. We will assist you in this regard, both through our website and with live support. However, the ultimate responsibility for obtaining this information and complying with any and all passport, visa, health or other requirements remains solely and exclusively with you.

                                <br />
                                <br />We strive to provide you with the most current information available concerning tour packages, flight schedules, travel destinations and prices on our website. However, please understand that all the information on our website is subject to change without prior notice. Also, travel products, packages and services described on our website are subject to availability.

                                <br />
                                <br />
                                <span>Baggage Policy:</span>&nbsp;Each airline has its own policies regarding baggage allowances, fees and restrictions. These policies differ from airline to airline and can change at any time. We try our best to display current baggage fee information on this website, but we cannot guarantee the accuracy of this information. Ultimately, you are responsible for verifying your airline’s baggage policies and fees before your departure. Also, please be aware that baggage fees are not included in the cost of your trip.

                                <br />
                                <br />Schedule change: Changes to flight schedules, including flight cancellations, can occur for any number of reasons, including bad weather, mechanical problems, crew issues and civil unrest. When this happens, we do our best to notify our customers of any changes to their itinerary, by phone and/or email. However, sometimes the airline does not provide advance notice of the change or cancellation. For this reason, we recommend that you telephone your airline or check your flight status online 24 hours before your scheduled departure.

                                <br />
                                <br />If your flight has been cancelled, please call us at our toll free number as on our website.We will work directly with the airline on your behalf to find out what options are available and figure out a solution for you. However, if you don’t find out about the cancellation until you’re already at the airport, or are in-between flights, we recommend you work directly with the airline staff to figure out a solution. Please note that in some cases, especially during bad weather, your options may be limited.

                            </p>
                            <h3>
                                PAYMENT AND FLIGHT INFORMATION AND CONFIRMATION
                            </h3>
                            <p>
                                Some banks and credit card companies charge a fee for international transactions. They will appear on your credit or bank card statement as a foreign or international transaction fee. For example, if you make a travel reservation through our website from outside the United States using a U.S. credit card, your bank may convert the payment amount to your local currency and may charge you a fee for the conversion. The amount of the charge appearing on your credit or bank card statement may be in your local currency and different than the purchase amount shown on the billing summary page for the reservation.

                                <br />
                                <br />In addition, a foreign transaction fee may be assessed if the bank that issued your credit card is located outside the United States.

                                <br />
                                <br />Booking international travel through our website may be considered an international transaction by the bank or credit card company since Traveljunction.ca may pass your payment on to an international travel supplier.

                                <br />
                                <br />Your bank or credit card company determines the currency exchange rate and the amount of the foreign transaction fee on the day it processes the transaction. Please contact your bank or credit card company should you have any questions about these fees or the exchange rate applied to your transaction.

                                <br />
                                <br />Booking notification: Once your purchase is complete, you should receive an email titled “Booking Notification.” Your booking may provide you with a confirmation number before a ticket has been issued. If this is the case, the booking process is not complete and the fare is subject to change until a ticket is issued.

                                <br />
                                <br />Once your ticket has been issued, you should receive your electronic ticket.

                                <br />
                                <br>We strongly recommend that you re-confirm your flight reservation with the airline 24 hours prior to departure for domestic flights, and 72 hours prior to departure for international flights.

                            </p>
                            <h3>
                                SPECIAL REQUEST; SEATS, MEALS AND FREQUENT FLYER
                            </h3>
                            <p>Please note that requesting specific seats, meals, frequent flyers etc. are requests only. The airline reserves the right to make revisions to the seat allocation without notification. All requests should be confirmed with the airline and we cannot guarantee that passengers will be assigned the seats they’ve requested. Furthermore, we are unable to promise that your meal/frequent flyer/other special requests will be confirmed by the airline in question. Please ensure that you contact the airline you’ve booked with in order to confirm the requests you’ve made.</p>
                            <h3>
                                SUITABILITY OF TRAVEL PRODUCTS AND SERVICES
                            </h3>
                            <p>
                                On our website, we offer a variety of travel products and services for our customers. However, we do not represent or warrant that any of these travel products and services are or will be suitable and proper for you.

                                <br />
                                <br>You agree to release us from any claims relative to the travel products and services detailed on our website, including but not limited to claims that these travel products and services are not or were not suitable for you.

                            </p>
                            <h3>
                                SPECIALS
                            </h3>
                            <p>
                                From time to time we offer “specials” on our website. This section applies to all specials we offer on this website. As well, all the terms and conditions spelled out above apply to specials we offer on this website.

                                <br />
                                <br />Specials are only available for a limited time. Please contact us if need be to determine whether a special shown on our website is still available.

                                <br />
                                <br />Specific terms and conditions may apply to any special shown on our website. Please contact either us or the third-party provider of the special to determine what terms and conditions apply to that special, if any, and how they may affect you.

                                <br />
                                <br>Payment for any special that you book and that we confirm is due within 72 hours of our confirmation, unless otherwise agreed to by us in writing. If you fail to pay for the special within this 72-hour timeframe, your booking may be cancelled. We accept no responsibility for any loss you incur as a result of cancellation for non-payment within 72 hours.

                            </p>
                            <h3>
                                INTELLECTUAL PROPERTY
                            </h3>
                            <p>
                                This website, including its underlying software and its text, design, graphics, layout and content, is owned or licensed by us or by the respective owners. All this material is protected by Canadian and international intellectual property laws.

                                <br />
                                <br />As a visitor to or user of this website, you have permission to view, use and electronically copy the pages and content of this website through the usual and ordinary use of a web browser.

                                <br />
                                <br />Any other use of this website and its contents, such as copying, distributing, selling, modifying, transmitting, re-using, re-posting or publishing, is not permitted and is strictly prohibited without the specific written permission of the owner(s) of such material.

                                <br />
                                <br />Any unauthorised use of our website or its contents will breach this agreement and may void your permission to use this website. It may also violate copyright and other laws.

                                <br />
                                <br>Certain trademarks, service-marks, business names, company names, logos, trade names and presentation techniques (trade dress) used on this website are owned by us or by our licensors. In particular, we own the trademark “Traveljunction.ca.” You do not have a right, license or permission to use any of them.

                            </p>
                            <br>
                            <p>
                                As a visitor to or user of this website, you must use it in a responsible and co-operative manner.

                                <br />
                                <br>You must not:

                            </p>
                            <ul>
                                <li>make any fraudulent, speculative or false enquiries, bookings, or reservations, or make any reservations in anticipation of demand;</li>
                                <li>use any form of robot, spider, scraper or other automated means, or any comparable manual process, for the purpose of accessing, monitoring or copying any of the content or information on this website without our prior written consent;</li>
                                <li>reproduce, upload, post, display, republish, distribute, or transmit any content of this website in any form or manner whatsoever;</li>
                                <li>place or enter false, misleading or incorrect information on the website;</li>
                                <li>make any form of booking, reservation or request through this website without fully intending to use that booking, reservation or request for legitimate travel purposes;</li>
                                <li>use another person’s name, user ID or password to make bookings, reservations or inquiries on this website without that person’s prior permission;</li>
                                <li>use this website while impersonating or acting as another person;</li>
                                <li>post on or transmit through this website any unlawful, threatening, defamatory, libelous, obscene, indecent, inflammatory or pornographic material or images, or any other material that could give rise to or result in civil or criminal proceedings;</li>
                                <li>access or use this website in any manner that, in our opinion, could impair, impede or otherwise negatively affect the proper functioning and performance of this website and its systems, or that could negatively impact other visitors to or users of this website;</li>
                                <li>tamper with or hinder the operation of this website or make unauthorised modifications to the website;</li>
                                <li>delete data from this website without our permission;</li>
                                <li>knowingly transmit any virus, malware or other disabling feature or software to or through this website;</li>
                                <li>breach the rights of any third party (including rights in intellectual property or contract as well as obligations of confidentiality or nondisclosure) or break any related laws in visiting or using this website;</li>
                                <li>frame this website as part of another website, or cache this website for commercial gain or advantage;</li>
                                <li>disguise or mask the origin device and/or IP address information of the data being transmitted through this website;</li>
                                <li>knowingly permit or allow another person to do any of the above acts.</li>
                            </ul>
                            <p>We reserve the right to restrict or terminate your access to any or all of the features and components of this website if we believe you have violated, or are violating, any of the above prohibitions. In the event of any such restriction or termination, you must immediately cease any prohibited use of this website. Attempting to access or use the website in violation of any restrictions or terminations shall constitute an act of trespass. We will pursue legal action to the fullest extent possible against anyone whom we believe is in breach of the above prohibitions or is committing trespass on the website, and we reserve the right to do so.</p>
                            <h3>
                                YOUR WARRANTIES
                            </h3>
                            <p>You declare and affirm the following:</p>
                            <ul>
                                <li>you have reached the age of majority and are therefore old enough to legally use this website and enter into legally-binding contractual obligations;</li>
                                <li>you agree to be responsible (financially and otherwise) for all uses you make of this website as well as the uses of those whom you allow to use your user ID and password to access this website;</li>
                                <li>all information you provide on or through this website will be correct, accurate, not misleading, not deceptive and not be likely or intended to mislead or deceive others.</li>
                            </ul>
                            <h3>
                                INDEMNITY
                            </h3>
                            <p>You agree to indemnify and hold harmless both our company and the officers, employees and agents of our company from and against any and all losses, damages, claims, costs and expenses arising from any or all of the following:</p>
                            <ul>
                                <li>any violations by you of these Terms &amp; Conditions;</li>
                                <li>any act or omission by you personally or by an officer, employee or agent of your company;</li>
                                <li>any claim, demand, cause of action or legal proceeding by a third party against us or our officers, employees and/or agents that arose by reason of an act or omission by you personally or by an officer, employee or agent of your company.</li>
                            </ul>
                            <h3>
                                YOUR PRIVACY
                            </h3>
                            <p>
                                Subject to the terms of our 'Privacy Policy' (found on a separate page on this website), we will not disclose your personal information without your permission unless we have to in order to comply with your request or instructions, or unless otherwise required by law. “Personal information” in this context includes such things as your name, contact information and browsing habits provided to us by you or by your web browser.

                                <br />
                                <br />In the course of providing you with travel-related products and services, we and our third-party providers of such products and services may disclose personal information about you to others in order to set up your travel package. For example, we may disclose information about you to airlines, hotels or car rental companies to complete your travel arrangements.

                                <br />
                                <br>Separately, we may disclose aggregated information about users and use statistics from our website as well as aggregated information about our sales and trading patterns to others in the ordinary course of our business.

                            </p>
                            <h3>
                                DISCLAIMERS
                            </h3>
                            <p>
                                This website and all its content is provided for your use on an “as is” basis and at no charge. We make no warranties or representations of any kind with respect to the website, its contents or any of the products or services offered, provided or made available on or through this website. Moreover, we do not warrant or represent that the content of this website is accurate, current or complete, or that it does not infringe the rights of others.

                                <br />
                                <br />We disclaim all implied warranties and representations to the maximum extent permitted by law, including, without limitation, implied warranties that the products and services offered, sold and provided through this website will be of merchantable quality, are fit for any purpose or comply with the descriptions and samples displayed on this website.

                                <br />
                                <br />We do not warrant or represent that this website, the server on which it resides or any of the products and services offered, sold or provided on or through this website are or will be free of errors, defects, viruses or other malicious software.

                                <br />
                                <br />We have endeavoured to make this website secure and safe to use, and will continue to do so. We have implemented security measures and technology for this purpose. However, because of the proliferation of viruses, malware and other malicious software on the Internet, we cannot and do not warrant or represent that this website is or will remain secure.<br /><br />The ability to access and use this website through the Internet is subject to factors over which we have no control. We therefore cannot and do not warrant or represent that you will be able to access this website at any time you want, or that access to the website will be uninterrupted or timely.<br /><br />If you are unable to access this website, or if the website fails to operate properly, or at all, and you incur loss or damage as a result, your sole remedy is the refund of the money you paid us to use this website, if any.<br /><br />Our role through this website is to help you make travel arrangements, including placing reservations and processing payments. The travel-related products and services offered, promoted and sold through this website are provided by third parties. We are acting as an agent for these third-party providers. As such, your legal relationship regarding these products and services is with the actual providers of these products and services, and not with us. Therefore, you release us from all liability, claims, damages, costs and expenses to the extent permitted by law arising out of the provision or failure to provide, as well as the use or non-use, of these third-party travel products and services. This includes direct, indirect, special and consequential loss or damage, whether in negligence or otherwise.<br /><br />Neither will we nor will any of our officers, employees, agents, shareholders or other representatives be liable in damages or otherwise to the maximum extent permitted by law in connection with your use of or inability to use or access this website or your purchase and use of any products and services offered, promoted or sold on or through this website.<br /><br />This limitation of liability applies to all damages of any kind, including compensatory, direct, indirect, special or consequential damages; loss of data, income or profit; loss of or damage to property; personal injury; and claims of third parties.<br /><br />If any warranties implied by law cannot be excluded, then our liability for breach of such warranties is limited, at our option, to:<br /><br />a. in the case of products: the replacement of the products or the supply of equivalent products; or the payment of the cost of replacing the products or acquiring equivalent products;<br /><br>b. in the case of services: the supply of the services again; or the payment of the cost of having the services supplied again.
                            </p><h3>CONFIDENTIALITY</h3><p>You can communicate with us through this website. The website also lists other ways you can communicate with us.<br /><br />We do not accept information that is confidential or proprietary, other than for making travel arrangements or reservations. Please understand that this is our policy.<br /><br>If you are concerned about the confidentiality of information you are sending us being compromised, do not transmit that information to us through this website; rather, mail or email the information to us instead. Please note, however, that any ideas or suggestions that you send or reveal to us through this website or otherwise are ours to use or disclose without limitation or restriction, even if you have marked the information as being confidential or proprietary or if you include statements that are contrary to these Terms &amp; Conditions.</p><h3>LINKING</h3><p>We may link our website to other websites on the Internet. We do this strictly for your convenience as you explore different travel options online. However, the inclusion of any such links does not indicate that we endorse the website or the business to which we have linked. Further, we have not verified the content of any website to which we have linked, and we bear no responsibility whatsoever for the content of any linked website. Should you incur any loss or damage from visiting or doing business with any linked website or business, we are not liable for that loss or damage.</p><h3>AMENDMENTS</h3><p>We may amend these Terms and Conditions at any time without prior notice to you, except as otherwise specified. We will post the amended Terms and Conditions on this website, and they will take effect immediately upon being posted on the website.</p><h3>TERMINATION</h3><p>We reserve the right to immediately terminate this Agreement as well as any other agreement between you and us if you breach any of these Terms and Conditions.</p><h3>OUR RELATIONSHIP</h3><p>No agency, partnership, joint venture, employer-employee or franchisor-franchisee relationship exists between you and us, nor is such a relationship created between you and us by these Terms and Conditions or by our Agreement with you.</p><br><h3>GOVERNING LAW</h3><p>Should any legal dispute arise concerning the interpretation or application of these Terms and Conditions and/or this Agreement, or should any legal dispute arise because of your use of this website, we will select the applicable legal jurisdiction and venue in our sole discretion.</p><h3>GENERAL</h3><p><span></span><span></span><span></span><span></span><span></span><span></span><span></span><span></span><span></span><span></span><span></span><span></span><span></span><span></span><span></span><span></span><span></span><span></span><span></span></p><p>If any of these Terms and Conditions is found by a court or other legal authority to be invalid or unenforceable, the invalid or unenforceable provisions will be stricken. The remaining terms and conditions will remain in full force and effect.<br /><br />The headings used in these Terms and Conditions are for reference purposes only.<br /><br />If we take no action in response to a violation by you or others of one or more of these Terms and Conditions, that inaction shall not constitute a waiver of the violated terms and conditions and shall not impair our right to take action in response to subsequent or similar violations.<br />In this Agreement, the term “website” includes any e-mail bulletins or other content that we provide to you through this website or otherwise initiated from this website.</p><p></p>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>";
        return Terms;
    }

    #endregion

    #region Check Success Message Check Box on pageLoad

    public static bool Check_Passenger_Status(string BookingID)
    {
        bool result = false;
        SqlConnection sqlcon = DataConnection.GetConnection();
        sqlcon.Open();
        try
        {
            SqlCommand cmd = new SqlCommand("select * from Passenger_perference Where Passenger_perference_Booking_ID ='" + BookingID + "'", sqlcon);
            using (SqlDataAdapter a = new SqlDataAdapter(cmd))
            {
                dtFetchPassengerRequest = new DataTable();
                a.Fill(dtFetchPassengerRequest);
                if (dtFetchPassengerRequest.Rows.Count > 0)
                {
                    result = true;
                }
            }
        }
        catch (Exception ex)
        {
            var message = ex.Message.ToString();
        }
        finally
        {
            sqlcon.Close();
        }
        return result;
    }

    #endregion

    #region IF NO ININERY FIND THEN SAVE DATA FROM NO_XP

    public static void GET_SET_Booking_NO_XP(DataTable dataTable)
    {
        SqlParameter[] param = new SqlParameter[3];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnection())
            {
                param[0] = new SqlParameter("@ParamBookingID", SqlDbType.VarChar)
                {
                    Value = dataTable.Rows[0]["BookingID"]
                };
                param[1] = new SqlParameter("@ParamNoXP", SqlDbType.VarChar)
                {
                    Value = dataTable.Rows[0]["NO_XP"]
                };
                param[2] = new SqlParameter("@ParamCounter", SqlDbType.VarChar)
                {
                    Value = "INSERT"
                };
                SqlHelper.ExecuteNonQuery(conection, CommandType.StoredProcedure, "GET_SET_Booking_NO_XP", param);
            }
        }
        catch (Exception ex)
        {

        }
    }


    #endregion

    #region Check selected values in Special Request Tab

    public string AutoFilledPassengerDetails(string Value, string PreferenceName)
    {
        string result = string.Empty;
        if (PreferenceName == "MEAL_PREFERENCE")
        {
            if (!string.IsNullOrEmpty(Value))
            {
                //meal pref
                if (Value == "AVML")
                {
                    result = "checked";
                }
                if (Value == "BBML")
                {
                    result = "checked";
                }
                if (Value == "BLML")
                {
                    result = "checked";
                }
                if (Value == "CHML")
                {
                    result = "checked";
                }
                if (Value == "DBML")
                {
                    result = "checked";
                }
                if (Value == "FPML")
                {
                    result = "checked";
                }
                if (Value == "GFML")
                {
                    result = "checked";
                }
                if (Value == "HNML")
                {
                    result = "checked";
                }
                if (Value == "KSML")
                {
                    result = "checked";
                }
                if (Value == "MOML")
                {
                    result = "checked";
                }
                if (Value == "RVML")
                {
                    result = "checked";
                }
                if (Value == "SFML")
                {
                    result = "checked";
                }
                if (Value == "SPML")
                {
                    result = "checked";
                }
                if (Value == "VGML")
                {
                    result = "checked";
                }
                if (Value == "OTML")
                {
                    result = "checked";
                }
            }
        }

        if (PreferenceName == "SEAT_PREFERENCE")
        {
            if (!string.IsNullOrEmpty(Value))
            {
                if (Value == "WINDOW")
                {
                    result = "checked";
                }
                if (Value == "AISLE")
                {
                    result = "checked";
                }
            }
        }

        if (PreferenceName == "WHEELCHAIR_PREFERENCE")
        {
            if (!string.IsNullOrEmpty(Value))
            {
                //Wheel Chair pref
                if (Value == "NEEDS HELP FOR LONG DISTANCE CAN ASCEND/DESCEND STAIRS")
                {
                    result = "checked";
                }
                if (Value == "NEEDS HELP FOR LONG DISTANCE CANNOT ASCEND/DESCEND STAIRS")
                {
                    result = "checked";
                }
                if (Value == "COMPLETELY IMMOBILE")
                {
                    result = "checked";
                }
            }
        }

        return result;
    }

    #endregion

    #region Change Passenger Type to their full values

    public string PassengerType(string PaxType)
    {
        string result = string.Empty;
        switch (PaxType)
        {
            case "ADT":
                result = "Adult";
                break;
            case "CNN":
            case "CHD":
                result = "Child";
                break;
            case "INF":
                result = "Infant without a seat";
                break;
            case "INS":
                result = "Infant with a seat";
                break;
            case "UNN":
                result = "Unaccompanied child";
                break;
            case "YTH":
                result = "Youth Confirmed";
                break;
        }
        return result;
    }

    #endregion


    public void WeatherDetails(string DestinationCity)
    {
        StringBuilder sb_weather = new StringBuilder();
        ResultViewModel rVM = new ResultViewModel();
        if (!string.IsNullOrEmpty(DestinationCity))
        {
            rVM = Weather_Info.GetWeatherInfo(DestinationCity);
        }

        if (rVM != null)
        {
            if (!string.IsNullOrEmpty(rVM.City))
            {
                string OM_URL = "http://openweathermap.org/img/w/";
                sb_weather.Append("<div class='weather-info-details text-center'>" +
                    "<h3>" + rVM.City + ", " + rVM.Country + "</h3> " +
                    "<p>" +
                    "<span class='temprature'><b>" + rVM.Temp + "<sup>°</sup>" +
                    "</b></span>" +
                    "<span class='weather-time-info'></span>" +
                    "<span class='weather-icon-box'>" +
                        "<img src=" + OM_URL + rVM.WeatherIcon + ".png" + " alt='Weather Icon'/></span> " +
                    "<span class='weather-type'>" + rVM.Description + "</span>" +
                "</p>" +
                "</div>");
            }
            else
            {
                sb_weather.Append("<a id='link_current' href='#' class='aw-current-weather'> " +
                     "<div class='aw-current-weather-inner'> " +
                        "<h3>NA</h3> " +
                        "<span class='aw -icon aw-icon-7-l' data-icon='7'></span> " +
                        "<p class='aw -temp-time-desc'> " +
                            "<span class='aw -temperature-today'><b>NA<sup></sup></b></span> " +
                            "<time>NA</time> " +
                            "<span class='aw -weather-description'>NA</span> " +
                        "</p> " +
                    "</div> " +
                "</a>");
            }
        }
        lblWeatherDetails.Text = sb_weather.ToString();
    }
}