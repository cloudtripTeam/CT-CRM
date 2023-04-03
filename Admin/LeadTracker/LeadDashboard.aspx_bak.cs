using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.Services;
using System.Web.UI.WebControls;

public partial class Admin_LeadDashboard : System.Web.UI.Page
{
    UserDetail objUserDetail = null;
    decimal TotalSpendValue = 0;
    double TotalCost = 0;
    double TotalSell = 0;
    double TotalProfit = 0;
    double IncompleteProfit = 0;
    double PaidProfit = 0;
    public string SourceMediaList = string.Empty;
    GetSetDatabase objGetSetDatabase = new GetSetDatabase();
    Common.DateTimeFilter dateTimeFilter = new Common.DateTimeFilter();
    DataTable dtProfitMonthData = new DataTable();
    DataTable dtCPC = new DataTable();
    DataTable dtFlight = new DataTable();
    DataTable dtSample = new DataTable();
    DataTable dtCampaign = new DataTable();
    List<LeadConversion> lstLeadConversion = new List<LeadConversion>();
    LeadConversion leadConversion = null;
    List<string> strCampaign = new List<string>();
    string StatusList = "Booked,Confirm,Decline,Documents,Payments,Incomplete,Issued,ReIssued,Follow UP,Option,Queue,Dupe,Refund,Deposit Forfeited,ETicket Sent,TKTNotFound,Completed";

    DataTable dtInsertMonthWiseData = new DataTable();
    DataTable dtInsertWeekWiseData = new DataTable();
    public static DataTable dtInsert = GetInsertDataTable();
    DataTable dtSalesData = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserDetails"] != null)
        {
            if (!IsPostBack)
            {
                objUserDetail = Session["UserDetails"] as UserDetail;
            }
        }

        DateTime dateTime = DateTime.Now;
        DateTime firstDayOfMonth = new DateTime(dateTime.Year, dateTime.Month, 1);

        var today = DateTime.Today.ToString("dd");
        var Year = DateTime.Now.Year.ToString();
        DateTime previous_Month = DateTime.Now.AddMonths(-1);
        int previousMonth_Days = DateTime.DaysInMonth(previous_Month.Year, previous_Month.Month);

        if (today == "04")
        {
            lblLastMonth.InnerText = "Month Data (From " + DateTime.Today.AddDays(-3).Day.ToString() + DateTime.Now.ToString("MMM") + " to " + DateTime.Today.Day + "th " + dateTime.ToString("MMM") + " " + Year + ")";
            lblLastWeek.InnerText = "Weekly Data (From " + Convert.ToString(previousMonth_Days) + DateTime.Now.ToString("MMM") + " to " + DateTime.Today.Day + "th " + dateTime.ToString("MMM") + " " + Year + ")";
            hd5.InnerText = DateTime.Today.Day.ToString();
            hd4.InnerText = DateTime.Today.AddDays(-1).Day.ToString();
            hd3.InnerText = DateTime.Today.AddDays(-2).Day.ToString();
            hd2.InnerText = DateTime.Today.AddDays(-3).Day.ToString();
            hd1.InnerText = Convert.ToString(previousMonth_Days);
        }
        if (today == "03")
        {
            lblLastMonth.InnerText = "Month Data (From " + DateTime.Today.AddDays(-3).Day.ToString() + DateTime.Now.ToString("MMM") + " to " + DateTime.Today.AddDays(-1) + "rd " + dateTime.ToString("MMM") + " " + Year + ")";
            lblLastWeek.InnerText = "Weekly Data (From " + Convert.ToString(previousMonth_Days - 1) + previous_Month.ToString("MMM") + " to " + DateTime.Today.AddDays(-1) + "rd " + dateTime.ToString("MMM") + " " + Year + ")";
            hd5.InnerText = DateTime.Today.AddDays(-1).Day.ToString();
            hd4.InnerText = DateTime.Today.AddDays(-2).Day.ToString();
            hd3.InnerText = DateTime.Today.AddDays(-3).Day.ToString();
            hd2.InnerText = Convert.ToString(previousMonth_Days - 1);
            hd1.InnerText = Convert.ToString(previousMonth_Days);
        }
        if (today == "02")
        {
            lblLastMonth.InnerText = "Month Data (From " + DateTime.Today.AddDays(-3).Day.ToString() + DateTime.Now.ToString("MMM") + " to " + DateTime.Today.AddDays(-2) + "rd " + dateTime.ToString("MMM") + " " + Year + ")";
            lblLastWeek.InnerText = "Weekly Data (From " + Convert.ToString(previousMonth_Days - 2) + previous_Month.ToString("MMM") + " to " + DateTime.Today.AddDays(-2) + "rd " + dateTime.ToString("MMM") + " " + Year + ")";
            hd5.InnerText = DateTime.Today.AddDays(-2).Day.ToString();
            hd4.InnerText = DateTime.Today.AddDays(-3).Day.ToString();
            hd3.InnerText = Convert.ToString(previousMonth_Days - 2);
            hd2.InnerText = Convert.ToString(previousMonth_Days - 1);
            hd1.InnerText = Convert.ToString(previousMonth_Days);
        }
        if (today == "01")
        {
            lblLastMonth.InnerText = "Month Data (From " + DateTime.Today.AddDays(-3).Day.ToString() + DateTime.Now.ToString("MMM") + " to " + DateTime.Today.AddDays(-3) + "rd " + dateTime.ToString("MMM") + " " + Year + ")";
            lblLastWeek.InnerText = "Weekly Data (From " + Convert.ToString(previousMonth_Days - 3) + previous_Month.ToString("MMM") + " to " + DateTime.Today.AddDays(-3) + "rd " + dateTime.ToString("MMM") + " " + Year + ")";
            hd5.InnerText = DateTime.Today.Day.ToString();
            hd4.InnerText = Convert.ToString(previousMonth_Days - 3);
            hd3.InnerText = Convert.ToString(previousMonth_Days - 2);
            hd2.InnerText = Convert.ToString(previousMonth_Days - 1);
            hd1.InnerText = Convert.ToString(previousMonth_Days);
        }


        //No of Days Start from 5
        if (Convert.ToInt32(today) > 4)
        {
            lblLastMonth.InnerText = "Last Month Data (From " + firstDayOfMonth.Day + dateTime.ToString("MMM") + " to " + RemoveZeroFromDay(Convert.ToInt32(today)) + " " + dateTime.ToString("MMM") + " " + Year + ")";
            lblLastWeek.InnerText = "Last Week Data (From " + (Convert.ToInt32(today) - 4) + dateTime.ToString("MMM") + " to " + RemoveZeroFromDay(Convert.ToInt32(today)) + " " + dateTime.ToString("MMM") + " " + Year + ")";
            hd5.InnerText = RemoveZeroFromDay(Convert.ToInt32(today));
            hd4.InnerText = Convert.ToString(Convert.ToInt32(today) - 1);
            hd3.InnerText = Convert.ToString(Convert.ToInt32(today) - 2);
            hd2.InnerText = Convert.ToString(Convert.ToInt32(today) - 3);
            hd1.InnerText = Convert.ToString(Convert.ToInt32(today) - 4);
        }


        GetLeadConversion();
        OneMonthPCCData();
        OneMonthCalculationtData();
        GetSalesData();

        MonthWiseData();
        LastFiveDaysData();
        txtMarketingSection.Text = MarketingSection();
        txtSalesData.Text = SalesSection();

    }

    public string RemoveZeroFromDay(int day)
    {
        string dayValue;
        if (day <= 9)
        {
            dayValue = DateTime.Today.Day.ToString();
        }
        else
        {
            dayValue = DateTime.Today.ToString("dd");
        }
        return dayValue;
    }


    public DataTable GetSalesData()
    {
        return dtSalesData = objGetSetDatabase.GET_SET_LeadDashBoard_Sales_Data(SourceMediaList, StatusList, Convert.ToDateTime(dateTimeFilter.FirstDay), Convert.ToDateTime(dateTimeFilter.TodayDayofMonth));
    }


    public DataTable OneMonthPCCData()
    {
        return dtProfitMonthData = Get_Profit(dateTimeFilter.FirstDayOfMonth, dateTimeFilter.TodayDayofMonth);
    }

    public DataTable OneMonthCalculationtData()
    {
        dtCPC = objGetSetDatabase.GetCPC_Online_Cost_Detail(string.Empty, string.Empty, "Select", 0);
        dtFlight = objGetSetDatabase.SearchPageTracker("", "", "", "", "", "", "",
        dateTimeFilter.FirstDay, dateTimeFilter.TodayDayofMonth, "", "", "", "", "", "", "", "",
        CompanyList(), "", CompanyList(), "Report");
        return dtFlight;
    }


    [WebMethod(EnableSession = true)]
    public static string UpdateMetaClick(string CampaignId, int MetaClicks)
    {
        GetSetDatabase objGetSetDatabase = new GetSetDatabase();
        DataTable a = objGetSetDatabase.GetCPC_Online_Cost_Detail(string.Empty, CampaignId, "Update", MetaClicks);
        return "true";
    }


    public void MonthWiseData()
    {
        double spendData = 0;
        double returnData = 0;
        double ratio = 0;
        dtInsertMonthWiseData = objGetSetDatabase.GET_SET_LastMonthData_LeadDashboard(0, 0, "", "SELECT");
        IEnumerable<DataRow> cDate1 = null;

        if (dtInsertMonthWiseData.Rows.Count > 0)
        {
            cDate1 = dtInsertMonthWiseData.AsEnumerable().Where(z => z.Field<DateTime>("Cdate").ToString("dd/MM/yyyy") == DateTime.Today.ToString("dd/MM/yyyy"));
        }

        if (cDate1.Count() == 0)
        {
            if (dtProfitMonthData != null)
            {
                DataTable dd = FilterData(dateTimeFilter.FirstDay, dateTimeFilter.TodayDayofMonth);
                foreach (DataRow dr in dd.Rows)
                {
                    TotalCost += Convert.ToDouble(dr["CostPrice"]);
                    TotalSell += Convert.ToDouble(dr["SellPrice"]);
                    TotalProfit += Convert.ToDouble(dr["Profit"]);

                    if (dr["Booking_Status"].ToString().ToLower() == "incomplete")
                    {
                        IncompleteProfit += Convert.ToDouble(dr["Profit"]);
                    }
                    else if (dr["Booking_Status"].ToString().ToLower() == "decline" || dr["Booking_Status"].ToString().ToLower() == "confirm")
                    {
                        PaidProfit += Convert.ToDouble(dr["Profit"]);
                    }
                }
            }

            spendData = Convert.ToDouble(Math.Round(GET_CPC_Data(dateTimeFilter.FirstDay, dateTimeFilter.TodayDayofMonth), 2));
            returnData = Convert.ToDouble(Math.Round(PaidProfit, 2));
            txtSpend.InnerText = "$" + spendData;
            txtReturn.InnerText = "$" + returnData.ToString().Replace("-", "");
            ratio = returnData / spendData;
            txtRatio.InnerText = Math.Round(ratio, 2).ToString().Replace("-", "");
            dtInsertMonthWiseData = objGetSetDatabase.GET_SET_LastMonthData_LeadDashboard(spendData, returnData, txtRatio.InnerText, "INSERT");
        }
        else
        {
            dtInsertMonthWiseData = objGetSetDatabase.GET_SET_LastMonthData_LeadDashboard(0, 0, "", "SELECT");
            spendData = Convert.ToDouble(dtInsertMonthWiseData.Compute("SUM(Spent)", string.Empty));
            returnData = Convert.ToDouble(dtInsertMonthWiseData.Compute("SUM(Return)", string.Empty));
            ratio = returnData / spendData;

            txtSpend.InnerText = "$" + spendData;
            txtReturn.InnerText = "$" + returnData.ToString().Replace("-", "");
            txtRatio.InnerText = Math.Round(ratio, 2).ToString().Replace("-", "");
        }
    }

    private DataTable FilterData(string startDate, string EndDate)
    {
        var newDT = dtProfitMonthData.AsEnumerable().Where(z => DateTime.Parse(z.Field<string>("BookingDate")) >= DateTime.Parse(startDate)
                    && DateTime.Parse(z.Field<string>("BookingDate")) <= DateTime.Parse(EndDate));
        var query = from row in newDT.AsEnumerable()
                    group row by new
                    {
                        Status = row.Field<string>("Booking_Status"),
                        BookingDate = row.Field<string>("BookingDate")

                    } into Booking
                    orderby Booking.Count() descending
                    select new
                    {
                        Booking.Key.Status,
                        BookigDate = Booking.Key.BookingDate,
                        CostPrice = Booking.Sum(x => x.Field<decimal>("CostPrice")),
                        SellPrice = Booking.Sum(x => x.Field<decimal>("SellPrice")),
                        Profit = Booking.Sum(x => x.Field<decimal>("Profit"))
                    };


        DataTable dtBooking = new DataTable();
        dtBooking.Columns.Add("Booking_Status", typeof(string));
        dtBooking.Columns.Add("BookingDate", typeof(string));
        dtBooking.Columns.Add("CostPrice", typeof(decimal));
        dtBooking.Columns.Add("SellPrice", typeof(decimal));
        dtBooking.Columns.Add("Profit", typeof(decimal));
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

    public decimal GET_CPC_Data(string StartDate, string EndDate)
    {
        var FlightList = from g in dtFlight.AsEnumerable().Where(z => Convert.ToDateTime(z.Field<string>("DatenTime")) >= Convert.ToDateTime(StartDate) &&
                         Convert.ToDateTime(z.Field<string>("DatenTime")) <= Convert.ToDateTime(EndDate))
                         group g by new
                         {
                             Destination = g.Field<string>("Destination"),
                             Origin = g.Field<string>("Origin"),
                             ReqSource = g.Field<string>("ReqSource"),
                             DatenTime = Convert.ToDateTime(g.Field<string>("DatenTime")).ToString("dd-MM-yyyy"),
                         } into FlightGroup
                         select new
                         {
                             FlightGroup.Key.Destination,
                             FlightGroup.Key.Origin,
                             FlightGroup.Key.ReqSource,
                             FlightGroup.Key.DatenTime,
                             NoOfHits = FlightGroup.Count()
                         };

        var dataSet = FlightList.OrderByDescending(x => x.NoOfHits).ToList();
        var result = from a in dataSet.AsEnumerable()
                     join b in dtCPC.AsEnumerable()
                     on a.ReqSource.ToLower() equals b.Field<string>("CompanyId").ToLower()
                     select new
                     {
                         TotalCost = Convert.ToDecimal(a.NoOfHits) * Convert.ToDecimal(dtCPC.Rows[0]["CPC_Cost"])
                     };

        TotalSpendValue = Convert.ToDecimal(result.Select(z => z.TotalCost).Sum());
        return TotalSpendValue;
    }

    private DataTable Get_Profit(string FirstDayOfMonth, string TodayDayofMonth)
    {
        GetSetDatabase objGetSetDatabase = new GetSetDatabase();
        DataTable dt = objGetSetDatabase.Get_ProfitLoss(FirstDayOfMonth, TodayDayofMonth, CompanyList());
        if (dt != null)
        {
            var query = from row in dt.AsEnumerable()
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
                            CostPrice = Booking.Sum(x => x.Field<decimal>("CostPrice")),
                            SellPrice = Booking.Sum(x => x.Field<decimal>("SellPrice")),
                            Profit = Booking.Sum(x => x.Field<decimal>("Profit"))
                        };

            DataTable dtBooking = new DataTable();
            dtBooking.Columns.Add("Booking_Status", typeof(string));
            dtBooking.Columns.Add("BookingDate", typeof(string));
            dtBooking.Columns.Add("CostPrice", typeof(decimal));
            dtBooking.Columns.Add("SellPrice", typeof(decimal));
            dtBooking.Columns.Add("Profit", typeof(decimal));
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

    public void LastFiveDaysData()
    {
        DataTable dt = new DataTable();
        double returnData1 = 0; double spendData1 = 0; double ratio1 = 0;
        double returnData2 = 0; double spendData2 = 0; double ratio2 = 0;
        double returnData3 = 0; double spendData3 = 0; double ratio3 = 0;
        double returnData4 = 0; double spendData4 = 0; double ratio4 = 0;
        double returnData5 = 0; double spendData5 = 0; double ratio5 = 0;


        DataRow drDay1Data = dtInsert.NewRow();
        DataRow drDay2Data = dtInsert.NewRow();
        DataRow drDay3Data = dtInsert.NewRow();
        DataRow drDay4Data = dtInsert.NewRow();
        DataRow drDay5Data = dtInsert.NewRow();

        #region Day 1 Data 
        dtInsertWeekWiseData = objGetSetDatabase.GET_SET_LastWeekData_LeadDashboard(dtInsert, "SELECT");
        IEnumerable<DataRow> cDate = null;
        if (dtInsertWeekWiseData.Rows.Count > 0)
        {
            cDate = dtInsertWeekWiseData.AsEnumerable().Where(z => z.Field<DateTime>("Cdate").ToString("dd/MM/yyyy") == DateTime.Today.ToString("dd/MM/yyyy"));
        }

        if (cDate.Count() == 0)
        {

            spendData1 = Convert.ToDouble(Math.Round(GET_CPC_Data(dateTimeFilter.FirstDay, dateTimeFilter.TodayDayofMonth), 2));
            if (spendData1 > 0)
            {
                dt = Get_Profit(dateTimeFilter.FirstDay, dateTimeFilter.TodayDayofMonth);
                if (dt != null)
                {
                    PaidProfit = 0;
                    DataTable dd = FilterData(dateTimeFilter.FirstDay, dateTimeFilter.TodayDayofMonth);
                    foreach (DataRow dr in dd.Rows)
                    {
                        TotalCost += Convert.ToDouble(dr["CostPrice"]);
                        TotalSell += Convert.ToDouble(dr["SellPrice"]);
                        TotalProfit += Convert.ToDouble(dr["Profit"]);

                        if (dr["Booking_Status"].ToString().ToLower() == "incomplete")
                        {
                            IncompleteProfit += Convert.ToDouble(dr["Profit"]);
                        }
                        else if (dr["Booking_Status"].ToString().ToLower() == "decline" || dr["Booking_Status"].ToString().ToLower() == "confirm")
                        {
                            PaidProfit += Convert.ToDouble(dr["Profit"]);
                        }
                    }
                }

                returnData1 = Convert.ToDouble(Math.Round(PaidProfit, 2));
                txtSpend1.InnerText = "$" + spendData1;
                txtReturn1.InnerText = "$" + returnData1.ToString().Replace("-", "");
                ratio1 = returnData1 / spendData1;
                txtRatio1.InnerText = Math.Round(ratio1, 2).ToString().Replace("-", "");

                drDay1Data["Spend"] = spendData1;
                drDay1Data["Return"] = returnData1;
                drDay1Data["Ratio"] = txtRatio1.InnerText;
                drDay1Data["CreatedDate"] = DateTime.Now;
                dtInsert.Rows.Add(drDay1Data);
            }
            else
            {
                txtSpend1.InnerText = "NA";
                txtReturn1.InnerText = "NA";
                txtRatio1.InnerText = "NA";
            }
        }
        else
        {
            dtInsertWeekWiseData = objGetSetDatabase.GET_SET_LastWeekData_LeadDashboard(dtInsert, "SELECT");
            var query_Data = from row in dtInsertWeekWiseData.AsEnumerable().Where(z => z.Field<DateTime>("CDate").ToString("dd/MM/yyyy") == Convert.ToString(dateTimeFilter.TodayDayofMonth))
                             group row by new
                             {
                                 Cdate = row.Field<DateTime>("Cdate"),
                                 Spent = row.Field<double>("Spent"),
                                 Return = row.Field<double>("Return"),
                                 Ratio = Math.Round(row.Field<double>("Return") / row.Field<double>("Spent"), 2).ToString().Replace("-", "")

                             } into DT
                             orderby DT.Count() descending
                             select new
                             {
                                 Spend = DT.Key.Spent,
                                 DT.Key.Return,
                                 DT.Key.Ratio,
                                 DT.Key.Cdate
                             };
            if (query_Data.Count() > 0)
            {
                foreach (var item in query_Data)
                {
                    txtSpend1.InnerText = "$" + item.Spend;
                    txtReturn1.InnerText = "$" + item.Return.ToString().Replace("-", "");
                    txtRatio1.InnerText = item.Ratio;
                }
            }
            else
            {
                txtSpend1.InnerText = "NA";
                txtReturn1.InnerText = "NA";
                txtRatio1.InnerText = "NA";
            }
        }
        #endregion

        #region Day 2 Data 

        if (dtInsertWeekWiseData.Rows.Count > 0)
        {
            cDate = dtInsertWeekWiseData.AsEnumerable().Where(z => z.Field<DateTime>("Cdate").ToString("dd/MM/yyyy") == dateTimeFilter.FirstDay);
        }

        if (cDate.Count() == 0)
        {
            spendData2 = Convert.ToDouble(Math.Round(GET_CPC_Data(dateTimeFilter.SecondDate, dateTimeFilter.FirstDay), 2));
            if (spendData2 > 0)
            {
                dt = new DataTable();
                dt = Get_Profit(dateTimeFilter.SecondDate, dateTimeFilter.FirstDay);
                if (dt != null)
                {
                    PaidProfit = 0;
                    DataTable dd = FilterData(dateTimeFilter.SecondDate, dateTimeFilter.FirstDay);
                    foreach (DataRow dr in dd.Rows)
                    {
                        TotalCost += Convert.ToDouble(dr["CostPrice"]);
                        TotalSell += Convert.ToDouble(dr["SellPrice"]);
                        TotalProfit += Convert.ToDouble(dr["Profit"]);

                        if (dr["Booking_Status"].ToString().ToLower() == "incomplete")
                        {
                            IncompleteProfit += Convert.ToDouble(dr["Profit"]);
                        }
                        else if (dr["Booking_Status"].ToString().ToLower() == "decline" || dr["Booking_Status"].ToString().ToLower() == "confirm")
                        {
                            PaidProfit += Convert.ToDouble(dr["Profit"]);
                        }
                    }
                }

                returnData2 = Convert.ToDouble(Math.Round(PaidProfit, 2));
                txtSpend2.InnerText = "$" + spendData2;
                txtReturn2.InnerText = "$" + returnData2.ToString().Replace("-", "");
                ratio2 = returnData2 / spendData2;
                txtRatio2.InnerText = Math.Round(ratio2, 2).ToString().Replace("-", "");


                drDay2Data["Spend"] = spendData2;
                drDay2Data["Return"] = returnData2;
                drDay2Data["Ratio"] = txtRatio2.InnerText;
                drDay2Data["CreatedDate"] = DateTime.Now.AddDays(-1);
                dtInsert.Rows.Add(drDay2Data);
            }
            else
            {
                txtSpend2.InnerText = "NA";
                txtReturn2.InnerText = "NA";
                txtRatio2.InnerText = "NA";
            }
        }
        else
        {
            dtInsertWeekWiseData = objGetSetDatabase.GET_SET_LastWeekData_LeadDashboard(dtInsert, "SELECT");
            var query_Data = from row in dtInsertWeekWiseData.AsEnumerable().Where(z => z.Field<DateTime>("CDate").ToString("dd/MM/yyyy") == Convert.ToString(dateTimeFilter.FirstDay))
                             group row by new
                             {
                                 Cdate = row.Field<DateTime>("Cdate"),
                                 Spent = row.Field<double>("Spent"),
                                 Return = row.Field<double>("Return"),
                                 Ratio = Math.Round(row.Field<double>("Return") / row.Field<double>("Spent"), 2).ToString().Replace("-", "")

                             } into DT
                             orderby DT.Count() descending
                             select new
                             {
                                 Spend = DT.Key.Spent,
                                 DT.Key.Return,
                                 DT.Key.Ratio,
                                 DT.Key.Cdate
                             };
            if (query_Data.Count() > 0)
            {
                foreach (var item in query_Data)
                {
                    txtSpend2.InnerText = "$" + item.Spend;
                    txtReturn2.InnerText = "$" + item.Return.ToString().Replace("-", "");
                    txtRatio2.InnerText = item.Ratio;
                }
            }
            else
            {
                txtSpend2.InnerText = "NA";
                txtReturn2.InnerText = "NA";
                txtRatio2.InnerText = "NA";
            }
        }

        #endregion

        #region Day 3 Data 
        if (dtInsertWeekWiseData.Rows.Count > 0)
        {
            cDate = dtInsertWeekWiseData.AsEnumerable().Where(z => z.Field<DateTime>("Cdate").ToString("dd/MM/yyyy") == dateTimeFilter.SecondDate);
        }

        if (cDate.Count() == 0)
        {
            spendData3 = Convert.ToDouble(Math.Round(GET_CPC_Data(dateTimeFilter.ThirdDate, dateTimeFilter.SecondDate), 2));
            if (spendData3 > 0)
            {
                dt = new DataTable();
                dt = Get_Profit(dateTimeFilter.ThirdDate, dateTimeFilter.SecondDate);
                if (dt != null)
                {
                    PaidProfit = 0;
                    DataTable dd = FilterData(dateTimeFilter.ThirdDate, dateTimeFilter.SecondDate);
                    foreach (DataRow dr in dd.Rows)
                    {
                        TotalCost += Convert.ToDouble(dr["CostPrice"]);
                        TotalSell += Convert.ToDouble(dr["SellPrice"]);
                        TotalProfit += Convert.ToDouble(dr["Profit"]);

                        if (dr["Booking_Status"].ToString().ToLower() == "incomplete")
                        {
                            IncompleteProfit += Convert.ToDouble(dr["Profit"]);
                        }
                        else if (dr["Booking_Status"].ToString().ToLower() == "decline" || dr["Booking_Status"].ToString().ToLower() == "confirm")
                        {
                            PaidProfit += Convert.ToDouble(dr["Profit"]);
                        }
                    }
                }


                returnData3 = Convert.ToDouble(Math.Round(PaidProfit, 2));
                txtSpend3.InnerText = "$" + spendData3;
                txtReturn3.InnerText = "$" + returnData3.ToString().Replace("-", "");
                ratio3 = returnData3 / spendData3;
                txtRatio3.InnerText = Math.Round(ratio3, 2).ToString().Replace("-", "");

                drDay3Data["Spend"] = spendData3;
                drDay3Data["Return"] = returnData3;
                drDay3Data["Ratio"] = txtRatio3.InnerText;
                drDay3Data["CreatedDate"] = DateTime.Now.AddDays(-2);
                dtInsert.Rows.Add(drDay3Data);
            }
            else
            {
                txtSpend3.InnerText = "NA";
                txtReturn3.InnerText = "NA";
                txtRatio3.InnerText = "NA";
            }
        }
        else
        {
            dtInsertWeekWiseData = objGetSetDatabase.GET_SET_LastWeekData_LeadDashboard(dtInsert, "SELECT");
            var query_Data = from row in dtInsertWeekWiseData.AsEnumerable().Where(z => z.Field<DateTime>("CDate").ToString("dd/MM/yyyy") == Convert.ToString(dateTimeFilter.SecondDate))
                             group row by new
                             {
                                 Cdate = row.Field<DateTime>("Cdate"),
                                 Spent = row.Field<double>("Spent"),
                                 Return = row.Field<double>("Return"),
                                 Ratio = Math.Round(row.Field<double>("Return") / row.Field<double>("Spent"), 2).ToString().Replace("-", "")

                             } into DT
                             orderby DT.Count() descending
                             select new
                             {
                                 Spend = DT.Key.Spent,
                                 DT.Key.Return,
                                 DT.Key.Ratio,
                                 DT.Key.Cdate
                             };
            if (query_Data.Count() > 0)
            {
                foreach (var item in query_Data)
                {
                    txtSpend3.InnerText = "$" + item.Spend;
                    txtReturn3.InnerText = "$" + item.Return.ToString().Replace("-", "");
                    txtRatio3.InnerText = item.Ratio;
                }
            }
            else
            {
                txtSpend3.InnerText = "NA";
                txtReturn3.InnerText = "NA";
                txtRatio3.InnerText = "NA";
            }
        }



        #endregion

        #region Day 4 Data  
        if (dtInsertWeekWiseData.Rows.Count > 0)
        {
            cDate = dtInsertWeekWiseData.AsEnumerable().Where(z => z.Field<DateTime>("Cdate").ToString("dd/MM/yyyy") == dateTimeFilter.ThirdDate);
        }

        if (cDate.Count() == 0)
        {
            if (spendData4 > 0)
            {
                spendData4 = Convert.ToDouble(Math.Round(GET_CPC_Data(dateTimeFilter.FourtDate, dateTimeFilter.ThirdDate), 2));
                dt = new DataTable();
                dt = Get_Profit(dateTimeFilter.FourtDate, dateTimeFilter.ThirdDate);
                if (dt != null)
                {
                    PaidProfit = 0;
                    DataTable dd = FilterData(dateTimeFilter.FourtDate, dateTimeFilter.ThirdDate);
                    foreach (DataRow dr in dd.Rows)
                    {
                        TotalCost += Convert.ToDouble(dr["CostPrice"]);
                        TotalSell += Convert.ToDouble(dr["SellPrice"]);
                        TotalProfit += Convert.ToDouble(dr["Profit"]);

                        if (dr["Booking_Status"].ToString().ToLower() == "incomplete")
                        {
                            IncompleteProfit += Convert.ToDouble(dr["Profit"]);
                        }
                        else if (dr["Booking_Status"].ToString().ToLower() == "decline" || dr["Booking_Status"].ToString().ToLower() == "confirm")
                        {
                            PaidProfit += Convert.ToDouble(dr["Profit"]);
                        }
                    }
                }
                returnData4 = Convert.ToDouble(Math.Round(PaidProfit, 2));
                txtSpend4.InnerText = "$" + spendData4;
                txtReturn4.InnerText = "$" + returnData4.ToString().Replace("-", "");
                ratio4 = returnData4 / spendData4;
                txtRatio4.InnerText = Math.Round(ratio4, 2).ToString().Replace("-", "");

                drDay4Data["Spend"] = spendData4;
                drDay4Data["Return"] = returnData4;
                drDay4Data["Ratio"] = txtRatio4.InnerText;
                drDay4Data["CreatedDate"] = DateTime.Now.AddDays(-3);
                dtInsert.Rows.Add(drDay4Data);
            }
            else
            {
                txtSpend4.InnerText = "NA";
                txtReturn4.InnerText = "NA";
                txtRatio4.InnerText = "NA";
            }
        }
        else
        {
            dtInsertWeekWiseData = objGetSetDatabase.GET_SET_LastWeekData_LeadDashboard(dtInsert, "SELECT");
            var query_Data = from row in dtInsertWeekWiseData.AsEnumerable().Where(z => z.Field<DateTime>("CDate").ToString("dd/MM/yyyy") == Convert.ToString(dateTimeFilter.ThirdDate))
                             group row by new
                             {
                                 Cdate = row.Field<DateTime>("Cdate"),
                                 Spent = row.Field<double>("Spent"),
                                 Return = row.Field<double>("Return"),
                                 Ratio = Math.Round(row.Field<double>("Return") / row.Field<double>("Spent"), 2).ToString().Replace("-", "")

                             } into DT
                             orderby DT.Count() descending
                             select new
                             {
                                 Spend = DT.Key.Spent,
                                 DT.Key.Return,
                                 DT.Key.Ratio,
                                 DT.Key.Cdate
                             };
            if (query_Data.Count() > 0)
            {
                foreach (var item in query_Data)
                {
                    txtSpend4.InnerText = "$" + item.Spend;
                    txtReturn4.InnerText = "$" + item.Return.ToString().Replace("-", "");
                    txtRatio4.InnerText = item.Ratio;
                }
            }
            else
            {
                txtSpend4.InnerText = "NA";
                txtReturn4.InnerText = "NA";
                txtRatio4.InnerText = "NA";
            }
        }

        #endregion

        #region Day 5 Data  
        if (dtInsertWeekWiseData.Rows.Count > 0)
        {
            cDate = dtInsertWeekWiseData.AsEnumerable().Where(z => z.Field<DateTime>("Cdate").ToString("dd/MM/yyyy") == dateTimeFilter.FourtDate);
        }

        if (cDate.Count() == 0)
        {
            spendData5 = Convert.ToDouble(Math.Round(GET_CPC_Data(dateTimeFilter.FifthDate, dateTimeFilter.FourtDate), 2));
            if (spendData5 > 0)
            {
                dt = new DataTable();
                dt = Get_Profit(dateTimeFilter.FifthDate, dateTimeFilter.FourtDate);
                if (dt != null)
                {
                    PaidProfit = 0;
                    DataTable dd = FilterData(dateTimeFilter.FifthDate, dateTimeFilter.FourtDate);
                    foreach (DataRow dr in dd.Rows)
                    {
                        TotalCost += Convert.ToDouble(dr["CostPrice"]);
                        TotalSell += Convert.ToDouble(dr["SellPrice"]);
                        TotalProfit += Convert.ToDouble(dr["Profit"]);

                        if (dr["Booking_Status"].ToString().ToLower() == "incomplete")
                        {
                            IncompleteProfit += Convert.ToDouble(dr["Profit"]);
                        }
                        else if (dr["Booking_Status"].ToString().ToLower() == "decline" || dr["Booking_Status"].ToString().ToLower() == "confirm")
                        {
                            PaidProfit += Convert.ToDouble(dr["Profit"]);
                        }
                    }
                }

                returnData5 = Convert.ToDouble(Math.Round(PaidProfit, 2));
                txtSpend5.InnerText = "$" + spendData5;
                txtReturn5.InnerText = "$" + returnData5.ToString().Replace("-", "");
                ratio5 = returnData5 / spendData5;
                txtRatio5.InnerText = Math.Round(ratio5, 2).ToString().Replace("-", "");

                drDay5Data["Spend"] = spendData5;
                drDay5Data["Return"] = returnData5;
                drDay5Data["Ratio"] = txtRatio5.InnerText;
                drDay5Data["CreatedDate"] = DateTime.Now.AddDays(-4);
                dtInsert.Rows.Add(drDay5Data);
            }
            else
            {
                txtSpend5.InnerText = "NA";
                txtReturn5.InnerText = "NA";
                txtRatio5.InnerText = "NA";
            }
        }
        else
        {
            dtInsertWeekWiseData = objGetSetDatabase.GET_SET_LastWeekData_LeadDashboard(dtInsert, "SELECT");
            var query_Data = from row in dtInsertWeekWiseData.AsEnumerable().Where(z => z.Field<DateTime>("CDate").ToString("dd/MM/yyyy") == Convert.ToString(dateTimeFilter.FourtDate))
                             group row by new
                             {
                                 Cdate = row.Field<DateTime>("Cdate"),
                                 Spent = row.Field<double>("Spent"),
                                 Return = row.Field<double>("Return"),
                                 Ratio = Math.Round(row.Field<double>("Return") / row.Field<double>("Spent"), 2).ToString().Replace("-", "")

                             } into DT
                             orderby DT.Count() descending
                             select new
                             {
                                 Spend = DT.Key.Spent,
                                 DT.Key.Return,
                                 DT.Key.Ratio,
                                 DT.Key.Cdate
                             };
            if (query_Data.Count() > 0)
            {
                foreach (var item in query_Data)
                {
                    txtSpend5.InnerText = "$" + item.Spend;
                    txtReturn5.InnerText = "$" + item.Return.ToString().Replace("-", "");
                    txtRatio5.InnerText = item.Ratio;
                }
            }
            else
            {
                txtSpend5.InnerText = "NA";
                txtReturn5.InnerText = "NA";
                txtRatio5.InnerText = "NA";
            }
        }

        if (dtInsert.Rows.Count > 0)
        {
            dtInsertWeekWiseData = objGetSetDatabase.GET_SET_LastWeekData_LeadDashboard(dtInsert, "INSERT");
        }
        #endregion
    }

    public static DataTable GetInsertDataTable()
    {
        dtInsert = new DataTable();
        dtInsert.Clear();
        dtInsert.Columns.Add("Spend", typeof(string));
        dtInsert.Columns.Add("Return", typeof(string));
        dtInsert.Columns.Add("Ratio", typeof(string));
        dtInsert.Columns.Add("CreatedDate", typeof(DateTime));
        return dtInsert;
    }

    public string CompanyList()
    {
        dtCampaign = objGetSetDatabase.GET_Campaign_Master(string.Empty, string.Empty);
        if (dtCampaign != null && dtCampaign.Rows.Count > 0)
        {
            foreach (DataRow dr in dtCampaign.Rows)
            {
                if (string.IsNullOrEmpty(SourceMediaList))
                {
                    SourceMediaList = Convert.ToString(dr["CampID"]);
                }
                else
                {
                    SourceMediaList += "," + Convert.ToString(dr["CampID"]);
                }
            }
        }
        return SourceMediaList;
    }

    public List<string> GetDistintValue(List<string[]> lstString)
    {
        List<string> newList = new List<string>();
        for (int i = 0; i < lstString.Count; i++)
        {
            if (!newList.Contains(Convert.ToString(lstString[i][0])))
            {
                newList.Add(Convert.ToString(lstString[i][0]));
            }
        }
        return newList.OrderBy(i => i).ToList();
    }

    public int TrackerClick(string mediaValue)
    {
        var DtFlight = objGetSetDatabase.SearchPageTracker("", "", "", "", "", "", "",
        dateTimeFilter.TodayDayofMonth, dateTimeFilter.TodayDayofMonth, "", "", "", "", "", "", "", "",
        mediaValue, "", mediaValue, "Report");

        var FlightList = from g in DtFlight.AsEnumerable()
                         group g by new
                         {
                             ReqSource = g.Field<string>("ReqSource"),
                             DateTime = Convert.ToDateTime(g.Field<string>("DatenTime")).ToString("dd-MM-yyyy"),
                         } into FlightGroup
                         let p = new
                         {
                             FlightGroup.Key.ReqSource,
                             FlightGroup.Key.DateTime,
                             NoOfHits = FlightGroup.Count()
                         }
                         select p;

        return FlightList.AsEnumerable().Select(z => z.NoOfHits).Sum();
    }

    public string MarketingSection()
    {
        StringBuilder sbMain = new StringBuilder();
        StringBuilder sbParent = new StringBuilder();
        StringBuilder sbChild = new StringBuilder();
        int newDTCounter = 0;
        double TotalMetaClick = 0;
        string Campaign = string.Empty;
        int iCounter = 1;
        strCampaign = GetDistintValue(dtCampaign.AsEnumerable().Select(z => z.Field<string>("CompanyID").Split(',')).ToList());
        dtSample = dtCPC;

        for (int i = 0; i < strCampaign.Count(); i++)
        {
            var resultData = dtSample.AsEnumerable().Where(z => z.Field<string>("CompanyId").ToLower() == strCampaign[i].ToLower());
            if (resultData.Count() > 0)
            {
                DataTable filterDataTable = resultData.CopyToDataTable();
                newDTCounter = filterDataTable.Rows.Count > 0 ? filterDataTable.Rows.Count : 2;
                TotalMetaClick = filterDataTable.AsEnumerable().Sum(x => x.Field<int>("Meta_Click"));
                int trackerClickParent = TrackerClick(strCampaign[i].ToLower());

                int Lead_Count_Parent = lstLeadConversion.Where(z => z.CompanyName.ToLower() == Convert.ToString(strCampaign[i]).ToLower()).Select(z => z.Leads).Count();
                int Conversion_Count_Parent = lstLeadConversion.Where(z => z.CompanyName.ToLower() == Convert.ToString(strCampaign[i]).ToLower() && z.Conversion != "Booked" && z.Conversion != "--Select--").Count();
                string Booking_Percentage_Parent = string.Empty;
                if (Lead_Count_Parent > 0)
                {
                    Booking_Percentage_Parent = Convert.ToString(Convert.ToDouble(Conversion_Count_Parent * 100 / Lead_Count_Parent)) + "%";
                }
                else
                {
                    Booking_Percentage_Parent = "NA";
                }
                sbParent.Append("<tr class='parent' id='row123" + i
                    + "' title='Click to expand/collapse' style='cursor: pointer;'>"
                    + "<td>"
                    + GetCampaignName(Convert.ToString(strCampaign[i]).ToLower(), "Parent")
                    + "</td>"
                    + "<td> " + trackerClickParent + "</td>"
                    + "<td>"
                    + TotalMetaClick
                    + "</td>"
                    + "<td>"
                    + Convert.ToString(filterDataTable.Rows[0]["CPC_Cost"])
                    + "</td>"
                    + "<td>"
                    + (TotalMetaClick * Convert.ToDouble(filterDataTable.Rows[0]["CPC_Cost"]))
                    + "</td>"
                    + "<td>" + Lead_Count_Parent + "</td>"
                    + "<td>" + Conversion_Count_Parent + "</td>"
                    + "<td> " + Booking_Percentage_Parent + "</td></tr>");
                sbChild = new StringBuilder();
                for (int j = 0; j < newDTCounter; j++)
                {
                    int Lead_Count_Child = lstLeadConversion.Where(z => z.SourceMedia.ToLower() == Convert.ToString(filterDataTable.Rows[j]["Campaign"]).ToLower()).Select(z => z.Leads).Count();
                    int Conversion_Count_Child = lstLeadConversion.Where(z => z.SourceMedia.ToLower() == Convert.ToString(filterDataTable.Rows[j]["Campaign"]).ToLower() && z.Conversion != "Booked" && z.Conversion != "--Select--").Count();
                    string Booking_Percentage_Child = string.Empty;
                    if (Lead_Count_Child > 0)
                    {
                        Booking_Percentage_Child = Convert.ToString(Convert.ToDouble(Conversion_Count_Child * 100 / Lead_Count_Child)) + "%";
                    }
                    else
                    {
                        Booking_Percentage_Child = "NA";
                    }


                    Campaign = Convert.ToString(filterDataTable.Rows[j]["Campaign"]);
                    int trackerClickChild = TrackerClick(Campaign.ToLower());

                    sbChild.Append("<tr class='child-row123" + i + " expand hideme' style='display: none;'>"
                                  + "<td>" + GetCampaignName(Campaign, "Child") + "</td>"
                                  + "<td>" + trackerClickChild + "</td> "
                                  + "<td><input type='text' maxlength='7' pattern='\\d+' class='txtRow' maxlength='5' id='txt_" + iCounter + "' name='name' value='" + (filterDataTable.Rows.Count > 0 ? Convert.ToString(filterDataTable.Rows[j]["Meta_Click"]) : "0") + "' style='height:25px;width:70px;padding-left:7px;margin-top:0px;margin-bottom:0px;' onblur=\"oblurUpdate('" + Campaign + "','" + (filterDataTable.Rows.Count > 0 ? Convert.ToString(filterDataTable.Rows[j]["Meta_Click"]) : "0") + "')\"/></td>"
                                  + "<td>" + Convert.ToString(filterDataTable.Rows[j]["CPC_Cost"]) + "</td>"
                                  + "<td>"
                                  + (Convert.ToDouble(filterDataTable.Rows.Count > 0 ? Convert.ToString(filterDataTable.Rows[j]["Meta_Click"]) : "0") * Convert.ToDouble(filterDataTable.Rows[j]["CPC_Cost"]))
                                  + "</td>"
                                  + "<td>" + Lead_Count_Child + "</td>"
                                  + "<td>" + Conversion_Count_Child + "</td>"
                                  + "<td>" + Booking_Percentage_Child + "</td>"
                                  + "</tr>");
                    iCounter++;
                }
                sbParent.Append(sbChild.ToString());
            }
        }

        sbMain.Append("<table id='detail_table' class='detail' style='width:100%!important''>"
                      + "<tr> "
                      + "<th>Medium </th>"
                      + "<th>Tracker Clicks </th>"
                      + "<th>Meta Clicks </th>"
                      + "<th>PCC </th>"
                      + "<th>Spent </th>"
                      + "<th>Lead </th>"
                      + "<th>Conversion </th>"
                      + "<th>Bookings </th>"
                      + "</tr>"
                      + sbParent.ToString()
                      + "</table>");

        return sbMain.ToString();
    }

    public string GetCampaignName(string value, string relation)
    {
        string retValue = string.Empty;
        if (relation == "Parent")
        {
            var filterDataTable = dtCampaign.AsEnumerable().OrderBy(z => z.Field<string>("CompanyID")).ToList();
            retValue = dtCampaign.AsEnumerable().Where(z => z.Field<string>("CompanyID").ToLower()
            == value.ToLower() && z.Field<string>("CampID").ToLower() == value.ToLower()).Select(z => z.Field<string>("CampName")).FirstOrDefault();
        }
        else
        {
            for (int i = 0; i < dtCampaign.Rows.Count; i++)
            {
                if (Convert.ToString(dtCampaign.Rows[i]["CampID"]).ToLower() == (value.ToLower()))
                {
                    retValue = Convert.ToString(dtCampaign.Rows[i]["CampName"]);
                }
            }
        }
        return retValue;
    }

    public List<LeadConversion> GetLeadConversion()
    {
        var getData = objGetSetDatabase.GET_BookingDetail_Date("", "", "", "", "", dateTimeFilter.TodayDayofMonth, dateTimeFilter.TodayDayofMonth, "",
            "", "", "", "", "", "", "", "", "", "", "", "", "", "");
        var queryData = from row in getData.AsEnumerable()
                        group row by new
                        {
                            BookingId = row.Field<string>("BookingID"),
                            BookingStatus = row.Field<string>("BookingStatus"),
                            BookingByCompany = row.Field<string>("BookingByCompany"),
                            SourceMedia = row.Field<string>("SourceMedia")
                        } into Booking
                        orderby Booking.Count() descending
                        select new
                        {
                            Conversion = Booking.Key.BookingStatus,
                            Leads = Booking.Key.BookingId,
                            CompanyName = Booking.Key.BookingByCompany,
                            SourceMedia = Booking.Key.SourceMedia,
                        };

        foreach (var item in queryData)
        {
            leadConversion = new LeadConversion
            {
                Conversion = item.Conversion,
                Leads = item.Leads,
                CompanyName = item.CompanyName,
                SourceMedia = item.SourceMedia,
            };
            lstLeadConversion.Add(leadConversion);
        }
        return lstLeadConversion;

    }

    public string SalesSection()
    {
        StringBuilder sbSales = new StringBuilder();
        StringBuilder sbSalesParent = new StringBuilder();
        StringBuilder sbSalesChild = new StringBuilder();
        StringBuilder sbSalesSubChild = new StringBuilder();
        string Booking_Percentage_Sales = "";
        double decBookingPercentage = 0;
        double TotalMetaClick = 0;
        int newDTCounter = 0;
        int dataNodeId = 1;
        int NodeIdForChild = 100;
        string Campaign = string.Empty;
        strCampaign = GetDistintValue(dtCampaign.AsEnumerable().Select(z => z.Field<string>("CompanyID").Split(',')).ToList());
        dtSample = dtCPC;

        for (int i = 0; i < strCampaign.Count(); i++)
        {
            var resultData = dtSample.AsEnumerable().Where(z => z.Field<string>("CompanyId").ToLower() == strCampaign[i].ToLower());
            if (resultData.Count() > 0)
            {
                DataTable filterDataTable = resultData.CopyToDataTable();
                newDTCounter = filterDataTable.Rows.Count > 0 ? filterDataTable.Rows.Count : 2;
                TotalMetaClick = filterDataTable.AsEnumerable().Sum(x => x.Field<int>("Meta_Click"));
                int trackerClickParent = TrackerClick(strCampaign[i].ToLower());

                int Lead_Count_Parent = lstLeadConversion.Where(z => z.CompanyName.ToLower() == Convert.ToString(strCampaign[i]).ToLower()).Select(z => z.Leads).Count();
                int Conversion_Count_Parent = lstLeadConversion.Where(z => z.CompanyName.ToLower() == Convert.ToString(strCampaign[i]).ToLower() && z.Conversion != "Booked" && z.Conversion != "--Select--").Count();
                string Booking_Percentage_Parent = string.Empty;
                if (Lead_Count_Parent > 0)
                {
                    Booking_Percentage_Parent = Convert.ToString(Convert.ToDouble(Conversion_Count_Parent * 100 / Lead_Count_Parent)) + "%";
                }
                else
                {
                    Booking_Percentage_Parent = "NA";
                }

                sbSalesParent.Append("<tr data-node-id='" + dataNodeId + "' class='padd'>"
                    + "<td>" + GetCampaignName(Convert.ToString(strCampaign[i]).ToLower(), "Parent") + "</td>"
                    + "<td> " + trackerClickParent + "</td>"
                    + "<td>" + TotalMetaClick + "</td>"
                    + "<td>" + Convert.ToString(filterDataTable.Rows[0]["CPC_Cost"]) + "</td>"
                    + "<td>" + (TotalMetaClick * Convert.ToDouble(filterDataTable.Rows[0]["CPC_Cost"])) + "</td>"
                    + "<td>" + Lead_Count_Parent + "</td>"
                    + "<td>" + Conversion_Count_Parent + "</td>"
                    + "<td> " + Booking_Percentage_Parent + "</td></tr>");


                sbSalesChild = new StringBuilder();
                for (int j = 0; j < newDTCounter; j++)
                {
                    Campaign = Convert.ToString(filterDataTable.Rows[j]["Campaign"]);
                    int trackerClickChild = TrackerClick(Campaign.ToLower());

                    int Lead_Count_Child = lstLeadConversion.Where(z => z.SourceMedia.ToLower() == Convert.ToString(filterDataTable.Rows[j]["Campaign"]).ToLower()).Select(z => z.Leads).Count();
                    int Conversion_Count_Child = lstLeadConversion.Where(z => z.SourceMedia.ToLower() == Convert.ToString(filterDataTable.Rows[j]["Campaign"]).ToLower() && z.Conversion != "Booked" && z.Conversion != "--Select--").Count();
                    string Booking_Percentage_Child = string.Empty;
                    if (Lead_Count_Child > 0)
                    {
                        Booking_Percentage_Child = Convert.ToString(Convert.ToDouble(Conversion_Count_Child * 100 / Lead_Count_Child)) + "%";
                    }
                    else
                    {
                        Booking_Percentage_Child = "NA";
                    }

                    sbSalesChild.Append("<tr data-node-id='" + NodeIdForChild + "' data-node-pid='" + dataNodeId + "' class='paddChild'>"
                        + "<td>" + GetCampaignName(Campaign, "Child") + "</td>"
                        + "<td>" + trackerClickChild + "</td> "
                        + "<td>" + (filterDataTable.Rows.Count > 0 ? Convert.ToString(filterDataTable.Rows[j]["Meta_Click"]) : "0") + "</td>"
                        + "<td>" + Convert.ToString(filterDataTable.Rows[j]["CPC_Cost"]) + "</td>"
                        + "<td>"
                        + (Convert.ToDouble(filterDataTable.Rows.Count > 0 ? Convert.ToString(filterDataTable.Rows[j]["Meta_Click"]) : "0") * Convert.ToDouble(filterDataTable.Rows[j]["CPC_Cost"]))
                        + "</td>"
                        + "<td>" + Lead_Count_Child + "</td>"
                        + "<td>" + Conversion_Count_Child + "</td>"
                        + "<td>" + Booking_Percentage_Child + "</td>"
                        + "</tr>");



                    //Agent Section
                    var salesResultData = dtSalesData.AsEnumerable().Where(z => z.Field<string>("Company").ToLower() == strCampaign[i].ToLower() &&
                                                                    z.Field<string>("SourceMedia").ToLower() == Campaign.ToLower());
                    
                    if (salesResultData != null && salesResultData.Count() > 0)
                    {
                        if ((Lead_Count_Parent + Lead_Count_Child) > 0)
                        {
                            decBookingPercentage = Math.Round(Convert.ToDouble((Conversion_Count_Parent + Conversion_Count_Child) * 100 / (Lead_Count_Parent + Lead_Count_Child)), 2);
                            Booking_Percentage_Sales = Convert.ToString(decBookingPercentage) + "%";
                        }
                        else
                        {
                            Booking_Percentage_Sales = "NA";
                        }
                         
                        var saleResult = from row in salesResultData.AsEnumerable().Where(z => z.Field<string>("BookingStatus") == "Booked")
                        group row by new
                        {
                        Company = row.Field<string>("Company"),
                        SourceMedia = row.Field<string>("SourceMedia"),
                        AgentName = row.Field<string>("Booking_By")
                        } into Booking
                        orderby Booking.Count() descending
                        select new
                        {
                             Booking.Key.AgentName
                        };

                        foreach (var item in saleResult)
                        {
                            sbSalesSubChild.Append("<tr data-node-id='1.1' data-node-pid='" + NodeIdForChild + "' class='paddSubChild'>" +
                            "<td>" + item.AgentName + " </td>" +
                            "<td> " + (trackerClickChild + trackerClickParent) + "</td>" +
                            "<td> " + (TotalMetaClick) + " </td>" +
                            "<td>" + Convert.ToString(filterDataTable.Rows[j]["CPC_Cost"]) + "</td>" +
                            "<td>"
                            + (Convert.ToDouble(filterDataTable.Rows.Count > 0 ? Convert.ToString(filterDataTable.Rows[j]["Meta_Click"]) : "0") * Convert.ToDouble(filterDataTable.Rows[j]["CPC_Cost"]))
                            + "</td>" +
                            "<td>" + (Lead_Count_Parent + Lead_Count_Child) + "</td>" +
                            "<td>" + (Conversion_Count_Parent + Conversion_Count_Child) + "</td>" +
                            "<td>" + Booking_Percentage_Sales + "</td>" +
                            "</tr>");
                        }
                    }
                    sbSalesChild.Append(sbSalesSubChild);
                    NodeIdForChild++;
                }
                dataNodeId++;
            }
            sbSalesParent.Append(sbSalesChild);
        }

        sbSales.Append("<table id='basic' style='width:100%' border='1'>"
                + "<tr>"
                + "<th>Medium </th>"
                + "<th>Tracker Clicks </th>"
                + "<th>Meta Clicks </th>"
                + "<th>PCC </th>"
                + "<th>Spent </th>"
                + "<th>Lead </th>"
                + "<th>Conversion </th>"
                + "<th>Bookings </th>"
                + "</tr>"
                + sbSalesParent.ToString() + ""
                + "</table>");


        return sbSales.ToString();
    }
    public class LeadConversion
    {
        public string Conversion { get; set; }
        public string Leads { get; set; }
        public string CompanyName { get; set; }
        public string SourceMedia { get; set; }
    }

}

