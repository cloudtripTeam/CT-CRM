using System;
using System.Data;
using HiQPdf;
using System.Net.Mail;
using System.Text;
using System.Configuration;

public partial class Admin_sendMail : System.Web.UI.Page
{
    DataTable dtPax = new DataTable();
    DataTable dtSector = new DataTable();
    DataTable dtSectorMaster = new DataTable();
    DataTable dtComp = new DataTable();
    DataTable dtContact = new DataTable();
    DataTable dtPrice = new DataTable();
    DataTable dtTrans = new DataTable();
    DataTable dtBD = new DataTable();
    Layout lot = new Layout();
    AttachmentCollection attachs;
    FandHServices.FandHServicesClient obj = new FandHServices.FandHServicesClient();
    MailMessage msg = new MailMessage();
    public eTicketLayout etktLayout = new eTicketLayout();
    UserDetail objUserDetail;
    public string terms { get; set; }
    public string PaxName { get; set; }
    string documents = "documents";

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
                    if (dtSectorMaster != null && dtSectorMaster.Rows.Count != 0)
                    {

                        if (dtSectorMaster.Rows[0]["Ticket_IssuedBy"].ToString() != "")
                        {
                            btnSend.Visible = true;
                            hplInvoice.HRef = "~/admin/invoice.aspx?BID=" + xp + "&PID=001";
                            hplreceipt.HRef = "~/admin/Atol_Reciept.aspx?BID=" + xp + "&PID=001";
                            hplcertificate.HRef = "~/admin/Atol_Certificate.aspx?BID=" + xp + "&PID=001";

                            if (dtSectorMaster.Rows[0]["Ticket_IssuedBy"].ToString() == "10950" && dtBD.Rows[0]["ATOL_Type"].ToString() == "Flights Only/PUBLIC BONDED")
                            {
                                ckboxReciept.Checked = false;
                                ckboxReciept.Enabled = false;
                                ckboxReciept.Visible = false;
                                hplreceipt.Visible = false;

                                ckboxCertificate.Checked = true;
                                ckboxCertificate.Enabled = true;
                                ckboxCertificate.Visible = true;
                                hplcertificate.Visible = true;

                                ckboxInvoice.Checked = true;
                                ckboxInvoice.Enabled = true;
                                ckboxInvoice.Visible = true;
                                hplInvoice.Visible = true;
                            }
                            else
                            {

                                ckboxReciept.Checked = true;
                                ckboxReciept.Enabled = true;
                                ckboxReciept.Visible = true;
                                hplreceipt.Visible = true;

                                ckboxCertificate.Checked = true;
                                ckboxCertificate.Enabled = true;
                                ckboxCertificate.Visible = true;
                                hplcertificate.Visible = true;

                                ckboxInvoice.Checked = false;
                                ckboxInvoice.Enabled = false;
                                ckboxInvoice.Visible = false;
                                hplInvoice.Visible = false;
                            }
                        }
                        else
                        {
                            ckboxCertificate.Checked = false;
                            ckboxCertificate.Enabled = false;
                            ckboxCertificate.Visible = false;
                            hplcertificate.Visible = false;

                            ckboxReciept.Checked = false;
                            ckboxReciept.Enabled = false;
                            ckboxReciept.Visible = false;
                            hplreceipt.Visible = false;

                            ckboxInvoice.Checked = false;
                            ckboxInvoice.Enabled = false;
                            ckboxInvoice.Visible = false;
                            hplInvoice.Visible = false;

                            btnSend.Visible = false;
                            ltrMsg.Text = "System can not send a mail because supplier information not available";
                        }
                    }
                    else
                    {

                        ckboxCertificate.Checked = false;
                        ckboxCertificate.Enabled = false;
                        ckboxCertificate.Visible = false;
                        hplcertificate.Visible = false;


                        ckboxReciept.Checked = false;
                        ckboxReciept.Enabled = false;
                        ckboxReciept.Visible = false;
                        hplreceipt.Visible = false;

                        ckboxInvoice.Checked = false;
                        ckboxInvoice.Enabled = false;
                        ckboxInvoice.Visible = false;
                        hplInvoice.Visible = false;

                        btnSend.Visible = false;
                    }


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
                    bindEmailIds(dtComp, dtContact);


                    //this Check for added only  C2BCA , TRVJUNCTION_CA ,FLTTROTT_CA
                    //C2BCA
                    //TRVJUNCTION_CA
                    //FLTTROTT_CA
                    var BookingByCompany = dtComp.Rows[0]["BookingByCompany"].ToString();
                    if (BookingByCompany == "C2BCA" || BookingByCompany == "TRVJUNCTION_CA" || BookingByCompany == "FLTTROTT_CA")
                    {
                        GetMailBody_New();
                    }
                    else
                    {
                        getMailBody();
                    }
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
        dtSectorMaster = obj.GET_Sectors_Master(XP, "001");
        dtComp = obj.GET_Booking_Master(XP);
        hfCompany.Value = dtComp.Rows[0]["BookingByCompany"].ToString();
        dtBD = obj.GET_Booking_Detail1(XP, "001", "", "", "", "", "", "", "", "");
        dtContact = obj.GET_Contact_Detail(XP, "001");
        dtPrice = obj.GET_Amount_Charges_Detail(XP, "001", "", "", "", "", "", "", "");
        dtTrans = obj.GET_Transaction_Master(XP, "");
        obj.GET_Transaction_Details(XP, "");

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

    private string generate_AtolReciept(string xp)
    {
        double AtolCharges = dtPax.Rows.Count * 2.5;
        string tran = string.Empty;
        double subTotal = 0;
        double tranTotal = 0;
        string heading = "ATOL Reciept";
        DataTable dtSupp = new DataTable();


        dtSupp = lot.Supplier(dtSectorMaster.Rows[0]["Ticket_IssuedBy"].ToString());
        string SupName = dtSupp.Rows[0]["SUP_Name"].ToString();
        string SupCode = dtSupp.Rows[0]["Sup_AtolNo"].ToString();

        if (dtBD.Rows.Count > 0)
        {

            AtolCharges = dtPax.Rows.Count * 2.5;
        }
        foreach (DataRow drprc in dtPrice.Rows)
        {
            subTotal += (Convert.ToDouble(drprc["SellPrice"]) * Convert.ToInt32(drprc["NoOfPax"]));
        }
        if (dtTrans.Rows.Count > 0)
        {
            tranTotal = Convert.ToDouble(dtTrans.Compute("Sum(TrnsAmount)", ""));
        }
        CompanyDetails objc = lot.SetCompanyDetail(dtComp.Rows[0]["BookingByCompany"].ToString());
        #region
        string inv = @"<table width='1000' border='0' align='center' cellpadding='0' cellspacing='0' style='font-family:Tahoma, Arial, Helvetica, sans-serif;'>" +
        "<tr>" +
        "<td style='padding:30px;'><table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
        "<tr>" +
        "<td align='left' valign='top'><table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
        "<tr>" +
        "<td align='left' valign='top'><img src='https://www.flightsandholidays.biz/images/logoes/" + objc.Comp_logo + ".png' width='251' height='79' alt='Logo' /></td>" +
        "<td align='right' valign='top' style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>" + objc.Comp_Address + "<br />" +
        "Tel :" + objc.Comp_contact + "<br/>" +
        objc.Comp_Emailid + " <br/>" +
        "www." + objc.Comp_Emailid.Split('@')[1].ToString() + "</td>" +
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
        "<tr>" +
        "<td style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>Invoice To :</td>" +
        "</tr>" +
        "<tr>" +
        "<td style='font-size:13px; font-weight:bold; font-family:Tahoma, Arial, Helvetica, sans-serif;'><strong>" + dtPax.Rows[0]["Title"] + " " + dtPax.Rows[0]["FName"] + " " + dtPax.Rows[0]["LName"] + "</strong></td>" +
        "</tr>" +
        "<tr>" +
        "<td>" + dtContact.Rows[0]["PAddress"] + " " + dtContact.Rows[0]["City"] + " " + dtContact.Rows[0]["PostCode"] + "</td>" +
        "</tr>" +
        "<tr>" +
        "<td>" + dtContact.Rows[0]["Country"] + "<br/></td>" +
        "</tr>" +
        "<tr>" +
        "<td style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>E-Mail &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp:&nbsp" + dtContact.Rows[0]["EmailID"] + "</td>" +
        "</tr>" +
        "<tr>" +
        "<td style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>Contact Number : " + dtContact.Rows[0]["MobileNo"] + "</td>" +
        "</tr>" +
        "</table></td>" +
        "<td align='left' valign='top'>&nbsp;</td>" +
        "<td align='left' valign='top' style='background:#2e4b6b; padding:10px;'><table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
        "<tr>" +
        "<td height='21' align='left' valign='top' style='color:#c5d100; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'><strong>Booking Ref</strong></td>" +

        "<td height='21' align='left' valign='top' style='color:#c5d100; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'><strong>Invoice No</strong></td>" +
        "<td align='left' valign='top' style='color:#c5d100; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'><strong>Invoice Date</strong></td>" +
        "<td align='left' valign='top' style='color:#c5d100; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'><strong>PNR</strong></td>" +
        //"<td align='left' valign='top' style='color:#c5d100; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'><strong>Airline Locator</strong></td>" +
        "<td align='left' valign='top' style='color:#c5d100; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'><strong></strong></td>" +
        "</tr>" +
        "<tr>" +
        "<td align='left' valign='top' style='color:#FFF; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>" + xp + "</td>" +
        "<td align='left' valign='top' style='color:#FFF; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>" + dtComp.Rows[0]["InvoiceNo"].ToString() + "</td>" +
        "<td align='left' valign='top' style='color:#FFF; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>" + dtComp.Rows[0]["Invoice_Date"].ToString() + "</td>" +
        "<td align='left' valign='top' style='color:#FFF; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>" + dtBD.Rows[0]["PNR"] + "</td>" +
        //"<td align='left' valign='top' style='color:#FFF; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>6FPLXS</td>" +
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
        "<table><tr>" +
        "<td width='220' align='left' valign='top' style='font-size:13px; font-weight:bold; font-family:Tahoma, Arial, Helvetica, sans-serif;'><strong>ATOL Holder</strong></td>" +
        "<td align='center' valign='top'>:</td>" +
        "<td align='left' valign='top' style='font-size:13px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>" + SupName + "</td>" +
        "<td width='220' align='right' valign='top' style='font-size:13px; font-weight:bold; font-family:Tahoma, Arial, Helvetica, sans-serif;'><strong>ATOL Number</strong></td>" +
        "<td align='center' valign='top'>:</td>" +
        "<td align='left' valign='top' style='font-size:13px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>" + SupCode + "</td>" +

        "</tr>" +
        "</table>" +
        "</td>" +
        "</tr>" +
        "<tr>" +
        "<td align='left' valign='top' style='padding-bottom:10px; padding-top:10px; font-size:13px; font-weight:600; font-family:Tahoma, Arial, Helvetica, sans-serif;'><strong>Flight Details :</strong></td>" +
        "</tr>" +
        "<tr>" +
        "<td align='left' valign='top'><table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
        "<tr>" +
        "<td align='left' valign='top' style='background:#2e4b6b; color:#FFF; padding:5px 5px; border-bottom:#000 solid 5px;'><table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
        "<tr>" +
        "<td width='200' align='left' valign='top' style='color:#FFF; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>From</td>" +
        "<td width='200' align='left' valign='top' style='color:#FFF; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>To</td>" +
        "<td width='120' align='left' valign='top' style='color:#FFF; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>Dep.Date</td>" +
        //"<td width='80' align='left' valign='top' style='color:#FFF; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>Class</td>" +
        "<td width='100' align='center' valign='top' style='color:#FFF; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>Flight No</td>" +
        "<td align='center' valign='top' style='color:#FFF; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>Dep.Time</td>" +
        "<td align='left' valign='top' style='color:#FFF; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>Arvl.Time</td>" +
        "<td align='left' valign='top' style='color:#FFF; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>Status</td>" +
        "</tr>" +
        "</table></td>" +
        "</tr>";
        foreach (DataRow drFlight in dtSector.Rows)
        {
            inv += "<tr>" +
            "<td align='left' valign='top' style='background:#e3e3e3; padding:5px 5px;'>" +
            "<table width='100%' border='0' cellspacing='0' cellpadding='0' style='margin-bottom:10px; border-bottom:#cbcbcb solid 1px; padding-bottom:10px;'>" +
            "<tr>" +
            "<td width='200' align='left' valign='top' style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>" +
            "<strong>" + drFlight["FromCityName"] + "</strong><br />" +
            "" + drFlight["FromDestName"] + " (" + drFlight["FromDest"] + ")" +
            "</td>" +
            "<td width='200' align='left' valign='top' style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>" +
            "<strong>" + drFlight["ToCityName"] + "</strong><br />" +
            drFlight["ToDestName"] + " (" + drFlight["ToDest"] + ")" +
            "</td>" +
            "<td width='120' align='left' valign='top' style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>" + Convert.ToDateTime(drFlight["FromDateTime"]).ToString("ddd, dd MMM yy") + "</td>" +
            //"<td width='80' align='left' valign='top' style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>" + drFlight["FClass"] +"</td>" +
            "<td width='100' align='center' valign='top' style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>" + drFlight["CarierName"] + drFlight["FlightNo"] + "</td>" +
            "<td align='center' valign='top' style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>" + Convert.ToDateTime(drFlight["FromDateTime"]).ToString("HH:mm") + "</td>" +
            "<td align='left' valign='top' style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>" + Convert.ToDateTime(drFlight["ToDateTime"]).ToString("HH:mm") + "</td>" +
            //"<td align='left' valign='top' style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>"+ drFlight["FStatus"].ToString() == "" ? "Confirm" : drFlight["FStatus"] + "</td>" +
            "<td align='left' valign='top' style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>Confirm</td>" +
            "</tr>" +
            "</table>" +
            "</td>" +
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
        "<td align='left' valign='top' style='padding-bottom:10px; padding-top:10px; font-size:13px; font-weight:600; font-family:Tahoma, Arial, Helvetica, sans-serif;'><strong>Passenger & Ticket Details :</strong></td>" +
        "</tr>" +
        "<tr>" +
        "<td align='left' valign='top'><table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
        "<tr>" +
        "<td align='left' valign='top' style='background:#2e4b6b; color:#FFF; padding:5px 5px; border-bottom:#000 solid 5px;'><table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
        "<tr>" +
        "<td width='25%' align='left' valign='top' style='color:#FFF; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>First Name</td>" +
        "<td width='25%'  align='left' valign='top' style='color:#FFF; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>Last Name</td>" +
        "<td width='25%' align='left' valign='top' style='color:#FFF; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>Ticket No</td>" +
        "<td align='right' valign='top' style='color:#FFF; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'></td>" +
        "</tr>" +
        "</table></td>" +
        "</tr>" +
        "<tr>" +
        "<td align='left' valign='top' style='background:#e3e3e3; padding:8px 5px;'><table width='100%' border='0' cellspacing='0' cellpadding='0' style='margin-bottom:10px; border-bottom:#cbcbcb solid 1px; padding-bottom:10px;'>" +
        "<tr>" +
        "<td align='left' valign='top'><table width='100%' border='0' cellspacing='0' cellpadding='0' style='margin-bottom:10px; padding-bottom:10px;'>";
        foreach (DataRow drPax in dtPax.Rows)
        {

            inv += "<tr>" +
            "<td width='25%' align='left' valign='top' style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif; padding-bottom:10px;'>" + drPax["Title"] + " " + drPax["FName"] + "</td>" +
            "<td width='25%'  align='left' valign='top' style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif; padding-bottom:10px;'>  " + drPax["LName"] + "</td>" +
            "<td width='25%' align='left' valign='top' style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif; padding-bottom:10px;'> " + drPax["Tickets"] + "</td>" +
            "<td align='right' valign='top' style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif; padding-bottom:10px;'></td>" +
            "</tr>";
        }
        inv += "</table></td>" +
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
        "<td align='left' valign='top' style='font-size:13px; font-weight:600; font-family:Tahoma, Arial, Helvetica, sans-serif; border-bottom:#333 solid 1px; padding-bottom:5px;'><strong style='background:#2e4b6b; color:#FFF; padding:5px 10px;'>Transaction Details :</strong></td>" +
        "</tr>" +
        "<tr>" +
        "<td>&nbsp;</td>" +
        "</tr>" +
        "<tr>" +
        "<td><table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
        "<tr>" +
        "<td height='35' align='left' valign='top' style='font-size:15px; font-family:Tahoma, Arial, Helvetica, sans-serif;'><strong>Transaction ID</strong></td>" +
        "<td height='35' align='left' valign='top' style='font-size:15px; font-family:Tahoma, Arial, Helvetica, sans-serif;'><strong>Date</strong></td>" +
        //"<td height='35' align='left' valign='top' style='font-size:15px; font-family:Tahoma, Arial, Helvetica, sans-serif;'><strong>Card Type</strong></td>" +
        //"<td height='35' align='left' valign='top' style='font-size:15px; font-family:Tahoma, Arial, Helvetica, sans-serif;'><strong>Card Holder</strong></td>" +
        "<td height='35' align='left' valign='top' style='font-size:15px; font-family:Tahoma, Arial, Helvetica, sans-serif;'><strong>Amount</strong></td>" +
        "</tr>";
        foreach (DataRow drTrans in dtTrans.Rows)
        {
            tran += "<tr>" +
            "<td height='35' align='left' valign='top' style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>" + drTrans["TrnsNo"] + "</td>" +
            "<td height='35' align='left' valign='top' style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>" + Convert.ToDateTime(drTrans["TrnsDateTime"]).ToString("dd MMM yyyy HH:mm") + "</td>" +
            //"<td height='35' align='left' valign='top' style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>" + drTrans["Card_Type"] + "</td>" +
            //"<td height='35' align='left' valign='top' style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>" + drTrans["Holder_Name"] + "</td>" +
            "<td height='35' align='left' valign='top' style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>" + drTrans["TrnsCurrencyType"] + Convert.ToDouble(drTrans["TrnsAmount"]).ToString("f2") + "</td>" +
            "</tr>";
        }
        inv += tran;
        inv += "</table></td>" +
        "</tr>" +
        "</table>" +
        "</td>" +
        #endregion
        #region
"<td width='3%'>&nbsp;</td>" +
        "<td style='background:#e3e3e3; padding:8px 5px;'><table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
        "<tr>" +
        "<td height='35' align='right' style='font-size:14px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>Sub-Total</td>" +
        "<td height='35' align='right' style='font-size:14px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>:</td>" +
        "<td height='35' align='right' style='font-size:14px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>" + Convert.ToDouble(subTotal - AtolCharges).ToString("f2") + "</td>" +
        "</tr>" +
        "<tr>" +
        "<td height='35' align='right' style='font-size:14px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>Other Charges</td>" +
        "<td height='35' align='right' style='font-size:14px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>:</td>" +
        "<td height='35' align='right' style='font-size:14px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>0.00</td>" +
        "</tr>" +
        "<tr>" +
        "<td height='35' align='right' style='font-size:14px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>ATOL Package</td>" +
        "<td height='35' align='right' style='font-size:14px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>:</td>" +
        "<td height='35' align='right' style='font-size:14px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>0.00</td>" +
        "</tr>" +
        "<tr>" +
        "<td height='35' align='right' style='font-size:14px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>ATOL Charges</td>" +
        "<td height='35' align='right' style='font-size:14px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>:</td>" +
        "<td height='35' align='right' style='font-size:14px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>" + AtolCharges.ToString("f2") + "</td>" +
        "</tr>" +
        "<tr>" +
        "<td height='35' align='right' style='border-bottom:#666 double 3px; font-size:14px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>Net Invoice Amount</td>" +
        "<td height='35' align='right' style='border-bottom:#666 double 3px; font-size:14px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>:</td>" +
        "<td height='35' align='right' style='border-bottom:#666 double 3px; font-size:15px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>" + Convert.ToDouble(subTotal).ToString("f2") + "</td>" +
        "</tr>" +
        "<tr>" +
        "<td height='35' align='right' style='border-bottom:#666 solid 1px; font-size:14px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>Paid Amount</td>" +
        "<td height='35' align='right' style='border-bottom:#666 solid 1px; font-size:14px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>:</td>" +
        "<td height='35' align='right' style='border-bottom:#666 solid 1px; font-size:15px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>" + tranTotal.ToString("f2") + "</td>" +
        "</tr>" +
        "<tr>" +
        "<td height='35' align='right' style='border-bottom:#666 double 3px; font-size:14px; font-family:Tahoma, Arial, Helvetica, sans-serif;'><strong>Due Amount</strong></td>" +
        "<td height='35' align='right' style='border-bottom:#666 double 3px; font-size:14px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>:</td>" +
        "<td height='35' align='right' style='border-bottom:#666 double 3px; font-size:15px; font-family:Tahoma, Arial, Helvetica, sans-serif;'><strong>" + Convert.ToDouble(subTotal - tranTotal).ToString("f2") + "</strong></td>" +
        "</tr>" +
        "</table></td>" +
        "</tr>" +
        "</table></td>" +
        "</tr>" +

        "<tr>" +
        "<td align='left' valign='top'>&nbsp;</td>" +
        "</tr>" +
        "<tr>" +
        "<td align='left' valign='top'></td>" +
        "</tr>" +
        "<tr>" +
        "<td align='left' valign='top' style='color:#F00; padding:10px 0px; font-size:14px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>IMPORTANT NOTICE</td>" +
        "</tr>" +
        "<tr>" +

        "<td align='left' valign='top' style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>This document is a receipt issued for your financial protection. It is not a Confirmation Invoice. Full details of your booking will be" +
        "shown on the ATOL holders Confirmation Invoice, a copy of which will be sent to you as soon as we receive it. Your booking is subject" +
        "to the ATOL holders terms and conditions.Monies are taken on behalf of the ATOL holder named above.</td> " +
        "</tr>" +

        "</table></td>" +
        "</tr>" +

        "</table></td>" +
        "</tr>" +
        "</table>";
        #endregion

        return inv;

    }
    private void generate_AtolReciept_Pdf(string html, string xp)
    {

        // create the HTML to PDF converter
        HtmlToPdf htmlToPdfConverter = new HtmlToPdf();
        //// set browser width
        htmlToPdfConverter.BrowserWidth = int.Parse("793");
        // set PDF page size and orientation
        htmlToPdfConverter.Document.FitPageWidth = false;
        htmlToPdfConverter.Document.PageSize = GetSelectedPageSize();
        htmlToPdfConverter.Document.PageOrientation = PdfPageOrientation.Portrait;

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

        System.IO.MemoryStream ms = new System.IO.MemoryStream(pdfBuffer);

        ms.Position = 0;

        System.Net.Mime.ContentType ct = new System.Net.Mime.ContentType(System.Net.Mime.MediaTypeNames.Application.Pdf);
        System.Net.Mail.Attachment attach = new System.Net.Mail.Attachment(ms, ct);
        attach.ContentDisposition.FileName = "AtolReciept.pdf";
        msg.Attachments.Add(attach);
        // SendMail("Atol Reciept", "Dear Customer", attach);


        // ms.Close();

    }


    private string generate_Atol_Certificate(string xp)

    {
        if (dtSectorMaster.Rows.Count == 0)
        {
            return "";
        }

        DataTable dtSupp = new DataTable();


        dtSupp = lot.Supplier(dtSectorMaster.Rows[0]["Ticket_IssuedBy"].ToString());
        string SupName = dtSupp.Rows[0]["SUP_Name"].ToString();
        string SupCode = dtSupp.Rows[0]["Sup_AtolNo"].ToString();
        double AtolCharges = dtPax.Rows.Count * 2.5;
        double subTotal = 0;

        foreach (DataRow drprc in dtPrice.Rows)
        {
            subTotal += (Convert.ToDouble(drprc["SellPrice"]) * Convert.ToInt32(drprc["NoOfPax"]));
        }

        CompanyDetails objc = lot.SetCompanyDetail(dtComp.Rows[0]["BookingByCompany"].ToString());

        string atol = @"<div style='background: #fffcd5; width:100%; height:100%; '><table width='1300px' border='0' align='center' cellpadding='0' cellspacing='0' style='font-family: Arial, Helvetica, sans-serif; font-size: 14px; font-weight: normal; background: #fffcd5 url(../../images/atol-logo.png) 700px -200px no-repeat;'>" +
        "<tr>" +
        "<td style='padding: 50px;'>" +
        "<table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
        "<tr>" +
        "<td align='center' valign='top' style='border: #000 solid 1px; padding: 5px 5px;'>This is an important document. Make sure that you take it with you when you travel.</td>" +
        "</tr>" +
        "<tr>" +
        "<td align='center' valign='top' style='font-size: 60px; font-weight: bold;'>ATOL Certificate</td>" +
        "</tr>" +
        "<tr>" +
        "<td align='center' valign='top' style='font-weight: bold; font-size: 18px;'>This confirms that your money is protected by the ATOL scheme<br /> if your travel company collapses.</td>" +
        "</tr>" +
        "<tr>" +
        "<td align='center' valign='top'>This certificate sets out how the ATOL scheme will protect the people<br /> named on it for the parts of their trip listed below.</td>" +
        "</tr>" +
        "<tr>" +
        "<td align='left' valign='top'>&nbsp;</td>" +
        "</tr>" +
        "<tr>" +
        "<td align='left' valign='top' style='border: #000 solid 1px; padding: 20px;'>" +
        "<table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
        "<tr>" +
        "<td>" +
        "<table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
        "<tr>" +
        "<td align='left' valign='top' style='font-weight: bold;'>Who is protected?</td>" +
        "<td align='right' valign='top' style='font-weight: bold;'>Number of passengers: [" + dtPax.Rows.Count + "]</td>" +
        "</tr>" +
        "</table>" +
        "</td>" +
        "</tr>";
        foreach (DataRow dr in dtPax.Rows)
        {

            atol += "<tr>" +
            "<td style='padding: 10px 0px;'>" + dr["Title"] + " " + dr["FName"] + " " + dr["LName"] + "</td>" +
            "</tr>";

        }


        atol += "<tr>" +
        "<td style='font-weight: bold;'>What is protected?</td>" +
        "</tr>";

        foreach (DataRow dr in dtSector.Rows)
        {
            atol += "<tr>" +
            "<td style='padding: 10px 0px; font-size: 12px;'>" + Convert.ToDateTime(dr["FromDateTime"]).ToString("dd MMM yy") + " " + dr["FromCityName"] + " (" + dr["FromDest"] + ") / " + dr["ToCityName"] + " (" + dr["ToDest"] + ") " + dr["CarierName"] + dr["FlightNo"] + "  " + dr["AirlineName"] + "</td>" +
            "</tr>";

        }


        atol += "<tr>" +
        "<td style='font-weight: bold;'>Who is protecting your flight?</td>" +
        "</tr>" +
        "<tr>" +
        "<td style='padding: 10px 0px;'>" + SupName + " ATOL NO " + SupCode + "  </td>" +
        "</tr>" +
        "<tr>" +
        "<td style='font-size: 18px; font-weight: bold;'>ATOL protected cost £ " + Convert.ToDouble(subTotal - AtolCharges).ToString("f2") + "</td>" +
        "</tr>" +
        "<tr>" +
        "<td align='center' style='font-size: 25px; font-weight: bold; padding: 10px 0px;'>Your protection</td>" +
        "</tr>" +
        "<tr>" +
        "<td>You are protected from when you were given this certificate until you return to the UK." +
        "<br />" +
        "<br />" +
        "If " + SupName + " stops trading, the passengers named above will either:<br /><br />" +

        "1. be returned to the UK; or<br />" +

        "receive a refund for the amount above (or your deposit if that is all you have paid so far).<br />" +
        "<br />" +


        "Your protection depends on the terms of the ATOL scheme (available at www.atol.org.uk)." +
        "If " + SupName + "<br />" +

        " stops trading, you must follow the instructions at <br />www.atol.org.uk(where there will be details of arrangements to bring people back to the <br/>UK, and information on how people can claim money back). <br/>Or, you can call(+44) 333 103 6350.</td>" +
        "</tr>" +
        "<tr>" +
        "<td align='center' style='font-size: 25px; font-weight: bold; padding: 10px 0px;'>Warning</td>" +
        "</tr>" +
        "<tr>" +
        "<td>This certificate only protects the above flight/s you have booked. Any other travel services <br />you booked are not protected by this certificate.</td>" +
        "</tr>" +
        "</table>" +
        " </td>" +
        "</tr>" +
        "<tr>" +
        "<td align='left' valign='top'>&nbsp;</td>" +
        "</tr>" +
        "<tr>" +
        "<td align='left' valign='top' style='border: #000 solid 1px; padding: 20px;'>By issuing this ATOL certificate, under Regulation 17 of the Civil Aviation (Air Travel Organisers’ Licensing) Regulations<br />" +
        "2012, " + objc.Comp_Name + " confirms that the flight to which it applies is sold in line with the ATOL held by < br />" +

        "" + SupName + " <br />" +

        "The <strong>ATOL</strong> scheme is run by the Civil Aviation Authority and paid for by the Air Travel Trust. To see what that is and what<br />" +

        "you can expect, together with full information on its terms and conditions, go to www.atol.org.uk.</td>" +
        "</tr>" +
        "<tr>" +
        "<td align='left' valign='top'>&nbsp;</td>" +
        "</tr>" +
        "<tr>" +
        "<td align='left' valign='top' style='border: #000 solid 1px; padding: 20px;'>" +
        "<table width='100%' border='1' cellspacing='0' cellpadding='0'>" +
        "<tr>" +
        "<td height='30' align='center' valign='middle' style='font-weight: bold;'>Unique reference number:</td>" +
        "<td height='30' align='center' valign='middle' style='font-weight: bold;'>Date of issue</td>" +
        "<td height='30' align='center' valign='middle' style='font-weight: bold;'>ATOL Certificate Issuer:</td>" +
        "<td height='30' align='center' valign='middle' style='font-weight: bold;'>ATOL number:</td>" +
        "<td rowspan='2' align='center' valign='middle' style='text-align: center; background: #808080; font-weight: bold;'>Flight Only Sale</td>" +
        "</tr>" +
        "<tr>" +
        "<td height='30' align='center' valign='middle'>" + xp + "</td>" +
        "<td height='30' align='center' valign='middle'>" + DateTime.Today.ToString("dd MMM yyyy") + "</td>" +
        "<td height='30' align='center' valign='middle'>" + objc.Comp_Name + "</td>" +
        "<td height='30' align='center' valign='middle'>" + SupCode + " </td>" +
        "</tr>" +
        "</table>" +
        "</td>" +
        "</tr>" +
        "<tr>" +
        "<td align='right' valign='top' style='padding: 10px; font-size: 12px;'>Copyright UK Civil Aviation Authority. The ATOL Logo is a registered trade mark.</td>" +
        "</tr>" +
        "<tr>" +
        "<td align='left' valign='top'>&nbsp;</td>" +
        "</tr>" +
        "</table>" +
        "</td>" +
        "</tr>" +
        "</table></div><div style='min-height: 230px; background: #fffcd5;'></div>";
        // Atol_Certificate = atol;
        return atol;

    }
    private void generate_Atol_CertificatPdf(string html, string xp)
    {
        if (html != "")
        {
            // create the HTML to PDF converter
            HtmlToPdf htmlToPdfConverter = new HtmlToPdf();


            //// set browser width
            htmlToPdfConverter.BrowserWidth = int.Parse("900");


            // set PDF page size and orientation
            htmlToPdfConverter.Document.FitPageWidth = false;
            htmlToPdfConverter.Document.PageSize = GetSelectedPageSize();
            htmlToPdfConverter.Document.PageOrientation = PdfPageOrientation.Portrait;
            htmlToPdfConverter.Document.FitPageHeight = false;


            // set PDF page margins
            htmlToPdfConverter.Document.Margins = new PdfMargins(-5);

            // set a wait time before starting the conversion
            htmlToPdfConverter.WaitBeforeConvert = 2;

            // convert HTML to PDF
            byte[] pdfBuffer = null;


            // convert HTML code
            string htmlCode = html;
            string baseUrl = "";

            // convert HTML code to a PDF memory buffer
            pdfBuffer = htmlToPdfConverter.ConvertHtmlToMemory(htmlCode, baseUrl);



            System.IO.MemoryStream ms = new System.IO.MemoryStream(pdfBuffer);

            ms.Position = 0;

            System.Net.Mime.ContentType ct = new System.Net.Mime.ContentType(System.Net.Mime.MediaTypeNames.Application.Pdf);
            System.Net.Mail.Attachment attach = new System.Net.Mail.Attachment(ms, ct);
            attach.ContentDisposition.FileName = "AtolCertificate.pdf";
            msg.Attachments.Add(attach);
            // ms.Close();
        }
        else
        {
            Response.Write("Sorry, system unable to generate ATOL certificate, please check a ticket supplier added with booking.");
        }

    }


    private string generate_Invoice(string xp)
    {
        double AtolCharges = dtPax.Rows.Count * 2.5;
        string tran = string.Empty;
        double subTotal = 0;
        double tranTotal = 0;
        string heading = string.Empty;

        if (dtComp.Rows[0]["InvoiceNo"].ToString() != "")
        { heading = "Confirmation Invoice"; }
        else
        { heading = "Booking Confirmation"; }
        if (dtBD.Rows.Count > 0)
        {
            //if (dtBD.Rows[0]["ATOL_Type"].ToString().ToLower() == "flights only/public bonded")
            //{ AtolCharges = dtPax.Rows.Count * 2.5; }
            //else { AtolCharges = 0; }
            AtolCharges = dtPax.Rows.Count * 2.5;
        }
        foreach (DataRow drprc in dtPrice.Rows)
        {
            subTotal += (Convert.ToDouble(drprc["SellPrice"]) * Convert.ToInt32(drprc["NoOfPax"]));
        }
        if (dtTrans.Rows.Count > 0)
        {
            tranTotal = Convert.ToDouble(dtTrans.Compute("Sum(TrnsAmount)", ""));
        }
        CompanyDetails objc = lot.SetCompanyDetail(dtComp.Rows[0]["BookingByCompany"].ToString());
        #region
        string inv = "<table width='1000' border='0' align='center' cellpadding='0' cellspacing='0' style='font-family:Tahoma, Arial, Helvetica, sans-serif;'>" +
        "<tr>" +
        "<td style='padding:30px;'><table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
        "<tr>" +
        "<td align='left' valign='top'><table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
        "<tr>" +
        "<td align='left' valign='top'><img src='https://www.flightsandholidays.biz/images/logoes/" + objc.Comp_logo + ".png' width='251' height='79' alt='Logo' /></td>" +
        "<td align='right' valign='top' style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>" + objc.Comp_Address + "<br />" +
        "Tel :" + objc.Comp_contact + "<br/>" +
        objc.Comp_Emailid + " <br/>" +
        "www." + objc.Comp_Emailid.Split('@')[1].ToString() + "</td>" +
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
        "<table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
        "<tr>" +
        "<td align='left' valign='top'><table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
        "<tr>" +
        "<td style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>Invoice To :</td>" +
        "</tr>" +
        "<tr>" +
        "<td style='font-size:13px; font-weight:bold; font-family:Tahoma, Arial, Helvetica, sans-serif;'><strong>" + dtPax.Rows[0]["Title"] + " " + dtPax.Rows[0]["FName"] + " " + dtPax.Rows[0]["LName"] + "</strong></td>" +
        "</tr>" +
        "<tr>" +
        "<td>" + dtContact.Rows[0]["PAddress"] + " " + dtContact.Rows[0]["City"] + " " + dtContact.Rows[0]["PostCode"] + "</td>" +
        "</tr>" +
        "<tr>" +
        "<td>" + dtContact.Rows[0]["Country"] + "<br/></td>" +
        "</tr>" +
        "<tr>" +
        "<td style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>E-Mail &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp:&nbsp" + dtContact.Rows[0]["EmailID"] + "</td>" +
        "</tr>" +
        "<tr>" +
        "<td style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>Contact Number : " + dtContact.Rows[0]["MobileNo"] + "</td>" +
        "</tr>" +
        "</table></td>" +
        "<td align='left' valign='top'>&nbsp;</td>" +
        "<td align='left' valign='top' style='background:#2e4b6b; padding:10px;'><table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
        "<tr>" +
        "<td height='21' align='left' valign='top' style='color:#c5d100; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'><strong>Booking Ref</strong></td>" +

        "<td height='21' align='left' valign='top' style='color:#c5d100; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'><strong>Invoice No</strong></td>" +
        "<td align='left' valign='top' style='color:#c5d100; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'><strong>Invoice Date</strong></td>" +
        "<td align='left' valign='top' style='color:#c5d100; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'><strong>PNR</strong></td>" +

        "<td align='left' valign='top' style='color:#c5d100; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'><strong></strong></td>" +
        "</tr>" +
        "<tr>" +
        "<td align='left' valign='top' style='color:#FFF; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>" + xp + "</td>" +
        "<td align='left' valign='top' style='color:#FFF; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>" + dtComp.Rows[0]["InvoiceNo"].ToString() + "</td>";
        string invoicedate = string.Empty;
        if (dtComp.Rows[0]["Invoice_Date"].ToString() != "")
        {
            invoicedate = Convert.ToDateTime(dtComp.Rows[0]["Invoice_Date"].ToString()).ToString("dd MMM yyyy");
        }

        inv += "<td align='left' valign='top' style='color:#FFF; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>" + invoicedate + "</td>";
        inv += "<td align='left' valign='top' style='color:#FFF; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>" + dtBD.Rows[0]["PNR"] + "</td>" +

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
        "<td align='left' valign='top' style='padding-bottom:10px; padding-top:10px; font-size:13px; font-weight:600; font-family:Tahoma, Arial, Helvetica, sans-serif;'><strong>Flight Details :</strong></td>" +
        "</tr>" +
        "<tr>" +
        "<td align='left' valign='top'><table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
        "<tr>" +
        "<td align='left' valign='top' style='background:#2e4b6b; color:#FFF; padding:5px 5px; border-bottom:#000 solid 5px;'><table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
        "<tr>" +
        "<td width='200' align='left' valign='top' style='color:#FFF; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>From</td>" +
        "<td width='200' align='left' valign='top' style='color:#FFF; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>To</td>" +
        "<td width='120' align='left' valign='top' style='color:#FFF; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>Dep.Date</td>" +
        //"<td width='80' align='left' valign='top' style='color:#FFF; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>Class</td>" +
        "<td width='100' align='center' valign='top' style='color:#FFF; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>Flight No</td>" +
        "<td align='center' valign='top' style='color:#FFF; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>Dep.Time</td>" +
        "<td align='left' valign='top' style='color:#FFF; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>Arvl.Time</td>" +
        "<td align='left' valign='top' style='color:#FFF; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>Status</td>" +
        "</tr>" +
        "</table></td>" +
        "</tr>";
        foreach (DataRow drFlight in dtSector.Rows)
        {
            inv += "<tr>" +
            "<td align='left' valign='top' style='background:#e3e3e3; padding:5px 5px;'>" +
            "<table width='100%' border='0' cellspacing='0' cellpadding='0' style='margin-bottom:10px; border-bottom:#cbcbcb solid 1px; padding-bottom:10px;'>" +
            "<tr>" +
            "<td width='200' align='left' valign='top' style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>" +
            "<strong>" + drFlight["FromCityName"] + "</strong><br />" +
            "" + drFlight["FromDestName"] + " (" + drFlight["FromDest"] + ")" +
            "</td>" +
            "<td width='200' align='left' valign='top' style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>" +
            "<strong>" + drFlight["ToCityName"] + "</strong><br />" +
            drFlight["ToDestName"] + " (" + drFlight["ToDest"] + ")" +
            "</td>" +
            "<td width='120' align='left' valign='top' style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>" + Convert.ToDateTime(drFlight["FromDateTime"]).ToString("ddd, dd MMM yy") + "</td>" +
            //"<td width='80' align='left' valign='top' style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>" + drFlight["FClass"] +"</td>" +
            "<td width='100' align='center' valign='top' style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>" + drFlight["CarierName"] + drFlight["FlightNo"] + "</td>" +
            "<td align='center' valign='top' style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>" + Convert.ToDateTime(drFlight["FromDateTime"]).ToString("HH:mm") + "</td>" +
            "<td align='left' valign='top' style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>" + Convert.ToDateTime(drFlight["ToDateTime"]).ToString("HH:mm") + "</td>" +
            //"<td align='left' valign='top' style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>"+ drFlight["FStatus"].ToString() == "" ? "Confirm" : drFlight["FStatus"] + "</td>" +
            "<td align='left' valign='top' style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>Confirm</td>" +
            "</tr>" +
            "</table>" +
            "</td>" +
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
        "<td align='left' valign='top' style='padding-bottom:10px; padding-top:10px; font-size:13px; font-weight:600; font-family:Tahoma, Arial, Helvetica, sans-serif;'><strong>Passenger & Ticket Details :</strong></td>" +
        "</tr>" +
        "<tr>" +
        "<td align='left' valign='top'><table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
        "<tr>" +
        "<td align='left' valign='top' style='background:#2e4b6b; color:#FFF; padding:5px 5px; border-bottom:#000 solid 5px;'><table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
        "<tr>" +
        "<td width='25%' align='left' valign='top' style='color:#FFF; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>First Name</td>" +
        "<td width='25%'  align='left' valign='top' style='color:#FFF; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>Last Name</td>" +
        "<td width='25%' align='left' valign='top' style='color:#FFF; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>Ticket No</td>" +
        "<td align='right' valign='top' style='color:#FFF; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'></td>" +
        "</tr>" +
        "</table></td>" +
        "</tr>" +
        "<tr>" +
        "<td align='left' valign='top' style='background:#e3e3e3; padding:8px 5px;'><table width='100%' border='0' cellspacing='0' cellpadding='0' style='margin-bottom:10px; border-bottom:#cbcbcb solid 1px; padding-bottom:10px;'>" +
        "<tr>" +
        "<td align='left' valign='top'><table width='100%' border='0' cellspacing='0' cellpadding='0' style='margin-bottom:10px; padding-bottom:10px;'>";
        foreach (DataRow drPax in dtPax.Rows)
        {

            inv += "<tr>" +
            "<td width='25%' align='left' valign='top' style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif; padding-bottom:10px;'>" + drPax["Title"] + " " + drPax["FName"] + "</td>" +
            "<td width='25%'  align='left' valign='top' style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif; padding-bottom:10px;'>  " + drPax["LName"] + "</td>" +
            "<td width='25%' align='left' valign='top' style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif; padding-bottom:10px;'> " + drPax["Tickets"] + "</td>" +
            "<td align='right' valign='top' style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif; padding-bottom:10px;'></td>" +
            "</tr>";
        }
        inv += "</table></td>" +
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
        "<td align='left' valign='top' style='font-size:13px; font-weight:600; font-family:Tahoma, Arial, Helvetica, sans-serif; border-bottom:#333 solid 1px; padding-bottom:5px;'><strong style='background:#2e4b6b; color:#FFF; padding:5px 10px;'>Transaction Details :</strong></td>" +
        "</tr>" +
        "<tr>" +
        "<td>&nbsp;</td>" +
        "</tr>" +
        "<tr>" +
        "<td><table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
        "<tr>" +
        "<td height='35' align='left' valign='top' style='font-size:15px; font-family:Tahoma, Arial, Helvetica, sans-serif;'><strong>Transaction ID</strong></td>" +
        "<td height='35' align='left' valign='top' style='font-size:15px; font-family:Tahoma, Arial, Helvetica, sans-serif;'><strong>Date</strong></td>" +
        //"<td height='35' align='left' valign='top' style='font-size:15px; font-family:Tahoma, Arial, Helvetica, sans-serif;'><strong>Card Type</strong></td>" +
        //"<td height='35' align='left' valign='top' style='font-size:15px; font-family:Tahoma, Arial, Helvetica, sans-serif;'><strong>Card Holder</strong></td>" +
        "<td height='35' align='left' valign='top' style='font-size:15px; font-family:Tahoma, Arial, Helvetica, sans-serif;'><strong>Amount</strong></td>" +
        "</tr>";
        foreach (DataRow drTrans in dtTrans.Rows)
        {
            tran += "<tr>" +
            "<td height='35' align='left' valign='top' style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>" + drTrans["TrnsNo"] + "</td>" +
            "<td height='35' align='left' valign='top' style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>" + Convert.ToDateTime(drTrans["TrnsDateTime"]).ToString("dd MMM yyyy HH:mm") + "</td>" +
            //"<td height='35' align='left' valign='top' style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>" + drTrans["Card_Type"] + "</td>" +
            //"<td height='35' align='left' valign='top' style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>" + drTrans["Holder_Name"] + "</td>" +
            "<td height='35' align='left' valign='top' style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>" + drTrans["TrnsCurrencyType"] + Convert.ToDouble(drTrans["TrnsAmount"]).ToString("f2") + "</td>" +
            "</tr>";
        }
        inv += tran;
        inv += "</table></td>" +
        "</tr>" +
        "</table>" +
        "</td>" +
        #endregion
        #region
"<td width='3%'>&nbsp;</td>" +
        "<td style='background:#e3e3e3; padding:8px 5px;'><table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
        "<tr>" +
        "<td height='35' align='right' style='font-size:14px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>Sub-Total</td>" +
        "<td height='35' align='right' style='font-size:14px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>:</td>" +
        "<td height='35' align='right' style='font-size:14px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>" + Convert.ToDouble(subTotal - AtolCharges).ToString("f2") + "</td>" +
        "</tr>" +
        "<tr>" +
        "<td height='35' align='right' style='font-size:14px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>Other Charges</td>" +
        "<td height='35' align='right' style='font-size:14px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>:</td>" +
        "<td height='35' align='right' style='font-size:14px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>0.00</td>" +
        "</tr>" +
        "<tr>" +
        "<td height='35' align='right' style='font-size:14px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>ATOL Package</td>" +
        "<td height='35' align='right' style='font-size:14px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>:</td>" +
        "<td height='35' align='right' style='font-size:14px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>0.00</td>" +
        "</tr>" +
        "<tr>" +
        "<td height='35' align='right' style='font-size:14px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>ATOL Charges</td>" +
        "<td height='35' align='right' style='font-size:14px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>:</td>" +
        "<td height='35' align='right' style='font-size:14px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>" + AtolCharges.ToString("f2") + "</td>" +
        "</tr>" +
        "<tr>" +
        "<td height='35' align='right' style='border-bottom:#666 double 3px; font-size:14px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>Net Invoice Amount</td>" +
        "<td height='35' align='right' style='border-bottom:#666 double 3px; font-size:14px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>:</td>" +
        "<td height='35' align='right' style='border-bottom:#666 double 3px; font-size:15px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>" + Convert.ToDouble(subTotal).ToString("f2") + "</td>" +
        "</tr>" +
        "<tr>" +
        "<td height='35' align='right' style='border-bottom:#666 solid 1px; font-size:14px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>Paid Amount</td>" +
        "<td height='35' align='right' style='border-bottom:#666 solid 1px; font-size:14px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>:</td>" +
        "<td height='35' align='right' style='border-bottom:#666 solid 1px; font-size:15px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>" + tranTotal.ToString("f2") + "</td>" +
        "</tr>" +
        "<tr>" +
        "<td height='35' align='right' style='border-bottom:#666 double 3px; font-size:14px; font-family:Tahoma, Arial, Helvetica, sans-serif;'><strong>Due Amount</strong></td>" +
        "<td height='35' align='right' style='border-bottom:#666 double 3px; font-size:14px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>:</td>" +
        "<td height='35' align='right' style='border-bottom:#666 double 3px; font-size:15px; font-family:Tahoma, Arial, Helvetica, sans-serif;'><strong>" + Convert.ToDouble(subTotal - tranTotal).ToString("f2") + "</strong></td>" +
        "</tr>" +
        "</table></td>" +
        "</tr>" +
        "</table></td>" +
        "</tr>" +

        "<tr>" +
        "<td align='left' valign='top'>&nbsp;</td>" +
        "</tr>" +
        "<tr>" +
        "<td align='left' valign='top'></td>" +
        "</tr>" +
        "<tr>" +
        "<td align='left' valign='top' style='color:#F00; padding:10px 0px; font-size:14px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>IMPORTANT NOTICE</td>" +
        "</tr>" +
        "<tr>" +

        "<td align='left' valign='top' style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>We would like to draw your attention to the booking conditions of the travel provider." +
        " You have informed us you have adequate travel insurance cover for this booking." +
        " Please note that it may be a legal requirement on your part to have such cover in force.</td> " +
        "</tr>" +

        "</table></td>" +
        "</tr>" +
        "<tr>" +
        "<td align='left' valign='top'><table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
        "<tr>" +
        "<td align='center' valign='top' style='border-top:#666 solid 1px; padding-top:20px; font-size:11px; font-family:Tahoma, Arial, Helvetica, sans-serif;'><strong>YOUR FINANCIAL PROTECTION</strong></td>" +
        "</tr>" +
        "<tr>" +
        "<td align='center' valign='top' style='font-size:11px; font-family:Tahoma, Arial, Helvetica, sans-serif; padding:10px 0px;'>When you buy an ATOL protected flight or flight inclusive holiday from us you will receive an ATOL certificate. This lists what is financially protected, where" +
        " you can get information on what this means for you and who to contact if things go wrong. Our ATOL number is " + objc.Comp_AtolNumber + "  for more information see our" +
        " booking teams and conditions. Cancellation rules: NON REFUNDABLE. Please read the confirmation invoice carefully.</td>" +
        "</tr>" +
        "<tr>" +
        "<td align='center' valign='top' style='font-size:11px; font-family:Tahoma, Arial, Helvetica, sans-serif;'><strong>www." + objc.Comp_Emailid.Split('@')[1].ToString() + " is a trading name of " + objc.Comp_Name + ". Company Reg:" + objc.Comp_RegNumber + "</strong></td>" +
        "</tr>" +
        "<tr>" +
        "<td align='right' valign='top' style='font-size:13px; font-weight:bold; font-family:Tahoma, Arial, Helvetica, sans-serif;'>Prepared by " + objUserDetail.userID.ToUpper() + "</td>" +
        "</tr>" +
        "</table></td>" +
        "</tr>" +
        "</table></td>" +
        "</tr>" +
        "</table>";
        #endregion

        return inv;

    }

    private void generate_Invoice_Pdf(string html, string xp)
    {

        // create the HTML to PDF converter
        HtmlToPdf htmlToPdfConverter = new HtmlToPdf();
        //// set browser width
        htmlToPdfConverter.BrowserWidth = int.Parse("793");

        // set PDF page size and orientation
        htmlToPdfConverter.Document.FitPageWidth = false;
        htmlToPdfConverter.Document.PageSize = GetSelectedPageSize();
        htmlToPdfConverter.Document.PageOrientation = PdfPageOrientation.Portrait;


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


        System.IO.MemoryStream ms = new System.IO.MemoryStream(pdfBuffer);

        ms.Position = 0;

        System.Net.Mime.ContentType ct = new System.Net.Mime.ContentType(System.Net.Mime.MediaTypeNames.Application.Pdf);
        System.Net.Mail.Attachment attach = new System.Net.Mail.Attachment(ms, ct);
        attach.ContentDisposition.FileName = "Invoice.pdf";
        msg.Attachments.Add(attach);
        // ms.Close();

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

    private void getMailBody()
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
                "Hope this mail finds you in good health ! <br />" +
                "<img src='" + BLL.WebsiteStaticData.WebsiteUrl + "images/section-shape.png' width='97' height='20' />" +
                "</td>" +
                "</tr>" +
                "<tr>" +
                "<td align='left' valign='top' style='font-size:13px; text-align:justify; font-family:Arial, Helvetica, sans-serif;'>Thank you for choosing " + etktLayout.BrandName + " as your preferred travel partner.</td>" +
                "</tr>" +
                "<tr>" +
                "<td>&nbsp;</td>" +
                "</tr>" +
                "<tr>" +
                "<td>This is in reference to your booking with the reference " + hidBookingID.Value + ", I would request you to kindly find the attached " + documents + " herewith for your expediency.</td>" +
                "</tr>" +
                "<tr>" +
                "<td>&nbsp;</td>" +
                "</tr>" +
                "<tr>" +
                "<td>Should you require any further assistance, please feel free to write an email to " + etktLayout.Email + "</td>" +
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


    #region   Rahul Ca TL 25 Nov 2020 
    //New Send Email Format applicable only for Click2Book Canada,Traveljunction Canada , FLight Trotters Canada

    private void GetMailBody_New()
    {
        CompanyDetails objc = lot.SetCompanyDetail(dtComp.Rows[0]["BookingByCompany"].ToString());
        string Http_URL = "https://www.";

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
                "<td align='center' valign='top' style='padding-bottom:50px; font-weight:bold; font-size:15px; color:#162849;'>" +
                 "KINDLY CALL BACK FOR REFERENCE NUMBER " + hidBookingID.Value +" <br />" +
                "<img src='" + BLL.WebsiteStaticData.WebsiteUrl + "images/section-shape.png' width='97' height='20' />" +
                "</td>" +
                "</tr>" +
                "<tr>" +
                "<td align='left' valign='top' style='font-size:13px; text-align:justify; font-family:Arial, Helvetica, sans-serif;'>Thank you for choosing <a target='_blank' href=" + Http_URL + etktLayout.SetCompanyDetail(dtComp.Rows[0]["BookingByCompany"].ToString()).Comp_Emailid.Split('@')[1]  +">"+ etktLayout.SetCompanyDetail(dtComp.Rows[0]["BookingByCompany"].ToString()).Comp_Emailid.Split('@')[1]  +"</a></td>" +
                "</tr>" +
                "<tr>" +
                "<td>&nbsp;</td>" +
                "</tr>" +
                "<tr>" +
                "<td style='font-family:inherit;font-size:small;'>Your flight reservation with Reference Number <b>" + hidBookingID.Value + "</b>, is confirmed but the transition is on hold since we require verifying certain information with you. We are unable to contact you on your provide phone number(s).Kindly call us immediately at "+ objc.Comp_contact + ".Your delayed response may result in change of fare and availability. <br><br> <b> NOTE: </b> We may have already charged your card, so please do not purchase this ticket elsewhere. </td>" +
                "</tr>" +
                "<tr>" +
                "<td>&nbsp;</td>" +
                "</tr>" +
                "<tr>" +
                "<td>Should you require any further assistance, please feel free to write an email to " + etktLayout.Email + "</td>" +
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

    #endregion



    private bool SendMail(string subject, string mailbody)
    {
        try
        {

            msg.From = new MailAddress(txtXPFrom.Text.Trim());
            msg.To.Add(new MailAddress(txtXPTo.Text.Trim()));
            msg.CC.Add(new MailAddress(txtXPFrom.Text.Trim()));

            msg.Subject = subject;
            msg.Body = mailbody;
            msg.IsBodyHtml = true;

            string fromAddress = txtXPFrom.Text;

            if (fromAddress.ToLower().Contains("@traveljunction.co.uk"))
            {
                SmtpClient smtp = new SmtpClient(ConfigurationManager.AppSettings["HostT"]);
                smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["PortT"]);
                smtp.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["UserNameT"], ConfigurationManager.AppSettings["PasswordT"]);
                smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSslT"]);
                smtp.Send(msg);
            }
            else if (fromAddress.ToLower().Contains("@flighttrotters.co.uk"))
            {
                SmtpClient smtp = new SmtpClient(ConfigurationManager.AppSettings["HostFTUK"]);
                smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["PortFTUK"]);
                smtp.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["UserNameFTUK"], ConfigurationManager.AppSettings["PasswordFTUK"]);
                smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSslFTUK"]);
                smtp.Send(msg);
            }
            else if (fromAddress.ToLower().Contains("@flighttrotters.ca"))
            {
                SmtpClient smtp = new SmtpClient(ConfigurationManager.AppSettings["HostFTCA"]);
                smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["PortFTCA"]);
                smtp.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["UserNameFTCA"], ConfigurationManager.AppSettings["PasswordFTCA"]);
                smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSslFTCA"]);
                smtp.Send(msg);
            }
            else if (fromAddress.ToLower().Contains("@click2book.ca"))
            {
                SmtpClient smtp = new SmtpClient(ConfigurationManager.AppSettings["HostC2BCA"]);
                smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["PortC2BCA"]);
                smtp.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["UserNameC2BCA"], ConfigurationManager.AppSettings["PasswordC2BCA"]);
                smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSslC2BCA"]);
                smtp.Send(msg);
            }
            else if (fromAddress.ToLower().Contains("@click2book.co.uk"))
            {
                SmtpClient smtp = new SmtpClient(ConfigurationManager.AppSettings["HostC2BUK"]);
                smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["PortC2BUK"]);
                smtp.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["UserNameC2BUK"], ConfigurationManager.AppSettings["PasswordC2BUK"]);
                smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSslC2BUK"]);
                smtp.Send(msg);
            }
            else if (fromAddress.ToLower().Contains("@traveljunctionus.com"))
            {
                SmtpClient smtp = new SmtpClient(ConfigurationManager.AppSettings["HostTJUS"]);
                smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["PortTJUS"]);
                smtp.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["UserNameTJUS"], ConfigurationManager.AppSettings["PasswordTJUS"]);
                smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSslTJUS"]);
                smtp.Send(msg);
            }
            else if (fromAddress.ToLower().Contains("@dial4travel.co.uk"))
            {
                SmtpClient smtp = new SmtpClient(ConfigurationManager.AppSettings["HostD4T"]);
                smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["PortD4T"]);
                smtp.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["UserNameD4T"], ConfigurationManager.AppSettings["PasswordD4T"]);
                smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSslD4T"]);
                smtp.Send(msg);
            }
            else
            {
                try
                {
                    SmtpClient smtp = new SmtpClient(ConfigurationManager.AppSettings["Host"]);
                    smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["Port"]);
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["UserName"], ConfigurationManager.AppSettings["Password"]);
                    smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]);
                    smtp.Send(msg);
                }
                catch
                {
                    SmtpClient smtp = new SmtpClient(ConfigurationManager.AppSettings["Host"]);
                    smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["Port"]);
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["UserName1"], ConfigurationManager.AppSettings["Password"]);
                    smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]);
                    smtp.Send(msg);
                }
            }

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }

    }

    protected void btnSend_Click(object sender, EventArgs e)
    {
        mailServices.DataServiceSoapClient objmail = new mailServices.DataServiceSoapClient();
        GetSetDatabase obj = new GetSetDatabase();
        string xp = hidBookingID.Value;
        BookingDetails(xp);

        //this Check for added only  C2BCA , TRVJUNCTION_CA ,FLTTROTT_CA
        //C2BCA
        //TRVJUNCTION_CA
        //FLTTROTT_CA
        var BookingByCompany = dtComp.Rows[0]["BookingByCompany"].ToString();
        if (BookingByCompany == "C2BCA" || BookingByCompany == "TRVJUNCTION_CA" || BookingByCompany == "FLTTROTT_CA")
        {
            GetMailBody_New();
        }
        else
        {
            getMailBody();
        }

        if (ckboxInvoice.Checked == false && ckboxReciept.Checked == false && ckboxCertificate.Checked == false)
        { ltrMsg.Text = "Sorry unable to send the mail because there is no check box checked. to send the mail atleast one check box must be selected."; }

        else
        {
            string remarks = "";
            if (ckboxReciept.Checked)
            {
                generate_AtolReciept_Pdf(generate_AtolReciept(xp), xp);
                remarks = ", ATOL Reciept ";
            }

            if (ckboxCertificate.Checked)
            {
                generate_Atol_CertificatPdf(generate_Atol_Certificate(xp), xp);
                remarks = remarks + ", ATOL Certificate ";
            }

            if (ckboxInvoice.Checked)
            {
                generate_Invoice_Pdf(generate_Invoice(xp), xp);
                remarks = remarks + ", Invoice ";
            }

            if (SendMail("Documents", terms))
            {
                ltrMsg.Text = "Mail sent successfully.";

                StringBuilder sb = new StringBuilder(remarks);
                sb[0] = ' ';
                remarks = sb.ToString();
                remarks = remarks.Replace(",", "");
                obj.SET_Booking_Detail(hidBookingID.Value.Trim(), "001", "", "", "", "", "", "", "", remarks + "  sent to  " + txtXPTo.Text, "", "", "", "", "", objUserDetail.userID + "/Sys", "", "", "Update", "");
            }
            else
            {
                ltrMsg.Text = "Sorry, unable to send mail.";
            }
        }

    }

    private void bindEmailIds(DataTable dtComp, DataTable dtContact)
    {
        #region From Email ID
        Layout lay = new Layout();
        if (dtComp != null)
        {
            if (dtComp.Rows.Count > 0)
            {
                hfCompany.Value = dtComp.Rows[0]["BookingByCompany"].ToString();

                CompanyDetails objc = lay.SetCompanyDetail(hfCompany.Value);
                hfCompany.Value = hfCompany.Value;
                if (hfCompany.Value.ToUpper() == "TRVJUNCTION")
                {
                    txtXPFrom.Text = "documents@traveljunction.co.uk";
                }
                else if (hfCompany.Value.ToUpper() == "DIAL4TRV")
                {
                    txtXPFrom.Text = "documents@dial4travel.co.uk";
                }
                else if (hfCompany.Value.ToUpper() == "FLTTROTT_CA")//
                {
                    txtXPFrom.Text = "documents@flighttrotters.ca";
                }
                else if (hfCompany.Value.ToUpper() == "FLTTROTT")//
                {
                    txtXPFrom.Text = "documents@flighttrotters.co.uk";
                }
                else if (hfCompany.Value.ToUpper() == "C2B")//
                {
                    txtXPFrom.Text = "document@click2book.co.uk";
                }
                hfCompany.Value = hfCompany.Value.ToUpper();
                if (!ltrMsg.Text.Contains("System"))
                    ltrMsg.Text = "";

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
            }

        }
        #endregion

    }
}