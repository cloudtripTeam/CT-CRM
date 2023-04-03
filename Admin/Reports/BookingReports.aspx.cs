using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using ExcelLibrary;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

public partial class Admin_Reports_BookingReports : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserDetails"] != null)
        {

            if (!IsPostBack)
            {
                UserDetail objUserDetail = Session["UserDetails"] as UserDetail;
                if (!objUserDetail.isAuth("BookingDetails.aspx"))
                {
                    Response.Redirect("Default.aspx");
                    return;
                }
                CommanBinding.BindCompanyDetails(ref ddlCompany, objUserDetail.userID);
                txtFromDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
               // BookingReport();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx", false);
        }
    }
    //protected void btnSearch_Click(object sender, EventArgs e)
    //{
    //    SearchBookingDetails();
    //}

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSourceMedia.Items.Clear();
        ddlSourceMedia.Items.Insert(0, new ListItem("Select Campaign", ""));
        if (ddlCompany.SelectedIndex != 0)
        {
            CommanBinding.BindCampaignDetails(ref ddlSourceMedia, ddlCompany.SelectedValue);
        }
       
    }

     [WebMethod(EnableSession = true)]
    public static string BookingReportByStatus(string fromDate , string  toDate,string  status ,  string  company)
    {
        try
        {
           //string fromDate ="",  toDate = "", status ="",  sourceMedia = "",  company="";


                GetSetDatabase objGetSetDatabase = new GetSetDatabase();
                DataTable dt = objGetSetDatabase.GET_BookingDetail("", "", "", company, "",
                    fromDate, toDate, status, "", "",
                    "", "", "", "", "", "", "", "","");

                if (dt != null)
                {

                    HttpContext.Current.Session["Bookings"] = dt;
                   
                    var query = from row in dt.AsEnumerable()
                                group row by row.Field<string>("BookingStatus") into booking
                                orderby booking.Key
                                select new
                                {
                                    Status = booking.Key,
                                    NoOfBookings = booking.Count()
                                };

                    DataTable dtBooking = new DataTable();
                    dtBooking.Columns.Add("Status", typeof(String));
                    dtBooking.Columns.Add("NoOfBookings", typeof(Int32));
                    foreach (var booking in query)
                    {
                        DataRow dr = dtBooking.NewRow();
                        dr["Status"] = booking.Status;
                        dr["NoOfBookings"] = booking.NoOfBookings;
                        dtBooking.Rows.Add(dr);

                    }
                    return CommanBinding.ConvertDataTableToJSON(dtBooking);
                }
                else
                { return ""; }
            
        }
        catch { return ""; }

    }

     [WebMethod(EnableSession = true)]
     public static string BookingReportByCompany()
     {
         try
         {
             if (HttpContext.Current.Session["Bookings"] != null)
             {

                 DataTable dt = (DataTable)HttpContext.Current.Session["Bookings"];
             //GetSetDatabase objGetSetDatabase = new GetSetDatabase();
             //DataTable dt = objGetSetDatabase.GET_BookingDetail("", "", "", "FLTXPT", "",
             //    "2016-07-24", "2016-07-26", "", "", "",
             //    "", "", "", "", "", "", "", "");
                 if (dt != null)
                 {

                     HttpContext.Current.Session["Bookings"] = dt;
                     var query = from row in dt.AsEnumerable()
                                 //group row by new {company = row.Field<string>("BookingByCompany"), status=  row.Field<string>("BookingStatus")} into booking
                                 group row by new { company = row.Field<string>("BookingByCompany")} into booking
                                 orderby booking.Key.company
                                 select new
                                 {
                                     Company = booking.Key.company,
                                    
                                     NoOfBookings = booking.Count()
                                 };

                     DataTable dtBooking = new DataTable();
                     dtBooking.Columns.Add("Company", typeof(String));
                     
                     dtBooking.Columns.Add("NoOfBookings", typeof(Int32));
                     foreach (var booking in query)
                     {
                         DataRow dr = dtBooking.NewRow();
                         dr["Company"] = booking.Company;
                         //dr["Status"] = booking.Status;
                         dr["NoOfBookings"] = booking.NoOfBookings;
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
     public static string BookingReportByCompanyCampaign()
     {
         try
         {
             if (HttpContext.Current.Session["Bookings"] != null)
             {

                 DataTable dt = (DataTable)HttpContext.Current.Session["Bookings"];
                 //GetSetDatabase objGetSetDatabase = new GetSetDatabase();
                 //DataTable dt = objGetSetDatabase.GET_BookingDetail("", "", "", "FLTXPT", "",
                 //    "2016-07-24", "2016-07-26", "", "", "",
                 //    "", "", "", "", "", "", "", "");
                 if (dt != null)
                 {

                     HttpContext.Current.Session["Bookings"] = dt;
                     var query = from row in dt.AsEnumerable()
                                 //group row by new {company = row.Field<string>("BookingByCompany"), status=  row.Field<string>("BookingStatus")} into booking
                                 group row by new {  campaign = row.Field<string>("SourceMedia") } into booking
                                 orderby booking.Key.campaign
                                 select new
                                 {
                                     Campaign = booking.Key.campaign,

                                     NoOfBookings = booking.Count()
                                 };

                     DataTable dtBooking = new DataTable();
                     dtBooking.Columns.Add("Campaign", typeof(String));

                     dtBooking.Columns.Add("NoOfBookings", typeof(Int32));
                     foreach (var booking in query)
                     {
                         DataRow dr = dtBooking.NewRow();
                         dr["Campaign"] = booking.Campaign;
                         //dr["Status"] = booking.Status;
                         dr["NoOfBookings"] = booking.NoOfBookings;
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
     public static string BookingReportByDestination()
     {
         try
         {
             if (HttpContext.Current.Session["Bookings"] != null)
             {

                 DataTable dt = (DataTable)HttpContext.Current.Session["Bookings"];
                 
                 if (dt != null)
                 {

                     HttpContext.Current.Session["Bookings"] = dt;
                     var query = from row in dt.AsEnumerable()
                                 //group row by new {company = row.Field<string>("BookingByCompany"), status=  row.Field<string>("BookingStatus")} into booking
                                 group row by new { destination = row.Field<string>("Destination") } into booking
                                 orderby booking.Key.destination
                                 select new
                                 {
                                     Destination = booking.Key.destination,

                                     NoOfBookings = booking.Count()
                                 };

                     DataTable dtBooking = new DataTable();
                     dtBooking.Columns.Add("Destination", typeof(String));

                     dtBooking.Columns.Add("NoOfBookings", typeof(Int32));
                     foreach (var booking in query)
                     {
                         DataRow dr = dtBooking.NewRow();
                         dr["Destination"] = booking.Destination;
                         //dr["Status"] = booking.Status;
                         dr["NoOfBookings"] = booking.NoOfBookings;
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
     public static string BookingReportByWeekDays()
     {
         try
         {
             if (HttpContext.Current.Session["Bookings"] != null)
             {

                 DataTable dt = (DataTable)HttpContext.Current.Session["Bookings"];
                 //GetSetDatabase objGetSetDatabase = new GetSetDatabase();
                 //DataTable dt = objGetSetDatabase.GET_BookingDetail("", "", "", "FLTXPT", "",
                 //    "2016-07-24", "2016-07-26", "", "", "",
                 //    "", "", "", "", "", "", "", "");
                 if (dt != null)
                 {

                     HttpContext.Current.Session["Bookings"] = dt;
                     var query = from row in dt.AsEnumerable()
                                 //group row by new {company = row.Field<string>("BookingByCompany"), status=  row.Field<string>("BookingStatus")} into booking
                                 group row by new { campaign = Convert.ToDateTime(row.Field<DateTime>("BookingDateTime")).DayOfWeek.ToString() } into booking
                                 orderby booking.Key.campaign
                                 select new
                                 {
                                     Campaign = booking.Key.campaign,

                                     NoOfBookings = booking.Count()
                                 };

                     DataTable dtBooking = new DataTable();
                     dtBooking.Columns.Add("WeekDays", typeof(String));

                     dtBooking.Columns.Add("NoOfBookings", typeof(Int32));
                     foreach (var booking in query)
                     {
                         DataRow dr = dtBooking.NewRow();
                         dr["WeekDays"] = booking.Campaign;
                         //dr["Status"] = booking.Status;
                         dr["NoOfBookings"] = booking.NoOfBookings;
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
     public static string BookingReportByTime()
     {
         try
         {
             if (HttpContext.Current.Session["Bookings"] != null)
             {

                 DataTable dt = (DataTable)HttpContext.Current.Session["Bookings"];

                 if (dt != null)
                 {

                     HttpContext.Current.Session["Bookings"] = dt;
                     var query = from row in dt.AsEnumerable()
                                 //group row by new {company = row.Field<string>("BookingByCompany"), status=  row.Field<string>("BookingStatus")} into booking
                                 group row by new { Time = Convert.ToInt16(Math.Ceiling(((Convert.ToDateTime(row.Field<DateTime>("BookingDateTime")).TimeOfDay).TotalHours) / 6)) } into booking
                                 orderby booking.Key.Time
                                 select new
                                 {
                                     Time = booking.Key.Time,

                                     NoOfBookings = booking.Count()
                                 };

                     DataTable dtBooking = new DataTable();
                     dtBooking.Columns.Add("Time", typeof(String));

                     dtBooking.Columns.Add("NoOfBookings", typeof(Int32));
                     foreach (var booking in query)
                     {
                         DataRow dr = dtBooking.NewRow();
                         dr["Time"] = getTimeRange(booking.Time);
                         //dr["Status"] = booking.Status;
                         dr["NoOfBookings"] = booking.NoOfBookings;
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
     private static string  getTimeRange(int time)
     {
         string timerange = string.Empty;
         switch (time)
         { 
             case 1:
                 timerange = "0-6";
                 break;
             case 2:
                 timerange = "7-12";
                 break;
             case 3:
                 timerange = "13-18";
                 break;
             case 4:
                 timerange = "19-24";
                 break;       
         
         
         }
         return timerange;
     }

     



     protected void btExcel_Click(object sender, EventArgs e)
     {
         try
         {
             if (HttpContext.Current.Session["Bookings"] != null)
             {
                 Common com = new Common();
                 DataTable dt = (DataTable)HttpContext.Current.Session["Bookings"];

                 if (dt != null)
                 {
                    
                    WriteExcelWithNPOI(dt, "xlsx");
                    //com.ExporttoExcel(dt);
                 }
             }
         }
         catch { }
     }

    private void WriteExcelWithNPOI(DataTable dt, String extension)
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
            string fileName = "BookingReport_" + DateTime.Now.ToString("ddMMMyyyy");
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