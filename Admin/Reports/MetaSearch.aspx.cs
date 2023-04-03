using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Reports_MetaSearch : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserDetails"] != null)
        {

            if (!IsPostBack)
            {
                UserDetail objUserDetail = Session["UserDetails"] as UserDetail;
                if (!objUserDetail.isAuth("MetaSearch"))
                {
                    Response.Redirect("Default.aspx");
                    return;
                }
              
                txtFromDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                // BookingReport();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx", false);
        }
    }

    [WebMethod(EnableSession = true)]
    public static string MetaSearchReportByDestination(string fromDate, string toDate)
    {
        try
        {
            //string fromDate ="",  toDate = "", status ="",  sourceMedia = "",  company="";


            GetSetDatabase objGetSetDatabase = new GetSetDatabase();
            DataTable dt = objGetSetDatabase.GET_MetaSearches(fromDate, toDate);

            if (dt != null)
            {

                HttpContext.Current.Session["MetaSearch"] = dt;
                var query = from row in dt.AsEnumerable()
                            group row by row.Field<string>("Destination") into metaSearch
                            orderby metaSearch.Key.Count() descending
                            select new
                            {
                                Destination = metaSearch.Key,
                                Searches = metaSearch.Count()
                            };

                DataTable dtBooking = new DataTable();
                dtBooking.Columns.Add("Destination", typeof(String));
                dtBooking.Columns.Add("Searches", typeof(Int32));
                foreach (var metaSearch in query)
                //foreach (DataRow dr1 in dt.Rows)
                {
                    DataRow dr = dtBooking.NewRow();
                    dr["Destination"] = metaSearch.Destination;//dr1["Destination"];//booking.Status;
                    dr["Searches"] = metaSearch.Searches;//dr1["Searchs"];// booking.NoOfBookings;
                    dtBooking.Rows.Add(dr);

                }
                return CommanBinding.ConvertDataTableToJSON(dtBooking);
            }
            else
            { return ""; }

        }
        catch { return ""; }

    }
    //

    [WebMethod(EnableSession = true)]
    public static string MetaSearchReportByDepartDate()
    {
        try
        {
            if (HttpContext.Current.Session["MetaSearch"] != null)
            {

                DataTable dt = (DataTable)HttpContext.Current.Session["MetaSearch"];               
                if (dt != null)
                {

                    HttpContext.Current.Session["MetaSearch"] = dt;
                    var query = from row in dt.AsEnumerable()
                                //group row by new {company = row.Field<string>("BookingByCompany"), status=  row.Field<string>("BookingStatus")} into booking
                                group row by new { Destination = row.Field<string>("Destination"), DepartDate = row.Field<DateTime>("DepartDate") } into metaSearch
                                orderby metaSearch.Key.Destination
                                select new
                                {
                                    Destination = metaSearch.Key.Destination,

                                    Searches = metaSearch.Count(),
                                    DepartDate = metaSearch.Key.DepartDate
                                };

                    DataTable dtBooking = new DataTable();
                    dtBooking.Columns.Add("Destination", typeof(String));
                    dtBooking.Columns.Add("Searches", typeof(Int32));
                    dtBooking.Columns.Add("DepartDate", typeof(DateTime));
                    foreach (var booking in query)
                    {
                        DataRow dr = dtBooking.NewRow();
                        dr["Destination"] = booking.Destination;                       
                        dr["Searches"] = booking.Searches;
                        dr["DepartDate"] = booking.DepartDate;
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
    public static string MetaSearchReportByTravelDate()
    {
        try
        {
            if (HttpContext.Current.Session["MetaSearch"] != null)
            {

                DataTable dt = (DataTable)HttpContext.Current.Session["MetaSearch"];
                if (dt != null)
                {

                    HttpContext.Current.Session["MetaSearch"] = dt;
                    var query = from row in dt.AsEnumerable()
                                //group row by new {company = row.Field<string>("BookingByCompany"), status=  row.Field<string>("BookingStatus")} into booking
                                group row by new { Origin = row.Field<string>("Origin"), Destination = row.Field<string>("Destination"), DepartDate = row.Field<DateTime>("DepartDate"), ReturnDate = row.Field<DateTime>("ReturnDate") } into metaSearch
                                orderby  metaSearch.Count() descending
                                select new
                                {
                                    Origin = metaSearch.Key.Origin,
                                    Destination = metaSearch.Key.Destination,

                                    Searches = metaSearch.Count(),
                                    DepartDate = metaSearch.Key.DepartDate,
                                    ReturnDate = metaSearch.Key.ReturnDate
                                };

                    DataTable dtBooking = new DataTable();
                    dtBooking.Columns.Add("Origin", typeof(String));
                    dtBooking.Columns.Add("Destination", typeof(String));
                    dtBooking.Columns.Add("Searches", typeof(Int32));
                    dtBooking.Columns.Add("DepartDate", typeof(DateTime));
                    dtBooking.Columns.Add("ReturnDate", typeof(DateTime));
                    foreach (var booking in query)
                    {
                        DataRow dr = dtBooking.NewRow();
                        dr["Origin"] = booking.Origin;
                        dr["Destination"] = booking.Destination;
                        dr["Searches"] = booking.Searches;
                        dr["DepartDate"] = booking.DepartDate;
                        dr["ReturnDate"] = booking.ReturnDate;
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
    public static string MetaSearchReportByTravelMonth()
    {
        try
        {
            if (HttpContext.Current.Session["MetaSearch"] != null)
            {

                DataTable dt = (DataTable)HttpContext.Current.Session["MetaSearch"];
                
                if (dt != null)
                {                   

                    HttpContext.Current.Session["MetaSearch"] = dt;
                    var query = from row in dt.AsEnumerable()
                                //group row by new {company = row.Field<string>("BookingByCompany"), status=  row.Field<string>("BookingStatus")} into booking
                                group row by new { Month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Convert.ToDateTime(row.Field<DateTime>("DepartDate")).Month) } into metaSearch
                                orderby metaSearch.Count() descending
                                select new
                                {
                                    //Destination = metaSearch.Key.Destination,
                                       Month = metaSearch.Key.Month,
                                    Searches = metaSearch.Count(),
                                   
                                   
                                };

                    DataTable dtBooking = new DataTable();
                    dtBooking.Columns.Add("Month", typeof(String));
                    //dtBooking.Columns.Add("Destination", typeof(String));
                    dtBooking.Columns.Add("Searches", typeof(Int32));
                  
                   
                    foreach (var booking in query)
                    {
                        DataRow dr = dtBooking.NewRow();
                        dr["Month"] = booking.Month;
                        //dr["Destination"] = booking.Destination;
                        dr["Searches"] = booking.Searches;
                      
                        
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


    protected void btExcel_Click(object sender, EventArgs e)
    {
        try
        {
            if (HttpContext.Current.Session["MetaSearch"] != null)
            {
                Common com = new Common();
                DataTable dt = (DataTable)HttpContext.Current.Session["MetaSearch"];

                if (dt != null)
                {
                    com.ExporttoExcel(dt);
                }
            }
        }
        catch { }
    }

}