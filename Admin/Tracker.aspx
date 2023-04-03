<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true"
    CodeFile="Tracker.aspx.cs" Inherits="Admin_Tracker" %>


<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
   <%-- <link href="../css/StyleSheet.css" rel="stylesheet" />--%>
    <link href="../../css/StyleSheet.css" rel="stylesheet" />
    <script src="https://www.traveljunction.co.uk/temp/CalendarAnyYear.js"></script>
    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
    <script src="../js/Trackerchart.js"></script>
   
    <style>
        .page-bar {
    padding: 0.5em;
    line-height: 1;
    background-color: #546f8f;
    color: white;
}
.table__heading {
    padding: 0.25em 0.5em 0.25em 0;
    font-weight: 700;
    vertical-align: middle;
    text-align: left;
    text-transform: uppercase;
    background-color: #769bc8;
    white-space: nowrap;
    color: white;
    font-size: 12px;
    font-size: 0.80em;
}
.heading-plot {
    padding-left: 0.8em;
    width: 3em;
    position: relative;
    vertical-align: middle;
    white-space: nowrap;
    cursor: pointer;
    font-size: 10px;
    font-size: 0.875rem;
    -moz-transition: background 0.25s;
    -o-transition: background 0.25s;
    -webkit-transition: background 0.25s;
    transition: background 0.25s;
}
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <input type="hidden" id="setascurrdate" />
    <input type="hidden" id="hdeprdate" />
   <%-- <div class="container">--%>
        <div style="height: 15px;"></div>
        <div class="panel panel-default">
            <div class="panel-heading">Online Tracker Report</div>
            <div class="panel-body" style="line-height: 34px;">
                <div class="row" style="display:none">
                    <div class="col-md-2">
                        <label>Origin</label>
                        <asp:TextBox ID="txtOrigin" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <label>Destination</label>
                        <asp:TextBox ID="txtDestination" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <label>Depart From Date</label>
                        <asp:TextBox ID="txtDepartFrom" CssClass="form-control" runat="server" onclick="showCalender(this);"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <label>Depart To Date</label>
                        <asp:TextBox ID="txtDepartTo" CssClass="form-control" runat="server" onclick="showCalender(this);"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <label>Return From Date</label>
                        <asp:TextBox ID="txtReturnFrom" CssClass="form-control" runat="server" onclick="showCalender(this);"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <label>Return To Date</label>
                        <asp:TextBox ID="txtReturnTo" CssClass="form-control" runat="server" onclick="showCalender(this);"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        <label>Traffic From Date</label>
                        <asp:TextBox ID="txtHitFrom" CssClass="form-control" runat="server" onclick="showCalender(this);"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <label>Traffic To Date</label>
                        <asp:TextBox ID="txtHitTo" CssClass="form-control" runat="server" onclick="showCalender(this);"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <label>Company</label>
                        <asp:DropDownList ID="ddlCompany" CssClass="form-control" runat="server" AutoPostBack="True" onchange="SearchTracker()" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2">
                        <label>Campaign</label>
                        <asp:DropDownList ID="ddlSourceMedia" CssClass="form-control" runat="server">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2">
                        <label>Page</label>
                        <asp:DropDownList ID="ddlPage" CssClass="form-control" runat="server">
                            <asp:ListItem Value="">Any</asp:ListItem>
                            <asp:ListItem Value="Result">Result</asp:ListItem>
                            <asp:ListItem Value="PassengerDetails.aspx">Passenger Details</asp:ListItem>
                            <asp:ListItem Value="SagePayServer">Payment Detail</asp:ListItem>
                            <asp:ListItem Value="Confirmation.aspx">Confirmaion</asp:ListItem>  
                             <asp:ListItem Value="fltcampaign.aspx">Fltcampaign</asp:ListItem>   
                             <asp:ListItem Value="FltDeeplink.aspx">FltDeeplink</asp:ListItem>      
                             <asp:ListItem Value="FlightOffer.aspx">FlightOffer</asp:ListItem>                         
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2">
                        <label>IPAddress</label>
                        <asp:TextBox ID="txtIP" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-12">
                        <asp:Label ID="lblMsg" runat="server" EnableViewState="false" Font-Bold="true" ForeColor="Red"></asp:Label>
                    </div>
                </div>
                 <div class="row">
                <div class="col-md-6">
                </div>
                <div class="col-md-2">
                      <label style="display:block">&nbsp;</label>
                    <asp:CheckBox ID="chkDup" runat="server" Text="Remove Duplicate IP" />
                </div>
                <div class="col-md-2">
                   <label style="display:block">&nbsp;</label>
                    <asp:Button ID="btnSearchTrack" runat="server" style="font-size: 17px;font-weight: 600;"
                        OnClick="btnSearchTrack_Click" CssClass="btn btn-primary btn-lg" OnClientClick="SearchTracker()" Text="Search Tracker Data" />
                </div>
                <div class="col-md-2">
                    <label style="display:block">&nbsp;</label>
                    <asp:Button ID="btnExport" Visible="false" style="font-size: 17px;font-weight: 600;" runat="server" class="btn btn-primary btn-lg" Text="Export Tracker Report" OnClick="btnExport_Click" />
                </div>
            </div>
            </div>
        </div>
    <%--</div>--%>
    <%--<div class="container">--%>
        <div style="height: 15px;"></div>
        <div class="panel panel-default">
         <%--   <div class="page-bar">Page Tracker Details</div>--%>
            <div class="page-bar">Page Tracker Details <asp:Label Text="" ID="lblTrackerClick" runat="server" style="float: right;font-weight: bold;"/></div>
            
            <div class="col-lg-9 col-md-9 panel-body" style="line-height: 34px; padding: 0px!important;">
               
                <asp:Repeater ID="rptrTrack" runat="server" EnableViewState="false" >
                    <HeaderTemplate>
                          <div class="row" style="font-size:10px !important; background: #337ab7;color: #fff;">
    <div class="col-md-1"><p>SrNo.</p></div>
    <div class="col-md-2"><p>IP</p></div>
    <div class="col-md-2"><p>Time</p></div>
    <div class="col-md-1"><p>Site</p></div>
    <div class="col-md-2"><p>Page</p></div>
    <div class="col-md-1"><p>Org</p></div>
    <div class="col-md-1"><p>Dest</p></div>
    <div class="col-md-1"><p>Dep</p></div>
    <div class="col-md-1"><p>Ret</p></div>
  </div>
                       
                    </HeaderTemplate>
                    <ItemTemplate>
    <div class="row" style="font-size:10px !important;background: #fff !important;">
    <div class="col-md-1"><p><%#Container.ItemIndex+1 %></p></div>
    <div class="col-md-2"><p><%# Eval("IPAddress")%></p></div>
    <div class="col-md-2"><p><%# Eval("DatenTime")%></p></div>    
    <div class="col-md-1"><p><%# Eval("ReqSource")%></p></div>
    <div class="col-md-2"><p><%# Eval("Page")%></p></div>
    <div class="col-md-1"><p><%# Eval("Origin")%></p></div>
    <div class="col-md-1"><p><%# Eval("Destination")%></p></div>
    <div class="col-md-1"><p><%# Eval("DepartDate","{0:dd MMM yyyy}")%></p></div>
    <div class="col-md-1"><p><%# Eval("ReturnDate","{0:dd MMM yyyy}")%></p></div>
  </div>
  <div class="row" style="color:red; font-size:12px!important;background: #fff !important;"><%# Eval("Remarks")%></div>
                          
                    </ItemTemplate>
                    <FooterTemplate>
                       
                    </FooterTemplate>
                </asp:Repeater>
                

            </div>
            <div class="col-lg-3 col-md-3 panel-body">
               
                <div id="chart_div_Country"></div>
                  <div id="chart_div_City"></div>

            </div>
        </div>
    <%--</div>--%>
    
    <script type="text/javascript">
     

        $(document).ready(function () {

            $.ajax({
                type: "POST",
                url: "Tracker.aspx/TrafficByCountry",
                contentType: "application/json; charset=utf-8",
                dataType: "json",

                responseType: "json",
                success: function (data) {
                    var jsdata = JSON.parse(data.d);
                    drawChartByTrafficFromCountry(jsdata);
                    TrafficCity();
                },
                error: function (data) { alert("error1") }
            });
        });

        function TrafficCity()
        {

            $.ajax({
                type: "POST",
                url: "Tracker.aspx/TrafficFromCity",
                contentType: "application/json; charset=utf-8",
                dataType: "json",

                responseType: "json",
                success: function (data) {
                    var jsdata = JSON.parse(data.d);
                    drawChartByTrafficFromCity(jsdata);

                },
                error: function (data) { alert("error1") }
            });


        }

        function drawChartByTrafficFromCountry(jsdata) {
            // Create the data table.
            var data1 = new google.visualization.DataTable();
            data1.addColumn('string', 'Country');
            data1.addColumn('number', 'NoOfHits');
            $.each(jsdata, function (key, value) {
                data1.addRows([
                  [value.Country, value.NoOfHits]
                ]);
            });


            // Set chart options
            var options = {
                'title': 'Traffic Report By Country',
                'width': 400,
                'height': 300,
                'is3D': true
            };

            // Instantiate and draw our chart, passing in some options.
            var chart = new google.visualization.PieChart(document.getElementById('chart_div_Country'));
            chart.draw(data1, options);


        }

        function drawChartByTrafficFromCity(jsdata) {
            // Create the data table.
            var data1 = new google.visualization.DataTable();
            data1.addColumn('string', 'City');
            data1.addColumn('number', 'NoOfHits');
            $.each(jsdata, function (key, value) {
                data1.addRows([
                  [value.City, value.NoOfHits]
                ]);
            });


            // Set chart options
            var options = {
                'title': 'Traffic Report By City',
                'width': 400,
                'height': 300,
                'is3D': true
            };

            // Instantiate and draw our chart, passing in some options.
            var chart = new google.visualization.PieChart(document.getElementById('chart_div_City'));
            chart.draw(data1, options);


        }

        function SearchTracker()
        {
            waitingDialog.show('Please Wait...');

        }
    </script>
</asp:Content>
