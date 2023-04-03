using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ChargebackReport : System.Web.UI.Page
{
    private SqlConnection con = DataConnection.GetConnection();
    //string idxp = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        hdnate.Value = DateTime.Today.ToString("dd/MM/yyyy");
        if (Session["UserDetails"] != null)
        {
            if (!IsPostBack)
            {
                UserDetail objUserDetail = Session["UserDetails"] as UserDetail;
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx", false);
        }
        if (!Page.IsPostBack)
        {

            bindMarkUp();
        }
    }
    private void bindMarkUp()
    {
        GetSetDatabase objGetSetDatabase = new GetSetDatabase();
        UserDetail objUserDetail = HttpContext.Current.Session["UserDetails"] as UserDetail;
        con.Open();
        SqlCommand cmd = new SqlCommand("SELECT * FROM Export_ChargeBack", con);   
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        con.Close();
        rptr.DataSource = dt;
        rptr.DataBind();
        ViewState["DATAEXEAL"] = dt;
    }
    protected void btnSerch_Click(object sender, EventArgs e)
    {
        GetSetDatabase objGetSetDatabase = new GetSetDatabase();
        UserDetail objUserDetail = HttpContext.Current.Session["UserDetails"] as UserDetail;
        con.Open();
        SqlCommand cmd = new SqlCommand("GET_ChargeBkpReport_DynSearch", con);
        cmd.Parameters.AddWithValue("@Booking_id", txtDisbuteAmunt.Text);
        cmd.Parameters.AddWithValue("@Chargeback_Received", txtChegRecivDate.Text);
        cmd.Parameters.AddWithValue("@ChargebackDisputereport", txtDisputeDate.Text);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dtt = new DataTable();
        da.Fill(dtt);
        con.Close();
        rptr.DataSource = dtt;
        rptr.DataBind();
        rptr.DataBind();
        ViewState["DATAEXEAL"] = dtt;
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {     
        DataTable dt = (DataTable)ViewState["DATAEXEAL"];
        if (dt != null)
        {
            string attachment = "attachment; filename=" + "ChargebackDisputereport" + DateTime.Now.ToString("dd/MM/yyyy") + ".xls";
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