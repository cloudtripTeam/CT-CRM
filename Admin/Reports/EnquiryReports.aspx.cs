using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Admin_Reports_EnquiryReports : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserDetails"] != null)
        {

            if (!IsPostBack)
            {
                UserDetail objUserDetail = Session["UserDetails"] as UserDetail;
                if (!objUserDetail.isAuth("Enquiry"))
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



    protected void btExcel_Click(object sender, EventArgs e)
    {
        try
        {
            if (HttpContext.Current.Session["EnquiryReport"] != null)
            {
                Common com = new Common();
                DataTable dt = (DataTable)HttpContext.Current.Session["EnquiryReport"];

                if (dt != null)
                {
                    com.ExporttoExcel(dt);
                }
            }
        }
        catch { }
    }


    [WebMethod(EnableSession = true)]
    public static string getCalls(string fromDate, string toDate, string company)
    {
        try
        {
            //string fromDate ="",  toDate = "", status ="",  sourceMedia = "",  company="";


            GetSetDatabase objGetSetDatabase = new GetSetDatabase();
            DataTable dt = objGetSetDatabase.GET_Call_Details("", "", "", "", "", "", "", fromDate,toDate,company, "EN");

            if (dt != null)
            {

                HttpContext.Current.Session["EnquiryReport"] = dt;

                var query = from row in dt.AsEnumerable()
                            group row by row.Field<string>("Status") into booking
                            orderby booking.Key
                            select new
                            {
                                Status = booking.Key,
                                NoOfBookings = booking.Count()
                            };

                DataTable dtBooking = new DataTable();
                dtBooking.Columns.Add("Status", typeof(String));
                dtBooking.Columns.Add("NoOfCalls", typeof(Int32));
                foreach (var booking in query)
                {
                    DataRow dr = dtBooking.NewRow();
                    dr["Status"] = booking.Status;
                    dr["NoOfCalls"] = booking.NoOfBookings;
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
    public static string getCallsByCompany()
    {
        try
        {
            if (HttpContext.Current.Session["EnquiryReport"] != null)
            {

                DataTable dt = (DataTable)HttpContext.Current.Session["EnquiryReport"];
                //GetSetDatabase objGetSetDatabase = new GetSetDatabase();
                //DataTable dt = objGetSetDatabase.GET_BookingDetail("", "", "", "FLTXPT", "",
                //    "2016-07-24", "2016-07-26", "", "", "",
                //    "", "", "", "", "", "", "", "");
                if (dt != null)
                {

                    HttpContext.Current.Session["EnquiryReport"] = dt;
                    var query = from row in dt.AsEnumerable()
                                //group row by new {company = row.Field<string>("BookingByCompany"), status=  row.Field<string>("BookingStatus")} into booking
                                group row by new { company = row.Field<string>("Brand_Name") } into booking
                                orderby booking.Key.company
                                select new
                                {
                                    Company = booking.Key.company,

                                    NoOfBookings = booking.Count()
                                };

                    DataTable dtBooking = new DataTable();
                    dtBooking.Columns.Add("Company", typeof(String));

                    dtBooking.Columns.Add("NoOfCalls", typeof(Int32));
                    foreach (var booking in query)
                    {
                        DataRow dr = dtBooking.NewRow();
                        dr["Company"] = booking.Company;
                        //dr["Status"] = booking.Status;
                        dr["NoOfCalls"] = booking.NoOfBookings;
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
    public static string getCallsBySource()
    {
        try
        {
            if (HttpContext.Current.Session["EnquiryReport"] != null)
            {

                DataTable dt = (DataTable)HttpContext.Current.Session["EnquiryReport"];
                //GetSetDatabase objGetSetDatabase = new GetSetDatabase();
                //DataTable dt = objGetSetDatabase.GET_BookingDetail("", "", "", "FLTXPT", "",
                //    "2016-07-24", "2016-07-26", "", "", "",
                //    "", "", "", "", "", "", "", "");
                if (dt != null)
                {

                    HttpContext.Current.Session["EnquiryReport"] = dt;
                    var query = from row in dt.AsEnumerable()
                                //group row by new {company = row.Field<string>("BookingByCompany"), status=  row.Field<string>("BookingStatus")} into booking
                                group row by new { company = row.Field<string>("Call_Source") } into booking
                                orderby booking.Key.company
                                select new
                                {
                                    Company = booking.Key.company,

                                    NoOfBookings = booking.Count()
                                };

                    DataTable dtBooking = new DataTable();
                    dtBooking.Columns.Add("Source", typeof(String));

                    dtBooking.Columns.Add("NoOfCalls", typeof(Int32));
                    foreach (var booking in query)
                    {
                        DataRow dr = dtBooking.NewRow();
                        dr["Source"] = booking.Company;
                        //dr["Status"] = booking.Status;
                        dr["NoOfCalls"] = booking.NoOfBookings;
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
    public static string getCallsByQuery()
    {
        try
        {
            if (HttpContext.Current.Session["EnquiryReport"] != null)
            {

                DataTable dt = (DataTable)HttpContext.Current.Session["EnquiryReport"];
                //GetSetDatabase objGetSetDatabase = new GetSetDatabase();
                //DataTable dt = objGetSetDatabase.GET_BookingDetail("", "", "", "FLTXPT", "",
                //    "2016-07-24", "2016-07-26", "", "", "",
                //    "", "", "", "", "", "", "", "");
                if (dt != null)
                {

                    HttpContext.Current.Session["EnquiryReport"] = dt;
                    var query = from row in dt.AsEnumerable()
                                //group row by new {company = row.Field<string>("BookingByCompany"), status=  row.Field<string>("BookingStatus")} into booking
                                group row by new { company = row.Field<string>("Reason_Of_Call") } into booking
                                orderby booking.Key.company
                                select new
                                {
                                    Company = booking.Key.company,

                                    NoOfBookings = booking.Count()
                                };

                    DataTable dtBooking = new DataTable();
                    dtBooking.Columns.Add("Query", typeof(String));

                    dtBooking.Columns.Add("NoOfCalls", typeof(Int32));
                    foreach (var booking in query)
                    {
                        DataRow dr = dtBooking.NewRow();
                        dr["Query"] = booking.Company;
                        //dr["Status"] = booking.Status;
                        dr["NoOfCalls"] = booking.NoOfBookings;
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
}