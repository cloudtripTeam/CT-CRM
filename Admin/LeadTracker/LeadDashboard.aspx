<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/AdminMasterPage.master" CodeFile="LeadDashboard.aspx.cs"
    Inherits="Admin_LeadDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../../css/trackerdashboard.css" rel="stylesheet" />

    <style>
        .panel-title a {
            text-decoration: none;
        }

        #btnSumitExcel {
            display: none;
        }

        .upExcel {
            background-color: green;
            color: white;
            padding: 10px;
            border-radius: 6px;
            margin-top: 23px;
            margin-left: 13px;
        }
    </style>

    <a id="lblLastMonth" runat="server" style="display: none"></a>
    <a id="lblLastWeek" runat="server" style="display: none"></a>
    <asp:HiddenField runat="server" ID="hdnUserRole" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="hdnPageSearch" ClientIDMode="Static" />


    <div class="row" style="margin-top: 15px!important">
        <div class="col-md-2">
            <button id="LastMonth" type="button" class="btn btn-primary btn-lg btncl" value="Last Month Data" onclick="HideAllDiv1(this.id); return false;">Last Month Data</button>
            <button id="LastWeek" type="button" class="btn btn-danger btn-lg btncl" value="Last Week Data" onclick="HideAllDiv2(this.id); return false;">Last Week Data</button>
            <button id="Marketing" type="button" class="btn btn-success btn-lg btncl" value="Marketing Section" onclick="HideAllDiv3(this.id); return false;">Marketing Section</button>
            <button id="Sales" type="button" class="btn btn-info btn-lg btncl" value="Sales Section" onclick="HideAllDiv4(this.id); return false;">Sales Section</button>

        </div>
        <div class="col-md-10">
            <div id="LastMonthDiv" class="hidediv">
                <span class="spanhead">Current Month Data</span>
                <div class="panel-body">
                    <table>
                        <tr>
                            <th>Spent</th>
                            <th>Return</th>
                            <th>Ratio</th>
                        </tr>
                        <tr>
                            <td runat="server" id="txtSpend" style="font-weight: bold"></td>
                            <td runat="server" id="txtReturn" style="font-weight: bold"></td>
                            <td runat="server" id="txtRatio" style="font-weight: bold"></td>
                        </tr>
                    </table>
                </div>
                <asp:Label Text="" runat="server" ID="chartDataforLastMonth" ClientIDMode="Static" Style="display: none" />
                <div>
                    <div id="chtLastMonthDataChart" class="chartClass"></div>
                </div>
            </div>

            <div id="LastWeekDiv" class="hidediv">
                <span class="spanhead">Current Week Data</span>
                <div class="panel-body">
                    <div class="in-block">
                        <table style="width: 0%!important; border: none; display: inline-block">
                            <tr>
                                <th colspan="3" style="text-align: center;" id="hd5" runat="server"></th>
                            </tr>
                            <tr>
                                <th>Spent</th>
                                <th>Return</th>
                                <th>Ratio</th>
                            </tr>
                            <tr>
                                <td runat="server" id="txtSpend5"></td>
                                <td runat="server" id="txtReturn5"></td>
                                <td runat="server" id="txtRatio5"></td>
                            </tr>
                        </table>
                    </div>
                    <div class="in-block">
                        <table style="width: 0%!important; border: none; display: inline-block">
                            <tr>
                                <th colspan="3" style="text-align: center;" id="hd4" runat="server"></th>
                            </tr>
                            <tr>
                                <th>Spent</th>
                                <th>Return</th>
                                <th>Ratio</th>
                            </tr>
                            <tr>
                                <td runat="server" id="txtSpend4"></td>
                                <td runat="server" id="txtReturn4"></td>
                                <td runat="server" id="txtRatio4"></td>
                            </tr>
                        </table>
                    </div>
                    <div class="in-block">
                        <table style="width: 0%!important; border: none; display: inline-block">
                            <tr>
                                <th colspan="3" style="text-align: center;" id="hd3" runat="server"></th>
                            </tr>
                            <tr>
                                <th>Spent</th>
                                <th>Return</th>
                                <th>Ratio</th>
                            </tr>
                            <tr>
                                <td runat="server" id="txtSpend3"></td>
                                <td runat="server" id="txtReturn3"></td>
                                <td runat="server" id="txtRatio3"></td>
                            </tr>

                        </table>
                    </div>
                    <div class="in-block">
                        <table style="width: 0%!important; border: none; display: inline-block">
                            <tr>
                                <th colspan="3" style="text-align: center;" id="hd2" runat="server"></th>
                            </tr>
                            <tr>
                                <th>Spent</th>
                                <th>Return</th>
                                <th>Ratio</th>
                            </tr>
                            <tr>
                                <td runat="server" id="txtSpend2"></td>
                                <td runat="server" id="txtReturn2"></td>
                                <td runat="server" id="txtRatio2"></td>
                            </tr>
                        </table>
                    </div>
                    <div class="in-block">
                        <table style="width: 0%!important; border: none; display: inline-block">
                            <tr>
                                <th colspan="3" style="text-align: center;" id="hd1" runat="server"></th>
                            </tr>
                            <tr>
                                <th>Spent</th>
                                <th>Return</th>
                                <th>Ratio</th>
                            </tr>
                            <tr>
                                <td runat="server" id="txtSpend1"></td>
                                <td runat="server" id="txtReturn1"></td>
                                <td runat="server" id="txtRatio1"></td>
                            </tr>
                        </table>
                    </div>

                </div>
                <asp:Label Text="" runat="server" ID="lblChartDataforLastWeek" ClientIDMode="Static" Style="display: none" />

                <div id="chtLastWeekDataChart" class="chartClass"></div>
                <div id="lblMinValue" style="display: none!important" runat="server"></div>
            </div>

            <div id="MarketingDiv" class="hidediv">
                <span class="spanhead">Marketing Section</span>
                <div class="panel">
                    <div class="row" style="padding: 10px!important; background-color: #dadada">
                        <div class="col-md-3">
                            <asp:FileUpload runat="server" ID="fileUpload" />
                        </div>
                        <div class="col-md-3">
                            <label for="FromDate">From Date:</label>
                            <input type="date" id="txtFromDate_MS" name="FromDate" placeholder="From Date" class="form-control" runat="server" />
                        </div>
                        <div class="col-md-3">
                            <label for="ToDate">To Date:</label>
                            <input type="date" id="txtToDate_MS" name="ToDate" placeholder="To Date" class="form-control" runat="server" />
                        </div>
                        <div class="col-md-3">
                            <label for="UploadData" style="display: block">&nbsp;</label>
                            <asp:Button ID="btnUpload" runat="server" CssClass="btn btn-success clsSalebtn" Text="Upload Excel" OnClick="btnUpload_Click" />
                        </div>
                        <a href="SampleExcelFile/UploadSampleFile.xlsx" style="color: lightcoral;" download="download">Click Here to download Sample Excel file</a>
                        <asp:Label Text="" runat="server" ID="lblMsg" style="float: left;color: red;"/>
                    </div>

                    <div class="row" style="padding: 15px!important">
                        <div class="col-md-3"></div>
                        <div class="col-md-3">
                            <label for="FromDate">From Date:</label>
                            <input type="date" id="txtFromDate_MS_Search" name="FromDate" placeholder="From Date" class="form-control" runat="server" />
                        </div>
                        <div class="col-md-3">
                            <label for="ToDate">To Date:</label>
                            <input type="date" id="txtToDate_MS_Search" name="ToDate" placeholder="To Date" class="form-control" runat="server" />
                        </div>
                        <div class="col-md-3">
                            <label for="SearchMarketingData" style="display: block">&nbsp;</label>
                            <asp:Button Text="Search" runat="server" class="btn btn-default clsSalebtn" ClientIDMode="Static"
                                ID="btnMarketingData" OnClick="btnMarketingData_Click" />
                        </div>

                    </div>
                </div>

                <div class="panel>">
                        <asp:Label Text="" runat="server" ID="txtMarketingSection" />
                        <asp:Label Text="" runat="server" ID="lblChartMarketingSection" ClientIDMode="Static" Style="display: none" /><br />
                </div>
                <div class="panel">
                     <div id="chtMarketingSectionDataChart" class="chartClass"></div>
                </div>
            </div>
            

            <div id="SalesDiv" class="hidediv">
                <span class="spanhead">Sales Data</span>
                <div class="panel">
                    <div class="col-md-12" style="margin: 10px 0px 12px -10px;">
                        <div class="col-md-4">
                            <label for="FromDate">From Date:</label>
                            <input type="date" id="txtFromDate" name="FromDate" placeholder="From Date" class="form-control" runat="server" />
                        </div>
                        <div class="col-md-4">
                            <label for="ToDate">To Date:</label>
                            <input type="date" id="txtToDate" name="ToDate" placeholder="To Date" class="form-control" runat="server" />
                        </div>
                        <div class="col-md-4">
                            <label for="SearchSalesData" style="display: block">&nbsp;</label>
                            <asp:Button Text="Search" runat="server" class="btn btn-default clsSalebtn" ClientIDMode="Static" ID="btnSearchSalesData" OnClick="btnSearchSalesData_Click" />
                        </div>
                    </div>

                    <div class="">
                            <asp:Label Text="" runat="server" ID="txtSalesData" />
                    </div>
                </div>
            </div>
        </div>
    </div>



    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js"></script>
    <script type="text/javascript">  

        $("#btnSearchSalesData").click(function () {
            waitingDialog.show('Please Wait...');
            var fDate = $("#ContentPlaceHolder1_txtToDate").val();
            var tDate = $("#ContentPlaceHolder1_txtFromDate").val();

            if (fDate == "" || tDate == "") {
                alert("Please select the From Date and To Date");
                waitingDialog.hide();
                return false;
            }
            else {
                return true;
                waitingDialog.hide();
            }
        });

        $("#btnMarketingData").click(function () {
            waitingDialog.show('Please Wait...');
            var from_Date_MS = $("#ContentPlaceHolder1_txtFromDate_MS_Search").val();
            var to_Date_MS = $("#ContentPlaceHolder1_txtToDate_MS_Search").val();

            if (from_Date_MS == "" || to_Date_MS == "") {
                alert("Please select the From Date and To Date");
                waitingDialog.hide();
                return false;
            }
            else {
                return true;
                waitingDialog.hide();
            }
        });



        $(document).ready(function ()
        {
            $('tr.parent')
                .css("cursor", "pointer")
                .attr("title", "Click to expand/collapse")
                .click(function () {
                    $(this).siblings('.hideme').hide();
                    $(this).siblings('.child-' + this.id).show();
                });
            $(this).siblings('.child-' + this.id).hide();

           
        });
    </script>


    <script>
        var metaClick = "";
        $(document).ready(function () {
            $("input").change(function () {
                metaClick = ($(this).val());
                $(this).focus();
            });
        });

        function oblurUpdate(CampingID, txtValue) {
            var _FromDate_MS = $("#<%= txtFromDate_MS.ClientID%>").val();
            var _ToDate_MS = $("#<%= txtToDate_MS.ClientID%>").val();

            $.ajax({
                type: "POST",
                url: "LeadDashboard.aspx/UpdateMetaClick",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify({ CampaignId: CampingID, MetaClicks: metaClick, FromDateMS: _FromDate_MS, ToDateMS: _ToDate_MS }),
                responseType: "json",
                success: function (data) {
                    if (data.d.length > 0) {
                        for (var i = 0; i < data.d.length; i++) {
                            if (data.d[i].Company_Name == "FLTTROTT") {
                                $("#spentP_" + data.d[i].Company_Name).text(data.d[i].Total_Spend_By_Company);
                            }
                            if (data.d[i].Company_Name == "TRAVELOFLIUK") {
                                $("#spentP_" + data.d[i].Company_Name).text(data.d[i].Total_Spend_By_Company);
                            }
                            $("#spentC_" + CampingID).text(data.d[i].Total_Spend_By_Campaign);
                        }
                    }
                },
            });
        }
    </script>


    <script src="../../Scripts/highcharts/7.1.2/highcharts.js"></script>
    <script>
        var userRole = "";
        $(document).ready(function ()
        {
            HideLabel();
            userRole = $("#hdnUserRole").val();
            if ($("#hdnPageSearch").val() == "" || $("#hdnPageSearch").val() == null || $("#hdnPageSearch").val() == "0")
            {
                ShowHideTabsForUser();
            }
            else if ($("#hdnPageSearch").val() == "1")
            {
                ShowHideTabForSearch_MarketingSection();
            }
            else if ($("#hdnPageSearch").val() == "2")
            {
                ShowHideTabForSearch_SalesSection();
            }


            //Last month Data
            if ($("#chartDataforLastMonth").text() != "") {
                var monthData = $("#chartDataforLastMonth").text();
                var aData = monthData.split('@');
                var arr = []
                var obj = {};
                obj.Spend = aData[0];
                obj.Return = aData[1];
                arr.push(obj);
                var myJsonString = JSON.stringify(arr);
                var jsonArray = JSON.parse(JSON.stringify(arr));

                //Draw Chart For Last Month Data
                DrawChartForLastMonthData(jsonArray);
            }

            ///Draw Chart For Last Five Days
            if ($("#lblChartDataforLastWeek").text() != "") {
                var jsonObject = $("#lblChartDataforLastWeek").text();
                var jsonParseData = JSON.parse(jsonObject);
                DrawChartForLastFiveDaysData(jsonParseData);
            }


            //Draw Chart for Marketing Section
            if ($("#lblChartMarketingSection").text() != "") {
                var marketingData = $("#lblChartMarketingSection").text();
                var jsonParsMarketingData = JSON.parse(marketingData);
                DrawChartForMarketingSectionData(jsonParsMarketingData);
            }
        });

        //Draw Chart For Last Month Data
        function DrawChartForLastMonthData(seriesData) {
            $('#chtLastMonthDataChart').highcharts({
                chart: {
                    plotBackgroundColor: null,
                    plotBorderWidth: null,
                    plotShadow: false,
                    type: 'pie'
                },
                title: {
                    text: 'Month wise graph'
                },
                tooltip: {
                    pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
                },
                colors: ['#f45b5b', ' #33cc33'],
                plotOptions: {
                    pie: {
                        allowPointSelect: true,
                        cursor: 'pointer',
                        showInLegend: true,
                        dataLabels: {
                            enabled: true,
                            format: '<b>{point.name}</b>: {point.percentage:.1f} %',
                            style:
                            {
                                color: (Highcharts.theme && Highcharts.theme.contrastTextColor) || 'black'
                            }
                        }
                    }
                },
                series: [{
                    name: "Value",
                    colorByPoint: true,
                    data: [{
                        name: '<b>Spend</b>',
                        y: parseFloat(seriesData[0].Spend),
                        sliced: true,
                        selected: true,
                        style: {
                            color: '#efefef'
                        }
                    },
                    {
                        name: '<b>Return</b>',
                        y: parseFloat(seriesData[0].Return),
                        style: {
                            color: 'tomato'
                        }
                    }],
                }]
            });
        }

        ///Draw Chart For Last Five Days
        function DrawChartForLastFiveDaysData(jsonParseData) {
            $('#chtLastWeekDataChart').highcharts({
                chart: {
                    type: 'bar'
                },
                title: {
                    text: 'Weekly Data'
                },
                xAxis: {
                    categories: ['Spend', 'Return']
                },
                yAxis: {
                    min: parseFloat($("#ContentPlaceHolder1_lblMinValue").text()),
                    title: {
                        text: 'Avg. weekly Data'
                    }
                },
                legend: {
                    reversed: true
                },
                plotOptions: {
                    /*series: {
                        stacking: 'normal'
                    }*/
                },
                series: [{
                    name: 'Day 5',
                    data: [parseFloat(jsonParseData.Spend1), parseFloat(jsonParseData.Return1)]
                },
                {
                    name: 'Day 4',
                    data: [parseFloat(jsonParseData.Spend2), parseFloat(jsonParseData.Return2)]
                },
                {
                    name: 'Day 3',
                    data: [parseFloat(jsonParseData.Spend3), parseFloat(jsonParseData.Return3)]
                },
                {
                    name: 'Day 2',
                    data: [parseFloat(jsonParseData.Spend4), parseFloat(jsonParseData.Return4)]
                },
                {
                    name: 'Day 1',
                    data: [parseFloat(jsonParseData.Spend5), parseFloat(jsonParseData.Return5)]
                }]
            });
        }

        ///Draw Chart For Marketing Section Data
        function DrawChartForMarketingSectionData(jsonParsMarketingData) {
            var array = jsonParsMarketingData;

            var series = [],
                len = jsonParsMarketingData.length,
                i = 0;

            for (i; i < len; i++) {
                series.push({
                    name: '' + jsonParsMarketingData[i].CampaignName + '',
                    data: [
                        parseFloat(jsonParsMarketingData[i].TrackerClicks),
                        parseFloat(jsonParsMarketingData[i].MetaClicks),
                        parseFloat(jsonParsMarketingData[i].PCC),
                        parseFloat(jsonParsMarketingData[i].Spent),
                        parseFloat(jsonParsMarketingData[i].Lead),
                        parseFloat(jsonParsMarketingData[i].Conversion),
                        ('' + jsonParsMarketingData[i].Bookings + '')
                    ]
                });
            }

            $('#chtMarketingSectionDataChart').highcharts({
                colors: ['#f45b5b', ' #33cc33', '#3366ff', '#ff9999', '#404040', '#336699', '#e6e600', '#666699'],
                title: {
                    text: 'Marketing Data',
                    x: -20 //center
                },
                series: series
            });
        }

        function HideLabel()
        {
            var seconds = 5;
            setTimeout(function () {
                document.getElementById("<%=lblMsg.ClientID %>").style.display = "none";
                }, seconds * 1000);
        };
    </script>



    <script>
        function HideAllDiv1(id) {
            $('#LastMonthDiv').show();
            $('#LastWeekDiv').hide();
            $('#MarketingDiv').hide();
            $('#SalesDiv').hide();
        }
        function HideAllDiv2(id) {
            $('#LastMonthDiv').hide();
            $('#LastWeekDiv').show();
            $('#MarketingDiv').hide();
            $('#SalesDiv').hide();
        }
        function HideAllDiv3(id) {
            $('#LastMonthDiv').hide();
            $('#LastWeekDiv').hide();
            $('#MarketingDiv').show();
            $('#SalesDiv').hide();
        }
        function HideAllDiv4(id) {
            $('#LastMonthDiv').hide();
            $('#LastWeekDiv').hide();
            $('#MarketingDiv').hide();
            $('#SalesDiv').show();
        }


        function ShowHideTabsForUser() {

            if (userRole == "superadmin" || userRole == "team head ft") {
                $('#LastMonth').show();   //all Buttons
                $('#LastWeek').show();    //all Buttons
                $('#Marketing').show();   //all Buttons
                $('#Sales').show();       //all Buttons

                $('#LastMonthDiv').show();    //all Div
                $('#LastWeekDiv').hide();     //all Div
                $('#MarketingDiv').hide();    //all Div
                $('#SalesDiv').hide();        //all Div
            }
            else if (userRole == "fareft") {
                $('#LastMonth').hide();   //all Buttons
                $('#LastWeek').hide();    //all Buttons
                $('#Marketing').show();   //all Buttons
                $('#Sales').show();       //all Buttons

                $('#LastMonthDiv').hide();    //all Div
                $('#LastWeekDiv').hide();     //all Div
                $('#MarketingDiv').show();    //all Div
                $('#SalesDiv').hide();        //all Div
            }
            else if (userRole == "online" || userRole == "operatorft" || userRole == "operator" || userRole == "agentft") {
                $('#LastMonth').hide();   //all Buttons
                $('#LastWeek').hide();    //all Buttons
                $('#Marketing').hide();   //all Buttons
                $('#Sales').show();       //all Buttons

                $('#LastMonthDiv').hide();    //all Div
                $('#LastWeekDiv').hide();     //all Div
                $('#MarketingDiv').hide();    //all Div
                $('#SalesDiv').show();        //all Div
            }
        }

        function ShowHideTabForSearch_MarketingSection()
        {
            

            if (userRole == "fareft" && $("#hdnPageSearch").val() == "1")
            {
                $('#MarketingDiv').show();
                $('#SalesDiv').hide();
            }
            else if (userRole == "fareft" && $("#hdnPageSearch").val() == "2")
            {
                $('#MarketingDiv').hide();
                $('#SalesDiv').show();
            }
            if ((userRole == "superadmin" || userRole == "team head ft") && $("#hdnPageSearch").val() == "1")
            {
                $('#LastMonthDiv').hide();    //all Div
                $('#LastWeekDiv').hide();     //all Div
                $('#MarketingDiv').show();    //all Div
                $('#SalesDiv').hide();        //all Div

                $('#LastMonth').show();   //all Buttons
                $('#LastWeek').show();    //all Buttons
                $('#Marketing').show();   //all Buttons
                $('#Sales').show();       //all Buttons
            }
            if ((userRole == "superadmin" || userRole == "team head ft") && $("#hdnPageSearch").val() == "2")
            {
                $('#LastMonthDiv').hide();    //all Div
                $('#LastWeekDiv').hide();     //all Div
                $('#MarketingDiv').hide();    //all Div
                $('#SalesDiv').show();        //all Div

                $('#LastMonth').show();   //all Buttons
                $('#LastWeek').show();    //all Buttons
                $('#Marketing').show();   //all Buttons
                $('#Sales').show();       //all Buttons
            }
        }

        function ShowHideTabForSearch_SalesSection() {
           
            if (userRole == "fareft" && $("#hdnPageSearch").val() == "1")
            {
                $('#MarketingDiv').show();
                $('#SalesDiv').hide();
                $('#Marketing').show();   //all Buttons
                $('#Sales').show();       //all Buttons
            }
            if (userRole == "fareft" && $("#hdnPageSearch").val() == "2")
            {
                $('#MarketingDiv').hide();
                $('#SalesDiv').show();
                $('#Marketing').show();   //all Buttons
                $('#Sales').show();       //all Buttons
            }
            if ((userRole == "online" || userRole == "operatorft" || userRole == "operator" || userRole == "agentft")
                && $("#hdnPageSearch").val() == "1")
            {
                $('#SalesDiv').show();
                $('#Marketing').hide();   //all Buttons
                $('#Sales').show();       //all Buttons
            }

            if ((userRole == "superadmin" || userRole == "team head ft") && $("#hdnPageSearch").val() == "1")
            {
                $('#LastMonthDiv').hide();    //all Div
                $('#LastWeekDiv').hide();     //all Div
                $('#MarketingDiv').show();    //all Div
                $('#SalesDiv').hide();        //all Div

                $('#LastMonth').show();   //all Buttons
                $('#LastWeek').show();    //all Buttons
                $('#Marketing').show();   //all Buttons
                $('#Sales').show();       //all Buttons
            }
            if ((userRole == "superadmin" || userRole == "team head ft") && $("#hdnPageSearch").val() == "2")
            {
                $('#LastMonthDiv').hide();    //all Div
                $('#LastWeekDiv').hide();     //all Div
                $('#MarketingDiv').hide();    //all Div
                $('#SalesDiv').show();        //all Div

                $('#LastMonth').show();   //all Buttons
                $('#LastWeek').show();    //all Buttons
                $('#Marketing').show();   //all Buttons
                $('#Sales').show();       //all Buttons
            }
        }
    </script>
    <style>
        .btncl {
            width: 170px;
            margin: 0px 0px 15px 0px;
        }

        .chartClass {
            min-width: 97px;
            height: 500px;
            overflow: hidden;
            margin-left: 10px;
        }

        .spanhead {
            font-size: 20px;
            font-weight: bold;
            background: #00275d;
            color: #fff;
            width: 100%;
            display: block;
            padding: 5px 15px;
            margin-bottom: 10px;
            border-radius: 5px;
        }


        .clsSalebtn {
            height: 35px !important;
            padding: 8px 10px 10px 10px !important;
            width: 110px !important;
            font-size: 15px !important;
        }
    </style>
</asp:Content>

