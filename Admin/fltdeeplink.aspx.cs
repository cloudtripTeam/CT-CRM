using System;
using System.Runtime.Remoting.Messaging;
using System.Web.UI;
using BLL;
using EL.Flight;
using System.Threading;
using System.Collections.Generic;
using System.Web;
using System.Data;

public partial class Admin_fltdeeplink : System.Web.UI.Page
{
    string filePath = string.Empty;
    string filePathFlaxi = string.Empty, searchID = string.Empty;
    string from, to, departDate, returnDate, cabinClass, preferedAirline, company, campaign, campaignType, pwd, totalPrice, index, valCarrier = string.Empty;
    bool tripType, nonStop, isCalendar = false; int Adults, Childs, infants = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        string _arriveDate = string.Empty;

        try
        {

            Init_SearchQuery();
            SearchLowFares(HttpContext.Current);
            SecureQueryString qs1 = new SecureQueryString();

            filePath = HttpContext.Current.Server.MapPath("~/App_Data/Result/" + searchID.ToString() + ".txt");
            SearchAsynch osysnch = new SearchAsynch();
            IAsyncResult ar = osysnch.FlightSearchAsync(SearchDetails.Current(searchID.ToString()), filePath);
            Session["fltResult"] = ar;
            if (isCalendar)
                qs1["redirectPage"] = "resultflx.aspx";
            else
                qs1["redirectPage"] = "flight-result.aspx";

            qs1["searchID"] = searchID.ToString();
            SearchDetails oSearch = SearchDetails.Current(searchID.ToString());
            EL.Credential oCred = new EL.Credential();
            oCred.Company_ID = company;
            oCred.Hap = campaign;
            oCred.Hap_Password = pwd;
            oCred.Hap_Type = campaignType;
            oSearch.flightFareSearchRQ.Credentials.Add(oCred);
            qs1["origin"] = oSearch.flightFareSearchRQ.Segments[0].Origin.AirportCityName;
            qs1["destination"] = oSearch.flightFareSearchRQ.Segments[0].Destination.AirportCityName;
            qs1["originCode"] = oSearch.flightFareSearchRQ.Segments[0].Origin.AirportCode;
            qs1["destinationCode"] = oSearch.flightFareSearchRQ.Segments[0].Destination.AirportCode;
            qs1["DepartDate"] = oSearch.flightFareSearchRQ.Segments[0].Date;
            if (oSearch.flightFareSearchRQ.Segments.Count == 2)
            { qs1["ReturnDate"] = oSearch.flightFareSearchRQ.Segments[1].Date; ; }
            else
            { qs1["ReturnDate"] = ""; }

            Response.Redirect("wait.aspx?q=" + qs1.ToString());

        }
        catch (ExpiredQueryStringException)
        {
            //misce.SetError("", "Your query string has been expired!");
            return;
        }
        catch (InvalidQueryStringException)
        {
            // misce.SetError("", "Your query string is invalid or corrupt!");
            return;
        }
        catch (Exception ex)
        {
            // misce.SetError("", query);
            return;
        }

    }




    #region Properties
    public string SourceMedia
    {
        get;
        set;
    }
    #endregion

    public void SearchLowFares(object someParameter)
    {
        EL.Trip_Type TT = EL.Trip_Type.Return_Trip;

        if (tripType)
        { TT = EL.Trip_Type.Return_Trip; }
        else if (tripType == false)
        { TT = EL.Trip_Type.OneWay_Trip; }

        BLL.FlightsBL OLowfareSearch = new BLL.FlightsBL();
        searchID = Guid.NewGuid().ToString();
        OLowfareSearch.LowFareSearch(from, to, departDate, returnDate,
               TT, cabinClass, Adults, Childs,
               infants, nonStop, isCalendar ? 3 : 0, preferedAirline, company, campaign, pwd, campaignType, searchID, someParameter,0);

    }

    private void Init_SearchQuery()
    {



        string query = string.Empty;
        #region Set Searching parameters

        from = Request.Params["org"].ToString();

        to = Request.Params["dest"].ToString();

        departDate = Request.Params["departDate"].ToString();

        returnDate = Request.Params["returnDate"].ToString();

        Adults = Convert.ToInt32(Request.Params["adt"]);
        query += " adt " + Adults;
        Childs = Convert.ToInt32(Request.Params["chd"]);
        query += " chd " + Childs;
        infants = Convert.ToInt32(Request.Params["inf"]);
        query += " inf " + infants;
        isCalendar = Convert.ToBoolean(Request.Params["isFlx"]);

        if (Request.Params["JType"] != null)
        {
            if (Request.Params["JType"] == "2")
                tripType = true;
            else
                tripType = false;
        }

        if (Request.Params["isReturn"] != null)
        {
            if (Request.Params["isReturn"] == "true")
                tripType = true;
            else
                tripType = false;
        }




        preferedAirline = Request.Params["airline"].ToString();

        query += " airline " + valCarrier;
        cabinClass = Request.Params["classType"].ToString();
        query += " cabinClass " + cabinClass;
        campaign = Request.Params["campaign"].ToString();
        company = Request.Params["company"].ToString();


        #region Set Company and Credintial password
        System.Data.DataTable dt = BLL.Miscellaneous.GET_Campaign_Master(campaign, company);
        if (dt != null && dt.Rows.Count > 0)
        {
            campaign = dt.Rows[0]["CampID"].ToString();
            company = dt.Rows[0]["CompanyID"].ToString();
            pwd = dt.Rows[0]["CampPassword"].ToString();
            campaignType = "LIVE";
        }
        else
        {
           
            campaign = "";
            company = "";
            pwd = "";
            campaignType = "" ;
        }
        #endregion


        BLL.PageTracker oTrack = new PageTracker();
        oTrack.PageTrack(HttpContext.Current, from, to, departDate, returnDate, campaign, query, campaign);
        #endregion


    }
}