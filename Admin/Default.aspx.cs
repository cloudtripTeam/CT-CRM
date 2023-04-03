using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using MoreLinq;

public partial class Admin_Default : BasePage
{

    public string today = string.Empty;
    public string yesterday = string.Empty;
    public string lastweek = string.Empty;
    public string ticketingAlert = string.Empty;
    public string queuedBooking = string.Empty;
    public string uniqueCalls = string.Empty;
    public string yesterdayUniqueCalls = string.Empty;
    public string turnOver = string.Empty;
    public string ticketIssuedBy = string.Empty;
    public string IssuedBooking = string.Empty;
    public string FollowUps { get; set; }
    UserDetail objUserDetail;
    DropDownList ddlCompany = new DropDownList();
    string fromDate = string.Empty; string toDate = string.Empty;
    string selectedBookingStatus = string.Join(",", "Booked", "Confirm", "Decline", "Documents", "Payments", "Incomplete", "Issued", "ReIssued", "Follow UP", "Option", "Queue", "Dupe", "Refund", "Deposit Forfeited", "ETicket Sent", "TKTNotFound", "Completed", "Future Credit", "Charge Back");
    string selectedCompany = string.Join(",", "C2BCA", "C2BUS", "FLTTROTT_CA", "FLTTROTT_USA", "JRXPTUS", "TPCA", "TPUSA", "TRVJUNCTION_CA", "TRVJUNCTION_USA");
    GetSetDatabase objGetSetDatabase = new GetSetDatabase();



    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            objUserDetail = Session["UserDetails"] as UserDetail;
            if (objUserDetail != null)
            {
                CommanBinding.BindCompanyDetails(ref ddlCompany, objUserDetail.userID);
                if (objUserDetail.userID.ToLower() == "george" || objUserDetail.userID.ToLower() == "adminsup")
                {
                    
                    try
                    {
                         BindData_NewDashboard("", "","0");
                        
                    }
                    catch
                    {

                    }
                  
                }
                else
                {
                    
                }

                if (objUserDetail.userRole.ToLower() == "admin" || objUserDetail.userRole.ToLower() == "superadmin" || objUserDetail.userRole.ToLower() == "team head ft" || objUserDetail.userRole.ToLower() == "team head ca")
                {
                    //DataTable dt = Get_ProfitLoss();
                    //if (dt != null)
                    //{
                    //    Today(dt);
                    //    Yesterday(dt);
                    //    LastWeek(dt);
                    //}
                    DataTable dturn = Get_QuaterlyTurnOver();
                    CurrentQuaterlyTurnOver(dturn);
                }
                if (objUserDetail.userRole.ToLower() == "admin" || objUserDetail.userRole.ToLower() == "superadmin" || objUserDetail.userRole.ToLower() == "team head" || objUserDetail.userRole.ToLower() == "team head ca" || objUserDetail.userRole.ToLower() == "customer care" || objUserDetail.userRole.ToLower() == "online" || objUserDetail.userRole.ToLower() == "onlinetl")
                {
                    if (objUserDetail.userRole.ToLower() == "admin")
                    {
                        TicketingAlert(objUserDetail.userID);
                        BookingsInQueue(objUserDetail.userID);
                        IssuedBookings("");
                        BookingFollows();

                    }
                    else
                    {
                        TicketingAlert("");
                        BookingsInQueue("");
                        BookingFollows();
                    }

                    if (objUserDetail.userRole.ToLower() == "admin" || objUserDetail.userRole.ToLower() == "superadmin")
                    {
                        BookingsIssuedBy(objUserDetail.userID);
                        IssuedBookings("");
                    }
                    if (objUserDetail.userRole.ToLower() == "superadmin")
                    {
                        btnClear.Visible = true;
                    }
                }
                else
                {
                    TicketingAlert(objUserDetail.userID);
                    BookingsInQueue(objUserDetail.userID);
                }

                GetAgentsProfit();
                CallsEnquiry(objUserDetail.userRole.ToLower());
                if (objUserDetail.userID.ToLower() == "Pankaj".ToLower())
                {
                    pnlVendor.Visible = true;
                }
            }
        }
    }
    public void BookingFollows()
    {
        DataTable dtf = new DataTable();
        try
        {
            string XP = null;
            DataTable dt = new DataTable();
            GetSetDatabase objGetSetDatabase = new GetSetDatabase();
            dt = objGetSetDatabase.GET_Booking_Follow(XP);


            if (dt.Rows.Count > 0)
            {
                FollowUps = "<div class='col-md-6'>" +
               "<div class='card'>" +
                   "<div class='card-head'>" +
                       "<header>Ticket Follow Up</header>" +

                   "</div>" +

                   "<div class='card-body no-padding height-6'>";
                if (dt != null)
                {
                    foreach (DataRow dr in dt.Rows)
                    {

                        FollowUps += @"<div class='row'>" +
                             "<div class='col-md-2 hidden-xs'>" +
                                        "<div class='force-padding text-sm '>" +
                                           "<p>" +
                                                 "<strong> " + dr["BKG_FLW_DateTime"] + " </strong>" +
                                            "</p>" +
                                        "</div>" +

                                    "</div>" +

                               " <div class='col-md-4 hidden-xs'>" +
                                    "<div class='force-padding text-sm '>" +
                                        "<p>" +
                                              "<strong> <a href='FollowUp.aspx?BID=" + dr["BKG_FLW_Ref_No"].ToString() + "'>" + dr["BKG_FLW_Ref_No"].ToString() + "</a></strong>" +
                                        "</p>" +
                                   " </div>" +
                                "</div>" +
                                 "<div class='col-md-4 hidden-xs'>" +
                                    "<div class='force-padding text-sm'>" +
                                        "<p>" +
                                             "<strong> " + dr["BKG_FLW_Remarks_By"].ToString() + "</strong>" +
                                        "</p>" +
                                    "</div>" +
                                "</div>" +

                            "</div>";


                    }
                }

                FollowUps +=
                "</div>" +

                    "</div>" +

                "</div>";
            }
        }
        catch
        {

        }
    }
    private void Today(DataTable dt)
    {
        DataTable dd = filterData(dt, "today");
        double totalCost = 0;
        double totalSell = 0;
        double totalProfit = 0;
        double incompleteProfit = 0;
        double paidProfit = 0;

        foreach (DataRow dr in dd.Rows)
        {
            totalCost += Convert.ToDouble(dr["CostPrice"]);
            totalSell += Convert.ToDouble(dr["SellPrice"]);
            totalProfit += Convert.ToDouble(dr["Profit"]);

            if (dr["Booking_Status"].ToString().ToLower() == "incomplete")
            {

                incompleteProfit += Convert.ToDouble(dr["Profit"]);
            }
            else if (dr["Booking_Status"].ToString().ToLower() == "decline" || dr["Booking_Status"].ToString().ToLower() == "confirm")
            {
                paidProfit += Convert.ToDouble(dr["Profit"]);
            }

        }

        today = @"<section>" +
             "<div class='col-md-12'>" +
                 "<div class='card-head'>" +
                     "<header>Sales and Profit</header>" +
                 "</div>" +
                 "<!--end .card-head -->" +

             "</div>" +
             "<div class='col-md-2 col-sm-4'>" +
                 "<div class='card'>" +
                     "<div class='card-body no-padding'>" +
                         "<div class='alert alert-callout alert-info no-margin'>" +

                             "<strong class='text-xl'>£ " + totalSell + "</strong><br>" +
                             "<span class='opacity-50'>today's total sales</span>" +
                             "<div class='stick-bottom-left-right'>" +
                                 "<div class='height-2 sparkline-revenue' data-line-color='#bdc1c1'>" +
                                     "<canvas style='display: inline-block; width: 289px; height: 80px; vertical-align: top;' width='289' height='80'></canvas>" +
                                 "</div>" +
                             "</div>" +
                         "</div>" +
                     "</div>" +
                     "<!--end .card-body -->" +
                 "</div>" +
                 "<!--end .card -->" +
             "</div>" +


             "<div class='col-md-2 col-sm-4'>" +
                 "<div class='card'>" +
                     "<div class='card-body no-padding'>" +
                         "<div class='alert alert-callout alert-warning no-margin'>" +
                             "<strong class='pull-right text-warning text-lg'> <i class='md md-swap-vert'></i></strong>" +
                             "<strong class='text-xl'>£ " + totalCost + "</strong><br>" +
                             "<span class='opacity-50'>Today's total cost</span>" +
                             "<div class='stick-bottom-right'>" +
                                 "<div class='height-1 sparkline-visits' data-bar-color='#e5e6e6'>" +
                                     "<canvas width='265' height='40' style='display: inline-block; width: 265px; height: 40px; vertical-align: top;'></canvas>" +
                                 "</div>" +
                             "</div>" +
                         "</div>" +
                     "</div>" +
                     "<!--end .card-body -->" +
                 "</div>" +
                 "<!--end .card -->" +
             "</div>" +

             "<div class='col-md-2 col-sm-4'>" +
                 "<div class='card'>" +
                     "<div class='card-body no-padding'>" +
                         "<div class='alert alert-callout alert-success no-margin'>" +
                          "<strong class='pull-right text-success text-lg'>" + Math.Round((((totalSell - totalCost) / totalCost) * 100), 2) + "% <i class='md md-trending-up'></i></strong>" +
                             "<h1 class='pull-right text-success'><i class='md md-timer'></i></h1>" +
                             "<strong class='text-xl'>£ " + totalProfit + "</strong><br>" +
                             " <span class='opacity-50'>Paid profit £ " + paidProfit + "</span><br>" +
                              " <span class='opacity-50'>Unpaid profit  £ " + incompleteProfit + "</span>" +
                        //" <br><span class='opacity-50'>Today's total profit</span>" +
                        " </div>" +
                     "</div>" +
                     "<!--end .card-body -->" +
                " </div>" +
                " <!--end .card -->" +
             "</div>" +

         "</section>";
    }
    private void Yesterday(DataTable dt)
    {
        DataTable dd = filterData(dt, "yesterday");

        double totalCost = 0;
        double totalSell = 0;
        double totalProfit = 0;
        double incompleteProfit = 0;
        double paidProfit = 0;
        foreach (DataRow dr in dd.Rows)
        {
            totalCost += Convert.ToDouble(dr["CostPrice"]);
            totalSell += Convert.ToDouble(dr["SellPrice"]); ;
            totalProfit += Convert.ToDouble(dr["Profit"]); ;
            if (dr["Booking_Status"].ToString().ToLower() == "incomplete")
            {

                incompleteProfit += Convert.ToDouble(dr["Profit"]);
            }
            else if (dr["Booking_Status"].ToString().ToLower() == "decline" || dr["Booking_Status"].ToString().ToLower() == "confirm")
            {
                paidProfit += Convert.ToDouble(dr["Profit"]);
            }
        }

        yesterday = @"<section>" +
             //"<div class='col-md-12'>" +
             //    "<div class='card-head'>" +
             //        "<header>Yesterday's</header>" +
             //    "</div>" +
             //    "<!--end .card-head -->" +

             //"</div>" +
             "<div class='col-md-2 col-sm-4'>" +
                 "<div class='card'>" +
                     "<div class='card-body no-padding'>" +
                         "<div class='alert alert-callout alert-info no-margin'>" +

                             "<strong class='text-xl'>£ " + totalSell + "</strong><br>" +
                             "<span class='opacity-50'>Yesterday's total sales</span>" +
                             "<div class='stick-bottom-left-right'>" +
                                 "<div class='height-2 sparkline-revenue' data-line-color='#bdc1c1'>" +
                                     "<canvas style='display: inline-block; width: 289px; height: 80px; vertical-align: top;' width='289' height='80'></canvas>" +
                                 "</div>" +
                             "</div>" +
                         "</div>" +
                     "</div>" +
                     "<!--end .card-body -->" +
                 "</div>" +
                 "<!--end .card -->" +
             "</div>" +


             "<div class='col-md-2 col-sm-4'>" +
                 "<div class='card'>" +
                     "<div class='card-body no-padding'>" +
                         "<div class='alert alert-callout alert-warning no-margin'>" +
                             "<strong class='pull-right text-warning text-lg'> <i class='md md-swap-vert'></i></strong>" +
                             "<strong class='text-xl'>£ " + totalCost + "</strong><br>" +
                             "<span class='opacity-50'>Yesterday's total cost</span>" +
                             "<div class='stick-bottom-right'>" +
                                 "<div class='height-1 sparkline-visits' data-bar-color='#e5e6e6'>" +
                                     "<canvas width='265' height='40' style='display: inline-block; width: 265px; height: 40px; vertical-align: top;'></canvas>" +
                                 "</div>" +
                             "</div>" +
                         "</div>" +
                     "</div>" +
                     "<!--end .card-body -->" +
                 "</div>" +
                 "<!--end .card -->" +
             "</div>" +

             "<div class='col-md-2 col-sm-4'>" +
                 "<div class='card'>" +
                     "<div class='card-body no-padding'>" +
                         "<div class='alert alert-callout alert-success no-margin'>" +
                          "<strong class='pull-right text-success text-lg'>" + Math.Round((((totalSell - totalCost) / totalCost) * 100), 2) + "% <i class='md md-trending-up'></i></strong>" +
                             "<h1 class='pull-right text-success'><i class='md md-timer'></i></h1>" +
                             "<strong class='text-xl'>£ " + totalProfit + "</strong><br>" +
                              " <span class='opacity-50'>Paid profit £ " + paidProfit + "</span><br>" +
                              " <span class='opacity-50'>Unpaid profit  £ " + incompleteProfit + "</span>" +
                        //"<br> <span class='opacity-50'>Yesterday's total profit</span>" +
                        " </div>" +
                     "</div>" +
                     "<!--end .card-body -->" +
                " </div>" +
                " <!--end .card -->" +
             "</div>" +

         "</section>";


    }
    private void LastWeek(DataTable dt)
    {

        //DataTable dd = filterData(dt,"lastweek");
    }

    private void CurrentQuaterlyTurnOver(DataTable dt)
    {
        double totalCost = 0;
        int totalpax = 0;
        int count = 0;
        if (dt != null && dt.Rows.Count > 0)
        {
            foreach (DataRow dr in dt.Rows)
            {
                totalpax += Convert.ToInt32(dr["NoOfPax"]);

                // totSell += Convert.ToDouble(dr["Sell_Price"].ToString());

                totalCost += Convert.ToDouble(dr["Sell_Price"]);
            }

        }
        turnOver = @"<section>" +
        "<div class='col-md-4 col-sm-4'>" +
            "<div class='card'>" +
                "<div class='card-body no-padding'>" +
                    "<div class='alert alert-callout alert-success no-margin'>" +
                     "<strong class='pull-right text-success text-lg'>" + "" + " <i class='md md-trending-up'></i></strong>" +
                        "<h1 class='pull-right text-success'><i class='md md-timer'></i></h1>" +
                        "<strong class='text-xl'>Passengers " + totalpax + "</strong> (by depart date) <br>" +
                         " <span class='opacity-50'>Sales " + totalCost.ToString("f2") + "</span><br>" +
                         " <span class='opacity-50'>Current Quarter (" + Convert.ToDateTime(fromDate).ToString("MMM yyyy") + " - " + Convert.ToDateTime(toDate).ToString("MMM yyyy") + ") </span>" +

                   " </div>" +
                "</div>" +
                "<!--end .card-body -->" +
           " </div>" +
           " <!--end .card -->" +
        "</div>" +

    "</section>";
    }

    private DataTable Get_QuaterlyTurnOver()
    {
        GetSetDatabase db = new GetSetDatabase();
        #region set Quaterly Date

        switch (DateTime.Now.Month)
        {
            case 1:
            case 2:
            case 3:
                fromDate = DateTime.Now.Year + "-01-01";
                toDate = DateTime.Now.Year + "-03-31";
                break;

            case 4:
            case 5:
            case 6:
                fromDate = DateTime.Now.Year + "-04-01";
                toDate = DateTime.Now.Year + "-06-30";
                break;
            case 7:
            case 8:
            case 9:
                fromDate = DateTime.Now.Year + "-07-01";
                toDate = DateTime.Now.Year + "-09-30";

                break;
            case 10:
            case 11:
            case 12:
                fromDate = DateTime.Now.Year + "-10-01";
                toDate = DateTime.Now.Year + "-12-31";
                break;

        }
        #endregion

        DataTable dtTurnOver = db.GET_Atol_Report("", "", "", "", CommanBinding.GetCompanyCodes(ddlCompany), "issued,completed,ETicket Sent", fromDate, toDate, "", "10950");

        #region Filter Atol Type
        if (dtTurnOver != null && dtTurnOver.Rows.Count > 0)
        {
            string atoltype = "ATOL_Type = 'Flights Only/PUBLIC BONDED'";
            DataRow[] dr = dtTurnOver.Select(atoltype);
            if (dr != null && dr.Count() > 0)
            {
                dtTurnOver = dr.CopyToDataTable();
            }
            else
            {
                dtTurnOver = null;
            }

        }
        #endregion

        return dtTurnOver;

    }

    private DataTable Get_ProfitLoss()
    {
        GetSetDatabase objGetSetDatabase = new GetSetDatabase();
        DateTime mondayOfLastWeek = DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek - 6);
        DataTable dt = objGetSetDatabase.Get_ProfitLoss(mondayOfLastWeek.ToString(), "", CommanBinding.GetCompanyCodes(ddlCompany));
        if (dt != null)
        {
            var query = from row in dt.AsEnumerable()
                            //group row by new {company = row.Field<string>("BookingByCompany"), status=  row.Field<string>("BookingStatus")} into booking
                        group row by new
                        {
                            Status = row.Field<string>("Booking_Status"),
                            BookingDate = row.Field<string>("BookingDate")
                        } into Booking
                        orderby Booking.Count() descending
                        select new
                        {
                            Status = Booking.Key.Status,
                            BookigDate = Booking.Key.BookingDate,
                            CostPrice = Booking.Sum(x => x.Field<Decimal>("CostPrice")),
                            SellPrice = Booking.Sum(x => x.Field<Decimal>("SellPrice")),
                            Profit = Booking.Sum(x => x.Field<Decimal>("Profit"))
                        };

            DataTable dtBooking = new DataTable();
            dtBooking.Columns.Add("Booking_Status", typeof(String));
            dtBooking.Columns.Add("BookingDate", typeof(String));
            dtBooking.Columns.Add("CostPrice", typeof(Decimal));
            dtBooking.Columns.Add("SellPrice", typeof(Decimal));
            dtBooking.Columns.Add("Profit", typeof(Decimal));
            foreach (var booking in query)
            {
                DataRow dr = dtBooking.NewRow();
                dr["Booking_Status"] = booking.Status;
                dr["BookingDate"] = booking.BookigDate;
                dr["CostPrice"] = booking.CostPrice;
                dr["SellPrice"] = booking.SellPrice;
                dr["Profit"] = booking.Profit;
                dtBooking.Rows.Add(dr);
            }
            return dtBooking;
        }
        return null;
    }

    private DataTable filterData(DataTable dt, string day)
    {
        string date = string.Empty;

        if (day == "today")
        {
            date = DateTime.Today.ToString("dd/MM/yyyy");

        }
        else if (day == "yesterday")
        {

            date = (DateTime.Today.AddDays(-1)).ToString("dd/MM/yyyy");
        }
        else if (day == "lastweek")
        {

        }
        var query = from row in dt.AsEnumerable()
                    where row.Field<string>("BookingDate") == date
                    group row by new
                    {
                        Status = row.Field<string>("Booking_Status"),
                        BookingDate = row.Field<string>("BookingDate")

                    } into Booking
                    orderby Booking.Count() descending
                    select new
                    {
                        Status = Booking.Key.Status,
                        BookigDate = Booking.Key.BookingDate,
                        CostPrice = Booking.Sum(x => x.Field<Decimal>("CostPrice")),
                        SellPrice = Booking.Sum(x => x.Field<Decimal>("SellPrice")),
                        Profit = Booking.Sum(x => x.Field<Decimal>("Profit"))
                    };


        DataTable dtBooking = new DataTable();
        dtBooking.Columns.Add("Booking_Status", typeof(String));
        dtBooking.Columns.Add("BookingDate", typeof(String));
        dtBooking.Columns.Add("CostPrice", typeof(Decimal));
        dtBooking.Columns.Add("SellPrice", typeof(Decimal));
        dtBooking.Columns.Add("Profit", typeof(Decimal));
        foreach (var booking in query)
        {
            DataRow dr = dtBooking.NewRow();
            dr["Booking_Status"] = booking.Status;
            dr["BookingDate"] = booking.BookigDate;
            dr["CostPrice"] = booking.CostPrice;
            dr["SellPrice"] = booking.SellPrice;
            dr["Profit"] = booking.Profit;
            dtBooking.Rows.Add(dr);

        }
        return dtBooking;

    }

    private void TicketingAlert(string bookingBy)
    {
        GetSetDatabase objGetSetDatabase = new GetSetDatabase();

        CommanBinding.BindCompanyDetails(ref ddlCompany, bookingBy);
        DataTable dt = objGetSetDatabase.Get_Ticketing_Alert(CommanBinding.GetCompanyCodes(ddlCompany), bookingBy);
        int count = 0;
        if (dt != null && dt.Rows.Count > 0)
        {
            ticketingAlert = "<div class='col-md-6'>" +
           "<div class='card'>" +
               "<div class='card-head'>" +
                   "<header>Ticketing Alert</header>" +
                   "<div class='tools'>" +
                       "<a class='btn btn-icon-toggle btn-collapse'><i class='fa fa-angle-down'></i></a>" +
                   "</div>" +
               "</div>" +
               "<!--end .card-head -->" +
               "<div class='card-body no-padding height-6' style='display: block;'>";
            foreach (DataRow dr in dt.Rows)
            {

                ticketingAlert += @"<div class='row'>" +
                     "<div class='col-md-1 hidden-xs'>" +
                                "<div class='force-padding text-sm '>" +
                                   "<p>" +
                                         "<strong> " + ++count + "</strong>" +
                                    "</p>" +
                                "</div>" +

                            "</div>" +
                            "<div class='col-md-2 hidden-xs'>" +
                                "<div class='force-padding text-sm '>" +
                                   "<p>" +
                                         "<strong> " + dr["BookingID"].ToString() + "</strong>" +
                                    "</p>" +
                                "</div>";
                if (Convert.ToDateTime(dr["TravelDate"].ToString()).ToString("dd MMM") == DateTime.Today.ToString("dd MMM"))
                    ticketingAlert += " <div class='progress progress-hairline'>" +
                                                "<div class='progress-bar progress-bar-danger' style='width:93%'></div>" +
                                            "</div>";
                ticketingAlert += "</div>" +

                        "<div class='col-md-2 hidden-xs'>" +
                            "<div class='force-padding text-sm '>" +
                                "<p>" +
                                    "<strong> " + Convert.ToDateTime(dr["TravelDate"].ToString()).ToString("dd MMM") + "</strong>" +
                                "</p>" +
                            "</div>" +
                        "</div>" +
                        "<div class='col-md-2 hidden-xs'>" +
                            "<div class='force-padding text-sm '>" +
                                "<p>" +
                                     "<strong> " + dr["FName"].ToString() + " " + dr["LName"].ToString() + "</strong>" +
                                "</p>" +
                            "</div>" +
                        "</div>" +
                        "<div class='col-md-1 hidden-xs'>" +
                            "<div class='force-padding text-sm'>" +
                                "<p>" +
                                    "<strong> " + dr["Carrier"].ToString() + "</strong>" +
                                "</p>" +
                            "</div>" +
                        "</div>" +
                       " <div class='col-md-2 hidden-xs'>" +
                            "<div class='force-padding text-sm '>" +
                                "<p>" +
                                      "<strong> " + dr["Destination"].ToString() + "</strong>" +
                                "</p>" +
                           " </div>" +
                        "</div>" +
                        "<div class='col-md-2 hidden-xs'>" +
                            "<div class='force-padding text-sm'>" +
                                "<p>" +
                                     "<strong> " + dr["BookingStatus"].ToString() + "</strong>" +
                                "</p>" +
                            "</div>" +
                        "</div>" +

                    "</div>";


            }

            ticketingAlert +=
            "</div>" +
                    "<!--end .card-body -->" +
                "</div>" +
                "<!--end .card -->" +
            "</div>";
        }
    }

    private void CallsEnquiry(string role)
    {
        try
        {
            GetSetDatabase objDl = new GetSetDatabase();
            DataTable dt = null;
            role = role.ToLower();
            if (role != "superadmin" && role != "admin" && role != "team head" && role != "team head ft")
            {

                dt = objDl.GET_Call_Details("", "", "", "", "", "", objUserDetail.userID, DateTime.Today.AddDays(-1).ToString("dd/MM/yyyy"), "", "FLTXPT,DIAL4TRV,TRVJUNCTION,OTHER,FLTTROTT", ""); ;

            }
            else if (role == "team head ft")
            {
                dt = objDl.GET_Call_Details("", "", "", "", "", "", "", DateTime.Today.AddDays(-1).ToString("dd/MM/yyyy"), "", "FLTTROTT", "");


            }
            else
            {
                dt = objDl.GET_Call_Details("", "", "", "", "", "", "", DateTime.Today.AddDays(-1).ToString("dd/MM/yyyy"), "", "FLTXPT,DIAL4TRV,TRVJUNCTION,OTHER", "");


            }
            var todayUniqueCalls = dt.AsEnumerable().DistinctBy(x => new { Contact_Number = x.Field<string>("Contact_Number") }).Where(x => x.Field<DateTime>("Create_Date").ToString("dd/MM/yyyy") == DateTime.Today.ToString("dd/MM/yyyy"));
            var yesterdayUniqueCalls = dt.AsEnumerable().DistinctBy(x => new { Contact_Number = x.Field<string>("Contact_Number") }).Where(x => x.Field<DateTime>("Create_Date").ToString("dd/MM/yyyy") == DateTime.Today.AddDays(-1).ToString("dd/MM/yyyy"));
            UniqueCalls(todayUniqueCalls.Count(), yesterdayUniqueCalls.Count());

        }
        catch { }

    }

    private void UniqueCalls(int todaycalls, int yesterdayscalls)
    {



        uniqueCalls = @"<section>" +
             "<div class='col-md-12'>" +
                 "<div class='card-head'>" +
                     "<header>Unique calls</header>" +
                 "</div>" +
                 "<!--end .card-head -->" +

             "</div>" +
             "<div class='col-md-3 col-sm-6'>" +
                 "<div class='card'>" +
                     "<div class='card-body no-padding'>" +
                         "<div class='alert alert-callout alert-info no-margin'>" +

                             "<strong class='text-xl'>" + yesterdayscalls + "</strong><br>" +
                             "<span class='opacity-50'>Yesterday's total calls</span>" +
                             "<div class='stick-bottom-left-right'>" +
                                 "<div class='height-2 sparkline-revenue' data-line-color='#bdc1c1'>" +
                                     "<canvas style='display: inline-block; width: 289px; height: 80px; vertical-align: top;' width='289' height='80'></canvas>" +
                                 "</div>" +
                             "</div>" +
                         "</div>" +
                     "</div>" +
                     "<!--end .card-body -->" +
                 "</div>" +
                 "<!--end .card -->" +
             "</div>" +


             "<div class='col-md-3 col-sm-6'>" +
                 "<div class='card'>" +
                     "<div class='card-body no-padding'>" +
                         "<div class='alert alert-callout alert-warning no-margin'>" +
                             "<strong class='pull-right text-warning text-lg'> <i class='md md-swap-vert'></i></strong>" +
                             "<strong class='text-xl'>" + todaycalls + "</strong><br>" +
                             "<span class='opacity-50'>Today's total calls</span>" +
                             "<div class='stick-bottom-right'>" +
                                 "<div class='height-1 sparkline-visits' data-bar-color='#e5e6e6'>" +
                                     "<canvas width='265' height='40' style='display: inline-block; width: 265px; height: 40px; vertical-align: top;'></canvas>" +
                                 "</div>" +
                             "</div>" +
                         "</div>" +
                     "</div>" +
                     "<!--end .card-body -->" +
                 "</div>" +
                 "<!--end .card -->" +
             "</div>" +

         //"<div class='col-md-3 col-sm-6'>" +
         //    "<div class='card'>" +
         //        "<div class='card-body no-padding'>" +
         //            "<div class='alert alert-callout alert-success no-margin'>" +
         //             "<strong class='pull-right text-success text-lg'>" + Math.Round((((totalSell - totalCost) / totalCost) * 100), 2) + "% <i class='md md-trending-up'></i></strong>" +
         //                "<h1 class='pull-right text-success'><i class='md md-timer'></i></h1>" +
         //                "<strong class='text-xl'>£ " + totalProfit + "</strong><br>" +
         //               " <span class='opacity-50'>Total Profit</span>" +
         //           " </div>" +
         //        "</div>" +
         //        "<!--end .card-body -->" +
         //   " </div>" +
         //   " <!--end .card -->" +
         //"</div>" +

         "</section>";
    }

    private void BookingsInQueue(string BookingBy)
    {
        GetSetDatabase objGetSetDatabase = new GetSetDatabase();
        DropDownList ddlCompany = new DropDownList();
        CommanBinding.BindCompanyDetails(ref ddlCompany, BookingBy);
        DataTable dt = objGetSetDatabase.GET_BookingDetail("", "", "", CommanBinding.GetCompanyCodes(ddlCompany), BookingBy, "", "", "Queue", "", "", "", "", "", "", "", "", "", "", "");
        int count = 0;
        if (dt != null && dt.Rows.Count > 0)
        {
            queuedBooking = "<div class='col-md-6'>" +
           "<div class='card' style='overflow: auto; max-height: 305px;'>" +
               "<div class='card-head'>" +
                   "<header>Bookings in Queue</header>" +
                   "<div class='tools'>" +
                       "<a class='btn btn-icon-toggle btn-collapse'><i class='fa fa-angle-down'></i></a>" +
                   "</div>" +
               "</div>" +
               "<!--end .card-head -->" +
               "<div class='card-body no-padding height-6' style='display: block;'>";
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {

                    queuedBooking += @"<div class='row'>" +
                         "<div class='col-md-1 hidden-xs'>" +
                                    "<div class='force-padding text-sm '>" +
                                       "<p>" +
                                             "<strong> " + ++count + "</strong>" +
                                        "</p>" +
                                    "</div>" +

                                "</div>" +
                                "<div class='col-md-2 hidden-xs'>" +
                                    "<div class='force-padding text-sm '>" +
                                       "<p>" +
                                             "<strong> " + dr["BookingID"].ToString() + "</strong>" +
                                        "</p>" +
                                    "</div>";
                    if (Convert.ToDateTime(dr["TravelDate"].ToString()).ToString("dd MMM") == DateTime.Today.ToString("dd MMM"))
                        queuedBooking += " <div class='progress progress-hairline'>" +
                                                    "<div class='progress-bar progress-bar-danger' style='width:93%'></div>" +
                                                "</div>";
                    queuedBooking += "</div>" +

                            "<div class='col-md-1 hidden-xs'>" +
                                "<div class='force-padding text-sm '>" +
                                    "<p>" +
                                        "<strong> " + Convert.ToDateTime(dr["TravelDate"].ToString()).ToString("dd MMM") + "</strong>" +
                                    "</p>" +
                                "</div>" +
                            "</div>" +
                            "<div class='col-md-2 hidden-xs'>" +
                                "<div class='force-padding text-sm '>" +
                                    "<p>" +
                                         "<strong> " + dr["FName"].ToString() + " " + dr["LName"].ToString() + "</strong>" +
                                    "</p>" +
                                "</div>" +
                            "</div>" +
                           //"<div class='col-md-1 hidden-xs'>" +
                           //    "<div class='force-padding text-sm'>" +
                           //        "<p>" +
                           //            "<strong> " + dr["Carrier"].ToString() + "</strong>" +
                           //        "</p>" +
                           //    "</div>" +
                           //"</div>" +
                           " <div class='col-md-1 hidden-xs'>" +
                                "<div class='force-padding text-sm '>" +
                                    "<p>" +
                                          "<strong> " + dr["Destination"].ToString() + "</strong>" +
                                    "</p>" +
                               " </div>" +
                            "</div>" +
                             "<div class='col-md-2 hidden-xs'>" +
                                "<div class='force-padding text-sm'>" +
                                    "<p>" +
                                         "<strong> " + dr["BookingByCompany"].ToString() + "</strong>" +
                                    "</p>" +
                                "</div>" +
                            "</div>" +
                            "<div class='col-md-1 hidden-xs'>" +
                                "<div class='force-padding text-sm'>" +
                                    "<p>" +
                                         "<strong> " + dr["BookingStatus"].ToString() + "</strong>" +
                                    "</p>" +
                                "</div>" +
                            "</div>" +
                             "<div class='col-md-2 hidden-xs'>" +
                                "<div class='force-padding text-sm'>" +
                                    "<p>" +
                                    "<a href='SendToSupplier.aspx?BID=" + dr["BookingID"].ToString() + "&PID=001'><span style='color:green'>Send to Supplier</span></a>" +
                                "</p>" +
                                "</div>" +
                            "</div>" +
                        "</div>";


                }
            }

            queuedBooking +=
            "</div>" +
                    "<!--end .card-body -->" +
                "</div>" +
                "<!--end .card -->" +
            "</div>";
        }

    }

    private void BookingsIssuedBy(string BookingBy)
    {
        GetSetDatabase objGetSetDatabase = new GetSetDatabase();
        DropDownList ddlCompany = new DropDownList();
        //CommanBinding.BindCompanyDetails(ref ddlCompany, BookingBy);
        DataTable dt = objGetSetDatabase.GET_TicketIssuedBy(DateTime.Today.AddDays(-1).ToString("dd/MM/yyyy"), DateTime.Today.ToString("dd/MM/yyyy"), "", "summary");
        int count = 0;
        if (dt != null && dt.Rows.Count > 0)
        {
            ticketIssuedBy = "<div class='col-md-6'>" +
           "<div class='card'>" +
               "<div class='card-head'>" +
                   "<header>Ticket Issued By (Yesterday)</header>" +
                   "<div class='tools'>" +
                       "<a class='btn btn-icon-toggle btn-collapse'><i class='fa fa-angle-down'></i></a>" +
                   "</div>" +
               "</div>" +
               "<!--end .card-head -->" +
               "<div class='card-body no-padding height-6' style='display: block;'>";
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {

                    ticketIssuedBy += @"<div class='row'>" +
                         "<div class='col-md-2 hidden-xs'>" +
                                    "<div class='force-padding text-sm '>" +
                                       "<p>" +
                                             "<strong> " + ++count + "</strong>" +
                                        "</p>" +
                                    "</div>" +

                                "</div>" +

                           " <div class='col-md-4 hidden-xs'>" +
                                "<div class='force-padding text-sm '>" +
                                    "<p>" +
                                          "<strong> <a href='ticketIssuedByDetails.aspx?issuedby=" + dr["Ticket_IssuedByCode"].ToString() + "'>" + dr["Ticket_IssuedBy"].ToString() + "</a></strong>" +
                                    "</p>" +
                               " </div>" +
                            "</div>" +
                             "<div class='col-md-4 hidden-xs'>" +
                                "<div class='force-padding text-sm'>" +
                                    "<p>" +
                                         "<strong> " + dr["CostPrice"].ToString() + "</strong>" +
                                    "</p>" +
                                "</div>" +
                            "</div>" +

                        "</div>";


                }
            }

            ticketIssuedBy +=
            "</div>" +
                    "<!--end .card-body -->" +
                "</div>" +
                "<!--end .card -->" +
            "</div>";
        }

    }

    private void GetAgentsProfit()
    {
        FandHServices.FandHServicesClient objServices = new FandHServices.FandHServicesClient();
        DataTable dt = objServices.GET_Agents_P_L("", objUserDetail.userID, (DateTime.Today.AddDays(-DateTime.Today.Day)).ToString("dd/MM/yyyy"), "",
              CommanBinding.GetCompanyCodes(ddlCompany), "");
    }

    private void IssuedBookings(string BookingBy)
    {
        try
        {
            GetSetDatabase objGetSetDatabase = new GetSetDatabase();
            DropDownList ddlCompany = new DropDownList();
            CommanBinding.BindCompanyDetails(ref ddlCompany, BookingBy);
            DataTable dt = objGetSetDatabase.GET_BookingByStatus("Issued", 5);
            int count = 0;
            if (dt != null && dt.Rows.Count > 0)
            {
                IssuedBooking = "<div class='col-md-6'>" +
               "<div class='card' style='overflow: auto; max-height: 305px;'>" +
                   "<div class='card-head'>" +
                       "<header>Issued Bookings (5 Days)</header>" +
                       "<div class='tools'>" +
                           "<a class='btn btn-icon-toggle btn-collapse'><i class='fa fa-angle-down'></i></a>" +
                       "</div>" +
                   "</div>" +
                   "<!--end .card-head -->" +
                   "<div class='card-body no-padding height-6' style='display: block;'>";
                if (dt != null)
                {
                    foreach (DataRow dr in dt.Rows)
                    {

                        IssuedBooking += @"<div class='row'>" +
                             "<div class='col-md-1 hidden-xs'>" +
                                        "<div class='force-padding text-sm '>" +
                                           "<p>" +
                                                 "<strong> " + ++count + "</strong>" +
                                            "</p>" +
                                        "</div>" +

                                    "</div>" +
                                    "<div class='col-md-2 hidden-xs'>" +
                                        "<div class='force-padding text-sm '>" +
                                           "<p>" +
                                                 "<strong><a href='FltBooking/AmendBooking.aspx?BID=" + dr["BookingID"].ToString() + "&PID=001' target='_blank'>" + dr["BookingID"].ToString() + "</a></strong>" +
                                            "</p>" +
                                        "</div>";
                        //if (Convert.ToDateTime(dr["TravelDate"].ToString()).ToString("dd MMM") == DateTime.Today.ToString("dd MMM"))
                        IssuedBooking += " <div class='progress progress-hairline'>" +
                                                    "<div class='progress-bar progress-bar-danger' style='width:93%'></div>" +
                                                "</div>";
                        IssuedBooking += "</div>" +

                                "<div class='col-md-1 hidden-xs'>" +
                                    "<div class='force-padding text-sm '>" +
                                        "<p>" +
                                            "<strong></strong>" +
                                        "</p>" +
                                    "</div>" +
                                "</div>" +
                                "<div class='col-md-2 hidden-xs'>" +
                                    "<div class='force-padding text-sm '>" +
                                        "<p>" +
                                             "<strong> " + dr["PNR"].ToString() + "</strong>" +
                                        "</p>" +
                                    "</div>" +
                                "</div>" +
                               //"<div class='col-md-1 hidden-xs'>" +
                               //    "<div class='force-padding text-sm'>" +
                               //        "<p>" +
                               //            "<strong> " + dr["Carrier"].ToString() + "</strong>" +
                               //        "</p>" +
                               //    "</div>" +
                               //"</div>" +
                               " <div class='col-md-1 hidden-xs'>" +
                                    "<div class='force-padding text-sm '>" +
                                        "<p>" +
                                              "<strong> " + dr["Destination"].ToString() + "</strong>" +
                                        "</p>" +
                                   " </div>" +
                                "</div>" +
                                 "<div class='col-md-2 hidden-xs'>" +
                                    "<div class='force-padding text-sm'>" +
                                        "<p>" +
                                             "<strong> " + dr["BookingByCompany"].ToString() + "</strong>" +
                                        "</p>" +
                                    "</div>" +
                                "</div>" +
                                "<div class='col-md-1 hidden-xs'>" +
                                    "<div class='force-padding text-sm'>" +
                                        "<p>" +
                                             "<strong> " + dr["BookingStatus"].ToString() + "</strong>" +
                                        "</p>" +
                                    "</div>" +
                                "</div>" +
                                 "<div class='col-md-2 hidden-xs'>" +
                                    "<div class='force-padding text-sm'>" +
                                        "<p>" + Convert.ToDateTime(dr["Invoice_Date"]).ToString("dd MMM yy") +
                                    "</p>" +
                                    "</div>" +
                                "</div>" +
                            "</div>";


                    }
                }

                IssuedBooking +=
                "</div>" +
                        "<!--end .card-body -->" +
                    "</div>" +
                    "<!--end .card -->" +
                "</div>";
            }
        }
        catch (Exception ex)
        {

        }

    }

    protected void btnUpdateVendor_Click(object sender, EventArgs e)
    {
        GetSetDatabase gsdb = new GetSetDatabase();
        int i = 0;
        i = gsdb.SET_SCredential("UPDATE", ddlGCompany.SelectedValue, ddlGatway.SelectedValue);
        if (i == 1)
            ltrGMsg.Text = "Gateway Change :" + ddlGatway.SelectedValue;
        else
            ltrGMsg.Text = "Not Update";


    }


    protected void btnClear_Click(object sender, EventArgs e)
    {
        GetSetDatabase objtrunc = new GetSetDatabase();
        if (objtrunc.TruncateLog() != 0)
            ltrMsg.Text = "log truncate successfully.";
        else
            ltrMsg.Text = "try again.";

    }

    #region New Dash board for George

    public void BindData_NewDashboard(string fromDate, string toDate, string pageSearch)
    {
       

        if (!string.IsNullOrEmpty(fromDate) || !string.IsNullOrEmpty(toDate))
        {
            SearchRangeData(fromDate, toDate);
        }
    }

  


    private void SearchRangeData(string fromDate, string ToDate)
    {
        var from_Date = Convert.ToDateTime(fromDate).ToString("dd/MM/yyyy");
        var to_Date = Convert.ToDateTime(ToDate).ToString("dd/MM/yyyy");

        DataTable dtOffline_RangeData = objGetSetDatabase.Get_Offline_Leads_Company_Based(from_Date, to_Date, selectedCompany);
        DataSet dsRangeData = objGetSetDatabase.Get_Agents_P_L_NEW_Dashboard(string.Empty, string.Empty, from_Date, to_Date, selectedCompany, selectedBookingStatus);

        DataTable dtRangeData = dsRangeData.Tables[0];
        DataTable dtRangeCountryData = dsRangeData.Tables[1];

        StringBuilder sbRange_Data_Country_Main = new StringBuilder();
        StringBuilder sbRange_Data_Country_Child = new StringBuilder();

        StringBuilder sbRange_Data_Main = new StringBuilder();
        StringBuilder sbRange_Data_Child = new StringBuilder();

        if (dtOffline_RangeData.Rows.Count > 0)
        {
            foreach (DataRow item in dtOffline_RangeData.Rows)
            {
                DataRow dr = dtRangeData.NewRow();
                dr["Company"] = Convert.ToString(item["Company"]);
                dr["TotalLeads"] = Convert.ToInt32(item["Company"]);
                dr["TotalBookings"] = "0";
                dr["Profit_amount"] = "0";
                dtRangeData.Rows.Add(dr);
            }
        }

     

    }

    #endregion


    protected void btnSearchDateRangeData_Click(object sender, EventArgs e)
    {
        var txtFromDate = Request.Form["ctl00$ContentPlaceHolder1$txtFromDate"];
        var txtToDate = Request.Form["ctl00$ContentPlaceHolder1$txtToDate"];

       
       
    }
}
