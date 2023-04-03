<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="BookingReports.aspx.cs" Inherits="Admin_Reports_BookingReports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <link href="../../css/StyleSheet.css" rel="stylesheet" />
    <script src="../../js/CalendarAnyYear.js"></script>
     <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
    <script src="../../js/chart.js"></script>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container">
        <div style="height: 15px;"></div>
        <div class="panel panel-default">
            <div class="panel-heading">Booking Report</div>

            <div class="panel-body">
                <div class="row">
                   <div class="col-md-2">
                            <span>Company</span>
                            <asp:DropDownList ID="ddlCompany" class="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-2" style="display:block">
                            <span>Source Media </span>
                            <asp:DropDownList class="form-control" ID="ddlSourceMedia" runat="server">
                            </asp:DropDownList>
                        </div>

                    <div class="col-md-2">
                        <span>Booking Status</span>
                        <asp:DropDownList ID="ddlStatus" runat="server" class="form-control">
                            <asp:ListItem Value="">Select Status</asp:ListItem>
                             <asp:ListItem Value="Incomplete">Incomplete</asp:ListItem>                               
                                <asp:ListItem Value="Option">Option</asp:ListItem>
                                <asp:ListItem Value="Booked">Booked</asp:ListItem>
                                <asp:ListItem Value="Decline">Decline</asp:ListItem>
                                 <asp:ListItem Value="Documents">Documents</asp:ListItem>
                                 <asp:ListItem Value="Payments">Payments</asp:ListItem>
                                <asp:ListItem Value="Queue">Queue</asp:ListItem>
                                 <asp:ListItem Value="Issued">Issued</asp:ListItem>
                                 <asp:ListItem Value="ReIssued">ReIssued</asp:ListItem>
                                 <asp:ListItem Value="Cancelled">Cancelled</asp:ListItem>
                                <asp:ListItem Value="Refund">Refund</asp:ListItem>
                                <asp:ListItem Value="Completed">Completed</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    
                    <div class="col-md-3">
                        <span>From Booking Date</span>
                        <asp:TextBox class="form-control" ID="txtFromDate" onclick="showCalender(this);" runat="server" />
                    </div>
                    <div class="col-md-3">
                        <span>To Booking Date</span>
                        <asp:TextBox class="form-control" ID="txtToDate" onclick="showCalender(this);" runat="server" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                         <span></span>
                        </div>
                    <div class="col-md-6">
                            <span></span>
                            <br />
                        <input id="btnSearch" type="button" value="Search" class="btn btn-danger btn-lg" onclick="return SearchBooking();" />
                           
                        </div>
                </div>                

               <hr />

                 <div class="row"><div id="divTotalRec"></div></div>

                <div class="row">
                    <div class="col-md-5"> <div id="chart_div"  style="width: 400px; height: 300px;"></div></div>
                     <div class="col-md-5"> <div id="chart_div_company" style="width: 400px; height: 300px;"></div></div>
                    <div class="col-md-2">   <asp:Button ID="btExcel" runat="server" class="btn btn-danger btn-lg" Text="Excel" OnClick="btExcel_Click" style="display:none" /></div>
                </div>
                 <div class="row">
                    <div class="col-md-6"> <div id="chart_div_source" style="width: 400px; height: auto;"></div></div>
                    <div class="col-md-6"> <div id="chart_div_query" style="width: 400px; height: auto;"></div></div>

                </div>

                 <div class="row">
                    <div class="col-md-6"> <div id="chart_div_campaign" style="width: 400px; height: auto;"></div></div>
                    <div class="col-md-6"> <div id="chart_div_destination" style="width: 400px; height: auto;"></div></div>

                </div>
                <div class="row">
                    <div class="col-md-6"> <div id="chart_div_weekDay" style="width: 600px; height: auto;"></div></div>
                    <div class="col-md-6"> <div id="chart_div_Time" style="width: 400px; height: auto;"></div></div>

                </div>
              
              
                <input id="setascurrdate" type="hidden" />
                <input id="hdeprdate" type="hidden" />
                <div style="height: 657px; width: 1366px; display: none;" id="fadebackground">
                </div>
                <div id="popProgressBar" style="height: 30%; width: 30%; top: 230px; left: 478px; display: none;" class="popup-product" align="center">
                    <table align="center" bgcolor="#ffffff" height="100%" width="100%">
                        <tbody>
                            <tr>
                                <td class="popup-header">Please wait while we process your request...
                                </td>
                            </tr>
                            <tr>
                                <td align="center" valign="middle">
                                    <img src="../../Images/Wait.gif" id="ImageProgressbar">
                                </td>
                            </tr>
                            <tr>
                                <td style="background-color: #ffffff; color: #B9B9B9; vertical-align: middle; text-align: center; font-size: 18px; font-family: Verdana;" align="center" height="40"></td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>


    </div>

   
    <script type="text/javascript">

        function popup(divProgressBar, width, height) {
            try {
                var height1 = $(window).height();
                var width1 = $(window).width();
                $('#' + divProgressBar).height(height + "%");
                $('#' + divProgressBar).width(width + "%");
                $('#' + divProgressBar).css({ top: ((height1 - ((height1 * parseInt(height)) / 100)) / 2).toFixed(0) + "px", left: ((width1 - ((width1 * parseInt(width)) / 100)) / 2).toFixed(0) + "px" });

                $('#fadebackground').height(height1 + "px");
                $('#fadebackground').width(width1 + "px");
                $('#fadebackground').toggle();
                $('#' + divProgressBar).toggle();
                return false;
            }
            catch (e) { return false; }
        }

        function SearchBooking() {

            var fromDate1 = $("#ContentPlaceHolder1_txtFromDate").val();
            var toDate1 = $("#ContentPlaceHolder1_txtToDate").val();
            var status1 = $("#ContentPlaceHolder1_ddlStatus").val();

            var company1 = $("#ContentPlaceHolder1_ddlCompany").val();
            if (company1 == "") {
                var count = 0
                $('#ContentPlaceHolder1_ddlCompany option').each(function () {
                    if (count == 0)
                        company1 += $(this).attr('value');
                    else
                        company1 += $(this).attr('value') + ',';

                    count++;
                });


            }

            GetBookingReport(fromDate1, toDate1, status1, company1);

        }

        function GetBookingReport(fromDate1, toDate1, status1, company1) {
            $.ajax({
                type: "POST",
                url: "BookingReports.aspx/BookingReportByStatus",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify({ fromDate: fromDate1, toDate: toDate1, status: status1, company: company1 }),
                responseType: "json",
                success: function (data) {
                    var jsdata = JSON.parse(data.d);
                    drawChartByStatus(jsdata);
                    GetBookingReportByCompany();
                    GetBookingReportByCompanyCampaign();
                    GetBookingReportByDestination();

                    GetBookingReportByWeekDays();
                    GetBookingReportByTime();

                    $("#ContentPlaceHolder1_btExcel").show();
                    $("#divTotalRec").text("Total Records - " + count(jsdata));

                },
                error: function (data) { alert("error1") }
            });
        }

        function GetBookingReportByDestination() {
          
            $.ajax({
                type: "POST",
                url: "BookingReports.aspx/BookingReportByDestination",
                contentType: "application/json; charset=utf-8",
                dataType: "json",

                responseType: "json",
                success: function (data) {
                  
                    var jsdata = JSON.parse(data.d);
                    drawChartByDestination(jsdata);

                },
                error: function (data) { alert("error destination") }
            });
        }

        function GetBookingReportByWeekDays() {

            $.ajax({
                type: "POST",
                url: "BookingReports.aspx/BookingReportByWeekDays",
                contentType: "application/json; charset=utf-8",
                dataType: "json",

                responseType: "json",
                success: function (data) {

                    var jsdata = JSON.parse(data.d);
                    drawChartByWeekDays(jsdata);

                },
                error: function (data) { alert("error destination") }
            });
        }

        function GetBookingReportByTime() {

            $.ajax({
                type: "POST",
                url: "BookingReports.aspx/BookingReportByTime",
                contentType: "application/json; charset=utf-8",
                dataType: "json",

                responseType: "json",
                success: function (data) {

                    var jsdata = JSON.parse(data.d);
                    drawChartByTime(jsdata);

                },
                error: function (data) { alert("error destination") }
            });
        }


        function count(jsdata) {
            var c = 0;
            $.each(jsdata, function (key, value) {
                c += value.NoOfBookings;
            });
            return c;
        }



        function drawChartByDestination(jsdata) {
            // Create the data table.
            var data1 = new google.visualization.DataTable();
            data1.addColumn('string', 'Destination');
            data1.addColumn('number', 'Bookings');
            $.each(jsdata, function (key, value) {
                data1.addRows([
                  [value.Destination, value.NoOfBookings]
                ]);
            });


            // Set chart options
            var options = {
                'title': 'Booking Report By Destination',
                'width': 400,
                'height': 300,
                'is3D': true
            };

            // Instantiate and draw our chart, passing in some options.
            var chart = new google.visualization.PieChart(document.getElementById('chart_div_destination'));
            chart.draw(data1, options);


        }

        function drawChartByWeekDays(jsdata) {
            // Create the data table.
            var data1 = new google.visualization.DataTable();
            data1.addColumn('string', 'WeekDays');
            data1.addColumn('number', 'Bookings');
            $.each(jsdata, function (key, value) {
                data1.addRows([
                  [value.WeekDays, value.NoOfBookings]
                ]);
            });


            // Set chart options
            var options = {
                'title': 'Booking Report By Days of Week',
                'width': 600,
                'height': 300,
                'is3D': true
            };

            // Instantiate and draw our chart, passing in some options.
            var chart = new google.visualization.LineChart(document.getElementById('chart_div_weekDay'));
            chart.draw(data1, options);


        }


        function drawChartByTime(jsdata) {
            // Create the data table.
            var data1 = new google.visualization.DataTable();
            data1.addColumn('string', 'Time');
            data1.addColumn('number', 'Bookings');
            $.each(jsdata, function (key, value) {
                data1.addRows([
                  [value.Time, value.NoOfBookings]
                ]);
            });


            // Set chart options
            var options = {
                'title': 'Booking Report By Time',
                'width': 400,
                'height': 300,
             
            };

            // Instantiate and draw our chart, passing in some options.
            var chart = new google.visualization.LineChart(document.getElementById('chart_div_Time'));
            chart.draw(data1, options);


        }

    </script>

    
</asp:Content>

