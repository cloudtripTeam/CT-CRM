<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="TrackerReport.aspx.cs" Inherits="Admin_Reports_TrackerReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../../css/StyleSheet.css" rel="stylesheet" />
    <script src="../../js/CalendarAnyYear.js"></script>
    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
    <script src="../../js/Trackerchart.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="container">
        <div style="height: 15px;"></div>
        <div class="panel panel-default">
            <div class="panel-heading">Online Tracker Report</div>
            <div class="panel-body" style="line-height: 34px;">
                <div class="row">
                    <div class="col-md-2">
                        <label>Traffic Date From</label>
                        <asp:TextBox ID="txtHitFrom" CssClass="form-control" runat="server" onclick="showCalender(this);"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <label>Traffic Date To</label>
                        <asp:TextBox ID="txtHitTo" CssClass="form-control" runat="server" onclick="showCalender(this);"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <label>Company</label>
                        <asp:DropDownList ID="ddlCompany" CssClass="form-control" runat="server" AutoPostBack="True" >
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-6">
                        <label>Campain</label>
                        <asp:CheckBoxList ID="ddlSourceMedia" RepeatColumns="3" runat="server">
                        </asp:CheckBoxList>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-10">
                        <asp:Label ID="lblMsg" runat="server" EnableViewState="false" Font-Bold="true" ForeColor="Red"></asp:Label>
                    </div>


                    <div class="col-md-2">
                         <input id="btnSearchTrack" type="button" value="Search" class="btn btn-danger btn-lg" onclick="return SearchTrack();" />                       

                    </div>
                </div>
                <br />
                <br />
                 <div class="row"><div id="divTotalRec"></div></div>

                <div class="row">
                    <div class="col-md-5">
                        <div id="chart_div_company"></div>
                    </div>
                    <div class="col-md-5">
                        <div id="chart_div_campaign" style="width: 400px; height: 300px;"></div>
                    </div>
                     <div class="col-md-2">   <asp:Button ID="btExcel" runat="server" class="btn btn-danger btn-lg" Text="Excel" OnClick="btExcel_Click" style="display:none" /></div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <div id="chart_div_page" style="width: 400px; height: 300px;"></div>
                    </div>
                    <div class="col-md-6">
                        <div id="chart_div_destination" style="width: 400px; height: 300px;"></div>
                    </div>

                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">

        function SearchTrack() {

            var fromDate = $("#ContentPlaceHolder1_txtHitFrom").val();
            var toDate = $("#ContentPlaceHolder1_txtHitTo").val();        

            var company = $("#ContentPlaceHolder1_ddlCompany").val();
            
            if (company == "") {
                var count = 0
                $('#ContentPlaceHolder1_ddlCompany option').each(function () {
                    if (count == 0)
                        company += $(this).attr('value');
                    else
                        company += $(this).attr('value') + ',';

                    count++;
                });


            }

            GetTrafficByCompany(fromDate, toDate, company);

        }

        function GetTrafficByCompany(fromDate, toDate, company) {
            $.ajax({
                type: "POST",
                url: "TrackerReport.aspx/TrafficByCompany",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify({ fromDate: fromDate, toDate: toDate, company: company }),
                responseType: "json",
                success: function (data) {
                    var jsdata = JSON.parse(data.d);
                    drawChartByTrafficByCompany(jsdata);

                    GetTrafficByCampaign();
                    GetTrafficOnPage();
                    GetTrafficByDestination();
                    $("#ContentPlaceHolder1_btExcel").show();
                    $("#divTotalRec").text("Total Records - " + count(jsdata));

                },
                error: function (data) { alert("error1") }
            });
        }
        function count(jsdata) {
            var c = 0;
            $.each(jsdata, function (key, value) {
                c += value.NoOfHits;
            });
            return c;
        }


        function GetTrafficByCampaign() {
            $.ajax({
                type: "POST",
                url: "TrackerReport.aspx/TrafficByCampaign",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
               
                responseType: "json",
                success: function (data) {
                    var jsdata = JSON.parse(data.d);
                    drawChartTrafficByCampaign(jsdata);

                },
                error: function (data) { alert("error1") }
            });
        }

        function GetTrafficOnPage() {
            $.ajax({
                type: "POST",
                url: "TrackerReport.aspx/TrafficOnPage",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
               
                responseType: "json",
                success: function (data) {
                    var jsdata = JSON.parse(data.d);
                    drawChartTafficOnPage(jsdata);

                },
                error: function (data) { alert("error1") }
            });
        }

        function GetTrafficByDestination() {
            $.ajax({
                type: "POST",
                url: "TrackerReport.aspx/TrafficByDestination",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
              
                responseType: "json",
                success: function (data) {
                    var jsdata = JSON.parse(data.d);
                    drawChartByTrafficForDestination(jsdata);

                },
                error: function (data) { alert("error1") }
            });
        }




       



    </script>

</asp:Content>

