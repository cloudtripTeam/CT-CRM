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

public partial class Admin_reciept : System.Web.UI.Page
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
    public static string Invoice1 { get; set; }
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

                if (!objUserDetail.isAuth("Invoice"))
                {
                    Response.Redirect("~/Admin/AccessDenied.aspx");
                    return;
                }

                if (!string.IsNullOrEmpty(Request.QueryString.Get("BID")))
                {

                    string xp = Request.QueryString.Get("BID");

                    BookingDetails(xp);
                    generate_Invoice_Pdf(generate_Invoice(xp), xp);
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


    }
    private string generate_Invoice(string xp)
    {
        //string heading = "Itinerary/e-ticket";

        string heading = "E-Ticket";


        CompanyDetails objc = lot.SetCompanyDetail(dtComp.Rows[0]["BookingByCompany"].ToString());
        #region




        string inv = @"<table width='1000' border='0' align='center' cellpadding='0' cellspacing='0' style='font-family:Tahoma, Arial, Helvetica, sans-serif;'>" +
        "<tr>" +
        "<td style='padding:30px;'><table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
        "<tr>" +
        "<td align='left' valign='top'><table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
        "<tr>" +
       
        "<td align='right' valign='top' style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>" + objc.Comp_Address + "<br />" +
       "" + "<k style='font-weight:bold;font-size:20px'>Tel: " + objc.Comp_contact + "</k><br/>" +
        "</5=_td></tr>" +





         
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

        "<table width='100%' border='0' cellspacing='0' cellpadding='0' ><tr>" +
        "<td width='50%' align='left' valign='top' style='padding-bottom:10px; padding-top:10px; font-size:13px; font-weight:600; font-family:Tahoma, Arial, Helvetica, sans-serif;'><strong>Booking Summary :</strong></td>" +
        "<td width='50%' align='right' valign='top' style='padding-bottom:10px; padding-top:10px; font-size:13px; font-weight:600; font-family:Tahoma, Arial, Helvetica, sans-serif;'><strong>Agent: " + objUserDetail.userID + "</strong></td>" +
        "</tr></table>" +

        "<table width='100%'  border='0' cellspacing='0' cellpadding='0'  style='background:#e3e3e3;border-bottom:#cbcbcb solid 1px;'><tr>" +
        "<td height='21' align='left' valign='top' style='color:#02c9e8; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;background:#2e4b6b;padding-bottom:10px; padding-top:10px;padding-left:5px;'><strong>PAX Name</strong></td>" +
        "<td height='21' align='left' valign='top' style='color:#02c9e8; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;background:#2e4b6b;padding-bottom:10px; padding-top:10px;padding-left:5px;'><strong>Booking Ref</strong></td>" +


        "<td align='left' valign='top' style='color:#02c9e8; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;background:#2e4b6b;padding-bottom:10px; padding-top:10px;padding-left:5px;'><strong>Booking Date</strong></td>" +
        "<td align='left' valign='top' style='color:#02c9e8; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;background:#2e4b6b;padding-bottom:10px; padding-top:10px;padding-left:5px;'><strong>PNR</strong></td>" +

        
        "</tr>" +
        "<tr>" +
        "<td align='left' valign='top' style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;padding-bottom:10px; padding-top:10px;padding-left:5px;'><strong>" + dtPax.Rows[0]["Title"] + " " + dtPax.Rows[0]["FName"] + " " + dtPax.Rows[0]["MName"] + " " + dtPax.Rows[0]["LName"] + "</strong></td>" +
        "<td align='left' valign='top' style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;padding-bottom:10px; padding-top:10px;padding-left:5px;'>" + xp + "</td>" +

        "<td align='left' valign='top' style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;padding-bottom:10px; padding-top:10px;padding-left:5px;'>" + Convert.ToDateTime(dtBD.Rows[0]["BookingDateTime"].ToString()).ToString("ddd, dd MMM yy") + "</td>" +
        "<td align='left' valign='top' style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;padding-bottom:10px; padding-top:10px;padding-left:5px;'>" + dtBD.Rows[0]["PNR"] + "</td>" +

        "<td align='left' valign='top' style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'></td>" +
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
        "<table width='100%' border='0' cellspacing='0' cellpadding='0' style='background:#e3e3e3;border-bottom:#cbcbcb solid 1px;'>" +
        "<tr>" +
        "<td width='25%' align='left' valign='top' style='color:#02c9e8; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;background:#2e4b6b;padding-bottom:10px; padding-top:10px;padding-left:5px;'>First Name</td>" +
        "<td width='25%'  align='left' valign='top' style='color:#02c9e8; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;background:#2e4b6b;padding-bottom:10px; padding-top:10px;padding-left:5px;'>Middle Name</td>" +
        "<td width='25%'  align='left' valign='top' style='color:#02c9e8; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;background:#2e4b6b;padding-bottom:10px; padding-top:10px;padding-left:5px;'>Last Name</td>" +
        "<td width='25%' align='left' valign='top' style='color:#02c9e8; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;background:#2e4b6b;padding-bottom:10px; padding-top:10px;padding-left:5px;'>Ticket No</td>" +
        "<td align='right' valign='top' style='border-bottom:#000 solid 2px;color:#02c9e8; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;background:#2e4b6b;padding-bottom:10px; padding-top:10px;padding-left:5px;'></td>" +
        "</tr>";
        foreach (DataRow drPax in dtPax.Rows)
        {
            inv += "<tr>" +
            "<td width='25%' align='left' valign='top' style='padding-bottom:10px; padding-top:10px;padding-left:5px;font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif; padding-bottom:10px;'>" + drPax["Title"] + " " + drPax["FName"] + "</td>" +
            "<td width='25%'  align='left' valign='top' style='padding-bottom:10px; padding-top:10px;padding-left:5px;font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif; padding-bottom:10px;'>  " + drPax["MName"] + "</td>" +
            "<td width='25%'  align='left' valign='top' style='padding-bottom:10px; padding-top:10px;padding-left:5px;font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif; padding-bottom:10px;'>  " + drPax["LName"] + "</td>" +
            "<td width='25%' align='left' valign='top' style='padding-bottom:10px; padding-top:10px;padding-left:5px;font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif; padding-bottom:10px;'> " + drPax["Tickets"] + "</td>" +
            "<td align='right' valign='top' style='padding-bottom:10px; padding-top:10px;padding-left:5px;font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif; padding-bottom:10px;'></td>" +
            "</tr>";
        }
        inv += "</table>" +
        "</td>" +
        "</tr>" +
         "<tr>" +
        "<td align='left' valign='top'><table width='100%' border='0' cellspacing='0' cellpadding='0'>&nbsp;" +

        "</td>" +
        "</tr>" +
        "<tr>" +
        "<td align='left' valign='top' style='padding-bottom:10px; padding-top:10px; font-size:13px; font-weight:600; font-family:Tahoma, Arial, Helvetica, sans-serif;'><strong>Flight Details :</strong></td>" +
        "</tr>" +


        "<tr>" +
        "<td align='left' valign='top'>" +


"<table width='100%' border='0' cellspacing='0' cellpadding='0' style='margin-bottom:10px; border-bottom:#cbcbcb solid 1px; padding-bottom:10px;background:#e3e3e3'>" +
        "<tr>" +
       "<td width='80' align='left' valign='top' style='color:#02c9e8; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;padding-bottom:10px; padding-top:10px;padding-left:5px;background:#2e4b6b;'>Airline</td>" +
        "<td width='180' align='left' valign='top' style='color:#02c9e8; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;padding-bottom:10px; padding-top:10px;padding-left:5px;background:#2e4b6b;'>From</td>" +
        "<td width='180' align='left' valign='top' style='color:#02c9e8; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;padding-bottom:10px; padding-top:10px;background:#2e4b6b;padding-left:5px;'>To</td>" +
        "<td width='100' align='left' valign='top' style='color:#02c9e8; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;padding-bottom:10px; padding-top:10px;background:#2e4b6b;padding-left:5px;'>Depart</td>" +
        "<td width='100' align='left' valign='top' style='color:#02c9e8; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;padding-bottom:10px; padding-top:10px;background:#2e4b6b;padding-left:5px;'>Arrival</td>" +

        "<td align='left' valign='top' style='color:#02c9e8; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;padding-bottom:10px; padding-top:10px;background:#2e4b6b;padding-left:5px;'>Cabin</td>" +
        "<td align='left' valign='top' style='color:#02c9e8; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;padding-bottom:10px; padding-top:10px;background:#2e4b6b;padding-left:5px;'>Baggage</td>" +
        "<td align='left' valign='top' style='color:#02c9e8; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;padding-bottom:10px; padding-top:10px;background:#2e4b6b;padding-left:5px;'>Airline Ref.</td>" +


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
        "<td align='left' valign='top'>&nbsp;</td>" +
        "</tr>" +
        "</table></td>" +
        "</tr>" +
        "<tr>" +
        "<td align='left' valign='top'><table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
        "<tr>" +
        "<td width='70%' align='left' valign='top'><table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
        "<tr>" +
        "<td>&nbsp;</td>" +
        "</tr>" +
        "<tr>" +
        "<td>&nbsp;</td>" +
        "</tr>" +
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

        if (Convert.ToString(dtComp.Rows[0]["BookingByCompany"]) == "TRVJUNCTION_USA" || Convert.ToString(dtComp.Rows[0]["BookingByCompany"]) == "C2BUS")
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
        if (!string.IsNullOrWhiteSpace(CompanyName) && (CompanyName == "TRVJUNCTION_USA" || CompanyName == "C2BUS"))
        {
            retResult = "<tr>" +
            "<td align='left' valign='top' style='color:#F00; padding:10px 0px; font-size:14px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>IMPORTANT NOTICE</td>" +
            "</tr>" +
            "<tr>" +
            "<td align='left' valign='top' style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>This is to inform you that your e-ticket has been issued." +
            "<br/><br/> You have to take a print out of e-ticket while going to airport." +
            "<br/><br/> Due to security measures, we recommend you to reach airport at least 5 hours prior to your departure time because in the case of a no show the tickets are totally non changeable and nonrefundable." +
            "<br/><br/> Please reconfirm your flights before 72 hours prior to your departure time to avoid any inconvenience." +
            "<br/><br/> Positive Identification required at Check-In (Passport) for E-Tickets." +
            "<br/><br/> The Electronic Ticket Record will not reflect flight reservation changes, schedule changes, or cancellations made after the electronic ticket was issued." +
            "<br/><br/> The Electronic Ticket Record may not meet all the requirements for receipts worldwide." +
            "<br/><br/> Terms/Conditions of Travel and Carrier Liability Notices can be requested from The Travel Agency or the Transporting Carrier." +
            "</td> " +
            "</tr>" +
            "</table>" +
            "</td>";
        }
        else
        {
            retResult = "<tr>" +
            "<td align='left' valign='top' style='color:#F00; padding:10px 0px; font-size:14px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>IMPORTANT NOTICE</td>" +
            "</tr>" +
            "<tr>" +
            "<td align='left' valign='top' style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>Since January 2009 international travellers visiting/transiting United Sates under the Visa Waiver Program" +
            " need to apply for authorisation through ESTA (Electronic System for Travel Authorisation). EASTA authorisation must be obtained at least 72 hours " +
            "prior to departure by visiting https://esta.cbp.dhs.gov/esta/ There is also a $14(USD) charge for each application." +
            "<br/><br/> PLEASE REPORT ATLEAST 3 HOURS PRIOR TO DEPARTURE FOR CHECK-IN" +
            "<br/> PLEASE RECHECK YOUR PASSPORT/VISA REQUIREMENTS" +
            "<br/> PLEASE RECONFIRM RETURN FLIGHT 3 DAYS BEFORE DEPARTURE" +
            "</td> " +
            "</tr>" +
            "</table>" +
            "</td>";
        }
        return retResult;
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