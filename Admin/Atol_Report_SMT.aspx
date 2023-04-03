<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="Atol_Report_SMT.aspx.cs" Inherits="Admin_Atol_Report_SMT" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%--<link href="../css/StyleSheet.css" rel="stylesheet" />--%>
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
                        <a data-toggle="collapse" href="#bookingdetails">Old System Data</a>
                    </h4>
                </div>
                <div class="pull-right"><a data-toggle="collapse" href="#bookingdetails"><span class="glyphicon glyphicon-search" style="color: #fff;"></span></a></div>
                <div class="clearfix"></div>
                <div class="row">
                    

                    <div class="col-md-3">

                        <asp:TextBox class="form-control mb-10" ID="txtFromDate" onclick="showCalender(this);" runat="server" placeholder="From Booking Date" />
                    </div>
                    <div class="col-md-3">

                        <asp:TextBox class="form-control mb-10" ID="txtToDate" onclick="showCalender(this);" runat="server" placeholder="To Booking Date" />
                    </div>
                     <div class="col-md-3">
                         <asp:DropDownList ID="ddlSupplier" runat="server" CssClass="form-control">
                              <asp:ListItem Value="" Selected="True">Select Supplier</asp:ListItem>
                                <asp:ListItem Value="FLIGHTS & HOLIDAYS UK LTD">FLIGHTS & HOLIDAYS UK LTD</asp:ListItem>
                                <asp:ListItem Value="Major Travel">Major Travel</asp:ListItem>
                                <asp:ListItem Value="Citibond Travel">Citibond Travel</asp:ListItem>
                                <asp:ListItem Value="Travel 2">Travel 2</asp:ListItem>
                                <asp:ListItem Value="INTERGLOBE AIR TRANSPORT">INTERGLOBE AIR TRANSPORT</asp:ListItem>
                                

                         </asp:DropDownList>
                     </div>
                     <div class="col-md-3">
                         <asp:DropDownList ID="ddlAtolType" runat="server" CssClass="form-control">
                              <asp:ListItem Value="" Selected="True">Select Atol Type</asp:ListItem>
                                <asp:ListItem Value="RETAIL AGENCY">RETAIL AGENCY</asp:ListItem>
                                
                                <asp:ListItem Value="Flights Only / PUBLIC BONDED">Flights Only / PUBLIC BONDED</asp:ListItem>
                                
                         </asp:DropDownList>
                         </div>
                    </div>
                <div  class="row">                   
                    

                    <div class="col-md-3">

                        <asp:TextBox class="form-control mb-10" ID="txtDepartFrom" onclick="showCalender(this);" runat="server" placeholder="From Depart Date" />
                    </div>
                    <div class="col-md-3">

                        <asp:TextBox class="form-control mb-10" ID="txtDepartTo" onclick="showCalender(this);" runat="server" placeholder="To Depart Date" />
                    </div>


                    <div class="col-md-3">
                        <asp:Button ID="btnFind" Text="Search" CssClass="btn btn-default" runat="server" OnClick="btnFind_Click" />
                        <input id="setascurrdate" type="hidden" />
                        <input id="hdeprdate" type="hidden" />
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
                                <th class="gdvh" >Invoice Date</th>
                                <th>Passenger</th>
                                <th class="gdvh">Ticket No</th>
                                <th class="gdvh">Cost</th>
                                <th class="gdvh">Departure Date</th>
                                <th class="gdvh">Supplier</th>
                                <th class="gdvh">PNR</th>
                                <th class="gdvh">ATOL Type</th>
                                <th class="gdvh">Sales</th>
                                

                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td class='gdvr'><span><%# Container.ItemIndex+1%></span></td>
                        <td class='gdvr'>
                            <asp:Literal ID="Label1" runat="server" Text='<%#Bind("Invoice_Number") %>'></asp:Literal>
                        </td>
                        <td class='gdvr'>
                            <asp:Literal ID="Label2" runat="server" Text='<%# Convert.ToDateTime(Eval("Invoice_Date")).ToString("dd MMM yyyy") %>'></asp:Literal></td>
                        <td class='gdvr'>
                            <asp:Literal ID="Label10" runat="server" Text='<%# Eval("Passenger") %>'></asp:Literal></td>

                        <td class='gdvr'>
                            <asp:Literal ID="Label11" runat="server" Text='<%# Eval("Ticket_No") %>'></asp:Literal></td>
                        <td class='gdvr'>
                            <asp:Literal ID="Label13" runat="server" Text='<%# Eval("Cost") %>'></asp:Literal></td>

                        <td class='gdvr'>
                             <asp:Literal ID="Literal3" runat="server" Text='<%# Convert.ToDateTime(Eval("Departure_Date")).ToString("dd MMM yyyy") %>'></asp:Literal></td>
                        </td>
                        <td class='gdvr'>
                            <asp:Literal ID="Label4" runat="server" Text='<%#Bind("Supplier") %>'></asp:Literal></td>
                         <td class='gdvr'>
                           <asp:Literal ID="Literal1" runat="server" Text='<%#Bind("PNR") %>'></asp:Literal></td>

                        <td class='gdvr'>
                            <asp:Literal ID="Label14" runat="server" Text='<%#Bind("ATOL_Type") %>'></asp:Literal></td>
                        <td class='gdvr'>
                            <asp:Literal ID="Label15" runat="server" Text='<%#Bind("Sales") %>'></asp:Literal></td>
                       

                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    <tr>
                        <td></td>
                        
                        <td colspan="2" class='gdvr'>
                            <h6>Total Passengers :</h6>
                        </td>
                        <td><h5>
                                <asp:Literal ID="ltrTotPax" runat="server"></asp:Literal></h5></td>
                        
                        
                         <td></td>
                        
                        <td colspan="2" class='gdvr'>
                            <h6>Total Cost :</h6>
                        </td>
                        <td class='gdvr'>
                            <h5><asp:Literal ID="ltrTotalNetProfit" runat="server"></asp:Literal>
                                </h5>
                        </td>

                        <td  colspan="2" class='gdvr'>
                            <h6>Total Sale :
                            <asp:Literal ID="ltrTotalProfit" runat="server"></asp:Literal></h46>
                        </td>                                      
                       
                       
                        
                    </tr>
                    </tbody>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>


