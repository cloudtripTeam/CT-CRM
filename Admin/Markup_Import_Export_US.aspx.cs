using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.OleDb;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.Common;
using Ionic.Zip;
using System.IO.Compression;
using System.Reflection;
using System.Runtime.InteropServices;
using SQL = System.Data;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using System.Text;
using System.Net;

public partial class OnlineMarkup_Markup_Import_Export_US : System.Web.UI.Page
{
    //HttpApplication app;
    //bool isFile = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        UserDetail objUserDetail = Session["UserDetails"] as UserDetail;
        lblMsg.Text = "";
        if (!objUserDetail.isAuth("Markup_Import_Export2"))
        {
            Response.Redirect("~/Admin/AccessDenied.aspx");
            return;
        }
        else if (objUserDetail.userID.ToLower() == "samft" || objUserDetail.userID.ToLower() == "seanft" || objUserDetail.userID.ToLower() == "robft" || objUserDetail.userID.ToLower() == "TIWARI".ToLower() || objUserDetail.userID.ToLower() == "Dinesh".ToLower())
        {

            //  pnlbulkfare.Visible = false;
            btnDeleteAll.Visible = false;
        }
    }

    #region Export in Excel

    protected void btnExport_Click(object sender, EventArgs e)
    {
        //string exportString = ConfigurationManager.ConnectionStrings["SqlServices"].ConnectionString.ToString();
        try
        {
            using (SqlConnection exportConn = DataConnection.GetConnectionMarkupUS())
            {
               // System.Data.DataTable dt = new System.Data.DataTable();
                System.Data.DataSet dt = new System.Data.DataSet();
                exportConn.Open();
                string cmdstr = "SELECT  isnull([Agnt_AirF_Markup_ID],'') AS SNo, isnull([Agnt_AirF_Markup_From],'') AS [From], isnull([Agnt_AirF_Markup_To],'') AS [To], isnull([Agnt_AirF_Markup_AirV],'') AS Airline," +
                "isnull([Agnt_AirF_Markup_Provider],'') AS GDS, isnull([Agnt_AirF_Markup_Category],'') AS CabinClass, isnull([Agnt_AirF_Markup_Class],'') AS [Class], isnull([Agnt_AirF_Markup_Fare_Type],'') AS FareType," +
                "isnull([Agnt_AirF_Markup_Amount],'') AS Amount, isnull([Agnt_AirF_Markup_Amount_Type],'') AS AmountType, isnull([Agnt_AirF_Markup_ValidFromDate],'') AS ValidFromDate," +
                "isnull([Agnt_AirF_Markup_ValidToDate],'') AS ValidToDate, isnull([COMP_DTL_Company_ID],'') AS Company_ID, isnull([Camp_ID],'') AS  Camp_ID, isnull([Agnt_AirF_Markup_JourneyType],'') AS JourneyType," +
                "isnull([Agnt_AirF_Markup_ModifiedBy],'') AS ModifiedBy, isnull([Agnt_AirF_Markup_LastModifiedDate],'') AS LastModifiedDate  ,isnull([DaysToDeparture],'') AS DaysToDeparture, isnull(Agnt_AirF_No_Of_Pax,1) as [PaxCount] FROM [FlightMarkupDetail]";
                SqlCommand cmd = new SqlCommand(cmdstr, exportConn);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);
                //ExporttoExcel(dt);
                ExporttoExcel1(dt);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void ExporttoExcel(System.Data.DataTable table)
    {
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.ClearHeaders();
        HttpContext.Current.Response.Buffer = true;
        HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
       // HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=Markup_" + DateTime.Now + ".xlsx");

        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
        //sets font
        ////////HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
        ////////HttpContext.Current.Response.Write("<BR><BR><BR>");
        //////////sets the table border, cell spacing, border color, font of the text, background, foreground, font height
        ////////HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' " +
        ////////  "borderColor='#000000' cellSpacing='0' cellPadding='0' " +
        ////////  "style='font-size:10.0pt; font-family:Calibri; background:white;'> <TR>");
        //////////am getting my grid's column headers
        //////////int columnscount = GridView_Result.Columns.Count;

        ////////for (int j = 0; j < table.Columns.Count; j++)
        ////////{      //write in new column
        ////////    HttpContext.Current.Response.Write("<Td>");
        ////////    //Get column headers  and make it as bold in excel columns
        ////////    HttpContext.Current.Response.Write("<B>");
        ////////    HttpContext.Current.Response.Write(table.Columns[j].ColumnName);
        ////////    HttpContext.Current.Response.Write("</B>");
        ////////    HttpContext.Current.Response.Write("</Td>");
        ////////}
        ////////HttpContext.Current.Response.Write("</TR>");
        ////////foreach (DataRow row in table.Rows)
        ////////{//write in new row
        ////////    HttpContext.Current.Response.Write("<TR>");
        ////////    for (int i = 0; i < table.Columns.Count; i++)
        ////////    {
        ////////        HttpContext.Current.Response.Write("<Td>");
        ////////        HttpContext.Current.Response.Write(row[i].ToString());
        ////////        HttpContext.Current.Response.Write("</Td>");
        ////////    }

        ////////    HttpContext.Current.Response.Write("</TR>");
        ////////}
        ////////HttpContext.Current.Response.Write("</Table>");
        ////////HttpContext.Current.Response.Write("</font>");

        HttpContext.Current.Response.Write(build(table));
        HttpContext.Current.Response.Flush();
        HttpContext.Current.Response.End();
    }


    private StringBuilder build(System.Data.DataTable table)
    {
        var sb = new StringBuilder();
        string tab = "";
       //sb.Append(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
       //sb.Append("<font style='font-size:10.0pt; font-family:Calibri;'>");
       //sb.Append("<BR><BR><BR>");
       // //sets the table border, cell spacing, border color, font of the text, background, foreground, font height
       //sb.Append("<Table border='1' bgColor='#ffffff' " +
       //   "borderColor='#000000' cellSpacing='0' cellPadding='0' " +
       //   "style='font-size:10.0pt; font-family:Calibri; background:white;'> <TR>");
       // //am getting my grid's column headers
       // //int columnscount = GridView_Result.Columns.Count;

        for (int j = 0; j < table.Columns.Count; j++)
        {      //write in new column
        //   sb.Append("<Td>");
        //    //Get column headers  and make it as bold in excel columns
        //   sb.Append("<B>");
        //   sb.Append(table.Columns[j].ColumnName);
        //   sb.Append("</B>");
        //   sb.Append("</Td>");

         sb.Append(table.Columns[j].ColumnName);
        sb.Append("\t");
        }
       //sb.Append("</TR>");
    sb.Append("\n");
        foreach (DataRow row in table.Rows)
        {//write in new row
           //sb.Append("<TR>");
            for (int i = 0; i < table.Columns.Count; i++)
            {
               //sb.Append("<Td>");
               //sb.Append(row[i].ToString());
               //sb.Append("</Td>");
                sb.Append(tab + row[i].ToString());
                tab = "\t";
            }

            sb.Append("\n");
        }
      // sb.Append("</Table>");
       //sb.Append("</font>");
        return sb;
    }

    private void ExporttoExcel1(System.Data.DataSet table)
    {
        string filepath =Server.MapPath(@"~\Admin\Markup\") + "Markup_US_"+DateTime.Now.ToString("ddMMMyyyy") + ".xlsx";
        ExcelLibrary.DataSetHelper.CreateWorkbook(filepath, table);

        lblMsg.Text = "Markup file generated, <a href='//www.flightsandholidays.biz/admin/Markup/Markup_US_" + DateTime.Now.ToString("ddMMMyyyy") + ".xlsx' >click here to download</a>";
       
        

       
    }

    #endregion

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
        {
            string FileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
            string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
            //string FolderPath = ConfigurationManager.AppSettings["FolderPath"];

            string FilePath = Server.MapPath("~/Admin/Markup/" + FileName);
            FileUpload1.SaveAs(FilePath);
            SaveRows(FilePath, Extension, "Yes");
        }
        else
        {
            lblMsg.Text = "Please choose an excell markup file to proceed";

        }
    }
    //private void Import_To_Grid(string FilePath, string Extension, string isHDR)
    //{
    //    try
    //    {
    //        string conStr = "";
    //        switch (Extension)
    //        {
    //            case ".xls": //Excel 97-03
    //                conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
    //                break;
    //            case ".xlsx": //Excel 07
    //                conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
    //                break;
    //        }
    //        conStr = String.Format(conStr, FilePath, isHDR);
    //        OleDbConnection connExcel = new OleDbConnection(conStr);
    //        OleDbCommand cmdExcel = new OleDbCommand();
    //        OleDbDataAdapter oda = new OleDbDataAdapter();
    //        System.Data.DataTable dt = new System.Data.DataTable();
    //        cmdExcel.Connection = connExcel;

    //        //Get the name of First Sheet
    //        connExcel.Open();
    //        System.Data.DataTable dtExcelSchema;
    //        dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
    //        string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
    //        connExcel.Close();

    //        //Read Data from First Sheet
    //        connExcel.Open();
    //        cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
    //        oda.SelectCommand = cmdExcel;
    //        oda.Fill(dt);
    //        connExcel.Close();

    //        //Bind Data to GridView
    //        GridView1.Caption = Path.GetFileName(FilePath);
    //        GridView1.DataSource = dt;
    //        GridView1.DataBind();
    //    }
    //    catch (Exception ex)
    //    {


    //    }
    //}
    public void SaveRows(string FilePath, string Extension, string isHDR)
    {
        UserDetail objUserDetail = Session["UserDetails"] as UserDetail;
        string sExcelconnectionstring = "";
        switch (Extension)
        {
            case ".xls": //Excel 97-03
                //sExcelconnectionstring = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FilePath + "; Extended Properties='Excel 8.0;HDR=" + isHDR + ";IMEX=1;'";
                sExcelconnectionstring = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FilePath + ";" + "Extended Properties=\"Excel 12.0 Xml; IMEX=1; HDR=Yes\"";
                break;
            case ".xlsx": //Excel 07
                sExcelconnectionstring = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FilePath + "; Extended Properties='Excel 12.0 Xml;  IMEX=1; HDR=" + isHDR + "'";
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
            //OleDbCommand oledbCmd2 = new OleDbCommand("update [" + SheetName + "] set AirF_Markup_ModifiedBy='" + objUserDetail.userFirstName + "'", OleDbCon);
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

                    dt.Rows.Add(CreateMarkupDtaRow(dt.NewRow(), dr["SNo"].ToString(), dr["From"].ToString(),
                        dr["To"].ToString(), dr["Airline"].ToString(), dr["GDS"].ToString(),
                        dr["CabinClass"].ToString(), dr["Class"].ToString(), dr["FareType"].ToString(),
                        dr["Amount"].ToString(), dr["AmountType"].ToString(), dr["ValidFromDate"].ToString(),
                        dr["ValidToDate"].ToString(), dr["Company_ID"].ToString(), dr["Camp_ID"].ToString(),
                        dr["JourneyType"].ToString(), objUserDetail.userID, ref ctr, dr["DaysToDeparture"].ToString(),dr["PaxCount"].ToString()));
                }

                using (SqlConnection SqlCon = DataConnection.GetConnectionMarkupUS())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = SqlCon;
                        cmd.CommandText = "AirFareMarkup_BulkCopy";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ParamAirFareMarkup", dt);
                        try
                        {
                            SqlCon.Open();
                           int i = cmd.ExecuteNonQuery();
                            lblMsg.Text = i + " Markups are successfully Uploaded out of " + dt.Rows.Count;
                        }
                        catch (Exception ex)
                        {

                            lblMsg.Text = ex.ToString();
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
    //public void ImportDatafromExcel(string excelfilepath, string fileName)
    //{
    //    string sExcelconnectionstring = "Provider=Microsoft.ACE.OLEDB.12.0; data source=" + excelfilepath + "; Extended Properties='Excel 12.0; HDR=YES;'";

    //    using (OleDbConnection connection = new OleDbConnection(sExcelconnectionstring))
    //    {

    //        OleDbCommand command = new OleDbCommand("Select * FROM [AirFareMarkup$]", connection);
    //        connection.Open();
    //        // Create DbDataReader to Data Worksheet
    //        SqlConnection SqlCon = DataConnection.GetConnectionMarkup();
    //        SqlCon.Open();
    //        SqlTransaction transactionmyCon;
    //        transactionmyCon = SqlCon.BeginTransaction();
    //        try
    //        {

    //            string t = SqlCon.ConnectionTimeout.ToString();
    //            string delstr = "Delete from dbo.AirFareMarkup";
    //            using (SqlCommand delcmd = new SqlCommand(delstr, SqlCon, transactionmyCon))
    //            {
    //                delcmd.ExecuteNonQuery();

    //            }
    //            using (DbDataReader dr = command.ExecuteReader())
    //            {
    //                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(SqlCon))
    //                {
    //                    bulkCopy.BulkCopyTimeout = 300;
    //                    bulkCopy.ColumnMappings.Add("AirF_Markup_SNO", "AirF_Markup_SNO");
    //                    bulkCopy.ColumnMappings.Add("AirF_Markup_From", "AirF_Markup_From");
    //                    bulkCopy.ColumnMappings.Add("AirF_Markup_To", "AirF_Markup_To");
    //                    bulkCopy.ColumnMappings.Add("AirF_Markup_AirV", "AirF_Markup_AirV");
    //                    bulkCopy.ColumnMappings.Add("AirF_Markup_Provider", "AirF_Markup_Provider");
    //                    bulkCopy.ColumnMappings.Add("AirF_Markup_Category", "AirF_Markup_Category");
    //                    bulkCopy.ColumnMappings.Add("AirF_Markup_Class", "AirF_Markup_Class");
    //                    bulkCopy.ColumnMappings.Add("AirF_Markup_Amount", "AirF_Markup_Amount");
    //                    bulkCopy.ColumnMappings.Add("AirF_Markup_Amount_Type", "AirF_Markup_Amount_Type");
    //                    bulkCopy.ColumnMappings.Add("AirF_Markup_TourCode", "AirF_Markup_TourCode");
    //                    bulkCopy.ColumnMappings.Add("AirF_Markup_ValidFromDate", "AirF_Markup_ValidFromDate");
    //                    bulkCopy.ColumnMappings.Add("AirF_Markup_ValidToDate", "AirF_Markup_ValidToDate");
    //                    bulkCopy.ColumnMappings.Add("AirF_Markup_ApplicableFor", "AirF_Markup_ApplicableFor");
    //                    bulkCopy.ColumnMappings.Add("AirF_Markup_JourneyType", "AirF_Markup_JourneyType");
    //                    bulkCopy.ColumnMappings.Add("AirF_Markup_ModifiedBy", "AirF_Markup_ModifiedBy");
    //                    bulkCopy.ColumnMappings.Add("AirF_Markup_TravelStartDate", "AirF_Markup_TravelStartDate");
    //                    bulkCopy.ColumnMappings.Add("AirF_Markup_TravelEndDate", "AirF_Markup_TravelEndDate");
    //                    bulkCopy.ColumnMappings.Add("AirF_Markup_Product_Type", "AirF_Markup_Product_Type");
    //                    bulkCopy.DestinationTableName = "dbo.AirFareMarkup";
    //                    bulkCopy.WriteToServer(dr);
    //                }
    //                transactionmyCon.Commit();
    //                SqlCon.Close();
    //                dr.Close();
    //                //transctnnum++;
    //            }
    //            //}
    //        }
    //        catch (Exception ex)
    //        {
    //            transactionmyCon.Rollback();

    //        }
    //    }
    //}

    #region Markup Make Table
    private System.Data.DataTable CreateMarkupDataTable()
    {
        System.Data.DataTable dt = new System.Data.DataTable();
        dt.TableName = "AirFareMarkup_Type";

        dt.Columns.Add("Agnt_AirF_Markup_ID", typeof(Int32));
        dt.Columns.Add("Agnt_AirF_Markup_From", typeof(String));
        dt.Columns.Add("Agnt_AirF_Markup_To", typeof(String));
        dt.Columns.Add("Agnt_AirF_Markup_AirV", typeof(String));
        dt.Columns.Add("Agnt_AirF_Markup_Provider", typeof(String));
        dt.Columns.Add("Agnt_AirF_Markup_Category", typeof(String));
        dt.Columns.Add("Agnt_AirF_Markup_Class", typeof(String));
        dt.Columns.Add("Agnt_AirF_Markup_Fare_Type", typeof(String));
        dt.Columns.Add("Agnt_AirF_Markup_Amount", typeof(Double));
        dt.Columns.Add("Agnt_AirF_Markup_Amount_Type", typeof(String));
        dt.Columns.Add("Agnt_AirF_Markup_ValidFromDate", typeof(DateTime));
        dt.Columns.Add("Agnt_AirF_Markup_ValidToDate", typeof(DateTime));
        dt.Columns.Add("COMP_DTL_Company_ID", typeof(String));
        dt.Columns.Add("Camp_ID", typeof(String));
        dt.Columns.Add("Agnt_AirF_Markup_JourneyType", typeof(String));
        dt.Columns.Add("Agnt_AirF_Markup_ModifiedBy", typeof(String));
        dt.Columns.Add("Agnt_AirF_Markup_LastModifiedDate", typeof(DateTime));
        dt.Columns.Add("DaysToDeparture", typeof(Int32));
        dt.Columns.Add("Agnt_AirF_No_Of_Pax", typeof(Int32));
        return dt;
    }

    private DataRow CreateMarkupDtaRow(DataRow dr, string SNO, string From, string To, string AirV, string Provider, string Category,
        string Class, string FareType, string Amount, string Amount_Type, string ValidFromDate, string ValidToDate, string CompanyID,
        string CampaignID, string JourneyType, string ModifiedBy, ref int ctr, string DaysToDeparture,string PaxCount)
    {
        try
        {
            dr["Agnt_AirF_Markup_ID"] = SNO == "" ? ++ctr : Convert.ToInt32(SNO);
            dr["Agnt_AirF_Markup_From"] = From.ToUpper();
            dr["Agnt_AirF_Markup_To"] = To.ToUpper();
            dr["Agnt_AirF_Markup_AirV"] = AirV.ToUpper();
            dr["Agnt_AirF_Markup_Provider"] = Provider.ToUpper();
            dr["Agnt_AirF_Markup_Category"] = Category.ToUpper();
            dr["Agnt_AirF_Markup_Class"] = Class.ToUpper();
            dr["Agnt_AirF_Markup_Fare_Type"] = FareType.ToUpper();
            dr["Agnt_AirF_Markup_Amount"] = Amount == "" ? 0 : Convert.ToDouble(Amount);
            dr["Agnt_AirF_Markup_Amount_Type"] = Amount_Type.ToUpper();
            dr["Agnt_AirF_Markup_ValidFromDate"] = ValidFromDate == "" ? DateTime.Today : Convert.ToDateTime(ValidFromDate);
            dr["Agnt_AirF_Markup_ValidToDate"] = ValidToDate == "" ? DateTime.Today.AddYears(10) : Convert.ToDateTime(ValidToDate);
            dr["COMP_DTL_Company_ID"] = CompanyID;
            dr["Camp_ID"] = CampaignID;
            dr["Agnt_AirF_Markup_JourneyType"] = JourneyType.ToUpper();
            dr["Agnt_AirF_Markup_ModifiedBy"] = ModifiedBy;
            dr["Agnt_AirF_Markup_LastModifiedDate"] = DateTime.Now;
            dr["DaysToDeparture"] = DaysToDeparture;
            dr["Agnt_AirF_No_Of_Pax"] = PaxCount;
        }
        catch (Exception ex)
        {
            var rt = ex.InnerException;
            //lblMsg.Text = "";
        }
        return dr;
    }

    #endregion

    protected void btnDeleteAll_Click(object sender, EventArgs e)
    {
        //string exportString = ConfigurationManager.ConnectionStrings["SqlServices"].ConnectionString.ToString();
        try
        {
            GetSetDatabase objGetSetDatabase = new GetSetDatabase();
            if (objGetSetDatabase.DeleteAllFlightMarkupUS("Truncate"))
                lblMsg.Text = "All markup are successfully deleted";
            else
                lblMsg.Text = "All markup are not successfully deleted";

        }
        catch (Exception ex)
        {
            lblMsg.Text = "All markup are not successfully deleted";
            throw ex;
        }
    }
}
