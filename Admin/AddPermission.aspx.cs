using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_AddPermission : CompressedPage
{
    GetSetDatabase objGetSetDatabase = new GetSetDatabase();
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (Session["UserDetails"] != null)
        {
            if (!IsPostBack)
            {
                UserDetail objUserDetail = Session["UserDetails"] as UserDetail;
                if (!objUserDetail.isAuth("Add Permission"))
                {
                    Response.Redirect("~/Admin/AccessDenied.aspx");
                    return;
                }   
              
                bindDdlDetails();
                BindRollMst();

            }
        }
        else
        {
            Response.Redirect("~/Login.aspx", false);
        }
    }

    public void bindDdlDetails()
    {

        DataTable dt = objGetSetDatabase.GET_Auth_PageGroup(0, "");
        if (dt != null)
        {
            gdvGroupDetail.DataSource = dt;
            gdvGroupDetail.DataBind();

            ddlGroup.DataSource = dt;
            ddlGroup.DataValueField = "GroupID";
            ddlGroup.DataTextField = "GroupName";
            ddlGroup.DataBind();
            ddlGroup.Items.Insert(0, "Choose Group");
            ViewState["GroupDetails"] = dt;

            ddlGroupName.DataSource = dt;
            ddlGroupName.DataValueField = "GroupID";
            ddlGroupName.DataTextField = "GroupName";
            ddlGroupName.DataBind();
            ddlGroupName.Items.Insert(0, "Choose Group");          
        }
    }

    #region Page Group


    protected void gdvGroupDetail_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gdvGroupDetail.EditIndex = e.NewEditIndex;
        bindPageGroup();
    }
    protected void gdvGroupDetail_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string GroupID = ((HiddenField)gdvGroupDetail.Rows[e.RowIndex].FindControl("hfGroupID")).Value;

        string Result = objGetSetDatabase.SET_Auth_PageGroup(Convert.ToInt32(GroupID), "", "", 0, "DELETE");
        if (Result.ToLower() == "true")
        {
            gdvGroupDetail.EditIndex = -1;
            bindPageGroup();
            lblMsg.Text = "Page group is successfuly deleted.";
        }
        else if (Result.ToLower() == "false")
        {
            lblMsg.Text = "Page group is not successfuly deleted.";
        }
        else if (Result.ToLower() == "duplicate")
        {
            lblMsg.Text = "Some page are find in this group, Please delete First page";
        }
    }
    protected void gdvGroupDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "AddGroup")
        {
            string GroupName = ((TextBox)gdvGroupDetail.FooterRow.FindControl("txtGroupName")).Text.Trim();
            string GroupDetail = ((TextBox)gdvGroupDetail.FooterRow.FindControl("txtGroupDetail")).Text.Trim();
            string GroupSequence = ((TextBox)gdvGroupDetail.FooterRow.FindControl("txtGroupSequence")).Text.Trim();
            if (string.IsNullOrEmpty(GroupSequence))
                GroupSequence = "0";

            string Result = objGetSetDatabase.SET_Auth_PageGroup(0, GroupName, GroupDetail, Convert.ToInt32(GroupSequence), "INSERT");
            if (Result.ToLower() == "true")
            {
                gdvGroupDetail.EditIndex = -1;
                bindPageGroup();
                lblMsg.Text = "Page group is successfuly Added.";
            }
            else if (Result.ToLower() == "false")
            {
                lblMsg.Text = "Page group is not successfuly Added.";
            }
            else if (Result.ToLower() == "duplicate")
            {
                lblMsg.Text = "duplicate page group is not allowed.";
            }
        }
    }
    protected void gdvGroupDetail_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow row = gdvGroupDetail.Rows[e.RowIndex];
        string GroupName = ((TextBox)row.FindControl("txtEditGroupName")).Text.Trim();
        string GroupDetail = ((TextBox)row.FindControl("txtEditGroupDetail")).Text.Trim();
        string GroupSequence = ((TextBox)row.FindControl("txtEditGroupSequence")).Text.Trim();
        if (string.IsNullOrEmpty(GroupSequence))
            GroupSequence = "0";
        string GroupID = ((HiddenField)row.FindControl("hfGroupID")).Value;

        string Result = objGetSetDatabase.SET_Auth_PageGroup(Convert.ToInt32(GroupID), GroupName, GroupDetail, Convert.ToInt32(GroupSequence), "UPDATE");
        if (Result.ToLower() == "true")
        {
            gdvGroupDetail.EditIndex = -1;
            bindDdlDetails();
            lblMsg.Text = "Page group is successfuly updated.";
        }
        else if (Result.ToLower() == "false")
        {
            lblMsg.Text = "Page group is not successfuly updated.";
        }
        else if (Result.ToLower() == "duplicate")
        {
            lblMsg.Text = "duplicate page group is not allowed.";
        }
    }
    protected void gdvGroupDetail_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gdvGroupDetail.EditIndex = -1;
        bindPageGroup();
    }
    public void bindPageGroup()
    {

        DataTable dt = objGetSetDatabase.GET_Auth_PageGroup(0, "");
        if (dt != null)
        {
            gdvGroupDetail.DataSource = dt;
            gdvGroupDetail.DataBind();            
            ViewState["GroupDetails"] = dt;          
        }
    }
    #endregion

    #region Page Details


    protected void btnAdd_Click(object sender, EventArgs e)
    {

        string Result = objGetSetDatabase.SET_Auth_PageName(0, txtPageName.Text.Trim(), txtPageUrl.Text.Trim(),
            txtPageDetail.Text.Trim(), Convert.ToInt32(ddlGroup.SelectedValue), "INSERT");
        if (Result.ToLower() == "true")
        {
            lblMsg.Text = "Page is successfuly deleted.";
            txtPageDetail.Text = "";
            txtPageName.Text = "";
            txtPageUrl.Text = "";
            ddlGroup.SelectedIndex = 0;
        }
        else if (Result.ToLower() == "false")
        {
            lblMsg.Text = "Page is not successfuly deleted.";
        }
        else if (Result.ToLower() == "duplicate")
        {
            lblMsg.Text = "Refrence is used in this page";
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
    public void BindGrid()
    {

        int GroupID = 0;
        if (ddlGroup.SelectedIndex != 0)
            GroupID = Convert.ToInt32(ddlGroup.SelectedValue);
        DataTable dt = objGetSetDatabase.GET_Auth_PageName(0, txtPageUrl.Text.Trim(), GroupID);
        gdvOptionDetail.DataSource = dt;
        gdvOptionDetail.DataBind();
    }
    protected void gdvOptionDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
        {
            DropDownList ddlEditGroup = e.Row.FindControl("ddlEditGroup") as DropDownList;
            ddlEditGroup.DataSource = ViewState["GroupDetails"] as DataTable;
            ddlEditGroup.DataValueField = "GroupID";
            ddlEditGroup.DataTextField = "GroupName";
            ddlEditGroup.DataBind();
            ddlEditGroup.SelectedValue = DataBinder.Eval(e.Row.DataItem, "GroupID").ToString();
        }
    }
    protected void gdvOptionDetail_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gdvOptionDetail.EditIndex = e.NewEditIndex;
        BindGrid();
    }
    protected void gdvOptionDetail_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gdvOptionDetail.EditIndex = -1;
        BindGrid();
    }
    protected void gdvOptionDetail_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void gdvOptionDetail_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow row = gdvOptionDetail.Rows[e.RowIndex];
        string PageName = ((TextBox)row.FindControl("txtEditPageName")).Text.Trim();
        string PageUrl = ((TextBox)row.FindControl("txtEditPageUrl")).Text.Trim();
        string PageDescription = ((TextBox)row.FindControl("txtEditPageDescription")).Text.Trim();
        string GroupID = ((DropDownList)row.FindControl("ddlEditGroup")).SelectedValue;
        string PageID = ((HiddenField)row.FindControl("hfEditPageID")).Value;

        string Result = objGetSetDatabase.SET_Auth_PageName(Convert.ToInt32(PageID), PageName, PageUrl, PageDescription, Convert.ToInt32(GroupID), "UPDATE");
        if (Result.ToLower() == "true")
        {
            gdvOptionDetail.EditIndex = -1;
            BindGrid();
            lblMsg.Text = "Page is successfuly updated.";
        }
        else if (Result.ToLower() == "false")
        {
            lblMsg.Text = "Page is not successfuly updated.";
        }
        else if (Result.ToLower() == "duplicate")
        {
            lblMsg.Text = "duplicate page is not allowed.";
        }
    }
    #endregion

    #region set Page Option details   

    protected void btnAddOption_Click(object sender, EventArgs e)
    {
        string Result = objGetSetDatabase.SET_Auth_Page_Option("", txtOptionCode.Text.Trim(), txtOptionName.Text.Trim(),
             txtOptionDescription.Text.Trim(), ddlPageName.SelectedValue, "INSERT");
        if (Result.ToLower() == "true")
        {
            lblMsg.Text = "Page is successfuly Added.";
            txtOptionCode.Text = "";
            txtOptionName.Text = "";
            txtOptionDescription.Text = "";           
        }
        else if (Result.ToLower() == "false")
        {
            lblMsg.Text = "Page is not successfuly Added.";
        }
        else if (Result.ToLower() == "duplicate")
        {
            lblMsg.Text = "Refrence is used in this page";
        }
    }

    protected void btnSearchOption_Click(object sender, EventArgs e)
    {
        BingPageOptionDetails();
    }

    public void BingPageOptionDetails()
    {
        DataTable dt = objGetSetDatabase.GET_Auth_Page_Option("", txtOptionName.Text.Trim(), ddlPageName.SelectedValue, "SELECT");
        gdvPageOptionDetail.DataSource = dt;
        gdvPageOptionDetail.DataBind();
    }
   
    protected void ddlGroupName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlGroupName.SelectedIndex == 0)
        {
            ddlPageName.Items.Clear();
            ddlPageName.Items.Insert(0, new ListItem("Select Page", ""));
        }
        else
        {
            ddlPageName.Items.Clear();
            DataTable dt = objGetSetDatabase.GET_Auth_PageName(0, "", Convert.ToInt32(ddlGroupName.SelectedValue));
            if (dt != null)
            {
                ddlPageName.DataSource = dt;
                ddlPageName.DataValueField = "PageID";
                ddlPageName.DataTextField = "PageName";
                ddlPageName.DataBind();
            }
            ddlPageName.Items.Insert(0, new ListItem("Select Page", ""));
        }
    }
    protected void gdvPageOptionDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
        //{
        //    DropDownList ddlEditGroup = e.Row.FindControl("ddlEditGroup") as DropDownList;
        //    ddlEditGroup.DataSource = ViewState["GroupDetails"] as DataTable;
        //    ddlEditGroup.DataValueField = "GroupID";
        //    ddlEditGroup.DataTextField = "GroupName";
        //    ddlEditGroup.DataBind();
        //    ddlEditGroup.SelectedValue = DataBinder.Eval(e.Row.DataItem, "GroupID").ToString();
        //}
    }
    protected void gdvPageOptionDetail_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gdvPageOptionDetail.EditIndex = e.NewEditIndex;
        BingPageOptionDetails();
    }
    protected void gdvPageOptionDetail_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gdvPageOptionDetail.EditIndex = -1;
        BingPageOptionDetails();
    }
    protected void gdvPageOptionDetail_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void gdvPageOptionDetail_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow row = gdvPageOptionDetail.Rows[e.RowIndex];
        string OptionCode = ((TextBox)row.FindControl("txtEditOptionCode")).Text.Trim();
        string OptionFullName = ((TextBox)row.FindControl("txtEditOptionFullName")).Text.Trim();
        string OptionDescription = ((TextBox)row.FindControl("txtEditOptionDescription")).Text.Trim();    
        string OptionID = ((HiddenField)row.FindControl("hfEditPageOptionID")).Value;

        string Result = objGetSetDatabase.SET_Auth_Page_Option(OptionID, OptionCode, OptionFullName, OptionDescription, "", "UPDATE");
        if (Result.ToLower() == "true")
        {
            gdvPageOptionDetail.EditIndex = -1;
            BingPageOptionDetails();
            lblMsg.Text = "Page is successfuly updated.";
        }
        else if (Result.ToLower() == "false")
        {
            lblMsg.Text = "Page is not successfuly updated.";
        }
        else if (Result.ToLower() == "duplicate")
        {
            lblMsg.Text = "duplicate page is not allowed.";
        }
    }
    #endregion

    #region Roll Master
   

    protected void gdvRollMst_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gdvRollMst.EditIndex = e.NewEditIndex;
        BindRollMst();
    }
    protected void gdvRollMst_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string GroupID = ((HiddenField)gdvRollMst.Rows[e.RowIndex].FindControl("hfGroupID")).Value;

        string Result = objGetSetDatabase.SET_Auth_Roll_Master(((HiddenField)gdvRollMst.Rows[e.RowIndex].FindControl("hfGroupID")).Value, "", "", "DELETE");
        if (Result.ToLower() == "true")
        {
            gdvRollMst.EditIndex = -1;
            BindRollMst();
            lblMsg.Text = "Roll Master is successfuly deleted.";
        }
        else if (Result.ToLower() == "false")
        {
            lblMsg.Text = "Roll Master is not successfuly deleted.";
        }
        else if (Result.ToLower() == "duplicate")
        {
            lblMsg.Text = "Some page are find in this Roll Master, Please delete First page";
        }
    }
    protected void gdvRollMst_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "AddRoll")
        {
            string RollName = ((TextBox)gdvRollMst.FooterRow.FindControl("txtRollName")).Text.Trim();
            string RollDesc = ((TextBox)gdvRollMst.FooterRow.FindControl("txtRollDetail")).Text.Trim();           
          

            string Result = objGetSetDatabase.SET_Auth_Roll_Master("", RollName, RollDesc,  "INSERT");
            if (Result.ToLower() == "true")
            {
                gdvRollMst.EditIndex = -1;
                BindRollMst();
                lblMsg.Text = "Roll Master is successfuly Added.";
            }
            else if (Result.ToLower() == "false")
            {
                lblMsg.Text = "Roll Master is not successfuly Added.";
            }
            else if (Result.ToLower() == "duplicate")
            {
                lblMsg.Text = "duplicate Roll Master is not allowed.";
            }
        }
    }
    protected void gdvRollMst_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow row = gdvRollMst.Rows[e.RowIndex];
        string RollName = ((TextBox)row.FindControl("txtEditRollName")).Text.Trim();
        string RollDesc = ((TextBox)row.FindControl("txtEditRollDetail")).Text.Trim();

        string RollID = ((HiddenField)row.FindControl("hfRollID")).Value;

        string Result = objGetSetDatabase.SET_Auth_Roll_Master(RollID, RollName, RollDesc,  "UPDATE");
        if (Result.ToLower() == "true")
        {
            gdvRollMst.EditIndex = -1;
            BindRollMst();
            lblMsg.Text = "Roll Master is successfuly updated.";
        }
        else if (Result.ToLower() == "false")
        {
            lblMsg.Text = "Roll Master is not successfuly updated.";
        }
        else if (Result.ToLower() == "duplicate")
        {
            lblMsg.Text = "duplicate Roll Master is not allowed.";
        }
    }
    protected void gdvRollMst_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gdvRollMst.EditIndex = -1;
        BindRollMst();
    }
    public void BindRollMst()
    {

        DataTable dt = objGetSetDatabase.GET_Auth_Roll_Master("","","","SELECT");
        if (dt != null)
        {
            gdvRollMst.DataSource = dt;
            gdvRollMst.DataBind();
           
        }
    }
    #endregion
}