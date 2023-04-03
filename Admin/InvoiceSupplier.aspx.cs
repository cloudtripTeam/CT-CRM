using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HiQPdf;

public partial class Admin_InvoiceSupplier : System.Web.UI.Page
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
                    if (dtComp.Rows[0]["BookingByCompany"].ToString() == "FLTTROTT_CA")
                        generate_Invoice_Pdf(generate_InvoiceJAZ(xp), xp);
                    else
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
        dtContact = obj.GET_Contact_Detail(XP, "001");
        dtPrice = obj.GET_Amount_Charges_Detail(XP, "001", "", "", "", "", "", "", "");
        dtTrans = obj.GET_Transaction_Master(XP, "");
        obj.GET_Transaction_Details(XP, "");
    }
    private string generate_Invoice(string xp)
    {
        double AtolCharges = dtPax.Rows.Count * 2.5;
        string sectorStatus = "Confirm";
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

        if (dtBD.Rows[0]["BookingStatus"].ToString().ToLower() == "refund" || dtBD.Rows[0]["BookingStatus"].ToString().ToLower() == "cancelled")
        {
            heading = "Cancellation Invoice";
            subTotal = 0.00;
            AtolCharges = 0.00;
            tranTotal = 0.00;
            sectorStatus = "Cancelled";

        }
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
                              "<td align='left' valign='top' style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>" + sectorStatus + "</td>" +
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
        if (dtBD.Rows[0]["BookingStatus"].ToString().ToLower() != "refund" && dtBD.Rows[0]["BookingStatus"].ToString().ToLower() != "cancelled")
        {
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
    private string generate_InvoiceJAZ(string xp)
    {
        double AtolCharges = dtPax.Rows.Count * 2.5;
        string sectorStatus = "Confirm";
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

        if (dtBD.Rows[0]["BookingStatus"].ToString().ToLower() == "refund" || dtBD.Rows[0]["BookingStatus"].ToString().ToLower() == "cancelled")
        {
            heading = "Cancellation Invoice";
            subTotal = 0.00;
            AtolCharges = 0.00;
            tranTotal = 0.00;
            sectorStatus = "Cancelled";

        }
        #region
        string inv = "<table width='1000' border='0' align='center' cellpadding='0' cellspacing='0' style='font-family:Tahoma, Arial, Helvetica, sans-serif;'>" +
  "<tr>" +
    "<td style='padding:30px;'><table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
        "<tr>" +
          "<td align='left' valign='top'><table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
              "<tr>" +
                "<td align='left' valign='top'><img src='../../images/logoes/jlogo.png' width='380' height='56' alt='Logo' /></td>" +
                "<td align='right' valign='top' style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>48 Education Rd, Brampton, Ontario, Canada<br />" +
                  "Tel :(437) 388-8706<br/>info@jaztravls.com<br/>" +
                  "www.jaztravels.com</td>" +
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
                            "<td style='font-size:13px; font-weight:bold; font-family:Tahoma, Arial, Helvetica, sans-serif;'><strong>Flighttrotters</strong></td>" +
                          "</tr>" +
                          "<tr>" +
                            "<td>Profile West 950 Great West Road Brentford, Middlesex</ td>" +
                          "</tr>" +
                          "<tr>" +
                            "<td>TW8 9ES<br/></td>" +
                          "</tr>" +
                           "<tr>" +
                            "<td style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>E-Mail &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp:&nbsp;info@flighttrotters.ca</ td>" +
                          "</tr>" +
                           "<tr>" +
                            "<td style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>Contact Number : 1-844-573-4079</ td>" +
                          "</tr>" +
                        "</table></td>" +
                      "<td align='left' valign='top'>&nbsp;</td>" +
                      "<td align='left' valign='top' style='background:#d40046; padding:10px;'><table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
                          "<tr>" +
                          "<td height='21' align='left' valign='top' style='color:#c5d100; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'><strong></strong></td>" +

                            "<td height='21' align='left' valign='top' style='color:#c5d100; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'><strong></strong></td>" +
                            "<td align='left' valign='top' style='color:#c5d100; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'><strong>Invoice Date</strong></td>" +
                            "<td align='left' valign='top' style='color:#c5d100; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'><strong>PNR</strong></td>" +

                            "<td align='left' valign='top' style='color:#c5d100; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'><strong></strong></td>" +
                          "</tr>" +
        "<tr>" +
                          "<td align='left' valign='top' style='color:#FFF; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'></td>" +
                            "<td align='left' valign='top' style='color:#FFF; font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'></td>";
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
                      "<td align='left' valign='top' style='background:#d40046; color:#FFF; padding:5px 5px; border-bottom:#000 solid 5px;'><table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
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
                              "<td align='left' valign='top' style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>" + sectorStatus + "</td>" +
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
                      "<td align='left' valign='top' style='background:#d40046; color:#FFF; padding:5px 5px; border-bottom:#000 solid 5px;'><table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
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
    "<td>&nbsp;</td>" +
  "</tr>" +
  "<tr>" +
    "<td>" +
            //"<table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
            //"<tr>" +
            //  "<td height='35' align='left' valign='top' style='font-size:15px; font-family:Tahoma, Arial, Helvetica, sans-serif;'><strong>Transaction ID</strong></td>" +
            //  "<td height='35' align='left' valign='top' style='font-size:15px; font-family:Tahoma, Arial, Helvetica, sans-serif;'><strong>Date</strong></td>" +
            //  //"<td height='35' align='left' valign='top' style='font-size:15px; font-family:Tahoma, Arial, Helvetica, sans-serif;'><strong>Card Type</strong></td>" +
            //  //"<td height='35' align='left' valign='top' style='font-size:15px; font-family:Tahoma, Arial, Helvetica, sans-serif;'><strong>Card Holder</strong></td>" +
            //  "<td height='35' align='left' valign='top' style='font-size:15px; font-family:Tahoma, Arial, Helvetica, sans-serif;'><strong>Amount</strong></td>" +
            //"</tr>";
            //if (dtBD.Rows[0]["BookingStatus"].ToString().ToLower() != "refund" && dtBD.Rows[0]["BookingStatus"].ToString().ToLower() != "cancelled")
            //{
            //    foreach (DataRow drTrans in dtTrans.Rows)
            //    {
            //        tran += "<tr>" +
            //      "<td height='35' align='left' valign='top' style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>" + drTrans["TrnsNo"] + "</td>" +
            //         "<td height='35' align='left' valign='top' style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>" + Convert.ToDateTime(drTrans["TrnsDateTime"]).ToString("dd MMM yyyy HH:mm") + "</td>" +
            //         //"<td height='35' align='left' valign='top' style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>" + drTrans["Card_Type"] + "</td>" +
            //         //"<td height='35' align='left' valign='top' style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>" + drTrans["Holder_Name"] + "</td>" +
            //         "<td height='35' align='left' valign='top' style='font-size:12px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>" + drTrans["TrnsCurrencyType"] + Convert.ToDouble(drTrans["TrnsAmount"]).ToString("f2") + "</td>" +
            //       "</tr>";
            //    }
            //}
            //inv += tran;
            //inv += "</table>" +
            "</td>" +
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
                               "<td height='35' align='right' style='font-size:14px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>" + Request.QueryString["CMN"] + "</td>" +
                             "</tr>" +

                             "<tr>" +
                               "<td height='35' align='right' style='border-bottom:#666 double 3px; font-size:14px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>Net Invoice Amount</td>" +
                               "<td height='35' align='right' style='border-bottom:#666 double 3px; font-size:14px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>:</td>" +
                               "<td height='35' align='right' style='border-bottom:#666 double 3px; font-size:15px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>" + Request.QueryString["CMN"] + "</td>" +
                             "</tr>" +
                             "<tr>" +
                               "<td height='35' align='right' style='border-bottom:#666 solid 1px; font-size:14px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>Paid Amount</td>" +
                               "<td height='35' align='right' style='border-bottom:#666 solid 1px; font-size:14px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>:</td>" +
                               "<td height='35' align='right' style='border-bottom:#666 solid 1px; font-size:15px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>" + Request.QueryString["CMN"] + "</td>" +
                             "</tr>" +
                             "<tr>" +
                               "<td height='35' align='right' style='border-bottom:#666 double 3px; font-size:14px; font-family:Tahoma, Arial, Helvetica, sans-serif;'><strong>Due Amount</strong></td>" +
                               "<td height='35' align='right' style='border-bottom:#666 double 3px; font-size:14px; font-family:Tahoma, Arial, Helvetica, sans-serif;'>:</td>" +
                               "<td height='35' align='right' style='border-bottom:#666 double 3px; font-size:15px; font-family:Tahoma, Arial, Helvetica, sans-serif;'><strong>0.00</strong></td>" +
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
   //      "<tr>" +
   //        "<td align='left' valign='top'><table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
   //            "<tr>" +
   //              "<td align='center' valign='top' style='border-top:#666 solid 1px; padding-top:20px; font-size:11px; font-family:Tahoma, Arial, Helvetica, sans-serif;'><strong>YOUR FINANCIAL PROTECTION</strong></td>" +
   //            "</tr>" +
   //            "<tr>" +
   //              "<td align='center' valign='top' style='font-size:11px; font-family:Tahoma, Arial, Helvetica, sans-serif; padding:10px 0px;'>When you buy an ATOL protected flight or flight inclusive holiday from us you will receive an ATOL certificate. This lists what is financially protected, where" +
   //                " you can get information on what this means for you and who to contact if things go wrong. Our ATOL number is " + objc.Comp_AtolNumber + "  for more information see our" +
   //                " booking teams and conditions. Cancellation rules: NON REFUNDABLE. Please read the confirmation invoice carefully.</td>" +
   //            "</tr>" +
   //            "<tr>" +
   //              "<td align='center' valign='top' style='font-size:11px; font-family:Tahoma, Arial, Helvetica, sans-serif;'><strong>www." + objc.Comp_Emailid.Split('@')[1].ToString() + " is a trading name of " + objc.Comp_Name + ". Company Reg:" + objc.Comp_RegNumber + "</strong></td>" +
   //            "</tr>" +
   //            "<tr>" +
   //              "<td align='right' valign='top' style='font-size:13px; font-weight:bold; font-family:Tahoma, Arial, Helvetica, sans-serif;'>Prepared by " + objUserDetail.userID.ToUpper() + "</td>" +
   //            "</tr>" +
   //          "</table></td>" +
   //      "</tr>" +
   //    "</table></td>" +
   //"</tr>" +
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