using System;

using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_SetPermission : CompressedPage
{
    GetSetDatabase objGetSetDatabase = new GetSetDatabase();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserDetails"] != null)
        {
            if (!IsPostBack)
            {
                UserDetail objUserDetail = Session["UserDetails"] as UserDetail;
                if (!objUserDetail.isAuth("Set Permission"))
                {
                    Response.Redirect("~/Admin/AccessDenied.aspx");
                    return;
                }
                bindAuthDetails();
                btnProceed.Visible = true;
                btnSetPermission.Visible = false;
                btnCompanyPermission.Visible = false;
                pnlDetails.Visible = false;
                ddlRollMaster.Enabled = true;
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
        ddlRollMaster.Items.Clear();
        if (dt != null)
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (!dr["MstName"].ToString().Equals("superadmin", StringComparison.OrdinalIgnoreCase))
                    ddlRollMaster.Items.Add(new ListItem(dr["MstName"].ToString() + "[Role]", dr["MstName"].ToString()));
            }
        }
        DataTable dtUser = objGetSetDatabase.GET_UserAccount("", "", "", "", "INTR", "", "");
        if (dtUser != null)
        {
            foreach (DataRow dr in dtUser.Rows)
            {
                if (dr["UserID"].ToString().ToLower() != "adminsup" && dr["UserID"].ToString().ToLower() != "admin")
                ddlUser.Items.Add(new ListItem(dr["UserID"].ToString() , dr["UserID"].ToString()));
            }
        }
       
        ddlRollMaster.Items.Insert(0, "Select");
        ddlUser.Items.Insert(0, "Select");
    }
    protected void btnProceed_Click(object sender, EventArgs e)
    {
        if (ddlRollMaster.SelectedIndex == 0)
        {
            btnProceed.Visible = true;
            btnSetPermission.Visible = false;
            pnlDetails.Visible = false;
            ddlRollMaster.Enabled = true;
        }
        else
        {
            UserDetail objUserDetail = Session["UserDetails"] as UserDetail;
           
            DataTable dt =objUserDetail.userRole.ToLower()=="superadmin"? objGetSetDatabase.GET_Auth_Roll_Authorization_New(ddlRollMaster.SelectedValue, "", "SELECTAllPAGE"): objGetSetDatabase.GET_Auth_Roll_Authorization_New(ddlRollMaster.SelectedValue, "true", objUserDetail.userRole,"SelectForSetPermission");
            rptPermissionDetails.DataSource = dt;
            rptPermissionDetails.DataBind();
            btnProceed.Visible = false;
            btnSetPermission.Visible = true;
            pnlDetails.Visible = true;
            ddlRollMaster.Enabled = false;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("SetPermission.aspx");
    }
    protected void btnSetPermission_Click(object sender, EventArgs e)
    {
        foreach (RepeaterItem item in rptPermissionDetails.Items)
        {
            if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
            {
                objGetSetDatabase.SET_Auth_Roll_Authorization_New("", ddlRollMaster.SelectedValue, (item.FindControl("hfOptionID") as HiddenField).Value,
                    (item.FindControl("chbIsAuth") as CheckBox).Checked.ToString(), "Insert");
            }
        }
        btnProceed.Visible = true;
        btnSetPermission.Visible = false;
        pnlDetails.Visible = false;
        ddlRollMaster.Enabled = true;
        lblMsg.Text = "All permission is set successfully!!";
    }

    protected void btnCompnyCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("SetPermission.aspx");
    }

    protected void btnCompanyPermission_Click(object sender, EventArgs e)
    {

        foreach (RepeaterItem item in rptrCompany.Items)
        {
            if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
            {
                objGetSetDatabase.GET_Auth_Coomapany_Authorization((item.FindControl("hfUserID") as HiddenField).Value, (item.FindControl("hfCompanyID") as HiddenField).Value,
                    (item.FindControl("chbIsAuth") as CheckBox).Checked.ToString(),"Insert");

                //objGetSetDatabase.SET_Auth_Roll_Authorization_New("", ddlRollMaster.SelectedValue, (item.FindControl("hfOptionID") as HiddenField).Value,
                //    (item.FindControl("chbIsAuth") as CheckBox).Checked.ToString(), "Insert");
            }
        }
        btnUserProceed.Visible = true;
        btnCompanyPermission.Visible = false;
        pnlCompany.Visible = false;
        ddlUser.Enabled = true;
        Label2.Text = "All permission is set successfully!!";
    }


    protected void btnUserProceed_Click(object sender, EventArgs e)
    {
        if (ddlUser.SelectedIndex == 0)
        {
            btnUserProceed.Visible = true;
            btnCompanyPermission.Visible = false;
            pnlCompany.Visible = false;
            ddlUser.Enabled = true;
        }
        else
        {
            UserDetail objUserDetail = Session["UserDetails"] as UserDetail;

            DataTable dt =  objGetSetDatabase.GET_Auth_Coomapany_Authorization(ddlUser.SelectedValue,"","","SELECT");
            rptrCompany.DataSource = dt;
            rptrCompany.DataBind();
            btnUserProceed.Visible = false;
            btnCompanyPermission.Visible = true;
            pnlCompany.Visible = true;
            ddlUser.Enabled = false;
        }

    }
}