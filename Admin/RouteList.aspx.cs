using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

public partial class Admin_BlockedIPs : CompressedPage
{
    private SqlConnection con = DataConnection.GetConnection();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            TableBind(); bindMarkUp();
        }
    }
    [WebMethod]
    protected void Button1_Click(object sender, EventArgs e)
    {
        var airline = txtDescription.Text;
        var airport = txtIsoCode.Text;
        if (airline != "" && airport != "")
        {
            SqlCommand cmd = new SqlCommand("[GET_SET_RouteList]", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Type", "I");
            cmd.Parameters.AddWithValue("@description", txtDescription.Text);
            cmd.Parameters.AddWithValue("@code", txtIsoCode.Text);
            con.Open();
            int k = cmd.ExecuteNonQuery();
            if (k > 0)
            {

                txtSuccessMsg.Text = "Saved successfully";

            }
            else
            {
                txtSuccessMsg.Text = "Value already saved";
            }
            con.Close();
            TableBind(); bindMarkUp();
            clearfields();
        }
        else
        {
            txtSuccessMsg.Text = "please fill out fields.";
        }
    }

    private void TableBind()
    {
        try
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select Pid,Description,ISOCode from RouteList", con);
            SqlDataReader rdr = cmd.ExecuteReader();
            GridView1.DataSource = rdr;
            GridView1.DataBind();
            con.Close();
        }
        catch (Exception ex)
        {
            con.Close();
        }
    }
    private void bindMarkUp()
    {
        con.Open();
        SqlCommand cmd = new SqlCommand("select Description,ISOCode from RouteList", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        con.Close();
        ViewState["DATAEXEAL"] = dt;


    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        txtSuccessMsg.Text = "";
        GridView1.EditIndex = e.NewEditIndex;
        TableBind(); bindMarkUp();
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            int Id = Convert.ToInt32(e.NewValues[2]);
            var Description = e.NewValues[0].ToString();
            var code = e.NewValues[1].ToString();
            SqlCommand cmd = new SqlCommand("[GET_SET_RouteList]", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Type", "U");
            cmd.Parameters.AddWithValue("@description", Description);
            cmd.Parameters.AddWithValue("@code", code);
            cmd.Parameters.AddWithValue("@id", Id);
            con.Open();
            int k = cmd.ExecuteNonQuery();
            con.Close();
            if (k > 0)
            {
                Response.Redirect("RouteList.aspx");
                //txtSuccessMsg.Text = "Value updated successfully";
            }
            else
            {
                txtSuccessMsg.Text = "Value already saved";
            }

        }
        catch (Exception es)
        {
            txtSuccessMsg.Text = "please fill out fields.";
        }


    }

    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        txtSuccessMsg.Text = "";
        TableBind(); bindMarkUp();
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int Id = Convert.ToInt32(e.Values[2]);
            SqlCommand cmd = new SqlCommand("[GET_SET_RouteList]", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Type", "D");
            cmd.Parameters.AddWithValue("@id", Id);
            con.Open();
            int k = cmd.ExecuteNonQuery();
            if (k != 0)
            {
                txtSuccessMsg.Text = "Delete successfully";
            }
            con.Close();
            TableBind(); bindMarkUp();
        }
        catch (Exception ex) { }
    }
    protected void clearfields()
    {
        txtDescription.Text = "";
        txtIsoCode.Text = "";
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)ViewState["DATAEXEAL"];
        if (dt != null)
        {
            string attachment = "attachment; filename=" + "Route List.xls";
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
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
        if (Extension == ".xls" || Extension == ".xlsx")
        {
            string FileName = Path.GetFileName(FileUpload1.PostedFile.FileName);


            if (!Directory.Exists(Server.MapPath("~/Admin/RouteList/")))
            {
                Directory.CreateDirectory(Server.MapPath("~/Admin/RouteList/"));
            }
            string FilePath = Server.MapPath("~/Admin/RouteList/" + FileName);
            FileUpload1.SaveAs(FilePath);
            SaveRows(FilePath, Extension, "Yes");
        }
        else
        {
            txtSuccessMsg.Text = "Please choose an excel file to proceed";

        }
    }

    private System.Data.DataTable CreateMarkupDataTable()
    {
        System.Data.DataTable dt = new System.Data.DataTable();
        dt.TableName = "AirFareMarkup_Type";

        dt.Columns.Add("ISOCode", typeof(string));
        dt.Columns.Add("Description", typeof(string));
        return dt;
    }
    private DataRow CreateMarkupDtaRow(DataRow dr,string ISOCode, string Description)
    {
        try
        {
            dr["ISOCode"] = ISOCode;
            dr["Description"] = Description;
        }
        catch (Exception ex)
        {
            var rt = ex.InnerException;
            //lblMsg.Text = "";
        }
        return dr;
    }
    public void SaveRows(string FilePath, string Extension, string isHDR)
    {
        //UserDetail objUserDetail = Session["UserDetails"] as UserDetail;
        string sExcelconnectionstring = "";
        switch (Extension)
        {
            case ".xls": //Excel 97-03
                //sExcelconnectionstring = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FilePath + ";Extended Properties='Excel 8.0;HDR=YES'";
                //sExcelconnectionstring = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FilePath + "; Extended Properties='Excel 12.0 Xml;HDR=" + isHDR + "'";
                sExcelconnectionstring = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FilePath + ";" + "Extended Properties=\"Excel 12.0 Xml; IMEX=1; HDR=Yes\"";
                break;
            case ".xlsx": //Excel 07
                sExcelconnectionstring = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FilePath + "; Extended Properties='Excel 12.0;  IMEX=2; HDR=" + isHDR + "'";
                break;
        }

        sExcelconnectionstring = String.Format(sExcelconnectionstring, FilePath, isHDR);

        using (OleDbConnection OleDbCon = new OleDbConnection(sExcelconnectionstring))
        {
            OleDbCon.Open();
            System.Data.DataTable dtExcelSchema;
            dtExcelSchema = OleDbCon.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
            OleDbCon.Close();
            System.Data.DataTable dt = CreateMarkupDataTable();
            OleDbCommand OleDbCmd = new OleDbCommand("Select * FROM [" + SheetName + "]", OleDbCon);
            OleDbCon.Open();
            using (DbDataReader dr = OleDbCmd.ExecuteReader())
            {
                int ctr = 0;
                OleDbCommand oleCMD2 = new OleDbCommand("select Max(SNo) from [" + SheetName + "]", OleDbCon);
                ctr = Convert.ToInt32(oleCMD2.ExecuteScalar());
                while (dr.Read())
                {
                    dt.Rows.Add(CreateMarkupDtaRow(dt.NewRow(), dr["ISOCode"].ToString(), dr["Description"].ToString()));
                }

                using (SqlConnection SqlCon = DataConnection.GetConnectionMarkup())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = SqlCon;
                        cmd.CommandText = "GET_SET_RouteList";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ParamAirFareMarkup", dt);
                        cmd.Parameters.AddWithValue("@Type", "I");
                        try
                        {
                            SqlCon.Open();
                            int i = cmd.ExecuteNonQuery();
                            txtSuccessMsg.Text = i + " Markups are successfully Uploaded out of " + dt.Rows.Count;
                        }
                        catch (Exception ex)
                        {

                            txtSuccessMsg.Text = ex.ToString();
                        }
                        finally
                        {
                            SqlCon.Close();
                        }
                    }
                }
            }

        }
    }

}