<%@ Page Title='' Language='C#' MasterPageFile='~/Admin/AdminMasterPage.master' AutoEventWireup='true' CodeFile='Default.aspx.cs' Inherits='Admin_Default' %>

<asp:Content ID='Content1' ContentPlaceHolderID='head' runat='Server'>
    <link href='https://www.traveljunction.co.uk/temp/dashboard.css' rel='stylesheet' />
    <link href="../../css/trackerdashboard.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID='Content2' ContentPlaceHolderID='ContentPlaceHolder1' runat='Server'>
    <asp:Button ID="btnClear" Visible="false" runat="server" Text="cls" Width="10px" BackColor="#e5e6e6" Height="10px" BorderWidth="0px" OnClick="btnClear_Click" />
    <asp:Literal ID="ltrMsg" runat="server"></asp:Literal>

  


    <div class='col-md-12'>
        <%=uniqueCalls %>
        <%=turnOver %>
    </div>
    <%-- <div class='col-md-12'>
        <div class='card'>
            <div class='card-head'>
                <header>Ticketing Alert</header>
                <div class='tools'>
                    <a class='btn btn-icon-toggle btn-collapse'><i class='fa fa-angle-down'></i></a>
                </div>
            </div>
            <!--end .card-head -->
            <div class='card-body no-padding height-6' style='display: block;'>--%>
    <%=ticketingAlert %>
    <%=queuedBooking %>
    <%=ticketIssuedBy %>
    <%=IssuedBooking %>
    <%--<%=FollowUps %>--%>
    <%--</div>
            <!--end .card-body -->
        </div>
        <!--end .card -->
    </div>--%>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div class="panel panel-default" id="pnlVendor" runat="server" visible="false">
                <div class="panel-heading">Change Payment Gateway</div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="col-md-3">Change Payment Gateway</div>
                            <div class="col-md-3">
                                <asp:DropDownList ID="ddlGCompany" CssClass="form-control" runat="server">
                                    <asp:ListItem Value="TRVJUNCTION_USA" Text="TRVJUNCTION USA"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList ID="ddlGatway" CssClass="form-control" runat="server">
                                    <asp:ListItem Value="flightsholiday" Text="flightsholiday"></asp:ListItem>
                                    <asp:ListItem Value="traveljunction" Text="traveljunction"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3">
                                <asp:Button ID="btnUpdateVendor" CssClass="btn btn-default" runat="server" Text="Change Vendor" OnClick="btnUpdateVendor_Click" />
                                <asp:Literal ID="ltrGMsg" runat="server"></asp:Literal>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress2" AssociatedUpdatePanelID="UpdatePanel2" DisplayAfter="1"
        runat="server">
        <ProgressTemplate>
            <asp:Literal ID="Literal2" runat="server" Text="Wait"></asp:Literal>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <script>

        $(document).ready(function ()
        { 
           ShowHideDiv($("#hdnPageSearch").val());
        });


        function ShowHideDiv(val)
        {
            if (val == "0") { //by default page load
                $('#todayDiv').show();
                $('#yesterdayDiv').hide();
                $('#lastWeekDiv').hide();
                $('#dateRangeDiv').hide();
            }
            else if (val == "1") //by searching
            {
                $('#todayDiv').hide();
                $('#yesterdayDiv').hide();
                $('#lastWeekDiv').hide();
                $('#dateRangeDiv').show();
            }
        }

        function HideDiv1(id) {
            $('#todayDiv').show();
            $('#yesterdayDiv').hide();
            $('#lastWeekDiv').hide();
            $('#dateRangeDiv').hide();
        }
        function HideDiv2(id) {
            $('#todayDiv').hide();
            $('#yesterdayDiv').show();
            $('#lastWeekDiv').hide();
            $('#dateRangeDiv').hide();
        }
        function HideDiv3(id) {
            $('#todayDiv').hide();
            $('#yesterdayDiv').hide();
            $('#lastWeekDiv').show();
            $('#dateRangeDiv').hide();
        }
        function HideDiv4(id) {
            $('#todayDiv').hide();
            $('#yesterdayDiv').hide();
            $('#lastWeekDiv').hide();
            $('#dateRangeDiv').show();
        }

        $("#btnSearchDateRangeData").click(function ()
        {
            waitingDialog.show('Please Wait...');
            var fDate = $("#ContentPlaceHolder1_txtFromDate").val();
            var tDate = $("#ContentPlaceHolder1_txtToDate").val();

            if (fDate == "" || tDate == "")
            {
                alert("Please select the From Date and To Date");
                waitingDialog.hide();
                return false;
            }
            else {
                return true;
                waitingDialog.hide();
            }
        });

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



