using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Reports_CallReports : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserDetails"] != null)
        {

            if (!IsPostBack)
            {
                UserDetail objUserDetail = Session["UserDetails"] as UserDetail;
                if (!objUserDetail.isAuth("Calls"))
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
            if (HttpContext.Current.Session["CallsReport"] != null)
            {
                Common com = new Common();
                DataTable dt = (DataTable)HttpContext.Current.Session["CallsReport"];

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
            DataTable dt = objGetSetDatabase.GET_Call_Details("", "", "", "", "", "", "", fromDate,toDate,company, "CL");

            if (dt != null)
            {
                
                HttpContext.Current.Session["CallsReport"] = dt;

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
            if (HttpContext.Current.Session["CallsReport"] != null)
            {

                DataTable dt = (DataTable)HttpContext.Current.Session["CallsReport"];
                //GetSetDatabase objGetSetDatabase = new GetSetDatabase();
                //DataTable dt = objGetSetDatabase.GET_BookingDetail("", "", "", "FLTXPT", "",
                //    "2016-07-24", "2016-07-26", "", "", "",
                //    "", "", "", "", "", "", "", "");
                if (dt != null)
                {

                    HttpContext.Current.Session["CallsReport"] = dt;
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
            if (HttpContext.Current.Session["CallsReport"] != null)
            {

                DataTable dt = (DataTable)HttpContext.Current.Session["CallsReport"];
                //GetSetDatabase objGetSetDatabase = new GetSetDatabase();
                //DataTable dt = objGetSetDatabase.GET_BookingDetail("", "", "", "FLTXPT", "",
                //    "2016-07-24", "2016-07-26", "", "", "",
                //    "", "", "", "", "", "", "", "");
                if (dt != null)
                {

                    HttpContext.Current.Session["CallsReport"] = dt;
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
            if (HttpContext.Current.Session["CallsReport"] != null)
            {

                DataTable dt = (DataTable)HttpContext.Current.Session["CallsReport"];
                //GetSetDatabase objGetSetDatabase = new GetSetDatabase();
                //DataTable dt = objGetSetDatabase.GET_BookingDetail("", "", "", "FLTXPT", "",
                //    "2016-07-24", "2016-07-26", "", "", "",
                //    "", "", "", "", "", "", "", "");
                if (dt != null)
                {

                    HttpContext.Current.Session["CallsReport"] = dt;
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

    [WebMethod(EnableSession = true)]
    public static string getCallsByDestination()
    {
        try
        {
            if (HttpContext.Current.Session["CallsReport"] != null)
            {

                DataTable dt = (DataTable)HttpContext.Current.Session["CallsReport"];
                //GetSetDatabase objGetSetDatabase = new GetSetDatabase();
                //DataTable dt = objGetSetDatabase.GET_BookingDetail("", "", "", "FLTXPT", "",
                //    "2016-07-24", "2016-07-26", "", "", "",
                //    "", "", "", "", "", "", "", "");
                if (dt != null)
                {

                    HttpContext.Current.Session["CallsReport"] = dt;
                    var query = from row in dt.AsEnumerable()
                                    //group row by new {company = row.Field<string>("BookingByCompany"), status=  row.Field<string>("BookingStatus")} into booking
                                group row by new { destination = row.Field<string>("Destination") } into booking
                                orderby booking.Key.destination.ToUpper()
                                select new
                                {
                                    Destination = booking.Key.destination,

                                    NoOfBookings = booking.Count()
                                };

                    DataTable dtBooking = new DataTable();
                    dtBooking.Columns.Add("Destination", typeof(String));

                    dtBooking.Columns.Add("NoOfCalls", typeof(Int32));
                    foreach (var booking in query)
                    {
                        DataRow dr = dtBooking.NewRow();
                        dr["Destination"] = booking.Destination;
                        //dr["Status"] = booking.Status;
                        dr["NoOfCalls"] = booking.NoOfBookings;
                        dtBooking.Rows.Add(dr);

                    }
                    dtBooking.DefaultView.Sort = "NoOfCalls DESC";
                    dtBooking = dtBooking.DefaultView.ToTable();
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