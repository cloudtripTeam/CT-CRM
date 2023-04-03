using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.Services;
using System.Web.UI.WebControls;

public partial class Rules_FT : CompressedPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserDetails"] != null)
        {
            if (!IsPostBack)
            {
                UserDetail objUserDetail = Session["UserDetails"] as UserDetail;
                if (!objUserDetail.isAuth("Rules_FT"))
                {
                    Response.Redirect("~/Admin/AccessDenied.aspx");
                    return;
                }
                //else if (objUserDetail.userID.ToLower() == "samft" || objUserDetail.userID.ToLower() == "seanft" ||
                //    objUserDetail.userID.ToLower() == "robft" || objUserDetail.userID.ToLower() == "TIWARI".ToLower() ||
                //    objUserDetail.userID.ToLower() == "Dinesh".ToLower())
                //{
                //    btnDeleteAll.Visible = false;
                //}

                if (objUserDetail.userRole.ToLower() == "admin" || objUserDetail.userRole.ToLower() == "superadmin" || objUserDetail.userID.ToLower() == "Mohan".ToLower() || objUserDetail.userID.ToLower() == "Rohan".ToLower())
                {
                    btnDeleteAll.Enabled = true;
                }
                else
                {
                    btnDeleteAll.Enabled = false;
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
                checkboxisActive.Checked = CheckDisCountRules_FT();
                lblMsg.Text = "";
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx", false);
        }
    }
    #region Rules details

    [WebMethod(EnableSession = true)]
    public static string DeleteRules(string ID)
    {
        GetSetDatabase objGetSetDatabase = new GetSetDatabase();
        return objGetSetDatabase.DeleteRulesFT(ID, "Delete").ToString().ToLower();
    }

    [WebMethod(EnableSession = true)]
    public static string UpdateRulesExcel(string ID, string UpdateField, string Value, string UpdatedBy)
    {
        try
        {
            GetSetDatabase objGetSetDatabase = new GetSetDatabase();

            return objGetSetDatabase.UpdateRulesFTExcel(ID, UpdateField, Value.Trim().ToUpper(), UpdatedBy);
        }
        catch { return "false"; }
    }
    #endregion

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        GetSetDatabase objGetSetDatabase = new GetSetDatabase();
        UserDetail objUserDetail = HttpContext.Current.Session["UserDetails"] as UserDetail;
        string retStatus = string.Empty;
        string[] Rulescalss = txtRulesClass.Text.Split(',');
        foreach (string cls in Rulescalss)
        {
            if (!string.IsNullOrEmpty(cls))
            {
                retStatus = objGetSetDatabase.SET_RuleFTDetail("", txtRulesFrom.Text.Trim().ToUpper(), txtRulesTo.Text.Trim().ToUpper(), txtAirV.Text.Trim().ToUpper(),
               "ANY", ddlCategory.SelectedValue, cls, "",
               txtAmount.Text.Trim(), DdlAmountType.SelectedValue, txtFromDate.Text, txtToDate.Text, ddlCompany.SelectedValue,
               ddlCampDetails.SelectedValue, ddlJourneyType.SelectedValue, objUserDetail.userID, "Insert");
            }
        }


        if (retStatus.ToLower() == "true")
        {
            lblMsg.Text = "Rules is successfully Updated in database!!";
            bindRules();
            txtRulesFrom.Text = "";
            txtRulesTo.Text = "";
            txtAirV.Text = "";
            txtRulesClass.Text = "";
            txtAmount.Text = "";
            ddlCompany.SelectedIndex = 0;
            ddlCampDetails.SelectedIndex = 0;

        }
        else if (retStatus.ToLower() == "duplicate")
        {
            lblMsg.Text = "duplicate Rules is already exits in database!!";
        }
        else
        {
            lblMsg.Text = "Rules is not successfully Updeted in database!!";
        }

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (Page.IsPostBack)
        {
            if (txtPasskey.Text != "7869")
            {
                Response.Redirect("~/Admin/AccessDenied.aspx");
                return;
            }
        }
        bindRules();
    }
    private void bindRules()
    {
        GetSetDatabase objGetSetDatabase = new GetSetDatabase();
        UserDetail objUserDetail = HttpContext.Current.Session["UserDetails"] as UserDetail;
        DataTable dt = objGetSetDatabase.GET_RulesFTDetail("", txtRulesFrom.Text.Trim(), txtRulesTo.Text.Trim(), txtAirV.Text.Trim(), "",
          ddlCategory.SelectedValue, txtRulesClass.Text.Trim(), "", ddlCompany.SelectedValue, ddlCampDetails.SelectedValue,
          ddlJourneyType.SelectedValue, "", "", DdlAmountType.SelectedValue);


        var CheckisActive = "isActive='True' and Amount < 0";
        if (dt.Select(CheckisActive).Length > 0)
        {
            //DataView DV = dt.DefaultView;
            //DV.RowFilter = CheckisActive;
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
            ViewState["RulesFT"] = dt;
            btnExport.Visible = true;
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
            lblMsg.Text = "No any Rules found for given searching criteria";
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


    protected void btnDelete_Click(object sender, EventArgs e)
    {
        foreach (RepeaterItem item in rptrDetails.Items)
        {
            CheckBox chk = item.FindControl("chk") as CheckBox;
            HiddenField hf = item.FindControl("hdMkid") as HiddenField;
            if (chk.Checked)
            {
                DeleteRules(hf.Value);
            }
        }
        lblMsg.Text = "Record Deleted.";
        bindRules();
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            using (SqlConnection exportConn = DataConnection.GetConnectionMarkup())
            {
                DataSet dt = new DataSet();
                exportConn.Open();
                string cmdstr = "SELECT ISNULL([Rules_FT_ID], '') AS SN,ISNULL([Rules_FT_From], '') AS [From],ISNULL([Rules_FT_To],'') AS[To],ISNULL([Rules_FT_AirV], '') AS Airline ,isnull([Rules_FT_Provider],'') AS GDS,isnull([Rules_FT_Category],'') AS CabinClass,isnull([Rules_FT_Class],'') AS[Class],isnull([Rules_FT_Fare_Type],'') AS FareType,isnull([Rules_FT_Amount],'') AS Amount,isnull([Rules_FT_Amount_Type],'') AS AmountType,isnull([Rules_FT_ValidFromDate],'') AS ValidFromDate,isnull([Rules_FT_ValidToDate],'') AS ValidToDate,isnull([Rules_FT_Company_ID],'') AS Company_ID,isnull([Rules_FT_Camp_ID],'') AS Camp_ID,isnull([Rules_FT_JourneyType],'') AS JourneyType,isnull([Rules_FT_ModifiedBy],'') AS ModifiedBy,isnull([Rules_FT_LastModifiedDate],'') AS LastModifiedDate  FROM[RULES_FT]";
                SqlCommand cmd = new SqlCommand(cmdstr, exportConn);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);
                //ExporttoExcel(dt);
                WriteExcelWithNPOI(dt.Tables[0], "xlsx");
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {

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
            string fileName = "Rules_" + DateTime.Now.ToString("ddMMMyyyy");
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

    //private void ExporttoExcel(DataSet table)
    //{
    //    if(!Directory.Exists(Server.MapPath(@"~\Admin\Rules_FT\")))
    //    {
    //        Directory.CreateDirectory(Server.MapPath(@"~\Admin\Rules_FT\"));
    //    }

    //    string filepath = Server.MapPath(@"~\Admin\Rules_FT\") + "Rules_" + DateTime.Now.ToString("ddMMMyyyy") + ".xlsx";
    //    ExcelLibrary.DataSetHelper.CreateWorkbook(filepath, table);
    //    lblMsg.Text = "Rules file generated, <a href='//www.flightsandholidays.biz/admin/Rules_FT/Rules_" + DateTime.Now.ToString("ddMMMyyyy") + ".xlsx' >Click here to download</a>";
    //}

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
        {
            string FileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
            string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName);

            if (!Directory.Exists(Server.MapPath("~/Admin/Rules/")))
            {
                Directory.CreateDirectory(Server.MapPath("~/Admin/Rules/"));
            }
            string FilePath = Server.MapPath("~/Admin/Rules/")  + FileName;
            FileUpload1.SaveAs(FilePath);
            SaveRows(FilePath, Extension, "Yes");
        }
        else
        {
            lblMsg.Text = "Please choose an excel Rules file to proceed";

        }
    }

    public void SaveRows(string FilePath, string Extension, string isHDR)
    {
        UserDetail objUserDetail = Session["UserDetails"] as UserDetail;
        string sExcelconnectionstring = "";
        switch (Extension)
        {
            case ".xls": //Excel 97-03
                sExcelconnectionstring = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FilePath + ";" + "Extended Properties=\"Excel 12.0 Xml; IMEX=1; HDR=Yes\"";
                break;
            case ".xlsx": //Excel 07
                sExcelconnectionstring = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FilePath + "; Extended Properties='Excel 12.0 Xml;  IMEX=1; HDR=" + isHDR + "'";
                break;
        }

        sExcelconnectionstring = string.Format(sExcelconnectionstring, FilePath, isHDR);

        using (OleDbConnection OleDbCon = new OleDbConnection(sExcelconnectionstring))
        {
            OleDbCon.Open();
            DataTable dtExcelSchema;
            dtExcelSchema = OleDbCon.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
            OleDbCon.Close();
            DataTable dt = CreateRulesDataTable();
            OleDbCommand OleDbCmd = new OleDbCommand("Select * FROM [" + SheetName + "]", OleDbCon);
            OleDbCon.Open();
            using (DbDataReader dr = OleDbCmd.ExecuteReader())
            {
                int ctr = 0;
                OleDbCommand oleCMD2 = new OleDbCommand("select Max(SN) from [" + SheetName + "]", OleDbCon);
                ctr = Convert.ToInt32(oleCMD2.ExecuteScalar());
                while (dr.Read())
                {

                    dt.Rows.Add(CreateRulesDataRow(dt.NewRow(), dr["SN"].ToString(), dr["From"].ToString(),
                        dr["To"].ToString(), dr["Airline"].ToString(), dr["GDS"].ToString(),
                        dr["CabinClass"].ToString(), dr["Class"].ToString(), dr["FareType"].ToString(),
                        dr["Amount"].ToString(), dr["AmountType"].ToString(), dr["ValidFromDate"].ToString(),
                        dr["ValidToDate"].ToString(), dr["Company_ID"].ToString(), dr["Camp_ID"].ToString(),
                        dr["JourneyType"].ToString(), objUserDetail.userID, ref ctr));
                }

                using (SqlConnection SqlCon = DataConnection.GetConnectionMarkup())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = SqlCon;
                        cmd.CommandText = "RULES_FT_BulkCopy";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ParamRulesType", dt);
                        try
                        {
                            SqlCon.Open();
                            int i = cmd.ExecuteNonQuery();
                            lblMsg.Text = i + " Rules are successfully Uploaded out of " + dt.Rows.Count;
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

    protected void btnDeleteAll_Click(object sender, EventArgs e)
    {
        try
        {
            GetSetDatabase objGetSetDatabase = new GetSetDatabase();
            if (objGetSetDatabase.SET_FlightRuleDetail("Truncate", ""))
                lblMsg.Text = "All rules are successfully deleted";

            else
                lblMsg.Text = "All rules are not successfully deleted";

        }
        catch (Exception ex)
        {
            lblMsg.Text = "All rules are not successfully deleted";
            throw ex;
        }
        finally
        {

        }
    }

    private DataTable CreateRulesDataTable()
    {
        DataTable dt = new DataTable
        {
            TableName = "Rules_FT_Type"
        };
        dt.Columns.Add("Rules_FT_ID", typeof(int));
        dt.Columns.Add("Rules_FT_From", typeof(string));
        dt.Columns.Add("Rules_FT_To", typeof(string));
        dt.Columns.Add("Rules_FT_AirV", typeof(string));
        dt.Columns.Add("Rules_FT_Provider", typeof(string));
        dt.Columns.Add("Rules_FT_Category", typeof(string));
        dt.Columns.Add("Rules_FT_Class", typeof(string));
        dt.Columns.Add("Rules_FT_Fare_Type", typeof(string));
        dt.Columns.Add("Rules_FT_Amount", typeof(double));
        dt.Columns.Add("Rules_FT_Amount_Type", typeof(string));
        dt.Columns.Add("Rules_FT_ValidFromDate", typeof(DateTime));
        dt.Columns.Add("Rules_FT_ValidToDate", typeof(DateTime));
        dt.Columns.Add("Rules_FT_Company_ID", typeof(string));
        dt.Columns.Add("Rules_FT_Camp_ID", typeof(string));
        dt.Columns.Add("Rules_FT_JourneyType", typeof(string));
        dt.Columns.Add("Rules_FT_ModifiedBy", typeof(string));
        dt.Columns.Add("Rules_FT_LastModifiedDate", typeof(DateTime));
        return dt;
    }

    private DataRow CreateRulesDataRow(DataRow dr, string SNO, string From, string To, string AirV, string Provider, string Category,
       string Class, string FareType, string Amount, string Amount_Type, string ValidFromDate, string ValidToDate, string CompanyID,
       string CampaignID, string JourneyType, string ModifiedBy, ref int ctr)
    {
        try
        {
            dr["Rules_FT_ID"] = SNO == "" ? ++ctr : Convert.ToInt32(SNO);
            dr["Rules_FT_From"] = From.ToUpper();
            dr["Rules_FT_To"] = To.ToUpper();
            dr["Rules_FT_AirV"] = AirV.ToUpper();
            dr["Rules_FT_Provider"] = Provider.ToUpper();
            dr["Rules_FT_Category"] = Category.ToUpper();
            dr["Rules_FT_Class"] = Class.ToUpper();
            dr["Rules_FT_Fare_Type"] = FareType.ToUpper();
            dr["Rules_FT_Amount"] = Amount == "" ? 0 : Convert.ToDouble(Amount);
            dr["Rules_FT_Amount_Type"] = Amount_Type.ToUpper();
            dr["Rules_FT_ValidFromDate"] = ValidFromDate == "" ? DateTime.Today : Convert.ToDateTime(ValidFromDate);
            dr["Rules_FT_ValidToDate"] = ValidToDate == "" ? DateTime.Today.AddYears(10) : Convert.ToDateTime(ValidToDate);
            dr["Rules_FT_Company_ID"] = CompanyID;
            dr["Rules_FT_Camp_ID"] = CampaignID;
            dr["Rules_FT_JourneyType"] = JourneyType.ToUpper();
            dr["Rules_FT_ModifiedBy"] = ModifiedBy;
            dr["Rules_FT_LastModifiedDate"] = DateTime.Now;
        }
        catch (Exception)
        {

        }
        finally
        {

        }
        return dr;
    }

    protected void btnExport1_Click(object sender, EventArgs e)
    {
        try
        {
            using (SqlConnection exportConn = DataConnection.GetConnectionMarkup())
            {
                DataSet dt = new DataSet();
                exportConn.Open();
                string cmdstr = "SELECT ISNULL([Rules_FT_ID], '') AS SN,ISNULL([Rules_FT_From], '') AS [From],ISNULL([Rules_FT_To],'') AS[To],ISNULL([Rules_FT_AirV], '') AS Airline ,isnull([Rules_FT_Provider],'') AS GDS,isnull([Rules_FT_Category],'') AS CabinClass,isnull([Rules_FT_Class],'') AS[Class],isnull([Rules_FT_Fare_Type],'') AS FareType,isnull([Rules_FT_Amount],'') AS Amount,isnull([Rules_FT_Amount_Type],'') AS AmountType,isnull([Rules_FT_ValidFromDate],'') AS ValidFromDate,isnull([Rules_FT_ValidToDate],'') AS ValidToDate,isnull([Rules_FT_Company_ID],'') AS Company_ID,isnull([Rules_FT_Camp_ID],'') AS Camp_ID,isnull([Rules_FT_JourneyType],'') AS JourneyType,isnull([Rules_FT_ModifiedBy],'') AS ModifiedBy,isnull([Rules_FT_LastModifiedDate],'') AS LastModifiedDate  FROM[RULES_FT]";
                SqlCommand cmd = new SqlCommand(cmdstr, exportConn);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);
                WriteExcelWithNPOI(dt.Tables[0], "xlsx");
                // ExporttoExcel(dt);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {

        }
    }

    [WebMethod(EnableSession = true)]
    public static string UpdateDisCountRules(string UseStatus)
    {
        try
        {
            GetSetDatabase objGetSetDatabase = new GetSetDatabase();
            return objGetSetDatabase.UpdateDisCountRules_FT(Convert.ToBoolean(UseStatus));
        }
        catch { return "false"; }
    }
    public bool CheckDisCountRules_FT()
    {
        try
        {
            GetSetDatabase objGetSetDatabase = new GetSetDatabase();
            return objGetSetDatabase.CheckDisCountRules_FT();

        }
        catch { return false; }
    }

    protected void linkbtn_Click(object sender, EventArgs e)
    {
        LinkButton button = sender as LinkButton;
        string rules_ID = button.ToolTip;

        try
        {
            GetSetDatabase objGetSetDatabase = new GetSetDatabase();
            if (objGetSetDatabase.SET_FlightRuleDetail("Copy", rules_ID))
            {
                bindRules();
                lblMsg.Visible = true;
                lblMsg.ForeColor = Color.Green;
                lblMsg.Text = "A row inserted successfully";
            }
            else
            {
                lblMsg.Visible = true;
                lblMsg.ForeColor = Color.Red;
                lblMsg.Text = "Copy failed";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Visible = true;
            lblMsg.ForeColor = Color.Red;
            lblMsg.Text = "Copy failed";
        }
        finally
        {

        }
    }
}
