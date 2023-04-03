using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HiQPdf;
using System.Net.Mail;
using System.Text;

public partial class Admin_sendMailReminder : System.Web.UI.Page
{
    DataTable dtPax = new DataTable(); 
    DataTable dtComp = new DataTable();
    DataTable dtContact = new DataTable();
    Layout lot = new Layout();
    System.Net.Mail.AttachmentCollection attachs;
    FandHServices.FandHServicesClient obj = new FandHServices.FandHServicesClient();
    MailMessage msg = new MailMessage();
    public eTicketLayout etktLayout = new eTicketLayout();
    UserDetail objUserDetail;
    public string terms { get; set; }
    public string PaxName { get; set; }
    

    protected void Page_Load(object sender, EventArgs e)
    {
        objUserDetail = new UserDetail();
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
                    hidBookingID.Value = xp;
                    BookingDetails(xp);


                    if (dtComp != null && dtComp.Rows.Count != 0)
                    {
                        //if (dtComp.Rows[0]["InvoiceNo"].ToString() !="")
                        //{
                        // ckboxInvoice.Checked = true;
                        //ckboxInvoice.Enabled = true;
                        //}
                        //else {
                        //    ckboxInvoice.Checked = false;
                        //    ckboxInvoice.Enabled = false;
                        //}
                    }
                    //bindEmailIds(dtComp, dtContact);
                    //getMailAlert();
                    Alerts(Request.QueryString["T"]);
                }

            }
        }
        else
        {
            Response.Redirect("~/Login.aspx", false);
        }
    }
    private void Alerts(string T)
    {
        switch (T)
        {
            case "0":
                getMailAlert();
                break;
            case "1":
                getMailAltert1();
                break;
            case "2":
                getMailAltert2();
                break;
            case "3":
                getMailAltert3();
                break;
            case "4":
                getMailAltert4();
                break;
            default:
                getMailAlert();
                break;
        }
    }
    public void BookingDetails(string XP)
    {
        dtPax = obj.GET_Passenger_Detail(XP, "001");
        dtComp = obj.GET_Booking_Master(XP);
        dtContact = obj.GET_Contact_Detail(XP, "001");
        hfCompany.Value = dtComp.Rows[0]["BookingByCompany"].ToString();
        txtXPTo.Text = Convert.ToString(dtContact.Rows[0]["EmailID"]);
        if (dtPax != null)
        {
            if (dtPax.Rows.Count > 0)
            {
                PaxName = dtPax.Rows[0]["Title"] + " " + dtPax.Rows[0]["LName"];
            }
            else
                PaxName = "Traveller";
        }
        else
        {
            PaxName = "Traveller";
        }



    }

    private void getMailAlert()
    {
        terms = @"<table   width='600' border='0' align='center' cellpadding='0' cellspacing='0' style='font-family: Arial, Helvetica, sans-serif;'>" +
                                               "<tr>" +
                                                   "<td>" +
                                                        "<table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
                                                           "<tr>" +
                                                                "<td align='left' valign='top' style='background: #73a5d8; padding: 30px 10px;'>" +
                                                                     etktLayout.Eticket_HeaderLayout(hfCompany.Value) +
                                                               "</td>" +
                                                            "</tr>" +
                                                           "<tr>" +
                                                                "<td align='left' valign='top' style='font-family: Arial, Helvetica, sans-serif; font-size: 12px; color: #000;'>" +

                                                                 "<table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
                                                                                                "<tr>" +
                                                                                                    "<td align='left' valign='top' style='padding:30px 0px; font-size:15px;'>Dear <strong style='color:#d40046;'> " + PaxName + "</strong> </td>" +
                                                                                                "</tr>" +
                                                                                                "<tr>" +
                                                                                                    "<td align='center' valign='top' style='padding-bottom:50px; font-weight:bold; font-size:22px; color:#162849;'>" +
                                                                                                        
                                                                                                        "<img src='" + BLL.WebsiteStaticData.WebsiteUrl + "images/section-shape.png' width='97' height='20' />" +
                                                                                                    "</td>" +
                                                                                                "</tr>" +
                                                                                                "<tr>" +
                                                                                                    "<td align='left' valign='top' style='font-size:13px; text-align:justify; font-family:Arial, Helvetica, sans-serif;'>Thank you for choosing " + etktLayout.BrandName + ".</td>" +
                                                                                                "</tr>" +
                                                                                                "<tr>" +
                                                                                                    "<td>&nbsp;</td>" +
                                                                                                "</tr>" +
                                                                                                "<tr>" +
                                                                                                    "<td>Due to high incidence of online fraud and to protect from any unauthorized activity on your card, made by unauthorized users, we sincerely request the card holder to call us from your registered billing phone number which is on file with your financial institution or from your personal phone number.</td>" +
                                                                                                "</tr>" +
                                                                                                 "<tr>" +
                                                                                                    "<td>&nbsp;</td>" +
                                                                                                "</tr>" +
                                                                                                "<tr>" +
                                                                                                    "<td>Kindly do not purchase this ticket elsewhere as we may have issued the ticket(s). Your e-ticket will be sent as soon as we receive a call from the card holder. Your prompt response will be appreciated. So please call us immediately at " + etktLayout.phone + ". The selected price may change in case your response is delayed.</td>" +
                                                                                                "</tr>" +
                                                                                                 "<tr>" +
                                                                                                    "<td>&nbsp;</td>" +
                                                                                                "</tr>" +
                                                                                                "<tr>" +
                                                                                                    "<td>Kindly call us immediately at "+etktLayout.phone+" your delayed response may result in change of fare and availability. </td>" +
                                                                                                "</tr>" +
                                                                                                 "<tr>" +
                                                                                                    "<td>&nbsp;</td>" +
                                                                                                "</tr>" +
                                                                                                "<tr>" +
                                                                                                    "<td>NB: We may have already charged your card, so please do not purchase this ticket elsewhere.</td>" +
                                                                                                "</tr>" +
                                                                                                 "<tr>" +
                                                                                                    "<td>&nbsp;</td>" +
                                                                                                "</tr>" +
                                                                                                "<tr>" +
                                                                                                    "<td style='padding:30px 0px;'>" +
                                                                                                        "Kind Regards<br />" +
                                                                                                        "<strong>" + etktLayout.BrandName + " </strong>" +
                                                                                                    "</td>" +
                                                                                                "</tr>" +
                                                                                            "</table>" +

                                                               "</td>" +
                                                            "</tr>" +
                                                           "<tr>" +
                                                                "<td align='center' valign='top' style='background: #1c2d4c; padding: 10px; color: #FFF; font-size: 11px; font-family: Arial, Helvetica, sans-serif;'>" +
                                                                    etktLayout.Eticket_Footer +
                                                               "</td>" +
                                                            "</tr>" +
                                                        "</table>" +
                                                   "</td>" +
                                                "</tr>" +
                                            "</table>";

    }

    private void getMailAltert4()
    {
        terms = @"<table   width='600' border='0' align='center' cellpadding='0' cellspacing='0' style='font-family: Arial, Helvetica, sans-serif;'>" +
        "<tr>" +
        "<td>" +
        "<table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
        "<tr>" +
        "<td align='left' valign='top' style='background: #73a5d8; padding: 30px 10px;'>" +
        etktLayout.Eticket_HeaderLayout(hfCompany.Value) +
        "</td>" +
        "</tr>" +
        "<tr>" +
        "<td align='left' valign='top' style='font-family: Arial, Helvetica, sans-serif; font-size: 12px; color: #000;'>" +
        "<table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
        "<tr>" +
        "<td align='left' valign='top' style='padding:30px 0px; font-size:15px;'>Dear <strong style='color:#d40046;'> " + PaxName + "</strong> </td>" +
        "</tr>" +
        "<tr>" +
        "<td align='center' valign='top' style='padding-bottom:50px; font-weight:bold; font-size:22px; color:#162849;'>" +
        "<img src='" + BLL.WebsiteStaticData.WebsiteUrl + "images/section-shape.png' width='97' height='20' />" +
        "</td>" +
        "</tr>" +
        "<tr>" +
        "<td align='left' valign='top' style='font-size:13px; text-align:justify; font-family:Arial, Helvetica, sans-serif;'>Thank you for choosing  " + etktLayout.BrandName + ".</td>" +
        "</tr>" +
        "<tr>" +
        "<td>&nbsp;</td>" +
        "</tr>" +
        "<tr>" +
        "<td>Your flight reservation with Reference Number " + Convert.ToString(Request.QueryString.Get("BID")) + "  is confirmed but the transition is on hold since we require verifying certain information with you.</td>" +
        "</tr>" +
        "<tr>" +
        "<td>&nbsp;</td>" +
        "</tr>" +
        "<tr>" +
        "<td>We are unable to contact you on your provide phone number(s).</td>" +
        "</tr>" +
        "<tr>" +
        "<td>&nbsp;</td>" +
        "</tr>" +
        "<tr>" +
        "<td>Kindly call us immediately at " + etktLayout.phone + "  or WhatsApp on +1437-533-3118.Your delayed response may result in change of fare and availability.</td>" +
        "</tr>" +
        "<tr>" +
        "<td>&nbsp;</td>" +
        "</tr>" +
        "<tr>" +
        "<td>NOTE: We may have already charged your card, so please do not purchase this ticket elsewhere. Should you require any further assistance, please feel free to write an email to  " + emailadd()+"</td>" +
        "</tr>" +
        "<tr>" +
        "<td>&nbsp;</td>" +
        "</tr>" +
        "<tr>" +
        "<td style='padding:30px 0px;'>" +
        "Kind Regards<br />" +
        "<strong>" + etktLayout.BrandName + " </strong>" +
        "</td>" +
        "</tr>" +
        "</table>" +
        "</td>" +
        "</tr>" +
        "<tr>" +
        "<td align='center' valign='top' style='background: #1c2d4c; padding: 10px; color: #FFF; font-size: 11px; font-family: Arial, Helvetica, sans-serif;'>" +
        etktLayout.Eticket_Footer +
        "</td>" +
        "</tr>" +
        "</table>" +
        "</td>" +
        "</tr>" +
        "</table>";
    }

    private void getMailAltert3()
    {
        terms = @"<table   width='600' border='0' align='center' cellpadding='0' cellspacing='0' style='font-family: Arial, Helvetica, sans-serif;'>" +
                                               "<tr>" +
                                                   "<td>" +
                                                        "<table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
                                                           "<tr>" +
                                                                "<td align='left' valign='top' style='background: #73a5d8; padding: 30px 10px;'>" +
                                                                     etktLayout.Eticket_HeaderLayout(hfCompany.Value) +
                                                               "</td>" +
                                                            "</tr>" +
                                                           "<tr>" +
                                                                "<td align='left' valign='top' style='font-family: Arial, Helvetica, sans-serif; font-size: 12px; color: #000;'>" +

                                                                 "<table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
                                                                                                "<tr>" +
                                                                                                    "<td align='left' valign='top' style='padding:30px 0px; font-size:15px;'>Dear <strong style='color:#d40046;'> " + PaxName + "</strong> </td>" +
                                                                                                "</tr>" +
                                                                                                "<tr>" +
                                                                                                    "<td align='center' valign='top' style='padding-bottom:50px; font-weight:bold; font-size:22px; color:#162849;'>" +

                                                                                                        "<img src='" + BLL.WebsiteStaticData.WebsiteUrl + "images/section-shape.png' width='97' height='20' />" +
                                                                                                    "</td>" +
                                                                                                "</tr>" +
                                                                                                "<tr>" +
                                                                                                    "<td align='left' valign='top' style='font-size:13px; text-align:justify; font-family:Arial, Helvetica, sans-serif;'>Greetings from  " + etktLayout.BrandName + ".</td>" +
                                                                                                "</tr>" +
                                                                                                "<tr>" +
                                                                                                    "<td>&nbsp;</td>" +
                                                                                                "</tr>" +
                                                                                                "<tr>" +
                                                                                                    "<td>Due to factors that affect Flight operation, we inform you that your flight with a reservation code 39FWXH has been modified. We apologize for the impact of this change on your travel plans.</td>" +
                                                                                                "</tr>" +
                                                                                                 "<tr>" +
                                                                                                    "<td>&nbsp;</td>" +
                                                                                                "</tr>" +
                                                                                                "<tr>" +
                                                                                                    "<td>We're unable to reach you, kindly give us a call to postponed your trip or cancel for future credit.</td>" +
                                                                                                "</tr>" +
                                                                                                 "<tr>" +
                                                                                                    "<td>&nbsp;</td>" +
                                                                                                "</tr>" +
                                                                                                "<tr>" +
                                                                                                    "<td>If you have additional questions please text us at " + etktLayout.phone + " ,Call " + etktLayout.phone + " or email " + etktLayout.Email + " for further information. Our phone lines are extremely busy right now, so that is the best way to get in touch with us to get answers as quickly as possible.</td>" +
                                                                                                "</tr>" +
                                                                                                 "<tr>" +
                                                                                                    "<td>&nbsp;</td>" +
                                                                                                "</tr>" +
                                                                                                "<tr>" +
                                                                                                    "<td>Again, thank you so much for your understanding during this difficult time.</td>" +
                                                                                                "</tr>" +
                                                                                                 "<tr>" +
                                                                                                    "<td>&nbsp;</td>" +
                                                                                                "</tr>" +
                                                                                                "<tr>" +
                                                                                                    "<td style='padding:30px 0px;'>" +
                                                                                                        "Kind Regards<br />" +
                                                                                                        "<strong>" + etktLayout.BrandName + " </strong>" +
                                                                                                    "</td>" +
                                                                                                "</tr>" +
                                                                                            "</table>" +

                                                               "</td>" +
                                                            "</tr>" +
                                                           "<tr>" +
                                                                "<td align='center' valign='top' style='background: #1c2d4c; padding: 10px; color: #FFF; font-size: 11px; font-family: Arial, Helvetica, sans-serif;'>" +
                                                                    etktLayout.Eticket_Footer +
                                                               "</td>" +
                                                            "</tr>" +
                                                        "</table>" +
                                                   "</td>" +
                                                "</tr>" +
                                            "</table>";

    }

    private void getMailAltert2()
    {
        terms = @"<table   width='600' border='0' align='center' cellpadding='0' cellspacing='0' style='font-family: Arial, Helvetica, sans-serif;'>" +
                                               "<tr>" +
                                                   "<td>" +
                                                        "<table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
                                                           "<tr>" +
                                                                "<td align='left' valign='top' style='background: #73a5d8; padding: 30px 10px;'>" +
                                                                     etktLayout.Eticket_HeaderLayout(hfCompany.Value) +
                                                               "</td>" +
                                                            "</tr>" +
                                                           "<tr>" +
                                                                "<td align='left' valign='top' style='font-family: Arial, Helvetica, sans-serif; font-size: 12px; color: #000;'>" +

                                                                 "<table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
                                                                                                "<tr>" +
                                                                                                    "<td align='left' valign='top' style='padding:30px 0px; font-size:15px;'>Dear <strong style='color:#d40046;'> " + PaxName + "</strong> </td>" +
                                                                                                "</tr>" +
                                                                                                "<tr>" +
                                                                                                    "<td align='center' valign='top' style='padding-bottom:50px; font-weight:bold; font-size:22px; color:#162849;'>" +

                                                                                                        "<img src='" + BLL.WebsiteStaticData.WebsiteUrl + "images/section-shape.png' width='97' height='20' />" +
                                                                                                    "</td>" +
                                                                                                "</tr>" +
                                                                                                "<tr>" +
                                                                                                    "<td align='left' valign='top' style='font-size:13px; text-align:justify; font-family:Arial, Helvetica, sans-serif;'>Greetings from " + etktLayout.BrandName + ".</td>" +
                                                                                                "</tr>" +
                                                                                                "<tr>" +
                                                                                                    "<td>&nbsp;</td>" +
                                                                                                "</tr>" +
                                                                                                "<tr>" +
                                                                                                    "<td>Due to new travel restrictions set by the Government your flight will be cancelled. If you want to learn more about the requirements needed to travel.</td>" +
                                                                                                "</tr>" +
                                                                                                 "<tr>" +
                                                                                                    "<td>&nbsp;</td>" +
                                                                                                "</tr>" +
                                                                                                "<tr>" +
                                                                                                    "<td>We're unable to reach you, kindly give us a call to postponed your trip or cancel for future credit.</td>" +
                                                                                                "</tr>" +
                                                                                                 "<tr>" +
                                                                                                    "<td>&nbsp;</td>" +
                                                                                                "</tr>" +
                                                                                                "<tr>" +
                                                                                                    "<td>If you have additional questions please text us at " + etktLayout.phone + " ,Call " + etktLayout.phone + " or email " + etktLayout.Email + " for further information. Our phone lines are extremely busy right now, so that is the best way to get in touch with us to get answers as quickly as possible.</td>" +
                                                                                                "</tr>" +
                                                                                                 "<tr>" +
                                                                                                    "<td>&nbsp;</td>" +
                                                                                                "</tr>" +
                                                                                                "<tr>" +
                                                                                                    "<td>Again, thank you so much for your understanding during this difficult time.</td>" +
                                                                                                "</tr>" +
                                                                                                 "<tr>" +
                                                                                                    "<td>&nbsp;</td>" +
                                                                                                "</tr>" +
                                                                                                "<tr>" +
                                                                                                    "<td style='padding:30px 0px;'>" +
                                                                                                        "Kind Regards<br />" +
                                                                                                        "<strong>" + etktLayout.BrandName + " </strong>" +
                                                                                                    "</td>" +
                                                                                                "</tr>" +
                                                                                            "</table>" +

                                                               "</td>" +
                                                            "</tr>" +
                                                           "<tr>" +
                                                                "<td align='center' valign='top' style='background: #1c2d4c; padding: 10px; color: #FFF; font-size: 11px; font-family: Arial, Helvetica, sans-serif;'>" +
                                                                    etktLayout.Eticket_Footer +
                                                               "</td>" +
                                                            "</tr>" +
                                                        "</table>" +
                                                   "</td>" +
                                                "</tr>" +
                                            "</table>";

    }

    private void getMailAltert1()
    {
        terms = @"<table   width='600' border='0' align='center' cellpadding='0' cellspacing='0' style='font-family: Arial, Helvetica, sans-serif;'>" +
                                               "<tr>" +
                                                   "<td>" +
                                                        "<table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
                                                           "<tr>" +
                                                                "<td align='left' valign='top' style='background: #73a5d8; padding: 30px 10px;'>" +
                                                                     etktLayout.Eticket_HeaderLayout(hfCompany.Value) +
                                                               "</td>" +
                                                            "</tr>" +
                                                           "<tr>" +
                                                                "<td align='left' valign='top' style='font-family: Arial, Helvetica, sans-serif; font-size: 12px; color: #000;'>" +

                                                                 "<table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
                                                                                                "<tr>" +
                                                                                                    "<td align='left' valign='top' style='padding:30px 0px; font-size:15px;'>Dear <strong style='color:#d40046;'> " + PaxName + "</strong> </td>" +
                                                                                                "</tr>" +                                                                                                
                                                                                                "<tr>" +
                                                                                                    "<td align='center' valign='top' style='padding-bottom:50px; font-weight:bold; font-size:22px; color:#162849;'>" +

                                                                                                        "<img src='" + BLL.WebsiteStaticData.WebsiteUrl + "images/section-shape.png' width='97' height='20' />" +
                                                                                                    "</td>" +
                                                                                                "</tr>" +
                                                                                                "<tr>" +
                                                                                                    "<td align='left' valign='top' style='font-size:13px; text-align:justify; font-family:Arial, Helvetica, sans-serif;'>Greetings from " + etktLayout.BrandName + ".</td>" +
                                                                                                "</tr>" +
                                                                                                "<tr>" +
                                                                                                    "<td>&nbsp;</td>" +
                                                                                                "</tr>" +
                                                                                                "<tr>" +
                                                                                                    "<td>We greatly value your business and the opportunity to serve your travel needs. As you know, the travel industry is experiencing widespread impacts as the COVID-19 situation continues to evolve. We are so sorry that your travel plans have been impacted but please know that it is important to us to get you the help you need.</td>" +
                                                                                                "</tr>" +
                                                                                                 "<tr>" +
                                                                                                    "<td>&nbsp;</td>" +
                                                                                                "</tr>" +
                                                                                                "<tr>" +
                                                                                                    "<td>We're unable to reach you, kindly give us a call to postponed your trip or cancel for future credit.</td>" +
                                                                                                "</tr>" +
                                                                                                 "<tr>" +
                                                                                                    "<td>&nbsp;</td>" +
                                                                                                "</tr>" +
                                                                                                "<tr>" + //"<a href='mailto:support@click2book.us'>support@click2book.us</a>"
                                                                                                    "<td>If you have additional questions please text us at " +etktLayout.phone+ " ,Call " + etktLayout.phone + " or email " + etktLayout.Email + " Our phone lines are extremely busy right now, so that is the best way to get in touch with us to get answers as quickly as possible.</td>" +
                                                                                                "</tr>" +
                                                                                                
                                                                                                 "<tr>" +
                                                                                                    "<td>&nbsp;</td>" +
                                                                                                "</tr>" +
                                                                                                "<tr>" +
                                                                                                    "<td>Again, thank you so much for your understanding during this difficult time.</td>" +
                                                                                                "</tr>" +
                                                                                                "<tr>" +
                                                                                                    "<td>&nbsp;</td>" +
                                                                                                "</tr>" +
                                                                                                "<tr>" +
                                                                                                    "<td style='padding:30px 0px;'>" +
                                                                                                        "Kind Regards<br />" +
                                                                                                        "<strong>" + etktLayout.BrandName + " </strong>" +
                                                                                                    "</td>" +
                                                                                                "</tr>" +
                                                                                            "</table>" +

                                                               "</td>" +
                                                            "</tr>" +
                                                           "<tr>" +
                                                                "<td align='center' valign='top' style='background: #1c2d4c; padding: 10px; color: #FFF; font-size: 11px; font-family: Arial, Helvetica, sans-serif;'>" +
                                                                    etktLayout.Eticket_Footer +
                                                               "</td>" +
                                                            "</tr>" +
                                                        "</table>" +
                                                   "</td>" +
                                                "</tr>" +
                                            "</table>";

    }

    private string emailadd()
    {
        var st = string.Empty;
        if (hfCompany.Value == "C2BUS")
        {
            st = "<a href='mailto:support@click2book.us'>support@click2book.us</a>";
        }
        else if (hfCompany.Value == "TRVJUNCTION_CA")
        {
            st = "<a href='mailto:support@traveljunction.ca'>support@traveljunction.ca</a>";
        }
        else if (hfCompany.Value == "FLTTROTT_CA")
        {
            st = "<a href='mailto:support@flighttrotters.ca'>support@flighttrotters.ca</a>";
        }
        else
        {

        }
        return st;
    }

    protected void btnSend_Click(object sender, EventArgs e)
    {
        GetSetDatabase obj = new GetSetDatabase();
        string xp = hidBookingID.Value;
        BookingDetails(xp);

         Alerts(Request.QueryString["T"]);

        //getMailAlert();

        mailServices.DataServiceSoapClient objmail = new mailServices.DataServiceSoapClient();

        if (objmail.Sendcustomermail(txtXPFrom.Text.Trim(), txtXPTo.Text.Trim(), "KINDLY CALL BACK FROM BILLING PHONE NUMBER ! - " + xp, terms, txtXPFrom.Text.Trim(), "dev@traveljunction.co.uk") == true)
        {
            ltrMsg.Text = "Mail Sent Successfully.";
            GetSetDatabase objGetSetDatabase = new GetSetDatabase();
            objGetSetDatabase.SET_Booking_Detail(hidBookingID.Value.Trim(), "001", "", "", "", "", "", "", "", "Alert/Call-Back Email sent to " + txtXPTo.Text.Trim(), "", "", "", "", "", objUserDetail.userID, "", "", "Update", "", "");
        }
        else
        {
            ltrMsg.Text = "Sorry, unable to send mail.";
        }
        
    }
}