using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_MyProfile : System.Web.UI.Page
{

    GetSetDatabase objGetSetDatabase = new GetSetDatabase();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserDetails"] != null)
        {
            if (!IsPostBack)
            {
                UserDetail objUserDetail = Session["UserDetails"] as UserDetail;
                if (!objUserDetail.isAuth("MyProfile"))
                {
                    Response.Redirect("~/Admin/AccessDenied.aspx");
                    return;
                }
                else {
                    GetProfile(objUserDetail.userID);
                }
                


            }
        }
        else
        {
            Response.Redirect("~/Login.aspx", false);
        }
    }

    private void GetProfile(string userID)
    {


        DataTable dt = objGetSetDatabase.GET_UserAccount("", userID, "", "", "", "", "");
        DataTable dt2 = objGetSetDatabase.GET_UserAccountDetails("", userID);
        if (dt != null && dt2 != null)
        {
            if (dt.Rows.Count > 0)
            {
                txtUserID.Text = dt.Rows[0]["UserID"].ToString();
                txtFName.Text = dt.Rows[0]["UserFirstName"].ToString();
                txtLName.Text = dt.Rows[0]["UserLastName"].ToString();
                ddlTitle.SelectedValue = dt.Rows[0]["UserTitle"].ToString();
                txtUserPassword.Text = dt.Rows[0]["UserPassword"].ToString();
                if (!dt.Rows[0]["UserRole"].ToString().Equals("superadmin", StringComparison.OrdinalIgnoreCase))
                    ddlUserRole.SelectedValue = dt.Rows[0]["UserRole"].ToString();

                ddlUserRole.Enabled = false;


            }
            if (dt2.Rows.Count > 0)
            {
                txtAddress1.Text = dt2.Rows[0]["Address1"].ToString();
                txtEmail.Text = dt2.Rows[0]["Email_ID"].ToString();
                txtMobile.Text = dt2.Rows[0]["Mobile_No"].ToString();
            }

           
        }

    }

    protected void btnbtnUpdate_Click(object sender, EventArgs e)
    {
        UserDetail objUserDetail = Session["UserDetails"] as UserDetail;
        string Result = objGetSetDatabase.SET_UserAccount("", txtUserID.Text.Trim(), txtUserPassword.Text.Trim(), ddlTitle.SelectedValue, txtFName.Text.Trim().ToUpper(),
            txtLName.Text.Trim().ToUpper(), "", "", ddlUserRole.SelectedValue, objUserDetail.userID, DateTime.Now.ToString(), "",
            "", "", "Update");
        if (Result.ToLower() == "true")
        {
            string result2 = objGetSetDatabase.SET_UserAccountDetails("", txtUserID.Text.Trim(), txtAddress1.Text.Trim(), "", "", "", "", txtMobile.Text.Trim(),
                txtMobile.Text.Trim(), txtEmail.Text.Trim(), objUserDetail.userID, "Update");
            if (result2.ToLower() == "true")
            {
                lblMsg.Text = "User is successfuly updated.";
                GetProfile(objUserDetail.userID);
                txtUserID.Enabled = false;
            }
            else if (Result.ToLower() == "false")
            {
                lblMsg.Text = "Profile is not successfuly updated.";
            }
        }
        else if (Result.ToLower() == "false")
        {
            lblMsg.Text = "Profile is not successfuly updated.";
        }
        else if (Result.ToLower() == "duplicate")
        {
            lblMsg.Text = "duplicate User is not allowed.";
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {

    }
}