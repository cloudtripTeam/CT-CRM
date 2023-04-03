<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="SearchByTransaction.aspx.cs" Inherits="Admin_SearchByTransaction" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
     <%--<link href="../css/StyleSheet.css" rel="stylesheet" />--%>
    <link href="../../css/StyleSheet.css" rel="stylesheet" />
    <script src="https://www.traveljunction.co.uk/temp/CalendarAnyYear.js"></script>
    
    <link href="//cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/css/bootstrap-multiselect.css" rel="stylesheet" type="text/css" />
    <script src="//cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/js/bootstrap-multiselect.js" type="text/javascript"></script>
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
                        <a data-toggle="collapse" href="#bookingdetails">Search by transaction</a>
                    </h4>
                </div>
                <div class="pull-right"><a data-toggle="collapse" href="#bookingdetails"><span class="glyphicon glyphicon-search" style="color: #fff;"></span></a></div>
                <div class="clearfix"></div>
            </div>

            <div id="bookingdetails" class="panel-collapse collapse">
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-3">
                            <asp:TextBox class="form-control mb-10" ID="txtTransRef" runat="server" placeholder="Transaction Ref" />
                        </div>

                        <div class="col-md-2">


                            <asp:ListBox ID="ddlBookingStatus" runat="server" SelectionMode="Multiple" class="form-control mb-10">
                                <asp:ListItem Value="">Select Booking Status</asp:ListItem>
                                <asp:ListItem Value="Booked">Booked</asp:ListItem>
                                <asp:ListItem Value="Cancelled">Cancelled</asp:ListItem>
                                <asp:ListItem Value="Confirm">Confirm</asp:ListItem>
                                <asp:ListItem Value="Decline">Decline</asp:ListItem>
                                <asp:ListItem Value="Documents">Documents</asp:ListItem>
                                <asp:ListItem Value="Option">Option</asp:ListItem>
                                <asp:ListItem Value="Payments">Payments</asp:ListItem>
                                <asp:ListItem Value="Issued">Issued</asp:ListItem>
                                <asp:ListItem Value="Incomplete">Incomplete</asp:ListItem>
                                <asp:ListItem Value="Queue">Queue</asp:ListItem>
                                <asp:ListItem Value="Dupe">Dupe</asp:ListItem>
                                <asp:ListItem Value="Refund">Refund</asp:ListItem>
                                <asp:ListItem Value="Deposit Forfeited">Deposit Forfeited</asp:ListItem>
                                <asp:ListItem Value="ETicket Sent">ETicket Sent</asp:ListItem>
                                <asp:ListItem Value="Completed">Completed</asp:ListItem>
                            </asp:ListBox>
                        </div>
                        <div class="col-md-2">
                            <asp:ListBox ID="ddlCompany" SelectionMode="Multiple" class="form-control mb-10" runat="server"></asp:ListBox>
                        </div>
                        <div class="col-md-5">                           
                            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-danger btn-lg" OnClientClick="return SearchBooking();" Text="Search" OnClick="btnSearch_Click"></asp:Button>
                        </div>

                    </div>
                    <div class="clearfix"></div> 
                </div>
                <div class="panel-body">
                </div>
            </div>
        </div>
    </div>


    <!-- View Bookings -->
    <div class="clearfix"></div>
    <div class="panel" style="padding-top: 0px; margin-top: 20px;">
        <div class="panel-body " style="border: 1px solid #ddd; padding: 0px!important;">
            <asp:Label ID="lblmessage" runat="server" Text="" ForeColor="Red"></asp:Label>

            <asp:Repeater ID="rptrDetails" runat="server">
                <HeaderTemplate>
                    <div class="table-responsive">
                        <table class="table table-striped  table-hover table-bordered">
                            <thead>
                                <tr>
                                    <th class="gdvh">S#</th>
                                    <th class="gdvh">BookingRef</th>
                                    <th class="gdvh">SupplierRef</th>
                                    <th class="gdvh">PNR</th>
                                    <th class="gdvh">Company</th>
                                    <th class="gdvh">Provider</th>
                                    <th class="gdvh">BokDate</th>
                                    <th class="gdvh">Name</th>
                                    <th class="gdvh">TrvDate</th>
                                    <th class="gdvh">Payment</th>
                                    <th class="gdvh">Status</th>
                                    <th class="gdvh"></th>
                                    <th class="gdvh"></th>
                                </tr>
                            </thead>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <tr>
                            <td class='gdvr'>
                                <span><%# Container.ItemIndex+1%></span>
                            </td>
                            <td class='gdvr'>
                                <span onclick="ShowBookingDetails(&#39;<%# Eval("BookingID")%>&#39;,&#39;<%# Eval("ProdID")%>&#39;)"><%# Eval("BookingID")%></span>
                            </td>
                            <td class='gdvr'>
                                <span><%# Eval("Supplier_Ref")%></span>
                            </td>
                            <td class='gdvr'><%# Eval("PNR")%></td>
                            <td class='gdvr'><%# Eval("BookingByCompany")%></td>
                            <td class='gdvr'><%# Eval("Provider")%></td>
                            <td class='gdvr'><%# Eval("BookingDateTime","{0:dd-MM-yyyy}")%></td>
                            <td class='gdvr'><%#  Eval("FName")%>&nbsp;<%#  Eval("LName")%></td>
                            <td class='gdvr'><%# Eval("TravelDate","{0:dd-MM-yyyy}")%></td>
                            <td class='gdvr'><%# (Convert.ToDecimal( Eval("Sell_Price")) <= Convert.ToDecimal(Eval("Trns_Amount"))) ? "Full Payment" :  Convert.ToDecimal(Eval("Trns_Amount")) == 0 ? "No Payment" : (Convert.ToDecimal( Eval("Sell_Price")) > Convert.ToDecimal(Eval("Trns_Amount"))) ? "Partial Payment" : "" %></td>
                            <td class='gdvr'><%# Eval("BookingStatus")%></td>
                            <td class='gdvr'><%# Eval("BookingByType").ToString() == "INTR"? "<span style='color:blue'>OFFLINE</span>": "<span style='color:green'>ONLINE</span>" %></td>

                        </tr>
                        <tr>
                            <td class='gdvr2' colspan='13'><%# Eval("BookingRemarks")%></td>
                        </tr>
                        <tr>
                            <td class='gdvr' colspan='13' id='<%# Eval("BookingID")%><%# Eval("ProdID")%>' style='display: none; background-color: rgba(253, 174, 2, 0.478431);'></td>
                        </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                    </div>
                </FooterTemplate>
            </asp:Repeater>

        </div>
    </div>


    <div id="divBokDetails">
    </div>
    <input id="setascurrdate" type="hidden" />
    <input id="hdeprdate" type="hidden" />
    <input runat="server" id="hduser" type="hidden" />
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
                        <img src="../Images/Wait.gif" id="ImageProgressbar">
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #ffffff; color: #B9B9B9; vertical-align: middle; text-align: center; font-size: 18px; font-family: Verdana;" align="center" height="40"></td>
                </tr>
            </tbody>
        </table>
    </div>





    <script type="text/javascript">
        var SellPrice = 0.0;

        function SearchBooking() {
            waitingDialog.show('Please Wait...');
            return true;
        }
        function ShowBookingDetails(BID, PID) {

            if ($("#" + BID + PID).html() == "") {
                var strHtml = "<div class='destination_tab'><ul>" +
                                "<li class='first-li12' id='li" + BID + PID + "1' onclick=\"ShowHideDestTab('1','" + BID + "','" + PID + "');\">Summary</li>" +
                                "<li class='first-li12' id='li" + BID + PID + "2' onclick=\"ShowHideDestTab('2','" + BID + "','" + PID + "');\">Passenger Details</li>" +
                                "<li class='first-li12' id='li" + BID + PID + "3' onclick=\"ShowHideDestTab('3','" + BID + "','" + PID + "');\">Sector Details</li>" +
                                "<li class='first-li12' id='li" + BID + PID + "4' onclick=\"ShowHideDestTab('4','" + BID + "','" + PID + "');\">Amount Details</li>" +
                                "<li class='first-li12' id='li" + BID + PID + "5' onclick=\"ShowHideDestTab('5','" + BID + "','" + PID + "');\">Trns Details</li>" +
                                "<li class='first-li12' id='li" + BID + PID + "6' onclick=\"ShowHideDestTab('6','" + BID + "','" + PID + "');\">Remarks</li></ul></div><div style='clear:both;'></div>" +
                            "<div class='h-ct-midd-container'>" +
                                "<div id='divFare" + BID + PID + "1' style='display: none;'></div>" +
                                "<div id='divFare" + BID + PID + "2' style='display: none;'></div>" +
                                "<div id='divFare" + BID + PID + "3' style='display: none;'></div>" +
                                "<div id='divFare" + BID + PID + "4' style='display: none;'></div>" +
                                "<div id='divFare" + BID + PID + "5' style='display: none;'></div>" +
                                "<div id='divFare" + BID + PID + "6' style='display: none;'></div></div>";
                $("#" + BID + PID).html(strHtml);
                ShowHideDestTab("1", BID, PID);
            }
            $("#" + BID + PID).toggle();

        }

        function ShowHideDestTab(ID, BID, PID) {

            for (var i = 1; i <= 6; i++) {
                $("#divFare" + BID + PID + i).hide();
                document.getElementById("li" + BID + PID + i).className = '';
            }
            $("#divFare" + BID + PID + ID).show();
            document.getElementById("li" + BID + PID + ID).className = 'first-li12';
            if ($("#divFare" + BID + PID + ID).html() == "" || ID == 4) {
                switch (ID) {
                    case "1":
                        GetBookingSummary(ID, BID, PID);
                        break;
                    case "2":
                        GetPassengerDetails(ID, BID, PID);
                        break;
                    case "3":
                        GetSectorDetails(ID, BID, PID);
                        break;
                    case "4":
                        GetChargeDetails(ID, BID, PID);
                        break;
                    case "5":
                        GetTrnsDetails(ID, BID, PID);
                        break;
                    case "6":
                        GetRemarksDetails(ID, BID, PID);
                        break;

                    default: break;
                }
            }
        }

        function GetBookingSummary(ID, BID, PID) {
            waitingDialog.show('Please Wait...');
            $.ajax({
                type: "POST",
                url: "BookingDetails.aspx/GetBookingSummary",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify({ BookingID: BID, ProdID: PID }),
                responseType: "json",
                success: function (data) {
                    var jsdata = JSON.parse(data.d);
                    if (jsdata.BD.BookingBy != document.getElementById("ContentPlaceHolder1_hduser").value) {
                        jsdata.CD.EmailID = "xxxxxxx@xxx.xx";
                        jsdata.CD.PhoneNo = "xxxxxxxxxx";
                        jsdata.CD.MobileNo = "xxxxxxxxxx";
                    }

                    var strHtml = "<table width='100%' class='table' cellspacing='0' border='0' cellpadding='0'>" +
                                "<tr><td><b> BookingID</b></td><td>" + jsdata.BM.BookingID + "</td>" +
                                "<td><b>Currencey</b></td><td>" + jsdata.BM.CurrencyType + "</td></tr>" +
                                "<tr><td><b>PNR</b></td><td>" + jsdata.BD.PNR + "</td>" +
                                "<td><b>ProductType</b></td><td>" + jsdata.BD.ProductType + "</td></tr>" +
                                "<tr><td><b>Booking Status</b></td><td>" + jsdata.BD.BookingStatus + "</td>" +
                                "<td><b>CabinClass</b></td><td>" + jsdata.SM.CabinClass + "</td></tr>" +
                                "<tr><td><b>Plating Carrier</b></td><td>" + jsdata.SM.ValCarrier + "</td>" +
                                "<td><b>Company</b></td><td>" + jsdata.BM.BookingByCompany + "</td></tr>" +
                                "<tr><td><b>Origin</b></td><td>" + jsdata.SM.Origin + "</td>" +
                                "<td><b>SourceMedia</b></td><td>" + jsdata.BD.SourceMedia + "</td></tr>" +
                                "<tr><td><b>Destination</b></td><td>" + jsdata.SM.Destination + "</td>" +
                                "<td><b>Address</b></td><td>" + jsdata.CD.PAddress + "</td></tr>" +
                                "<tr><td><b>TripType</b></td><td>" + jsdata.SM.JType + "</td>" +
                                "<td><b>EmailID</b></td><td>" + jsdata.CD.EmailID + "</td></tr>" +
                                "<tr><td><b>BookingType</b></td><td>" + jsdata.BD.BookingByType + "</td>" +
                                "<td><b>PhoneNo</b></td><td>" + jsdata.CD.PhoneNo + "</td></tr>" +
                                "<tr><td><b>LastTickingDate</b></td><td>" + jsdata.SM.LastTktDate + "</td>" +
                                "<td><b>MobileNo</b></td><td>" + jsdata.CD.MobileNo + "</td></tr>" +
                                "<tr><td><b>BookingDate</b></td><td>" + jsdata.BD.BookingDateTime + "</td>" +
                                "<td><b>BookingBy</b></td><td>" + jsdata.BD.BookingBy + "</td></tr>" +
                                "<tr><td><b>Remarks</b></td><td colspan='3'>" + jsdata.BD.BookingRemarks + "</td>" +
                                "<td colspan='3'></td></tr></table>";
                    $("#divFare" + BID + PID + ID).html(strHtml);
                },
                error: function (data) { }
            });
            waitingDialog.hide();
        }

        function GetPassengerDetails(ID, BID, PID) {
            waitingDialog.show('Please Wait...');
            $.ajax({
                type: "POST",
                url: "BookingDetails.aspx/GetPassengerDetails",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify({ BookingID: BID, ProdID: PID }),
                responseType: "json",
                success: function (data) {
                    var jsdata = JSON.parse(data.d);
                    var strHtml = "<table width='100%' cellpadding='0' cellspacing='0' class='table' >" +
                                  "<tr><td class='gdvh'>SrNo</td>" +
                                  "<td class='gdvh'>PaxType</td>" +
                                  "<td class='gdvh'>Title</td>" +
                                  "<td class='gdvh'>FirstName</td>" +
                                  "<td class='gdvh'>Last Name</td>" +
                                  "<td class='gdvh'>DOB</td>" +
                                  "<td class='gdvh'>Gender</td></tr>";
                    $.each(jsdata.PD, function (key, value) {
                        strHtml += "<tr><td class='gdvr'>" + (key + 1) + "</td>" +
                                   "<td class='gdvr'>" + value.PaxType + "</td>" +
                                   "<td class='gdvr'>" + value.Title + "</td>" +
                                   "<td class='gdvr'>" + value.FName + "</td>" +
                                   "<td class='gdvr'>" + value.LName + "</td>" +
                                   "<td class='gdvr'>" + value.DOB + "</td>" +
                                   "<td class='gdvr'>" + value.PaxSex + "</td></tr>";
                    });
                    if (jsdata.length == 0)
                        strHtml += "<tr><td class='gdvr' colspan='7'>No any Pax!!</td></tr>";
                    $("#divFare" + BID + PID + ID).html(strHtml + "</table>");
                },
                error: function (data) { }
            });
            waitingDialog.hide();
        }

        function GetSectorDetails(ID, BID, PID) {
            waitingDialog.show('Please Wait...');
            $.ajax({
                type: "POST",
                url: "BookingDetails.aspx/GetSectorDetails",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify({ BookingID: BID, ProdID: PID }),
                responseType: "json",
                success: function (data) {
                    var jsdata = JSON.parse(data.d);
                    var strHtml = "<table width='100%' cellpadding='0' cellspacing='0' class='table' >" +
                                "<tr><td class='gdvh'>SrNo</td>" +
                                "<td class='gdvh'>CarierName</td>" +
                                "<td class='gdvh'>FlightNo</td>" +
                                "<td class='gdvh'>Class</td>" +
                                "<td class='gdvh'>FromDest</td>" +
                                "<td class='gdvh'>FromDateTime</td>" +
                                "<td class='gdvh'>ToDest</td>" +
                                "<td class='gdvh'>ToDateTime</td>" +
                                 "<td class='gdvh'>BaggageAllownce</td>" +
                                "<td class='gdvh'>TFrom</td>" +
                                "<td class='gdvh'>TTo</td>" +
                                "<td class='gdvh'>Status</td></tr>";
                    $.each(jsdata.SD, function (key, value) {
                        strHtml += "<tr><td class='gdvr'>" + value.SegID + "</td>" +
                                   "<td class='gdvr'>" + value.CarierName + "</td>" +
                                   "<td class='gdvr'>" + value.FlightNo + "</td>" +
                                   "<td class='gdvr'>" + value.FClass + "</td>" +
                                   "<td class='gdvr'>" + value.FromDest + "</td>" +
                                   "<td class='gdvr'>" + value.FromDateTime + "</td>" +
                                   "<td class='gdvr'>" + value.ToDest + "</td>" +
                                   "<td class='gdvr'>" + value.ToDateTime + "</td>" +
                                    "<td class='gdvr'>" + value.BaggageAllownce + "</td>" +
                                   "<td class='gdvr'>" + value.TerminalFrom + "</td>" +
                                   "<td class='gdvr'>" + value.TerminalTo + "</td>" +
                                   "<td class='gdvr'>" + value.FStatus + "</td></tr>";

                    });
                    if (jsdata.length == 0)
                        strHtml += "<tr><td class='gdvr' colspan='12'>No any Sectors!!</td></tr>";
                    $("#divFare" + BID + PID + ID).html(strHtml + "</table>");
                },
                error: function (data) { }
            });
            waitingDialog.hide();
        }

        function GetChargeDetails(ID, BID, PID) {
            waitingDialog.show('Please Wait...');
            $.ajax({
                type: "POST",
                url: "BookingDetails.aspx/GetChargeDetails",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify({ BookingID: BID, ProdID: PID }),
                responseType: "json",
                success: function (data) {
                    var jsdata = JSON.parse(data.d);
                    var strHtml = "<table width='100%' cellpadding='0' cellspacing='0' class='table' >" +
                                "<tr><td class='gdvh'>SrNo</td>" +
                                "<td class='gdvh'>ChargeID</td>" +
                                "<td class='gdvh'>ChargesFor</td>" +
                                "<td class='gdvh'>SellAmount</td>" +
                                "<td class='gdvh'>CostAmount</td>" +
                                "<td class='gdvh'>Remarks</td></tr>";
                    SellPrice = 0;
                    var CostPrice = 0.0;
                    $.each(jsdata.ACD, function (key, value) {
                        strHtml += "<tr><td class='gdvr'>" + (key + 1) + "</td>" +
                                   "<td class='gdvr'>" + value.ChargeID + "</td>" +
                                   "<td class='gdvr'>" + value.ChargesFor + "</td>" +
                                   "<td class='gdvr'>" + value.SellPrice + "</td>" +
                                   "<td class='gdvr'>" + value.CostPrice + "</td>" +
                                   "<td class='gdvr'>" + value.ChrgesRemarks + "</td></tr>";
                        SellPrice += parseFloat(value.SellPrice) * parseFloat(value.NoOfPax);
                        CostPrice += parseFloat(value.CostPrice) * parseFloat(value.NoOfPax);

                    });
                    if (jsdata.length == 0)
                        strHtml += "<tr><td class='gdvr' colspan='8'>No any Charges!!</td></tr>";
                    else {
                        strHtml += "<tr><td class='gdvf'></td>" +
                                     "<td class='gdvf'></td>" +
                                     "<td class='gdvf'>Total</td>" +
                                     "<td class='gdvf'>" + SellPrice.toFixed(2) + "</td>" +
                                     "<td class='gdvf'>" + CostPrice.toFixed(2) + "</td>" +
                            "<td class='gdvf'>Profit : " + (SellPrice.toFixed(2) - CostPrice.toFixed(2)).toFixed(2) + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href='payment/paynow.aspx?BID=" + BID + "'>Pay Now</a></td></tr>";
                    }
                    $("#divFare" + BID + PID + ID).html(strHtml + "</table>");
                },
                error: function (data) { }
            });


            waitingDialog.hide();
        }

        function GetTrnsDetails(ID, BID, PID) {
            waitingDialog.show('Please Wait...');
            //get the Chargers details to calculate the due amount
            GetChargeDetails("4", BID, PID);
            $.ajax({
                type: "POST",
                url: "BookingDetails.aspx/GetTrnsDetails",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify({ BookingID: BID, ProdID: PID }),
                responseType: "json",
                success: function (data) {
                    var jsdata = JSON.parse(data.d);


                    var strHtml = "<table width='100%' cellpadding='0' cellspacing='0' class='table' >" +
                        "<tr><td class='gdvh'>TrnsNo</td>" +
                        "<td class='gdvh'>TrnsAmount</td>" +
                        "<td class='gdvh'>CurrencyType</td>" +
                        "<td class='gdvh'>FuturePayID</td>" +
                        "<td class='gdvh'>TrnsType</td>" +
                        "<td class='gdvh'>PaymentStatus</td>" +
                        "<td class='gdvh'>TrnsDateTime</td>" +
                        "<td class='gdvh'>TrnsBy</td></tr>";
                    var TotalTRNAMT = 0.0;
                    if (jsdata.TM != null) {
                        $.each(jsdata.TM, function (key, value) {
                            strHtml += "<tr><td class='gdvr'>" + value.TrnsNo + "</td>" +
                                "<td class='gdvr'>" + value.TrnsAmount + "</td>" +
                                "<td class='gdvr'>" + value.TrnsCurrencyType + "</td>" +
                                "<td class='gdvr'>" + value.TrnsAuthNo + "</td>" +
                                "<td class='gdvr'>" + value.TrnsType + "</td>" +
                                "<td class='gdvr'>" + value.TrnsPaymentStatus + "</td>" +
                                "<td class='gdvr'>" + value.TrnsDateTime + "</td>" +
                                "<td class='gdvr'>" + value.TrnsBy + "</td></tr > ";
                            TotalTRNAMT += parseFloat(value.TrnsAmount);

                        });

                        if (jsdata.TM != null) {
                            strHtml += "<tr>" +
                                "<td class='gdvf'>Total : </td>" +
                                "<td class='gdvf'>" + TotalTRNAMT.toFixed(2) + "</td>" +
                                "<td class='gdvf'></td>" +
                                "<td class='gdvf'>Due Amount</td>" +
                                "<td class='gdvf'>" + (SellPrice - TotalTRNAMT).toFixed(2) + "</td>" +
                                "<td class='gdvf'></td>" +
                                "<td class='gdvf'></td>" +
                                "<td class='gdvf'></td></tr>";

                            $("#divFare" + BID + PID + ID).html(strHtml);
                        }

                    }




                    else {

                        $("#divFare" + BID + PID + ID).html("<table width='100%' class='table' cellspacing='0' border='0' cellpadding='0'>" +
                            "<tr><td colspan='8' style='color:#ff0000;'><b>No any transaction is found!!</b></td></tr>" +

                            "<tr>" +
                            "<td class='gdvf'>Total : </td>" +
                            "<td class='gdvf'>" + TotalTRNAMT.toFixed(2) + "</td>" +
                            "<td class='gdvf'></td>" +
                            "<td class='gdvf'>Due Amount</td>" +
                            "<td class='gdvf'>" + (SellPrice - TotalTRNAMT).toFixed(2) + "</td>" +
                            "<td class='gdvf'></td>" +
                            "<td class='gdvf'></td>" +
                            "<td class='gdvf'></td></tr>" +

                            "</table>");
                    }

                },
                error: function (data) {
                    $("#divFare" + BID + PID + ID).html("<table width='100%' class='table' cellspacing='0' border='0' cellpadding='0'>" +
                        "<tr><td style='color:#ff0000;'><b>No any transaction is found!!</b></td></tr></table>");
                }
            });
            waitingDialog.hide();
        }

        function GetRemarksDetails(ID, BID, PID) {
            waitingDialog.show('Please Wait...');
            $.ajax({
                type: "POST",
                url: "BookingDetails.aspx/GetRemarks",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify({ BookingID: BID, ProdID: PID }),
                responseType: "json",
                success: function (data) {
                    var jsdata = JSON.parse(data.d);
                    var strHtml = "<table width='100%' cellpadding='0' cellspacing='0' class='table' >" +
                                "<tr><td class='gdvh'>SrNo</td>" +
                                "<td class='gdvh'>Remarks</td>" +
                                "<td class='gdvh'>By</td>" +
                                "<td class='gdvh'>DatenTime</td>" +
                                "</tr>";

                    $.each(jsdata.RM, function (key, value) {
                        strHtml += "<tr><td class='gdvr'>" + (key + 1) + "</td>" +
                                   "<td class='gdvr'>" + value.Remark + "</td>" +
                                   "<td class='gdvr'>" + value.RemarksBy + "</td>" +
                                   "<td class='gdvr'>" + value.DatenTime + "</td>" +
                                   "</tr>";


                    });
                    if (jsdata.length == 0)
                        strHtml += "<tr><td class='gdvr' colspan='8'>No remark found</td></tr>";

                    $("#divFare" + BID + PID + ID).html(strHtml + "</table>");
                },
                error: function (data) { }
            });
            waitingDialog.hide();
        }


    </script>
</asp:Content>

