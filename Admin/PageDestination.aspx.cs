using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_PageDestination : CompressedPage
{
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
                if (!objUserDetail.isAuth("PageDestination"))
                {
                    Response.Redirect("~/Admin/AccessDenied.aspx");
                    return;
                }
                CommanBinding.BindCompanyDetails(ref ddlCompany, objUserDetail.userID);
                //UserDetail objUserDetail = HttpContext.Current.Session["UserDetails"] as UserDetail;
                hfUpdatedBy.Value = objUserDetail.userID;
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx", false);
        }
    }

    
    protected void btnAddPageDestination_Click(object sender, EventArgs e)
    {
        GetSetDatabase objGetSetDatabase = new GetSetDatabase();
        UserDetail objUserDetail = HttpContext.Current.Session["UserDetails"] as UserDetail;
        string retStatus = objGetSetDatabase.SET_PageDestination("", txtDest.Text.Trim(), txtAirport.Text.Trim(), ddlClass.SelectedValue.Trim(),
            ddlPage.SelectedValue, ddlCompany.SelectedValue, objUserDetail.userID, "Insert").ToString().ToLower();
        if (retStatus.ToLower() == "true")
        {
            lblMsg.Text = "Record is successfully Updeted in database!!";
            BindDetails();
            txtDest.Text = "";
            //txtAirport.Text = "";
            //ddlCompany.SelectedIndex = 0;
            //ddlClass.SelectedIndex = 0;
            //ddlPage.SelectedIndex = 0;
        }
        else if (retStatus.ToLower() == "duplicate")
        {
            lblMsg.Text = "duplicate record is already exits in database!!";
        }
        else
        {
            lblMsg.Text = "Record is not successfully Updeted in database!!";
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindDetails();
    }
    private void BindDetails()
    {
        GetSetDatabase objGetSetDatabase = new GetSetDatabase();
        DataTable dt = objGetSetDatabase.GET_PageDestination("", txtDest.Text.Trim(), txtAirport.Text.Trim(), ddlClass.SelectedValue.Trim(), ddlPage.SelectedValue, ddlCompany.SelectedValue);
        if (dt != null)
        {
            dlDetails.DataSource = dt;
            dlDetails.DataBind();
        }
    }
    protected void dlDetails_EditCommand(object source, DataListCommandEventArgs e)
    {
        dlDetails.EditItemIndex = e.Item.ItemIndex;
        BindDetails();
    }
    protected void dlDetails_CancelCommand(object source, DataListCommandEventArgs e)
    {
        dlDetails.EditItemIndex = -1;
        BindDetails();
    }
    protected void dlDetails_UpdateCommand(object source, DataListCommandEventArgs e)
    {
        GetSetDatabase objGetSetDatabase = new GetSetDatabase();
        string retStatus = objGetSetDatabase.SET_PageDestination(e.CommandArgument.ToString(), ((TextBox)e.Item.FindControl("txtEditDest")).Text.Trim(),
            ((TextBox)e.Item.FindControl("txtEditAirport")).Text.Trim(), ((DropDownList)e.Item.FindControl("ddlEditClass")).SelectedValue, 
            ((DropDownList)e.Item.FindControl("ddlEditPage")).SelectedValue, "", hfUpdatedBy.Value, "Update").ToString().ToLower();
       if (retStatus.ToLower() == "true")
       {
           lblMsg.Text = "Record is successfully Updeted in database!!";
           dlDetails.EditItemIndex = -1;
           BindDetails();
       }
       else if (retStatus.ToLower() == "duplicate")
       {
           lblMsg.Text = "duplicate record is already exits in database!!";
       }
       else
       {
           lblMsg.Text = "Record is not successfully Updeted in database!!";
       }
    }
    protected void dlDetails_DeleteCommand(object source, DataListCommandEventArgs e)
    {
        GetSetDatabase objGetSetDatabase = new GetSetDatabase();
        if (objGetSetDatabase.SET_PageDestination(e.CommandArgument.ToString(), "", "", "", "", "", "", "Delete").ToString().ToLower() == "true")
        {
            lblMsg.Text = "Page Destination is successfully deleted from database!!";
            dlDetails.EditItemIndex = -1;
            BindDetails();
        }
        else
        {
            dlDetails.EditItemIndex = -1;
            lblMsg.Text = "Page Destination is not successfully deleted from database!!";
        }
    }
    protected void dlDetails_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.EditItem)
        {
            ((DropDownList)e.Item.FindControl("ddlEditClass")).SelectedValue = DataBinder.Eval(e.Item.DataItem, "CClass").ToString();
            ((DropDownList)e.Item.FindControl("ddlEditPage")).SelectedValue = DataBinder.Eval(e.Item.DataItem, "Page").ToString();
           
        }
    }
}