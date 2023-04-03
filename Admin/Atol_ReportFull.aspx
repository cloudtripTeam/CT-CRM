<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="Atol_ReportFull.aspx.cs" Inherits="Admin_Atol_ReportFull" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
 <%--   <link href="../css/StyleSheet.css" rel="stylesheet" />--%>
    <link href="../../css/StyleSheet.css" rel="stylesheet" />
    <script src="https://www.traveljunction.co.uk/temp/CalendarAnyYear.js"></script>
<%--    <link href="//cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/css/bootstrap-multiselect.css" rel="stylesheet" type="text/css" />
    <script src="//cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/js/bootstrap-multiselect.js" type="text/javascript"></script>--%>
    <script type="text/javascript">
        $(function () {
            $('[id*=ddlBookingStatus]').multiselect({
                includeSelectAllOption: true
            });
        });

        $(function () {
            $('[id*=ddlCompany]').multiselect({
                includeSelectAllOption: true
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Search Panel -->
    <div class="panel-group">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="pull-left">
                    <h4 class="panel-title">
                        <a data-toggle="collapse" href="#bookingdetails">Atol Report</a>
                    </h4>
                </div>
                <div class="pull-right"><a data-toggle="collapse" href="#bookingdetails"><span class="glyphicon glyphicon-search" style="color: #fff;"></span></a></div>
                <div class="clearfix"></div>
                <div class="row">
                    <div class="col-md-2">
                        <asp:ListBox ID="ddlCompany" class="form-control mb-10" SelectionMode="Multiple" runat="server"></asp:ListBox>
                    </div>
                    <div class="col-md-2">
                        <asp:ListBox ID="ddlBookingStatus" runat="server" SelectionMode="Multiple" class="form-control mb-10">
                                <asp:ListItem Value="">Select Booking Status</asp:ListItem>
                                <asp:ListItem Value="Booked">Booked</asp:ListItem>                               
                                <asp:ListItem Value="Confirm">Confirm</asp:ListItem>
                                <asp:ListItem Value="Decline">Decline</asp:ListItem>
                                 <asp:ListItem Value="Documents">Documents</asp:ListItem>
                                <asp:ListItem Value="Payments">Payments</asp:ListItem>
                                 <asp:ListItem Value="Issued">Issued</asp:ListItem>                                
                                <asp:ListItem Value="Queue">Queue</asp:ListItem>                               
                                <asp:ListItem Value="Refund">Refund</asp:ListItem>
                                 <asp:ListItem Value="Deposit Forfeited">Deposit Forfeited</asp:ListItem>
                                  <asp:ListItem Value="ETicket Sent">ETicket Sent</asp:ListItem>
                                <asp:ListItem Value="Completed">Completed</asp:ListItem>
                            </asp:ListBox>

                    </div>

                    <div class="col-md-2">

                        <asp:TextBox class="form-control mb-10" ID="txtFromDate" onclick="showCalender(this);" runat="server" placeholder="From Booking Date" />
                    </div>
                    <div class="col-md-2">

                        <asp:TextBox class="form-control mb-10" ID="txtToDate" onclick="showCalender(this);" runat="server" placeholder="To Booking Date" />
                    </div>
                     <div class="col-md-2">
                         <asp:DropDownList ID="ddlSupplier" runat="server" CssClass="form-control"></asp:DropDownList>
                     </div>
                     <div class="col-md-2">
                         <asp:DropDownList ID="ddlAtolType" runat="server" CssClass="form-control">
                              <asp:ListItem Value="" Selected="True">Select Atol Type</asp:ListItem>
                                <asp:ListItem Value="Airline Ticket Agent">Airline Ticket Agent</asp:ListItem>
                                <asp:ListItem Value="Flight Plus/SCHEDULE BONDED">Flight Plus/SCHEDULE BONDED</asp:ListItem>
                                <asp:ListItem Value="Flights Only/PUBLIC BONDED">Flights Only/PUBLIC BONDED</asp:ListItem>
                                <asp:ListItem Value="AGENCY FACILITIES">AGENCY FACILITIES</asp:ListItem>
                                <asp:ListItem Value="Package Only/FULLY BONDED">Package Only/FULLY BONDED</asp:ListItem>
                                <asp:ListItem Value="ATOL TO ATOL">ATOL TO ATOL</asp:ListItem>
                                <asp:ListItem Value="RETAIL AGENCY">RETAIL AGENCY</asp:ListItem>
                                <asp:ListItem Value="NON ATOL">NON ATOL</asp:ListItem>
                         </asp:DropDownList>
                         </div>
                    </div>
                <div  class="row">

                    <div class="col-md-2">

                        <asp:TextBox class="form-control mb-10" ID="txtAirline" MaxLength="2"  runat="server" placeholder="Airline Code" />
                    </div>
                    

                    <div class="col-md-2">

                        <asp:TextBox class="form-control mb-10" ID="txtDepartFrom" onclick="showCalender(this);" runat="server" placeholder="From Depart Date" />
                    </div>
                    <div class="col-md-2">

                        <asp:TextBox class="form-control mb-10" ID="txtDepartTo" onclick="showCalender(this);" runat="server" placeholder="To Depart Date" />
                    </div>


                    <div class="col-md-2">
                        <asp:TextBox class="form-control mb-10" ID="txtIssuedDateFrom" onclick="showCalender(this);" runat="server" placeholder="From Issued Date" />
                       
                    </div>
                     <div class="col-md-2">
                        
                       <asp:TextBox class="form-control mb-10" ID="txtIssuedDateTo" onclick="showCalender(this);" runat="server" placeholder="To Issued Date" />
                    </div>
                    <div class="col-md-2">
                        <asp:Button ID="btnFind" Text="Search" CssClass="btn btn-default" runat="server" OnClick="btnFind_Click" />
                        <input id="setascurrdate" type="hidden" />
                        <input id="hdeprdate" type="hidden" />
                         <a style="color:white" href="atol_report_smt.aspx">Old System Data</a>
                        </div>
                </div>
            </div>
        </div>
    </div>

    <!-- View Reports -->
    <div class="clearfix"></div>
    <div class="panel" style="padding-top: 0px; margin-top: 20px;">
        <div class="panel-body " style="border: 1px solid #ddd; padding: 0px!important;">
            <asp:Label ID="lblmessage" runat="server" Text="" ForeColor="Red"></asp:Label>
            <asp:Button ID="btnExport" runat="server" Text="Export"
                                OnClick="btnExport_Click" Visible="false" />

            <asp:Repeater ID="rptAtolReport" runat="server">
                <HeaderTemplate>
                    <table class="table table-striped  table-hover table-bordered">
                        <thead>
                            <tr>
                                <th class="gdvh">S#</th>
                                <th class="gdvh">Invoice No.</th>
                                <th class="gdvh" class="gdvh">Invoice Date</th>
                                <th>Departure</th>
                                <th class="gdvh">Destination</th>
                                <th class="gdvh">Passengers</th>
                                <th class="gdvh">Lead Passenger</th>
                                <th class="gdvh">PNR</th>
                                <th class="gdvh">Depart Date</th>
                                <th class="gdvh">Airline</th>
                                <th class="gdvh">Currency</th>
                                <th class="gdvh">Net Fare</th>
                                <th class="gdvh">Sales</th>
                                <th class="gdvh">Supplier</th>
                                <th class="gdvh">Atol Type</th>
                                <th class="gdvh">Company</th>
                                <th class="gdvh">Status</th>

                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td class='gdvr'><span><%# Container.ItemIndex+1%></span></td>
                        <td class='gdvr'>
                            <asp:Literal ID="Label1" runat="server" Text='<%#Bind("BookingRef") %>'></asp:Literal>
                        </td>
                        <td class='gdvr'>
                            <asp:Literal ID="Label2" runat="server" Text='<%# Convert.ToDateTime(Eval("Booking_Date_Time")).ToString("dd MMM yyyy") %>'></asp:Literal></td>
                        <td class='gdvr'>
                            <asp:Literal ID="Label10" runat="server" Text='<%# Eval("Origin") %>'></asp:Literal></td>

                        <td class='gdvr'>
                            <asp:Literal ID="Label11" runat="server" Text='<%# Eval("Destination") %>'></asp:Literal></td>
                        <td class='gdvr'>
                            <asp:Literal ID="Label13" runat="server" Text='<%# Eval("NoOfPax") %>'></asp:Literal></td>

                        <td class='gdvr'>
                            <asp:Literal ID="Label3" runat="server" Text='<%# String.Format("{0} {1} {2}", Eval("First_Name"), Eval("Middle_Name") , Eval("Last_Name")) %>'></asp:Literal>
                        </td>
                        <td class='gdvr'>
                            <asp:Literal ID="Label4" runat="server" Text='<%#Bind("PNR") %>'></asp:Literal></td>
                         <td class='gdvr'>
                            <asp:Literal ID="Literal1" runat="server" Text='<%# Convert.ToDateTime(Eval("DepartDate")).ToString("dd MMM yyyy") %>'></asp:Literal></td>

                        <td class='gdvr'>
                            <asp:Literal ID="Label14" runat="server" Text='<%#Bind("Validating_Carrier") %>'></asp:Literal></td>
                        <td class='gdvr'>
                            <asp:Literal ID="Label15" runat="server" Text='<%#Bind("Currency_Type") %>'></asp:Literal></td>
                        <td class='gdvr'>
                            <asp:Literal ID="Label6" runat="server" Text='<%#Bind("Cost_Price") %>'></asp:Literal></td>
                        <td class='gdvr'>
                            <asp:Literal ID="Label7" runat="server" Text='<%#Bind("Sell_Price") %>'></asp:Literal></td>
                        <td class='gdvr'>
                            <asp:Literal ID="Label9" runat="server" Text='<%#Bind("Supplier_Name") %>'></asp:Literal>
                        </td>
                        <td class='gdvr'>
                            <asp:Literal ID="Label8" runat="server" Text='<%#Bind("ATOL_Type") %>'></asp:Literal>
                        </td>

                        <td class='gdvr'>
                            <asp:Literal ID="Label12" runat="server" Text='<%# Eval("Company") %>'></asp:Literal>
                        </td>
                        
                        <td class='gdvr'>
                            <asp:Literal ID="Literal2" runat="server" Text='<%# Eval("BookingStatus") %>'></asp:Literal>
                        </td>

                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                         <td><h6>Total Pax :</h6></td>
                        <td><h5>
                                <asp:Literal ID="ltrTotPax" runat="server"></asp:Literal></h5></td>
                        <td></td>
                        <td></td>
                         <td></td>
                         <td></td>
                        <td class='gdvr'>
                            <h6>Total Net :</h6>
                        </td>
                        <td class='gdvr'>
                            <h5><asp:Literal ID="ltrTotalNetProfit" runat="server"></asp:Literal>
                                </h5>
                        </td>

                        <td class='gdvr'>
                            <h6>Total Sale :
                            <asp:Literal ID="ltrTotalProfit" runat="server"></asp:Literal></h46>
                        </td>
                        <td class='gdvr'>
                            <h5>
                                </h5>
                        </td>


                      
                       
                        <td></td>
                        <td></td>
                    </tr>
                    </tbody>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>



