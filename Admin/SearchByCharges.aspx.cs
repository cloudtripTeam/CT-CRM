using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_SearchByCharges : System.Web.UI.Page
{
    DataTable dtCharges = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserDetails"] != null)
        {

            if (!IsPostBack)
            {
                UserDetail objUserDetail = Session["UserDetails"] as UserDetail;
                if (!objUserDetail.isAuth("SearchByCharges"))
                {
                    Response.Redirect("Default.aspx");
                    return;
                }
                else
                {
                   
                    txtFromDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    string role = objUserDetail.userRole.ToLower();
                    

                }
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx", false);
        }
    }

    protected void btnFind_Click(object sender, EventArgs e)
    {
        this.gvChargesBy.DataSource = null;
        
        GetSetDatabase db = new GetSetDatabase();
        UserDetail objUserDetail = Session["UserDetails"] as UserDetail;

        if (objUserDetail != null)
        {
            string role = objUserDetail.userRole.ToLower();

            dtCharges = db.SearchByCharges(ddlChargesType.SelectedValue,txtFromDate.Text.Trim(),txtToDate.Text.Trim());
            if (dtCharges != null)
            {
                gvChargesBy.DataSource = dtCharges;
                gvChargesBy.DataBind();
                btnExport.Visible = true;
            }
            else
            {
                btnExport.Visible = false;
            }

            
            ViewState["SearchByCharges"] = dtCharges;
           

            
        }

    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)ViewState["SearchByCharges"];
        if (dt != null)
        {


            string attachment = "attachment; filename=" + "ChargesType_" + DateTime.Now.ToString("ddMMMyyyy") + ".xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.ms-excel";
            string tab = "";
            foreach (DataColumn dc in dt.Columns)
            {
                Response.Write(tab + dc.ColumnName);
                tab = "\t";
            }
            Response.Write("\n");
            int i;
            foreach (DataRow dr in dt.Rows)
            {
                tab = "";
                for (i = 0; i < dt.Columns.Count; i++)
                {
                    Response.Write(tab + dr[i].ToString());
                    tab = "\t";
                }
                Response.Write("\n");
            }
            Response.End();
        }
    }
}