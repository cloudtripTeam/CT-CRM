using System;

using System.Data;
using System.Linq;

using System.Web.UI.WebControls;

public partial class Admin_SearchByTransaction : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserDetails"] != null)
        {

            if (!IsPostBack)
            {
                UserDetail objUserDetail = Session["UserDetails"] as UserDetail;
                if (!objUserDetail.isAuth("SearchByTransaction"))
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
                if (!string.IsNullOrEmpty(txtTransRef.Text.Trim()))
                {
                    dt = objGetSetDatabase.SearchByTransaction("", "", "", CommanBinding.GetCompanyCodes(ddlCompany), "",
                          "", "", bookingStatus, "", "",
                          "", "", "",
                          "", "", "", "", "", "", txtTransRef.Text.Trim());


                    


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
                else { lblmessage.Text = "Invalid Transaction ref."; }
            }
        }
        catch { }

    }
}