using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Admin_Payment_PayNow : System.Web.UI.Page
{
    FandHServices.FandHServicesClient objPM = new FandHServices.FandHServicesClient();

    GetSetDatabase obj = new GetSetDatabase();

    GetSetDatabase GetSetDatabase = new GetSetDatabase();
    UserDetail objUserDetail;
    DataTable dtPax = new DataTable();
    DataTable dtSector = new DataTable();
    DataTable dtComp = new DataTable();
    DataTable dtContact = new DataTable();
    DataTable dtPrice = new DataTable();
    DataTable dtTrans = new DataTable();
    DataTable dtBD = new DataTable();
    DataTable dtTransdetails = new DataTable();
    
    public double subTotal = 0;
    public double tranTotal = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        hidBookingID.Value = Request.QueryString.Get("BID"); //"XP17332";
        lblBookingRef.Text = hidBookingID.Value;
        if (Session["UserDetails"] != null)
        {
            objUserDetail = Session["UserDetails"] as UserDetail;
           

                if (!objUserDetail.isAuth("PayNow"))
                {
                    Response.Redirect("~/Admin/AccessDenied.aspx");
                    return;
                }
                else {
                    if (hidBookingID.Value.Trim() == "")
                    {
                        Response.Redirect("~/Admin/AccessDenied.aspx");
                        return;

                    }
                    else
                    {
                        BookingDetails(hidBookingID.Value);
                    }

                }
               

            
        }
        else
        {
            Response.Redirect("~/Login.aspx", false);
        }

    }
    protected void btnCard_Click(object sender, EventArgs e)
    {
        
        string TransactionID = GetSetDatabase.GenerateIDs("TRS");
        //EL.Payment.Billing bill = new EL.Payment.Billing();
        //bill.Address1 = txtAdd1.Text;
        //bill.Address2 = txtAdd2.Text;
        //bill.City = txtCity.Text;
        //bill.PostCode = txtPostCode.Text;
        //bill.FirstNames = txtFName.Text;
        //bill.Surname = txtLName.Text;
        //bill.Region = txtCity.Text;
       
        //Session["payNow"] = bill;


        

        if (ddlGateway.SelectedValue.ToUpper() == "MPT")
        {

            DataTable dt = GetSetDatabase.GET_Transaction_Master(hidBookingID.Value, TransactionID);
            if (dt == null || dt.Rows.Count == 0)
            {
                string str = GetSetDatabase.SET_Transaction_Master(hidBookingID.Value, TransactionID, ddlPayment.SelectedValue, "OK", txtAmount.Text, ddlCurrency.SelectedValue, objUserDetail.userID, System.DateTime.Now.ToString(), "Manual card charge", txtmnlTRNRef.Text.Trim(), "OK", "", "", txtmnlTRNRef.Text.Trim(), "", "", "", "", "", "", "", "Insert");
                if (str.ToLower() == "true")
                {
                    str = obj.SET_Transaction_Details(hidBookingID.Value, TransactionID, TextBox_card_number.Text, txtFName.Text.Trim()+" "+ txtLName.Text.Trim(), TextBox_Expiry.Text, TextBox_Expiry.Text, "", TextBox_cvv.Text,
                       "", selectCountry.SelectedIndex.ToString(), TextBox_state.Text, txtCity.Text, txtPostCode.Text, txtAdd1.Text, "",  "Billing", "", "Insert");

                    if (str == "true")
                    { lblMsg.Text = "Transaction save successfully."; }
                    else
                    {
                        lblMsg.Text = "Sorry, Could  not save transaction.";
                    }
                }
            }
            else
            {

                lblMsg.Text = "Sorry, Could  not save transaction because Transaction Ref already exist.";

            }

        }
        //if (ddlGateway.SelectedValue.ToLower() == "sagepay")
        //{

        //    Response.Redirect("~/admin/Payment/sagepay/SagePayServer_Reg.aspx?BookingID=" + hidBookingID.Value + "&TRNID=" + TransactionID + "&fID=" + Session.SessionID + "&Emailid=", false);

        //}
        //else if (ddlGateway.SelectedValue.ToLower() == "payzone")
        //{

        //    Response.Redirect("~/admin/Payment/payzone/PayzoneServer_Reg.aspx?BookingID=" + hidBookingID.Value + "&TRNID=" + TransactionID + "&fID=" + Session.SessionID + "&Emailid=", false);
        //}
        //else if (ddlGateway.SelectedValue.ToLower() == "emerchant")
        //{
        //    Response.Redirect("~/admin/Payment/eMerchant/eMerchantServer_Reg.aspx?BookingID=" + hidBookingID.Value + "&TRNID=" + TransactionID + "&fID=" + Session.SessionID + "&Emailid=", false);
        //}




    }
    protected void btnCash_Click(object sender, EventArgs e)
    {
        string TransactionID = GetSetDatabase.GenerateIDs("TRS");
        string str = objPM.SET_Transaction_Master(hidBookingID.Value, TransactionID, "CASH", "OK", txtCSAmount.Text, ddlCurrency.SelectedValue, objUserDetail.userID, System.DateTime.Now.ToString(), "", "", "OK", "", "", "", "", "", "", "", "", "", "", "Insert");
        if (str.ToLower() == "true")
        {
            str = objPM.SET_Transaction_Details(hidBookingID.Value, TransactionID, "", txtGivenBy.Text, "", "", "", "",
                "", "", "", "", "", "", "", "Billing", "", "Insert");
        }
    }
    protected void btnCheque_Click(object sender, EventArgs e)
    {
        string TransactionID = GetSetDatabase.GenerateIDs("TRS");
        string str = objPM.SET_Transaction_Master(hidBookingID.Value, TransactionID, "CHEQUE", ddlStatus.SelectedValue, txtCHQAmount.Text, ddlCurrency.SelectedValue, objUserDetail.userID, System.DateTime.Now.ToString(), "", "", "OK", "", "", "", "", "", "", "", "", "", "", "Insert");
        if (str.ToLower() == "true")
        {
            str = objPM.SET_Transaction_Details(hidBookingID.Value, TransactionID, "", txtFName.Text + " " + txtLName.Text, "", "", "", "",
                "", "", "", "", "", "", "", "Billing", "", "Insert");
        }
    }
    protected void btnBT_Click(object sender, EventArgs e)
    {
        string TransactionID = GetSetDatabase.GenerateIDs("TRS");
        string str = objPM.SET_Transaction_Master(hidBookingID.Value, TransactionID, "BNKTRNS", "OK", txtBTAmount.Text, ddlCurrency.SelectedValue, objUserDetail.userID, System.DateTime.Now.ToString(), "", "", "OK", "", "", "", "", "", "", "", "", "", "", "Insert");
        if (str.ToLower() == "true")
        {
            str = objPM.SET_Transaction_Details(hidBookingID.Value, TransactionID, "", txtFName.Text + " " + txtLName.Text, "", "", "", "",
                "", "", "", "", "", "", "", "Billing", "", "Insert");
        }
    }

    protected void btnmanulCard_Click(object sender, EventArgs e)
    {
        string TransactionID = GetSetDatabase.GenerateIDs("TRS");
       
        if (ddlGateway.SelectedValue.ToLower() == "MPT")
        {

            DataTable dt = GetSetDatabase.GET_Transaction_Master("", txtmnlTRNRef.Text.Trim());
            if (dt == null || dt.Rows.Count == 0)
            {
                string str = GetSetDatabase.SET_Transaction_Master(hidBookingID.Value, TransactionID, ddlPaymentgateway.Value, "OK", txtmnlAmount.Text, ddlCurrency.SelectedValue, objUserDetail.userID, System.DateTime.Now.ToString(), "Manual card charge", txtmnlTRNRef.Text.Trim(), "OK", "", "", txtmnlTRNRef.Text.Trim(), "", "", "", "", "", "", "", "Insert");
                if (str.ToLower() == "true")
                {
                    str = objPM.SET_Transaction_Details(hidBookingID.Value, TransactionID, "", txtmnlFirstName.Text.Trim(), "", "", "", "",
                        "", "", "", "", "", "", "", "Billing", "", "Insert");

                    if (str == "true")
                    { lblMsg.Text = "Transaction save successfully."; }
                    else
                    {
                        lblMsg.Text = "Sorry, Could  not save transaction.";
                    }
                }
            }
            else
            {

                lblMsg.Text = "Sorry, Could  not save transaction because Transaction Ref already exist.";

            }

        }
    }

    public void BookingDetails(string XP)
    {
        dtPax = objPM.GET_Passenger_Detail(XP, "001");
        rptrPax.DataSource = dtPax;
        rptrPax.DataBind();

        dtSector = objPM.GET_Sector_Detail(XP, "001");
        rptrSect.DataSource = dtSector;
        rptrSect.DataBind();

        dtComp = objPM.GET_Booking_Master(XP);

        dtBD = objPM.GET_Booking_Detail1(XP, "001", "", "", "", "", "", "", "", "");

        dtContact = objPM.GET_Contact_Detail(XP, "001");

        dtPrice = obj.GET_Amount_Charges_Detail(XP, "001", "", "", "", "", "", "", "");

        dtTrans = obj.GET_Transaction_Master(XP, "");

        dtTransdetails = obj.GET_Transaction_Details(XP, "");

        if (dtTransdetails.Rows.Count>0)
        {
            rptrTrans.DataSource = dtTransdetails;
            rptrTrans.DataBind();
            
        }

        foreach (DataRow drprc in dtPrice.Rows)
        {
            subTotal += (Convert.ToDouble(drprc["SellPrice"]) * Convert.ToInt32(drprc["NoOfPax"]));
        }
        if (dtTrans.Rows.Count > 0)
        {
            tranTotal = Convert.ToDouble(dtTrans.Compute("Sum(TrnsAmount)", ""));
        }

        //txtAmount.Text = (subTotal - tranTotal).ToString("f2");
        //txtmnlAmount.Text = (subTotal - tranTotal).ToString("f2");
        //txtCSAmount.Text = (subTotal - tranTotal).ToString("f2");
        //txtCHQAmount.Text = (subTotal - tranTotal).ToString("f2");
        //txtBTAmount.Text = (subTotal - tranTotal).ToString("f2");
    }

}