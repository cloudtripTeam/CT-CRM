using System;
using System.IO;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web;
using System.Web.Services;
using System.Linq;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;

public partial class Admin_Tracker : System.Web.UI.Page
{
    GetSetDatabase objGetSetDatabase = new GetSetDatabase();
    private static string _ipAddress = string.Empty;
    public static string ipAddress
    {
        get { return _ipAddress; }
        set { _ipAddress = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {        
        if (Session["UserDetails"] != null)
        {
            if (!IsPostBack)
            {
                UserDetail objUserDetail = Session["UserDetails"] as UserDetail;
                if (!objUserDetail.isAuth("Tracker"))
                {
                    Response.Redirect("~/Admin/AccessDenied.aspx");
                    return;
                }  
                BindCompany();

                txtHitFrom.Text = DateTime.Today.ToString("dd/MM/yyyy");
                txtHitTo.Text = DateTime.Today.ToString("dd/MM/yyyy");
                //bindDetails();
                HttpContext.Current.Session["Traffics"] = null;

            }
        }
        else
        {
            Response.Redirect("Login.aspx", false);
        }
    }
    private void BindCompany()
    {
         UserDetail objUserDetail = Session["UserDetails"] as UserDetail;
         CommanBinding.BindCompanyDetails(ref ddlCompany, objUserDetail.userID);
        
    }
    private void bindDetails()
    {
        GetSetDatabase objGetSetDatabase = new GetSetDatabase();
        DataTable dt = objGetSetDatabase.SearchPageTrackerOnline(txtOrigin.Text.Trim(), txtDestination.Text.Trim(), txtDepartFrom.Text.Trim(), txtDepartTo.Text.Trim(), 
            txtReturnFrom.Text.Trim(), txtReturnTo.Text.Trim(),CommanBinding.GetCompanyCodes(ddlCompany), txtHitFrom.Text.Trim(), txtHitTo.Text.Trim(), "", txtIP.Text.Trim(),
            ddlPage.SelectedValue, "", "", "", "", "", ddlSourceMedia.SelectedValue, "","", "Select");

        if (ddlCompany.SelectedValue != "")
        {
            var result = from r in dt.AsEnumerable()
                         where r.Field<string>("COMP_DTL_Company_ID") == ddlCompany.SelectedValue
                         select r;
            if (result.Count() > 0)
            {
                dt = result.CopyToDataTable();
            }
        }

        if (ddlSourceMedia.SelectedValue != "")
        {
            var result1 = from r in dt.AsEnumerable()
                            where 
                                r.Field<string>("ReqSource") == ddlSourceMedia.SelectedValue
                            select r;
            if (result1.Count() > 0)
            {
                dt = result1.CopyToDataTable();
            }
        }

        //According to Rohan Change by Dinesh on 05 Feb 2021
        if (ddlCompany.SelectedValue == "FLTTROTT" || ddlCompany.SelectedValue == "TRAVELOFLIUK"
           || ddlCompany.SelectedValue == "TRVJUNCTION_USA" || ddlCompany.SelectedValue == "C2BUS")
        {
            var resultPage = from r in dt.AsEnumerable()
                             where r.Field<string>("Page") == "passengerDetails.aspx" || r.Field<string>("Page") == "passengerdetails.aspx" || r.Field<string>("Page") == "flight-result.aspx"
                             select r;
            if (resultPage.Count() > 0)
            {
                dt = resultPage.CopyToDataTable();

            }
            lblTrackerClick.Text = "Total Tracker Clicks: " + Convert.ToString(dt.Rows.Count);
        }
        else
        {
            lblTrackerClick.Text = "";
        }

        //DataView dv = dt.DefaultView;
        //dv.Sort = "DatenTime desc, Page desc";
        //DataTable sortedDT = dv.ToTable();

        Session["Traffics"] = dt;
        if (chkDup.Checked)
        {

           dt= dt.DefaultView.ToTable(true, "IPAddress");


            var uniqueRows = dt.AsEnumerable()
                //.GroupBy(r => r.Field<string>("IPAddress"), r => r.Field<string>("Page"), r => r.Field<string>("ReqSource"))
                      //.GroupBy(r => new { IPAddress = r["IPAddress"], Page = r["Page"], ReqSource = r["ReqSource"],  Destination = r["Destination"] })
                       .GroupBy(r => new { IPAddress = r["IPAddress"] })                      
                       .Select(g => g.FirstOrDefault());
            dt = uniqueRows.CopyToDataTable();
        }       

        if (dt != null)
        {
           
            rptrTrack.DataSource = dt;
           // rptrTrack.DataSource = uniqueRows.ToList();
            rptrTrack.DataBind();
            btnExport.Visible = true;

        }
        else
        {
            lblMsg.Text = "There is no record found as per your searching criteria.";
            btnExport.Visible = false;
        }
    }
    protected void btnSearchTrack_Click(object sender, EventArgs e)
    {
        bindDetails();
    }

   
    
    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnExport.Visible = false;
        if (ddlCompany.SelectedIndex == 0)
        {
            ddlSourceMedia.Items.Clear();
            ddlSourceMedia.Items.Insert(0, new ListItem("Select", ""));
        }
        else
        {
            GetSetDatabase objGetSetDatabase = new GetSetDatabase();
            DataTable dt = objGetSetDatabase.GET_Campaign_Master("", ddlCompany.SelectedValue);
            ddlSourceMedia.Items.Clear();
            if (dt != null)
            {
                ddlSourceMedia.DataSource = dt;
                ddlSourceMedia.DataValueField = "CampID";
                ddlSourceMedia.DataTextField = "CampName";
                ddlSourceMedia.DataBind();

            }
            ddlSourceMedia.Items.Insert(0, new ListItem("Select", ""));
        }
        lblTrackerClick.Text = "";
    }

    [WebMethod(EnableSession = true)]
    public static string TrafficByCountry()
    {
        try
        {
            if (HttpContext.Current.Session["Traffics"] != null)
            {

                DataTable dt = (DataTable)HttpContext.Current.Session["Traffics"];

                if (dt != null)
                {


                    var query = from row in dt.AsEnumerable()
                                    //group row by new {company = row.Field<string>("BookingByCompany"), status=  row.Field<string>("BookingStatus")} into booking
                                group row by new { country = row.Field<string>("IPCountry") } into traffic
                                orderby traffic.Key.country
                                select new
                                {
                                    Country = traffic.Key.country,

                                    NoOfHits = traffic.Count()
                                };

                    DataTable dtBooking = new DataTable();
                    dtBooking.Columns.Add("Country", typeof(String));

                    dtBooking.Columns.Add("NoOfHits", typeof(Int32));
                    foreach (var booking in query)
                    {
                        DataRow dr = dtBooking.NewRow();
                        dr["Country"] = (booking.Country == "" || booking.Country == null) ? "NA" : booking.Country;
                        //dr["Status"] = booking.Status;
                        dr["NoOfHits"] = booking.NoOfHits;
                        dtBooking.Rows.Add(dr);

                    }
                    return CommanBinding.ConvertDataTableToJSON(dtBooking);
                }
                else
                { return ""; }
            }
            else
                return "";
        }
        catch { return ""; }

    }

    [WebMethod(EnableSession = true)]
    public static string TrafficFromCity()
    {
        try
        {
            if (HttpContext.Current.Session["Traffics"] != null)
            {

                DataTable dt = (DataTable)HttpContext.Current.Session["Traffics"];

                if (dt != null)
                {


                    var query = from row in dt.AsEnumerable()
                                    //group row by new {company = row.Field<string>("BookingByCompany"), status=  row.Field<string>("BookingStatus")} into booking
                                group row by new { city = row.Field<string>("IPCiry") } into traffic
                                orderby traffic.Key.city
                                select new
                                {
                                    City = traffic.Key.city,

                                    NoOfHits = traffic.Count()
                                };

                    DataTable dtBooking = new DataTable();
                    dtBooking.Columns.Add("City", typeof(String));

                    dtBooking.Columns.Add("NoOfHits", typeof(Int32));
                    foreach (var booking in query)
                    {
                        DataRow dr = dtBooking.NewRow();
                        dr["City"] = (booking.City == "" || booking.City == null) ? "NA" : booking.City;
                        //dr["Status"] = booking.Status;
                        dr["NoOfHits"] = booking.NoOfHits;
                        dtBooking.Rows.Add(dr);

                    }
                    return CommanBinding.ConvertDataTableToJSON(dtBooking);
                }
                else
                { return ""; }
            }
            else
                return "";
        }
        catch { return ""; }

    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = (DataTable)Session["Traffics"];
            WriteExcelWithNPOI(dt, "xlsx");

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void WriteExcelWithNPOI(DataTable dtOld, string extension)
    {

        string[] selectedColumns = new[] { "IPAddress", "DatenTime", "Company_Name", "ReqSource", "Page", "Origin", "Destination", "DepartDate", "ReturnDate" };
        DataTable dt = new DataView(dtOld).ToTable(false, selectedColumns);


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
            string fileName = "Tracker_" + DateTime.Now.ToString("ddMMMyyyy");
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
}
