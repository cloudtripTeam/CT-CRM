<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="EnquiryReports.aspx.cs" Inherits="Admin_Reports_EnquiryReports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
      <link href="../../css/StyleSheet.css" rel="stylesheet" />
    <script src="../../js/CalendarAnyYear.js"></script>
     <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
    <script src="../../js/enquiry.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container">
        <div style="height: 15px;"></div>
        <div class="panel panel-default">
            <div class="panel-heading">Calls Report</div>

            <div class="panel-body">
                <div class="row">
                   <div class="col-md-4">
                            <span>Company</span>
                            <asp:DropDownList ID="ddlCompany" class="form-control" runat="server" >
                            </asp:DropDownList>
                        </div>
                    
                    <div class="col-md-4">
                        <span>From  Calls Date</span>
                        <asp:TextBox class="form-control" ID="txtFromDate" onclick="showCalender(this);" runat="server" />
                    </div>
                    <div class="col-md-4">
                        <span>To  Calls Date</span>
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
                        <input id="btnSearch" type="button" value="Search" class="btn btn-danger btn-lg" onclick="return generateReports();" />
                           
                        </div>
                </div>
                

                <div class="row"><div id="divTotalRec"></div></div>
                <div class="row">
                    <div class="col-md-5"> <div id="chart_div"  style="width: 400px; height: 300px;"></div></div>
                     <div class="col-md-5"> <div id="chart_div_company" style="width: 400px; height: 300px;"></div></div>
                    <div class="col-md-2">   <asp:Button ID="btExcel" runat="server" class="btn btn-danger btn-lg" Text="Excel" OnClick="btExcel_Click" style="display:none" /></div>
                </div>
                 <div class="row">
                    <div class="col-md-6"> <div id="chart_div_query" style="width: 400px; height: 300px;"></div></div>
                    <div class="col-md-6"> <div id="chart_div_source" style="width: 400px; height: 300px;"></div></div>

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
    <script  type="text/javascript" src="<%= ResolveUrl("~/javascripts/bootstrap.js") %>"></script>
   
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

        function generateReports() {

            var fromDate1 = $("#ContentPlaceHolder1_txtFromDate").val();
            var toDate1 = $("#ContentPlaceHolder1_txtToDate").val();          

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

            GetReport(fromDate1, toDate1, company1);

        }

        function GetReport(fromDate1, toDate1, company1) {
            $.ajax({
                type: "POST",
                url: "EnquiryReports.aspx/getCalls",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify({ fromDate: fromDate1, toDate: toDate1, company: company1 }),
                responseType: "json",
                success: function (data) {
                    var jsdata = JSON.parse(data.d);
                    drawChartByStatus(jsdata);
                    GetCallsReportByCompany();                   
                    GetCallsReportBySource();
                    GetCallsReportByQuery();
                    $("#ContentPlaceHolder1_btExcel").show();
                    $("#divTotalRec").text("Total Records - " + count(jsdata));
                },
                error: function (data) { alert("error1") }
            });
        }

        function count(jsdata) {
            var c = 0;
            $.each(jsdata, function (key, value) {
                c += value.NoOfCalls;
            });
            return c;
        }


        function GetCallsReportByQuery() {
        
            $.ajax({
                type: "POST",
                url: "EnquiryReports/getCallsByQuery",
                contentType: "application/json; charset=utf-8",
                dataType: "json",

                responseType: "json",
                success: function (data) {
                    var jsdata = JSON.parse(data.d);
                    drawChartByQuery(jsdata);

                },
                error: function (data) { alert("error destination") }
            });
        }
       
        function drawChartByQuery(jsdata) {
            // Create the data table.
           
            var data1 = new google.visualization.DataTable();
            data1.addColumn('string', 'Query');
            data1.addColumn('number', 'NoOfCalls');
            $.each(jsdata, function (key, value) {
                data1.addRows([
                  [value.Query, value.NoOfCalls]
                ]);
            });


            // Set chart options
            var options = {
                'title': 'Enquiry Report By Query Type',
                'width': 400,
                'height': 300,
                'is3D': true
            };

            // Instantiate and draw our chart, passing in some options.
            var chart = new google.visualization.PieChart(document.getElementById('chart_div_query'));
            chart.draw(data1, options);


        }

    </script>

    
</asp:Content>
