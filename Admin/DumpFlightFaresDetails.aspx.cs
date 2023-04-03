using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_DumpFlightFaresDetails : CompressedPage
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserDetails"] != null)
        {
            if (!IsPostBack)
            {

                UserDetail objUserDetail = Session["UserDetails"] as UserDetail;
                if (!objUserDetail.isAuth("DumpFlightFaresDetails.aspx"))
                {
                    Response.Redirect("Default.aspx");
                    return;
                }  
            }
        }
        else
        {
            Response.Redirect("..", false);
        }
    }
    [WebMethod(EnableSession = true)]
    public static string GetDumpAirOfferFare(string FromDestination, string ToDestination, string Airline, string FClass,
        string ClassType, string WebSite, string TravelStartFrom, string TravelStartTo, string TravelEndFrom, string TravelEndTo,
        string DumpDateFrom, string DumpDateTo, string ExpDateFrom, string ExpDateTo)
    {
        List<DumpAirOffer> lst = new List<DumpAirOffer>();
        try
        {
            GetSetDatabase objGetSetDatabase = new GetSetDatabase();
            DataTable dt = objGetSetDatabase.GET_FR_Dmp_AirFareOffers("", FromDestination, ToDestination, Airline, FClass, ClassType, "", "", "", "", DumpDateFrom, DumpDateTo, "", TravelStartFrom, TravelStartTo, TravelEndFrom, TravelEndTo, "", ExpDateFrom, ExpDateTo, WebSite, "", "", "", "", "");
           
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {

                    DataView dv = dt.DefaultView;
                    //   Sort data
                    dv.Sort = "Total";
                    //   Convert back your sorted DataView to DataTable
                    dt = dv.ToTable();

                    foreach (DataRow dr in dt.Rows)
                    {
                        lst.Add(new DumpAirOffer(dr));
                    }
                
                }
               
            }
        }
        catch { }
        JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
        return javaScriptSerializer.Serialize(lst);
    }
    [WebMethod(EnableSession = true)]
    public static string UpdateAirOfferFare(string FaredetailId, string BaseFare, string Tax, string From, string To, string Airline_Code, string CClass,
        string ClassType, string ExpOffers_Date, string Travel_DateStart, string Travel_DateEnd)
    {

        try
        {
            double total = (BaseFare == "" ? 0 : Convert.ToDouble(BaseFare)) + (Tax == "" ? 0 : Convert.ToDouble(Tax));
            GetSetDatabase objGetSetDatabase = new GetSetDatabase();
            return objGetSetDatabase.SET_FR_Dmp_AirFareOffers(FaredetailId, From.ToUpper(), To.ToUpper(), Airline_Code.ToUpper(), CClass.ToUpper(), ClassType.ToUpper(),
                BaseFare, Tax, (total == 0 ? "" : total.ToString()), "", "", Travel_DateStart, Travel_DateEnd, ExpOffers_Date, "Update").ToString().ToLower();

        }
        catch { return ""; }

    }

}
