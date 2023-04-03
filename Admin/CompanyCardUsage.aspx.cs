using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

public partial class Admin_CompanyCardUsage : System.Web.UI.Page
{
    GetSetDatabase gsd = new GetSetDatabase();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserDetails"] != null)
        {
            if (!IsPostBack)
            {
                UserDetail objUserDetail = Session["UserDetails"] as UserDetail;
                if (!objUserDetail.isAuth("CompanyCardUsage"))
                {
                    Response.Redirect("~/Admin/AccessDenied.aspx");
                    return;
                }
                else
                {
                    //if (objUserDetail.userID.ToLower() == "")
                    //{
                    //}
                }
            }
        }

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        UserDetail objUserDetail = Session["UserDetails"] as UserDetail;
        bool b = gsd.SET_CompanyCardUses("INSERT", txtTravelDate.Value, txtPaxName.Value, txtMerchant.Value, ddlCurrency.Value, txtAmount.Value, txt4Digit.Value, txtMerchantRef.Value, txtOurRef.Value, objUserDetail.userID, txtNotes.Value);
        if (b == true)
            ltrMsg.Text = "Record Inserted.";
        else
            ltrMsg.Text = "Sorry, unable to save the record,try Again.";

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DataTable dt=new DataTable();
        dt = gsd.GET_CompanyCardUses("SELECT", txtTranslDate.Value, txtTranslDateTo.Value, txt4Digit.Value, txtOurRef.Value, "");
        if (dt.Rows.Count>0)
        {
            rptCCU.DataSource = dt;
            rptCCU.DataBind();
        }
    }
    
}