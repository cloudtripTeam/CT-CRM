using mailServices;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Web.UI.HtmlControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Configuration;

public partial class Admin_Send_Eticket : System.Web.UI.Page
{
    FandHServices.FandHServicesClient obj = new FandHServices.FandHServicesClient();
    DataTable dtPax = new DataTable();
    DataTable dtSector = new DataTable();
    DataTable dtSectorMaster = new DataTable();
    DataTable dtComp = new DataTable();
    DataTable dtContact = new DataTable();
    DataTable dtBookingDtl = new DataTable();
    public string XPDetails { get; set; }
    public string PaxName { get; set; }
    public string company = string.Empty;
    public string terms { get; set; }
    public eTicketLayout etktLayout = new eTicketLayout();
    UserDetail objUserDetail;

    protected void Page_Load(object sender, EventArgs e)
    {
        objUserDetail = Session["UserDetails"] as UserDetail;
       
        if (Session["UserDetails"] != null)
        {
            if (!IsPostBack)
            {
                objUserDetail = Session["UserDetails"] as UserDetail;
                if (!objUserDetail.isAuth("SentEticket"))
                {
                    Response.Redirect("~/Admin/AccessDenied.aspx");
                    return;
                }


                if (!string.IsNullOrEmpty(Request.QueryString.Get("BID")))
                {

                    string xp = Request.QueryString.Get("BID");
                    //txtInvoice.Text = xp;
                    hidBookingID.Value = xp;
                    XPDetails = BookingDetails(xp);
                    bindEmailIds(dtComp,dtContact);

                    if (dtComp.Rows[0]["BookingByCompany"].ToString() == "TRVJUNCTION_USA" || dtComp.Rows[0]["BookingByCompany"].ToString() == "C2BUS")
                    {
                        getEticket_us();
                    }
                    else
                    {
                        getEticket();
                    }


                    
                    dvXP.Visible = true;
                }
               
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx", false);
        }
    }

    public string BookingDetails(string XP)
    {
        dtPax = obj.GET_Passenger_Detail(XP, "001");
        dtSector = obj.GET_Sector_Detail(XP, "001");
        dtSectorMaster = obj.GET_Sectors_Master(XP, "001");
        dtComp = obj.GET_Booking_Master(XP);
        dtContact = obj.GET_Contact_Detail(XP, "001");
        dtBookingDtl = obj.GET_Booking_Detail1(XP, "001", "", "", "", "", "", "", "", "");
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
       

        StringBuilder sb = new StringBuilder();
       

        
        return sb.ToString();


    }
    protected void btnSend_Click(object sender, EventArgs e)
    {

       
        GetSetDatabase objGetSetDatabase = new GetSetDatabase();
        XPDetails = BookingDetails(hidBookingID.Value.Trim());
        mailServices.DataServiceSoapClient objmail = new DataServiceSoapClient();
        if(ckboxEticket.Checked)
        {

            if (dtComp.Rows[0]["BookingByCompany"].ToString() == "TRVJUNCTION_USA" || dtComp.Rows[0]["BookingByCompany"].ToString() == "C2BUS")
            {
                getEticket_us();
            }
            else
            {
                getEticket();
            }

            if (SendMail("E-TICKET - "+ hidBookingID.Value.Trim(), terms,"eticket"))
            {
                ltrMsg.Text += "E-TICKET MAIL Sent to " + txtXPTo.Text;
                objGetSetDatabase.SET_Booking_Detail(hidBookingID.Value.Trim(), "001", "", "", "", "", "", "", "", "e-ticket sent to  " + txtXPTo.Text, "", "", "", "", "", objUserDetail.userID + "/Sys", "", "", "Update", "");
            }
            else {

                ltrMsg.Text += "<br/> Unable to send E-TICKET to " + txtXPTo.Text;
            }

        }
        if (ckboxSMS.Checked)
        {

            string sms = "Dear Customer, E-Ticket has been sent to your email id. Kindly visit your email to get e-ticket(s). Thank you";
            if (objmail.SendSMS("WHUJ3Z", "mcDbmc", txtMobile.Text.Trim(), sms, "TRVJUNCTION") == "true")
            {
                ltrMsg.Text += "<br/> E-TICKET SMS Sent to " + txtMobile.Text;
                objGetSetDatabase.SET_Booking_Detail(hidBookingID.Value.Trim(), "001", "", "", "", "", "", "", "", "e-ticket SMS sent to  " + txtMobile.Text, "", "", "", "", "", objUserDetail.userID + "/Sys", "", "", "Update", "");
            }
            else
            {
                ltrMsg.Text += "<br/> Unable to send  SMS to " + txtMobile.Text;
            }
        }
        if (ckboxFeedback.Checked)
        {
            if (SendMail("Feedback ", getFeedback(),""))
            {
                ltrMsg.Text += " <br/> FEEDBACK mail Sent to " + txtXPTo.Text;
                objGetSetDatabase.SET_Booking_Detail(hidBookingID.Value.Trim(), "001", "", "", "", "", "", "", "", "feedback mail  sent to  " + txtXPTo.Text, "", "", "", "", "", objUserDetail.userID + "/Sys", "", "", "Update", "");
            }
            else
            {
                ltrMsg.Text = "Sorry, unable to send feedback mail to " + txtXPTo.Text;
            }
           
        }
        if (ckboxVoucher.Checked)
        {
            if (SendMail("Voucher ", getVoucher(),"voucher"))
            {
                ltrMsg.Text += " <br/> Voucher mail Sent to " + txtXPTo.Text;
                objGetSetDatabase.SET_Booking_Detail(hidBookingID.Value.Trim(), "001", "", "", "", "", "", "", "", "Voucher mail  sent to  " + txtXPTo.Text, "", "", "", "", "", objUserDetail.userID + "/Sys", "", "", "Update", "");
            }
            else
            {
                ltrMsg.Text = "Sorry, unable to send Voucher mail to " + txtXPTo.Text;
            }
        }
        
    }

    private void bindEmailIds(DataTable dtComp,DataTable dtContact)
    {
        #region From Email ID
        Layout lay = new Layout();
        if (dtComp != null)
        {
            if (dtComp.Rows.Count > 0)
            {
                company = dtComp.Rows[0]["BookingByCompany"].ToString();

                CompanyDetails objc = lay.SetCompanyDetail(company);
                hfCompany.Value = company;
                if (company.ToUpper() == "TRVJUNCTION")
                {
                    txtXPFrom.Text = "documents@traveljunction.co.uk";
                   
                }
                else if (company.ToUpper() == "DIAL4TRV")//FLTTROTT
                {
                    txtXPFrom.Text = "documents@dial4travel.co.uk";
                }
                else if (company.ToUpper() == "FLTTROTT")//
                {
                    txtXPFrom.Text = "documents@flighttrotters.co.uk";
                }
                hfCompany.Value = company.ToUpper();
                //if (hfCompany.Value == "TRVJUNCTION")
                //{
                    ckboxFeedback.Enabled = objc.isFeedBack;
                    btnSend.Enabled = objc.isEticket; ;
                    ckboxSMS.Enabled = objc.isSMS;
                    ckboxEticket.Enabled = objc.isEticket;
                    ckboxVoucher.Enabled = objc.isVoucher;
                    ltrMsg.Text = "";
                //}

                //else
                //{
                //    ckboxFeedback.Enabled = false;
                //    btnSend.Enabled = false;
                //    ckboxSMS.Enabled = false;
                //    ckboxEticket.Enabled = false;
                //    ltrMsg.Text = "Only Travel Junction's booking allowed";

                //}
            }

        }
        #endregion

        #region To Email ID

        if (dtContact != null)
        {
            if (dtContact.Rows.Count > 0)
            {
              string to = dtContact.Rows[0]["EmailID"].ToString();
                txtXPTo.Text = to;
                txtMobile.Text = dtContact.Rows[0]["MobileNo"].ToString();
            }

        }
        #endregion

    }
    private bool SendMail(string subject, string mailbody,string type)
    {
       
        try
        {
            string fromAddress = txtXPFrom.Text.Trim();
            MailMessage msg = new MailMessage();
            msg.From = new MailAddress(txtXPFrom.Text.Trim());
            msg.To.Add(new MailAddress(txtXPTo.Text.Trim()));
            msg.CC.Add(new MailAddress(txtXPFrom.Text.Trim()));

            msg.Subject = subject;
            msg.Body = mailbody;
            msg.IsBodyHtml = true;
            if (type == "eticket")
            {
                if (fileAttached.HasFile)
                {
                    string FileName = Path.GetFileName(fileAttached.PostedFile.FileName);
                    msg.Attachments.Add(new Attachment(fileAttached.PostedFile.InputStream, FileName));
                }
            }
            else if (type == "voucher")
            {
                string path = Server.MapPath("~/images/Gift-Voucher10.jpg");

                msg.Attachments.Add(new Attachment(path));
            }

            //if (company.ToUpper() == "TRVJUNCTION")
            if (fromAddress.ToLower().Contains("@traveljunction.co.uk"))
            {
                SmtpClient smtp = new SmtpClient("smtp-relay.gmail.com");
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential("suraj@traveljunction.co.uk", "trotters258*");
                smtp.EnableSsl = false;
                smtp.Port = 25;
                smtp.Send(msg);
            }
            else if (fromAddress.ToLower().Contains("@skyworldtravel.co.uk"))
            {
                SmtpClient smtp = new SmtpClient("smtp-relay.gmail.com");
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential("it@skyworldtravel.co.uk", "Trotters258*");
                smtp.EnableSsl = false;
                smtp.Port = 25;
                smtp.Send(msg);
            }
            else if (fromAddress.ToLower().Contains("@travel-performance.co.uk"))
            {
                SmtpClient smtp = new SmtpClient("smtp-relay.gmail.com");
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential("it@flightsassist.com", "trotters258*");
                smtp.EnableSsl = false;
                smtp.Port = 25;
                smtp.Send(msg);
            }
            else if (fromAddress.ToLower().Contains("@click2book.co.uk"))
            {
                SmtpClient smtp = new SmtpClient("smtpout.secureserver.net");
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential("sendout@click2book.co.uk", "asd@1234");
                smtp.EnableSsl = false;
                smtp.Port = 25;
                smtp.Send(msg);
            }
            else if (fromAddress.ToLower().Contains("@traveljunctionus.com"))
            {
                SmtpClient smtp = new SmtpClient("smtpout.secureserver.net");
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential("sendout@traveljunctionus.com", "asd@1234");
                smtp.EnableSsl = false;
                smtp.Port = 25;
                smtp.Send(msg);
            }
            else if (fromAddress.ToLower().Contains("@flighttrotters.co.uk"))
            {
                SmtpClient smtp = new SmtpClient("mail.flighttrotters.co.uk");
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential("sendmail@flighttrotters.co.uk", "asd@1234");
                smtp.EnableSsl = false;
                smtp.Port = 25;
                smtp.Send(msg);
            }
            else if (fromAddress.ToLower().Contains("@traveloflights.co.uk"))
            {
                SmtpClient smtp = new SmtpClient(ConfigurationManager.AppSettings["HostFTCA"]);
                smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["PortFTCA"]);
                smtp.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["UserNameFTCA"], ConfigurationManager.AppSettings["PasswordFTCA"]);
                smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSslFTCA"]);
                smtp.Send(msg);
            }

            else
            {
                SmtpClient smtp = new SmtpClient("smtpout.secureserver.net");
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential("sendout@traveljunctionus.com", "asd@1234");
                smtp.EnableSsl = false;
                smtp.Port = 25;
                smtp.Send(msg);



                //SmtpClient smtp = new SmtpClient("smtp.livemail.co.uk");
                //smtp.UseDefaultCredentials = false;
                //smtp.Credentials = new System.Net.NetworkCredential("sendout@dial4travel.co.uk", "asd@1234");
                //smtp.EnableSsl = false;
                //smtp.Port = 25;
                //smtp.Send(msg);


            }
            return true;           
           
        }
        catch (Exception ex)
        {
            return false;
        }

    }
    private void addHeader()
    {


        string pdfpath = Server.MapPath("~/Images/ElectronicTicket.pdf");

        string imagepath = Server.MapPath("~/Images/logos/TRVJUNCTION.png");

        Document doc = new Document();

        try

        {

            PdfWriter.GetInstance(doc, new FileStream(pdfpath, FileMode.Create));
            doc.Open();

            doc.Add(new Paragraph("logo"));

            iTextSharp.text.Image gif = iTextSharp.text.Image.GetInstance(imagepath);

            doc.Add(gif);

        }

        catch (Exception ex)

        {

            //Log error;

        }

        finally

        {

            doc.Close();

        }

    }

    private void getEticket()

    {
        terms = @"<table   width='600' border='0' align='center' cellpadding='0' cellspacing='0' style='font-family: Arial, Helvetica, sans-serif;'>" +
                                               "<tr>" +
                                                   "<td>" +
                                                        "<table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
                                                           "<tr>" +
                                                                "<td align='left' valign='top' style='background: #03386e; padding: 30px 10px;'>" +
                                                                     etktLayout.Eticket_HeaderLayout(hfCompany.Value) +
                                                               "</td>" +
                                                            "</tr>" +
                                                           "<tr>" +
                                                                "<td align='left' valign='top' style='font-family: Arial, Helvetica, sans-serif; font-size: 12px; color: #000;'>" +
                                                                    "<table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
                                                                       "<tr>" +
                                                                           "<td>" +
                                                                                "<img runat='server' src='" + BLL.WebsiteStaticData.WebsiteUrl + "images/boarding-pass.png' width='600' height='202' /></td>" +
                                                                        "</tr>" +
                                                                       "<tr>" +
                                                                            "<td align='left' valign='top' style='padding: 30px 0px; font-size: 14px;'>Dear <strong>" + PaxName + ",</strong></td>" +
                                                                        "</tr>" +
                                                                       "<tr>" +
                                                                            "<td align='center' valign='top' style='padding-bottom: 50px; font-weight: bold; font-size: 20px;'>Greetings from " + etktLayout.BrandName + "!</td>" +
                                                                        "</tr>" +
                                                                       "<tr>" +
                                                                            "<td align='left' valign='top' style='font-size: 14px;'>Thank you for booking with us. Please find attached your Travel Document for your upcoming travel.<br />" +
                                                                                "<br />" +
                                                                                "An electronic ticket (e-ticket) is a paperless ticketing method. Because your electronic ticket is held in the airlines computer, you cannot forget it or lose it. More importantly, your electronic ticket cannot be stolen, saving you the cost of a replacement ticket. When you arrive at the airline check in desk, you will be required to present the following to receive your boarding pass, an official form of identification i.e. your passport, a print out of your confirmation email to show to the airline." +

                                                                           "</td>" +
                                                                        "</tr>" +
                                                                       "<tr>" +
                                                                            "<td align='left' valign='top' style='font-size: 14px;'>&nbsp;</td> " +
                                                                        "</tr>" +
                                                                       "<tr>" +
                                                                            "<td align='left' valign='top' style='font-size: 14px;'><strong>We advise you to reconfirm your flight 72 Hrs prior to departure </strong></td>" +
                                                                        "</tr>" +
                                                                       "<tr>" +
                                                                            "<td align='left' valign='top' style='font-size: 14px;'>&nbsp;</td>" +
                                                                        "</tr>" +
                                                                       "<tr>" +
                                                                            "<td align='left' valign='top' style='font-size: 14px;'>" +
                                                                                "<table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
                                                                                   "<tr>" +
                                                                                        "<td width='30' align='center' valign='top'>" +
                                                                                            "<img runat='server' src='" + BLL.WebsiteStaticData.WebsiteUrl + "images/bullet.png' width='12' height='12' /></td>" +
                                                                                        "<td width='10' align='left' valign='top'>&nbsp;</td>" +
                                                                                        "<td align='left' valign='top'>By European law we strongly recommend you to buy insurance before travelling.</td>" +
                                                                                    "</tr>" +
                                                                                   "<tr>" +
                                                                                        "<td width='30' align='center' valign='top'>" +
                                                                                            "<img runat='server' src='" + BLL.WebsiteStaticData.WebsiteUrl + "images/bullet.png' width='12' height='12' /></td>" +
                                                                                        "<td width='10' align='left' valign='top'>&nbsp;</td>" +
                                                                                        "<td align='left' valign='top'>Kindly reconfirm all the terms and conditions regarding your reservation before confirming with our travel consultant.</td>" +
                                                                                    "</tr>" +
                                                                                   "<tr>" +
                                                                                        "<td align='center' valign='top'>" +
                                                                                            "<img runat='server' src='" + BLL.WebsiteStaticData.WebsiteUrl + "images/bullet.png' width='12' height='12' /></td>" +
                                                                                        "<td align='left' valign='top'>&nbsp;</td>" +
                                                                                        "<td align='left' valign='top'>The tickets will be issued as electronically. Please get your VISA if required before travelling.</td>" +
                                                                                    "</tr>" +
                                                                                   "<tr>" +
                                                                                        "<td align='center' valign='top'>&nbsp;</td>" +
                                                                                        "<td align='left' valign='top'>&nbsp;</td>" +
                                                                                        "<td align='left' valign='top'>&nbsp;</td>" +
                                                                                    "</tr>" +

                                                                                "</table>" +
                                                                           "</td>" +
                                                                        "</tr>" +
                                                                       "<tr>" +
                                                                            "<td align='left' valign='top' style='font-size: 14px;'>Please note that you must have a valid Passport with a minimum of 6 months on it or you will not be able to travel. Your passport must also be in excellent condition - the presentation of damaged passports may mean you are unable to travel. It is mandatory to carry a machine readable passport or valid visa for travel to USA; otherwise you will be denied boarding.</td>" +
                                                                        "</tr>" +
                                                                       "<tr>" +
                                                                            "<td align='left' valign='top' style='font-size: 14px;'>&nbsp;</td>" +
                                                                        "</tr>" +
                                                                       "<tr>" +
                                                                            "<td align='left' valign='top' style='font-size: 14px;'>Check in Times: Due to security measures we currently recommend 3 hours for intercontinental flights and 2 hours for European and Domestic flights.</td>" +
                                                                        "</tr>" +
                                                                       "<tr>" +
                                                                            "<td align='left' valign='top' style='font-size: 14px;'>&nbsp;</td>" +
                                                                        "</tr>"+
                                                                       // etktLayout.FeedbackLink +

                                                                       "<tr>" +
                                                                            "<td style='padding: 30px 0px;'>Kind Regards<br />" +

                                                                                "<strong>" +etktLayout.BrandName + "</strong>" +
                                                                           "</td>" +
                                                                        "</tr>"+
                                                                    "</table>" +
                                                               "</td>" +
                                                            "</tr>"+
                                                           "<tr>"+
                                                                "<td align='center' valign='top' style='background: #1c2d4c; padding: 10px; color: #FFF; font-size: 11px; font-family: Arial, Helvetica, sans-serif;'>" +
                                                                   
                                                               "</td>"+
                                                            "</tr>"+
                                                        "</table>"+
                                                   "</td>"+
                                                "</tr>"+
                                            "</table>";


        


    }

    private void getEticket_us()

    {

        terms = @"<table   width='600' border='0' align='center' cellpadding='0' cellspacing='0' style='font-family: Arial, Helvetica, sans-serif;'>" +
                                               "<tr>" +
                                                   "<td>" +
                                                        "<table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
                                                           "<tr>" +
                                                                "<td align='left' valign='top' style='background: #03386e; padding: 30px 10px;'>" +
                                                                     etktLayout.Eticket_HeaderLayout(hfCompany.Value) +
                                                               "</td>" +
                                                            "</tr>" +
                                                           "<tr>" +
                                                                "<td align='left' valign='top' style='font-family: Arial, Helvetica, sans-serif; font-size: 12px; color: #000;'>" +
                                                                    "<table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
                                                                       "<tr>" +
                                                                           "<td>" +
                                                                                "<img runat='server' src='" + BLL.WebsiteStaticData.WebsiteUrl + "images/boarding-pass.png' width='600' height='202' /></td>" +
                                                                        "</tr>" +
                                                                       "<tr>" +
                                                                            "<td align='left' valign='top' style='padding: 30px 0px; font-size: 14px;'>Dear <strong>" + PaxName + ",</strong></td>" +
                                                                        "</tr>" +
                                                                       "<tr>" +
                                                                            "<td align='center' valign='top' style='padding-bottom: 50px; font-weight: bold; font-size: 20px;'>Greetings from " + etktLayout.BrandName + "!</td>" +
                                                                        "</tr>" +
                                                                       "<tr>" +
                                                                            "<td align='left' valign='top' style='font-size: 14px;'>This is to inform you that your e-ticket has been issued. Please find below the e-ticket for your ready reference.<br /><br/>" +
                                                                                "<br />" +
                                                                                "You have to take a print out of e-ticket while going to airport." +

                                                                           "</td>" +
                                                                        "</tr>" +

                                                                     "<tr>" +
                                                                            "<td align='left' valign='top' style='font-size: 14px;'>" +
                                                                            "Due to security measures, we recommend you to reach airport at least 5 hours prior to your departure time because in the case of a no show the tickets are totally non changeable and nonrefundable.<br /><br/>" +


                                                                           "</td>" +
                                                                        "</tr>" +

                                                                          "<tr>" +
                                                                            "<td align='left' valign='top' style='font-size: 14px;'>" +
                                                                            " Please reconfirm your flights before 72 hours prior to your departure time to avoid any inconvenience.<br /><br/>" +


                                                                           "</td>" +
                                                                        "</tr>" +

                                                                          "<tr>" +
                                                                            "<td align='left' valign='top' style='font-size: 14px;'>" +
                                                                            "Positive Identification required at Check-In (Passport) for E-Tickets.<br /><br/>" +


                                                                           "</td>" +
                                                                        "</tr>" +

                                                                          "<tr>" +
                                                                            "<td align='left' valign='top' style='font-size: 14px;'>" +
                                                                            "The Electronic Ticket Record will not reflect flight reservation changes, schedule changes, or cancellations made after the electronic ticket was issued.<br /><br/>" +


                                                                           "</td>" +
                                                                        "</tr>" +


                                                                          "<tr>" +
                                                                            "<td align='left' valign='top' style='font-size: 14px;'>" +
                                                                            "Terms/Conditions of Travel and Carrier Liability Notices can be requested from The Travel Agency or the Transporting Carrier.<br /><br/>" +


                                                                           "</td>" +
                                                                        "</tr>" +

                                                                        "<tr>" +
                                                                            "<td align='left' valign='top' style='font-size: 14px;'>" +
                                                                            "RT-PCR report not earlier than 72 hours before scheduled time of landing and any " +
                                                                            "more documents required please contact airline.<br/><br/>" +
                                                                           "</td>" +
                                                                        "</tr>" +

                                                                        "<tr>" +
                                                                            "<td align='left' valign='top' style='font-size: 14px;'>" +
                                                                            "Have a Safe Journey<br /><br/>" +
                                                                           "</td>" +

                                                                          

                                                                          "<tr>" +
                                                                            "<td align='left' valign='top' style='font-size: 18px;background:#0befda;'>" +
                                                                            "Please reply to us “I received the E-Ticket”<br />" +


                                                                           "</td>" +
                                                                        "</tr>" +


                                                                         "<tr>" +
                                                                            "<td align='left' valign='top' style='font-size: 16px;color:#'>" +
                                                                            "<br/>Por favor responda a nosotros 'Recibí el boleto electrónico'<br/>" +


                                                                           "</td>" +
                                                                        "</tr>" +
                                                                          "<tr>" +
                                                                            "<td align='left' valign='top' style='font-size: 16px;color:#'>" +
                                                                            "<br/><b>Click here to see the</b> <a href='https://www." + etktLayout.Email.Split('@')[1] + "/terms-conditions.aspx' target='_blank'> Terms & conditions</a>" +
                                                                           "</td>" +
                                                                        "</tr>" +
                                                                       // etktLayout.FeedbackLink +

                                                                       "<tr>" +
                                                                            "<td style='padding: 30px 0px;'>Kind Regards<br />" +

                                                                                "<strong>" + etktLayout.BrandName + "</strong>" +
                                                                           "</td>" +
                                                                        "</tr>" +
                                                                    "</table>" +
                                                               "</td>" +
                                                            "</tr>" +
                                                           "<tr>" +
                                                                "<td align='center' valign='top' style='background: #1c2d4c; padding: 10px; color: #FFF; font-size: 11px; font-family: Arial, Helvetica, sans-serif;'>" +

                                                               "</td>" +
                                                            "</tr>" +
                                                        "</table>" +
                                                   "</td>" +
                                                "</tr>" +
                                            "</table>";





    }
    private string getFeedback()

    {
        string link = string.Empty;
        string logo = string.Empty;
        if (hfCompany.Value == "TRVJUNCTION")
        { link = "https://www.trustpilot.com/review/www.traveljunction.co.uk"; logo = "trust-pilot.png"; }
        else if(hfCompany.Value == "TRVJUNCTION")
        { link = "https://www.reviewcentre.com/Travel-Agents/Click2book-www-click2book-co-uk-reviews_9614427"; logo = "rcLogo.png";}

        return  @"<table   width='600' border='0' align='center' cellpadding='0' cellspacing='0' style='font-family: Arial, Helvetica, sans-serif;'>" +
                                               "<tr>" +
                                                   "<td>" +
                                                        "<table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
                                                           "<tr>" +
                                                                "<td align='left' valign='top' style='background: #03386e; padding: 30px 10px;'>" +
                                                                     etktLayout.Eticket_HeaderLayout(hfCompany.Value) +
                                                               "</td>" +
                                                            "</tr>" +
                                                           "<tr>" +
                                                                "<td align='left' valign='top' style='font-family: Arial, Helvetica, sans-serif; font-size: 12px; color: #000;'>" +
                                                                    "<table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
                                                                       "<tr>" +
                                                                           "<td>" +
                                                                                "<img runat='server' src='" + BLL.WebsiteStaticData.WebsiteUrl + "images/boarding-pass.png' width='600' height='202' /></td>" +
                                                                        "</tr>" +
                                                                       "<tr>" +
                                                                            "<td align='left' valign='top' style='padding: 30px 0px; font-size: 14px;'>Dear <strong>" + PaxName + ",</strong></td>" +
                                                                        "</tr>" +
                                                                       
                                                                       "<tr>" +
                                                                            "<td align='left' valign='top' style='font-size: 14px;'>Thank You for choosing us. We appreciate your business and value you as a customer. To help us continue our high quality of service, our goal is always to provide our very best product so that our customers are happy. It's also our goal to continue improving. We invite you to leave us your feedback." +

                                                                           "</td>" +
                                                                        "</tr>" +
                                                                       
                                                                       "<tr>" +
                                                                            "<td align='left' valign='top' style='font-size: 14px;'><strong>Leave us a review or suggestion by clicking on the link below :</strong></td>" +
                                                                        "</tr>" +
                                                                       "<tr>" +
                                                                            "<td align='left' valign='top' style='font-size: 14px;'>&nbsp;</td>" +
                                                                        "</tr>" +
                                                                       "<tr>" +
                                                                            "<td align='left' valign='top' style='font-size: 14px;'><a href='"+link+"'>" + link + "</a></td>" +
                                                                        "</tr>" +
                                                                       "<tr>" +
                                                                            "<td align='left' valign='top' style='font-size: 14px;'>&nbsp;</td>" +
                                                                        "</tr>" +
                                                                       "<tr>" +
                                                                            "<td align='left' valign='top' style='font-size: 14px;'><a href='" + link + "'>" +
                                                                                "<img runat='server' src='" + BLL.WebsiteStaticData.WebsiteUrl + "images/"+logo+
                                                                                "' width='600' height='179' border='0' /></a></td>" +
                                                                        "</tr>" +

                                                                       "<tr>" +
                                                                            "<td style='padding: 30px 0px;'>Kind Regards<br />" +

                                                                                "<strong>" + etktLayout.BrandName + "</strong>" +
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

    private string getVoucher()
    {
        return @"<table   width='600' border='0' align='center' cellpadding='0' cellspacing='0' style='font-family: Arial, Helvetica, sans-serif;'>" +
                                               "<tr>" +
                                                   "<td>" +
                                                        "<table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
                                                           "<tr>" +
                                                                "<td align='left' valign='top' style='background: #03386e; padding: 30px 10px;'>" +
                                                                     etktLayout.Eticket_HeaderLayout(hfCompany.Value) +
                                                               "</td>" +
                                                            "</tr>" +
                                                           "<tr>" +
                                                                "<td align='left' valign='top' style='font-family: Arial, Helvetica, sans-serif; font-size: 12px; color: #000;'>" +

                                                                 "<table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
                                                                                                "<tr>" +
                                                                                                    "<td align='left' valign='top' style='padding:30px 0px; font-size:15px;'>Dear <strong style='color:#d40046;'>" + PaxName + "</strong> </td>" +
                                                                                                "</tr>" +
                                                                                                "<tr>" +
                                                                                                    "<td align='center' valign='top' style='padding-bottom:50px; font-weight:bold; font-size:22px; color:#162849;'>" +
                                                                                                        "Hope this mail meets you well ! <br />" +
                                                                                                        "<img src='" + BLL.WebsiteStaticData.WebsiteUrl + "images/section-shape.png' width='97' height='20' />" +
                                                                                                    "</td>" +
                                                                                                "</tr>" +
                                                                                                "<tr>" +
                                                                                                    "<td align='left' valign='top' style='font-size:13px; text-align:justify; font-family:Arial, Helvetica, sans-serif;'>This email has been sent to you as a part of the referral scheme we have initiated for our passengers. We have attached a voucher which you can use to refer your friends or family members who are looking for the tickets. As a part of this scheme, you can get a discount of £10 per booking for your next reservation with us. Also, the person using your referral will get a discount of £10 for their first booking with us. In order to be entitled for this discount, you referral should have the attached voucher and it should be forwarded through your email address to him/her. Please ask him/her to forward the same email to us so that we will be able to recognize that you have referred this passenger. Once we will receive the forwarded email from him/her, a discount voucher of £10 will be added to your next reservation. Keep Referring <span style='font-size:20px; color:#d40046;'>&#x263A;</span></td>" +
                                                                                                "</tr>" +
                                                                                                "<tr>" +
                                                                                                    "<td>&nbsp;</td>" +
                                                                                                "</tr>" +
                                                                                                "<tr>" +
                                                                                                    "<td>Looking forward to serve you again in near future.</td>" +
                                                                                                "</tr>" +
                                                                                                "<tr>" +
                                                                                                    "<td><img src='" + BLL.WebsiteStaticData.WebsiteUrl + "images/Gift-Voucher10.jpg' width='600' height='300' /></td>" +
                                                                                                "</tr>" +
                                                                                                "<tr>" +
                                                                                                    "<td style='padding:30px 0px;'>" +
                                                                                                        "Kind Regards<br />" +
                                                                                                        "<strong>" + etktLayout.BrandName + " </strong>" +
                                                                                                    "</td>" +
                                                                                                "</tr>" +
                                                                                            "</table>"+

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

}
