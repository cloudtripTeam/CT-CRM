using System;
using System.Data;
using System.Web.UI.WebControls;

public partial class Admin_editCallDetails : System.Web.UI.Page
{
    readonly GetSetDatabase objDl = new GetSetDatabase();
    UserDetail objUserDetail;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserDetails"] != null)
        {
            objUserDetail = Session["UserDetails"] as UserDetail;
            if (!IsPostBack)
            {
                objUserDetail = Session["UserDetails"] as UserDetail;
                if (!objUserDetail.isAuth("Lead"))
                {
                    Response.Redirect("~/Admin/AccessDenied.aspx");
                    return;
                }

                ddlBrand.Items.Clear();
                CommanBinding.BindCompanyDetails(ref ddlBrand, objUserDetail.userID);
                if (Request.QueryString["CID"] != null)
                {
                    hfID.Value = Request.QueryString["CID"].ToString();
                    hfUpdatedBy.Value = objUserDetail.userID;
                    DataTable dt = objDl.GET_Call_Details(Request.QueryString["CID"].ToString(), "", "", "", "", "", "", "", "", CommanBinding.GetCompanyCodes(ddlBrand), "", "");
                    if (dt != null && dt.Rows.Count > 0)
                    {

                        lblRef.Text = dt.Rows[0]["Call_Ref"].ToString();
                        lblAgent.Text = "Agent - " + dt.Rows[0]["Agent_Name"].ToString();
                        ddlSource.SelectedValue = dt.Rows[0]["Call_Source"].ToString();
                        ddlBrand.SelectedValue = dt.Rows[0]["Brand_Name"].ToString();

                        ddlSourceMedia.Items.Clear();
                        ddlSourceMedia.Items.Insert(0, new ListItem("Select Campaign", ""));
                        if (ddlBrand.SelectedIndex != 0)
                        {
                            CommanBinding.BindCampaignDetails(ref ddlSourceMedia, ddlBrand.SelectedValue);
                        }

                        ddlSourceMedia.SelectedValue = dt.Rows[0]["Source_Media"].ToString();
                        txtOrigin.Value = dt.Rows[0]["Origin"].ToString();
                        txtDestination.Value = dt.Rows[0]["Destination"].ToString();
                        txtContact.Value = dt.Rows[0]["Contact_Number"].ToString();
                        txtEmail.Value = dt.Rows[0]["Email_Address"].ToString();
                        txtOBDate.Value = dt.Rows[0]["Outbound_Date"].ToString();
                        txtIBDate.Value = dt.Rows[0]["Return_Date"].ToString();
                        txtPaxName.Value = dt.Rows[0]["Pax_Name"].ToString();
                        txtAdults.Value = dt.Rows[0]["Adults"].ToString();
                        txtChilds.Value = dt.Rows[0]["Childs"].ToString();
                        Infants.Value = dt.Rows[0]["Infants"].ToString();
                        txtAirline.Value = dt.Rows[0]["Airline"].ToString();
                        ddlStatus.SelectedValue = dt.Rows[0]["Status"].ToString();
                        ddlReason.SelectedValue = dt.Rows[0]["Reason_of_Call"].ToString();
                        bookingID.Value = dt.Rows[0]["BookingId"].ToString();
                        txtRemarks.Value = dt.Rows[0]["Remarks"].ToString();
                        ddlSourceMedia.SelectedValue = dt.Rows[0]["Source_Media"].ToString();  
                    }
                    else
                    {
                        //record not found
                    }
                }
            }
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        bool result = false;
        result = objDl.SET_Call_Details(hfID.Value, "UPDATE", hfUpdatedBy.Value, ddlSource.SelectedValue, ddlBrand.SelectedValue, txtContact.Value, txtPaxName.Value, txtEmail.Value, txtOrigin.Value, txtDestination.Value, txtOBDate.Value, txtIBDate.Value, txtAirline.Value, ddlReason.SelectedValue, ddlStatus.SelectedValue, txtRemarks.Value, txtAdults.Value, txtChilds.Value, Infants.Value,bookingID.Value,ddlSourceMedia.SelectedValue);
        if (result)
        {
            lblMsg.Text = "Record updated successfully";
        }
        else
        {
            lblMsg.Text = "Sorry unbale to update the record";
        }
    }

    protected void ddlBrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSourceMedia.Items.Clear();
        ddlSourceMedia.Items.Insert(0, new ListItem("Select Campaign", ""));
        if (ddlBrand.SelectedIndex != 0)
        {
            CommanBinding.BindCampaignDetails(ref ddlSourceMedia, ddlBrand.SelectedValue);
        }
    }
}