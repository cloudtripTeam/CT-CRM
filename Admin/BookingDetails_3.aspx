﻿<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/AdminMasterPage.master" CodeFile="BookingDetails_3.aspx.cs" Inherits="Admin_BookingDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%-- <link href="../css/StyleSheet.css" rel="stylesheet" />--%>
    <link href="../../css/StyleSheet.css" rel="stylesheet" />
    <script src="https://www.traveljunction.co.uk/temp/CalendarAnyYear.js"></script>
    <link rel="stylesheet" href="https://pro.fontawesome.com/releases/v5.10.0/css/all.css"/>

    <%--    <link href="//cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/css/bootstrap-multiselect.css" rel="stylesheet" type="text/css" />
    <script src="//cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/js/bootstrap-multiselect.js" type="text/javascript"></script>--%>

    <style>
        .btnbook {
            background-color: #f3f3f3 !important;
            border: green 1px solid;
            padding: 2px 3px;
            border-radius: 5px;
            display: inline-block;
            width: 85px;
            margin-bottom: 2px;
            text-align: center;
            font-size: 11px !important;
            padding-right: 0px !important;
        }

            .btnbook span {
                padding-right: 0px !important;
                text-align: center;
            }

        .btnbookins {
            background-color: #f3f3f3 !important;
            border: green 1px solid;
            padding: 2px 3px;
            border-radius: 5px;
            display: inline-block;
            width: 135px;
            text-align: center;
            font-size: 11px !important;
            padding-right: 0px !important;
        }
    </style>

    <style>
        .blink_me 
        {
            animation: blinker 1s linear infinite;
        }
        @keyframes blinker 
        {
            50% {
                opacity: 0;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="HiddenField1" Value="0.0" runat="server" />
    <!-- Search Panel -->
    <div class="panel-group">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="pull-left">
                    <h4 class="panel-title">
                        <a data-toggle="collapse" href="#bookingdetails">Booking Details</a>
                    </h4>
                </div>
                <div class="pull-right">
                    <a data-toggle="collapse" href="#bookingdetails">
                      <i class="fa fa-fw fa-chevron-down" style="color:white"></i></a></div>
                <div class="clearfix"></div>
            </div>


            <div id="bookingdetails" class="panel-collapse collapse">
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-2">
                            <asp:TextBox class="form-control mb-10" ID="txtBookingID" runat="server" placeholder="Booking ID" />
                        </div>
                        <div class="col-md-2">
                            <asp:TextBox ID="txtPNRConfirmation" class="form-control mb-10" runat="server" placeholder="PNR" />
                        </div>
                        <div class="col-md-2">
                            <asp:TextBox class="form-control mb-10" ID="txtFromDate" onclick="showCalender(this);" runat="server" AutoComplete="off" placeholder="From Booking Date" />
                        </div>
                        <div class="col-md-2">
                            <asp:TextBox class="form-control mb-10" ID="txtToDate" onclick="showCalender(this);" runat="server" AutoComplete="off" placeholder="To Booking Date" />
                        </div>
                        <div class="col-md-2" style="display:none">
                            <asp:TextBox class="form-control mb-10" ID="txtJFromDate" onclick="showCalender(this);" AutoCompleteType="None" runat="server" placeholder="Departure Date" />
                        </div>
                        <div class="col-md-2" style="display:none">
                            <asp:TextBox class="form-control mb-10" ID="txtJToDate" onclick="showCalender(this);" runat="server" AutoCompleteType="None" placeholder="Return Date" />
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-2">
                            <asp:ListBox ID="ddlCompany" SelectionMode="Multiple" class="form-control mb-10" runat="server"></asp:ListBox>
                        </div>
                        <div class="col-md-2" style="display:none">
                            <asp:DropDownList class="form-control" ID="ddlSourceMedia" runat="server">
                                <asp:ListItem Value="">ANY Source Media</asp:ListItem>
                            </asp:DropDownList>
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
                                <asp:ListItem Value="ReIssued">ReIssued</asp:ListItem>
                                <asp:ListItem Value="Incomplete">Incomplete</asp:ListItem>
                                <asp:ListItem Value="Queue">Queue</asp:ListItem>
                                <asp:ListItem Value="Follow UP">Follow UP</asp:ListItem>
                                <asp:ListItem Value="Dupe">Dupe</asp:ListItem>
                                <asp:ListItem Value="Refund">Refund</asp:ListItem>
                                <asp:ListItem Value="TKTNotFound">TKTNotFound</asp:ListItem>
                                <asp:ListItem Value="Deposit Forfeited">Deposit Forfeited</asp:ListItem>
                                <asp:ListItem Value="ETicket Sent">ETicket Sent</asp:ListItem>
                                <asp:ListItem Value="Completed">Completed</asp:ListItem>
                                <asp:ListItem Value="FutureCredit">Future Credit</asp:ListItem>
                                <asp:ListItem Value="ChargeBack">Charge Back</asp:ListItem>
                                <asp:ListItem Value="Customer_Denied">Customer Denied</asp:ListItem>
                                <asp:ListItem Value="Left_voice_mail">Left Voicemail</asp:ListItem>
                            </asp:ListBox>
                        </div>
                        <div class="col-md-2" style="display:none">
                            <asp:DropDownList ID="ddlBookingType" class="form-control mb-10" runat="server">
                                <asp:ListItem Value="">All Booking Type</asp:ListItem>
                                <asp:ListItem Value="DICT">Online</asp:ListItem>
                                <asp:ListItem Value="INTR">Offline</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-2" style="display:none">
                            <asp:DropDownList ID="ddlPaymentType" class="form-control mb-10" runat="server">
                                <asp:ListItem Value="">Payment Type</asp:ListItem>
                                <asp:ListItem Value="Full">Full Payment</asp:ListItem>
                                <asp:ListItem Value="Partial">Partial Payment</asp:ListItem>
                                <asp:ListItem Value="NoPayment">No Payment</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            <asp:DropDownList ID="ddlYear" class="form-control mb-10" runat="server">
                                <asp:ListItem Value="LY" Selected="True">Last Year</asp:ListItem>
                                <asp:ListItem Value="L2Y">Last 2 Years</asp:ListItem>
                                <asp:ListItem Value="ALL">ALL</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="clearfix"></div>

                    <div class="row">
                        <div class="col-md-2">
                            <asp:TextBox class="form-control mb-10" ID="txtPaxFirstName" runat="server" placeholder="First Name"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <asp:TextBox class="form-control mb-10" ID="txtPaxLastName" runat="server" placeholder="Last Name"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <asp:TextBox class="form-control mb-10" ID="txtEmailAddress" runat="server" placeholder="Email ID"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <asp:TextBox class="form-control mb-10" ID="txtPhoneNo" runat="server" placeholder="Phone No"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <asp:TextBox class="form-control mb-10" ID="txtMobileNo" runat="server" placeholder="Mobile No"></asp:TextBox>
                        </div>
                        <div class="col-md-2" style="display:none">
                            <asp:TextBox class="form-control mb-10" ID="txtSupplierRef" runat="server" placeholder="Supplier ref"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2" style="display:none">
                            <asp:TextBox class="form-control mb-10" ID="txtTransRef" runat="server" placeholder="Transaction ref"></asp:TextBox>
                        </div>
                        <div class="col-md-2" style="display:none">
                            <asp:CheckBox ID="chkSelf" Text=" Own" TextAlign="Left" runat="server" />
                        </div>
                        <div class="col-md-2"></div>
                        <div class="col-md-2"></div>
                        <div class="col-md-2"></div>
                        <div class="col-md-2">
                            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-danger btn-lg" OnClientClick="return SearchBooking();"
                                Text="Search" OnClick="btnSearch_Click"></asp:Button>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>
    <!-- View Bookings -->
    <div class="clearfix"></div>
    <div class="panel" style="padding-top: 0px;">
        <div class="panel-body " style="border: 1px solid #ddd; padding: 0px!important;">
            <asp:Label ID="lblmessage" runat="server" Text="" ForeColor="Red"></asp:Label>
            <asp:Button ID="btnExport" runat="server" Text="Export"
                OnClick="btnExport_Click" Visible="false" />
            <asp:Repeater ID="rptrDetails" runat="server">
                <HeaderTemplate>
                    <div class="table-responsive">
                        <table class="table table-striped  table-hover table-bordered" style="font-size: 12px !important;">
                            <thead>
                                <tr>
                                    <th class="gdvh">S#</th>
                                    <th class="gdvh">Bok Ref</th>
                                    <th class="gdvh">Supp Ref</th>
                                    <th class="gdvh">PNR</th>
                                    <th class="gdvh">Origin</th>
                                    <th class="gdvh">Dest</th>
                                    <th class="gdvh">Carrier</th>
                                    <th class="gdvh">Company</th>
                                    <th class="gdvh">Src Media</th>
                                    <th class="gdvh">Provider</th>
                                    <th class="gdvh">BokDate</th>
                                    <th class="gdvh">Name</th>
                                    <th class="gdvh">TrvDate</th>
                                    <th class="gdvh">Payment</th>
                                    <th class="gdvh">Assigned</th>
                                    <th class="gdvh">Status</th>
                                    <th class="gdvh">Follow</th>
                                    <th class="gdvh">BokMode</th>
                                    <th class="gdvh">Action</th>
                                    <th class="gdvh"></th>
                                </tr>
                            </thead>
                </HeaderTemplate>
                <ItemTemplate>

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
                        <td class='gdvr'><%# Eval("Origin")%></td>
                        <td class='gdvr'><%# Eval("Destination")%></td>
                        <td class='gdvr'><%# Eval("Carrier")%></td>
                        <td class='gdvr'><%# Eval("BookingByCompany")%></td>
                        <td class='gdvr'><%# Eval("SourceMedia")%></td>
                        <td class='gdvr'><%# Eval("Provider")%></td>
                        <td class='gdvr'><%# Eval("BookingDateTime","{0:dd-MM-yy}")%></td>
                        <td class='gdvr'><%#  Eval("FName").ToString().ToUpper() %>&nbsp;<%#  Eval("LName").ToString().ToUpper() %></td>
                        <td class='gdvr'><%# Eval("TravelDate","{0:dd-MM-yy}")%></td>
                        <td class='gdvr'><%# (Convert.ToDecimal( Eval("Sell_Price")) <= Convert.ToDecimal(Eval("Trns_Amount"))) ? "Full Payment" :  Convert.ToDecimal(Eval("Trns_Amount")) == 0 ? "No Payment" : (Convert.ToDecimal( Eval("Sell_Price")) > Convert.ToDecimal(Eval("Trns_Amount"))) ? "Partial Payment" : "" %></td>
                        <td class='gdvr'><%# Eval("BookingAssigned")%></td>
                        <td class='gdvr'><%# Eval("BookingStatus")%></td>
                        <td class='gdvr blink_me' style="color: crimson;"><%# Eval("FollowDate","{0:dd MMM yy}")%></td>
                        <td class='gdvr'><%# Eval("BookingByType").ToString() == "INTR"? "<span style='color:blue'>OFFLINE</span>": "<span style='color:green'>ONLINE</span>" %>
                            <%# string.Concat("<a Target='_blank'  style='display:none' href='FlightQuote2.aspx?BID=", Common.EncryptString(Convert.ToString(Eval("BookingID")),""), "&PID=001'>Send&nbsp;Quote</a>") %>
                          
                        </td>
                        <%-- <td class='gdvr'>
                            <a runat='server' class='btnbook' target="_blank" href='<%# Eval("BookingID", "~/Admin/FltBooking/AmendBooking.aspx?BID={0}&PID=001" ) %>'>Edit</a>
                                <%# Eval("BookingStatus").ToString().ToUpper() == "QUEUE"  ? string.Concat("<a class='btnbook' href='SendToSupplier.aspx?BID=", Eval("BookingID") , "&PID=001'><span style='color:green'>Send&nbsp;to&nbsp;Supplier</span></a>") : ((Convert.ToDecimal( Eval("Sell_Price")) <= Convert.ToDecimal(Eval("Trns_Amount"))) ||  (Eval("BookingByType").ToString() =="COMPLETE") || (Eval("BookingByType").ToString() =="INTR") || Eval("BookingStatus").ToString().ToUpper() == "INCOMPLETE") ? (Eval("BookingByCompany").ToString().ToUpper() == "XYZ") ? string.Concat("<a Target='_blank' class='btnbook' href='FlightQuote2.aspx?BID=", Common.EncryptString(Convert.ToString(Eval("BookingID")),""), "&PID=001'>Send&nbsp;Quote</a>") : string.Concat("<a Target='_blank' class='btnbook' href='FlightQuote.aspx?BID=", Eval("BookingID") , "&PID=001'>Send&nbsp;Quote</a>") : "" %>
                            

                            <%# (Eval("BookingStatus").ToString().ToUpper() == "ISSUED" ||  Eval("BookingStatus").ToString().ToUpper() == "COMPLETED")  ? string.Concat("<a class='btnbook' href='send_eticket.aspx?BID=", Eval("BookingID") , "&PID=001'><span style='color:green'>Send Eticket</span></a>")  : ""%> <%# com.GetAtolLink( Eval("IssuedBy").ToString(),Eval("ATOL_Type").ToString(),Eval("BookingID").ToString(),"001") %>
                            <%# Eval("Invoice_No").ToString() !="" ? string.Concat("<a class='btnbook' href='Invoice.aspx?BID=", Eval("BookingID") , "&PID=001'><span style='color:green'>Invoice</span></a>") :  Eval("BookingStatus").ToString().ToUpper() != "DUPE" && Eval("BookingStatus").ToString().ToUpper() != "CANCELLED" && Eval("BookingStatus").ToString().ToUpper() != "INCOMPLETE" ? string.Concat("<a class='btnbook' href='Invoice.aspx?BID=", Eval("BookingID") , "&PID=001'><span style='color:green'>Confirmation</span></a>") : "" %> 
                        </td>--%>


                        <td class='gdvr'>
                            <a runat='server' class='btnbook' target="_blank" href='<%# Eval("BookingID", "~/Admin/FltBooking/AmendBooking.aspx?BID={0}&PID=001" ) %>'>Edit</a>
                            <%# Eval("BookingStatus").ToString().ToUpper() == "QUEUE"  ? string.Concat("<a class='btnbook' href='SendToSupplier.aspx?BID=", Eval("BookingID") ,
                            "&PID=001'><span style='color:green'>Send&nbsp;to&nbsp;Supplier</span></a>") : string.Concat("<a Target='_blank' class='btnbook' href='FlightQuote.aspx?BID=", Eval("BookingID") , "&PID=001'>Send&nbsp;Quote</a>")   %>


                            <%# (Eval("BookingStatus").ToString().ToUpper() == "ISSUED" ||  Eval("BookingStatus").ToString().ToUpper() == "COMPLETED")  ? string.Concat("<a class='btnbook' href='send_eticket.aspx?BID=", Eval("BookingID") , "&PID=001'><span style='color:green'>Send Eticket</span></a>")  : ""%> <%# com.GetAtolLink( Eval("IssuedBy").ToString(),Eval("ATOL_Type").ToString(),Eval("BookingID").ToString(),"001") %>
                            <%# Eval("Invoice_No").ToString() !="" ? string.Concat("<a class='btnbook' href='Invoice.aspx?BID=", Eval("BookingID") , "&PID=001'><span style='color:green'>Invoice</span></a>") :  Eval("BookingStatus").ToString().ToUpper() != "DUPE" && Eval("BookingStatus").ToString().ToUpper() != "CANCELLED" && Eval("BookingStatus").ToString().ToUpper() != "INCOMPLETE" ? string.Concat("<a class='btnbook' href='Invoice.aspx?BID=", Eval("BookingID") , "&PID=001'><span style='color:green'>Confirmation</span></a>") : "" %> 
                        </td>

                        <td><a runat='server' style="color: #0d9dd4; font-size: 12px;" target="_blank" href='<%# Eval("BookingID", "~/Admin/FollowUp.aspx?BID={0}&PID=001" ) %>'>Follow</a></td>
                    </tr>
                    <tr>
                        <td class='gdvr2' colspan='16'><%# Eval("BookingRemarks")%></td>
                    </tr>
                    <tr>
                        <td class='gdvr' colspan='16' id='<%# Eval("BookingID")%><%# Eval("ProdID")%>' style='display: none; background-color: rgba(253, 174, 2, 0.478431);'></td>
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
                var strHtml = "<div class='destination_tab'><div class='col-md-12'><ul>" +
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
                    "<div id='divFare" + BID + PID + "6' style='display: none;'></div></div></div>";
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
                    var strHtml = " <div class='row'>" +
                        "<div class='col-md-8'>" +

                        "<table width='100%' class='table' cellspacing='0' border='0' cellpadding='0'>" +
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

                        "<tr><td><b>IP-Address</b></td><td>" + jsdata.BD.IpAddress + "</td>" +
                        "<td><b>City</b></td><td>" + jsdata.BD.IpCity + "</td></tr>" +

                        "<tr><td><b>Country</b></td><td>" + jsdata.BD.IpCountry + "</td>" +
                        "<td></td><td></td></tr>" +


                        "<tr><td><b>Remarks</b></td><td colspan='6'>" + jsdata.BD.BookingRemarks + "</td>" +
                        "</td>" +
                        "</tr></table>" +
                        "</div>" +
                        "<div class='col-md-4'>" +
                        "<table width='100%' class='table' cellspacing='0' border='0' cellpadding='0'>" +
                        "<tr>" +

                        "<td><a class='btnbookins'   href='BookingEditDetails.aspx?BID=" + jsdata.BM.BookingID + "&PID=" + jsdata.BD.ProdID + "'>Edit Booking Details</a></td>" +
                        "</tr>" +
                        " <tr>" +
                        "<td><a class='btnbookins' href='reciept.aspx?BID=" + jsdata.BM.BookingID + "&PID=" + jsdata.BD.ProdID + "'>Itinerary/E-Ticket</a></td>" +
                        "</tr>" +
                        "<tr>" +

                        "<td><a class='btnbookins' href='atol_reciept.aspx?BID=" + jsdata.BM.BookingID + "&PID=" + jsdata.BD.ProdID + "'>Atol Reciept</a></td>" +
                        "</tr>" +

                        //"<tr>" +
                        //    "<td>" +
                        //        "<a href='sendMail.aspx?BID=" + jsdata.BM.BookingID + "&PID=" + jsdata.BD.ProdID + "'>Send Mail</a>" +
                        //        "<a href='sendMailReminder.aspx?BID=" + jsdata.BM.BookingID + "&PID=" + jsdata.BD.ProdID + "'>Alert Mail</a>" +
                        //     "</td>" +
                        //"</tr>" +

                        //added by Dinesh as per Gogrge sir



                        " <tr>" +
                        "<td>" +
                        "<a class='btnbookins'  href='addHotel.aspx?BID=" + jsdata.BM.BookingID + "&PID=" + jsdata.BD.ProdID + "'>Add Hotel</a>" +

                        "</td>" +

                        "</tr>" +
                        " <tr>" +
                        "<td>" +
                        "<a href='InvoiceSupplier.aspx?BID=" + jsdata.BM.BookingID + "&PID=" + jsdata.BD.ProdID + "&CMN=<%= HiddenField1.Value %>'><%=UserName.ToLower()=="pankaj".ToLower() || UserName.ToLower()=="aryan".ToLower()?"Supplier Invoice":"" %></a>&nbsp;&nbsp;" +
                            "<input type='text' onkeyup='myFunction(this.value)' style='width:50px;' name='txtCMoney'>" +
                            "</td>" +

                            "</tr>" +


                            "<tr>" +
                            "<td>" +
                            "<select id='selectbox'  style='width:126px;height: 26px;padding: 3px;color: red;' onchange='OpenRedirect(this.value)'>" +
                            "<option value='-1'>-Select Mail Option-</option>" +
                            //Remove Call Back as per pankaj 05 March 2021 by Dinesh // Send Mail
                            "<option value='/admin/sendMail.aspx/?BID=" + jsdata.BM.BookingID + "&PID=" + jsdata.BD.ProdID + "'>Send Mail</option>" +
                            "<option value='/admin/sendMailReminder.aspx/?BID=" + jsdata.BM.BookingID + "&PID=" + jsdata.BD.ProdID + "&T=0'>Alert E-Mail</option>" +
                            "<option value='/admin/sendMailReminder.aspx/?BID=" + jsdata.BM.BookingID + "&PID=" + jsdata.BD.ProdID + "&T=1'>Flight Schedule Change</option>" +
                            "<option value='/admin/sendMailReminder.aspx/?BID=" + jsdata.BM.BookingID + "&PID=" + jsdata.BD.ProdID + "&T=2'>Flight Schedule Change</option>" +
                            "<option value='/admin/sendMailReminder.aspx/?BID=" + jsdata.BM.BookingID + "&PID=" + jsdata.BD.ProdID + "&T=3'>Flight Schedule Change</option>" +
                            "<option value='/admin/sendMailReminder.aspx/?BID=" + jsdata.BM.BookingID + "&PID=" + jsdata.BD.ProdID + "&T=4'>Urgent Call-back</option>" +
                            "</select> " +
                            "</td>" +
                            "</tr>" +
                            //"<tr>"+
                            //    "<td>"+
                            //        "<a href='addTransfer.aspx?BID="+  jsdata.BM.BookingID  +"&PID="+  jsdata.BD.ProdID  +"'>Add Transfer</a>"+

                            //    "</td>"+
                            //"</tr>"+
                            //"<tr>"+
                            //    "<td>"+
                            //        "<a href='addTrain.aspx?BID="+  jsdata.BM.BookingID  +"&PID="+  jsdata.BD.ProdID  +"'>Add Train</a>"+
                            //   " </td>"+
                            //"</tr>"+
                            "</table>" +
                            "</div>";
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
                        "<td class='gdvh'>M Name</td>" +
                        "<td class='gdvh'>Last Name</td>" +
                        "<td class='gdvh'>DOB</td>" +
                        "<td class='gdvh'>Gender</td>" +
                        "<td class='gdvh'>E-Ticket</td></tr>";
                    $.each(jsdata.PD, function (key, value) {
                        strHtml += "<tr><td class='gdvr'>" + (key + 1) + "</td>" +
                            "<td class='gdvr'>" + value.PaxType.toUpperCase() + "</td>" +
                            "<td class='gdvr'>" + value.Title.toUpperCase() + "</td>" +
                            "<td class='gdvr'>" + value.FName.toUpperCase() + "</td>" +
                            "<td class='gdvr'>" + value.MName.toUpperCase() + "</td>" +
                            "<td class='gdvr'>" + value.LName.toUpperCase() + "</td>" +
                            "<td class='gdvr'>" + value.DOB + "</td>" +
                            "<td class='gdvr'>" + value.PaxSex + "</td>" +
                            "<td class='gdvr'>" + value.Tickets + "</td></tr>";
                    });
                    if (jsdata.length == 0)
                        strHtml += "<tr><td class='gdvr' colspan='8'>No any Pax!!</td></tr>";
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
                        "<td class='gdvh'>Cabin</td>" +
                        "<td class='gdvh'>TFrom</td>" +
                        "<td class='gdvh'>TTo</td>" +
                        "<td class='gdvh'>Status</td>" +
                        "<td class='gdvh'>Airline Ref No.</td></tr>";
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
                            "<td class='gdvr'>" + GetCabinClassChangeValue(value.CabinClass) + "</td>" +
                            "<td class='gdvr'>" + value.TerminalFrom + "</td>" +
                            "<td class='gdvr'>" + value.TerminalTo + "</td>" +
                            "<td class='gdvr'>" + value.FStatus + "</td>" +
                            "<td class='gdvr'>" + value.AirlineConfirmationCode + "</td></tr>";

                    });
                    if (jsdata.length == 0)
                        strHtml += "<tr><td class='gdvr' colspan='12'>No any Sectors!!</td></tr>";
                    $("#divFare" + BID + PID + ID).html(strHtml + "</table>");
                },
                error: function (data) { }
            });
            waitingDialog.hide();
        }

        function GetCabinClassChangeValue(value) {
            var result = "";
            if (value != "" && value != null) {
                if (value.toLowerCase() == "premium") {
                    result = "Premium Economy";
                }
                else {
                    result = value;
                }
            }
            else {
                result = "Economy";
            }
            return result;
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
        function myFunction(val) {
            document.getElementById('<%=HiddenField1.ClientID %>').value = val;
        }




    </script>

    <script type="text/javascript">
        function OpenRedirect(val) {
            if (val == "-1") {
                return false
            }
            else {
                window.open(val, '_blank');
            }
        };
    </script>
</asp:Content>


