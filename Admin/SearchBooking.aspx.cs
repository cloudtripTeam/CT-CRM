using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_SearchBooking : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserDetails"] != null)
        {
            if (!IsPostBack)
            {
                UserDetail objUserDetail = Session["UserDetails"] as UserDetail;
                if (!objUserDetail.isAuth("SearchBooking.aspx"))
                {
                    Response.Redirect("~/Admin/AccessDenied.aspx");
                    return;
                }
                CommanBinding.BindCompanyDetails(ref ddlCompany, objUserDetail.userID);
                hduser.Value = objUserDetail.userID;
               
                
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
           

            if (objUserDetail != null)
            {
                string role = objUserDetail.userRole.ToLower();
                
                dt = objGetSetDatabase.GET_BookingDetail(txtBookingID.Text.Trim(), "","", CommanBinding.GetCompanyCodes(ddlCompany), "",
                      "", "", bookingStatus, txtPNRConfirmation.Text.Trim(), "",
                      "", txtPhoneNo.Text, txtMobileNo.Text.Trim(),
                      txtEmailAddress.Text.Trim(), txtPaxFirstName.Text.Trim(), "", txtPaxLastName.Text.Trim(), "", "");
                

               

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