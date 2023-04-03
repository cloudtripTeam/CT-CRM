using mailServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Default2 : System.Web.UI.Page
{
    private static Binding binding = (Binding)new BasicHttpBinding();
    private static EndpointAddress endpointAddress = new EndpointAddress("http://dataservice.cloudtrip.us/DataService.asmx");
    private DataServiceSoapClient client = new DataServiceSoapClient(binding, endpointAddress);
    public string emailContent { get; set; }
    public string hfBookingID { get; set; }
    public string hfProdID { get; set; }
    public string trnsid { get; set; }
    UserDetail objUserDetail;
    TransactionResult Transaction = new TransactionResult();
    Itinerary.FlightDetails iti = new Itinerary.FlightDetails("", "", false, false, false, false, false, false, false, false, false);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserDetails"] != null)
        {
            objUserDetail = Session["UserDetails"] as UserDetail;
            if (!IsPostBack)
            {
                if (!objUserDetail.isAuth("amend Booking"))
                {
                    Response.Redirect("Default.aspx");
                    return;
                }
               
            }
            if (Request.QueryString["BID"] != null && Request.QueryString["PID"] != null && Request.QueryString["TRS"] != null)
            {
                lblBookingID.Text=hfBookingID = Request.QueryString["BID"].ToString();
                hfProdID = Request.QueryString["PID"].ToString();
                trnsid = Request.QueryString["TRS"].ToString();

                iti = new Itinerary.FlightDetails(hfBookingID, hfProdID, true, true, true, true, true, true, true, true, true);
            }
        }
        if(!Page.IsPostBack)
        {
            BindDetail();
            if (iti.AD.Count() > 0)
            {
                rptrAuthdoc.DataSource = iti.AD;
                rptrAuthdoc.DataBind();
            }
        }
       
    }
    protected void SendBtn_Click(object sender, EventArgs e)
    {
        BindDetail();

        var pathlist = "";
        foreach(var pth in iti.AD)
        {
            var rt_path_new = pth.DocPath.ToString().Replace("../../", "c:/Websites/CRM/");
            pathlist += rt_path_new + "#";
        }
        pathlist = pathlist.TrimEnd('#');
        try
        {
            //client.Sendcustomermail("sales@cloudtrip.us", "sales@cloudtrip.us", "Kindly charge " + iti.TM.First().TrnsAmount + " " + iti.BM.CurrencyType + " / " + Booking_Type.Value + " / " + Airline.Value, emailContent, "hrishi@cloudtrip.us", "info@cloudtrip.us");
            client.SendCustomerMultipleAttachment("sales@cloudtrip.us", "hrishi@cloudtrip.us", "Kindly charge " + iti.TM.First().TrnsAmount + " " + iti.BM.CurrencyType + " / " + Booking_Type.Value + " / " + Airline.Value, emailContent, "hrishi@cloudtrip.us", "eliza@cloudtrip.us", pathlist);
            lblStatus.Text = "Sent Successfully";
        }
        catch (Exception ex)
        {
        }

    }

    private string BindDetail()
    {
        string url = HttpContext.Current.Request.Url.AbsoluteUri;
        emailContent = "";
        emailContent += " <table><thead><tr><td style='text-align:left; '> Hi Team,</td></tr>";
        emailContent += "<tr><td style='text-align:left; '>Kindly charge "+iti.TM.First().TrnsAmount + " "+iti.BM.CurrencyType+ " / <span id='subjectline'></span>"+ Booking_Type.Value + " / <span id='airlineName'></span>" + Airline.Value + "</td></tr>";
        emailContent +="<tr><th>&nbsp;</th></tr></thead></table><table style='border-spacing: unset;border: 1px solid;'>";
        emailContent += "<tbody> <tr><th style='width: 200px;text-align:left;border-bottom: 1px solid #000;border-right: 1px solid #000;padding:5px;'>Agent</th><td style='min-width: 500px;border-bottom: 1px solid #000; text-align: center; padding: 5px;'>Cloudtrip</td></tr>";
        emailContent += "<tr><th style='text-align:left;border-bottom: 1px solid #000;border-right: 1px solid #000;padding:5px;'>Mco</th><th style='border-bottom: 1px solid #000;text-align:center;padding:5px;'>"+ iti.TM.First().TrnsAmount + " " + iti.BM.CurrencyType +  " </th></tr>";
        emailContent += "<tr><th style='text-align:left;border-bottom: 1px solid #000;border-right: 1px solid #000;padding:5px;'>Pnr</th><td style='border-bottom: 1px solid #000;text-align:center;padding:5px;'>"+iti.BD.BookingID+"</td></tr>";
        emailContent += "<tr><th style='text-align:left;border-bottom: 1px solid #000;border-right: 1px solid #000;padding:5px;'>Card No</th><td style='border-bottom: 1px solid #000; text-align: center; padding: 5px; '>"+ iti.TM.First().TrnsCard_No+ "</td></tr>";
        emailContent += "<tr><th style='text-align:left;border-bottom: 1px solid #000;border-right: 1px solid #000;padding:5px;'>EXP</th><td style='border-bottom: 1px solid #000; text-align: center; padding: 5px;'>" + iti.TM.First().TrnsCardExp + "</td></tr>";
        emailContent += "<tr><th style='text-align:left;border-bottom: 1px solid #000;border-right: 1px solid #000;padding:5px;'>CVV</th><td style='border-bottom: 1px solid #000; text-align: center; padding: 5px;'>" + iti.TM.First().TrnsCardCVV + "</td></tr>";
        emailContent += "<tr><th style='text-align:left;border-bottom: 1px solid #000;border-right: 1px solid #000;padding:5px;'>CCH</th><td style='border-bottom: 1px solid #000; text-align: center; padding: 5px;'>" + iti.TM.First().TrnsHolder_Name + "</td></tr>";
        emailContent += "<tr><th style='text-align:left;border-bottom: 1px solid #000;border-right: 1px solid #000;padding:5px;'>Mobile</th><td style='border-bottom: 1px solid #000; text-align: center; padding: 5px;'>" + iti.CD.MobileNo + "</td></tr>";
        emailContent += "<tr><th style = 'text-align:left;border-bottom: 1px solid #000;border-right: 1px solid #000;padding:5px;'> Address </th><td style = 'border-bottom: 1px solid #000; text-align: center; padding: 5px;'>" + iti.TM.First().TrnsCard_Address + "</td></tr>";
        emailContent += "<tr><th style = 'text-align:left;border-bottom: 1px solid #000;border-right: 1px solid #000;padding:5px;'> Remarks </th><td style = 'border-bottom: 1px solid #000; text-align: center; padding: 5px;' > <span id='remarkstext'>"+Remarks.Value+"</span> </td></tr>";
        emailContent += "<tr><th style = 'text-align:left;border-bottom: 1px solid #000;border-right: 1px solid #000;padding:5px;'> Pax DOB </th><td style = 'border-bottom: 1px solid #000; text-align: center; padding: 5px;'>"+iti.PD.First().DOB+"</td></tr>";
        emailContent += "<tr><th style='text-align:left;border-bottom: 1px solid #000;border-right: 1px solid #000;padding:5px;'>Date of Dep</th><td style='border-bottom: 1px solid #000; text-align: center; padding: 5px; '>" + iti.SD.First().FromDateTime + "</td></tr>";
        emailContent += "<tr><th style='text-align:left;border-bottom: 1px solid #000;border-right: 1px solid #000;padding:5px;'>Date of Return</th><td style='border-bottom: 1px solid #000; text-align: center; padding: 5px;'></td></tr>";
        emailContent += "<tr><th style='text-align:left;border-bottom: 1px solid #000;border-right: 1px solid #000;padding:5px;'>Email</th><td style='border-bottom: 1px solid #000; text-align: center; padding: 5px; '>" + iti.CD.EmailID + "</td></tr>";
        emailContent += "<tr><th style='text-align:left;border-right: 1px solid #000;padding:5px;'>Airline</th><td style=' text-align: center; padding: 5px; '>" + iti.SM.ValCarrier + "</td></tr>";
        emailContent += "</tbody>";
        emailContent += "</table>";
       
        return emailContent;


    }
}