using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_SeoContents : System.Web.UI.Page
{
    GetSetDatabase objGetSetDatabase = new GetSetDatabase();
    DataServices.FandHServicesClient client = new DataServices.FandHServicesClient();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserDetails"] != null)
        {
            if (!IsPostBack)
            {
                UserDetail objUserDetail = Session["UserDetails"] as UserDetail;
                if (!objUserDetail.isAuth("SEO"))
                {
                    Response.Redirect("~/Admin/AccessDenied.aspx");
                    return;
                }
                BindCompanyDetails();

                
                ddlCompanyID.Enabled = true;
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx", false);
        }
    }

    protected void rptrDetails_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "EditDetails")
        {
            
        }
    }

    private void BindCompanyDetails()
    {
        UserDetail objUserDetail = Session["UserDetails"] as UserDetail;
        CommanBinding.BindCompanyDetails(ref ddlCompanyID, objUserDetail.userID);

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        
        UserDetail objUserDetail = HttpContext.Current.Session["UserDetails"] as UserDetail;       
        Uri url = new Uri(txtUrl.Text.Trim());
            string pageUrl = String.Format("{0}{1}{2}{3}", url.Scheme,  Uri.SchemeDelimiter, url.Authority, url.AbsolutePath);
            string contents = txtContent.Text.Trim();
            if (ddlContentType.SelectedValue.ToLower() == "script")
            {

                contents = Server.HtmlEncode(contents);
            }

            if (client.SET_SEOContents("Insert", -1, ddlCompanyID.SelectedValue, pageUrl, ddlContentType.SelectedValue, contents, objUserDetail.userID))
        {
            lblMsg.Text = "Record is successfully inserted!!";
            lblMsg.ForeColor = System.Drawing.Color.Green;
           
            txtContent.Text = "";
            
        }
        else
        {
            lblMsg.Text = "Record is not successfully inserted!!";
            lblMsg.ForeColor = System.Drawing.Color.Red;
        }

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Uri url; string pageUrl = string.Empty;
        if (!string.IsNullOrEmpty(txtUrl.Text))
        {
            url = new Uri(txtUrl.Text.Trim());
            pageUrl = String.Format("{0}{1}{2}{3}", url.Scheme, Uri.SchemeDelimiter, url.Authority, url.AbsolutePath);
        }
      DataTable dt=  client.Get_SEOContents("Select", ddlCompanyID.SelectedValue, pageUrl, ddlContentType.SelectedValue);
      if (dt != null)
      {

          rptrDetails.DataSource = dt;
          rptrDetails.DataBind();
      }

    }

    [WebMethod(EnableSession = true)]
    public static string DeleteContents(string ID)
    {
        DataServices.FandHServicesClient client = new DataServices.FandHServicesClient();
        if (client.SET_SEOContents("Delete", Convert.ToInt32(ID), "", "", "", "", ""))
            return "true";
        else
            return "false";
        
    }

    [WebMethod(EnableSession = true)]
    public static string UpdateContents(string ID, string UpdateField, string Value)
    {
        GetSetDatabase objGetSetDatabase = new GetSetDatabase();
        UserDetail objUserDetail = HttpContext.Current.Session["UserDetails"] as UserDetail;
        string str = string.Empty;
        if (UpdateField == "BaseFare")
        {
            str = objGetSetDatabase.SET_ChangeFare_UK(ID, "", "", "",
               "", "", "", "", Value,"","",
               "", objUserDetail.userID, "Update","","","","1","-1","");
        }
        else if (UpdateField == "Tax")
        {

            str = objGetSetDatabase.SET_ChangeFare_UK(ID, "", "", "",
                   "", "", "", "", "",
                   Value,"","",objUserDetail.userID, "Update","","","","1", "-1", "");
        }
        else if (UpdateField == "Markup")
        {

            str = objGetSetDatabase.SET_ChangeFare_UK(ID, "", "", "",
                   "", "", "", "", "",
                   "", Value,"",objUserDetail.userID, "Update","","","","1", "-1", "");
        }
        else if (UpdateField == "MarkupTJ")
        {

            str = objGetSetDatabase.SET_ChangeFare_UK(ID, "", "", "",
                   "", "", "", "", "",
                   "","", Value,objUserDetail.userID, "Update","","","", "1", "-1", "");
        }
        else if (UpdateField == "MarkupXPT")
        {

            str = objGetSetDatabase.SET_ChangeFare_UK(ID, "", "", "",
                   "", "", "", "", "",
                   "","",Value,objUserDetail.userID, "Update","","","", "1", "-1", "");
        }
        else if (UpdateField == "Commission")
        {
            str = objGetSetDatabase.SET_ChangeFare_UK(ID, "", "", "",
                   "", "", "", "", "",
                   "", "",Value, objUserDetail.userID, "Update","","","", "1", "-1", "");
        }
        else if (UpdateField == "FlightType")
        {
            str = objGetSetDatabase.SET_ChangeFare_UK(ID, "", "", "",
                  "", "", "", "", "",
                  "", "", "",objUserDetail.userID, "Update", "", "", "", "", Value, "");
        }
        else if (UpdateField == "ModifiedDate")
        {
            str = objGetSetDatabase.SET_ChangeFare_UK(ID, "", "", "",
                  "", "", "", "", "",
                  "", "", "",objUserDetail.userID, "Update", "", "", "", "", "-1",Value);
        }
        return str;
    }
}