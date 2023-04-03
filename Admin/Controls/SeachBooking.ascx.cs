using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Controls_SeachBooking : System.Web.UI.UserControl
{

    public DataTable Bookings { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserDetails"] != null)
        {

            if (!IsPostBack)
            {
                UserDetail objUserDetail = Session["UserDetails"] as UserDetail;
                if (!objUserDetail.isAuth("BookingDetails.aspx"))
                {
                    Response.Redirect("Default.aspx");
                    return;
                }
                CommanBinding.BindCompanyDetails(ref ddlCompany, objUserDetail.userID);
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
            DataTable dt = objGetSetDatabase.GET_BookingDetail(txtBookingID.Text.Trim(), "", "", CommanBinding.GetCompanyCodes(ddlCompany), "",
                txtFromDate.Text.Trim(), txtToDate.Text.Trim(), ddlStatus.SelectedValue, txtPNRConfirmation.Text.Trim(), ddlSourceMedia.SelectedValue,
                "", txtPhoneNo.Text, txtMobileNo.Text.Trim(),
                txtEmailAddress.Text.Trim(), txtPaxFirstName.Text.Trim(), "", txtPaxLastName.Text.Trim(), "","");
            if (dt != null)
            {
                this.Bookings = dt;
               

            }
        }
        catch { }

    }
}