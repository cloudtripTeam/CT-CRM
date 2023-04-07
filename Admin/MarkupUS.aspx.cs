
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;

using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_MarkupUS : CompressedPage
{
    protected void Page_Load(object sender, EventArgs e)
    {


       

        if (Session["UserDetails"] != null)
        {
            if (!IsPostBack)
            {
                UserDetail objUserDetail = Session["UserDetails"] as UserDetail;
                if (!objUserDetail.isAuth("MarkupUS"))
                {
                    Response.Redirect("~/Admin/AccessDenied.aspx");
                    return;
                }
                if(objUserDetail.userID.ToLower() == "samft" || objUserDetail.userID.ToLower() == "seanft" || objUserDetail.userID.ToLower() == "robft" || objUserDetail.userID.ToLower() == "TIWARI".ToLower() || objUserDetail.userID.ToLower() == "Dinesh".ToLower())
                {
                    btnDeleteAll.Visible = false;
                }


                CommanBinding.BindCompanyDetails(ref ddlCompany, objUserDetail.userID);
                txtFromDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                txtToDate.Text = DateTime.Today.AddMonths(12).ToString("dd/MM/yyyy");
                hfUpdatedBy.Value = objUserDetail.userID;
                if (ddlCompany.Items.Count == 1)
                {
                    ddlCampDetails.Items.Clear();

                    CommanBinding.BindCampaignDetails(ref ddlCampDetails, ddlCompany.SelectedValue);

                }
                checkboxisActive.Checked = CheckDiscountMarkup_US();
                logicdiv.Visible = false;
            }
            if(IsPostBack)
            {
                logicdiv.Visible = true;
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx", false);
        }
    }
    #region Markup details

    [WebMethod(EnableSession = true)]
    public static string DeleteMarkup(string ID)
    {
        GetSetDatabase objGetSetDatabase = new GetSetDatabase();
        return objGetSetDatabase.DeleteFlightMarkupUS(ID, "Delete").ToString().ToLower();
    }

    [WebMethod(EnableSession = true)]
    public static string UpdateMarkupExcel(string ID, string UpdateField, string Value, string UpdatedBy)
    {
        try
        {
            GetSetDatabase objGetSetDatabase = new GetSetDatabase();

            return objGetSetDatabase.UpdateMarkupExcelUS(ID, UpdateField, Value.Trim().ToUpper(), UpdatedBy);
        }
        catch { return "false"; }
    }

    [WebMethod(EnableSession = true)]
    public static string UpdateDiscountMarkup_US(string UseStatus)
    {
        try
        {
            GetSetDatabase objGetSetDatabase = new GetSetDatabase();
            return objGetSetDatabase.UpdateDiscountMarkup_US(Convert.ToBoolean(UseStatus));

        }
        catch { return "false"; }
    }
    public bool CheckDiscountMarkup_US()
    {
        try
        {
            GetSetDatabase objGetSetDatabase = new GetSetDatabase();
            return objGetSetDatabase.CheckDiscountMarkup_US();

        }
        catch { return false; }
    }
    #endregion

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (txtPasskey.Text != "4829")
        {
            Response.Redirect("~/Admin/AccessDenied.aspx");
            return;
        }
        GetSetDatabase objGetSetDatabase = new GetSetDatabase();
        UserDetail objUserDetail = HttpContext.Current.Session["UserDetails"] as UserDetail;
        string retStatus = string.Empty;
        string[] markupcalss = txtMarkupClass.Text.Split(',');
        foreach (string cls in markupcalss)
        {
            if (!string.IsNullOrEmpty(cls))
            {
                retStatus = objGetSetDatabase.SET_FlightMarkupDetailUS("", txtMarkupFrom.Text.Trim().ToUpper(),
                txtMarkupTo.Text.Trim().ToUpper(), txtAirV.Text.Trim().ToUpper(),
                "ANY", ddlCategory.SelectedValue, cls, "",
                txtAmount.Text.Trim(), DdlAmountType.SelectedValue, txtFromDate.Text, txtToDate.Text, ddlCompany.SelectedValue,
                ddlCampDetails.SelectedValue, ddlJourneyType.SelectedValue, objUserDetail.userID, "Insert",
                Convert.ToInt32(ddlPax.SelectedValue), Convert.ToInt32(txtDTD.Text.Trim()),txtRestrictedClass.Text);
            }
        }


        if (retStatus.ToLower() == "true")
        {
            lblMsg.Text = "Markup is successfully Updated in database!!";
            bindMarkUp();
            txtMarkupFrom.Text = "";
            txtMarkupTo.Text = "";
            txtAirV.Text = "";
            txtMarkupClass.Text = "";
            txtAmount.Text = "";
            ddlCompany.SelectedIndex = 0;
            ddlCampDetails.SelectedIndex = 0;

        }
        else if (retStatus.ToLower() == "duplicate")
        {
            lblMsg.Text = "Duplicate markup already exists in database!!";
        }
        else
        {
            lblMsg.Text = "Markup is not successfully Updated in database!!";
        }

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (Page.IsPostBack)
        {

            if (txtPasskey.Text != "4829")
            {
                Response.Redirect("~/Admin/AccessDenied.aspx");
                return;
            }
        }
        bindMarkUp();
    }
    private void bindMarkUp()
    {
        GetSetDatabase objGetSetDatabase = new GetSetDatabase();
        UserDetail objUserDetail = HttpContext.Current.Session["UserDetails"] as UserDetail;
        DataTable dt = objGetSetDatabase.GET_FlightMarkupDetailUS(string.Empty,
																  txtMarkupFrom.Text.Trim(),
																  txtMarkupTo.Text.Trim(),
																  txtAirV.Text.Trim(),
																  string.Empty,
																  ddlCategory.SelectedValue,
																  txtMarkupClass.Text.Trim(),
                                                                  string.Empty,
																  ddlCompany.SelectedValue,
																  ddlCampDetails.SelectedValue,
																  ddlJourneyType.SelectedValue,
																  string.Empty,
                                                                  string.Empty,
																  Convert.ToInt32(ddlPax.SelectedValue),
																  txtModifyBy.Text,
																  txtAmount.Text,
																  DdlAmountType.SelectedValue,
																  txtFromDate.Text,
																  txtToDate.Text,
																  objUserDetail.userID,
                                                                  txtDTD.Text == "" ? 0  : Convert.ToInt32(txtDTD.Text),
																  txtRestrictedClass.Text);

        var CheckisActive = " isActive='True'";
        if (dt.Select(CheckisActive).Length > 0)
        {
            DataView DV = dt.DefaultView;
            DV.RowFilter = CheckisActive;
            checkboxisActive.Checked = true;

        }
        else
        {
            checkboxisActive.Checked = false;
        }

        rptrEdit.Visible = false;
        rptrDetails.Visible = false;
        if (dt != null && dt.Rows.Count > 0)
        {
            ViewState["Makups"] = dt;
            btnExport1.Visible = true;
            btnDelete.Visible = true;
            if (chbSeeInExcel.Checked)
            {
                rptrEdit.DataSource = dt;
                rptrEdit.DataBind();
                rptrEdit.Visible = true;
                rptrDetails.Visible = false;
            }
            else
            {
                rptrDetails.DataSource = dt;
                rptrDetails.DataBind();
                rptrEdit.Visible = false;
                rptrDetails.Visible = true;
            }
        }
        else
        {
            lblMsg.Text = "No markup found for given search criteria";
        }

    }
    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlCampDetails.Items.Clear();
        ddlCampDetails.Items.Insert(0, new ListItem("Select Campaign", ""));
        if (ddlCompany.SelectedIndex != 0)
        {
            CommanBinding.BindCampaignDetails(ref ddlCampDetails, ddlCompany.SelectedValue);
        }
    }

    protected void btnExport1_Click(object sender, EventArgs e)
    {
        if (txtPasskey.Text != "4829")
        {
            Response.Redirect("~/Admin/AccessDenied.aspx");
            return;
        }
        DataTable dt = (DataTable)ViewState["Makups"];
        if (dt != null)
        {
            string attachment = "attachment; filename=" + "MarkupUS_" + DateTime.Now.ToString("ddMMMyyyy") + ".xlsx";
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

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (txtPasskey.Text != "4829")
        {
            Response.Redirect("~/Admin/AccessDenied.aspx");
            return;
        }
        foreach (RepeaterItem item in rptrDetails.Items)
        {
            CheckBox chk = item.FindControl("chk") as CheckBox;
            HiddenField hf = item.FindControl("hdMkid") as HiddenField;
            if (chk.Checked)
            {
                DeleteMarkup(hf.Value);
            }
        }
        lblMsg.Text = "Record Deleted.";
        bindMarkUp();
    }



    #region Export in Excel

    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (txtPasskey.Text != "4829")
        {
            Response.Redirect("~/Admin/AccessDenied.aspx");
            return;
        }
        try
        {
            using (SqlConnection exportConn = DataConnection.GetConnectionMarkupUS())
            {
                DataSet dt = new DataSet();
                exportConn.Open();
                string cmdstr = "SELECT  isnull([Agnt_AirF_Markup_ID],'') AS SNo, isnull([Agnt_AirF_Markup_From],'') AS [From], isnull([Agnt_AirF_Markup_To],'') AS [To], isnull([Agnt_AirF_Markup_AirV],'') AS Airline," +
                "isnull([Agnt_AirF_Markup_Provider],'') AS GDS, isnull([Agnt_AirF_Markup_Category],'') AS CabinClass, isnull([Agnt_AirF_Markup_Class],'') AS [Class], isnull([Agnt_AirF_Markup_Fare_Type],'') AS FareType," +
                "isnull([Agnt_AirF_Markup_Amount],'') AS Amount, isnull([Agnt_AirF_Markup_Amount_Type],'') AS AmountType, isnull([Agnt_AirF_Markup_ValidFromDate],'') AS ValidFromDate," +
                "isnull([Agnt_AirF_Markup_ValidToDate],'') AS ValidToDate, isnull([COMP_DTL_Company_ID],'') AS Company_ID, isnull([Camp_ID],'') AS  Camp_ID, isnull([Agnt_AirF_Markup_JourneyType],'') AS JourneyType," +
                "isnull([Agnt_AirF_Markup_ModifiedBy],'') AS ModifiedBy, isnull([Agnt_AirF_Markup_LastModifiedDate],'') AS LastModifiedDate  ,isnull([DaysToDeparture],'') AS DaysToDeparture, isnull(Agnt_AirF_No_Of_Pax,1) as [PaxCount],isnull([Agnt_AirF_Restricted_Class],'') AS RestrictedClass FROM [FlightMarkupDetail]";
                SqlCommand cmd = new SqlCommand(cmdstr, exportConn);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);
                WriteExcelWithNPOI(dt.Tables[0], "xlsx");
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void WriteExcelWithNPOI(DataTable dt, String extension)
    {

        IWorkbook workbook;

        if (extension == "xlsx")
        {
            workbook = new XSSFWorkbook();
        }
        else if (extension == "xls")
        {
            workbook = new HSSFWorkbook();
        }
        else
        {
            throw new Exception("This format is not supported");
        }

        ISheet sheet1 = workbook.CreateSheet("Sheet 1");

        //make a header row
        IRow row1 = sheet1.CreateRow(0);

        for (int j = 0; j < dt.Columns.Count; j++)
        {

            ICell cell = row1.CreateCell(j);
            String columnName = dt.Columns[j].ToString();
            cell.SetCellValue(columnName);
        }

        //loops through data
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            IRow row = sheet1.CreateRow(i + 1);
            for (int j = 0; j < dt.Columns.Count; j++)
            {

                ICell cell = row.CreateCell(j);
                String columnName = dt.Columns[j].ToString();
                cell.SetCellValue(dt.Rows[i][columnName].ToString());
            }
        }

        using (var exportData = new MemoryStream())
        {
            Response.Clear();
            workbook.Write(exportData);
            string fileName = "MarkUpUS_" + DateTime.Now.ToString("ddMMMyyyy");
            if (extension == "xlsx") //xlsx file format
            {
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", fileName + ".xlsx"));
                Response.BinaryWrite(exportData.ToArray());
            }
            else if (extension == "xls")  //xls file format
            {
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", fileName + ".xls"));
                Response.BinaryWrite(exportData.GetBuffer());
            }
            Response.End();
        }
    }


    #endregion

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (txtPasskey.Text != "4829")
        {
            Response.Redirect("~/Admin/AccessDenied.aspx");
            return;
        }
        if (FileUpload1.HasFile)
        {
            string FileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
            string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName);

            if (!Directory.Exists(Server.MapPath("~/Admin/Markup/")))
            {
                Directory.CreateDirectory(Server.MapPath("~/Admin/Markup/"));
            }
            string FilePath = Server.MapPath("~/Admin/Markup/" + FileName);
            FileUpload1.SaveAs(FilePath);
            SaveRows(FilePath, Extension, "Yes");
        }
        else
        {
            lblMsg.Text = "Please choose an excell markup file to proceed";

        }
    }

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
                        dr["JourneyType"].ToString(), objUserDetail.userID, ref ctr, dr["DaysToDeparture"].ToString(),
                        dr["PaxCount"].ToString(), dr["RestrictedClass"].ToString()));
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

    #region Markup Make Table
    private System.Data.DataTable CreateMarkupDataTable()
    {
        System.Data.DataTable dt = new System.Data.DataTable();
        dt.TableName = "AirFareMarkup_Type";

        dt.Columns.Add("Agnt_AirF_Markup_ID", typeof(int));
        dt.Columns.Add("Agnt_AirF_Markup_From", typeof(string));
        dt.Columns.Add("Agnt_AirF_Markup_To", typeof(string));
        dt.Columns.Add("Agnt_AirF_Markup_AirV", typeof(string));
        dt.Columns.Add("Agnt_AirF_Markup_Provider", typeof(string));
        dt.Columns.Add("Agnt_AirF_Markup_Category", typeof(string));
        dt.Columns.Add("Agnt_AirF_Markup_Class", typeof(string));
        dt.Columns.Add("Agnt_AirF_Markup_Fare_Type", typeof(string));
        dt.Columns.Add("Agnt_AirF_Markup_Amount", typeof(double));
        dt.Columns.Add("Agnt_AirF_Markup_Amount_Type", typeof(string));
        dt.Columns.Add("Agnt_AirF_Markup_ValidFromDate", typeof(DateTime));
        dt.Columns.Add("Agnt_AirF_Markup_ValidToDate", typeof(DateTime));
        dt.Columns.Add("COMP_DTL_Company_ID", typeof(string));
        dt.Columns.Add("Camp_ID", typeof(string));
        dt.Columns.Add("Agnt_AirF_Markup_JourneyType", typeof(string));
        dt.Columns.Add("Agnt_AirF_Markup_ModifiedBy", typeof(string));
        dt.Columns.Add("Agnt_AirF_Markup_LastModifiedDate", typeof(DateTime));
        dt.Columns.Add("DaysToDeparture", typeof(int));
        dt.Columns.Add("Agnt_AirF_No_Of_Pax", typeof(int));
        dt.Columns.Add("Agnt_AirF_Restricted_Class", typeof(string));
        return dt;
    }

    private DataRow CreateMarkupDtaRow(DataRow dr, string SNO, string From, string To, string AirV, string Provider, string Category,
        string Class, string FareType, string Amount, string Amount_Type, string ValidFromDate, string ValidToDate, string CompanyID,
        string CampaignID, string JourneyType, string ModifiedBy, ref int ctr, string DaysToDeparture, string PaxCount, string RestrictedClass)
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
            dr["Agnt_AirF_Restricted_Class"] = RestrictedClass;
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
        if (txtPasskey.Text != "4829")
        {
            Response.Redirect("~/Admin/AccessDenied.aspx");
            return;
        }
        try
        {
            GetSetDatabase objGetSetDatabase = new GetSetDatabase();
            if (objGetSetDatabase.DeleteAllFlightMarkupUS("Truncate"))
            {
                lblMsg.Text = "All markup are successfully deleted";
            }
            else
            {
                lblMsg.Text = "All markup are not successfully deleted";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "All markup are not successfully deleted";
            throw ex;
        }
    }



}
