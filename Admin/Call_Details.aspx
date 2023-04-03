<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="Call_Details.aspx.cs" Inherits="Admin_Call_Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
  <%--  <link href="../css/StyleSheet.css" rel="stylesheet" />--%>
    <link href="../../css/StyleSheet.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
   
    <script src="//ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js"
        type="text/javascript"></script>
    <link href="//ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css"
        rel="Stylesheet" type="text/css" />
    <script>
        $(document).ready(function () {
            $('#<%=txtOBDate.ClientID %>').datepicker({
                minDate: 0,
                maxDate: "+360D",
                numberOfMonths: 2,
                dateFormat: "dd/mm/yy",
                onSelect: function (selected) {
                    $('#<%=txtIBDate.ClientID %>').datepicker("option", "minDate", selected)
                }
            });
            $('#<%=txtIBDate.ClientID %>').datepicker({
                minDate: 0,
                maxDate: "+360D",
                numberOfMonths: 2,
                dateFormat: "dd/mm/yy",
                onSelect: function (selected) {
                    $('#<%=txtOBDate.ClientID %>').datepicker("option", "maxDate", selected)
                }
            });
            $('#<%=txtCreationDate.ClientID %>').datepicker({ dateFormat: "dd/mm/yy" });
            $('#<%=txtCreationdateTo.ClientID %>').datepicker({ dateFormat: "dd/mm/yy" });
        });
    </script>


    <div class="container-fluid">
        <div style="height: 15px;"></div>
        <div class="panel panel-default">
            <div class="panel-heading">Calls/Enquiries</div>
            <div class="panel-body" style="line-height: 34px;">
                <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="usr">Reason:</label>
                                <select class="form-control" id="ddlReason" runat="server">
                                    <option>New Inquiry</option>
                                    <option>Special Request</option>
                                    <option>Refund</option>
                                    <option>Cancellation</option>
                                    <option>Call Transfer</option>
                                    <option>Amendment</option>
                                    <option>Complain</option>
                                    <option>Customer Service</option>
                                    <option>Others</option>
                                </select>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="usr">Pax Name:<span class="required">*</span></label>
                                <input type="text" class="form-control" id="txtPaxName" runat="server" />
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="row">
                                <div class="form-group">
                                    <div class="col-md-4">
                                        <label for="usr">ADT:</label>
                                        <input type="number" value="1" min="0" max="10" class="form-control" id="txtAdults" runat="server" data-bind="value: replyNumber" />
                                    </div>
                                    <div class="col-md-4">
                                        <label for="usr">CHD:</label>
                                        <input type="number" value="0" min="0" max="10" class="form-control" id="txtChilds" runat="server" data-bind="value: replyNumber" />
                                    </div>
                                    <div class="col-md-4">
                                        <label for="usr">INF:</label>
                                        <input type="number" value="0" min="0" max="5" class="form-control" id="Infants" runat="server" data-bind="value: replyNumber" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="usr">Contact No:<span class="required">*</span></label>
                                <input type="number" class="form-control" id="txtContact" min="0" runat="server" />
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="usr">Email ID:</label>
                                <input type="email" class="form-control" id="txtEmail" runat="server" />
                            </div>
                        </div>
                        <div class="col-md-3">
                                <div class="form-group">
                                    <label for="usr">Type</label>
                                    <select class="form-control" id="ddlType" runat="server">
                                        <option selected="selected" value="CL">Call</option>
                                        <option value="EN">Enquiry</option>
                                    </select>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label for="usr">Airline:</label>
                                    <input type="text" class="form-control" maxlength="2" id="txtAirline" runat="server" />
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label for="usr">Agent</label>
                                    <input type="text" class="form-control" id="txtAgent" runat="server" />
                                </div>
                            </div>
                    </div>

                </div>
                <div class="row">
                    <div class="col-md-12">


                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="usr">Origin:<span class="required">*</span></label>
                                <input type="text" class="form-control" maxlength="3" id="txtOrigin" runat="server" />
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="usr">Destination:<span class="required">*</span></label>
                                <input type="text" class="form-control" maxlength="3" id="txtDestination" runat="server" />
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="usr">Outbound Date:<span class="required">*</span></label>
                                <input type="text" class="form-control" id="txtOBDate" runat="server" />
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="usr">Inbound Date:</label>
                                <input type="text" class="form-control" id="txtIBDate" runat="server" />
                            </div>
                        </div>
                    </div>

                </div>
                <div class="row">
                    <div class="col-md-12">

                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="usr">Brand Name:<span class="required">*</span></label>
                                <asp:DropDownList CssClass="form-control" ID="ddlBrand" runat="server" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlBrand_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="usr">Campaign Name:<span class="required">*</span></label>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList CssClass="form-control" ID="ddlSourceMedia" runat="server">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                         <div class="col-md-3">
                            <div class="form-group">
                                <label for="usr">Status:<span class="required">*</span></label>
                                <select class="form-control" id="ddlStatus" runat="server">
                                    <option>Select Status</option>
                                    <option>Booked</option>
                                    <option>Follow Up</option>
                                    <option>Resolved</option>
                                </select>
                            </div>
                        </div>
                        

                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="usr">Medium:</label>
                                <select class="form-control" id="ddlSource" runat="server">
                                    <option>Phone</option>
                                    <option>Inquiry mail</option>
                                    <option>Chat</option>
                                    <option>Facebook</option>
                                    <option>Others</option>
                                </select>
                            </div>
                        </div>
                    </div>
                </div>


                <div class="row">
                    <div class="col-md-12">
                       

                        <div class="col-md-3" id="dvBookingId" style="display: none">
                            <div class="form-group">
                                <label for="usr">Booking ID:</label>
                                <input type="text" class="form-control" id="bookingID" runat="server" />
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="usr">Creation Date:</label>
                                <input type="text" class="form-control" id="txtCreationDate" runat="server" />
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="usr">Creation Date To:</label>
                                <input type="text" class="form-control" id="txtCreationdateTo" runat="server" />
                            </div>
                        </div>
                    </div>

</div>
                    <div class="row">
                        <div class="col-md-12">
                            
                        </div>
                    </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label for="usr">Remarks:<span class="required">*</span></label>
                                <textarea type="text" class="form-control" id="txtRemarks" runat="server"></textarea>
                            </div>
                        </div>
                    </div>
                
                </div>

                </div>

                <asp:HiddenField ID="hfID" runat="server" />

                <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="usr"></label>

                                <asp:Button ID="btnInsert" CssClass="btn btn-primary btn-block" OnClientClick="return Validate()" runat="server" Text="Insert"
                                    OnClick="btnInsert_Click" />
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="usr"></label>
                                <asp:Button ID="btnSearch" CssClass="btn btn-primary btn-block" runat="server" Text="Search"
                                    OnClick="btnSearch_Click" />

                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>



    <div class="panel panel-default">
        <div class="panel-heading">
            Call/Enquiry Details&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Calls - <%=UniqueCalls %> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnExport" runat="server" Text="Export"
                OnClick="btnExport_Click" Style="background-color: #2b303c;" Visible="false" />
        </div>
        <div class="panel-body" style="line-height: 34px; padding: 0px!important;">
            <asp:Repeater ID="rptrDetails" runat="server">
                <HeaderTemplate>
                    <div class="table-responsive">
                        <table class="table table-striped  table-hover table-bordered">
                            <thead>
                                <tr>
                                    <th class="gdvh">S#</th>
                                    <th class="gdvh">CallRef</th>
                                    <th class="gdvh">Bok Ref</th>
                                    <th class="gdvh">Pax Name</th>
                                    <th class="gdvh">Contact</th>
                                    <th class="gdvh">Destination</th>
                                    <th class="gdvh">ADT</th>
                                    <th class="gdvh">CHD</th>
                                    <th class="gdvh">INF</th>
                                    <th class="gdvh">Airline</th>

                                    <th class="gdvh">Brand</th>
                                    <th class="gdvh">Source Media</th>
                                    <th class="gdvh">Status</th>
                                    <th class="gdvh">Agent</th>
                                    <th class="gdvh">DateTime</th>


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
                                <span onclick="ShowCallDetails(&#39;<%# Eval("Call_Ref")%>&#39;, &#39;<%# Eval("Brand_Name")%>&#39;)"><%# Eval("Call_Ref")%></span>
                            </td>
                            <td class='gdvr'><%# Eval("BookingId")%></td>
                            <td class='gdvr'><%# Eval("Pax_Name")%></td>

                            <td class='gdvr'><%# Eval("Contact_Number")%></td>
                            <td class='gdvr'><%# Eval("Destination")%></td>
                            <td class='gdvr'><%# Eval("Adults")%></td>
                            <td class='gdvr'><%# Eval("Childs")%></td>
                            <td class='gdvr'><%# Eval("Infants")%></td>
                            <td class='gdvr'><%# Eval("Airline")%></td>
                            <td class='gdvr'><%# Eval("Brand_Name")%></td>
                            <td class='gdvr'><%# Eval("Source_Media_Name")%></td>
                            <td class='gdvr'><%# Eval("Status")%></td>
                            <td class='gdvr'><%# Eval("Agent_Name")%></td>
                            <td class='gdvr'><%# Eval("Create_Date")%></td>


                        </tr>
                        <tr>
                            <td class='gdvr2' colspan='13'><%# Eval("Remarks")%></td>
                            <td class='gdvr2' colspan='2' style="text-align: center">
                                <a runat='server' target="_blank" href='<%# Eval("Call_Ref", "~/Admin/editcalldetails.aspx?CID={0}" ) %>'>Edit</a>


                            </td>
                        </tr>
                        <tr>
                            <td class='gdvr' colspan='13' id='<%# Eval("Call_Ref")%>' style='display: none; background-color: rgba(253, 174, 2, 0.478431);'></td>
                        </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                    </div>
                </FooterTemplate>
            </asp:Repeater>

        </div>
    </div>

    <asp:HiddenField ID="hfUpdatedBy" runat="server" />

    <style>
        .required 
        {
            color: red;
            font-size:15px!important;
        }
    </style>


    <link href="../Content/sweetalert/sweet-alert.css" rel="stylesheet" />
    <script src="../Content/sweetalert/sweetalert.min.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            $("#dvBookingId").hide();
        });

        function popup(divProgressBar, width, height) {
            try {
                var height1 = $(window).height();
                var width1 = $(window).width();
                $('#' + divProgressBar).height(height + "%");
                $('#' + divProgressBar).width(width + "%");
                $('#' + divProgressBar).css({ top: ((height1 - ((height1 * parseInt(height)) / 100)) / 2).toFixed(0) + "px", left: ((width1 - ((width1 * parseInt(width)) / 100)) / 2).toFixed(0) + "px" });

                $('#fadebackground').height(height1 + "px");
                $('#fadebackground').width(width1 + "px");
                $('#fadebackground').toggle();
                $('#' + divProgressBar).toggle();
                return false;
            }
            catch (e) { return false; }
        }
        function SearchCall() {
            popup('popProgressBar', 30, 30);
            return true;
        }
        function ShowCallDetails(BID, brand) {

            if ($("#" + BID).html() == "") {
                var strHtml = "<div class='destination_tab'><ul>" +
                    "<li class='first-li12' id='li" + BID + "1' onclick=\"ShowHideDestTab('1','" + BID + "');\">Call Details</li>" +
                    "</ul></div><div style='clear:both;'></div>" +
                    "<div class='h-ct-midd-container'>" +
                    "<div id='divFare" + BID + "1' style='display: none;'></div>" +

                    "</div>";
                $("#" + BID).html(strHtml);
                ShowHideDestTab("1", BID, brand);
            }
            $("#" + BID).toggle();

        }


        function ShowHideDestTab(ID, BID, brand) {

            for (var i = 1; i <= 1; i++) {
                $("#divFare" + BID + i).hide();
                document.getElementById("li" + BID + i).className = '';
            }
            $("#divFare" + BID + ID).show();
            document.getElementById("li" + BID + ID).className = 'first-li12';
            if ($("#divFare" + BID + ID).html() == "") {
                switch (ID) {
                    case "1":
                        GetCallSummary(ID, BID, brand);
                        break;
                    case "2":
                        GetPassengerDetails(ID, BID);
                        break;
                    case "3":
                        GetSectorDetails(ID, BID);
                        break;

                    default: break;
                }
            }
        }

        function GetCallSummary(ID, BID, brand) {

            $.ajax({
                type: "POST",
                url: "Call_Details.aspx/GetCallSummary",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify({ CallRef: BID, Brand: brand }),
                responseType: "json",
                success: function (data) {
                    var jsdata = JSON.parse(data.d);
                    var strHtml = "<table width='100%' class='table' cellspacing='0' border='0' cellpadding='0'>" +
                        "<tr><td><b> Call Ref</b></td><td>" + jsdata[0].Call_Ref + "</td>" +
                        "<td><b>Pax Name</b></td><td>" + jsdata[0].Pax_Name + "</td></tr>" +
                        "<tr><td><b>Origin</b></td><td>" + jsdata[0].Origin + "</td>" +
                        "<td><b>Destination</b></td><td>" + jsdata[0].Destination + "</td></tr>" +
                        "<tr><td><b>Out-Bound</b></td><td>" + jsdata[0].Outbound_Date + "</td>" +
                        "<td><b>In-Bound</b></td><td>" + jsdata[0].Return_Date + "</td></tr>" +
                        "<tr><td><b>Number of Pax</b></td><td>" + (jsdata[0].Adults + jsdata[0].Childs + jsdata[0].Infants) + "</td>" +
                        "<td><b>Airline</b></td><td>" + jsdata[0].Airline + "</td></tr>" +
                        "<tr><td><b>Call of Reason</b></td><td>" + jsdata[0].Reason_of_Call + "</td>" +
                        "<td><b>Status</b></td><td>" + jsdata[0].Status + "</td></tr>" +
                        "<tr><td><b>Brand Name</b></td><td>" + jsdata[0].Brand_Name + "</td>" +
                        "<td><b>Creation Date</b></td><td>" + jsdata[0].Create_Date + "</td></tr>" +
                        "<tr><td><b>Agent</b></td><td>" + jsdata[0].Agent_Name + "</td><td><b>Call Source</b></td><td>" + jsdata[0].Call_Source + "</td></tr>" +
                        "<tr><td><b>Remarks</b></td><td colspan='2'>" + jsdata[0].Remarks + "</td>" +
                        "<td><a target='_blank' href='fltdeeplink.aspx?org=" + jsdata[0].Origin + "&dest=" + jsdata[0].Destination + "&departDate=" + jsdata[0].Outbound_Date + "&returnDate=" + jsdata[0].Return_Date + "&adt=" + jsdata[0].Adults + "&chd=" + jsdata[0].Childs + "&inf=" + jsdata[0].Infants + "&classType=0&airline=" + jsdata[0].Airline + "&JType=2&DFlights=false&isFlx=false&campaign=BKOFFICE&company=BKOFFICE'><b>Search Now</b></a>  &nbsp;&nbsp;&nbsp;<a href='../Admin/FltBooking/makeBooking.aspx'>Create</a> &nbsp;&nbsp;&nbsp;<a href='FlightQuote.aspx'>Send Quotation</a></tr>" +
                        "</table>";
                    $("#divFare" + BID + ID).html(strHtml);
                },
                error: function (data) { }
            });
        }


        function Validate()
        {

            //Customer Service
            var reason = document.getElementById('<%= ddlReason.ClientID %>').value;
            if (reason != "Customer Service")
            {
                var origin = document.getElementById('<%= txtOrigin.ClientID %>').value;
                if (origin == "") {
                    swal({
                        icon: "error",
                        text: "Enter Origin"
                    });
                    document.getElementById('<%= txtOrigin.ClientID %>').focus();
                    return false;
                }
                var destination = document.getElementById('<%= txtDestination.ClientID %>').value;
                if (destination == "")
                {
                    swal({
                        icon: "error",
                        text: "Enter Destination"
                    });
                    document.getElementById('<%= txtDestination.ClientID %>').focus();
                    return false;
                }
                var txtOBDate = document.getElementById('<%= txtOBDate.ClientID %>').value;
                if (txtOBDate == "")
                {
                    swal({
                        icon: "error",
                        text: "Enter Outbound Date"
                    });
                        document.getElementById('<%= txtOBDate.ClientID %>').focus();
                        return false;
                    }

            }

            var paxName = document.getElementById('<%= txtPaxName.ClientID %>').value;
            if (paxName == "")
            {
                swal({
                    icon: "error",
                    text: "Enter Pax Name"
                });
                document.getElementById('<%= txtPaxName.ClientID %>').focus();
                return false;
            }
            var contactNo = document.getElementById('<%= txtContact.ClientID %>').value;
            if (contactNo == "")
            {
                swal({
                    icon: "error",
                    text: "Enter Contact Number"
                });
                document.getElementById('<%= txtContact.ClientID %>').focus();
                return false;
            }
           
            var bname = document.getElementById('<%= ddlBrand.ClientID %>').value;
            if (bname == "" || bname == "--Select--")
            {
                swal({
                    icon: "error",
                    text: "Select Brand Name"
                });
                document.getElementById('<%= ddlBrand.ClientID %>').focus();
                return false;
            }

            var SourceMedia = document.getElementById('<%= ddlSourceMedia.ClientID %>').value;
            if (SourceMedia == "" || SourceMedia == "Select Campaign")
            {
                swal({
                    icon: "error",
                    text: "Select Campaign Name"
                });
                document.getElementById('<%= ddlSourceMedia.ClientID %>').focus();
                return false;
            }

            var statusVal = document.getElementById('<%= ddlStatus.ClientID %>').value;
            if (statusVal == "" || statusVal == "Select Status")
            {
                swal({
                    icon: "error",
                    text: "Select Call Status"
                });
                document.getElementById('<%= ddlStatus.ClientID %>').focus();
                return false;
            }
            var remarks = document.getElementById('<%= txtRemarks.ClientID %>').value;
            if (remarks == "")
            {
                swal({
                    icon: "error",
                    text: "Enter Remarks"
                });
                document.getElementById('<%= txtRemarks.ClientID %>').focus();
                return false;
            }
          

            var ddlValue = $('#<%= ddlStatus.ClientID %>').val();
            if (ddlValue == "Booked") {
                var bookingId = document.getElementById('<%= bookingID.ClientID %>').value;
                if (bookingId == "")
                {
                    swal({
                        icon: "error",
                        text: "Enter Booking ID"
                    });
                    document.getElementById('<%= bookingID.ClientID %>').focus();
                    return false;
                }
            }
        }




        $('#<%= ddlStatus.ClientID %>').change(function () {
            var ddlValue = $('#<%= ddlStatus.ClientID %>').val();
            if (ddlValue == "Booked") {
                $("#dvBookingId").show();
            }
            else {
                $("#dvBookingId").hide();
            }
        });

    </script>
</asp:Content>

