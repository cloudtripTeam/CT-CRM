using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_TicketIssuance : System.Web.UI.Page
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
                if (!objUserDetail.isAuth("TicketIssuance"))
                {
                    Response.Redirect("~/Admin/AccessDenied.aspx");
                    return;
                }
                CommanBinding.BindSupplierDetails(ref ddlSupplier, "");
               

            }
        }
        else
        {
            Response.Redirect("~/Login.aspx", false);
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        UserDetail objUserDetail = HttpContext.Current.Session["UserDetails"] as UserDetail;
        double charges =  -1;
        if (txtCharges.Text.Trim() != "")
        { charges = Convert.ToDouble(txtCharges.Text); }
        DataTable dt = objGetSetDatabase.Get_TicketIssuance("", txtAirline.Text, charges, ddlCurrency.SelectedValue, ddlSupplier.SelectedValue);
        if (dt != null)
        {
            if (objUserDetail.userRole.ToLower() == "admin" || objUserDetail.userRole.ToLower() == "superadmin")
            {
                rptrDetails.DataSource = dt;
                rptrDetails.DataBind();
            }
            else
            {
                rpt.DataSource = dt;
                rpt.DataBind();
            }
        }

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        UserDetail objUserDetail = HttpContext.Current.Session["UserDetails"] as UserDetail;
       
         double charges =  -1;
        if (txtCharges.Text.Trim() != "")
        { charges = Convert.ToDouble(txtCharges.Text); }
        if (objGetSetDatabase.SET_TicketIssuance("", txtAirline.Text.ToUpper().Trim(), charges, ddlCurrency.SelectedValue, ddlSupplier.SelectedValue, objUserDetail.userID, "INSERT"))
        {
            lblMsg.Text = "Record is successfully inserted!!";
            lblMsg.ForeColor = System.Drawing.Color.Green;

            txtAirline.Text = "";
            txtCharges.Text = "";

        }
        else
        {
            lblMsg.Text = "Record is not successfully inserted!!";
            lblMsg.ForeColor = System.Drawing.Color.Red;
        }
    }

    protected void rptrDetails_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "EditDetails")
        {

        }
    }

    [WebMethod(EnableSession = true)]
    public static bool DeleteContents(string ID)
    {
        GetSetDatabase objGetSetdb = new GetSetDatabase();
        UserDetail objUserDetail = HttpContext.Current.Session["UserDetails"] as UserDetail;
        return objGetSetdb.SET_TicketIssuance(ID, "", -1, "","","", "Delete");

    }

    [WebMethod(EnableSession = true)]
    public static string UpdateContents(string ID, string UpdateField, string Value)
    {
        GetSetDatabase objGetSetdb = new GetSetDatabase();
        UserDetail objUserDetail = HttpContext.Current.Session["UserDetails"] as UserDetail;
        string str = string.Empty;
       
        return str;
    }


   

   
}