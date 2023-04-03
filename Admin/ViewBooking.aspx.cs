using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_ViewBooking : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserDetails"] != null)
        {

            if (!IsPostBack)
            {
                UserDetail objUserDetail = Session["UserDetails"] as UserDetail;
                if (!objUserDetail.isAuth("BookingDetails.aspx"))
                {
                    Response.Redirect("~/Admin/AccessDenied.aspx");
                    return;
                }
                CommanBinding.BindCompanyDetails(ref ddlCompany, objUserDetail.userID);
                hduser.Value = objUserDetail.userID;
                txtFromDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                SearchBookingDetails();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx", false);
        }
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        SearchBookingDetails();
    }
    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSourceMedia.Items.Clear();
        ddlSourceMedia.Items.Insert(0, new ListItem("Select Campaign", ""));
        if (ddlCompany.SelectedIndex != 0)
        {
            CommanBinding.BindCampaignDetails(ref ddlSourceMedia, ddlCompany.SelectedValue);
        }
    }
    public void SearchBookingDetails()
    {
        try
        {
            GetSetDatabase objGetSetDatabase = new GetSetDatabase();
            DataTable dt = null; ;
            UserDetail objUserDetail = Session["UserDetails"] as UserDetail;
            string bookingStatus = string.Empty;
            string bookingMultiStatus = string.Empty;
            int count = 0;
            foreach (ListItem item in ddlBookingStatus.Items)
            {
                if (item.Selected)
                {
                    count++;
                    if (count == 1)
                    {
                        bookingStatus = item.Value;
                        bookingMultiStatus = "BookingStatus = '" + item.Value + "'";
                    }
                    else
                    {
                        bookingStatus = string.Empty;
                        bookingMultiStatus += " OR BookingStatus = '" + item.Value + "'";
                    }
                }
            }

            if (objUserDetail != null)
            {
                string role = objUserDetail.userRole.ToLower();
                //if (role != "superadmin" && role != "admin" && role != "team head" && role != "onlinetl" && role != "team head ft" && role != "fares" && role != "fareft" && role != "customer care" && role != "marketing head" && role != "online")
                //{
                //    dt = objGetSetDatabase.GET_BookingDetail(txtBookingID.Text.Trim(), "", "", CommanBinding.GetCompanyCodes(ddlCompany), objUserDetail.userID,
                //           txtFromDate.Text.Trim(), txtToDate.Text.Trim(), bookingStatus, txtPNRConfirmation.Text.Trim(), ddlSourceMedia.SelectedValue,
                //           "", txtPhoneNo.Text, txtMobileNo.Text.Trim(),
                //           txtEmailAddress.Text.Trim(), txtPaxFirstName.Text.Trim(), "", txtPaxLastName.Text.Trim(), "", txtSupplierRef.Text);

                //}

                //else
                //{
                    dt = objGetSetDatabase.GET_BookingDetail(txtBookingID.Text.Trim(), "", "", CommanBinding.GetCompanyCodes(ddlCompany), "",
                          txtFromDate.Text.Trim(), txtToDate.Text.Trim(), bookingStatus, txtPNRConfirmation.Text.Trim(), ddlSourceMedia.SelectedValue,
                          "", txtPhoneNo.Text, txtMobileNo.Text.Trim(),
                          txtEmailAddress.Text.Trim(), txtPaxFirstName.Text.Trim(), "", txtPaxLastName.Text.Trim(), "", txtSupplierRef.Text);
                //}

                if (count > 1)
                {

                    dt = dt.Select(bookingMultiStatus).CopyToDataTable();
                }


                //fare team can only view online bookings
                if (role == "fares")
                {
                    var result1 = from r in dt.AsEnumerable()
                                  where
                                       r.Field<string>("BookingByType") == "DICT" || r.Field<string>("BookingBy").ToLower() == objUserDetail.userID.ToLower()
                                  select r;
                    if (result1.Count() > 0)
                    {
                        dt = result1.CopyToDataTable();
                    }
                    else
                        dt = null;

                }

                else if (ddlBookingType.SelectedValue != "")
                {

                    var result1 = from r in dt.AsEnumerable()
                                  where
                                       r.Field<string>("BookingByType") == ddlBookingType.SelectedValue
                                  select r;
                    if (result1.Count() > 0)
                    {
                        dt = result1.CopyToDataTable();
                    }
                    else
                        dt = null;

                }
                #region fileter Payment Type
                try
                {
                    if (ddlPaymentType.SelectedValue == "Full")
                    {
                        var result1 = from r in dt.AsEnumerable()
                                      where
                                          r.Field<decimal>("Sell_Price") <= r.Field<decimal>("Trns_Amount")
                                      select r;
                        if (result1.Count() > 0)
                        {
                            dt = result1.CopyToDataTable();
                        }
                        else
                            dt = null;
                    }
                    else if (ddlPaymentType.SelectedValue == "Partial")
                    {

                        var result1 = from r in dt.AsEnumerable()
                                      where
                                          r.Field<decimal>("Sell_Price") > r.Field<decimal>("Trns_Amount")
                                      select r;
                        if (result1.Count() > 0)
                        {
                            dt = result1.CopyToDataTable();
                        }
                        else
                            dt = null;
                    }
                    else if (ddlPaymentType.SelectedValue == "NoPayment")
                    {

                        var result1 = from r in dt.AsEnumerable()
                                      where
                                           r.Field<decimal>("Trns_Amount") == 0
                                      select r;
                        if (result1.Count() > 0)
                        {
                            dt = result1.CopyToDataTable();
                        }
                        else
                            dt = null;
                    }
                }
                catch (Exception ex)
                { }

                #endregion


                if (dt != null)
                {
                    rptrDetails.DataSource = dt;
                    rptrDetails.DataBind();
                    lblmessage.Text = "";
                }
                else
                {
                    lblmessage.Text = "No record found.";


                }
            }
        }
        catch { }

    }
}