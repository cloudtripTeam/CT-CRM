using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Reports_TrackerReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserDetails"] != null)
        {

            if (!IsPostBack)
            {
                UserDetail objUserDetail = Session["UserDetails"] as UserDetail;
                if (!objUserDetail.isAuth("TrackerReport"))
                {
                    Response.Redirect("Default.aspx");
                    return;
                }
                CommanBinding.BindCompanyDetails(ref ddlCompany, objUserDetail.userID);
                txtHitFrom.Text = DateTime.Today.ToString("dd/MM/yyyy");
               
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx", false);
        }
    }


    [WebMethod(EnableSession = true)]
    public static string TrafficByCompany(string fromDate, string toDate, string company)
    {
        try
        {
            
            
            GetSetDatabase objGetSetDatabase = new GetSetDatabase();
            DataTable dt = objGetSetDatabase.SearchPageTrackerOnline("", "", "", "", "", "", company,
                fromDate, toDate, "", "", "", "", "", "", "", "", "", "", "", "Select");

            if (dt != null)
            {

                HttpContext.Current.Session["Tracker"] = dt;
                var query = from row in dt.AsEnumerable()
                            group row by row.Field<string>("Company_Name") into traffic
                            orderby traffic.Key
                            select new
                            {
                                Company = traffic.Key,
                                NoOfHits = traffic.Count()
                            };

                DataTable dtBooking = new DataTable();
                dtBooking.Columns.Add("Company", typeof(String));
                dtBooking.Columns.Add("NoOfHits", typeof(Int32));
                foreach (var traffic in query)
                {
                    DataRow dr = dtBooking.NewRow();
                    dr["Company"] = traffic.Company;
                    dr["NoOfHits"] = traffic.NoOfHits;
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
    public static string TrafficByCampaign()
    {
        try
        {
            if (HttpContext.Current.Session["Tracker"] != null)
            {
                DataTable dt = (DataTable)HttpContext.Current.Session["Tracker"];              
                if (dt != null)
                {
                    HttpContext.Current.Session["Tracker"] = dt;
                    var query = from row in dt.AsEnumerable()
                                //group row by new {company = row.Field<string>("BookingByCompany"), status=  row.Field<string>("BookingStatus")} into booking
                                group row by new { campaign = row.Field<string>("Campaign_Name") } into traffic
                                orderby traffic.Key.campaign
                                select new
                                {
                                    Campaign = traffic.Key.campaign,
                                    NoOfHits = traffic.Count()
                                };

                    DataTable dtBooking = new DataTable();
                    dtBooking.Columns.Add("Campaign", typeof(String));

                    dtBooking.Columns.Add("NoOfHits", typeof(Int32));
                    foreach (var booking in query)
                    {
                        DataRow dr = dtBooking.NewRow();
                        dr["Campaign"] = booking.Campaign;
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
    public static string TrafficByDestination()
    {
        try
        {
            if (HttpContext.Current.Session["Tracker"] != null)
            {

                DataTable dt = (DataTable)HttpContext.Current.Session["Tracker"];

                if (dt != null)
                {

                    HttpContext.Current.Session["Tracker"] = dt;
                    var query = from row in dt.AsEnumerable()
                                //group row by new {company = row.Field<string>("BookingByCompany"), status=  row.Field<string>("BookingStatus")} into booking
                                group row by new { destination = row.Field<string>("Destination") } into traffic
                                orderby traffic.Key.destination
                                select new
                                {
                                    Destination = traffic.Key.destination,
                                    NoOfHits = traffic.Count()
                                };

                    DataTable dtBooking = new DataTable();
                    dtBooking.Columns.Add("Destination", typeof(String));

                    dtBooking.Columns.Add("NoOfHits", typeof(Int32));
                    foreach (var booking in query)
                    {
                        DataRow dr = dtBooking.NewRow();
                        dr["Destination"] = booking.Destination;
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
    public static string TrafficOnPage()
    {
        try
        {
            if (HttpContext.Current.Session["Tracker"] != null)
            {

                DataTable dt = (DataTable)HttpContext.Current.Session["Tracker"];

                if (dt != null)
                {

                    HttpContext.Current.Session["Tracker"] = dt;
                    var query = from row in dt.AsEnumerable()
                                //group row by new {company = row.Field<string>("BookingByCompany"), status=  row.Field<string>("BookingStatus")} into booking
                                group row by new { page = row.Field<string>("Page") } into traffic
                                orderby traffic.Key.page
                                select new
                                {
                                    Page = traffic.Key.page,

                                    NoOfHits = traffic.Count()
                                };

                    DataTable dtBooking = new DataTable();
                    dtBooking.Columns.Add("Page", typeof(String));

                    dtBooking.Columns.Add("NoOfHits", typeof(Int32));
                    foreach (var booking in query)
                    {
                        DataRow dr = dtBooking.NewRow();
                        dr["Page"] = booking.Page;
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