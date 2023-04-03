<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="MetaSearch.aspx.cs" Inherits="Admin_Reports_MetaSearch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <link href="../../css/StyleSheet.css" rel="stylesheet" />
    <script src="../../js/CalendarAnyYear.js"></script>
     <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
    <script src="../../js/MetaSearch.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container">
        <div style="height: 15px;"></div>
        <div class="panel panel-default">
            <div class="panel-heading">Booking Report</div>

            <div class="panel-body">
                <div class="row">
                   
                    
                    <div class="col-md-4">
                        <span>From Booking Date</span>
                        <asp:TextBox class="form-control" ID="txtFromDate" onclick="showCalender(this);" runat="server" />
                    </div>
                    <div class="col-md-4">
                        <span>To Booking Date</span>
                        <asp:TextBox class="form-control" ID="txtToDate" onclick="showCalender(this);" runat="server" />
                    </div>
                    <div  class="col-md-4"> <input id="btnSearch" type="button" value="Search" class="btn btn-danger btn-lg" onclick="return MetaSearch();" /></div>
                </div>
                
                <br />
                <br />
                 <div class="row"><div id="divTotalRec"></div></div>

                <div class="row">
                    <div class="col-md-10"> <div id="chart_div" style="width: 1000px; height: 500px;" ></div></div>
                     

                </div>
                <div class="row">
                    
                     <div class="col-md-10"  style="overflow:auto"> <div id="chart_div_line_TravelDate"  style="width: 100%; height: 500px;"> loading...</div></div>
                     <div  class="col-md-2"> <asp:Button ID="btExcel" runat="server" class="btn btn-danger btn-lg" Text="Excel" OnClick="btExcel_Click" style="display:none" /></div>
                </div>
                <div class="row">
                    
                     <div class="col-md-10"  style="overflow:auto"> <div id="chart_div_line_Month"  style="width: 100%; height: 500px;"> loading...</div></div>
                    
                </div>

                <div class="row">
                    
                     <div class="col-md-10"> <div id="chart_div_line" style="width: 1000px; height: 500px;"></div></div>

                </div>


                <%-- <div class="row">
                    <div class="col-md-12" style="overflow:auto"> <div id="chart_DepartDate" style="width: 5000px; height: 500px;" ></div></div>
                     

                </div>--%>

                

                <div class="row">
                    
                     <div class="col-md-10"> <div id="chart_div_line_DepartDate" style="width: 1000px; height: 500px;"></div></div>
                   

                </div>
                 
              
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

        function MetaSearch() {
            waitingDialog.show("Please wait...");
            var fromDate1 = $("#ContentPlaceHolder1_txtFromDate").val();
            var toDate1 = $("#ContentPlaceHolder1_txtToDate").val();
           
            GetMetaSearchReport(fromDate1, toDate1);
            waitingDialog.hide();
        }

        function GetMetaSearchReport(fromDate1, toDate1) {
            $.ajax({
                type: "POST",
                url: "MetaSearch.aspx/MetaSearchReportByDestination",
                headers: { 'Accept-Encoding': 'gzip' },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify({ fromDate: fromDate1, toDate: toDate1 }),
                responseType: "json",
                success: function (data) {
                    var jsdata = JSON.parse(data.d);
                    drawChartByDestination(jsdata);
                   // GetMetaSearchReportByDepartDate();
                    GetMetaSearchReportByTravelDate();
                    GetMetaSearchReportByTravelMonth();
                    $("#ContentPlaceHolder1_btExcel").show();
                    $("#divTotalRec").text("Total Records - " + count(jsdata));

                },
                error: function (data) { alert("error1") }
            });
        }

        function count(jsdata) {
            var c = 0;
            $.each(jsdata, function (key, value) {
                c += value.Searches;
            });
            return c;
        }


        function GetMetaSearchReportByDepartDate() {
            $.ajax({
                type: "POST",
                url: "MetaSearch.aspx/MetaSearchReportByDepartDate",
                headers: { 'Accept-Encoding': 'gzip' },
                contentType: "application/json; charset=utf-8",
                dataType: "json",            
                responseType: "json",
                success: function (data) {
                    var jsdata = JSON.parse(data.d);
                    drawChartByDepartDate(jsdata);
                  
                },
                error: function (data) { alert("error1") }
            });
        }
        function GetMetaSearchReportByTravelDate() {
            $.ajax({
                type: "POST",
                url: "MetaSearch.aspx/MetaSearchReportByTravelDate",
                headers: { 'Accept-Encoding': 'gzip' },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                responseType: "json",
                success: function (data) {
                    var jsdata = JSON.parse(data.d);
                    drawChartByTravelDate(jsdata);

                },
                error: function (data) { alert("error1") }
            });
        }
        function GetMetaSearchReportByTravelMonth() {
            $.ajax({
                type: "POST",
                url: "MetaSearch.aspx/MetaSearchReportByTravelMonth",
                headers: { 'Accept-Encoding': 'gzip' },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                responseType: "json",
                success: function (data) {
                    var jsdata = JSON.parse(data.d);
                    drawChartByTravelMonth(jsdata);

                },
                error: function (data) { alert("error1") }
            });
        }


        // Load the Visualization API and the corechart package.
        google.charts.load('current', { 'packages': ['corechart', 'bar'] });
        google.charts.load('current', { 'packages': ['table'] });
        // Set a callback to run when the Google Visualization API is loaded.
        google.charts.setOnLoadCallback(drawChartByDestination);
       // google.charts.setOnLoadCallback(drawChartByDepartDate);
        google.charts.setOnLoadCallback(drawChartByTravelDate);

        google.charts.setOnLoadCallback(drawChartByTravelMonth);

        // Callback that creates and populates a data table,
        // instantiates the pie chart, passes in the data and
        // draws it.
        function drawChartByDestination(jsdata) {
         
            // Create the data table.
            var data = new google.visualization.DataTable();
            //var data = google.visualization.arrayToDataTable([["Element", "Density"]]);
            data.addColumn('string', 'Destination');
            data.addColumn('number', 'Searches');
            $.each(jsdata, function (key, value) {
               
                data.addRows([
                  [value.Destination, value.Searches]
                ]);
            });

           


            // Set chart options
            var options = {
                'title': 'Meta Search Report By Destination',

                'is3D': true
            };

            // Instantiate and draw our chart, passing in some options.
            //var chart = new google.visualization.PieChart(document.getElementById('chart_div'));
            var chart = new google.visualization.ColumnChart(document.getElementById('chart_div'));
            var chartline = new google.visualization.LineChart(document.getElementById('chart_div_line'));
            chart.draw(data, options);
            chartline.draw(data, options);
        }
        function drawChartByDepartDate(jsdata) {

            // Create the data table.
            var data = new google.visualization.DataTable();
            //var data = google.visualization.arrayToDataTable([["Element", "Density"]]);
            data.addColumn('string', 'Destination');
            data.addColumn('number', 'Searches');
            data.addColumn('date', 'DepartDate');
            $.each(jsdata, function (key, value) {

                data.addRows([
                  [value.Destination, value.Searches, new Date(value.DepartDate)]
                ]);
            });




            // Set chart options
            var options = {
                'title': 'Meta Search Report By Depart Date',

                'is3D': true
            };

            // Instantiate and draw our chart, passing in some options.           

            var chart = new google.charts.Bar(document.getElementById('chart_DepartDate'));
            var chartline = new google.visualization.LineChart(document.getElementById('chart_div_line_DepartDate'));
            chart.draw(data, options);
            chartline.draw(data, options);
        }


        function drawChartByTravelDate(jsdata) {
            
            // Create the data table.
            var data = new google.visualization.DataTable();
            //var data = google.visualization.arrayToDataTable([["Element", "Density"]]);
            data.addColumn('string', 'Origin');
            data.addColumn('string', 'Destination');
            data.addColumn('number', 'Searches');
            data.addColumn('date', 'DepartDate');
            data.addColumn('date', 'ReturnDate');
            data.addColumn('string', 'Momondo');
            data.addColumn('string', 'JetCost');
            $.each(jsdata, function (key, value) {
                var momondo = "<a target='_blank' href='http://www.momondo.co.uk/flightsearch/?Search=true&TripType=2&SegNo=2&SO0=" + value.Origin + "&SD0=" + value.Destination + "&SDP0=" + value.DepartDate + "&SO1=" + value.Destination + "&SD1=" + value.Origin + "&SDP1=" + value.ReturnDate + "&AD=1&TK=ECO&DO=false&NA=false'" + " >Momondo</a>";
                var jetCost = "<a target='_blank' href='http://www.jetcost.co.uk/wait.php?allerRetour=true&_from=" + value.Origin + "&trip_from=" + value.Origin + "&To=" + value.Destination + "&tripTo=" + value.Destination + "&date1=" + formatDate(value.DepartDate) + "&date2=" + formatDate(value.ReturnDate) + "&NbAdults=1&NbEnfants=0&NBBaby=0&class=0'" + " >Jetcost</a>";
                
                data.addRows([
                  [value.Origin, value.Destination, value.Searches, new Date(value.DepartDate), new Date(value.ReturnDate), momondo, jetCost]
                ]);
            });
           

            var table = new google.visualization.Table(document.getElementById('chart_div_line_TravelDate'));

            table.draw(data, { showRowNumber: true, allowHtml: true, width: '100%', height: '100%' });

            
        }
        function drawChartByTravelMonth(jsdata) {
         
            // Create the data table.
            var data = new google.visualization.DataTable();
            
             data.addColumn('string', 'Month');
            data.addColumn('number', 'Searches');
          
            
            $.each(jsdata, function (key, value) {               
                data.addRows([
                  [value.Month,value.Searches]
                ]);
            });

            // Set chart options
            var options = {
                'title': 'Meta Search Report By Depart Travel Month',
                'is3D': true
            };
            var chartlineMonth = new google.visualization.ColumnChart(document.getElementById('chart_div_line_Month'));
            chartlineMonth.draw(data, options);          


        }

        function formatDate(value) {

            var d = new Date(value);
           
            return d.getDate() + "/" + (d.getMonth() + 1) + "/" + d.getFullYear();
        }

    </script>
</asp:Content>

