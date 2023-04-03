<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="ManualFlight.aspx.cs" Inherits="Admin_FltBooking_ManualFlight" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
   <%-- <link href="../css/StyleSheet.css" rel="stylesheet" />--%>
    <link href="../../css/StyleSheet.css" rel="stylesheet" />
    <script src="https://www.traveljunction.co.uk/temp/CalendarAnyYear.js"></script>

    <link href="//cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/css/bootstrap-multiselect.css" rel="stylesheet" type="text/css" />
    <script src="//cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/js/bootstrap-multiselect.js" type="text/javascript"></script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container-fluid">
        <div style="height: 15px;"></div>
        <div class="panel panel-default">
            <div class="panel-heading">Make Flight Reservation</div>
            <div class="panel-body" style="line-height: 34px;">
                <div class="row">
                    <div class="col-md-1">
                        <label>From</label>

                        <asp:TextBox runat="server" ID="txtPFrom" MaxLength='3' CssClass="form-control" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatora2" ControlToValidate="txtPFrom" InitialValue="" ValidationGroup="makebooking" runat="server" ErrorMessage="From"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-md-1">
                        <label>To</label>
                        <asp:TextBox runat="server" CssClass="form-control" MaxLength='3' ID="txtPTo" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidataor3" ControlToValidate="txtPTo" InitialValue="" ValidationGroup="makebooking" runat="server" ErrorMessage="destination"></asp:RequiredFieldValidator>


                    </div>
                    <div class="col-md-1">
                        <label>From Date</label>
                        <asp:TextBox runat="server" ID="txtDepDate" onclick="showCalender(this);" AutoCompleteType="None" CssClass="form-control" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidaator11" ControlToValidate="txtDepDate" InitialValue="" ValidationGroup="makebooking" runat="server" ErrorMessage="Dep Date"></asp:RequiredFieldValidator>

                    </div>
                    <div class="col-md-1">
                        <label>To Date</label>
                        <asp:TextBox runat="server" ID="txtRetDate" MaxLength='20' onclick="showCalender(this);" CssClass="form-control" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidyator10" ControlToValidate="txtRetDate" InitialValue="" ValidationGroup="makebooking" runat="server" ErrorMessage="Ret Date"></asp:RequiredFieldValidator>
                    </div>

                    <div class="col-md-1">
                        


                        <label>Airline</label>
                        <input maxlength="2" class="form-control" style="width: 60px; font-size: 12px;" id='txtAirline' runat="server" type="text">

                    </div>


                    <div class="col-md-1">
                        <label>Adult</label>

                        <input maxlength="6" class="form-control" style="width: 60px; font-size: 12px;" id='txtAdt' value="0" runat="server" type="text">
                    </div>

                    <div class="col-md-1">
                        <label>Child</label>

                        <input maxlength="6" class="form-control" style="width: 60px; font-size: 12px;" id='txtChd' value="0" runat="server" type="text">
                    </div>

                    <div class="col-md-1">
                        <label>Infant</label>
                        <input maxlength="6" class="form-control" style="width: 60px; font-size: 12px;" id='txtInfants' value="0" runat="server" type="text">
                    </div>
                    <div class="col-md-1">
                        <label>Journey</label>
                        <asp:DropDownList ID="ddlJourney" runat="server" CssClass="form-control">
                            <asp:ListItem Text="Return" Value="1" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="One way" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-1">
                        <label>Base</label>
                        <input maxlength="6" class="form-control" style="width: 60px; font-size: 12px;" id='txtBase' value="0" runat="server" type="text">
                    </div>
                    <div class="col-md-1">
                        <label>Tax</label>
                        <input maxlength="6" class="form-control" style="width: 60px; font-size: 12px;" id='txtTax' value="0" runat="server" type="text">
                    </div>
                    <div class="col-md-1">
                        <label>Markup</label>
                       
                        <asp:TextBox ID="txtMarkup" CssClass="form-control" value="0" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-1">
                        <label>Fare Type</label>
                        <input maxlength="6" class="form-control" style="width: 60px; font-size: 12px;" id='txtFType' runat="server" type="text">
                    </div>
                    <div class="col-md-1">
                        <label>Currency</label>
                        <asp:DropDownList ID="ddlCurrency" runat="server" CssClass="form-control">
                            <asp:ListItem Value="GBP" Selected="True">GBP</asp:ListItem>
                            <asp:ListItem Value="USD">USD</asp:ListItem>
                            <asp:ListItem Value="CAD">CAD</asp:ListItem>
                            <asp:ListItem Value="EUR">EUR</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-1">
                        <label>Sectors</label>

                        <asp:DropDownList ID="ddlSectors" runat="server" CssClass="form-control">

                            <asp:ListItem Text="1" Value="1"></asp:ListItem>
                            <asp:ListItem Text="2" Value="2"></asp:ListItem>
                            <asp:ListItem Text="3" Value="3"></asp:ListItem>
                            <asp:ListItem Selected="True" Text="4" Value="4"></asp:ListItem>
                            <asp:ListItem Text="5" Value="5"></asp:ListItem>
                            <asp:ListItem Text="6" Value="6"></asp:ListItem>
                            <asp:ListItem Text="7" Value="7"></asp:ListItem>
                            <asp:ListItem Text="8" Value="8"></asp:ListItem>
                            <asp:ListItem Text="9" Value="9"></asp:ListItem>
                            <asp:ListItem Text="10" Value="10"></asp:ListItem>
                            <asp:ListItem Text="11" Value="11"></asp:ListItem>
                            <asp:ListItem Text="12" Value="12"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-1">
                        <label>&nbsp;</label><br />
                        <asp:Button ID="btnSearch" ValidationGroup="makebooking" runat="server" CssClass="btn btn-primary btn-lg" Text="Proceed" OnClick="btnSearch_Click" />
                        
                    </div>
                     <div class="col-md-1">
                        <label>&nbsp;</label><br />
                         <asp:Button ID="btnFind" runat="server" Text="Search" CssClass="btn btn-primary btn-lg" OnClick="btnFind_Click" />
                         </div>

                </div>


                <div class="row">
                    <div class="col-md-12">
                        <asp:Label ID="lblMsg" runat="server" EnableViewState="false" ForeColor="Red"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
        <div class="panel panel-default">
            <div class="panel-heading">Flight Sector Details</div>
            <div class="panel-body" style="line-height: 34px; padding: 0px!important;">
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
                        <td class='gdvh'>Cabin</td>

                    </tr>
                     </HeaderTemplate>
                    <ItemTemplate>
                    <tr>
                        <td class="gdvr exelcss">

                              <%# Container.ItemIndex+ 1 %>

                        </td>
                        <td class="gdvr exelcss">

                            <asp:TextBox ID="txtFrom" MaxLength="3" Width="60px" Font-Size="12px" CssClass="form-control" SetFocusOnError="true" ValidationGroup="cxp" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" CssClass="sr-only" ControlToValidate="txtFrom" ValidationGroup="cxp" ErrorMessage="Please enter Departure!" />
                        </td>
                        <td class="gdvr exelcss">

                            <asp:TextBox ID="txtTo" MaxLength="3" Width="60px" Font-Size="12px" CssClass="form-control" SetFocusOnError="true" ValidationGroup="cxp" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" CssClass="sr-only" ControlToValidate="txtTo" ValidationGroup="cxp" ErrorMessage="Please enter Destination!" />
                        </td>

                        <td class="gdvr exelcss">

                            <asp:TextBox ID="txtAirV" MaxLength="2" Width="40px" Font-Size="12px" CssClass="form-control" SetFocusOnError="true" ValidationGroup="cxp" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" CssClass="sr-only" ControlToValidate="txtAirV" ValidationGroup="cxp" ErrorMessage="Please enter Airline!" />
                        </td>
                        <td class="gdvr exelcss">

                            <asp:TextBox ID="txtFLTNO" MaxLength="6" Width="60px" Font-Size="12px" CssClass="form-control" SetFocusOnError="true" ValidationGroup="cxp" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" CssClass="sr-only" ControlToValidate="txtFLTNO" ValidationGroup="cxp" ErrorMessage="Please enter Flight No!" />
                        </td>
                        <td class="gdvr exelcss">

                            <asp:TextBox ID="txtClass" MaxLength="50" Width="80px" Font-Size="12px" CssClass="form-control" ValidationGroup="cxp" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" CssClass="sr-only" SetFocusOnError="true" ControlToValidate="txtClass" ValidationGroup="cxp" ErrorMessage="Please enter Class!" />
                        </td>
                        <td class="gdvr exelcss">

                            <asp:TextBox ID="FromDate" onclick="showCalender(this);" MinLength="8" MaxLength="10" Width="100px" Font-Size="12px" CssClass="form-control" SetFocusOnError="true" autofill="false" ValidationGroup="cxp" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" CssClass="sr-only" SetFocusOnError="true" ControlToValidate="FromDate" ValidationGroup="cxp" ErrorMessage="Please enter a valid departure Date!" />
                        </td>
                        <td class="gdvr exelcss">
                            <asp:TextBox ID="FromTime" MaxLength="6" Width="80px" Font-Size="12px" CssClass="form-control" SetFocusOnError="true" ValidationGroup="cxp" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator7" CssClass="sr-only" ControlToValidate="FromTime" ValidationGroup="cxp" ErrorMessage="Please enter valid departure Time!" />
                        </td>
                        <td class="gdvr exelcss">
                            <asp:TextBox ID="ToDate" onclick="showCalender(this);" MinLength="8" MaxLength="10" Width="100px" Font-Size="12px" CssClass="form-control" SetFocusOnError="true" autofill="false" ValidationGroup="cxp" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator8" CssClass="sr-only" ControlToValidate="ToDate" ValidationGroup="cxp" ErrorMessage="Please enter a valid  arrival date!" />
                        </td>
                        <td class="gdvr exelcss">

                            <asp:TextBox ID="ToTime" MaxLength="6" Width="80px" Font-Size="12px" CssClass="form-control" SetFocusOnError="true" ValidationGroup="cxp" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator9" CssClass="sr-only" ControlToValidate="ToTime" ValidationGroup="cxp" ErrorMessage="Please enter valid Arrival Time!" />
                        </td>
                        <td class="gdvr exelcss">
                            <asp:TextBox ID="txtCabin" CssClass="form-control" runat="server"></asp:TextBox>
                            </td>

                    </tr>
                    <tr>
                        <td class="gdvr exelcss"></td>
                        <td class="gdvr exelcss" colspan="2">Equip Type</td>
                        <td class="gdvr exelcss" colspan="2">Tech Stop</td>
                        <td class="gdvr exelcss" colspan="2">Is Return</td>
                        <td class="gdvr exelcss">Opt Carrier</td>
                        <td class="gdvr exelcss">Mark Carrier</td>
                        <td class="gdvr exelcss">Bag Info</td>
                        <td class="gdvr exelcss">No of Seats</td>

                    </tr>
                    <tr>
                        <td class="gdvr exelcss"></td>
                        <td class="gdvr exelcss" colspan="2">
                           
                            <asp:TextBox ID="txtEquipType" class="form-control" runat="server"></asp:TextBox>
                        </td>
                        <td class="gdvr exelcss" colspan="2">
                            
                             <asp:TextBox ID="txtTechStop" class="form-control" TextMode="Number" runat="server"></asp:TextBox>
                        </td>
                        <td class="gdvr exelcss" colspan="2">
                            
                            <asp:DropDownList ID="ddlIsReturn" runat="server" CssClass="form-control">
                            <asp:ListItem Text="Yes" Selected="True" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                             
                        </td>
                        <td class="gdvr exelcss">
                            
                             <asp:TextBox ID="txtoptcr" class="form-control" runat="server"></asp:TextBox>
                        </td>
                        <td class="gdvr exelcss">
                            
                             <asp:TextBox ID="txtmarkcr" class="form-control" runat="server"></asp:TextBox>
                        </td>
                        <td class="gdvr exelcss">
                            
                             <asp:TextBox ID="txtbinfo" class="form-control" runat="server"></asp:TextBox>
                        </td>
                        <td class="gdvr exelcss">
                            <asp:TextBox ID="txtNoSeats" CssClass="form-control" TextMode="Number" runat="server"></asp:TextBox>
                        </td>

                    </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                    <tr>
                        <td colspan="11">
                            <input id="setascurrdate" type="hidden" />
                            <input id="hdeprdate" type="hidden" /></td>
                    </tr>
                </table>
                </FooterTemplate>
                </asp:Repeater>
                
            </div>
            <br />
                <table>
                    <tr>
                       <td style="vertical-align:middle; padding-left:400px; ">
                           <asp:Literal ID="ltrMsg" runat="server"></asp:Literal>
                            <asp:Button ID="btnAdd" CssClass="btn btn-primary btn-lg" runat="server" ValidationGroup="cxp" Text="Insert Flight Details" Visible="false" OnClick="btnAdd_Click" />
                           <br />
                       </td>
                    </tr>
                </table>
        </div>
         <div class="panel panel-default">
            <div class="panel-heading">Your Details</div>
             <div class="panel-body" style="line-height: 34px; padding: 0px!important;">
                 <asp:Repeater ID="rptViewDetails" runat="server">
                     <ItemTemplate>
                         <div class="row">
                             <div class="col-md-1"><%# Eval("From") %>-<%# Eval("To") %></div>
                             
                             <div class="col-md-1"><%# Eval("Fdate", "{0:d}") %></div>
                             <div class="col-md-1"><%# Eval("Tdate", "{0:d}") %></div>
                             <div class="col-md-1"><%# Eval("Jtype").ToString()=="True"?"Return":"One Way" %>/<%# Eval("ValCarrier") %></div>
                             <div class="col-md-1"><%# Eval("Adt") %></div>
                             <div class="col-md-1"><%# Eval("Chd") %></div>
                             <div class="col-md-1"><%# Eval("Inf") %></div>
                             <div class="col-md-1"><%# Eval("BaseFare") %></div>
                             <div class="col-md-1"><%# Eval("Tax") %></div>
                             <div class="col-md-1"><%# Eval("Markup") %></div>
                             <div class="col-md-1"><%# Eval("Currency") %></div>
                             
                         </div>
                     </ItemTemplate>
                 </asp:Repeater>
             </div>
    </div>



    <script type="text/javascript">

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

        if ($("#Text1").val() == "") {
            args.IsValid = false;
        }

    </script>
</asp:Content>

