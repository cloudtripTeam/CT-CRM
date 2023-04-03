<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="AgentAssignedBooking.aspx.cs" Inherits="Admin_AgentAssignedBooking" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%--<link href="../css/StyleSheet.css" rel="stylesheet" />--%>
    <link href="../../css/StyleSheet.css" rel="stylesheet" />
    <script src="https://www.traveljunction.co.uk/temp/CalendarAnyYear.js"></script>
    <%--     <link href="//cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/css/bootstrap-multiselect.css" rel="stylesheet" type="text/css" />
    <script src="//cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/js/bootstrap-multiselect.js" type="text/javascript"></script>--%>
    <script type="text/javascript">

        $(function () {
            $('[id*=ddlCompany]').multiselect({
                includeSelectAllOption: true
            });
            $('[id*=ddlBookingStatus]').multiselect({
                includeSelectAllOption: true
            });
        });


    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="p-20" style="background: #fff;">
        <table class="table table-hover">
            <thead>
                <tr>
                    <th>Booking Ref. No.</th>
                    <th>Agent</th>
                    <th>From Date</th>
                    <th>To Date</th>
                    <th>Company</th>
                    <th>Status</th>
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
                            <asp:ListItem Value="Cancelled">Cancelled</asp:ListItem>
                            <asp:ListItem Value="Confirm">Confirm</asp:ListItem>
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
                            <asp:ListItem Value="Customer_Denied">Customer Denied</asp:ListItem>
                            <asp:ListItem Value="Left_voice_mail">Left Voicemail</asp:ListItem>

                        </asp:ListBox></td>

                    <td>
                        <asp:Button ID="btnFind" Text="Search" CssClass="btn btn-default" runat="server" OnClick="btnFind_Click" /></td>

                    <input id="setascurrdate" type="hidden" />
                    <input id="hdeprdate" type="hidden" />
                </tr>
            </tbody>
        </table>
        <hr />
        <div class="row">
            <table class="table table-hover">
                <tr>
                    <td>
                        <asp:Button ID="btnExport" runat="server" Text="Export"
                            OnClick="btnExport_Click" Visible="false" />
                        <asp:Literal ID="ltrInvc" runat="server"></asp:Literal>

                        <asp:GridView ID="gvAssignedBooking" Width="100%" runat="server" EmptyDataText="No Record Found." AutoGenerateColumns="false" OnDataBound="OnDataBound" OnRowCreated="OnRowCreated">
                            <Columns>
                                <asp:BoundField DataField="Assigned_To" HeaderStyle-CssClass="gdvh" ItemStyle-CssClass="gdvr" HeaderText="Agent" />
                                <asp:BoundField DataField="Booking_Ref" HeaderStyle-CssClass="gdvh" ItemStyle-CssClass="gdvr" HeaderText="Booking Ref No." />
                                <asp:BoundField DataField="PNR_Confirmation" HeaderStyle-CssClass="gdvh" ItemStyle-CssClass="gdvr" HeaderText="PNR" />
                                <asp:BoundField DataField="Origin" HeaderStyle-CssClass="gdvh" ItemStyle-CssClass="gdvr" HeaderText="Origin" />
                                <asp:BoundField DataField="Destination" HeaderStyle-CssClass="gdvh" ItemStyle-CssClass="gdvr" HeaderText="Destination" />
                                <asp:BoundField DataField="Source_Media" HeaderStyle-CssClass="gdvh" ItemStyle-CssClass="gdvr" HeaderText="Source Media" />
                                <asp:BoundField DataField="Company" HeaderStyle-CssClass="gdvh" ItemStyle-CssClass="gdvr" HeaderText="Company" />
                                <asp:BoundField DataField="Assigned_By" HeaderStyle-CssClass="gdvh" ItemStyle-CssClass="gdvr" HeaderText="Assigned By" />
                                <asp:BoundField DataField="Booking_By" HeaderStyle-CssClass="gdvh" ItemStyle-CssClass="gdvr" HeaderText="Booking By" />
                                <asp:BoundField DataField="Booking_Status" HeaderStyle-CssClass="gdvh" ItemStyle-CssClass="gdvr" HeaderText="Status" />
                                <asp:BoundField DataField="Booking_Date_Time" DataFormatString="{0:dd MMM yy}" HeaderStyle-CssClass="gdvh" ItemStyle-CssClass="gdvr" HeaderText="Booking Date" />
                                <asp:TemplateField HeaderText="Edit Booking" HeaderStyle-CssClass="gdvh">
                                    <ItemTemplate>
                                        <a href="FltBooking/amendBooking.aspx?BID=<%# Eval("Booking_Ref")%>&PID=001" style="text-decoration: none;background-color: blanchedalmond;" 
                                            class="gdvr">Edit</a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>

        </div>

    </div>




    <%--<div id="myModal" class="modal fade" role="dialog">
        <div class="modal-dialog" style="margin: 150px auto; position: absolute; left: 13%; width: 74%;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" style="margin-top:0px!important">&times;</button>
                    <h4 class="modal-title">Remarks..</h4>
                </div>
                <div class="modal-body" style="max-height: 500px; min-height: 200px; overflow-y: scroll;">
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>--%>

    <style>
        .gdvr_right {
            font-size: 0.90em;
            color: #222;
            padding: 5px 3px 5px 5px;
            text-align: right;
            /* height: 20px; */
            border-bottom: 1px solid #e1e1e1;
        }

        .DvDisp {
            display: none;
        }
    </style>


    <%--<script>
        function ViewRemarks(BookingRef)
        {
            $.ajax({
                type: "POST",
                url: "AgentAssignedBooking.aspx/BookingRemarks",
                data: '{bookingID: "' + BookingRef + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response)
                {
                    if (response.d != '')
                    {
                        $("#myModal .modal-body").html('');
                        $("#myModal .modal-body").html("<table width='100%' cellpadding='0' cellspacing='0' class='table'>"+ response.d +"</table>");
                        $('#myModal').modal("show");
                    }
                }
            });
        }
    </script>--%>
</asp:Content>

