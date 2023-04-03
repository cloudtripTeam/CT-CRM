<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/AdminMasterPage.master" CodeFile="BookingDetails.aspx.cs" Inherits="Admin_BookingDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../../css/StyleSheet.css" rel="stylesheet" />
    <script src="../../js/CalendarAnyYear.js"></script>
    
  
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
            width: 100%;
            text-align: center;
            font-size: 11px !important;
            padding-right: 0px !important;
        }
        .panel-group .panel
        {
            overflow:inherit;
        }
          .btndoc {
           background-color: #4fc3a1 !important;
    border: green 1px solid;
    /* padding: 2px 3px; */
    border-radius: 5px;
    display: inline-block;
    width: 100%;
    text-align: center;
    /* font-size: 11px !important; */
   
        }
    </style>

    <style>
        
    .fl-table tbody tr:nth-child(odd) {
        background: none;
    }
     .fl-table tbody tr:first-child td {
        background-color: #adf0ff;
    }
    .fl-table tr:nth-child(even) {
        background: transparent;
    }
    .fl-table tr td:nth-child(odd) {
        
        border-right: 1px solid #E6E4E4;
    }
    .fl-table tr td:nth-child(even) {
        border-right: 1px solid #E6E4E4;
    }
   
        .fl-table {
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
        .fl-table thead th {
    color: #ffffff;
    background: #4FC3A1;
}


.fl-table thead th:nth-child(odd) {
    color: #ffffff;
    background: #00366c;
}

.fl-table tr:nth-child(even) {
    background: #F8F8F8;
}
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
         .blink_me tr
        {
            animation: blinker 1s linear infinite;
           color:red;
        }
        @keyframes blinker 
        {
            50% {
                opacity: 0;
            }
        }

          .blink_me_charge
        {
            animation: blinker 1s linear infinite;
             color:red;
             font-size:14px;
             background:rgba(253, 174, 2, 0.48);
             border-radius:25px;
             padding:5px 10px;
             text-align:center;
             font-weight:bold;
        }
        @keyframes blinker 
        {
            50% {
                opacity: 0;
            }
        }
         .blink_me_charge tr
        {
            animation: blinker 1s linear infinite;
          
        }
        @keyframes blinker 
        {
            50% {
                opacity: 0;
            }
        }
      

        .fl1-table tbody tr:nth-child(odd) {
        background: none;
    }
     .fl1-table tbody tr:first-child td {
        background-color: none;
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
    background: none;
}
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
         .blink_me tr
        {
            animation: blinker 1s linear infinite;
           color:red;
        }
        @keyframes blinker 
        {
            50% {
                opacity: 0;
            }
        }

          .blink_me_charge
        {
            animation: blinker 1s linear infinite;
             color:red;
             font-size:14px;
             background:rgba(253, 174, 2, 0.48);
             border-radius:25px;
             padding:5px 10px;
             text-align:center;
             font-weight:bold;
        }
        @keyframes blinker 
        {
            50% {
                opacity: 0;
            }
        }
         .blink_me_charge tr
        {
            animation: blinker 1s linear infinite;
          
        }
        @keyframes blinker 
        {
            50% {
                opacity: 0;
            }
        }
      .btn-group > .btn:first-child
      {
          width:100%;
      }
       .btn-group
      {
          width:100%;
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
                           <asp:Table ID="drpXp" ClientIDMode="Static"  runat="server"></asp:Table>
                        </div>
                        
                        <div class="col-md-2">
                            <asp:TextBox class="form-control mb-10" ID="txtFromDate" onclick="showCalender(this);" runat="server" AutoComplete="off" placeholder="From Booking Date" />
                        </div>
                        <div class="col-md-2">
                            <asp:TextBox class="form-control mb-10" ID="txtToDate" onclick="showCalender(this);" runat="server" AutoComplete="off" placeholder="To Booking Date" />
                        </div>
                        <div class="col-md-2">
                            <asp:TextBox class="form-control mb-10" ID="txtJFromDate" onclick="showCalender(this);" AutoCompleteType="None" runat="server" placeholder="Departure Date" />
                        </div>
                        <div class="col-md-2">
                            <asp:TextBox class="form-control mb-10" ID="txtJToDate" onclick="showCalender(this);" runat="server" AutoCompleteType="None" placeholder="Return Date" />
                        </div>
                        <div class="col-md-2">
                            <asp:ListBox ID="ddlCompany" SelectionMode="Multiple" class="form-control mb-10" runat="server"></asp:ListBox>
                        </div>
                </div>

                    


                    <div class="row">                        
                        <div class="col-md-2" style="display:none">
                            <asp:DropDownList class="form-control" ID="ddlSourceMedia" runat="server">
                                <asp:ListItem Value="">ANY Source Media</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                                                                
                        <div class="col-md-2" style="display:none">>
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
                                <asp:ListItem Value="Pending">Pending</asp:ListItem>
                                <asp:ListItem Value="Payment_Decline">Payment Decline</asp:ListItem>
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
                       
                        
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <asp:TextBox ID="txtPNRConfirmation" class="form-control mb-10" runat="server" placeholder="PNR" />
                        </div>
                        
                        <div class="col-md-2">
                            <asp:DropDownList ID="ddlPaymentType" class="form-control mb-10" runat="server">
                                <asp:ListItem Value="">Payment Type</asp:ListItem>
                                <asp:ListItem Value="Full">Full Payment</asp:ListItem>
                                <asp:ListItem Value="Partial">Partial Payment</asp:ListItem>
                                <asp:ListItem Value="NoPayment">No Payment</asp:ListItem>
                            </asp:DropDownList>
                        </div>

                        <div class="col-md-2">
                            <asp:DropDownList ID="ddlBookingType" class="form-control mb-10" runat="server">
                                <asp:ListItem Value="">All Booking Type</asp:ListItem>
                                <asp:ListItem Value="DICT">Online</asp:ListItem>
                                <asp:ListItem Value="INTR">Offline</asp:ListItem>
                            </asp:DropDownList>
                        </div>

                        <div class="col-md-2">
                            <asp:CheckBox ID="chkSelf" Text="OWN REPORT/DATA" CssClass="btn-submit btn rerr" TextAlign="Left" runat="server" style="background-color: #4fc3a1;
    width: 100%;
    color: #fff;" />
                        </div>
                        <div class="col-md-2" style="display:none">
                            <asp:TextBox class="form-control mb-10" ID="txtTransRef" runat="server" placeholder="Transaction ref"></asp:TextBox>
                        </div>
                        
                        <div class="col-md-2" >
                            
                            <asp:TextBox class="form-control mb-10" style="display:none" ID="txtSupplierRef" runat="server" placeholder="Supplier ref"></asp:TextBox>
                        </div>
                         <div class="col-md-2" >
                          <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-sm" style="float:right!important" OnClientClick="return SearchBooking();"
                                Text="Search" OnClick="btnSearch_Click"></asp:Button>
                        </div>
                        
                        
                        </div>

                    <div class="row">
                        <div class="col-md-12">
                             <asp:Label ID="lblmessage" runat="server" Text="" ForeColor="Red"></asp:Label>
            <asp:Button ID="btnExport" runat="server" Text="Export"
                OnClick="btnExport_Click" Visible="false" CssClass="btn btn-success btn-xs" />
                       
                        
                             
                            
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>

    <script>

        $(function () {
            $('[data-toggle="tooltip"]').tooltip();
            $('[rel=popover]').popover();
        })
    </script>
    <!-- View Bookings -->
    <div class="clearfix"></div>
    <div class="panel" style="padding-top: 0px;">
        <div class="panel-body " style="border: 1px solid #ddd; padding: 0px!important;">
           
 
             <asp:Repeater ID="rptrDetails" runat="server">
                <HeaderTemplate>
                    <div class="table-responsive">
                        <table class="table table-striped  table-hover table-bordered fl1-table" style="font-size: 12px !important;">
                            <thead>
                                <tr>
                                    <th class="gdvh">S#</th>
                                    <th class="gdvh">Bok Ref</th>
                                    <th class="gdvh">PNR</th>
                                    <th class="gdvh">Dest</th>
                                    <th class="gdvh">Agent</th>
                                    <th class="gdvh">Company</th>
                                    <th class="gdvh">Source&nbsp;Media</th>
                                    <th class="gdvh">Provider</th>
                                    <th class="gdvh">Name</th>
                                    <th class="gdvh">Trv&nbsp;Date</th>
                                    <th class="gdvh">Payment</th>
                                    <th class="gdvh">Booking&nbsp;Status</th>
                                    <th class="gdvh">Book&nbsp;Mode</th>
                                    <th class="gdvh">Assignee</th>
                                    <th class="gdvh">Action</th>
                                    <%--<th class="gdvh"></th>--%>
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
                        <td class='gdvr'><%# Eval("PNR")%></td>
                        <td class='gdvr'><%# Eval("Destination")%></td>
                        <td class='gdvr'><%# Eval("BookingBy")%></td>
                       <td class='gdvr'><%# Eval("BookingByCompany")%></td>
                        <td class='gdvr'><%# Eval("SourceMedia")%></td>
                        <td class='gdvr'><%# Eval("Provider")%></td>
                        <td class='gdvr'><%# Eval("FName").ToString().ToUpper() %>&nbsp;<%#  Eval("LName").ToString().ToUpper() %></td>
                        <td class='gdvr'><%# Eval("TravelDate","{0:dd-MM-yy}")%></td>
                        <td class='gdvr'><%# (Convert.ToDecimal( Eval("Sell_Price")) <= Convert.ToDecimal(Eval("Trns_Amount"))) ? "Full Payment" : 
                                                 Convert.ToDecimal(Eval("Trns_Amount")) == 0 ? "No Payment" : (Convert.ToDecimal( Eval("Sell_Price")) > Convert.ToDecimal(Eval("Trns_Amount"))) ? "Partial Payment" : "" %></td>
                        <td class='gdvr'><%# Eval("BookingStatus")%></td>
                        <td class='gdvr'><%# Eval("BookingByType").ToString() == "INTR"? "<span style='color:blue'>OFFLINE</span>": "<span style='color:green'>ONLINE</span>" %>
                            <%# string.Concat("<a Target='_blank'  style='display:none' href='FlightQuote2.aspx?BID=", Common.EncryptString(Convert.ToString(Eval("BookingID")),""), "&PID=001'>Send&nbsp;Quote</a>") %>
                        </td>
                         <td class='gdvr' ><div class="tip" rel="popover" data-trigger="hover" data-content="<%# Eval("BookingRemarks")%>" data-original-title="Remarks"> <%# Eval("BookingAssigned").ToString()=="1"?"NA": Eval("BookingAssigned")%></div></td>
                        <td class='gdvr'>
                            <a runat='server' class='btnbook' target="_blank" href='<%# Eval("BookingID", "~/Admin/FltBooking/AmendBooking.aspx?BID={0}&PID=001" ) %>'>Edit</a>
                            <%# Eval("BookingStatus").ToString().ToUpper() == "QUEUE"  ? string.Concat("<a class='btnbook' href='SendToSupplier.aspx?BID=", Eval("BookingID") ,
                            "&PID=001'><span style='color:green'>Send&nbsp;to&nbsp;Supplier</span></a>") : string.Concat("<a Target='_blank' class='btnbook' href='FlightQuote.aspx?BID=", Eval("BookingID") , "&PID=001'>Send&nbsp;Quote</a>")   %>


                            <%# (Eval("BookingStatus").ToString().ToUpper() == "ISSUED" ||  Eval("BookingStatus").ToString().ToUpper() == "COMPLETED")  ? string.Concat("<a class='btnbook' href='send_eticket.aspx?BID=", Eval("BookingID") , "&PID=001'><span style='color:green'>Send Eticket</span></a>")  : ""%> <%# com.GetAtolLink( Eval("IssuedBy").ToString(),Eval("ATOL_Type").ToString(),Eval("BookingID").ToString(),"001") %>
                            <%# Eval("Invoice_No").ToString() !="" ? string.Concat("<a class='btnbook' href='Invoice.aspx?BID=", Eval("BookingID") , "&PID=001'><span style='color:green'>Invoice</span></a>") :  Eval("BookingStatus").ToString().ToUpper() != "DUPE" && Eval("BookingStatus").ToString().ToUpper() != "CANCELLED" && Eval("BookingStatus").ToString().ToUpper() != "INCOMPLETE" ? string.Concat("<a class='btnbook' href='Invoice.aspx?BID=", Eval("BookingID") , "&PID=001'><span style='color:green'>Confirmation</span></a>") : "" %> 
                       
                            

                        </td>

                        <%--<td><a runat='server' style="color: #0d9dd4; font-size: 12px;" target="_blank" href='<%# Eval("BookingID", "~/Admin/FollowUp.aspx?BID={0}&PID=001" ) %>'>Follow</a></td>--%>
                    </tr>
                   <%-- <tr>
                        <td class='gdvr2' colspan='16'><%# Eval("BookingRemarks")%></td>
                             
                    </tr>--%>
                    <tr>
                        <td class='gdvr' colspan='16' id='<%# Eval("BookingID")%><%# Eval("ProdID")%>' style='display: none; background-color: #8d8d8d7a'>




                        </td>
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
            debugger;
           
            //if ($("#" + BID + PID).html() == "") {
                if (1==1) {
                

                var strHtml = "<div class='destination_tab'><div class='col-md-2 row'><ul>" +
                    "<li class='first-li12' id='li" + BID + PID + "1' onclick=\"ShowHideDestTab('1','" + BID + "','" + PID + "');\">Summary</li>" +
                    "<li class='first-li12' id='li" + BID + PID + "2' onclick=\"ShowHideDestTab('2','" + BID + "','" + PID + "');\">Passengers</li>" +
                    "<li class='first-li12' id='li" + BID + PID + "3' onclick=\"ShowHideDestTab('3','" + BID + "','" + PID + "');\">Sectors</li>" +
                    "<li class='first-li12' id='li" + BID + PID + "4' onclick=\"ShowHideDestTab('4','" + BID + "','" + PID + "');\">Amount Details</li>" +
                    "<li class='first-li12' id='li" + BID + PID + "5' onclick=\"ShowHideDestTab('5','" + BID + "','" + PID + "');\">Transactions</li>" +
                    "<li class='first-li12' id='li" + BID + PID + "7' onclick=\"ShowHideDestTab('7','" + BID + "','" + PID + "');\">Hotels</li>" +
                    "<li class='first-li12' id='li" + BID + PID + "6' onclick=\"ShowHideDestTab('6','" + BID + "','" + PID + "');\">Remarks</li>" +
                    "<li class='first-li12' id='li" + BID + PID + "8' onclick=\"ShowHideDestTab('8','" + BID + "','" + PID + "');\">Auth Doc</li></ul></div>" +
                    "<div class='h-ct-midd-container col-md-10'>" +
                    "<div id='divFare" + BID + PID + "1' style='display: none;'></div>" +
                    "<div id='divFare" + BID + PID + "2' style='display: none;'></div>" +
                    "<div id='divFare" + BID + PID + "3' style='display: none;'></div>" +
                    "<div id='divFare" + BID + PID + "4' style='display: none;'></div>" +
                    "<div id='divFare" + BID + PID + "5' style='display: none;'></div>" +
                    "<div id='divFare" + BID + PID + "7' style='display: none;'></div>" +
                    "<div id='divFare" + BID + PID + "8' style='display: none;'></div>" +
                    "<div id='divFare" + BID + PID + "6' style='display: none;'></div></div></div>";
               

                $("#" + BID + PID).html(strHtml);
                ShowHideDestTab("1", BID, PID);
            }
            $("#" + BID + PID).toggle();

        }

        function ShowHideDestTab(ID, BID, PID) {
            
            for (var i = 1; i <= 8; i++) {
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
                    case "7":
                    
                        GetHotelDetails(ID, BID, PID);
                        break;
                    case "8":

                        GetAuthDetails(ID, BID, PID);
                        break;

                    default: break;
                }
            }
        }

        var uxip;
        
        function GetBookingSummary(ID, BID, PID)
        {
            
             uxip = BID;
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
                    debugger;
                    var ChStatus = "";

                    if (jsdata.BM.CBS != "Won") {
                        ChStatus = jsdata.BM.CBS;
                    }
                    else {
                        ChStatus = "";
                    }
                    var strHtml = "<div class='row'>" +
                        "<div class='col-md-10'>" +

                        "<table width='100%' class='table' cellspacing='0' border='0' cellpadding='0' style='table-layout: fixed;'>" +
                        "<tr><td><b> Booking ID</b></td><td>" + jsdata.BM.BookingID + "</td>" +
                        "<td><b>Currencey</b></td><td>" + jsdata.BM.CurrencyType + "</td></tr>" +
                        "<tr><td><b>PNR</b></td><td>" + jsdata.BD.PNR + "</td>" +
                        "<td><b>Product Type</b></td><td>" + jsdata.BD.ProductType + "</td></tr>" +
                        /*"<tr><td><b>Booking Status</b></td><td>" + jsdata.BD.BookingStatus + " " + ChStatus + "</td>" +*/
                        "<tr><td><b>Booking Status</b></td><td>" + jsdata.BD.BookingStatus + " " + "</td>" +
                        "<td><b>Cabin Class</b></td><td>" + jsdata.SM.CabinClass + "</td></tr>" +
                        "<tr><td><b>Plating Carrier</b></td><td>" + jsdata.SM.ValCarrier + "</td>" +
                        "<td><b>Company</b></td><td>" + jsdata.BM.BookingByCompany + "</td></tr>" +
                        "<tr><td><b>Origin</b></td><td>" + jsdata.SM.Origin + "</td>" +
                        "<td><b>Source Media</b></td><td>" + jsdata.BD.SourceMedia + "</td></tr>" +
                        "<tr><td><b>Destination</b></td><td>" + jsdata.SM.Destination + "</td>" +
                        "<td><b>Address</b></td><td>" + jsdata.CD.PAddress + "</td></tr>" +
                        "<tr><td><b>Trip Type</b></td><td>" + jsdata.SM.JType + "</td>" +
                        "<td><b>Email ID</b></td><td>" + jsdata.CD.EmailID + "</td></tr>" +
                        "<tr><td><b>Booking Type</b></td><td>" + jsdata.BD.BookingByType + "</td>" +
                        "<td><b>Phone No</b></td><td>" + jsdata.CD.PhoneNo + "</td></tr>" +
                        "<tr><td><b>Last Ticking Date</b></td><td>" + jsdata.SM.LastTktDate + "</td>" +
                        "<td><b>Mobile No</b></td><td>" + jsdata.CD.MobileNo + "</td></tr>" +
                        "<tr><td><b>Booking Date</b></td><td>" + jsdata.BD.BookingDateTime + "</td>" +
                        "<td><b>Booking By</b></td><td>" + jsdata.BD.BookingBy + "</td></tr>" +

                        "<tr><td><b>IP-Address</b></td><td>" + jsdata.BD.IpAddress + "</td>" +
                        "<td><b>City</b></td><td>" + jsdata.BD.IpCity + "</td></tr>" +
                        "<tr><td><b>Status</b></td><td>" + jsdata.BD.IpCity + "</td>" +
                        "<td><b>Country</b></td><td>" + jsdata.BD.IpCountry+ "</td></tr>" +
                      
                        "<tr><td><b>Remarks</b></td><td colspan='6'>" + jsdata.BD.BookingRemarks + "</td>" +
                        "</td>" +
                        "</tr></table>" +
                        "</div>" +
                        "<div class='col-md-2 buttonslist'>" +
                        "<ul>"+
                        " <li><a class='btnbookins' href='reciept.aspx?BID=" + jsdata.BM.BookingID + "&PID=" + jsdata.BD.ProdID + "'>Itinerary/E-Ticket</a></li>" +

                       

                       /* "<li><a class='btnbookins'  href='addHotel.aspx?BID=" + jsdata.BM.BookingID + "&PID=" + jsdata.BD.ProdID + "'>Add Hotel</a></li>" +*/

                       /* "<li><a class='btnbookins' href='DocuSignFile.aspx?BID=" + jsdata.BM.BookingID + "&PID=" + jsdata.BD.ProdID + "'>AuthDoc</a></li>" +*/

                       /* "<li><a class='btnbookins' target='_blank' href='ChargebackUploading.aspx?BID=" + jsdata.BM.BookingID + "&PID=" + jsdata.BD.ProdID + "'>Charge Back</a></li>" +*/

                       

                        "<li>" + "<select id='selectbox'  style='width:100%;height: 26px;padding: 3px;color: red;font-size:12px' onchange='OpenRedirect(this.value)'>" +
                        "<option value='-1'>Select Mail Option</option>" +
                        
                        "<option value='/admin/sendMail.aspx/?BID=" + jsdata.BM.BookingID + "&PID=" + jsdata.BD.ProdID + "'>Send Mail</option>" +
                        "<option value='/admin/sendeticket.aspx/?BID=" + jsdata.BM.BookingID + "&PID=" + jsdata.BD.ProdID + "'>Send E-ticket</option>" +
                        "<option value='/admin/sendMailReminder.aspx/?BID=" + jsdata.BM.BookingID + "&PID=" + jsdata.BD.ProdID + "&T=0'>Alert E-Mail</option>" +
                        "<option value='/admin/sendMailReminder.aspx/?BID=" + jsdata.BM.BookingID + "&PID=" + jsdata.BD.ProdID + "&T=1'>Flight Schedule Change</option>" +
                        "<option value='/admin/sendMailReminder.aspx/?BID=" + jsdata.BM.BookingID + "&PID=" + jsdata.BD.ProdID + "&T=4'>Urgent Call-back</option>"
                    "</select> " +"<li>" +

                        +"</ul > " +
                     
                        "</div>";

                   
                    $("#divFare" + BID + PID + ID).html(strHtml);
                
                    },
                error: function (data) { }

            });
            
            $(function () {
                var name = 'Welcome ' + ' <%= Session["UserName"] %>'
                        $('#lblusr').text(name)
            });

            waitingDialog.hide();     
            $.ajax({
                type: "POST",
                url: "BookingDetails.aspx/GetPersonDetails",
                data: '{name: "' + uxip + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response.d != '') {
                         var user = JSON.parse(response.d);                   
                        $('#xpidStatus').text("ChargeBack Uploaded");
                        $('#xpidStatus').show();
                    }
                    else {
                        $('#xpidStatus').hide();
                      
                    }
                },
                error: function (response) {
                    alert(response.responseText);
                }
            });
        }
        //$("#ddlHotel").change(function () {
        //   /* var end = this.value;*/
        //    BindHotel($('#ddlHotel').val(),"","","","");
        //});
        //$('#ddlHotel').on('change', function () {
        //    alert('here');
        //});
      
        function GetHotelDetails(ID, BID, PID) {
            $("#_bookingId").val(BID);
            $("#_ProductId").val(PID);
            waitingDialog.show('Please Wait...');
            var strHtml = "";
            $.ajax({
                type: "POST",
                url: "BookingDetails.aspx/GetHotelDetail",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify({ BookingID: BID, ProdID: PID }),
                responseType: "json",
                success: function (data) {
                    var jsdata = JSON.parse(data.d);
                    var _Row = 0;
                    var _JD = JSON.parse(jsdata._HD);
                    if (_JD.length != 0) {
                        BindHotel(_Row, _JD, BID, PID, ID)
                    }
                    else {
                        strHtml += "<table width='100%' cellpadding='0' cellspacing='0' class='table'><tr><td class='gdvr' colspan='8'>No any Hotel !!</td></tr>";
                        $("#divFare" + BID + PID + ID).html(strHtml + "</table>");
                    }
                   
                }
            });
            waitingDialog.hide('Please Wait...');
        };
        var _HJD;
        var _HBID;
        var _HPID;
        var _HID;
        function BindHotel(_Row, _HJD, _HBID, _HPID, _HID) {
            debugger;
            if (typeof (_HJD) === "undefined") {
               
            }
            else {
                _JD = _HJD;
                BID = _HBID;
                PID = _HPID;
                ID = _HID;
            }
           
            var _Str = "";
            _Str = '<table style="width:100%;background-color:white;padding-top:50px;>">';
            if (_JD.length > 1) {
                _Str = _Str + '<tr>';
                _Str = _Str + '<td style="width:100%;vertical-align:top;" colspan="2">';
                _Str = _Str + '<table style="width:100%;vertical-align:top">';
                _Str = _Str + '<tr>';
                _Str = _Str + '<td style="width:20%;" class="BorderHead"><b>Select Hotel</b></td>';
                _Str = _Str + '<td style="width:80%;vertical-align:top;">';
                //_Str = _Str + '<select id="ddlHotel" style="width:100%;height:30px">';
                //_Str = _Str + '<option>Select Hotel</option>';
                console.log(_JD);
                for (var x = 0; x < _JD.length; x++) {
                    /* _Str = _Str + '<option value="' + _JD[x].HTL_DTL_UniqId + '">' + _JD[x].HTL_DTL_Hotel_Name +'</option>';*/
                   /* _Str = _Str + '<option value="' + x + '">' + _JD[x].HTL_DTL_Hotel_Name + '</option>';*/
                    _Str = _Str + '  <a id="btnHotel" class="tdHead" onclick=BindHotel("' + x + '");>' + _JD[x].HTL_DTL_Hotel_Name + '</a>'
                }

               /* _Str = _Str + '</select>';*/

                _Str = _Str + '</td>';

                _Str = _Str + '</tr>';
                _Str = _Str + '</table>';
                _Str = _Str + '</td>';
                _Str = _Str + '</tr>';
            }
            _Str = _Str + '<tr>';
            _Str = _Str + '<td style="width:50%;vertical-align:top;">';
            _Str = _Str + '<table style="width:100%;vertical-align:top">';
            _Str = _Str + '<tr>';
            _Str = _Str + '<td style="width:40%;" class="BorderHead">Primary Contact:</td>';
            _Str = _Str + '<td style="width:60%;" class="Border">' + _JD[_Row].PrimaryContact + '</td>';
            _Str = _Str + '</tr>';
            _Str = _Str + '<tr>';
            _Str = _Str + '<td style="width:40%;" class="BorderHead">Hotel Name:</td>';
            _Str = _Str + '<td style="width:60%;" class="Border">' + _JD[_Row].HTL_DTL_Hotel_Name + '</td>';
            _Str = _Str + '</tr>';
            _Str = _Str + '<tr>';
            _Str = _Str + '<td style="width:40%;" class="BorderHead">Check In :</td>';
            _Str = _Str + '<td style="width:60%;" class="Border">' + _JD[_Row].CheckIn + '</td>';
            _Str = _Str + '</tr>';
            _Str = _Str + '<tr>';
            _Str = _Str + '<td style="width:40%;" class="BorderHead">Room Type / No Off Room  :</td>';
            _Str = _Str + '<td style="width:60%;" class="Border">' + _JD[_Row].HTL_DTL_RoomType + '/' + _JD[_Row].HTL_DTL_NoOffRoom + '</td>';
            _Str = _Str + '</tr>';
            _Str = _Str + '<tr>';
            _Str = _Str + '<td style="width:40%;" class="BorderHead">Meal type :</td>';
            _Str = _Str + '<td style="width:60%;" class="Border">' + _JD[_Row].MealType + '</td>';
            _Str = _Str + '</tr>';
            _Str = _Str + '<tr>';
            _Str = _Str + '<td style="width:40%;" class="BorderHead">Hotel PNR :</td>';
            _Str = _Str + '<td style="width:60%;" class="Border">' + _JD[_Row].HTL_DTL_Pnr + '</td>';
            _Str = _Str + '</tr>';
            _Str = _Str + '<tr>';
            _Str = _Str + '<td style="width:40%;" class="BorderHead">Hotel Supplier :</td>';
            _Str = _Str + '<td style="width:60%;" class="Border">' + _JD[_Row].HTL_DTL_Supplier + '</td>';
            _Str = _Str + '</tr>';
            _Str = _Str + '<tr>';
            _Str = _Str + '<td style="width:40%;" class="BorderHead">Address 1:</td>';
            _Str = _Str + '<td style="width:60%;" class="Border">' + _JD[_Row].HTL_DTL_Address1 + '</td>';
            _Str = _Str + '</tr>';
            _Str = _Str + '<tr>';
            _Str = _Str + '<td style="width:40%;" class="BorderHead">Address 2:</td>';
            _Str = _Str + '<td style="width:60%;" class="Border">' + _JD[_Row].HTL_DTL_Address2 + '</td>';
            _Str = _Str + '</tr>';
            _Str = _Str + '<tr>';
            _Str = _Str + '<td style="width:40%;" class="BorderHead">Postal :</td>';
            _Str = _Str + '<td style="width:60%;" class="Border">' + _JD[_Row].HTL_DTL_Postal + '</td>';
            _Str = _Str + '</tr>';
            _Str = _Str + '<tr>';
            _Str = _Str + '<td style="width:40%;" class="BorderHead">City :</td>';
            _Str = _Str + '<td style="width:60%;" class="Border">' + _JD[_Row].HTL_DTL_City + '</td>';
            _Str = _Str + '</tr>';
            _Str = _Str + '<tr>';
            _Str = _Str + '<td style="width:40%;" class="BorderHead">Country :</td>';
            _Str = _Str + '<td style="width:60%;" class="Border">' + _JD[_Row].HTL_DTL_Country + '</td>';
            _Str = _Str + '</tr>';
            _Str = _Str + '<tr>';
            _Str = _Str + '<td style="width:40%;" class="BorderHead">Telephone :</td>';
            _Str = _Str + '<td style="width:60%;" class="Border">' + _JD[_Row].HTL_DTL_Telephone + '</td>';
            _Str = _Str + '</tr>';
            _Str = _Str + '<tr>';
            _Str = _Str + '<td style="width:40%;" class="BorderHead">Email :</td>';
            _Str = _Str + '<td style="width:60%;" class="Border">' + _JD[_Row].HTL_DTL_EmailId + '</td>';
            _Str = _Str + '</tr>';
            _Str = _Str + '</table>';
            _Str = _Str + '</td>';
            _Str = _Str + '<td style="width:50%;vertical-align:top;">';
            _Str = _Str + '<table style="width:100%;height:100%">';
            _Str = _Str + '<tr>';
            _Str = _Str + '<td style="width:100%;height:25px;text-align:center" class="BorderHead"><b>Price BreakUp</b></td>';
            _Str = _Str + '</tr>';
            var _PB = JSON.parse(_JD[_Row].HTL_DTL_PricebreakUp);
            console.log(_PB);
            _Str = _Str + '<tr>';
            _Str = _Str + '<td style="width:100%;text-align:center;padding-left:5%;padding-top:1%">';
            _Str = _Str + '<table style="width:90%;">'
            _Str = _Str + '<tr>';
            _Str = _Str + '<td style="width:25%;" class="tdHead">Charge Type</td>';
            _Str = _Str + '<td style="width:25%;" class="tdHead">Charge For</td>';
            _Str = _Str + '<td style="width:25%;" class="tdHead">Sell Amount</td>';
            _Str = _Str + '<td style="width:25%;" class="tdHead">Cost Amount</td>';
            _Str = _Str + '</tr>';
            for (var r = 0; r < _PB.length; r++) {
                _Str = _Str + '<tr>';
                _Str = _Str + '<td style="width:25%;" class="Border">' + _PB[r].AMT_CHG_MST_Charge_ID + '</td>';
                _Str = _Str + '<td style="width:25%;" class="Border">' + _PB[r].AMT_CHG_DTL_Charges_For + '</td>';
                _Str = _Str + '<td style="width:25%;" class="Border">' + _PB[r].AMT_CHG_DTL_Sell_Price + '</td>';
                _Str = _Str + '<td style="width:25%;" class="Border">' + _PB[r].AMT_CHG_DTL_Cost_Price + '</td>';
                _Str = _Str + '</tr>';
            }
            _Str = _Str + '</table>';
            _Str = _Str + '</td>';
            _Str = _Str + '</tr>';
            _Str = _Str + '<tr>';
            _Str = _Str + '<td style="width:100%;height:25px;text-align:center" class="BorderHead"><a id="btnHotel" class="tdHead" onclick = DeleteHotel("' + _JD[_Row].HTL_DTL_UniqId + '","' + _JD[_Row].HTL_DTL_BookingID + '");>Delete Hotel</a ></td>';
            _Str = _Str + '</tr>';
            _Str = _Str + '</table>';
            _Str = _Str + '</td>';
            _Str = _Str + '</tr>';
            _Str = _Str + '</table>';
            
            $("#divFare" + BID + PID + ID).html(_Str);
            waitingDialog.hide();
          
        }
        function DeleteHotel(_UnidId, XPID) {
            debugger;
            var r = confirm("Are you sure to delete this Hotel?");
            if (r == true) {
                waitingDialog.show('Please Wait...');
                $.ajax({
                    type: "POST",
                    url: "BookingDetails.aspx/DeleteHotel",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({ _HotelUniqId: _UnidId, _XPID: XPID }),
                    responseType: "json",
                    success: function (data) {
                        debugger;
                        console.log(data);
                        GetHotelDetails("7", $("#_bookingId").val(), $("#_ProductId").val());
                    },
                    error: function (data) { }
                });
                waitingDialog.hide();
            } else {
                waitingDialog.hide();
            }
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
                    var strHtml = "<table width='100%' cellpadding='0' cellspacing='0' class='table fl-table' >" +
                        "<tr><td class='gdvh'>Sr No</td>" +
                        "<td class='gdvh'>Pax Type</td>" +
                        "<td class='gdvh'>Title</td>" +
                        "<td class='gdvh'>First Name</td>" +
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
                    var strHtml = "<table width='100%' cellpadding='0' cellspacing='0' class='table fl-table' >" +
                        "<tr><td class='gdvh'>Sr No</td>" +
                        "<td class='gdvh'>Carrier</td>" +
                        "<td class='gdvh'>Flight No</td>" +
                        "<td class='gdvh'>Class</td>" +
                        "<td class='gdvh'>Departure</td>" +
                        "<td class='gdvh'>Dep Date</td>" +
                        "<td class='gdvh'>Arrival</td>" +
                        "<td class='gdvh'>Arrival Date</td>" +
                        "<td class='gdvh'>Baggage</td>" +
                        "<td class='gdvh'>Cabin</td>" +
                        "<td class='gdvh'>Dep Term</td>" +
                        "<td class='gdvh'>Arr Term</td>" +
                        "<td class='gdvh'>Status</td>" +
                        "<td class='gdvh'>Airline Ref</td></tr>";
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
                    var strHtml = "<table width='100%' cellpadding='0' cellspacing='0' class='table fl-table' >" +
                        "<tr><td class='gdvh'>SrNo</td>" +
                        "<td class='gdvh'>Charge ID</td>" +
                        "<td class='gdvh'>Charges For</td>" +
                        "<td class='gdvh'>Sell Amount</td>" +
                        "<td class='gdvh'>Cost Amount</td>" +
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
                            "<td class='gdvf'>Profit: " + (SellPrice.toFixed(2) - CostPrice.toFixed(2)).toFixed(2) + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a class='btn btn-success btn-sm' target='_blank' href='payment/paynow.aspx?BID=" + BID + "'>Pay Now</a></td></tr>";
                        $("#TotalSellPrice").val(SellPrice.toFixed(2));
                    }
                    $("#divFare" + BID + PID + ID).html(strHtml + "</table>");
                },
                error: function (data) { }
            });


            waitingDialog.hide();
        }

        function GetTrnsDetails(ID, BID, PID) {
           // waitingDialog.show('Please Wait...');
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


                    var strHtml = "<table width='100%' cellpadding='0' cellspacing='0' class='table fl-table' >" +
                        "<tr><td class='gdvh'>Trns No</td>" +
                        "<td class='gdvh'>Trns Amount</td>" +
                        "<td class='gdvh'>CurrencyType</td>" +
                        "<td class='gdvh'>Pay ID</td>" +
                        "<td class='gdvh'>Trns Type</td>" +
                        "<td class='gdvh'>Status</td>" +
                        "<td class='gdvh'>DateTime</td>" +
                        "<td class='gdvh'>Trns By</td></tr>";
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
                    var strHtml = "<table width='100%' cellpadding='0' cellspacing='0' class='table fl-table' >" +
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

        function GetAuthDetails(ID, BID, PID) {
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
                    var strHtml = "<div style='background-color:#fff;min-height: 384px;display:inline-block'><div class='col-md-12'><br>" +
                        "<div class='col-md-2'><select id='selectboxforcharge'  style='margin-top:5px' class='form-control' >" +
                        "<option value='-1'>Select Supplier</option>" +

                        "<option value='ATH'>Global efare</option>" +
                        "<option value='ATX'>ATX</option>" +
                        "</select> " + "</div>" +

                        "<div class='col-md-2'><input type='text' id='AuthAirline' class='form-control' name='AuthAirline' placeholder='Airline/Supplier' style='margin-top:5px;width:100%' /></div>" +
                        "<div class='col-md-2'><input type='text' id='CardHolder' class='form-control' name='CardHolder' placeholder='Card Holder Name' style='margin-top:5px;width:100%' />" + "</div>" +
                        "<div class='col-md-2'><input type='number' id='CardNum' class='form-control' name='CardNum' placeholder='Last 4 Digit Card Number' style='margin-top:5px;width:100%' />" + "</div>" +

                        "<div class='col-md-2'><input type='text' id='Expiry' class='form-control' name='Expiry' placeholder='Expiry' style='margin-top:5px;width:100%' />" + "</div>" +


                        "<div class='col-md-2'><button type='button' class='form-control btndoc btn-success' style='margin-top:5px' value='/admin/DocuSignFile.aspx?BID=" + BID + "&PID=" + PID + "' onclick='OpenRedirectAuthDoc(this.value)' >Download Auth Doc</button></div></div></div>";

                        
                    if (jsdata.length == 0)
                        strHtml += "<tr><td class='gdvr' colspan='8'>No remark found</td></tr>";

                    $("#divFare" + BID + PID + ID).html(strHtml + "</table>");
                },
                error: function (data) { }
            });
            waitingDialog.hide();
        }

    </script>
    <input type="hidden" id="TotalSellPrice" name="TotalSellPrice" />

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
     <script type="text/javascript">
         function OpenRedirectAuthDoc(val) {
            
             if (val == "-1") {
                 return false
             }
             else {
                 
                 if ($('#selectboxforcharge').val() == '-1') {
                     alert("Select Payment Gateway");
                     return false;
                 }
                 else {
                     var charge = "&Auth=" + $('#selectboxforcharge').val();

                     val = val + charge ;
                 }
                 if ($("#TotalSellPrice").val().length > 0) {
                     val = val + "&SellCost=" + $("#TotalSellPrice").val();
                 }
                 else {
                     alert("Sell Cost Missing");
                     return false;
                 }
             
                 var airline = $("#AuthAirline").val();
             if (airline.length > 2) {
                 val = val + '&AirlineName=' + airline;
                 }
                 var CardHolder = $("#CardHolder").val();
                 if (CardHolder.length > 2) {
                     val = val + '&CardHolder=' + CardHolder;
                 }
                 
                 var CardNum = $("#CardNum").val();
                 if (CardNum.length > 2) {
                     val = val + '&CardNum=' + CardNum;
             }
                 else {

                     alert("Card Number Missing");
                     return false;
                 }

                 var Expiry = $("#Expiry").val();
                 if (Expiry.length > 2) {
                     val = val + '&Expiry=' + Expiry;
                 }
                 else {

                     alert("Expiry Missing");
                     return false;
                 }
                 //alert(val);

                 window.open(val, '_blank');
                 
                 return false;
             }
         };

     </script>  
    <style>
        .buttonslist ul li {
  border-radius: 4px 4px 0 0;
    color: #0e192d;
    float: left;
    font: 17px/21px 'Open Sans',Arial,sans-serif;
    list-style-type: none;
    margin: 0 5px 5px 5px;
    padding: 10px 10px;
    background: #e2e2e2;
    border-radius: 5px;
    width:100%;
}
    .BorderHead{
        border-style: solid;
        border-width:1px;
       height:20px;
        font-weight:bold;
        font-size:12px;
        padding-left:5px;
    }
    .tdHead{
        border-style: solid;
        border-width:1px;
       height:20px;
       background-color:bisque;
        font-weight:bold;
        font-size:12px;
        padding-left:5px;
    }
    .Border{
        border-style: solid;
        border-width:1px;
         padding-left:5px;
         font-size:12px;
    }
   
</style>
    <input type="hidden" id="_bookingId" />
    <input type="hidden" id="_ProductId" />
</asp:Content>


