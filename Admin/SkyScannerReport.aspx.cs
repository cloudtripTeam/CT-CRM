using System;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Services;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data.Common;
using System.Text;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;


public partial class Admin_Default2 : CompressedPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserDetails"] != null)
        {
            if (!IsPostBack)
            {
                UserDetail objUserDetail = Session["UserDetails"] as UserDetail;
                if (!objUserDetail.isAuth("Markup.aspx"))
                {
                    Response.Redirect("~/Admin/AccessDenied.aspx");
                    return;
                }

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
        return objGetSetDatabase.DeleteFlightMarkup(ID, "Delete").ToString().ToLower();
    }

    [WebMethod(EnableSession = true)]
    public static string UpdateMarkupExcel(string ID, string UpdateField, string Value, string UpdatedBy)
    {
        try
        {
            GetSetDatabase objGetSetDatabase = new GetSetDatabase();

            return objGetSetDatabase.UpdateMarkupExcel(ID, UpdateField, Value.Trim().ToUpper(), UpdatedBy);
        }
        catch { return "false"; }
    }
    #endregion


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindMarkUp();
    }
    private void bindMarkUp()
    {


        if (string.IsNullOrEmpty(txtMarkupFrom.Text))
        {
            txtMarkupFrom.Text = Convert.ToString(DateTime.Now.Date);
        }

        if (string.IsNullOrEmpty(txtMarkupTo.Text))
        {
            txtMarkupTo.Text = Convert.ToString(DateTime.Now.Date);
        }


        GetSetDatabase objGetSetDatabase = new GetSetDatabase();
        UserDetail objUserDetail = HttpContext.Current.Session["UserDetails"] as UserDetail;

        DataTable dt = objGetSetDatabase.GET_SkyScannerDetail(txtMarkupFrom.Text, txtMarkupTo.Text);


        rptrDetails.Visible = false;
        if (dt != null && dt.Rows.Count > 0)
        {
            ViewState["Makups"] = dt;
            btnExport1.Visible = true;

            {
                rptrDetails.DataSource = dt;
                rptrDetails.DataBind();

                rptrDetails.Visible = true;
            }
        }
        else
        {
            lblMsg.Text = "No any record found for given searching criteria";
        }

    }

    protected void btnExport1_Click(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)ViewState["Makups"];
        if (dt != null)
        {


            string attachment = "attachment; filename=" + "SkyScanner_" + DateTime.Now.ToString("ddMMMyyyy") + ".xls";
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
        try
        {
            using (SqlConnection exportConn = DataConnection.GetConnectionMarkup())
            {
                System.Data.DataSet dt = new System.Data.DataSet();
                exportConn.Open();
                string cmdstr = "SELECT  isnull([Agnt_AirF_Markup_ID],'') AS SNo, isnull([Agnt_AirF_Markup_From],'') AS [From], isnull([Agnt_AirF_Markup_To],'') AS [To], isnull([Agnt_AirF_Markup_AirV],'') AS Airline," +
                "isnull([Agnt_AirF_Markup_Provider],'') AS GDS, isnull([Agnt_AirF_Markup_Category],'') AS CabinClass, isnull([Agnt_AirF_Markup_Class],'') AS [Class], isnull([Agnt_AirF_Markup_Fare_Type],'') AS FareType," +
                "isnull([Agnt_AirF_Markup_Amount],'') AS Amount, isnull([Agnt_AirF_Markup_Amount_Type],'') AS AmountType, isnull([Agnt_AirF_Markup_ValidFromDate],'') AS ValidFromDate," +
                "isnull([Agnt_AirF_Markup_ValidToDate],'') AS ValidToDate, isnull([COMP_DTL_Company_ID],'') AS Company_ID, isnull([Camp_ID],'') AS  Camp_ID, isnull([Agnt_AirF_Markup_JourneyType],'') AS JourneyType," +
                "isnull([Agnt_AirF_Markup_ModifiedBy],'') AS ModifiedBy, isnull([Agnt_AirF_Markup_LastModifiedDate],'') AS LastModifiedDate  FROM [FlightMarkupDetail]";
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






}