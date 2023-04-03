using System;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using ExcelLibrary;

using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;


public partial class Admin_OwnFares : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        if (Session["UserDetails"] != null)
        {
            if (!IsPostBack)
            {
                UserDetail objUserDetail = Session["UserDetails"] as UserDetail;
                if (objUserDetail.userRole.ToLower() == "admin" || objUserDetail.userRole.ToLower() == "superadmin" || objUserDetail.userID.ToLower() == "Dinesh".ToLower())
                {
                    btnDeleteAll.Enabled = true;
                }
                else
                {
                    btnDeleteAll.Enabled = false;
                }
                if (!objUserDetail.isAuth("Own Fares"))
                {
                    Response.Redirect("~/Admin/AccessDenied.aspx");
                    return;
                }
                else
                {
                    ddlCampaign.Items.Clear();
                    if (objUserDetail.userID.ToLower() == "samft" || objUserDetail.userID.ToLower() == "seanft" || objUserDetail.userID.ToLower() == "robft" || objUserDetail.userID.ToLower() == "TIWARI".ToLower() || objUserDetail.userID.ToLower() == "Dinesh".ToLower())
                    {
                        CommanBinding.BindCampaignDetails(ref ddlCampaign, "FLTTROTT");
                        ddlCampaign.Items.Add("FT_DLCKR");
                        ddlCampaign.Items.Add("FT_JC");
                        ddlCampaign.Items.RemoveAt(0);
                    }
                    else
                    {
                        CommanBinding.BindPreferedCampaign(ref ddlCampaign);
                    }
                }
                checkboxisActive.Checked = CheckDiscountOwnFare_UK();
                hfUpdatedBy.Value = objUserDetail.userID;
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx", false);
        }
    }

    public bool CheckDiscountOwnFare_UK()
    {
        try
        {
            GetSetDatabase objGetSetDatabase = new GetSetDatabase();
            return objGetSetDatabase.CheckDiscountOwnFare_UK();

        }
        catch { return false; }
    }

    [WebMethod(EnableSession = true)]
    public static string UpdateDiscountOwnFare_UK(string UseStatus, string Origin, string Destination, string Airline, string AirlineClass, string TripType, string OfferType, string Pax, string FlightType, string Campaign)
    {
        try
        {
            GetSetDatabase objGetSetDatabase = new GetSetDatabase();

            //Add For Log on/off Button Log
            //SqlConnection conn;
            //SqlCommand comm;
            //SqlConnection connection = DataConnection.GetConnectionMarkupUS();
            //string connstring = connection.ConnectionString;

            //conn = new SqlConnection(connstring);
            //conn.Open();
            //comm = new SqlCommand("INSERT INTO OwnFareStatus_Log(UserName,CreatedDate) values('" + UserId + "','" + DateTime.Now + "')", conn);
            //try
            //{
            //    comm.ExecuteNonQuery();
            //}
            //catch (Exception ex)
            //{

            //}
            //finally
            //{
            //    conn.Close();
            //}
            //End

            return objGetSetDatabase.UpdateDiscountOwnFare_UK(Convert.ToBoolean(UseStatus), Origin, Destination, Airline, AirlineClass, TripType, OfferType, Pax, FlightType, Campaign);
        }
        catch
        {
            return "false";
        }
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        GetSetDatabase objGetSetDatabase = new GetSetDatabase();
        UserDetail objUserDetail = HttpContext.Current.Session["UserDetails"] as UserDetail;

        string str = objGetSetDatabase.SET_ChangeFare_UK("", txtOrigin.Text.Trim().ToUpper(), txtDestination.Text.Trim().ToUpper(), txtTravelDateStart.Text.Trim().ToUpper(),
            txtTravelDateEnd.Text.Trim().ToUpper(), txtAieline.Text.Trim().ToUpper(), ddlCabinClass.SelectedValue, txtAirlineClass.Text.Trim().ToUpper(), txtBaseFare.Text.Trim(),
            txtTax.Text.Trim(), txtMarkup.Text.Trim(), txtCommssion.Text.Trim(), objUserDetail.userID, "Insert",
            ddlCampaign.SelectedValue, ddlOfferType.SelectedValue, ddlTripType.SelectedValue, ddlActive.SelectedValue, ddlFlightType.SelectedValue, txtModifydate.Text);

        if (str.ToLower() == "true")
        {
            lblMsg.Text = "Record is successfully inserted";
            rptrDetails.DataSource = objGetSetDatabase.GET_ChangeFare_UK("", txtOrigin.Text.Trim().ToUpper(), txtDestination.Text.Trim().ToUpper(),
                txtTravelDateStart.Text.Trim().ToUpper(), txtTravelDateEnd.Text.Trim().ToUpper(), txtAieline.Text.Trim().ToUpper(), ddlCabinClass.SelectedValue,
                txtAirlineClass.Text.Trim().ToUpper(), "", "Select", "", "", ddlCampaign.SelectedValue, ddlTripType.SelectedValue, Convert.ToBoolean(ddlActive.SelectedValue), ddlFlightType.SelectedValue);


            rptrDetails.DataBind();
            txtAieline.Text = "";
            txtAirlineClass.Text = "";
            txtDestination.Text = "";
            txtOrigin.Text = "";
            txtTravelDateEnd.Text = "";
            txtTravelDateStart.Text = "";
            txtBaseFare.Text = "0";
            txtTax.Text = "0";
            txtMarkup.Text = "0";
            // txtMarkupTJ.Text = "0";

            txtCommssion.Text = "0";
            ddlCabinClass.SelectedIndex = 0;
        }
        else
        {
            lblMsg.Text = "Record is not successfully inserted";
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Bind_OwnFare_DataGrid();
    }

    protected void rptrDetails_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "EditDetails")
        {
            GetSetDatabase objGetSetDatabase = new GetSetDatabase();
            DataTable dt = objGetSetDatabase.GET_ChangeFare_UK(e.CommandArgument.ToString(),txtOrigin.Text, txtDestination.Text,txtTravelDateStart.Text, txtTravelDateEnd.Text,txtAieline.Text,ddlCabinClass.SelectedValue, txtAirlineClass.Text, "","Select", "", "",ddlCampaign.SelectedValue, ddlTripType.SelectedValue, Convert.ToBoolean(ddlActive.SelectedValue), ddlFlightType.SelectedValue);
            if (dt != null && dt.Rows.Count > 0)
            {

                txtAieline.Text = dt.Rows[0]["Airline"].ToString();
                txtAirlineClass.Text = dt.Rows[0]["AClass"].ToString();
                txtDestination.Text = dt.Rows[0]["Destination"].ToString();
                txtOrigin.Text = dt.Rows[0]["Origin"].ToString();
                txtTravelDateEnd.Text = Convert.ToDateTime(dt.Rows[0]["FromDateEnd"]).ToString("dd/MM/yyyy");
                txtTravelDateStart.Text = Convert.ToDateTime(dt.Rows[0]["FromDateStart"]).ToString("dd/MM/yyyy");
                txtBaseFare.Text = Convert.ToDouble(dt.Rows[0]["BaseFare"]).ToString("f2");
                txtTax.Text = Convert.ToDouble(dt.Rows[0]["Tax"]).ToString("f2");
                txtMarkup.Text = Convert.ToDouble(dt.Rows[0]["Markup"]).ToString("f2");
                ddlCampaign.SelectedValue = dt.Rows[0]["Campaign"].ToString();
                string isActive = dt.Rows[0]["Active"].ToString();
                ddlActive.SelectedValue = isActive;
                ddlFlightType.SelectedValue = (Convert.ToString(dt.Rows[0]["FlightType"]));
                txtCommssion.Text = Convert.ToDouble(dt.Rows[0]["Commission"]).ToString("f2");
                ddlCabinClass.SelectedValue = dt.Rows[0]["CabinClass"].ToString();
                hfSrNo.Value = dt.Rows[0]["SrNo"].ToString();
                btnInsert.Visible = false;
                btnSearch.Visible = false;
                btnUpdate.Visible = true;
                btnCancel.Visible = true;
            }
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        GetSetDatabase objGetSetDatabase = new GetSetDatabase();
        UserDetail objUserDetail = HttpContext.Current.Session["UserDetails"] as UserDetail;
        string str = objGetSetDatabase.SET_ChangeFare_UK(hfSrNo.Value, txtOrigin.Text.Trim().ToUpper(), txtDestination.Text.Trim().ToUpper(), txtTravelDateStart.Text.Trim().ToUpper(),
            txtTravelDateEnd.Text.Trim().ToUpper(), txtAieline.Text.Trim().ToUpper(), ddlCabinClass.SelectedValue, txtAirlineClass.Text.Trim().ToUpper(), txtBaseFare.Text.Trim(),
            txtTax.Text.Trim(), txtMarkup.Text.Trim(), txtCommssion.Text.Trim(), objUserDetail.userID, "Update", "", "", "", ddlActive.SelectedValue, ddlFlightType.SelectedValue, txtModifydate.Text);
        if (str.ToLower() == "true")
        {
            lblMsg.Text = "Record is successfully Updated!!";
            rptrDetails.DataSource = objGetSetDatabase.GET_ChangeFare_UK("", txtOrigin.Text.Trim().ToUpper(), txtDestination.Text.Trim().ToUpper(),
                txtTravelDateStart.Text.Trim().ToUpper(), txtTravelDateEnd.Text.Trim().ToUpper(), txtAieline.Text.Trim().ToUpper(), ddlCabinClass.SelectedValue,
                txtAirlineClass.Text.Trim().ToUpper(), "", "Select", "", "", ddlCampaign.SelectedValue, "", Convert.ToBoolean(ddlActive.SelectedValue), ddlFlightType.SelectedValue);
            rptrDetails.DataBind();
            txtAieline.Text = "";
            txtAirlineClass.Text = "";
            txtDestination.Text = "";
            txtOrigin.Text = "";
            txtTravelDateEnd.Text = "";
            txtTravelDateStart.Text = "";
            txtBaseFare.Text = "0";
            txtTax.Text = "0";
            txtMarkup.Text = "0";
            txtCommssion.Text = "0";
            ddlCabinClass.SelectedIndex = 0;
            ddlCampaign.SelectedIndex = 0;
            btnInsert.Visible = true;
            btnSearch.Visible = true;
            btnUpdate.Visible = false;
            btnCancel.Visible = false;
        }
        else
        {
            lblMsg.Text = "Record is not successfully Updated!!";
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("OwnFares.aspx");
    }

    public void CopyFares(object sender, EventArgs e)
    {
        SqlConnection sqlcon = DataConnection.GetConnection();
        sqlcon.Open();
        DataSet ds = new DataSet();

        LinkButton button = sender as LinkButton;
        string id = button.ToolTip;
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            using (SqlConnection conection = DataConnection.GetConnection())
            {
                param[0] = new SqlParameter("@Id", SqlDbType.Int, 10);
                {
                    param[0].Value = id;
                }
                ds = SqlHelper.ExecuteDataset(conection, CommandType.StoredProcedure, "Insert_Copy_ChangeFare", param);

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        Common.ErrorClass.ErrorNumber = Convert.ToString(ds.Tables[0].Rows[0]["ErrorNumber"]);
                    }
                }

                if (Common.ErrorClass.ErrorNumber == "1")
                {
                    Bind_OwnFare_DataGrid();
                    lblMsg.Visible = true;
                    lblMsg.ForeColor = Color.Green;
                    lblMsg.Text = "A row inserted successfully";
                }
                else
                {
                    Bind_OwnFare_DataGrid();
                    lblMsg.Visible = true;
                    lblMsg.ForeColor = Color.Red;
                    lblMsg.Text = "Copy failed";
                }
            }
        }
        catch (Exception ex)
        {
            Common.ErrorClass.ErrorNumber = Convert.ToString(ds.Tables[0].Rows[0]["ErrorNumber"]);
            Common.ErrorClass.ErrorProcedure = Convert.ToString(ds.Tables[0].Rows[0]["ErrorProcedure"]);
            Common.ErrorClass.ErrorLine = Convert.ToString(ds.Tables[0].Rows[0]["ErrorLine"]);
            Common.ErrorClass.ErrorMessage = Convert.ToString(ds.Tables[0].Rows[0]["ErrorMessage"]);
        }
        finally
        {
            sqlcon.Close();
        }
    }



    public void Bind_OwnFare_DataGrid()
    {
        GetSetDatabase objGetSetDatabase = new GetSetDatabase();
        UserDetail objUserDetail = HttpContext.Current.Session["UserDetails"] as UserDetail;
        rptrDetails.DataSource = objGetSetDatabase.GET_ChangeFare_UK("", txtOrigin.Text.Trim().ToUpper(), txtDestination.Text.Trim().ToUpper(), txtTravelDateStart.Text,
        txtTravelDateEnd.Text, txtAieline.Text.Trim().ToUpper(), ddlCabinClass.SelectedValue, txtAirlineClass.Text.Trim().ToUpper(), "", "Select", txtModifyBy.Text,
        txtModifydate.Text, ddlCampaign.SelectedValue, ddlTripType.SelectedValue, Convert.ToBoolean(ddlActive.SelectedValue), ddlFlightType.SelectedValue);
        rptrDetails.DataBind();
    }


    [WebMethod(EnableSession = true)]
    public static string DeleteFares(string ID)
    {
        GetSetDatabase objGetSetDatabase = new GetSetDatabase();
        UserDetail objUserDetail = HttpContext.Current.Session["UserDetails"] as UserDetail;
        return objGetSetDatabase.DeleteOwnFares_UK(ID, "Delete").ToString().ToLower();

        //return objGetSetDatabase.DeleteFlightMarkup(ID, "").ToString().ToLower();
    }

    [WebMethod(EnableSession = true)]
    public static string UpdateFare(string ID, string UpdateField, string Value, string UpdatedBy)
    {
        GetSetDatabase objGetSetDatabase = new GetSetDatabase();
        UserDetail objUserDetail = HttpContext.Current.Session["UserDetails"] as UserDetail;
        return objGetSetDatabase.UpdateOwnFareUK_Excel(ID, UpdateField, Value, UpdatedBy);
    }



    #region Export in Excel

    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            using (SqlConnection exportConn = DataConnection.GetConnection())
            {
                DataSet dt = new DataSet();
                exportConn.Open();

                StringBuilder strcmd = new StringBuilder("SELECT  [Change_Fare_SrNo] AS [SrNo],isnull([Change_Fare_Origin],'') AS [From]  ,isnull([Change_Fare_Destination],'')  AS [To], isnull([Change_Fare_FromDateStart],'') as FromDate, isnull([Change_Fare_FromDateEnd],'')  As ToDate, isnull([Change_Fare_Airline],'') AS Airline, isnull([Change_Fare_CabinClass],'') AS CabinClass, isnull([Change_Fare_Class],'') AS [Class], isnull([Change_Fare_BaseFare],'') AS [Fare], isnull([Change_Fare_Tax],'') AS Tax, isnull([Change_Fare_Markup],'') as Markup, isnull([Change_Fare_Commission],'') AS  Commission, isnull([Change_Fare_ModifyBy],'') AS ModifyBy, isnull([Change_Fare_ModifyDateTime] ,'') AS ModifyDateTime, isnull([Change_Fare_Campaign],'') AS Campaign, isnull([Change_Fare_OfferType],'') AS OfferType, isnull([Change_Fare_TripType],'') AS TripType , [Change_Fare_Flight_type] as [FlightType]  FROM [ChangeFare]");
                strcmd.Append(" Where 1=1 ");

                //if (txtOrigin.Text != "-1")
                //{
                //    strcmd.Append(" and Change_Fare_TripType='" + ddlTripType.SelectedValue + "'");
                //}

                if (txtOrigin.Text != "")
                {
                    strcmd.Append(" and Change_Fare_Origin='" + txtOrigin.Text + "'");
                }

                if (txtDestination.Text != "")
                {
                    strcmd.Append(" and Change_Fare_Destination='" + txtDestination.Text + "'");
                }

                if (txtTravelDateStart.Text != "")
                {
                    strcmd.Append(" and Change_Fare_FromDateStart='" + txtTravelDateStart.Text + "'");
                }

                if (txtTravelDateEnd.Text != "")
                {
                    strcmd.Append(" and Change_Fare_FromDateEnd='" + txtTravelDateEnd.Text + "'");
                }

                if (txtAieline.Text != "")
                {
                    strcmd.Append(" and Change_Fare_Airline='" + txtAieline.Text + "'");
                }

                if (txtAirlineClass.Text != "")
                {
                    strcmd.Append(" and Change_Fare_Class='" + txtAirlineClass.Text + "'");
                }

                if (ddlCampaign.SelectedValue != "")
                {
                    strcmd.Append(" and Change_Fare_Campaign='" + ddlCampaign.SelectedValue + "'");
                }

                //if (ddlOfferType.SelectedValue != "")
                //{
                //    strcmd.Append(" and Change_Fare_OfferType='" + ddlOfferType.SelectedValue + "'");
                //}

                //if (ddlFlightType.SelectedValue != "-2")
                //{
                //    strcmd.Append(" and Change_Fare_Flight_type='" + ddlFlightType.SelectedValue + "'");
                //}


                SqlCommand cmd = new SqlCommand(strcmd.ToString(), exportConn);
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
            string fileName = "OwnFares_" + DateTime.Now.ToString("ddMMMyyyy");
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

    public void ExporttoExcel(System.Data.DataTable table)
    {
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.ClearHeaders();
        HttpContext.Current.Response.Buffer = true;
        HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        // HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=OwnFares_" + DateTime.Now + ".xlsx");

        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");


        HttpContext.Current.Response.Write(build(table));
        HttpContext.Current.Response.Flush();
        HttpContext.Current.Response.End();
    }



    private StringBuilder build(System.Data.DataTable table)
    {
        var sb = new StringBuilder();
        string tab = "";


        for (int j = 0; j < table.Columns.Count; j++)
        {

            sb.Append(table.Columns[j].ColumnName);
            if (!(j == (table.Columns.Count - 1)))
                sb.Append("\t");
        }

        sb.Append("\n");
        foreach (DataRow row in table.Rows)
        {
            for (int i = 0; i < table.Columns.Count; i++)
            {

                sb.Append(tab + row[i].ToString());

                if (!(i == (table.Columns.Count - 1)))
                    tab = "\t";
            }

            sb.Append("\n");
        }
        // sb.Append("</Table>");
        //sb.Append("</font>");
        return sb;
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
            //Import_To_Grid(FilePath, Extension, "Yes");
            SaveRows(FilePath, Extension, "Yes");
        }
        else
        {
            lblMsg.Text = "Please choose an excell own fare file to proceed";

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
            // string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
            string SheetName = "Sheet 1$";
            OleDbCon.Close();
            //OleDbCommand oledbCmd2 = new OleDbCommand("update [" + SheetName + "] set AirF_Markup_ModifiedBy='" + objUserDetail.userFirstName + "'", OleDbCon);
            System.Data.DataTable dt = CreateOwnFareDataTable();
            OleDbCommand OleDbCmd = new OleDbCommand("Select * FROM [" + SheetName + "]", OleDbCon);
            OleDbCon.Open();
            using (DbDataReader dr = OleDbCmd.ExecuteReader())
            {
                int ctr = 0;
                //OleDbCommand oleCMD2 = new OleDbCommand("select Max(SrNo) from [" + SheetName + "]", OleDbCon);
                // ctr = Convert.ToInt32(oleCMD2.ExecuteScalar());
                while (dr.Read())
                {

                    dt.Rows.Add(CreateOwnDataRow(dt.NewRow(), dr["SrNo"].ToString().Trim(), dr["From"].ToString().Trim(),
                        dr["To"].ToString().Trim(), dr["FromDate"].ToString().Trim(), dr["ToDate"].ToString().Trim(),
                        dr["Airline"].ToString().Trim(), dr["CabinClass"].ToString().Trim(), dr["Class"].ToString().Trim(),
                        dr["Fare"].ToString().Trim(), dr["Tax"].ToString().Trim(), dr["Markup"].ToString().Trim(),
                        dr["Commission"].ToString().Trim(), objUserDetail.userID, dr["Campaign"].ToString().Trim(),
                        dr["OfferType"].ToString().Trim(), dr["TripType"].ToString().Trim(), dr["FlightType"].ToString().Trim(), ref ctr));
                }

                using (SqlConnection SqlCon = DataConnection.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = SqlCon;
                        cmd.CommandTimeout = 240;
                        cmd.CommandText = "OwnFare_BulkCopy_UK";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ParamOwnFare", dt);
                        try
                        {
                            SqlCon.Open();
                            int rows = cmd.ExecuteNonQuery();
                            lblMsg.Text += " All (" + rows + "/" + ctr + " rows) Own Fares are successfully Uploaded";
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


    #region Own Fare Make Table
    private DataTable CreateOwnFareDataTable()
    {
        DataTable dt = new DataTable();
        dt.TableName = "OwnFares";
        dt.Columns.Add("Change_Fare_SrNo", typeof(Int32)).AllowDBNull = true;
        dt.Columns.Add("Change_Fare_Origin", typeof(String));
        dt.Columns.Add("Change_Fare_Destination", typeof(String));
        dt.Columns.Add("Change_Fare_FromDateStart", typeof(DateTime));
        dt.Columns.Add("Change_Fare_FromDateEnd", typeof(DateTime));
        dt.Columns.Add("Change_Fare_Airline", typeof(String));
        dt.Columns.Add("Change_Fare_CabinClass", typeof(String));
        dt.Columns.Add("Change_Fare_Class", typeof(String));
        dt.Columns.Add("Change_Fare_BaseFare", typeof(Double));
        dt.Columns.Add("Change_Fare_Tax", typeof(Double));
        dt.Columns.Add("Change_Fare_Markup", typeof(Double));
        dt.Columns.Add("Change_Fare_Commission", typeof(Double));
        dt.Columns.Add("Change_Fare_ModifyBy", typeof(String));
        dt.Columns.Add("Change_Fare_ModifyDateTime", typeof(DateTime));
        dt.Columns.Add("Change_Fare_MarkupTJ", typeof(Double));
        dt.Columns.Add("Change_Fare_MarkupXPT", typeof(Double));
        dt.Columns.Add("Change_Fare_Campaign", typeof(String));
        dt.Columns.Add("Change_Fare_OfferType", typeof(String));
        dt.Columns.Add("Change_Fare_TripType", typeof(String));
        dt.Columns.Add("Change_Fare_No_Of_Pax", typeof(String));
        dt.Columns.Add("Change_Fare_Flight_type", typeof(String));
        return dt;
    }

    private DataRow CreateOwnDataRow(DataRow dr, string SNO, string From, string To, string ValidFromDate, string ValidToDate, string AirV, string Category,
        string Class, string basefare, string tax, string markup, string commission, string ModifiedBy,
        string CampaignID, string offerType, string TripType, string flightType, ref int ctr)
    {
        try
        {

            dr["Change_Fare_SrNo"] = SNO == "" ? -1 : Convert.ToInt32(SNO);
            dr["Change_Fare_Origin"] = From.ToUpper();
            dr["Change_Fare_Destination"] = To.ToUpper();
            dr["Change_Fare_FromDateStart"] = ValidFromDate == "" ? DateTime.Today : Convert.ToDateTime(ValidFromDate);
            dr["Change_Fare_FromDateEnd"] = ValidToDate == "" ? DateTime.Today.AddYears(10) : Convert.ToDateTime(ValidToDate);
            dr["Change_Fare_Airline"] = AirV.ToUpper();
            dr["Change_Fare_CabinClass"] = Category.ToUpper();
            dr["Change_Fare_Class"] = Class.ToUpper();
            dr["Change_Fare_BaseFare"] = basefare == "" ? 0 : Convert.ToDouble(basefare);
            dr["Change_Fare_Tax"] = tax == "" ? 0 : Convert.ToDouble(tax);
            dr["Change_Fare_Markup"] = markup == "" ? 0 : Convert.ToDouble(markup);
            dr["Change_Fare_Commission"] = commission == "" ? 0 : Convert.ToDouble(commission);
            dr["Change_Fare_ModifyBy"] = ModifiedBy;
            dr["Change_Fare_ModifyDateTime"] = DateTime.Now;
            dr["Change_Fare_MarkupTJ"] = markup == "" ? 0 : Convert.ToDouble(markup);
            dr["Change_Fare_MarkupXPT"] = markup == "" ? 0 : Convert.ToDouble(markup);
            dr["Change_Fare_Campaign"] = CampaignID;
            dr["Change_Fare_OfferType"] = offerType.ToUpper();
            dr["Change_Fare_TripType"] = TripType.ToUpper();
            dr["Change_Fare_No_Of_Pax"] = 1;
            dr["Change_Fare_Flight_type"] = flightType;
            ctr++;

        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
        }
        return dr;
    }

    #endregion

    protected void btnDeleteAll_Click(object sender, EventArgs e)
    {
        //string exportString = ConfigurationManager.ConnectionStrings["SqlServices"].ConnectionString.ToString();
        if (ddlCampaign.SelectedValue != "")
        {
            DeleteSeletedFare(ddlCampaign.SelectedValue);
        }
        else
        {
            truncateRecords();
        }
    }

    private void truncateRecords()
    {
        try
        {

            GetSetDatabase objGetSetDatabase = new GetSetDatabase();
            if (objGetSetDatabase.DeleteOwnFares_UK("", "Truncate") == "true")
                lblMsg.Text = "All Own Fares are successfully deleted";
            else
                lblMsg.Text = "All Own Fares are not successfully deleted";

        }
        catch (Exception ex)
        {
            lblMsg.Text = "All Own Fares are not successfully deleted";
            throw ex;
        }
    }

    private void DeleteSeletedFare(string CompaignID)
    {
        try
        {
            using (SqlConnection exportConn = DataConnection.GetConnection())
            {
                System.Data.DataSet dt = new System.Data.DataSet();
                exportConn.Open();
                string filter = string.Empty;
                string cmdstr = "Delete FROM [ChangeFare] WHERE Change_Fare_Campaign = '" + CompaignID + "'";

                SqlCommand cmd = new SqlCommand(cmdstr, exportConn);
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                    lblMsg.Text = "All Own Fares are successfully deleted";
                else
                    lblMsg.Text = "All Own Fares are not successfully deleted";

            }
        }
        catch (Exception ex)
        {
            throw ex;

        }

    }
}
