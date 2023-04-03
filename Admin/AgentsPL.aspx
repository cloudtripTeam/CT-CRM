<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="AgentsPL.aspx.cs" Inherits="Admin_AgentsPL" %>

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
     <div class="container-fluid">
    <div class="panel panel-default">
                <div class="panel-heading">
                    Agent P&L Report
                  
                </div>
                <div class="panel-body" style="line-height: 34px;">
    <div class="p-20" style="background: #fff;">
        <table class="table table-hover fl-table">
            <thead>
                <tr>
                    <th>Booking Ref.</th>
                    <th>Agent</th>
                    <th>From Date</th>
                    <th>To Date</th>
                    <th>Company</th>
                    <th>Status</th>
                    <th>Booking Type</th>
                    <th></th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>
                        <asp:TextBox ID="txtInvNo" placeholder="reference no" CssClass="form-control" runat="server"></asp:TextBox></td>
                    <td>
                        <asp:TextBox ID="txtAgent" placeholder="Agent" CssClass="form-control" runat="server"></asp:TextBox></td>
                    <td>
                        <asp:TextBox ID="txtFromDate" onclick="showCalender(this);" placeholder="From Date" CssClass="form-control" runat="server"></asp:TextBox></td>
                    <td>
                        <asp:TextBox ID="txtToDate" onclick="showCalender(this);" placeholder="Date Till" CssClass="form-control" runat="server"></asp:TextBox></td>
                    <td>
                        <asp:ListBox ID="ddlCompany" class="form-control mb-10" SelectionMode="Multiple" runat="server"></asp:ListBox>
                    </td>
                    <td>
                        <asp:ListBox ID="ddlBookingStatus" runat="server" SelectionMode="Multiple" class="form-control mb-10">
                            <asp:ListItem Value="">Select Booking Status</asp:ListItem>
                            <asp:ListItem Value="Booked">Booked</asp:ListItem>
                            <asp:ListItem Value="Confirm">Confirm</asp:ListItem>
                             <asp:ListItem Value="Cancelled">Cancelled</asp:ListItem>
                            <asp:ListItem Value="Decline">Decline</asp:ListItem>
                            <asp:ListItem Value="Documents">Documents</asp:ListItem>
                            <asp:ListItem Value="Payments">Payments</asp:ListItem>
                            <asp:ListItem Value="Incomplete">Incomplete</asp:ListItem>
                            <asp:ListItem Value="Issued">Issued</asp:ListItem>
                             <asp:ListItem Value="ReIssued">ReIssued</asp:ListItem>
                            <asp:ListItem Value="Follow UP">Follow Up</asp:ListItem>
                            <asp:ListItem Value="Option">Option</asp:ListItem>
                            <asp:ListItem Value="Queue">Queue</asp:ListItem>
                            <asp:ListItem Value="Dupe">Dupe</asp:ListItem>
                            <asp:ListItem Value="Refund">Refund</asp:ListItem>
                            <asp:ListItem Value="Deposit Forfeited">Deposit Forfeited</asp:ListItem>
                            <asp:ListItem Value="ETicket Sent">ETicket Sent</asp:ListItem>
                            <asp:ListItem Value="TKTNotFound">TKTNotFound</asp:ListItem>
                            <asp:ListItem Value="Completed">Completed</asp:ListItem>
                            <asp:ListItem Value="FutureCredit">Future Credit</asp:ListItem>
                            <asp:ListItem Value="ChargeBack">Charge Back</asp:ListItem>
                            <asp:ListItem Value="Customer_Denied">Customer Denied</asp:ListItem>
                            <asp:ListItem Value="Left_voice_mail">Left Voicemail</asp:ListItem>

                        </asp:ListBox></td>
                    <td>
                        <asp:DropDownList ID="ddlBookingType" class="form-control mb-10" runat="server">
                            <asp:ListItem Value="">All Booking Type</asp:ListItem>
                            <asp:ListItem Value="DICT">Online</asp:ListItem>
                            <asp:ListItem Value="INTR">Offline</asp:ListItem>
                        </asp:DropDownList>

                    </td>
                    <td></td>
                    <td>
                        <asp:Button ID="btnFind" Text="Search" CssClass="btn btn-default" runat="server" OnClick="btnFind_Click" /></td>
                    <input id="setascurrdate" type="hidden" />
                    <input id="hdeprdate" type="hidden" />
                </tr>
            </tbody>
        </table>
       
        <div class="row">
            <table class="table table-hover">
                <tr>
                    <td>
                        <asp:Button ID="btnExport" runat="server" Text="Export"
                            OnClick="btnExport_Click" Visible="false" />
                        <asp:Literal ID="ltrInvc" runat="server"></asp:Literal>

                        <asp:GridView ID="gvInvoice" Width="100%" runat="server" EmptyDataText="No Record Found." AutoGenerateColumns="false" OnDataBound="OnDataBound" OnRowCreated="OnRowCreated" OnRowDataBound="gvInvoice_RowDataBound">
                            <Columns>
                                <asp:BoundField DataField="Booking_By" HeaderStyle-CssClass="gdvh" ItemStyle-CssClass="gdvr" HeaderText="Agent" />
                                <asp:BoundField DataField="BookingRef" HeaderStyle-CssClass="gdvh" ItemStyle-CssClass="gdvr" HeaderText="Booking Ref No." />
                                <asp:BoundField DataField="Profit_Amout" HeaderStyle-CssClass="gdvh" ItemStyle-CssClass="gdvr_right" HeaderText="Profit" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="Cost_Price" HeaderStyle-CssClass="gdvh" ItemStyle-CssClass="gdvr_right" HeaderText="Cost Price" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="Sell_Price" HeaderStyle-CssClass="gdvh" ItemStyle-CssClass="gdvr" HeaderText="Payment Type" />
                                <asp:BoundField DataField="Trns_Amount" HeaderStyle-CssClass="gdvh" ItemStyle-CssClass="gdvr_right" HeaderText="Trns Amount" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="First_Name" HeaderStyle-CssClass="gdvh" ItemStyle-CssClass="gdvr" HeaderText="Pax" />
                                <asp:BoundField DataField="Origin" HeaderStyle-CssClass="gdvh" ItemStyle-CssClass="gdvr" HeaderText="Origin" />
                                <asp:BoundField DataField="Destination" HeaderStyle-CssClass="gdvh" ItemStyle-CssClass="gdvr" HeaderText="Dest" />
                                <asp:BoundField DataField="PNR" HeaderStyle-CssClass="gdvh" ItemStyle-CssClass="gdvr" HeaderText="PNR" />
                                <asp:BoundField DataField="Validating_Carrier" HeaderStyle-CssClass="gdvh" ItemStyle-CssClass="gdvr" HeaderText="Airline" />
                                <asp:BoundField DataField="Company" HeaderStyle-CssClass="gdvh" ItemStyle-CssClass="gdvr" HeaderText="Company" />
                                <asp:BoundField DataField="SourceMedia" HeaderStyle-CssClass="gdvh" ItemStyle-CssClass="gdvr" HeaderText="SourceMedia" />
                                <asp:BoundField DataField="BookingStatus" HeaderStyle-CssClass="gdvh" ItemStyle-CssClass="gdvr" HeaderText="Status" />
                                <asp:BoundField DataField="Booking_Date_Time" DataFormatString="{0:dd MMM yy}" HeaderStyle-CssClass="gdvh" ItemStyle-CssClass="gdvr" HeaderText="Booking Date" />

                                <asp:HyperLinkField DataNavigateUrlFields="BookingRef" Target="_blank" ItemStyle-CssClass="gdvr" DataNavigateUrlFormatString="~/Admin/FltBooking/AmendBooking.aspx?BID={0}&PID=001" Text="Edit" />
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>

        </div>

    </div>
</div>
        </div>
    <style>
        .gdvr_right {
            font-size: 0.90em;
            color: #222;
            padding: 5px 3px 5px 5px;
            text-align: right;
            /* height: 20px; */
          
        }

        .DvDisp {
            display: none;
        }
        
         .fl1-table tbody tr:nth-child(odd) {
         background-color: #4fc3a1;
            color: #fff;
    }
     .fl1-table tbody tr:first-child td {
        background-color: #adf0ff;
         height: 0px;
    }
    .fl1-table tr:nth-child(even) {
        background: transparent;
    }
    .fl1-table tr td:nth-child(odd) {
        
        border-right: 1px solid #E6E4E4;
    }
    .fl1-table tr td:nth-child(even) {
        border-right: 1px solid #E6E4E4;
    }
   
        .fl1-table {
    border-radius: 5px;
    font-size: 12px;
    font-weight: normal;
    border: none;
    border-collapse: collapse;
    width: 100%;
    max-width: 100%;
    white-space: nowrap;
    background-color: white;
}
        .fl1-table thead th {
    color: #ffffff;
    background: #4FC3A1;
}


.fl1-table thead th:nth-child(odd) {
    color: #ffffff;
    background: #00366c;
   
}

.fl1-table tr:nth-child(even) {
    background: #F8F8F8;
}
    </style>
         </div>
</asp:Content>

