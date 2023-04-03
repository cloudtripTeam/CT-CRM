
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HiQPdf;

public partial class Admin_DocuSignFile : System.Web.UI.Page
{
    DataTable dtPax = new DataTable();
    DataTable dtSector = new DataTable();
    DataTable dtComp = new DataTable();
    DataTable dtContact = new DataTable();
    DataTable dtPrice = new DataTable();
    DataTable dtTrans = new DataTable();
    DataTable dtBD = new DataTable();
    Layout lot = new Layout();
    FandHServices.FandHServicesClient obj = new FandHServices.FandHServicesClient();
    public string Invoice1 { get; set; }
    public  string AuthSupplier { get; set; }
    public  string Airline_Supplier { get; set; }
    public  string SellCost { get; set; }
    public  string CardNum { get; set; }
    public  string Expiry { get; set; }
    public  string CT { get; set; }
    public string CardHolder { get; set; }
    
UserDetail objUserDetail;

    protected void Page_Load(object sender, EventArgs e)
    {
        


        objUserDetail = new UserDetail();
        //objUserDetail.userID = "Rozer";
        objUserDetail = Session["UserDetails"] as UserDetail;
        if (Session["UserDetails"] != null)
        {
            if (!IsPostBack)
            {

                //if (!objUserDetail.isAuth("Invoice"))
                //{
                //    Response.Redirect("~/Admin/AccessDenied.aspx");
                //    return;
                //}

                if (!string.IsNullOrEmpty(Request.QueryString.Get("BID")))
                {

                     CT = Request.QueryString.Get("BID");
                    BookingDetails(CT);
                    if (!string.IsNullOrEmpty(Request.QueryString.Get("Auth")))
                    {
                        if (Request.QueryString.Get("Auth") == "ATH")
                        {
                            AuthSupplier = "Air Tickets Hub / Global eFare";
                        }
                        else if (Request.QueryString.Get("Auth") == "ATX")
                        {
                            AuthSupplier = "Airtax";
                        }

                        if (!string.IsNullOrEmpty(Request.QueryString.Get("AirlineName")))
                        {
                            Airline_Supplier = Request.QueryString.Get("AirlineName") + " & ";
                        }

                        if (!string.IsNullOrEmpty(Request.QueryString.Get("SellCost")))
                        {
                            SellCost = Request.QueryString.Get("SellCost");
                        }
                        if (!string.IsNullOrEmpty(Request.QueryString.Get("CardNum")))
                        {
                            CardNum = Request.QueryString.Get("CardNum");
                        }
                        if (!string.IsNullOrEmpty(Request.QueryString.Get("Expiry")))
                        {
                            Expiry = Request.QueryString.Get("Expiry");
                        }
                        if (!string.IsNullOrEmpty(Convert.ToString(Request.QueryString.Get("CardHolder"))))
                        {
                            CardHolder = Request.QueryString.Get("CardHolder");
                        }
                        else
                        {
                            CardHolder = dtPax.Rows[0]["FName"] + " " + dtPax.Rows[0]["MName"] + " " + dtPax.Rows[0]["LName"];
                        }

                    }
                   
                    generate_Invoice_Pdf(generate_Invoice(CT), CT);
                }

            }
        }
        else
        {
            Response.Redirect("~/Login.aspx", false);
        }
        
    }
    
    public void BookingDetails(string XP)
    {
        dtPax = obj.GET_Passenger_Detail(XP, "001");
        dtSector = obj.GET_Sector_Detail(XP, "001");
        dtComp = obj.GET_Booking_Master(XP);
        dtBD = obj.GET_Booking_Detail1(XP, "001", "", "", "", "", "", "", "", "");
        // dtPrice=obj.GET_Amount_Charges_Detail()
        GetSetDatabase objGetSetDatabase = new GetSetDatabase();
        dtPrice = objGetSetDatabase.GET_Amount_Charges_Detail(XP, "001", "", "", "", "", "", "", "");
    }
    private string generate_Invoice(string xp)
    {
        //string heading = "Itinerary/e-ticket";

        string heading = "Payment Authorization";


        CompanyDetails objc = lot.SetCompanyDetail(dtComp.Rows[0]["BookingByCompany"].ToString());
        #region
        string inv = @"<table width='1000' border='0' align='center' cellpadding='0' cellspacing='0' style='font-family:Tahoma, Arial, Helvetica, sans-serif;'>" +
        "<tr>" +
        "<td style='padding:30px;'><table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
        "<tr>" +
        "<td align='left' valign='top'><table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
        "<tr>" +
        "<td align='left' valign='top'></td>" +
        "<td align='right' valign='top' style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>" + objc.Comp_Address + "<br />" +
        "" + "<k style='font-weight:bold;font-size:20px'>Tel: " + objc.Comp_contact + "</k><br/>" +
        //objc.Comp_Emailid + " <br/>" +
        //"www." + objc.Comp_Emailid.Split('@')[1].ToString() + "</td>" +
        "</tr>" +
        "</table></td>" +
        "</tr>" +
        "<tr>" +
        "<td align='left' valign='top' style='font-size:24px; text-align:center; border-bottom:#666 solid 1px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>" + heading + "</td>" +
        "</tr>" +
        "<tr>" +
        "<td align='left' valign='top'>&nbsp;</td>" +
        "</tr>" +
        "<tr>" +
        "<td align='left' valign='top'><table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
        "<tr>" +
        "<td align='left' valign='top'><table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
        "<tr>" +
        "<td align='left' valign='top'><table width='100%' border='0' cellspacing='0' cellpadding='0'>" +


        "</table></td>" +

        "<table width='100%'><tr>" +
        "<td width='50%' align='left' valign='top' style='padding-bottom:10px; padding-top:10px; font-size:13px; font-weight:600; font-family:Tahoma, Arial, Helvetica, sans-serif;'><strong>Booking Summary :</strong></td>" +
        "<td width='50%' align='right' valign='top' style='padding-bottom:10px; padding-top:10px; font-size:13px; font-weight:600; font-family:Tahoma, Arial, Helvetica, sans-serif;'><strong>Agent: " + objUserDetail.userID+ "</strong></td>" +
        "</tr></table>" +
         "<tr>" +
        "<td align='left' valign='top' style='background:#2e4b6b; padding:10px;'><table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
        "</tr>" +
        "<tr>" +
        "<td height='21' align='left' valign='top' style='color:#02c9e8; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'><strong>Card Holder</strong></td>" +
        "<td height='21' align='left' valign='top' style='color:#02c9e8; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'><strong>Booking Ref</strong></td>" +


        "<td align='left' valign='top' style='color:#02c9e8; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'><strong>Booking Date</strong></td>" +
        "<td align='left' valign='top' style='color:#02c9e8; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'><strong>PNR</strong></td>" +

        "<td align='left' valign='top' style='color:#02c9e8; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'><strong></strong></td>" +
        "</tr>" +
        "<tr>" +
        "<td align='left' valign='top' style='color:#FFF; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>" + CardHolder + "</td>" +
        "<td align='left' valign='top' style='color:#FFF; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>" + xp + "</td>" +

        "<td align='left' valign='top' style='color:#FFF; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>" + Convert.ToDateTime(dtBD.Rows[0]["BookingDateTime"].ToString()).ToString("ddd, dd MMM yy") + "</td>" +
        "<td align='left' valign='top' style='color:#FFF; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>" + dtBD.Rows[0]["PNR"] + "</td>" +
       
        "<td align='left' valign='top' style='color:#FFF; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'></td>" +
        "</tr>" +
        "</table></td>" +
        "</tr>" +
        "</table></td>" +
        "</tr>" +
        "<tr>" +
        "<td align='left' valign='top'>&nbsp;</td>" +
        "</tr>" +
        "<tr>" +
        "<td align='left' valign='top'><table width='100%' border='0' cellspacing='0' cellpadding='0'>" +

        "</td>" +
        "</tr>" +
        "<tr>" +
        "<td align='left' valign='top' style='padding-bottom:10px; padding-top:10px; font-size:13px; font-weight:600; font-family:Tahoma, Arial, Helvetica, sans-serif;'><strong>Passenger & Ticket Details :</strong></td>" +
        "</tr>" +
        "<tr>" +
        "<td align='left' valign='top'><table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
        "<tr>" +
        "<td align='left' valign='top' style='color:#FFF;'>" +
        "<table width='100%' border='0' cellspacing='0' cellpadding='0' style='background:#e3e3e3'>" +
        "<tr>" +
        "<td width='25%' align='left' valign='top'  style='color:#FFF; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;background:#2e4b6b;padding-bottom:10px; padding-top:10px;padding-left:5px;'>First Name</td>" +
        "<td width='25%'  align='left' valign='top' style='color:#FFF; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;background:#2e4b6b;padding-bottom:10px; padding-top:10px;padding-left:5px;'>Middle Name</td>" +
        "<td width='25%'  align='left' valign='top' style='color:#FFF; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;background:#2e4b6b;padding-bottom:10px; padding-top:10px;padding-left:5px;'>Last Name</td>" +
        "<td width='25%'  align='left' valign='top' style='color:#FFF; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;background:#2e4b6b;padding-bottom:10px; padding-top:10px;padding-left:5px;'>DOB</td>" +

        "</tr>";
        foreach (DataRow drPax in dtPax.Rows)
        {
            inv += "<tr>" +
            "<td width='25%' align='left' valign='top' style='padding-bottom:10px; padding-top:10px;padding-left:5px;border-bottom:1px solid #333;font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif; padding-bottom:10px;'>" + drPax["Title"] + " " + drPax["FName"] + "</td>" +
            "<td width='25%'  align='left' valign='top' style='padding-bottom:10px; padding-top:10px;padding-left:5px;border-bottom:1px solid #333;font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif; padding-bottom:10px;'>  " + drPax["MName"] + "</td>" +
            "<td width='25%'  align='left' valign='top' style='padding-bottom:10px; padding-top:10px;padding-left:5px;border-bottom:1px solid #333;font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif; padding-bottom:10px;'>  " + drPax["LName"] + "</td>" +
            "<td width='25%'  align='left' valign='top' style='padding-bottom:10px; padding-top:10px;padding-left:5px;border-bottom:1px solid #333;font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif; padding-bottom:10px;'>  " + Convert.ToDateTime((drPax["DOB"])).ToString("dd MMM yyyy") + "</td>" +

            "</tr>";
        }
        inv += "</table>" +
        "</td>" +
        "</tr>" +
        "<tr>" +
        "<td align='left' valign='top'><table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
       "&nbsp;" +
        "</td>" +
         "</tr>" +
        "<tr>" +
        "<td align='left' valign='top' style='padding-bottom:10px; padding-top:10px; font-size:13px; font-weight:600; font-family:Tahoma, Arial, Helvetica, sans-serif;'><strong>Flight Details :</strong></td>" +
        "</tr>" +


        "<tr>" +
        "<td align='left' valign='top'>" +


"<table width='100%' border='0' cellspacing='0' cellpadding='0' style='margin-bottom:10px; border-bottom:#cbcbcb solid 1px; padding-bottom:10px;background:#e3e3e3'>" +
        "<tr>" +
       "<td width='80' align='left' valign='top' style='color:#FFF; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;padding-bottom:10px; padding-top:10px;padding-left:5px;background:#2e4b6b;'>Airline</td>" +
        "<td width='180' align='left' valign='top' style='color:#FFF; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;padding-bottom:10px; padding-top:10px;padding-left:5px;background:#2e4b6b;'>From</td>" +
        "<td width='180' align='left' valign='top' style='color:#FFF; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;padding-bottom:10px; padding-top:10px;background:#2e4b6b;padding-left:5px;'>To</td>" +
        "<td width='100' align='left' valign='top' style='color:#FFF; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;padding-bottom:10px; padding-top:10px;background:#2e4b6b;padding-left:5px;'>Depart</td>" +
        "<td width='100' align='left' valign='top' style='color:#FFF; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;padding-bottom:10px; padding-top:10px;background:#2e4b6b;padding-left:5px;'>Arrival</td>" +

        "<td align='left' valign='top' style='color:#FFF; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;padding-bottom:10px; padding-top:10px;background:#2e4b6b;padding-left:5px;'>Cabin</td>" +
        "<td align='left' valign='top' style='color:#FFF; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;padding-bottom:10px; padding-top:10px;background:#2e4b6b;padding-left:5px;'>Baggage</td>" +
        "<td align='left' valign='top' style='color:#FFF; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;padding-bottom:10px; padding-top:10px;background:#2e4b6b;padding-left:5px;'>Airline Ref.</td>" +


        "</tr>";

        foreach (DataRow drFlight in dtSector.Rows)
        {
            inv += "<tr>" +
                "<td align='left' width='80' valign='top' style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;padding-bottom:10px; padding-top:10px;padding-left:5px;  '>" + "<img style='border-radius:5px;border:2px solid #fff;padding:2px' src='http://crm.cloudtrip.us/images/alogo/" + drFlight["CarierName"] + "m.png'  height='35' alt='Logo' />" + "</td>" +
            "<td width='180' align='left' valign='top' style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;padding-bottom:10px; padding-top:10px;padding-left:5px;'>" +
            "<strong>" + drFlight["FromCityName"] + "</strong><br />" +
            "" + drFlight["FromDestName"] + " (" + drFlight["FromDest"] + ")" +
            "</td>" +
            "<td width='180' align='left' valign='top' style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;padding-bottom:10px; padding-top:10px;padding-left:5px;'>" +
            "<strong>" + drFlight["ToCityName"] + "</strong><br />" +
            drFlight["ToDestName"] + " (" + drFlight["ToDest"] + ")" +
            "</td>" +
            "<td width='130' align='left' valign='top' style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;padding-bottom:10px; padding-top:10px;padding-left:5px;'>" + Convert.ToDateTime(drFlight["FromDateTime"]).ToString("ddd, dd MMM yy") + "<br>" + SET_TIME_AM_PM(Convert.ToDateTime(drFlight["FromDateTime"]).ToString("HH:mm")) + "<br><b>" + drFlight["CarierName"] + drFlight["FlightNo"] + "</b></td>" +
            "<td width='130' align='left' valign='top' style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;padding-bottom:10px; padding-top:10px;padding-left:5px;'>" + Convert.ToDateTime(drFlight["ToDateTime"]).ToString("ddd, dd MMM yy") + "<br>" + SET_TIME_AM_PM(Convert.ToDateTime(drFlight["ToDateTime"]).ToString("HH:mm")) + "<br><b>" + drFlight["CarierName"] + drFlight["FlightNo"] + "</b></td>" +

            "<td align='left' valign='top' style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;padding-bottom:10px; padding-top:10px;padding-left:5px;  '>" + Convert.ToString(drFlight["CabinClass"]) + "</td>" +
            "<td align='left' valign='top' style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;padding-bottom:10px; padding-top:10px;padding-left:5px;  '>" + Convert.ToString(drFlight["BaggageAllownce"]) + " pc</td>" +
            "<td align='left' valign='top' style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;padding-bottom:10px; padding-top:10px;padding-left:5px;  '>" + Convert.ToString(drFlight["AirlineConfirmationCode"]) + "</td>" +

            "</tr>";
        }
        inv += "</table>" +

        "</td>" +
        "</tr>" +
         "<tr>" +
        "<td align='left' valign='top'><table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
       "&nbsp;" +
        "</td>" +
         "</tr>" +
         "<tr>" +
        "<td align='left' valign='top' style='padding-bottom:10px; padding-top:10px; font-size:13px; font-weight:600; font-family:Tahoma, Arial, Helvetica, sans-serif;'><strong>Price Details :</strong></td>" +
        "</tr>" +
         "<tr>" +
        "<td align='left' valign='top' style='color:#FFF;'>" +
        "<table width='100%' border='0' cellspacing='0' cellpadding='0' style='background:#e3e3e3'>" +
        "<tr>" +
        "<td width='25%' align='left' valign='top'  style='color:#FFF; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;background:#2e4b6b;padding-bottom:10px; padding-top:10px;padding-left:5px;'>PAX Type</td>" +
        "<td width='25%'  align='left' valign='top' style='color:#FFF; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;background:#2e4b6b;padding-bottom:10px; padding-top:10px;padding-left:5px;'>Per PAX Cost</td>" +
        "<td width='25%'  align='left' valign='top' style='color:#FFF; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;background:#2e4b6b;padding-bottom:10px; padding-top:10px;padding-left:5px;'>No. of PAX</td>" +
        "<td width='25%' align='left' valign='top'  style='color:#FFF; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;background:#2e4b6b;padding-bottom:10px; padding-top:10px;padding-left:5px;'>Total</td>" +
       
        "</tr>";
        
        foreach (DataRow drcost in dtPrice.Rows)
        {


            var PaxType = string.Empty;
            switch (Convert.ToString(drcost["ChargesFor"])) {
                case "ADT":
                    PaxType="Adult";
                    break;
                case "CHD":
                case "CNN":
                    PaxType = "Child";
                    break;
                case "INF":
                    PaxType = "Infant";
                    break;
                default:
                    PaxType = "Adult";
                    break;
            }
            var total = Convert.ToInt32(drcost["NoOfPax"]) * Convert.ToInt32(drcost["SellPrice"]);
            inv += "<tr>" +
            "<td width='25%' align='left' valign='top' style='padding-bottom:10px; padding-top:10px;padding-left:5px;border-bottom:1px solid #333;font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif; padding-bottom:10px;'>" + PaxType + "</td>" +
            "<td width='25%'  align='left' valign='top' style='padding-bottom:10px; padding-top:10px;padding-left:5px;border-bottom:1px solid #333;font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif; padding-bottom:10px;'>  " + Convert.ToDouble(drcost["SellPrice"]).ToString("f2") + " " + dtComp.Rows[0]["CurrencyType"] + "</td>" +
            "<td width='25%'  align='left' valign='top' style='padding-bottom:10px; padding-top:10px;padding-left:5px;border-bottom:1px solid #333;font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif; padding-bottom:10px;'>  " + drcost["NoOfPax"] + "</td>" +
            "<td width='25%' align='left' valign='top' style='padding-bottom:10px; padding-top:10px;padding-left:5px;border-bottom:1px solid #333;font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif; padding-bottom:10px;'> " + total + " "+dtComp.Rows[0]["CurrencyType"] +"</td>" +
            
            "</tr>";
        }
        inv += "</table>" +
        "</td>" +
        "</tr>" +
         "<tr>" +
        "<td align='left' valign='top' style='color:green; padding:10px 0px; font-size:14px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>Total " + SellCost + " " + dtComp.Rows[0]["CurrencyType"] +"</td>" +
        "</tr>" +

       
        "</table></td>" +
        "</tr>" +
        "<tr>" +
        "<td align='left' valign='top'><table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
        "<tr>" +
        "<td width='70%' align='left' valign='top'><table width='100%' border='0' cellspacing='0' cellpadding='0'>" +

        "<tr>" +
        "<td align='left' valign='top'>&nbsp;</td>" +
        "</tr>" +
        "<tr>" +
        "<td align='left' valign='top'></td>" +
        "</tr>" +

            IMPORTANT_NOTICE_Text(Convert.ToString(dtComp.Rows[0]["BookingByCompany"]));


        #endregion

        Invoice1 = inv;
        return inv;

    }
    public string GetAirlineData(string prefix)
    {

        string result = "";

        DataTable dt = new DataTable();
        SqlConnection sqlcon = DataConnection.GetConnection();

        try
        {
            sqlcon.Open();
            SqlCommand cmd = new SqlCommand();
            StringBuilder strcmd = new StringBuilder("SELECT * from  Airline_Detail WHERE Airline_Code='" + prefix + "'");

            cmd.Connection = sqlcon;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = strcmd.ToString();
            SqlDataAdapter adp = new SqlDataAdapter(cmd);

            adp.Fill(dt);

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {

                    foreach (DataRow dr in dt.Rows)
                        result = dr["Airline_Name"].ToString();

                }
            }

        }
        catch (Exception Ex)
        {

        }
        finally
        {
            sqlcon.Close();
            //checkUseStatus();
        }





        return result;


    }
    public string SET_TIME_AM_PM(string timeFormat)
    {
        string strTime = string.Empty;
        string[] timeArr = timeFormat.Split(':');

        if (1==1)
        {
            int Hours = 0;
            int Minutes = 0;
            if (!string.IsNullOrEmpty(timeArr[0]))
            {
                Hours = Convert.ToInt32(timeArr[0]);
            }
            if (!string.IsNullOrEmpty(timeArr[1]))
            {
                Minutes = Convert.ToInt32(timeArr[1]);
            }

            TimeSpan ts = new TimeSpan(Hours, Minutes, 00);
            DateTime dtTemp = DateTime.ParseExact(ts.ToString(), "HH:mm:ss", CultureInfo.InvariantCulture);
            strTime = dtTemp.ToString("hh:mm tt");

        }
        else
        {
            strTime = timeFormat;
        }

        return strTime;
    }
    public string IMPORTANT_NOTICE_Text(string CompanyName)
    {
        string retResult = string.Empty;
       
            retResult = "<tr>" +
            "<td align='left' valign='top' style='color:#4863A0; padding:10px 0px; font-size:14px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>IMPORTANT NOTICE</td>" +
            "</tr>" +

            "<tr><td style='text-align:justify;font-size:12px;'>" +
        "Please find below the details of your flight and price we have agreed upon. We would like to inform you that we are in the process of issuing your ticket. Hence, you are requested to kindly go through your flight itinerary including names, flight connection and airlines & bring into our attention straightaway in case of any discrepancy as we might not be able to change it after the issuance of the ticket. Once you verify all the information including passenger(s) name, flight connection, airlines, number of passengers, price and our Terms and Condition etc., we would request you to kindly send us an email with your acknowledgement to proceed further with the booking." +
        "</td></tr>"
        +
          "<tr><td style='text-align:justify;font-size:12px;color:green'><br>" +
        "Acknowledgement on this e-mail implies that you have read and accepted the terms & conditions associated with the booking. Hence, you must crosscheck the complete details before confirming the reservation." +
        "</td></tr>"
        +
         "<tr><td style='text-align:justify;font-size:12px;'><br>" +
        "I, "+CardHolder+", holder of a credit/debit card number *****" + CardNum + " expiring " + Expiry + " on do hereby authorize you to process the payment for " + SellCost + " " + dtComp.Rows[0]["CurrencyType"] + "  against the booking number " + "PNR/REF " + CT +
        "</td></tr>"
        +
         "<tr><td style='text-align:justify;font-size:12px;'><br>" +
        "Charged on Card (Billed By " + Airline_Supplier + AuthSupplier + ") ******" + CardNum + " " + dtComp.Rows[0]["CurrencyType"] +" "+ SellCost +
        "</td></tr>"
        +
         "<tr><td style='text-align:justify;font-size:12px;'><br>" +
        "Note:- You may receive multiple transactions however it will not exceed the total amount quoted."
        +
        "</td></tr>"
        +
         "<tr><td style='text-align:justify;font-size:12px;'><br>" +
        "I acknowledge the purchase of airline tickets against the PNR reference(as mentioned above) and the details described above.I understand the restrictions and / or penalties for my reservation." +
        "</td></tr>"
        +
         "<tr><td style='text-align:justify;font-size:12px;'><br>" +
        "*Fees include airline penalty and agency service charge(s). You must cancel your flight reservation at least 24 hours prior to your departure or else the ticket has no refund value. If you do not show up for your flight or if you are late for your flight and for that reason denied boarding, the ticket has no refund or change value. Non-Refundable tickets carry no credit under any circumstances even on medical grounds. We strongly recommend buying travel insurance." +
        "</td></tr>"
        +
        "<tr>" +
            "<td align='left' valign='top' style='color:#4863A0; padding:10px 0px; font-size:14px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>General information:</td>" +
            "</tr>" +
              "<tr><td style='text-align:justify;font-size:12px;'>" +
        "By purchasing a travel ticket from us, the client understands that we are a travel agency and is responsible for providing the client with a valid travel ticket. Purchasing a ticket from us does not extinguish the relationship between the client and the airline. The client is encouraged to contact us at any time but it is the client's responsibility to follow up with us or the airline." +
        "</td></tr>"
        +
         "<tr>" +
            "<td align='left' valign='top' style='color:#4863A0; padding:10px 0px; font-size:14px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>To deal with the following:</td>" +
            "</tr>" +
              "<tr><td style='text-align:justify;font-size:12px;'>" +
        "For re-confirming booking, updates on Flight Status, Date Changes or Weather Delay. We cannot be held liable for any Missed Flight or hardship suffered by the client unless the client can show proof of inaccurate information provided in writing by us. All tickets are non-refundable or refundable with a fee unless otherwise specified by us in writing.<br>It is the client's responsibility to obtain all necessary documents, paperwork and entry visas for their travel." +
        "</td></tr>"
        +


         "<tr>" +
            "<td align='left' valign='top' style='color:#4863A0; padding:10px 0px; font-size:14px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>Terms and Conditions:</td>" +
            "</tr>" +
              "<tr><td style='text-align:justify;font-size:12px;'>" +
        "<p>Please Get Your Coronavirus Test Report Before Going To The Airport.</p>" +
        "<p>Once purchased, the tickets are considered non-refundable and non-transferable including service fee.</p>" +
        "<p>Ticket cost doesn’t not include Baggage/Carry-On fees or other fees charged directly by the airlines at the airport.</p>" +
        "<p>Name changes are not permitted.</p>" +
        "<p>All changes are subject to availability, additional fees may apply as per the airline's rules and regulations.</p>" +
        "<p>All travelers must confirm that the travel documents required for travel are current and valid (Destination Specific).</p>" +
         "<p>Your credit/debit card may be billed in multiple charges totaling the above amount. Also, charges reflecting in your credit/debit card statements may be in the name of the airline, any one of their service providers or one of our consolidators.</p>" +
        "<p>Airfares may change, until the purchase procedures are fulfilled and the tickets are not issued.</p>" +
         "<p>If you fail to board your departing flight, the airline will automatically cancel your return flight, unless you booked a roundtrip flight with 2 separate one-way fares.</p>" +
        "<p>Airlines may cancel or reschedule the flight without prior notice. You are recommended to call us at least 24 Hrs. before departure to ensure hassle free travel.</p>" +
        "</td></tr>"
        +
            "<tr><td style='text-align:justify;font-size:12px;color:green'><br>" +
        "Please note that before your departure, please recheck with Airlines for any Covid restrictions or any other formalities to be fulfilled.We as company will not be responsible if airlines deny boarding due to these reasons." +
        "</td></tr>"
        +
         "<tr>" +
            "<td align='left' valign='top' style='color:#4863A0; padding:10px 0px; font-size:14px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>For US bookings, even if your ticket is nonrefundable:</td>" +
            "</tr>" +
             "<tr><td style='text-align:justify;font-size:12px;'>" +
        "<p>Within same day midnight you may cancel your booking, 'subject to our cancellation fees'</p>" +
         "<p>All Airline Basic Economy tickets and Promotion tickets cancellation are not permitted Non-refundable tickets.</p>" +
          "<p>All reservations are also non-changeable and non-transferable unless otherwise stated. If you need to make a change to your reservation and that change is allowed, please be aware that such change is subject to a fee of $150.00 per passenger for domestic flights, $200.00 for trans - border flights and $300.00 for all other flights.There may also be fees or differences in price charged by any third - party suppliers(e.g., airlines, hotels, cruise lines, etc.) included in your reservation.</p>" +
           "<p>Please be aware that once you have made a reservation, name changes are not allowed. If you find you need to change or correct the spelling of a name after you’ve made a reservation, you will have to cancel your original reservation—if allowed—and then make a new reservation with a new flight at the then-current rate using the correct spelling of the name. This will likely incur fees and penalties. Therefore, it is imperative—and your responsibility— to verify the spelling of the names of all passengers before making your reservation.</p>" +
          "<p>The rate applied on the date of issuance of the ticket is only valid for a ticket fully utilized and in the sequential order of flight segments on the dates indicated. Improper use may void the ticket and result in cancellation of the entire trip.</p>" +
           "<p>Pricing is displayed in US currency.</p>" +
          
        "</td></tr>"
        +
         "<tr>" +
            "<td align='left' valign='top' style='color:#4863A0; padding:10px 0px; font-size:14px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>PAYMENT AND FLIGHT INFORMATION AND CONFIRMATION:</td>" +
            "</tr>" +

              "<tr><td style='text-align:justify;font-size:12px;'>" +
        "<p>Some banks and credit card companies charge a fee for international transactions. They will appear on your credit or bank card statement as a foreign or international transaction fee. For example, if you make a travel reservation through our website from outside the United States using a U.S. credit card, your bank may convert the payment amount to your local currency and may charge you a fee for the conversion. The amount of the charge appearing on your credit or bank card statement may be in your local currency and different than the purchase amount shown on the billing summary page for the reservation.</p>" +
        "<p>In addition, a foreign transaction fee may be assessed if the bank that issued your credit card is located outside the United States.</p>" +
        "<p>Booking international travel through our website may be considered an international transaction by the bank or credit card company since company may pass your payment on to an international travel supplier.</p>" +
        "<p>Your bank or credit card company determines the currency exchange rate and the amount of the foreign transaction fee on the day it processes the transaction. Please contact your bank or credit card company should you have any questions about these fees or the exchange rate applied to your transaction.</p>" +
        "<p>Booking notification: Once your purchase is complete, you should receive an email titled “Booking Notification.” Your booking may provide you with a confirmation number before a ticket has been issued. If this is the case, the booking process is not complete and the fare is subject to change until a ticket is issued.</p>" +
        "<p>Once your ticket has been issued, you should receive your electronic ticket.</p>" +
        "<p>We strongly recommend that you re-confirm your flight reservation with the airline 24 hours prior to departure for domestic flights, and 72 hours prior to departure for international flights.</p>" +
        

        "</td></tr>"
        +
         "<tr>" +
            "<td align='left' valign='top' style='color:green; padding:10px 0px; font-size:14px; font-family:Tahoma, Arial, Helvetica, sans-serif;'><br><br>Signature</td>" +
            "</tr>" +
             "<tr>" +
            "<td align='left' valign='top' style='color:#000000; padding:10px 0px; font-size:14px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>"+CardHolder+"</td>" +
            "</tr>" +
            "</table>" +
            "</td>";
        
        return retResult;
    }

    private string BindDocusignInfo()
    {
        string terms = @"<tr><td align='left' valign='top' style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'><br>Total Flight Booking Amount: " + Convert.ToString(dtComp.Rows[0]["CurrencyType"]) + " " + Convert.ToString(dtBD.Rows[0]["TotAmt"]) + " will be charged in two separate transactions; however total amount will be same as quoted.</td></tr></table>" +
             "<tr><td align='left' valign='top' style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'><br>I acknowledge receipt of this email for related charges described herein and fully understand and agree to the itinerary and " +
 "restrictions on the tickets, I have checked and verified the itinerary, including all names, flight details, dates and times.</td></tr>" +
 "<tr><td align='left' valign='top' style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'><br>I " + Convert.ToString(dtPax.Rows[0]["Title"]) + " " + Convert.ToString(dtPax.Rows[0]["FName"]) + " " + Convert.ToString(dtPax.Rows[0]["LName"]) + " hereby authorize Travel Service Pad / Air Tickets / ARC and associated suppliers to charge a total amount of " +
 Convert.ToString(dtComp.Rows[0]["CurrencyType"]) + " " + Convert.ToString(dtBD.Rows[0]["TotAmt"]) + " from my card.</td></tr>";
        return terms;
    }

    private void generate_Invoice_Pdf(string html, string xp)
    {

        // create the HTML to PDF converter
        HtmlToPdf htmlToPdfConverter = new HtmlToPdf();


        //// set browser width
        htmlToPdfConverter.BrowserWidth = int.Parse("793");

        //// set browser height if specified, otherwise use the default
        //if (textBoxBrowserHeight.Text.Length > 0)
        //    htmlToPdfConverter.BrowserHeight = int.Parse(textBoxBrowserHeight.Text);

        //// set HTML Load timeout
        //htmlToPdfConverter.HtmlLoadedTimeout = int.Parse(textBoxLoadHtmlTimeout.Text);

        // set PDF page size and orientation
        htmlToPdfConverter.Document.FitPageWidth = false;
        htmlToPdfConverter.Document.PageSize = GetSelectedPageSize();
        htmlToPdfConverter.Document.PageOrientation = PdfPageOrientation.Portrait;

        // set the PDF standard used by the document
        //htmlToPdfConverter.Document. =  PdfStandard.PdfA;

        // set PDF page margins
        htmlToPdfConverter.Document.Margins = new PdfMargins(5);


        // set a wait time before starting the conversion
        htmlToPdfConverter.WaitBeforeConvert = 2;

        // convert HTML to PDF
        byte[] pdfBuffer = null;


        // convert HTML code
        string htmlCode = html;
        string baseUrl = "";

        // convert HTML code to a PDF memory buffer
        pdfBuffer = htmlToPdfConverter.ConvertHtmlToMemory(htmlCode, baseUrl);


        // inform the browser about the binary data format
        HttpContext.Current.Response.AddHeader("Content-Type", "application/pdf");

        // let the browser know how to open the PDF document, attachment or inline, and the file name
        HttpContext.Current.Response.AddHeader("Content-Disposition", String.Format("{0}; filename={2}.pdf; size={1}",
        "attachment", pdfBuffer.Length.ToString(), xp));

        // write the PDF buffer to HTTP response
        HttpContext.Current.Response.BinaryWrite(pdfBuffer);

        // call End() method of HTTP response to stop ASP.NET page processing
        HttpContext.Current.Response.End();

    }

    private PdfPageSize GetSelectedPageSize()
    {
        switch ("A3")
        {
            case "A0":
                return PdfPageSize.A0;
            case "A1":
                return PdfPageSize.A1;
            case "A10":
                return PdfPageSize.A10;
            case "A2":
                return PdfPageSize.A2;
            case "A3":
                return PdfPageSize.A3;
            case "A4":
                return PdfPageSize.A4;
            case "A5":
                return PdfPageSize.A5;
            case "A6":
                return PdfPageSize.A6;
            case "A7":
                return PdfPageSize.A7;
            case "A8":
                return PdfPageSize.A8;
            case "A9":
                return PdfPageSize.A9;
            case "ArchA":
                return PdfPageSize.ArchA;
            case "ArchB":
                return PdfPageSize.ArchB;
            case "ArchC":
                return PdfPageSize.ArchC;
            case "ArchD":
                return PdfPageSize.ArchD;
            case "ArchE":
                return PdfPageSize.ArchE;
            case "B0":
                return PdfPageSize.B0;
            case "B1":
                return PdfPageSize.B1;
            case "B2":
                return PdfPageSize.B2;
            case "B3":
                return PdfPageSize.B3;
            case "B4":
                return PdfPageSize.B4;
            case "B5":
                return PdfPageSize.B5;
            case "Flsa":
                return PdfPageSize.Flsa;
            case "HalfLetter":
                return PdfPageSize.HalfLetter;
            case "Ledger":
                return PdfPageSize.Ledger;
            case "Legal":
                return PdfPageSize.Legal;
            case "Letter":
                return PdfPageSize.Letter;
            case "Letter11x17":
                return PdfPageSize.Letter11x17;
            case "Note":
                return PdfPageSize.Note;
            default:
                return PdfPageSize.A4;
        }
    }
}