using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web.Services;
using System.Web.UI.WebControls;

public partial class Admin_LeadDashboard : System.Web.UI.Page
{
    FandHServices.FandHServicesClient objServices = new FandHServices.FandHServicesClient();
    UserDetail objUserDetail = null;
    public string SourceMediaList = string.Empty;
    GetSetDatabase objGetSetDatabase = new GetSetDatabase();
    Common.DateTimeFilter dateTimeFilter = new Common.DateTimeFilter();
    DataTable dtFlight = new DataTable();
    DataTable dtSample = new DataTable();
    DataTable dtCampaign = new DataTable();
    List<LeadConversion> lstLeadConversion = new List<LeadConversion>();
    LeadConversion leadConversion = null;
    List<string> strCampaign = new List<string>();
    string StatusList = "Booked,Confirm,Decline,Documents,Payments,Incomplete,Issued,ReIssued,Follow UP,Option,Queue,Dupe,Refund,Deposit Forfeited,ETicket Sent,TKTNotFound,Completed,Future Credit,Charge Back";
    readonly Layout lo = new Layout();
    int isDataTrue = 0;

    clsMarketingSection clsMarketingSections = new clsMarketingSection();
    List<clsMarketingSection> lstMarketingSection = new List<clsMarketingSection>();

    DataTable dtInsertMonthWiseData = new DataTable();
    DataTable dtInsertWeekWiseData = new DataTable();
    public static DataTable dtInsert = GetInsertDataTable();
    DataTable dtTrackerClicks = new DataTable();
    public static string UserId = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserDetails"] != null)
        {
            objUserDetail = Session["UserDetails"] as UserDetail;
            UserId = objUserDetail.userID;
            hdnPageSearch.Value = "0";
            lblMsg.Text = "";

            GetLeadConversion("", "");
            OneMonthCalculationtData("", "");
            CompanyList();

            if (!IsPostBack)
            {
                if (!objUserDetail.isAuth("LeadDashboard"))
                {
                    Response.Redirect("Default.aspx");
                    return;
                }
                else
                {
                    if (objUserDetail.userRole.ToLower() == "superadmin" || objUserDetail.userRole.ToLower() == "team head ft")
                {
                    txtMarketingSection.Text = MarketingSection("", "");
                    txtSalesData.Text = SalesSection("", "");
                    MonthWiseData();
                    LastFiveDaysData();
                    hdnUserRole.Value = objUserDetail.userRole.ToLower();
                }
                else if (objUserDetail.userRole.ToLower() == "online" ||
                        objUserDetail.userRole.ToLower() == "operatorft" ||
                        objUserDetail.userRole.ToLower() == "operator" ||
                        objUserDetail.userRole.ToLower() == "agentft")
                {
                    MarketingSection("", "");
                    txtSalesData.Text = SalesSection("", "");
                    hdnUserRole.Value = objUserDetail.userRole.ToLower();
                }
                else if (objUserDetail.userRole.ToLower() == "fareft")
                {
                    txtMarketingSection.Text = MarketingSection("", "");
                    txtSalesData.Text = SalesSection("", "");
                    hdnUserRole.Value = objUserDetail.userRole.ToLower();
                }
               }


                DateTime dateTime = DateTime.Now;
                DateTime firstDayOfMonth = new DateTime(dateTime.Year, dateTime.Month, 1);

                var today = DateTime.Today.ToString("dd");
                var Year = DateTime.Now.Year.ToString();
                DateTime previous_Month = DateTime.Now.AddMonths(-1);
                int previousMonth_Days = DateTime.DaysInMonth(previous_Month.Year, previous_Month.Month);
                if (DateTime.Now.Month != 2)
                {
                    if (today == "04")
                    {
                        lblLastMonth.InnerText = "Month Data (From " + DateTime.Today.AddDays(-3).Day.ToString() + " " + DateTime.Now.ToString("MMM") + " to " + DateTime.Today.Day + "th " + dateTime.ToString("MMM") + " " + Year + ")";
                        lblLastWeek.InnerText = "Weekly Data (From " + Convert.ToString(previousMonth_Days) + " " + DateTime.Now.ToString("MMM") + " to " + DateTime.Today.Day + "th " + dateTime.ToString("MMM") + " " + Year + ")";
                        hd5.InnerText = DateTime.Today.Day.ToString();
                        hd4.InnerText = DateTime.Today.AddDays(-1).Day.ToString();
                        hd3.InnerText = DateTime.Today.AddDays(-2).Day.ToString();
                        hd2.InnerText = DateTime.Today.AddDays(-3).Day.ToString();
                        hd1.InnerText = Convert.ToString(previousMonth_Days);
                    }
                    if (today == "03")
                    {
                        lblLastMonth.InnerText = "Month Data (From " + DateTime.Today.AddDays(-3).Day.ToString() + " " + DateTime.Now.ToString("MMM") + " to " + DateTime.Today.AddDays(-1) + "rd " + dateTime.ToString("MMM") + " " + Year + ")";
                        lblLastWeek.InnerText = "Weekly Data (From " + Convert.ToString(previousMonth_Days - 1) + " " + previous_Month.ToString("MMM") + " to " + DateTime.Today.AddDays(-1) + "rd " + dateTime.ToString("MMM") + " " + Year + ")";
                        hd5.InnerText = DateTime.Today.AddDays(-1).Day.ToString();
                        hd4.InnerText = DateTime.Today.AddDays(-2).Day.ToString();
                        hd3.InnerText = DateTime.Today.AddDays(-3).Day.ToString();
                        hd2.InnerText = Convert.ToString(previousMonth_Days - 1);
                        hd1.InnerText = Convert.ToString(previousMonth_Days);
                    }
                    if (today == "02")
                    {
                        lblLastMonth.InnerText = "Month Data (From " + DateTime.Today.AddDays(-3).Day.ToString() + " " + DateTime.Now.ToString("MMM") + " to " + DateTime.Today.AddDays(-2) + "rd " + dateTime.ToString("MMM") + " " + Year + ")";
                        lblLastWeek.InnerText = "Weekly Data (From " + Convert.ToString(previousMonth_Days - 2) + " " + previous_Month.ToString("MMM") + " to " + DateTime.Today.AddDays(-2) + "rd " + dateTime.ToString("MMM") + " " + Year + ")";
                        hd5.InnerText = DateTime.Today.AddDays(-2).Day.ToString();
                        hd4.InnerText = DateTime.Today.AddDays(-3).Day.ToString();
                        hd3.InnerText = Convert.ToString(previousMonth_Days - 2);
                        hd2.InnerText = Convert.ToString(previousMonth_Days - 1);
                        hd1.InnerText = Convert.ToString(previousMonth_Days);
                    }
                    if (today == "01")
                    {
                        lblLastMonth.InnerText = "Month Data (From " + DateTime.Today.AddDays(-3).Day.ToString() + " " + DateTime.Now.ToString("MMM") + " to " + DateTime.Today.AddDays(-3) + "rd " + dateTime.ToString("MMM") + " " + Year + ")";
                        lblLastWeek.InnerText = "Weekly Data (From " + Convert.ToString(previousMonth_Days - 3) + " " + previous_Month.ToString("MMM") + " to " + DateTime.Today.AddDays(-3) + "rd " + dateTime.ToString("MMM") + " " + Year + ")";
                        hd5.InnerText = DateTime.Today.Day.ToString();
                        hd4.InnerText = Convert.ToString(previousMonth_Days - 3);
                        hd3.InnerText = Convert.ToString(previousMonth_Days - 2);
                        hd2.InnerText = Convert.ToString(previousMonth_Days - 1);
                        hd1.InnerText = Convert.ToString(previousMonth_Days);
                    }
                }

                //No of Days Start from 5
                if (Convert.ToInt32(today) > 4)
                {
                    lblLastMonth.InnerText = "Last Month Data (From " + firstDayOfMonth.Day + " " + dateTime.ToString("MMM") + " to " + RemoveZeroFromDay(Convert.ToInt32(today)) + " " + dateTime.ToString("MMM") + " " + Year + ")";
                    lblLastWeek.InnerText = "Last Week Data (From " + (Convert.ToInt32(today) - 4) + " " + dateTime.ToString("MMM") + " to " + RemoveZeroFromDay(Convert.ToInt32(today)) + " " + dateTime.ToString("MMM") + " " + Year + ")";
                    hd1.InnerText = RemoveZeroFromDay(Convert.ToInt32(today));
                    hd2.InnerText = Convert.ToString(Convert.ToInt32(today) - 1);
                    hd3.InnerText = Convert.ToString(Convert.ToInt32(today) - 2);
                    hd4.InnerText = Convert.ToString(Convert.ToInt32(today) - 3);
                    hd5.InnerText = Convert.ToString(Convert.ToInt32(today) - 4);
                }
            }
        }
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


    public DataTable OneMonthCalculationtData(string FromDate, string Todate)
    {
        dtFlight = new DataTable();
        dtFlight = objGetSetDatabase.SearchPageTracker("", "", "", "", "", "", "",
        FromDate != "" ? FromDate : dateTimeFilter.TodayDayofMonth,
        Todate != "" ? Convert.ToDateTime(Todate).ToString() : dateTimeFilter.TodayDayofMonth,
        "", "", "", "", "", "", "", "",
        SourceMediaList, "", SourceMediaList, "Report");
        return dtFlight;
    }



    [WebMethod(EnableSession = true)]
    public static List<RetVal> UpdateMetaClick(string CampaignId, int MetaClicks, string FromDateMS, string ToDateMS)
    {
        RetVal retVal = new RetVal();
        List<RetVal> lstRetVal = new List<RetVal>();
        GetSetDatabase objGetSetDatabase = new GetSetDatabase();

        DataTable dt = objGetSetDatabase.GetCPC_Online_Cost_Detail(FromDateMS, ToDateMS, CampaignId, "Update", MetaClicks, UserId);

        if (dt.Rows.Count > 0)
        {
            foreach (DataRow dtRow in dt.Rows)
            {
                retVal = new RetVal
                {
                    Company_Name = dtRow["Company"].ToString(),
                    Total_Spend_By_Company = dtRow["TotalSpentByCompany"].ToString(),
                    Total_Spend_By_Campaign = dtRow["TotalSpendByCampaign"].ToString()
                };
                lstRetVal.Add(retVal);
            }
        }
        else
        {
            lstRetVal = new List<RetVal>();
        }
        return lstRetVal;
    }


    public void MonthWiseData()
    {
        dtInsertMonthWiseData = objGetSetDatabase.GET_SET_LastMonthData_LeadDashboard(0, 0, "", "SELECT","");
        if (dtInsertMonthWiseData.Rows.Count > 0)
        {
            double spendData = Convert.ToDouble(dtInsertMonthWiseData.Compute("SUM(Spent)", string.Empty));
            double returnData = Convert.ToDouble(dtInsertMonthWiseData.Compute("SUM(Return)", string.Empty));
            double ratio = returnData / spendData;
            txtSpend.InnerText = "£" + spendData;
            txtReturn.InnerText = "£" + returnData.ToString().Replace("-", "");
            txtRatio.InnerText = "1 / " + Math.Round(decimal.Parse(ratio.ToString()), 2).ToString();
            chartDataforLastMonth.Text = (spendData + "@" + returnData);
        }
        else
        {
            txtSpend.InnerText = "NA";
            txtReturn.InnerText = "NA";
            txtRatio.InnerText = "NA";
        }
    }


    public void LastFiveDaysData()
    {
        ChartDataForLastFiveDays chartDataForLastFiveDays = new ChartDataForLastFiveDays();


        dtInsertWeekWiseData = objGetSetDatabase.GET_SET_LastWeekData_LeadDashboard(dtInsert, "SELECT");

        if (dtInsertWeekWiseData.Rows.Count > 0)
        {
            var dayWiseData = from g in dtInsertWeekWiseData.AsEnumerable()
                              group g by new
                              {
                                  Spend = Convert.ToDouble(g.Field<double>("Spent")),
                                  Return = Convert.ToDouble(g.Field<double>("Return")),
                                  Ratio = Convert.ToDouble(g.Field<double>("Ratio")),
                                  DateTime = Convert.ToDateTime(g.Field<DateTime>("Cdate")).ToString("dd/MM/yyyy"),
                              } into WeekData
                              let p = new
                              {
                                  WeekData.Key.Spend,
                                  WeekData.Key.Return,
                                  WeekData.Key.Ratio,
                                  WeekData.Key.DateTime,
                                  Total = WeekData.Count()
                              }
                              select p;

            foreach (var item in dayWiseData)
            {
                #region Day 5

                if (item.DateTime == DateTime.Today.ToString("dd/MM/yyyy"))
                {
                    txtSpend1.InnerText = "£" + item.Spend;
                    txtReturn1.InnerText = "£" + Convert.ToString(item.Return).Replace("-", "");
                    txtRatio1.InnerText = Convert.ToString(item.Ratio).Replace("-", "");

                    chartDataForLastFiveDays.Spend1 = Convert.ToString(item.Spend);
                    chartDataForLastFiveDays.Return1 = Convert.ToString(item.Return);
                }


                #endregion

                #region Day 4

                if (item.DateTime == DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy"))
                {
                    txtSpend2.InnerText = "£" + item.Spend;
                    txtReturn2.InnerText = "£" + Convert.ToString(item.Return).Replace("-", "");
                    txtRatio2.InnerText = Convert.ToString(item.Ratio).Replace("-", "");

                    chartDataForLastFiveDays.Spend2 = Convert.ToString(item.Spend);
                    chartDataForLastFiveDays.Return2 = Convert.ToString(item.Return);

                }


                #endregion

                #region Day 3

                if (item.DateTime == DateTime.Now.AddDays(-2).ToString("dd/MM/yyyy"))
                {
                    txtSpend3.InnerText = "£" + item.Spend;
                    txtReturn3.InnerText = "£" + Convert.ToString(item.Return).Replace("-", "");
                    txtRatio3.InnerText = Convert.ToString(item.Ratio).Replace("-", "");

                    chartDataForLastFiveDays.Spend3 = Convert.ToString(item.Spend);
                    chartDataForLastFiveDays.Return3 = Convert.ToString(item.Return);
                }


                #endregion

                #region Day 2

                if (item.DateTime == DateTime.Now.AddDays(-3).ToString("dd/MM/yyyy"))
                {
                    txtSpend4.InnerText = "£" + item.Spend;
                    txtReturn4.InnerText = "£" + Convert.ToString(item.Return).Replace("-", "");
                    txtRatio4.InnerText = Convert.ToString(item.Ratio).Replace("-", "");

                    chartDataForLastFiveDays.Spend4 = Convert.ToString(item.Spend);
                    chartDataForLastFiveDays.Return4 = Convert.ToString(item.Return);
                }


                #endregion

                #region Day 1

                if (item.DateTime == DateTime.Now.AddDays(-4).ToString("dd/MM/yyyy"))
                {
                    txtSpend5.InnerText = "£" + item.Spend;
                    txtReturn5.InnerText = "£" + Convert.ToString(item.Return).Replace("-", "");
                    txtRatio5.InnerText = Convert.ToString(item.Ratio).Replace("-", "");

                    chartDataForLastFiveDays.Spend5 = Convert.ToString(item.Spend);
                    chartDataForLastFiveDays.Return5 = Convert.ToString(item.Return);
                }
                #endregion
            }



            DataView dv = dtInsertWeekWiseData.DefaultView;
            dv.Sort = "Return asc";
            DataTable sortedDT = dv.ToTable();

            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(ChartDataForLastFiveDays));
            MemoryStream msObj = new MemoryStream();
            js.WriteObject(msObj, chartDataForLastFiveDays);

            msObj.Position = 0;
            StreamReader sr = new StreamReader(msObj);
            string json = sr.ReadToEnd();
            sr.Close();
            msObj.Close();

            lblMinValue.InnerText = Convert.ToString(Convert.ToInt32(sortedDT.AsEnumerable().Min(row => row["Spent"])));
            lblChartDataforLastWeek.Text = json;
        }
        else
        {
            txtSpend1.InnerText = "NA";
            txtReturn1.InnerText = "NA";
            txtRatio1.InnerText = "NA";

            txtSpend2.InnerText = "NA";
            txtReturn2.InnerText = "NA";
            txtRatio2.InnerText = "NA";

            txtSpend3.InnerText = "NA";
            txtReturn3.InnerText = "NA";
            txtRatio3.InnerText = "NA";

            txtSpend4.InnerText = "NA";
            txtReturn4.InnerText = "NA";
            txtRatio4.InnerText = "NA";

            txtSpend5.InnerText = "NA";
            txtReturn5.InnerText = "NA";
            txtRatio5.InnerText = "NA";
        }
    }

    public string CompanyList()
    {
        dtCampaign = CommanBinding.BindCampaignDetailsForDT(objUserDetail.userID, "Select_FT_Campaign");
        if (dtCampaign != null && dtCampaign.Rows.Count > 0)
        {
            foreach (DataRow dr in dtCampaign.Rows)
            {
                if (string.IsNullOrEmpty(SourceMediaList))
                {
                    SourceMediaList = Convert.ToString(dr["CompanyID"]);
                }
                else if (!SourceMediaList.Contains(Convert.ToString(dr["CompanyID"])))
                {
                    SourceMediaList += "," + Convert.ToString(dr["CompanyID"]);
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

    public int TrackerClick(string mediaValue, string FromDate, string ToDate)
    {
        strCampaign = GetDistintValue(dtCampaign.AsEnumerable().Select(z => z.Field<string>("CompanyID").Split(',')).ToList());
        var strCampaigns = string.Join<string>(",", strCampaign);
        int ClicksCount = 0;

        if (isDataTrue == 0)
        {
            dtTrackerClicks = objGetSetDatabase.SearchPageTrackerOnline_FORFT("", "", "", "", "", "", strCampaigns,
        FromDate != "" ? FromDate : dateTimeFilter.TodayDayofMonth,
        ToDate != "" ? Convert.ToDateTime(ToDate).ToString() : dateTimeFilter.NextDayofMonth,
        "", "", "", "", "", "", "", "", "", "", "", "Select");
            isDataTrue = 1;
        }
        var filterData = dtTrackerClicks;

        var result = from r in filterData.AsEnumerable()
                     where (r.Field<string>("COMP_DTL_Company_ID") == "FLTTROTT" ||
                            r.Field<string>("COMP_DTL_Company_ID") == "TRAVELOFLIUK")
                     select r;
        if (result.Count() > 0)
        {
            filterData = result.CopyToDataTable();
        }
        else
        {
            filterData = new DataTable();
        }

        var result1 = from r in filterData.AsEnumerable()
                      where
                      r.Field<string>("ReqSource") == mediaValue.ToUpper()
                      select r;
        if (result1.Count() > 0)
        {
            filterData = result1.CopyToDataTable();
        }
        else
        {
            filterData = new DataTable();
        }


        //According to Rohan Change by Dinesh on 05 Feb 2021
        if (strCampaign[0] == "FLTTROTT" || strCampaign[1] == "TRAVELOFLIUK")
        {
            var resultPage = from r in filterData.AsEnumerable()
                             where r.Field<string>("Page") == "passengerDetails.aspx"
                             select r;
            if (resultPage.Count() > 0)
            {
                filterData = resultPage.CopyToDataTable();
            }
        }

        if (filterData.Rows.Count > 0)
        {
            var FlightList = from g in filterData.AsEnumerable()
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
            ClicksCount = FlightList.AsEnumerable().Select(z => z.NoOfHits).Sum();
        }
        else
        {
            ClicksCount = 0;
        }
        return ClicksCount;
    }

    public string MarketingSection(string fromDate_MS, string ToDate_MS)
    {
        if (IsPostBack)
        {
            GetLeadConversion(fromDate_MS, ToDate_MS);
            OneMonthCalculationtData(fromDate_MS, ToDate_MS);
        }

        StringBuilder sbMain = new StringBuilder();
        StringBuilder sbParent = new StringBuilder();
        StringBuilder sbChild = new StringBuilder();
        int newDTCounter = 0;
        int trackerClickChild = 0;
        double TotalMetaClick = 0;
        string Campaign = string.Empty;
        int iCounter = 1;
        int trackerClickParent = 0;
        int trackerClickParent2 = 0;
        strCampaign = GetDistintValue(dtCampaign.AsEnumerable().Select(z => z.Field<string>("CompanyID").Split(',')).ToList());
        var strCampaigns = string.Join<string>(",", strCampaign);
        string childCampaignName = "";
        string parentCampaignName = "";
        double total_Spend_FT = 0;
        double total_Spend_TFK = 0;

        dtSample = objGetSetDatabase.GetCPC_Online_Cost_Detail(
            fromDate_MS != "" ? fromDate_MS : dateTimeFilter.TodayDayofMonth,
            ToDate_MS != "" ? Convert.ToDateTime(ToDate_MS).ToString() : dateTimeFilter.TodayDayofMonth,
            string.Empty,
            "Select",0,
            objUserDetail.userID);

        for (int i = 0; i < strCampaign.Count(); i++)
        {
            var resultData = dtSample.AsEnumerable().Where(z => z.Field<string>("CompanyId").ToLower() == strCampaign[i].ToLower());
            if (resultData.Count() > 0)
            {
                DataTable filterDataTable = resultData.CopyToDataTable();
                newDTCounter = filterDataTable.Rows.Count > 0 ? filterDataTable.Rows.Count : 2;
                TotalMetaClick = filterDataTable.AsEnumerable().Sum(x => Convert.ToInt32(x.Field<string>("Meta_Click")));
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

                parentCampaignName = GetCampaignName(Convert.ToString(strCampaign[i]).ToLower(), "Parent");

                sbParent.Append("<tr class='parent' id='row123" + i
                    + "' title='Click to expand/collapse' style='cursor: pointer;'>"
                    + "<td><b>" + parentCampaignName + "</b></td>"
                    //+ "<td> " + ((parentCampaignName == "Flight Trotters") ? "@@trackerClickParent@@" : "@@trackerClickParent2@@") + "</td>"
                    + "<td id=metaClickP_" + parentCampaignName + ">" + TotalMetaClick + "</td>"
                    + "<td id=pccP_" + parentCampaignName + ">" + Convert.ToString(filterDataTable.Rows[0]["CPC_Cost"]) + "</td>"
                    + "<td id=spentP_" + (Convert.ToString(strCampaign[i])) + "> " + ((parentCampaignName == "Flight Trotters") ? "@@total_Spend@@" : "@@total_Spend2@@") + "</td>"
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
                    //if (Campaign != "FLTTROTT" || Campaign != "TRAVELOFLIUK")
                    //{
                    trackerClickChild = TrackerClick(Campaign.ToLower(), fromDate_MS, ToDate_MS);
                    childCampaignName = GetCampaignName(Campaign, "Child");
                    double total_Spend = (Convert.ToDouble(filterDataTable.Rows.Count > 0 ? Convert.ToString(filterDataTable.Rows[j]["Meta_Click"]) : "0") * Convert.ToDouble(filterDataTable.Rows[j]["CPC_Cost"]));


                    sbChild.Append("<tr class='child-row123" + i + " expand hideme' style='display: none;'>"
                                  + "<td>" + childCampaignName + "</td>"
                                  //+ "<td>" + trackerClickChild + "</td> "
                                  + "<td>" + (filterDataTable.Rows.Count > 0 ? Convert.ToString(filterDataTable.Rows[j]["Meta_Click"]) : "0") + "</td> "

                                  //+ "<td><input type='text' maxlength='7' pattern='\\d+' class='txtRow' id='txt_" + iCounter + "' name='name' value='" + (filterDataTable.Rows.Count > 0 ? Convert.ToString(filterDataTable.Rows[j]["Meta_Click"]) : "0") + "' style='height:25px;width:70px;padding-left:7px;margin-top:0px;margin-bottom:0px;' onblur=\"oblurUpdate('" + Campaign + "','" + (filterDataTable.Rows.Count > 0 ? Convert.ToString(filterDataTable.Rows[j]["Meta_Click"]) : "0") + "')\"/></td>"

                                  + "<td id=pcC_" + Campaign + ">" + Convert.ToString(filterDataTable.Rows[j]["CPC_Cost"]) + "</td>"
                                  + "<td id=spentC_" + Campaign + ">" + total_Spend + "</td>"
                                  + "<td>" + Lead_Count_Child + "</td>"
                                  + "<td>" + Conversion_Count_Child + "</td>"
                                  + "<td>" + Booking_Percentage_Child + "</td>"
                                  + "</tr>");

                    if (parentCampaignName == "Flight Trotters")
                    {
                        trackerClickParent += trackerClickChild;
                        total_Spend_FT += total_Spend;
                    }
                    else if (parentCampaignName == "traveloflights.co.uk") //TRAVELOFLIUK
                    {
                        trackerClickParent2 += trackerClickChild;
                        total_Spend_TFK += total_Spend;
                    }
                    //}
                    iCounter++;

                    clsMarketingSections = new clsMarketingSection();
                    clsMarketingSections.CampaignName = childCampaignName;
                    clsMarketingSections.TrackerClicks = trackerClickChild;
                    clsMarketingSections.MetaClicks = (filterDataTable.Rows.Count > 0 ? Convert.ToInt32(filterDataTable.Rows[j]["Meta_Click"]) : 0);
                    clsMarketingSections.PCC = Convert.ToDouble(filterDataTable.Rows[j]["CPC_Cost"]);
                    clsMarketingSections.Spent = total_Spend;
                    clsMarketingSections.Lead = Lead_Count_Child;
                    clsMarketingSections.Conversion = Conversion_Count_Child;
                    clsMarketingSections.Bookings = Booking_Percentage_Child;

                    lstMarketingSection.Add(clsMarketingSections);
                }
            }
           

            sbParent.Replace("@@trackerClickParent@@", Convert.ToString(trackerClickParent));
            sbParent.Replace("@@trackerClickParent2@@", Convert.ToString(trackerClickParent2));

            sbParent.Replace("@@total_Spend@@", Convert.ToString(total_Spend_FT));
            sbParent.Replace("@@total_Spend2@@", Convert.ToString(total_Spend_TFK));


            sbParent.Append(sbChild.ToString());
        }


        DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(List<clsMarketingSection>));
        MemoryStream msObj = new MemoryStream();
        js.WriteObject(msObj, lstMarketingSection);

        msObj.Position = 0;
        StreamReader sr = new StreamReader(msObj);
        string json = sr.ReadToEnd();
        sr.Close();
        msObj.Close();

        lblChartMarketingSection.Text = json;


        sbMain.Append("<table id='detail_table' class='detail' style='width:100%!important''>"
                      + "<tr> "
                      + "<th>Medium </th>"
                      //+ "<th>Tracker Clicks </th>"
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
            retValue = dtCampaign.AsEnumerable().Where(z => z.Field<string>("CampID").ToLower() == value.ToLower() && z.Field<string>("CompanyID").ToLower() == value.ToLower()).Select(z => z.Field<string>("CampName")).FirstOrDefault();
        }
        else
        {
            retValue = dtCampaign.AsEnumerable().Where(z => z.Field<string>("CampID").ToLower() == value.ToLower()).Select(z => z.Field<string>("CampName")).FirstOrDefault();
        }
        return retValue;
    }

    public List<LeadConversion> GetLeadConversion(string FromDate, string ToDate)
    {
        lstLeadConversion = new List<LeadConversion>();
        var getData = objGetSetDatabase.GET_BookingDetail_Date("", "", "", "", "",
        FromDate != "" ? FromDate : dateTimeFilter.TodayDayofMonth,
        ToDate != "" ? Convert.ToDateTime(ToDate).ToString() : dateTimeFilter.TodayDayofMonth,
        "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");

        var queryData = from row in getData.AsEnumerable()
                        group row by new
                        {
                            BookingId = row.Field<string>("BookingID"),
                            BookingStatus = row.Field<string>("BookingStatus"),
                            BookingByCompany = row.Field<string>("BookingByCompany"),
                            SourceMedia = row.Field<string>("SourceMedia"),
                            BookingBy = row.Field<string>("BookingBy")
                        } into Booking
                        orderby Booking.Count() descending
                        select new
                        {
                            Conversion = Booking.Key.BookingStatus,
                            Leads = Booking.Key.BookingId,
                            CompanyName = Booking.Key.BookingByCompany,
                            SourceMedia = Booking.Key.SourceMedia,
                            BookingBy = Booking.Key.BookingBy
                        };

        foreach (var item in queryData)
        {
            leadConversion = new LeadConversion
            {
                Conversion = item.Conversion,
                Leads = item.Leads,
                CompanyName = item.CompanyName,
                SourceMedia = item.SourceMedia,
                BookingBy = item.BookingBy,
            };
            lstLeadConversion.Add(leadConversion);
        }
        return lstLeadConversion;

    }

    public string SalesSection(string FromDate, string ToDate)
    {
        if (IsPostBack)
        {
            MarketingSection(FromDate, ToDate);
        }

        UserDetail objUserDetail = Session["UserDetails"] as UserDetail;
        var FT_USERS_LIST = CommanBinding.BindCampaignDetailsForDT(objUserDetail.userID, "Select_Company_Users");
 
        string role = objUserDetail.userRole.ToLower();
        DataTable dtUserData = new DataTable();
        StringBuilder sbSalesMain = new StringBuilder();
        StringBuilder sbSalesUser = new StringBuilder();
        StringBuilder sbSalesNew = new StringBuilder();

        double All_Booking_Wise_Profit = 0;
        string Campaign = string.Empty;
        string UserName = string.Empty;
        string Conversion_Ratio = string.Empty;
        string BookingStatus = string.Empty;
        string BookingID = string.Empty;
        int offlineLeads = 0;
        int online_OfflineBooking = 0;

        strCampaign = GetDistintValue(dtCampaign.AsEnumerable().Select(z => z.Field<string>("CompanyID").Split(',')).ToList());
        var strCampaigns = string.Join<string>(",", strCampaign);

        if (role != "superadmin" && role != "admin" && role != "team head" && role != "team head ft" && role != "team head ca" && role != "marketing head" && role != "onlinetl")
        {
            dtUserData = objServices.GET_Agents_P_L(null,
                objUserDetail.userID,
                (FromDate != "" ? FromDate : dateTimeFilter.TodayDayofMonth),
                (ToDate != "" ? Convert.ToDateTime(ToDate).AddDays(1).ToString("dd/MM/yyyy") : dateTimeFilter.NextDayofMonth),
                strCampaigns,
                StatusList);
        }
        else
        {
            dtUserData = objServices.GET_Agents_P_L(null,
                null,
                (FromDate != "" ? FromDate : dateTimeFilter.TodayDayofMonth),
                (ToDate != "" ? Convert.ToDateTime(ToDate).AddDays(1).ToString("dd/MM/yyyy") : dateTimeFilter.NextDayofMonth),
                strCampaigns,
                StatusList);
        }

        var queryData = from row in dtUserData.AsEnumerable()
                        group row by new
                        {
                            BookingId = row.Field<string>("BookingRef"),
                            BookingStatus = row.Field<string>("BookingStatus"),
                            BookingByCompany = row.Field<string>("Company"),
                            BookingBy = row.Field<string>("Booking_By"),
                            Profit_Amout = row.Field<decimal>("Profit_Amout"),
                            Destination = row.Field<string>("Destination"),
                            SourceMedia = row.Field<string>("SourceMedia"),
                            Date_Time = row.Field<DateTime>("Booking_Date_Time")
                        } into Booking
                        orderby Booking.Count() descending
                        select new
                        {
                            BookingStatus = Booking.Key.BookingStatus,
                            BookingID = Booking.Key.BookingId,
                            CompanyName = Booking.Key.BookingByCompany,
                            BookingBy = Booking.Key.BookingBy,
                            Profit_Amout = Booking.Key.Profit_Amout,
                            Destination = Booking.Key.Destination,
                            SourceMedia = Booking.Key.SourceMedia,
                            Date_Time = Booking.Key.Date_Time,
                        };



        for (int j = 0; j < FT_USERS_LIST.Rows.Count; j++)
        {
            StringBuilder sbSalesCampaign = new StringBuilder();
            UserName = queryData.Where(z => z.BookingBy.ToLower() == Convert.ToString(FT_USERS_LIST.Rows[j]["UserId"]).ToLower()).FirstOrDefault() != null ? queryData.Where(z => z.BookingBy.ToLower() == Convert.ToString(FT_USERS_LIST.Rows[j]["UserId"]).ToLower()).FirstOrDefault().BookingBy : "";  //Convert.ToString(dtUserData.Rows[j]["Booking_By"]);
            if (!string.IsNullOrEmpty(UserName))
            {
                BookingID = queryData.Where(z => z.BookingBy.ToLower() == Convert.ToString(FT_USERS_LIST.Rows[j]["UserId"]).ToLower()).FirstOrDefault().BookingID;
                Campaign = queryData.Where(z => z.BookingBy.ToLower() == Convert.ToString(FT_USERS_LIST.Rows[j]["UserId"]).ToLower()).FirstOrDefault().CompanyName;  //Convert.ToString(dtUserData.Rows[j]["Company"]);
                BookingStatus = queryData.Where(z => z.BookingBy.ToLower() == Convert.ToString(FT_USERS_LIST.Rows[j]["UserId"]).ToLower()).FirstOrDefault().BookingStatus;  //Convert.ToString(dtUserData.Rows[j]["BookingStatus"]);

                int Lead_Count = queryData.Where(z => z.BookingBy.ToLower() == Convert.ToString(FT_USERS_LIST.Rows[j]["UserId"]).ToLower()).Select(z => z.BookingID).Count();
                
                var getADT_CHD_IFT_DATA = objGetSetDatabase.GET_Passenger_Details_For_Sales(BookingID, UserName, (FromDate != "" ? FromDate : dateTimeFilter.TodayDayofMonth));
                All_Booking_Wise_Profit = queryData.Where(z => z.BookingBy.ToLower() == Convert.ToString(FT_USERS_LIST.Rows[j]["UserId"]).ToLower()
                && (z.BookingStatus == "Booked" || z.BookingStatus == "Completed" || z.BookingStatus == "Queue" || z.BookingStatus == "ETicket Sent")).Sum(z => Convert.ToDouble(z.Profit_Amout));
                 
                int Bookings = queryData.Where(z => z.BookingBy.ToLower() == Convert.ToString(FT_USERS_LIST.Rows[j]["UserId"]).ToLower()
                && (z.BookingStatus == "Booked" || z.BookingStatus == "Completed" || z.BookingStatus == "Queue" || z.BookingStatus == "ETicket Sent")).Select(z => z.BookingID).Count();

                if (Lead_Count > 0 && Bookings > 0)
                {
                    var offlineBooking = getADT_CHD_IFT_DATA.Tables[1].AsEnumerable().Where(z => z.Field<string>("Agent_Name") == UserName &&
                        (z.Field<string>("Status") == "Booked")).Count();

                     online_OfflineBooking = Bookings + offlineBooking;
                    Conversion_Ratio = Convert.ToString(Math.Round(Convert.ToDouble(online_OfflineBooking * 100 / Lead_Count), 2)) + "%";
                }
                else
                {
                    Conversion_Ratio = "NA";
                }

                sbSalesUser.Append("<tr class='parent' id='row123" + j + "' title='Click to expand/collapse' style='cursor: pointer;'>"
                + "<td><b>" + UserName + "</b></td>"
                + "<td>@@Lead_Count@@</td>"
                + "<td>" + online_OfflineBooking + "</td>"
                + "<td>" + Conversion_Ratio + "</td>"
                + "<td colspan = '6'>" + (All_Booking_Wise_Profit > 0 ? All_Booking_Wise_Profit : 0) + "</td></tr>");


                sbSalesCampaign.Append("<tr class='child-row123" + j + " expand hideme' style='display:none'>"
                        + "<th>Booking Ref</th>"
                        + "<th>Destination</th>"
                        + "<th>ADT</th>"
                        + "<th>CHD</th>"
                        + "<th>INF</th>"
                        + "<th>COMPANY</th>"
                        + "<th>SRC MEDIA</th>"
                        + "<th>STATUS</th>"
                        + "<th>PROFIT</th>"
                        + "<th>DATE_TIME</th>"
                        + "</tr>");

                foreach (var item in queryData.Where(z => z.BookingBy.ToLower() == Convert.ToString(FT_USERS_LIST.Rows[j]["UserId"]).ToLower()))
                {

                    sbSalesCampaign.Append("<tr class='child-row123" + j + " expand hideme' style='display:none' >"
                    + "<td>" + item.BookingID + "</td>"
                    + "<td>" + item.Destination + "</td>"
                    + "<td>" + Convert.ToString(getADT_CHD_IFT_DATA.Tables[0].Rows[0]["PAX_ADT"]) + "</td>"
                    + "<td>" + Convert.ToString(getADT_CHD_IFT_DATA.Tables[0].Rows[0]["PAX_CHD"]) + "</td>"
                    + "<td>" + Convert.ToString(getADT_CHD_IFT_DATA.Tables[0].Rows[0]["PAX_INF"]) + "</td>"
                    + "<td>" + lo.SetCompanyDetail(item.CompanyName).Comp_Emailid.Split('@')[1] + "</td>"
                    + "<td>" + (item.SourceMedia ?? "NA") + "</td>"
                    + "<td>" + item.BookingStatus + "</td>"
                    + "<td>" + (item.Profit_Amout > 0 ? Convert.ToDouble(item.Profit_Amout) : 0) + "</td>"
                    + "<td>" + (item.Date_Time).ToString("dd-MM-yyyy hh:mm") + "</td>");
                }

                //foreach loop for call details
                //count leads for Agent
                foreach (DataRow dataRow in getADT_CHD_IFT_DATA.Tables[1].Rows)
                {
                    sbSalesCampaign.Append("<tr class='child-row123" + j + " expand hideme' style='display:none' >"
                    + "<td>" + Convert.ToString(dataRow["BookingID"]) + "</td>"
                    + "<td>" + Convert.ToString(dataRow["Destination"]) + "</td>"
                    + "<td>" + Convert.ToString(dataRow["ADT"]) + "</td>"
                    + "<td>" + Convert.ToString(dataRow["CHD"]) + "</td>"
                    + "<td>" + Convert.ToString(dataRow["INF"]) + "</td>"
                    + "<td>" + lo.SetCompanyDetail(Convert.ToString(dataRow["Company"])).Comp_Emailid.Split('@')[1] + "</td>"
                    + "<td>" + (Convert.ToString(dataRow["SRC_Media"]) ?? "NA") + "</td>"
                    + "<td>" + Convert.ToString(dataRow["Status"]) + "</td>"
                    + "<td>0</td>"
                    + "<td>" + Convert.ToDateTime(Convert.ToString(dataRow["Created_Date"])).ToString("dd-MM-yyyy hh:mm") + "</td>");
                }

                if (getADT_CHD_IFT_DATA != null)
                {
                    if (getADT_CHD_IFT_DATA.Tables[1].Rows.Count > 0)
                    {
                        offlineLeads = getADT_CHD_IFT_DATA.Tables[1].AsEnumerable().Where(z => z.Field<string>("Agent_Name") == UserName).Count();
                        sbSalesUser.Replace("@@Lead_Count@@", Convert.ToString(Lead_Count + offlineLeads));
                    }
                    else
                    {
                        sbSalesUser.Replace("@@Lead_Count@@", Convert.ToString(Lead_Count));
                    }
                }


                sbSalesUser.Append(sbSalesCampaign.ToString());
            }
        }

        sbSalesMain.Append("<table id='basic' style='width:100%' border='1'>"
                                   + "<tr>"
                                   + "<th>AGENTS_NAME</th>"
                                   + "<th>LEADS</th>"
                                   + "<th>BOOKINGS </th>"
                                   + "<th>CONVERSION </th>"
                                   + "<th colspan = '6'>PROFIT</th>"
                                   + "</tr>"
                                   + sbSalesUser.ToString()
                                   + ""
                                   + "</table>");


        #region Insert Data

        //Insert Data for Last Five Daya//
        //Marketing se Spend of both FT/TFK
        //SC Total Profit of all agent per day basis
        IEnumerable<DataRow> currentDate = null;

        dtInsertMonthWiseData = objGetSetDatabase.GET_SET_LastMonthData_LeadDashboard(0, 0, "", "SELECT","");
        dtInsertWeekWiseData = objGetSetDatabase.GET_SET_LastWeekData_LeadDashboard(dtInsert, "SELECT");

        //Check if Current Date Data inserted into Table or not
        currentDate = dtInsertWeekWiseData.AsEnumerable().Where(z => z.Field<DateTime>("Cdate").ToString("dd/MM/yyyy") == DateTime.Today.ToString("dd/MM/yyyy"));
        dtInsert = GetInsertDataTable();

        if (currentDate.Count() == 0)
        {
            var totalSpendDayWise = lstMarketingSection.Select(z => z.Spent).Sum(); //lstMarketingSection.Where(z => z.CampaignName == "FLTTROTT" || z.CampaignName == "TRAVELOFLIUK").Select(z => z.Spent).Sum();
            var totalReturnDayWise = queryData.Sum(z => Convert.ToDouble(z.Profit_Amout));

            if (totalReturnDayWise > 0 && totalSpendDayWise > 0)
            {
                DataRow dr = dtInsert.NewRow();
                dr["Spend"] = totalSpendDayWise;
                dr["Return"] = totalReturnDayWise;
                dr["Ratio"] = (totalSpendDayWise > 0 ? Math.Round((totalReturnDayWise / totalSpendDayWise), 2) : 0);
                dr["CreatedDate"] = (FromDate != "" ? FromDate : dateTimeFilter.TodayDayofMonth);

                dtInsert.Rows.Add(dr);
                objGetSetDatabase.GET_SET_LastWeekData_LeadDashboard(dtInsert, "INSERT");

                //Insert for Month Data Per day basis
                objGetSetDatabase.GET_SET_LastMonthData_LeadDashboard(totalSpendDayWise, totalReturnDayWise, Convert.ToString(totalSpendDayWise > 0 ? Math.Round((totalReturnDayWise / totalSpendDayWise), 2) : 0), "INSERT", (FromDate != "" ? FromDate : dateTimeFilter.TodayDayofMonth));
            }
        }
        else
        {
            var totalSpendDayWise = lstMarketingSection.Select(z => z.Spent).Sum(); //lstMarketingSection.Where(z => z.CampaignName == "FLTTROTT" || z.CampaignName == "TRAVELOFLIUK").Select(z => z.Spent).Sum();
            var totalReturnDayWise = queryData.Sum(z => Convert.ToDouble(z.Profit_Amout));

            if (totalReturnDayWise > 0 && totalSpendDayWise > 0)
            {
                DataRow dr = dtInsert.NewRow();
                dr["Spend"] = totalSpendDayWise;
                dr["Return"] = totalReturnDayWise;
                dr["Ratio"] = (totalSpendDayWise > 0 ? Math.Round((totalReturnDayWise / totalSpendDayWise), 2) : 0);
                dr["CreatedDate"] = (FromDate != "" ? FromDate : dateTimeFilter.TodayDayofMonth);

                dtInsert.Rows.Add(dr);
                objGetSetDatabase.GET_SET_LastWeekData_LeadDashboard(dtInsert, "UPDATE");

                //Insert for Month Data Per day basis
                objGetSetDatabase.GET_SET_LastMonthData_LeadDashboard(totalSpendDayWise, totalReturnDayWise, Convert.ToString(totalSpendDayWise > 0 ? Math.Round((totalReturnDayWise / totalSpendDayWise), 2) : 0), "UPDATE", (FromDate != "" ? FromDate : dateTimeFilter.TodayDayofMonth));
            }
        }

        MonthWiseData();
        LastFiveDaysData();

        #endregion


        return sbSalesMain.ToString();
    }

    protected void btnSearchSalesData_Click(object sender, EventArgs e)
    {
        var txtfromDate_1 = Request.Form["ctl00$ContentPlaceHolder1$txtFromDate"];
        var txtToDate_2 = Request.Form["ctl00$ContentPlaceHolder1$txtToDate"];

        txtSalesData.Text = SalesSection(txtfromDate_1, txtToDate_2);
        hdnUserRole.Value = objUserDetail.userRole.ToLower();
        hdnPageSearch.Value = "2";
        txtFromDate.Value = "";
        txtToDate.Value = "";
    }

    protected void btnMarketingData_Click(object sender, EventArgs e)
    {
        var txtFromDate_MS_1 = Request.Form["ctl00$ContentPlaceHolder1$txtFromDate_MS_Search"];
        var txtToDate_MS_2 = Request.Form["ctl00$ContentPlaceHolder1$txtToDate_MS_Search"];

        txtMarketingSection.Text = MarketingSection(txtFromDate_MS_1, txtToDate_MS_2);
        hdnUserRole.Value = objUserDetail.userRole.ToLower();

        txtToDate_MS_Search.Value = "";
        txtFromDate_MS_Search.Value = "";
        hdnPageSearch.Value = "1";
    }


    public class LeadConversion
    {
        public string Conversion { get; set; }
        public string Leads { get; set; }
        public string CompanyName { get; set; }
        public string SourceMedia { get; set; }
        public string BookingBy { get; set; }
    }

    public class ChartDataForLastFiveDays
    {
        public string Spend1 { get; set; }
        public string Return1 { get; set; }

        public string Spend2 { get; set; }
        public string Return2 { get; set; }

        public string Spend3 { get; set; }
        public string Return3 { get; set; }

        public string Spend4 { get; set; }
        public string Return4 { get; set; }

        public string Spend5 { get; set; }
        public string Return5 { get; set; }
    }

    public class clsMarketingSection
    {
        public string CampaignName { get; set; }
        public int TrackerClicks { get; set; }

        public double PCC { get; set; }
        public int MetaClicks { get; set; }
        public double Spent { get; set; }
        public int Lead { get; set; }
        public int Conversion { get; set; }
        public string Bookings { get; set; }
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

    public class RetVal
    {
        public string Total_Spend_By_Campaign { get; set; }
        public string Total_Spend_By_Company { get; set; }
        public string Company_Name { get; set; }
    }


    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (fileUpload.HasFile)
        {
            string FileName = Path.GetFileName(fileUpload.PostedFile.FileName);
            string Extension = Path.GetExtension(fileUpload.PostedFile.FileName);
            string FileNameWoithoutExtension = Path.GetFileNameWithoutExtension(fileUpload.PostedFile.FileName);

            string fileName = FileNameWoithoutExtension + "_" + DateTime.Now.ToString("ddMMMyyyy") + Extension;

            if (!Directory.Exists(Server.MapPath("~/Admin/LeadTracker/MarketingExcelFiles/")))
            {
                Directory.CreateDirectory(Server.MapPath("~/Admin/LeadTracker/MarketingExcelFiles/"));
            }
            string FilePath = Server.MapPath("~/Admin/LeadTracker/MarketingExcelFiles/" + fileName);
            fileUpload.SaveAs(FilePath);
            SaveExcelRows(FilePath, Extension, "Yes");

            txtFromDate_MS.Value = "";
            txtToDate_MS.Value = "";
            hdnPageSearch.Value = "1";
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
        }
        else
        {
            lblMsg.Text = "Please choose an excel file to proceed";
        }
    }
    public void SaveExcelRows(string FilePath, string Extension, string isHDR)
    {
        UserDetail objUserDetail = Session["UserDetails"] as UserDetail;
        string sExcelconnectionstring = "";
        switch (Extension)
        {
            case ".xls": //Excel 97-03
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
            string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
            OleDbCon.Close();
            System.Data.DataTable dt = CreateExcelforMarketingSection();
            OleDbCommand OleDbCmd = new OleDbCommand("Select * FROM [" + SheetName + "]", OleDbCon);
            OleDbCon.Open();
            using (DbDataReader dr = OleDbCmd.ExecuteReader())
            {
                while (dr.Read())
                {
                 dt.Rows.Add(CreateExcelforMarketingSectionDataRow(dt.NewRow(),
                        dr["CampaignID"].ToString(),
                        dr["MetaClicks"].ToString(),
                        dr["UploadedDate"].ToString(),
                        objUserDetail.userID));
                }

                using (SqlConnection SqlCon = DataConnection.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = SqlCon;
                        cmd.CommandText = "BulkUpdate_ForMarketingSection";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ParamdtMetaClick", dt);
                        try
                        {
                            SqlCon.Open();
                            int i = cmd.ExecuteNonQuery();
                            lblMsg.Text = i + " Records are successfully Uploaded out of " + dt.Rows.Count;
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

    private DataRow CreateExcelforMarketingSectionDataRow(DataRow dr,string CampaignId,
        string MetaClicks,string uploadedDate,string uploadedBy)
    {
        try
        {
            dr["CPC_MC_CampaignID"] = CampaignId;
            dr["CPC_MC_MetaClicks"] = MetaClicks;
            dr["CPC_MC_RecordDate"] = Convert.ToDateTime(uploadedDate);
            dr["CPC_MC_CreatedBy"] = uploadedBy;
            dr["CPC_MC_CreatedDate"] = DateTime.Now;
            dr["CPC_MC_ModifiedDate"] = DateTime.Now.AddYears(-5);
            dr["CPC_MC_ModifiedBy"] = "";
        }
        catch (Exception ex)
        {

        }
        return dr;
    }


    private DataTable CreateExcelforMarketingSection()
    {
        DataTable dt = new DataTable
        {
            TableName = "Online_CPC_Detail_MetaClick"
        };
        dt.Columns.Add("CPC_MC_CampaignID", typeof(string));
        dt.Columns.Add("CPC_MC_MetaClicks", typeof(int));
        dt.Columns.Add("CPC_MC_RecordDate", typeof(DateTime));
        dt.Columns.Add("CPC_MC_CreatedDate", typeof(DateTime));
        dt.Columns.Add("CPC_MC_CreatedBy", typeof(string));
        dt.Columns.Add("CPC_MC_ModifiedDate", typeof(DateTime));
        dt.Columns.Add("CPC_MC_ModifiedBy", typeof(string));

        return dt;
    }
}

