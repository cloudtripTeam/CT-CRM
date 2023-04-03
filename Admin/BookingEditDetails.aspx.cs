using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_BookingEditDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserDetails"] != null)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["BID"] != null && Request.QueryString["PID"] != null)
                {
                    hfBookingID.Value = Request.QueryString["BID"].ToString();
                    hfProdID.Value = Request.QueryString["PID"].ToString();
                    UserDetail objUserDetail = HttpContext.Current.Session["UserDetails"] as UserDetail;
                    hfUpdatedBy.Value = objUserDetail.userID;
                }
                else
                {
                    Response.Redirect("BookingEditDetails.aspx");
                }
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx", false);
        }
    }
    #region Booking Details



    [WebMethod]
    public static string GetBookingSummary(string BookingID, string ProdID)
    {
        JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
        return javaScriptSerializer.Serialize(new Itinerary.FlightDetails(BookingID, ProdID, true, true, false, true, true, false, false, false, false));
    }

    [WebMethod]
    public static string GetPassengerDetails(string BookingID, string ProdID)
    {
        JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
        return javaScriptSerializer.Serialize(new Itinerary.FlightDetails(BookingID, ProdID, false, false, true, false, false, false, false, false, false));

    }

    [WebMethod]
    public static string GetSectorDetails(string BookingID, string ProdID)
    {
        JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
        return javaScriptSerializer.Serialize(new Itinerary.FlightDetails(BookingID, ProdID, false, false, false, false, false, true, false, false, false));

    }

    [WebMethod]
    public static string GetChargeDetails(string BookingID, string ProdID)
    {
        JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
        return javaScriptSerializer.Serialize(new Itinerary.FlightDetails(BookingID, ProdID, false, false, false, false, false, false, true, false, false));

    }

    [WebMethod]
    public static string GetTrnsDetails(string BookingID, string ProdID)
    {
        JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
        return javaScriptSerializer.Serialize(new Itinerary.FlightDetails(BookingID, ProdID, false, false, false, false, false, false, false, true, false));
    }
    [WebMethod]
    public static string EditBookingSummary(string BookingID, string ProdID, string PNR, string Origin, string Destination, string EmailID, string MobileNo,
        string PhoneNo, string Remarks, string BookingStatus, string UpdatedBy)
    {
        try
        {
            GetSetDatabase objGetSetDatabase = new GetSetDatabase();
            objGetSetDatabase.SET_Booking_Detail(BookingID, ProdID, "", "", "", "", "", "", "", Remarks, "", PNR, "", "", "", UpdatedBy, "", "", "Update","");
           // objGetSetDatabase.SET_Sectors_Master(BookingID, ProdID, "", "", Origin, Destination, "", "", UpdatedBy, "Update","","");
            //objGetSetDatabase.SET_Contact_Detail(BookingID, ProdID, "", "1", PhoneNo, MobileNo, "", EmailID, "", "", "", "", "", "", UpdatedBy, "Update");
            return "true";
        }
        catch
        {
            return "false";
        }
    }
    [WebMethod]
    public static string UpdatePassenger(string SrNo, string Title, string FName, string Lname, string DOB, string UpdatedBy)
    {
        try
        {
            GetSetDatabase objGetSetDatabase = new GetSetDatabase();
            return objGetSetDatabase.SET_Passenger_Detail("", "", SrNo, "", Title.Trim().ToUpper(), FName.Trim().ToUpper(), "", Lname.Trim().ToUpper(), "", "", "", "", "", "",
                DOB.Trim(), "", "", UpdatedBy, "Update","").ToLower();

        }
        catch
        {
            return "false";
        }
    }
    [WebMethod]
    public static string UpdateSectorDetails(List<Itinerary.SectorDetails> ESectors)
    {
        try
        { 
            GetSetDatabase objGetSetDatabase = new GetSetDatabase();
            int ctr = 0;
            foreach (Itinerary.SectorDetails SD in ESectors)
            { 
                if(SD.SrNo!="")
                {
                    try
                    {
                        SD.FromDateTime = (DateTime.ParseExact(SD.FromDateTime.Trim(), "dd MMM yyyy HHmm", CultureInfo.InvariantCulture)).ToString("dd MMM yyyy HH:mm");
                    }
                    catch{ SD.FromDateTime="";}
                    try
                    {
                        SD.ToDateTime = (DateTime.ParseExact(SD.ToDateTime.Trim(), "dd MMM yyyy HHmm", CultureInfo.InvariantCulture)).ToString("dd MMM yyyy HH:mm");
                    }
                    catch { SD.ToDateTime = ""; }

                    if (objGetSetDatabase.SET_Sector_Detail(SD.BookingID, SD.ProdID, SD.SrNo, SD.CarierName.Trim().ToUpper(), SD.FromDest.Trim().ToUpper(), SD.FromDateTime,
                        SD.ToDest.Trim().ToUpper(), SD.ToDateTime, SD.FlightNo, SD.FClass.Trim().ToUpper(), "", "", "", "", "", SD.TerminalFrom.Trim().ToUpper(), 
                        SD.TerminalTo.Trim().ToUpper(), "", "", "", SD.ModifiedBy, "Update",SD.AirlineConfirmationCode,SD.CabinClass) == "true")
                        ctr++;
                }
            }           
          return  ESectors.Count==ctr? "true": "false";
        }
        catch
        {
            return "false";
        }
    }

    [WebMethod]
    public static string UpdateChargeDetails(List<Itinerary.AmountChargesDetail> ECharges)
    {
        try
        {
            GetSetDatabase objGetSetDatabase = new GetSetDatabase();
            int ctr = 0;
            foreach (Itinerary.AmountChargesDetail SD in ECharges)
            {
                if (SD.SrNo != "")
                {
                    if (objGetSetDatabase.SET_Amount_Charges_Detail(SD.BookingID, SD.ProdID, SD.SrNo, SD.ChargeID, SD.ChargesFor, SD.CostPrice,
                        SD.SellPrice,"","",SD.ChrgesRemarks,"",SD.ModifiedBy, "Update") == "true")
                        ctr++;
                }
            }
            return ECharges.Count == ctr ? "true" : "false";
        }
        catch
        {
            return "false";
        }
    }
    #endregion
}