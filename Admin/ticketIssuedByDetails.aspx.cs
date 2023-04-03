using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_ticketIssuedByDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserDetails"] != null)
        {

            if (!IsPostBack)
            {
                UserDetail objUserDetail = Session["UserDetails"] as UserDetail;
                if (!objUserDetail.isAuth("ticketIssuedByDetails"))
                {
                    Response.Redirect("Default.aspx");
                    return;
                }
                else
                {
                    GetSetDatabase objGetSetDatabase = new GetSetDatabase();
                    string issuedby = Request.QueryString["issuedby"].ToString() ;
                    DataTable dt = objGetSetDatabase.GET_TicketIssuedBy(DateTime.Today.AddDays(-1).ToString("dd/MM/yyyy"), DateTime.Today.ToString("dd/MM/yyyy"), issuedby, "details");
                   ViewState["ticketIssuedBy"] = dt;

                    int count = 0;
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        btnExport.Visible = true;

                        rptIssuedBy.DataSource = dt;
                        rptIssuedBy.DataBind();

                        Control FooterTemplate = rptIssuedBy.Controls[rptIssuedBy.Controls.Count - 1].Controls[0];
                        Literal ltrCost = FooterTemplate.FindControl("ltrTotalcost") as Literal;
                        Literal ltrTotalSell = FooterTemplate.FindControl("ltrTotalSell") as Literal;
                        double totSell = 0.0; double totCost = 0;
                        foreach (DataRow dr in dt.Rows)
                        {


                            totSell += Convert.ToDouble(dr["SellPrice"].ToString());

                            totCost += Convert.ToDouble(dr["CostPrice"]);
                        }
                        ltrTotalSell.Text = totSell.ToString();
                        ltrCost.Text = totCost.ToString();



                    }
                    else {

                        btnExport.Visible = false;
                    }
                }
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx", false);
        }
    }


    protected void btnExport_Click(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)ViewState["ticketIssuedBy"];
        if (dt != null)
        {


            string attachment = "attachment; filename=" + "TicketIssuedBy_" + DateTime.Now.ToString("ddMMMyyyy") + ".xls";
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