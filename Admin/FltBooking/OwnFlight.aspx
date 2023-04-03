<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="OwnFlight.aspx.cs" Inherits="Admin_FltBooking_OwnFlight" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../../css/StyleSheet.css" rel="stylesheet" />    
    <script src="../../js/CalendarAnyYear.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <!-- Search Panel -->
    <div class="panel-group">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="pull-left">
                    <h4 class="panel-title">
                        <a data-toggle="collapse" href="#bookingdetails">Flight Itinerary Details</a>
                    </h4>
                </div>
                <div class="pull-right"><a data-toggle="collapse" href="#FlightItinerarydetails"><span class="glyphicon glyphicon-search" style="color:#fff;"></span></a></div>
                <div class="clearfix"></div>
            </div>

            <div id="FlightItinerarydetails" class="panel-collapse collapse">
                <div class="panel-body">                    
                    <div class="clearfix"></div>
                    <div class="row">
                        <div class="col-md-3">
                            <asp:TextBox class="form-control mb-10" ID="txtSSource" runat="server" placeholder="source"></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox class="form-control mb-10" ID="txtSDestination" runat="server" placeholder="destination"></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox class="form-control mb-10" ID="txtSOperator" runat="server" placeholder="airline"></asp:TextBox>
                        </div>

                        <div class="col-md-3">
                           
                            <asp:Button ID="btnISearch" runat="server" CssClass="btn btn-default btn-lg" OnClick="btnISearch_Click" Text="Search Now"></asp:Button>
                        </div>
                    </div>
                </div>                
            </div>
        </div>
    </div>





    <input id="setascurrdate" type="hidden" />
    <input id="hdeprdate" type="hidden" />
    <div class="container">
        <div style="height: 15px;"></div>
        <div class="panel panel-default">
            <div class="panel-heading">Flight Itinerary</div>
            <div class="panel-body" style="line-height: 34px;">
                <div class="row">
                    <div class="col-md-2">
                        <label>Source</label>

                        <asp:TextBox runat="server" ID="txtSource" MaxLength='3' placeholder="source Code" CssClass="form-control" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtSource" InitialValue="" ValidationGroup="makebooking" runat="server" ErrorMessage="provide depart"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-md-2">
                        <label>Destination</label>
                        <asp:TextBox runat="server" CssClass="form-control" MaxLength='3' ID="txtDestination" placeholder="Destination Code" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtDestination" InitialValue="" ValidationGroup="makebooking" runat="server" ErrorMessage="provide destination"></asp:RequiredFieldValidator>


                    </div>
                    <div class="col-md-1">
                        <label>Airline</label>
                        <asp:TextBox runat="server" CssClass="form-control" MaxLength='2' ID="txtAirline" placeholder="Airline Code" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtAirline" InitialValue="" ValidationGroup="makebooking" runat="server" ErrorMessage="provide airline"></asp:RequiredFieldValidator>

                    </div>
                    <div class="col-md-2">
                        <label>Valid From</label>

                        <asp:TextBox runat="server" CssClass="form-control" ID="txtVFDate" onclick="showCalender(this);" placeholder="fare valid from" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ControlToValidate="txtVFDate" InitialValue="" ValidationGroup="makebooking" runat="server" ErrorMessage="provide valid date"></asp:RequiredFieldValidator>


                    </div>
                    <div class="col-md-2">
                        <label>Valid Till</label>

                        <asp:TextBox runat="server" CssClass="form-control" ID="txtVTDate" onclick="showCalender(this);" placeholder="fare valid till" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" ControlToValidate="txtVTDate" InitialValue="" ValidationGroup="makebooking" runat="server" ErrorMessage="provide valid date"></asp:RequiredFieldValidator>


                    </div>
                    <div class="col-md-1">
                        <label>Price</label>
                        <asp:TextBox runat="server" CssClass="form-control" onkeypress="return isNumber(event)" ID="txtPrice" placeholder="Adult Price" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" ControlToValidate="txtPrice" InitialValue="" ValidationGroup="makebooking" runat="server" ErrorMessage="provide price"></asp:RequiredFieldValidator>



                    </div>
                    <div class="col-md-2">
                        <label>Company</label><br />
                        <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control">

                        </asp:DropDownList>
                    </div>
                </div>
            </div>
        </div>

        <div class="panel panel-default">
            <div class="panel-heading">Flight Itinerary Details</div>
            <div class="panel-body" style="line-height: 34px; padding: 0px!important;">
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:Repeater ID="rptrSect" runat="server">
                            <HeaderTemplate>
                                <table width='100%' cellpadding='0' cellspacing='0' class='table' style='margin-bottom: 0px;'>
                                    <tr>
                                        <td class='gdvh'>SrNo</td>
                                        <td class='gdvh'>From</td>
                                        <td class='gdvh'>To</td>
                                        <td class='gdvh'>AirV</td>
                                        <td class='gdvh'>FLT No.</td>
                                        <td class='gdvh'>Class</td>
                                        <td class='gdvh'>FromDate</td>
                                        <td class='gdvh'>FromTime</td>
                                        <td class='gdvh'>ToDate</td>
                                        <td class='gdvh'>ToTime</td>
                                        <td class='gdvh'>Status</td>

                                    </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td class="gdvr"><%# Container.ItemIndex+ 1 %></td>
                                    <td class="gdvr exelcss">

                                        <asp:TextBox ID="txtGFrom" MaxLength="3" Width="60px" Font-Size="12px" CssClass="form-control" SetFocusOnError="true" Text='<%# Eval("From")%>' ValidationGroup="cxp" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="gdvr exelcss">

                                        <asp:TextBox ID="txtGTo" MaxLength="3" Width="60px" Font-Size="12px" CssClass="form-control" SetFocusOnError="true" Text='<%# Eval("To")%>' ValidationGroup="cxp" runat="server"></asp:TextBox>
                                    </td>

                                    <td class="gdvr exelcss">

                                        <asp:TextBox ID="txtGAirV" MaxLength="2" Width="60px" Font-Size="12px" CssClass="form-control" SetFocusOnError="true" Text='<%# Eval("Airline")%>' ValidationGroup="cxp" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="gdvr exelcss">

                                        <asp:TextBox ID="txtGFLTNO" MaxLength="6" Width="80px" Font-Size="12px" CssClass="form-control" SetFocusOnError="true" Text='<%# Eval("FlightNo")%>' ValidationGroup="cxp" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="gdvr exelcss">

                                        <asp:TextBox ID="txtGClass" MaxLength="1" Width="40px" Font-Size="12px" CssClass="form-control"  ValidationGroup="cxp" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="gdvr exelcss">

                                        <asp:TextBox ID="txtGFromDate" MinLength="8" MaxLength="10" Width="100px" Font-Size="12px" CssClass="form-control" Text='<%# Eval("DepartureDate")%>' ValidationGroup="cxp" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="gdvr exelcss">
                                        <asp:TextBox ID="txtGFromTime" MaxLength="6" Width="80px" Font-Size="12px" CssClass="form-control" SetFocusOnError="true" Text='<%# Eval("DepartureTime")%>' ValidationGroup="cxp" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="gdvr exelcss">
                                        <asp:TextBox ID="txtGToDate" MinLength="8" MaxLength="10" Width="100px" Font-Size="12px" CssClass="form-control" Text='<%# Eval("ArrivalDate")%>' ValidationGroup="cxp" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="gdvr exelcss">

                                        <asp:TextBox ID="txtGToTime" MaxLength="6" Width="80px" Font-Size="12px" CssClass="form-control" SetFocusOnError="true" Text='<%# Eval("ArrivalTime")%>' ValidationGroup="cxp" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="gdvr exelcss">
                                        <input maxlength="6" class="form-control" style="width: 60px; font-size: 12px;" id='txtGStatus' runat="server" value='<%# Eval("Trip")%>' type="text" />

                                    </td>

                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                        <table width='100%' cellpadding='0' cellspacing='0' class='table' margin-bottom: 0px;'>
                            <tr>

                                <td class='gdvh'>From</td>
                                <td class='gdvh'>To</td>
                                <td class='gdvh'>AirV</td>
                                <td class='gdvh'>FLT No.</td>
                                <td class='gdvh'>Class</td>
                                <td class='gdvh'>FromDate</td>
                                <td class='gdvh'>FromTime</td>
                                <td class='gdvh'>ToDate</td>
                                <td class='gdvh'>ToTime</td>
                                <td class='gdvh'>Trip</td>

                            </tr>
                            <tr>

                                <td class="gdvr exelcss">

                                    <asp:TextBox ID="txtFrom" MaxLength="3" Width="60px" Font-Size="12px" CssClass="form-control" SetFocusOnError="true"  ValidationGroup="cxp" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="txtFrom" ValidationGroup="cxp" ErrorMessage="Please enter Departure!" />
                                </td>
                                <td class="gdvr exelcss">

                                    <asp:TextBox ID="txtTo" MaxLength="3" Width="60px" Font-Size="12px" CssClass="form-control" SetFocusOnError="true"  ValidationGroup="cxp" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ControlToValidate="txtTo" ValidationGroup="cxp" ErrorMessage="Please enter Destination!" />
                                </td>
                                <td class="gdvr exelcss">

                                    <asp:TextBox ID="txtAirV" MaxLength="2" Width="60px" Font-Size="12px" CssClass="form-control" SetFocusOnError="true"  ValidationGroup="cxp" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" ControlToValidate="txtAirV" ValidationGroup="cxp" ErrorMessage="Please enter Airline!" />
                                </td>
                                <td class="gdvr exelcss">

                                    <asp:TextBox ID="txtFLTNO" MaxLength="6" Width="80px" Font-Size="12px" CssClass="form-control" SetFocusOnError="true"  ValidationGroup="cxp" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator7" ControlToValidate="txtFLTNO" ValidationGroup="cxp" ErrorMessage="Please enter Flight No!" />
                                </td>
                                <td class="gdvr exelcss">

                                    <asp:TextBox ID="txtClass" MaxLength="1" Width="40px" Font-Size="12px" CssClass="form-control"  ValidationGroup="cxp" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator8" SetFocusOnError="true" ControlToValidate="txtClass" ValidationGroup="cxp" ErrorMessage="Please enter Class!" />
                                </td>
                                <td class="gdvr exelcss">

                                    <asp:TextBox ID="txtFromDate" onclick="showCalender(this);" autocomplete="off" MinLength="8" MaxLength="10" Width="100px" Font-Size="12px" CssClass="form-control" SetFocusOnError="true" autofill="false" ValidationGroup="cxp" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator9" SetFocusOnError="true" ControlToValidate="txtFromDate" ValidationGroup="cxp" ErrorMessage="Please enter a valid departure Date!" />
                                </td>
                                <td class="gdvr exelcss">
                                    <asp:TextBox ID="txtFromTime" MaxLength="6" Width="80px" Font-Size="12px" CssClass="form-control" SetFocusOnError="true" ValidationGroup="cxp" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator10" ControlToValidate="txtFromTime" ValidationGroup="cxp" ErrorMessage="Please enter valid departure Time!" />
                                </td>
                                <td class="gdvr exelcss">
                                    <asp:TextBox ID="txtToDate" autocomplete="off" onclick="showCalender(this);" MinLength="8" MaxLength="10" Width="100px" Font-Size="12px" CssClass="form-control" SetFocusOnError="true" autofill="false" ValidationGroup="cxp" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator11" ControlToValidate="txtToDate" ValidationGroup="cxp" ErrorMessage="Please enter a valid  arrival date!" />
                                </td>
                                <td class="gdvr exelcss">

                                    <asp:TextBox ID="txtToTime" MaxLength="6" Width="80px" Font-Size="12px" CssClass="form-control" SetFocusOnError="true" ValidationGroup="cxp" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator12" ControlToValidate="txtToTime" ValidationGroup="cxp" ErrorMessage="Please enter valid Arrival Time!" />
                                </td>
                                <td class="gdvr exelcss">
                                    <select id="ddlTrip" class="form-control" runat="server">
                                            <option value="" >Select</option>
                                            <option value="IB">In</option>
                                            <option value="OB">Out</option>                                           
                                            
                                        </select>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator16" ControlToValidate="ddlTrip" ValidationGroup="cxp" ErrorMessage="Please select IB/OB!" />
                           
                                </td>
                                <td>
                                    <asp:Button ID="btnAdd" CssClass="btn btn-default" runat="server" ValidationGroup="cxp" Text="Add" OnClick="btnAdd_Click" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>

                <div class="row text-center">

                    <asp:UpdateProgress ID="UpdateProgress2" AssociatedUpdatePanelID="UpdatePanel2" DisplayAfter="1"
                        runat="server">
                        <ProgressTemplate>
                            <asp:Literal ID="Literal2" runat="server" Text="Wait"></asp:Literal>
                        </ProgressTemplate>
                    </asp:UpdateProgress>

                </div>

                <div class="row text-center pt-20">
                    <asp:Button ID="btnSearch" ValidationGroup="makebooking" runat="server" CssClass="btn btn-primary btn-lg" Text="Save Flight" OnClick="btnSearch_Click" />
                    <asp:Literal ID="ltrMessage" runat="server"></asp:Literal>
                </div>
            </div>
        </div>
    </div>

    <div class="container">

    <div class="panel panel-default" id="pnlFlightDetails" runat="server" visible="false">
        <div class="panel-heading">Flight Itinerary Details</div>
        <div class="panel-body">
            <asp:Repeater ID="rptFlightIDetails" runat="server">
                <HeaderTemplate>
                    <div class="row">
                    <div class="col-md-2">
                        <label>FlightID</label>
                    </div>
                    <div class="col-md-1">
                        <label>Source</label>
                    </div>
                    <div class="col-md-1">
                        <label>Destination</label>
                    </div>
                    <div class="col-md-1">
                        <label>Airline</label>
                    </div>
                    <div class="col-md-2">
                        <label>Valid From</label>
                    </div>
                    <div class="col-md-2">
                        <label>Valid Till</label>
                    </div>
                    <div class="col-md-1">
                        <label>Price</label>
                    </div>
                    <div class="col-md-2">
                        <label>Company</label>
                    </div>
                </div>
                </HeaderTemplate>
                <ItemTemplate>
                    <div class="row">
                        <div class="col-md-2">
                            <a href="#"><%# Eval("Flight_ID") %></a>
                            
                        </div>
                        <div class="col-md-1">
                            <%# Eval("SourceIATA") %>
                        </div>
                        <div class="col-md-1">
                            <%# Eval("DestIATA") %>
                        </div>
                        <div class="col-md-1">
                            <%# Eval("Operator") %>
                        </div>
                        <div class="col-md-2">
                            <%# String.Format("{0:dd/MM/yyyy}", Eval("Valid_from")) %>
                        </div>
                        <div class="col-md-2">
                             <%# String.Format("{0:dd/MM/yyyy}", Eval("valid_till")) %>
                            
                        </div>
                        <div class="col-md-1">
                            <%# Eval("Adult_Total") %>
                        </div>
                        <div class="col-md-2">
                            <%# Eval("Company") %>
                        </div>
                    </div>

                </ItemTemplate>
            </asp:Repeater>

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
        </div>
    </div>


</div>


    <script type="text/javascript">
        var SellPrice = 0.0;

        function SearchBooking() {
            waitingDialog.show('Please Wait...');
            return true;
        }
        function ShowFlightDetails(MFID) {
            waitingDialog.show('Please Wait...');
            $.ajax({
                type: "POST",
                url: "OwnFlight.aspx/GetFlightItineraryDetails",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify({MainFlightID: MFID}),
                responseType: "json",
                success: function (data) {
                    var jsdata = JSON.parse(data.d);
                    var strHtml = "<table width='100%' cellpadding='0' cellspacing='0' class='table' >" +
                        "<tr><td class='gdvh'>SrNo</td>" +
                        "<td class='gdvh'>From</td>" +
                        "<td class='gdvh'>To</td>" +
                        "<td class='gdvh'>Dep Date</td>" +
                        "<td class='gdvh'>Dep Time</td>" +
                        "<td class='gdvh'>Arr Date</td>" +
                        "<td class='gdvh'>Arr Time</td>" +
                        "<td class='gdvh'>Operator</td>" +
                        "<td class='gdvh'>Flight No.</td>" +
                        "<td class='gdvh'>Trip</td></tr>";
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
                            "<td class='gdvr'>" + value.TerminalFrom + "</td></tr>";

                    });
                    if (jsdata.length == 0)
                        strHtml += "<tr><td class='gdvr' colspan='12'>No Details!!</td></tr>";
                    $("#divFare" + MFID).html(strHtml + "</table>");
                },
                error: function (data) { }
            });
            waitingDialog.hide();
        }

       
    </script>

    <script>
        function isNumber(evt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode;
          if (charCode != 46 && charCode > 31 
            && (charCode < 48 || charCode > 57))
             return false;

          return true;
}
    </script>
</asp:Content>

