using mailServices;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_SendtoSupplier : System.Web.UI.Page
{
    FandHServices.FandHServicesClient obj = new FandHServices.FandHServicesClient();
    DataTable dtPax = new DataTable();
    DataTable dtSector = new DataTable();
    DataTable dtSectorMaster = new DataTable();
    DataTable dtComp = new DataTable();
    DataTable dtContact = new DataTable();
    DataTable dtBookingDtl = new DataTable();
    public string XPDetails { get; set; }
    
    public string terms { get; set; }
    Layout lo = new Layout();
    UserDetail objUserDetail;
    protected void Page_Load(object sender, EventArgs e)
    {
        objUserDetail = Session["UserDetails"] as UserDetail;
        if (Session["UserDetails"] != null)
        {
            if (!IsPostBack)
            {
                objUserDetail = Session["UserDetails"] as UserDetail;
                if (!objUserDetail.isAuth("SentToSupplier"))
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

                }
                //ddlBrand.Items.Clear();
                //if (objUserDetail.userRole.ToLower() == "agentft" || objUserDetail.userRole.ToLower() == "team head ft")
                //{
                //    ddlBrand.Items.Add(new ListItem("Flight Trotters", "FLTTROTT"));

                //}
                //else
                //{
                //    ddlBrand.Items.Add(new ListItem("Dial4travel", "DIAL4TRV"));
                //    ddlBrand.Items.Add(new ListItem("Travel Junction", "TRVJUNCTION"));
                //    ddlBrand.Items.Add(new ListItem("Flight XpertUk", "FLTXPT"));
                //    ddlBrand.Items.Add(new ListItem("Other", "OTHER"));


                //}
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx", false);
        }
    }
    
    protected void btnSearch_Click(object sender, EventArgs e)
    {

       // XPDetails=BookingDetails(txtInvoice.Text.Trim());
    }

    public string BookingDetails(string XP)
    {
        dtPax = obj.GET_Passenger_Detail(XP, "001");
        dtSector = obj.GET_Sector_Detail(XP, "001");
        dtSectorMaster = obj.GET_Sectors_Master(XP, "001");
        dtComp = obj.GET_Booking_Master(XP);
        dtContact= obj.GET_Contact_Detail(XP, "001");
        dtBookingDtl = obj.GET_Booking_Detail1(XP, "001", "", "", "", "", "", "", "", "");
        string price = txtPrice.Text == "" ? "" : "Total Price : £" + txtPrice.Text.Trim();
        txtXPFrom.Text = objUserDetail.Email;
        string note=txtNote.Text==""?"":"<strong>Notes :</strong>"+txtNote.Text;
        string PNR = dtBookingDtl.Rows.Count > 0 ? dtBookingDtl.Rows[0]["PNR"] == "" ? "" : "PNR : " + dtBookingDtl.Rows[0]["PNR"] + "" : "";
        StringBuilder sb = new StringBuilder();
        #region bind ticket supplier list
        CommanBinding.BindSupplierDetails(ref ddlSupplier, "");
        try
        {
            if (!string.IsNullOrEmpty(dtSectorMaster.Rows[0]["Ticket_IssuedBy"].ToString()))
            { ddlSupplier.SelectedValue = dtSectorMaster.Rows[0]["Ticket_IssuedBy"].ToString(); }
        }
        catch { }

        #endregion


        sb.Append("<table width='800' border='0' align='center' cellpadding='0' cellspacing='0' style='font-size: 12px; font-family: Arial, Helvetica, sans-serif; color:#000;'>" +
    "<tr>" +
        "<td style='padding: 20px; border: #e6e6e6 solid 1px;'>" +
            lo.HeaderLayout(dtComp.Rows[0]["BookingByCompany"].ToString()) +
                   " </td>" +
                " </tr>" +
                "<tr>" +
                    "<td>&nbsp;</td>" +
                " </tr>");
       
        sb.Append("<tr>" +
                    "<td>" +
                        "<table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
                            "<tr>" +
                             
                                "<td width='10' align='left' valign='middle'></td>" +
                                "<td align='left' valign='middle' style='font-size: 14px; color: #333333; padding-bottom:15px; font-weight:bold;'>Dear " + ddlSupplier.SelectedItem.Text + "," +

                                "</td>" +
                            " </tr>" +
                             "<tr>" +

                                "<td width='10' align='left' valign='middle'>&nbsp;</td>" +
                                "<td align='left' valign='middle' style='font-size: 14px; color: #333333;'>We request you to kindly issue the tickets as per the below mentioned details of our clients. We expect to get it timely as we have to provide tickets to our client and complete all the formalities prior to their date of journey.<br/><br/></td>" +
                            " </tr>" +
                        "</table>" +
                   " </td>" +
                " </tr>" +
                "<tr>" +
                    "<td>" +
                        "<table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
                            "<tr>" +
                                "<td align='left' valign='top' style='background: #e6e6e6;'>" +
                                    "<table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
                                        "<tr>" +
                                            "<td align='left' valign='top' style='background: #333333; border-bottom: #FFF solid 1px; color: #FFF; padding: 5px 10px; font-size: 12px; font-weight: bold; font-family: Arial, Helvetica, sans-serif;'>Reference Number</td>" +
                                        " </tr>" +
                                        "<tr>" +
                                            "<td align='left' valign='top' style='padding: 10px;'>" +
            // <!--passenger contact-->
                                                "<table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
                                                    "<tr>" +
                                                        "<td align='center' valign='middle' style='background: #FFF; border: #000 solid 1px; color: #F90; font-size: 18px; font-weight: bold; text-transform: uppercase; padding: 5px 10px;'>" + XP + "</td>" +
                                                        "<td width='10' align='left' valign='middle'>&nbsp;</td>" +
                                                        "<td align='center' valign='middle' style='background: #FFF; border: #000 solid 1px; color: #F90; font-size: 18px; font-weight: bold; text-transform: uppercase; padding: 5px 10px;'>" +
                                                            
                                                       PNR+"</td>" +
                                                    " </tr>" +
                                                    "<tr>" +
                                                        "<td align='left' valign='middle'>&nbsp;</td>" +
                                                        "<td width='10' align='left' valign='middle'>&nbsp;</td>" +
                                                        "<td align='left' valign='middle'>&nbsp;</td>" +
                                                    " </tr>" +
                                                    "<tr>" +
                                                        "<td align='left' valign='middle'>Name</td>" +
                                                        "<td width='10' align='left' valign='middle'>:</td>" +
                                                        "<td align='left' valign='middle'>" + dtPax.Rows[0]["Title"] + " " + dtPax.Rows[0]["FName"] + " " + dtPax.Rows[0]["LName"] + "</td>" +
                                                    " </tr>" +
                                                    "<tr>" +
                                                        "<td align='left' valign='middle'>Booking Date</td>" +
                                                        "<td align='left' valign='middle'>:</td>" +
                                                        "<td align='left' valign='middle'>" + DateTime.Today.ToString("dd MMM yyyy") + "</td>" +
                                                    " </tr>" +

                                                "</table>" +
                                           " </td>" +
                                        " </tr>" +
                                    "</table>" +
                               " </td>" +
                                "<td width='10' align='left' valign='top'>&nbsp;</td>" +
                                "<td align='left' valign='top' style='background: #e6e6e6;'>" +

                                    "<table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
                                        "<tr>" +
                                            "<td style='background: #ED8323; border-bottom: #FFF solid 1px; color: #FFF; padding: 5px 10px; font-size: 12px; font-weight: bold; font-family: Arial, Helvetica, sans-serif;'>Name of Passenger(s)</td>" +
                                        " </tr>" +
                                        "<tr>" +
                                            "<td style='padding: 10px;'>" +
                                                "<table width='100%' border='0' cellspacing='0' cellpadding='0'>");
        //Passenegr details
        foreach (DataRow drPax in dtPax.Rows)
        {
            sb.Append("<tr>" +
                        "<td height='30' align='left' valign='middle'>" +
                          "<img src='" + BLL.WebsiteStaticData.WebsiteUrl + "images/pax.png' width='24' height='24' />" +
                            " </td>" +
                             "<td width='5' height='30' align='left' valign='middle'>&nbsp;</td>" +
                             "<td height='30' align='left' valign='middle'>" + drPax["Title"] + " " + drPax["FName"] + " " + drPax["MName"] + " " + drPax["LName"] + "</td>" +
                               " </tr>");

        }


                    sb.Append("</table>" +
                      " </td>" +
                   " </tr>" +
               "</table>" +
            " </td>" +
            " </tr>" +
            "</table>" +
            " </td>" +
            " </tr>" +
            "<tr>" +
            "<td>&nbsp;</td>" +
            " </tr>" +
            "<tr>" +
            "<td>" +
            "<table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
            "<tr>" + 
            "<td width='32' align='left' valign='middle'>" +
               "<img src='" + BLL.WebsiteStaticData.WebsiteUrl + "images/airplane-ico.png' style='width:32px; height:32px;' />" +
            " </td>" +
            "<td width='10' align='left' valign='middle'>&nbsp;</td>" +
            "<td align='left' valign='middle' style='font-size: 18px; color: #333333; font-weight: bold;'>Your flight itinerary</td>" +
            "<td width='250' align='center' valign='middle' style='background: #FFF; border: #000 solid 0px; color: #F90; font-size: 18px; font-weight: bold; text-transform: uppercase; padding: 5px 10px;'>"+
            price+
            "</td>" +
            " </tr>" +
            "</table>" +
            " </td>" +
            " </tr>" +
            "<tr>" +
            "<td style='background: #eeeeee;'>" +
                        // <!--Itinerary Details-->
            "<table class='table table-bordered' width='100%' cellpadding='5'>" +
            "<thead>" +
            "<tr>" +
              " <th align='left' valign='middle' style='background: #ED8323'>Date</th>" +
               "<th align='left' valign='middle' style='background: #ED8323'>Flight Number</th>" +
               "<th align='left' valign='middle' style='background: #ED8323'>Departing</th>" +
               "<th align='left' valign='middle' style='background: #ED8323'>Arriving</th>" +
            " </tr>" +
            "</thead>" +
            "<tbody>");
        foreach (DataRow drFlight in dtSector.Rows)
        {
            sb.Append("<tr>" +
                  "<td align='left' valign='top' style='border-right: #FFF solid 1px; border-bottom: #FFF solid 1px;'>" +
                      "<strong>" + Convert.ToDateTime(drFlight["FromDateTime"]).ToString("ddd, dd MMM yyyyy") + "</strong><br />" +
                 " </td>" +
                  "<td align='left' valign='top' style='border-right: #FFF solid 1px; border-bottom: #FFF solid 1px;'>" +
                      "<p>" + drFlight["CarierName"]+drFlight["FlightNo"] + "</p>" +
                      drFlight["AirlineName"] +
                 " </td>" +
                  "<td align='left' valign='top' style='border-right: #FFF solid 1px; border-bottom: #FFF solid 1px;'>" +
                      "<strong>" + Convert.ToDateTime(drFlight["FromDateTime"]).ToString("H:mm") +' '+drFlight["FromDest"] + "</strong>" +
                      "<p>" +

                           drFlight["FromCityName"] +

                      "</p>" +
                 " </td>" +
                  "<td align='left' valign='top' style='border-bottom: #FFF solid 1px;'>" +
                      "<strong>" + Convert.ToDateTime(drFlight["ToDateTime"]).ToString("H:mm") + ' ' + drFlight["ToDest"] + "</strong>" +
                      "<p>" +

                          drFlight["ToCityName"] +

                      "</p>" +
                 " </td>" +
              " </tr>");
        }

                sb.Append("</tbody>" +
               "</table>" +
          " </td>" +
        " </tr>" +
        "<tr>" +
           "<td style='background: #eeeeee;'>");
                    
                sb.Append("<tr>" +
                    "<td>&nbsp;</td>" +
                " </tr>" +
                "<tr style='border-bottom: 1pt solid black;'>" +
                    "<td></td>" +
                " </tr>" +
               "<tr>" +
                    "<td>" + note + "<br/><br/><br/></td>" +
                " </tr>" +
                "<tr style='border-bottom: 1pt solid black;'>" +
                    "<td></td>" +
                " </tr>" +
                "<tr>" +
                    "<td><br/></td>" +
                " </tr>" +
                "<tr>" +
                    "<td><br/></td>" +
                " </tr>" +
                "<tr>" +
                    "<td style='padding: 10px 0px;'>" +
                        lo.xpFooter +
                            " </tr>" +
                        "</table>" +
                   " </td>" +
                " </tr>" +
            "</table>" +
        " </td>" +
        " </tr>" +
        "</table>");

                dvXP.Visible = true;
                return sb.ToString();


    }
    protected void btnSend_Click(object sender, EventArgs e)
    {

        XPDetails = BookingDetails(hidBookingID.Value.Trim());
        mailServices.DataServiceSoapClient objmail = new DataServiceSoapClient();
        if (objmail.Sendcustomermail(txtXPFrom.Text.Trim(), txtXPTo.Text.Trim(), "Please Issue the Ticket(s) " + txtXPTo.Text.Trim(), XPDetails, "", "javed@dial4travel.co.uk") == true)
        {
            ltrMsg.Text = "Mail Sent to Supplier";
            GetSetDatabase objGetSetDatabase = new GetSetDatabase();
            objGetSetDatabase.SET_Booking_Detail(hidBookingID.Value.Trim(), "001", "", "", "", "", "", "", "", "Mail sent to supplier to isssue the tickets ", "", "", "", "", "", objUserDetail.userID, "", "", "Update","");

        }
        else {
            ltrMsg.Text = "Sorry, unable to send mail to Supplier";
        }
    }
    protected void btnSendXP_Click(object sender, EventArgs e)
    {
        
    }
}
