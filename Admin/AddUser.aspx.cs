using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_AddUser : System.Web.UI.Page
{
    GetSetDatabase objGetSetDatabase = new GetSetDatabase();
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["UserDetails"] == null)
        //{
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        GetSetCache.ContinueSession(HttpContext.Current.User.Identity.Name);
        //    }
        //}
        if (Session["UserDetails"] != null)
        {
            if (!IsPostBack)
            {
                UserDetail objUserDetail = Session["UserDetails"] as UserDetail;
                if (!objUserDetail.isAuth("Add User"))
                {
                    Response.Redirect("~/Admin/AccessDenied.aspx");
                    return;
                }
                bindAuthDetails();
                btnCancel.Visible = false;
                btnUpdate.Visible = false;
                btnAdd.Visible = true;
                btnSearch.Visible = true;
                txtUserID.Enabled = true;
                BindUserDetails();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx", false);
        }
    }
    public void bindAuthDetails()
    {

        DataTable dt = objGetSetDatabase.GET_Auth_Roll_Master("", "", "", "Select");
        ddlUserRole.Items.Clear();
        if (dt != null)
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (!dr["MstName"].ToString().Equals("superadmin", StringComparison.OrdinalIgnoreCase))
                    ddlUserRole.Items.Add(new ListItem(dr["MstName"].ToString(), dr["MstName"].ToString()));
            }
        }
        //ddlUserRole.DataSource = dt;
        //ddlUserRole.DataValueField = "MstName";
        //ddlUserRole.DataTextField = "MstName";
        //ddlUserRole.DataBind();
        ddlUserRole.Items.Insert(0, new ListItem("Select", ""));
    }
    private void BindUserDetails()
    {
        UserDetail objUserDetail = Session["UserDetails"] as UserDetail;
        DataTable dt = objGetSetDatabase.GET_UserAccount("", txtUserID.Text.Trim(), txtFName.Text.Trim().ToUpper(), "", "", "", "", objUserDetail.userRole.ToLower() == "superadmin" ? "Select" : "SelectNonSuperAdmin");
        if (dt != null)
        {
            gdvUserDetail.DataSource = dt;
            gdvUserDetail.DataBind();
        }
    }

    protected void gdvUserDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditUser")
        {
            DataTable dt = objGetSetDatabase.GET_UserAccount("", e.CommandArgument.ToString(), "", "", "", "", "");
            DataTable dt2 = objGetSetDatabase.GET_UserAccountDetails("", e.CommandArgument.ToString());
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
                    chbIsCheckIp.Checked = Convert.ToBoolean(dt.Rows[0]["IsIpCheck"]);
                    chbIsUserActive.Checked = Convert.ToBoolean(dt.Rows[0]["UserisActive"]);
                }
                if (dt2.Rows.Count > 0)
                {
                    txtAddress1.Text = dt2.Rows[0]["Address1"].ToString();
                    txtEmail.Text = dt2.Rows[0]["Email_ID"].ToString();
                    txtMobile.Text = dt2.Rows[0]["Mobile_No"].ToString();
                }

                btnCancel.Visible = true;
                btnUpdate.Visible = true;
                btnAdd.Visible = false;
                btnSearch.Visible = false;
                txtUserID.Enabled = false;
            }
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        UserDetail objUserDetail = Session["UserDetails"] as UserDetail;
        string Result = objGetSetDatabase.SET_UserAccount("FLTXPT", txtUserID.Text.Trim(), txtUserPassword.Text.Trim(), ddlTitle.SelectedValue, txtFName.Text.Trim().ToUpper(),
            txtLName.Text.Trim().ToUpper(), "INTR", chbIsUserActive.Checked.ToString(), ddlUserRole.SelectedValue, objUserDetail.userID, DateTime.Now.ToString(),
            DateTime.Now.ToString(), objUserDetail.userID, chbIsCheckIp.Checked.ToString(), "Insert");
        if (Result.ToLower() == "true")
        {
            string result2 = objGetSetDatabase.SET_UserAccountDetails("", txtUserID.Text.Trim(), txtAddress1.Text.Trim(), "", "", "", "", txtMobile.Text.Trim(),
                txtMobile.Text.Trim(), txtEmail.Text.Trim(), objUserDetail.userID, "Insert");
            if (result2.ToLower() == "true")
            {
                Clear();
                BindUserDetails();
                lblMsg.Text = "User is successfuly inserted.";
            }
            else if (Result.ToLower() == "false")
            {
                lblMsg.Text = "User is not successfuly inserted.";
            }
        }
        else if (Result.ToLower() == "false")
        {
            lblMsg.Text = "User is not successfuly inserted.";
        }
        else if (Result.ToLower() == "duplicate")
        {
            lblMsg.Text = "duplicate User is not allowed.";
        }
    }

    protected void btnbtnUpdate_Click(object sender, EventArgs e)
    {
        UserDetail objUserDetail = Session["UserDetails"] as UserDetail;
        string Result = objGetSetDatabase.SET_UserAccount("", txtUserID.Text.Trim(), txtUserPassword.Text.Trim(), ddlTitle.SelectedValue, txtFName.Text.Trim().ToUpper(),
            txtLName.Text.Trim().ToUpper(), "", chbIsUserActive.Checked.ToString(), ddlUserRole.SelectedValue, objUserDetail.userID, DateTime.Now.ToString(), "",
            "", chbIsCheckIp.Checked.ToString(), "Update");
        if (Result.ToLower() == "true")
        {
            string result2 = objGetSetDatabase.SET_UserAccountDetails("", txtUserID.Text.Trim(), txtAddress1.Text.Trim(), "", "", "", "", txtMobile.Text.Trim(),
                txtMobile.Text.Trim(), txtEmail.Text.Trim(), objUserDetail.userID, "Update");
            if (result2.ToLower() == "true")
            {
                lblMsg.Text = "User is successfuly updated.";
                Clear();
                BindUserDetails();
                btnCancel.Visible = false;
                btnUpdate.Visible = false;
                btnAdd.Visible = true;
                btnSearch.Visible = true;
                txtUserID.Enabled = true;
            }
            else if (Result.ToLower() == "false")
            {
                lblMsg.Text = "User is not successfuly updated.";
            }
        }
        else if (Result.ToLower() == "false")
        {
            lblMsg.Text = "User is not successfuly updated.";
        }
        else if (Result.ToLower() == "duplicate")
        {
            lblMsg.Text = "duplicate User is not allowed.";
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
        btnCancel.Visible = false;
        btnUpdate.Visible = false;
        btnAdd.Visible = true;
        btnSearch.Visible = true;
        txtUserID.Enabled = true;

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindUserDetails();
    }
    private void Clear()
    {

        txtUserID.Text = "";
        txtAddress1.Text = "";
        txtEmail.Text = "";
        txtFName.Text = "";
        txtLName.Text = "";
        ddlTitle.SelectedIndex = 0;
        txtMobile.Text = "";
        txtUserPassword.Text = "";
        ddlUserRole.SelectedIndex = 0;

        chbIsCheckIp.Checked = false;
        chbIsUserActive.Checked = false;

    }
}