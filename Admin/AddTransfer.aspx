<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="AddTransfer.aspx.cs" Inherits="Admin_AddTransfer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:HiddenField ID="hfBookingID" runat="server" Value="" />
    <asp:HiddenField ID="hfProdID" runat="server" Value="" />
    <asp:HiddenField ID="hfUpdatedBy" runat="server" />
    <asp:HiddenField ID="hfOldStatus" runat="server" />
    <asp:HiddenField ID="hfOldCompany" runat="server" />

    <asp:HiddenField ID="hfAdults" runat="server" />
    <asp:HiddenField ID="hfChilds" runat="server" />
    <asp:HiddenField ID="hfInfants" runat="server" />
    <asp:Panel ID="pnlBooking" Visible="true" runat="server">

        <div class="container">
            <div style="height: 15px;"></div>

            <div class="panel panel-default">
                <div class="panel-heading">
                    Booking Summary  -
                    <asp:Label ID="lblBookingID" runat="server" Text=""></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblBookkingDate" runat="server" Text=""></asp:Label>
                </div>
            </div>


            <div class="panel panel-default">
                <div class="panel-heading">Flight Travellers Details</div>
                <div class="panel-body" style="line-height: 34px; padding: 0px!important;">
                    <asp:Repeater ID="rptrPax" runat="server">
                        <HeaderTemplate>
                            <table class="table" style="margin-bottom: 0px; width: 100%; cellpadding: 0; cellspacing: 0;">
                                <tr>
                                    <td class='gdvh'>SrNo</td>
                                    <td class='gdvh'>PaxType</td>
                                    <td class='gdvh'>Title</td>
                                    <td class='gdvh'>First Name</td>
                                    <td class='gdvh'>Last Name</td>
                                    <td class='gdvh'>Tickets</td>
                                    <td class='gdvh'>DOB</td>
                                    <td class='gdvh'>Action</td>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr id='trP<%# Eval("SrNo") %>'>
                                <td class="gdvr"><%# Container.ItemIndex+ 1 %></td>
                                <td class="gdvr exelcss">
                                    <input maxlength="15" style="width: 80px; font-size: 12px;" class="form-control" id='txtPaxType<%# Eval("SrNo")%>' readonly="true" value='<%# Eval("PaxType")%>' type="text">
                                </td>
                                <td class="gdvr exelcss">
                                    <input maxlength="15" style="width: 80px; font-size: 12px;" class="form-control" id='txtTitle<%# Eval("SrNo")%>' readonly="true" value='<%# Eval("Title")%>' type="text">
                                </td>
                                <td class="gdvr exelcss">
                                    <input maxlength="40" style="width: 220px; font-size: 12px;" class="form-control" id='txtFirstName<%# Eval("SrNo")%>' readonly="true" value='<%# Eval("FName")%>' type="text">
                                </td>
                                <td class="gdvr exelcss">
                                    <input maxlength="40" style="width: 220px; font-size: 12px;" class="form-control" id='txtLastName<%# Eval("SrNo")%>' readonly="true" value='<%# Eval("LName")%>' type="text">
                                </td>
                                <td class="gdvr exelcss">
                                    <input maxlength="100" style="width: 200px; font-size: 12px;" class="form-control" id='txtTickets<%# Eval("SrNo")%>' readonly="true" value='<%# Eval("Tickets")%>' type="text">
                                </td>

                                <td class="gdvr exelcss">
                                    <input maxlength="10" style="width: 100px; font-size: 12px;" class="form-control" id='txtDOB<%# Eval("SrNo")%>' readonly="true" value='<%# Eval("DOB")%>' type="text">

                                    <asp:HiddenField ID="hidBID" runat="server" Value='<%# Eval("BookingID")%>' />
                                    <asp:HiddenField ID="hidPID" runat="server" Value='<%# Eval("ProdID")%>' />
                                    <asp:HiddenField ID="hidSNO" runat="server" Value='<%# Eval("SrNo")%>' />

                                </td>

                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            <tr>
                                <td colspan="8" class="gdvr"></td>
                            </tr>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>

            </div>

            <div class="panel panel-default">
                <div class="panel-heading">Flight Sector Details </div>
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
                                    <td class='gdvh'>Status</td>
                                    <td class='gdvh'>Action</td>

                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr id='trS<%# Eval("SrNo") %>'>
                                <td class="gdvr"><%# Container.ItemIndex+ 1 %></td>
                                <td class="gdvr exelcss">
                                    <input maxlength="3" style="width: 60px; font-size: 12px;" class="form-control" id='txtFrom<%# Eval("SrNo")%>' readonly="true" value='<%# Eval("FromDest")%>' type="text">
                                </td>
                                <td class="gdvr exelcss">
                                    <input maxlength="3" style="width: 60px; font-size: 12px;" class="form-control" id='txtTo<%# Eval("SrNo")%>' readonly="true" value='<%# Eval("ToDest")%>' type="text">
                                </td>

                                <td class="gdvr exelcss">
                                    <input maxlength="2" style="width: 60px; font-size: 12px;" class="form-control" id='txtAirV<%# Eval("SrNo")%>' readonly="true" value='<%# Eval("CarierName")%>' type="text">
                                </td>
                                <td class="gdvr exelcss">
                                    <input maxlength="8" style="width: 80px; font-size: 12px;" class="form-control" id='txtFLTNO<%# Eval("SrNo")%>' readonly="true" value='<%# Eval("FlightNo")%>' type="text">
                                </td>
                                <td class="gdvr exelcss">
                                    <input maxlength="1" style="width: 40px; font-size: 12px;" class="form-control" id='txtClass<%# Eval("SrNo")%>' readonly="true" value='<%# Eval("FClass")%>' type="text">
                                </td>
                                <td class="gdvr exelcss">
                                    <input maxlength="10" style="width: 100px; font-size: 12px;" class="form-control" id='txtFromDate<%# Eval("SrNo")%>' readonly="true" value='<%#Convert.ToDateTime(Eval("FromDateTime")).ToString("dd/MM/yyyy")%>' type="text">
                                </td>
                                <td class="gdvr exelcss">
                                    <input maxlength="5" style="width: 80px; font-size: 12px;" class="form-control" id='txtFromTime<%# Eval("SrNo")%>' readonly="true" value='<%# Convert.ToDateTime(Eval("FromDateTime")).ToString("HH:mm")%>' type="text">
                                </td>
                                <td class="gdvr exelcss">
                                    <input maxlength="10" style="width: 100px; font-size: 12px;" class="form-control" id='txtToDate<%# Eval("SrNo")%>' readonly="true" value='<%#Convert.ToDateTime(Eval("ToDateTime")).ToString("dd/MM/yyyy")%>' type="text">
                                </td>
                                <td class="gdvr exelcss">
                                    <input maxlength="5" style="width: 80px; font-size: 12px;" class="form-control" id='txtToTime<%# Eval("SrNo")%>' readonly="true" value='<%# Convert.ToDateTime(Eval("ToDateTime")).ToString("HH:mm")%>' type="text">
                                </td>
                                <td class="gdvr exelcss">
                                    <input maxlength="6" class="form-control" style="width: 60px; font-size: 12px;" id='txtStatus<%# Eval("SrNo")%>' readonly="true" value='<%# Eval("FStatus")%>' type="text"></td>

                            </tr>

                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
            </div>          
            
                <div style="height: 15px;"></div>
                <div class="panel panel-default">
                    <div class="panel-heading">Hotel Details</div>
                    <div class="panel-body" style="line-height: 34px;">

                        <div class="row">
                            <div class="col-md-12">

                                <div class="col-md-3">
                                    <label>Destination:</label><asp:TextBox runat="server" ID="txtHTLDest" CssClass="form-control" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ControlToValidate="txtHTLDest" InitialValue="" ValidationGroup="makehbooking"
                                        runat="server" ErrorMessage="provide hotel destination"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-3">
                                    <label>Hotel Name:</label><asp:TextBox runat="server" ID="txtHTLName" CssClass="form-control" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" ControlToValidate="txtHTLName" InitialValue="" ValidationGroup="makehbooking"
                                        runat="server" ErrorMessage="provide hotel name"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-3">
                                    <label>Check In:</label><asp:TextBox runat="server" onclick="showCalender(this);" ID="txtHTLCheckIn" CssClass="form-control" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" ControlToValidate="txtHTLCheckIn" InitialValue="" ValidationGroup="makehbooking"
                                        runat="server" ErrorMessage="provide hotel check In"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-3">
                                    <label>Check Out:</label><asp:TextBox runat="server" onclick="showCalender(this);" ID="txtHTLCheckOut" CssClass="form-control" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator16" ControlToValidate="txtHTLCheckOut" InitialValue="" ValidationGroup="makehbooking"
                                        runat="server" ErrorMessage="provide hotel check Out"></asp:RequiredFieldValidator>
                                </div>

                            </div>
                            <hr />
                            <div class="col-md-12">


                                <div class="col-md-3">
                                    <label>Room Type:</label><asp:TextBox runat="server" ID="txtHTLRoomType" CssClass="form-control" /></div>
                                <div class="col-md-3">
                                    <label>No of Room:</label>
                                    <asp:DropDownList ID="ddlHTLNoRooms" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                        <asp:ListItem Text="6" Value="6"></asp:ListItem>
                                        <asp:ListItem Text="7" Value="7"></asp:ListItem>
                                        <asp:ListItem Text="8" Value="8"></asp:ListItem>
                                        <asp:ListItem Text="9" Value="9"></asp:ListItem>

                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-3">
                                    <label>Meal Type:</label>
                                    <asp:DropDownList ID="ddlHTLMealType" runat="server" CssClass="form-control">
                                         <asp:ListItem Text="Room Only" Value="RO "></asp:ListItem>
                                        <asp:ListItem Text="Breakfast" Value="ВB "></asp:ListItem>
                                        <asp:ListItem Text="Breakfast and Dinner" Value="HB"></asp:ListItem>
                                        <asp:ListItem Text="Extended half board" Value="HB+"></asp:ListItem>
                                        <asp:ListItem Text="Breakfast, Lunch, Dinner" Value="FB"></asp:ListItem>
                                        <asp:ListItem Text="Breakfast, Lunch, Dinner plus drinks during meals" Value="FB+"></asp:ListItem>
                                        <asp:ListItem Text="All included" Value="AI"></asp:ListItem>
                                    </asp:DropDownList>

                                </div>


                                <div class="col-md-3">
                                    <label>Hotel PNR:</label><asp:TextBox runat="server" ID="txtHTLPNR" CssClass="form-control" /></div>

                            </div>
                        </div>
                    </div>
                </div>

                <div style="height: 15px;"></div>
                <div class="panel panel-default">
                    <div class="panel-heading">Hotel Address</div>
                    <div class="panel-body" style="line-height: 34px;">



                        <div class="row">
                            <div class="col-md-12">

                                <div class="col-md-3">
                                    <label>Hotel Supplier:</label><asp:TextBox runat="server" ID="txtHTLSupplier" CssClass="form-control" /></div>
                                <div class="col-md-3">
                                    <label>Address 1:</label><asp:TextBox runat="server" ID="txtHTLAdd1" CssClass="form-control" /></div>
                                <div class="col-md-3">
                                    <label>Address 2:</label><asp:TextBox runat="server" ID="txtHTLAdd2" CssClass="form-control" /></div>
                                <div class="col-md-3">
                                    <label>Postal:</label><asp:TextBox runat="server" ID="txtHTLPostal" CssClass="form-control" /></div>
                            </div>
                            <hr />
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <label>City:</label><asp:TextBox runat="server" ID="txtHTLCity" CssClass="form-control" /></div>
                                <div class="col-md-3">
                                    <label>Country:</label><asp:TextBox runat="server" ID="txtHTLCountry" CssClass="form-control" /></div>
                                <div class="col-md-3">
                                    <label>Telephone:</label><asp:TextBox runat="server" ID="txtHTLTelephone" CssClass="form-control" /></div>
                                <div class="col-md-3">
                                    <label>Email ID:</label><asp:TextBox runat="server" ID="txtHTLEmailID" CssClass="form-control" /></div>
                            </div>
                        </div>
                    </div>
                </div>
            
            <div class="panel panel-default">
                <div class="panel-heading">Pricebreak up</div>
                <div class="panel-body" style="line-height: 34px; padding: 0px!important;">

                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:Repeater ID="rptPrice" runat="server" OnItemCommand="rptPrice_ItemCommand" OnItemDataBound="rptPrice_ItemDataBound">
                                <HeaderTemplate>
                                    <table width='100%' cellpadding='0' cellspacing='0' height="100px" class='table' style='margin-bottom: 0px'>
                                        <tr>
                                            <td class='gdvh'>Charge Type</td>
                                            <td class='gdvh'>Charge For</td>
                                            <td class='gdvh'>Sell Amt</td>
                                            <td class='gdvh'>Cost Amt</td>
                                            <td class='gdvh'></td>
                                        </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td class="gdvr exelcss">
                                            <asp:Label ID="lbtChargeID" runat="server" Text='<%# Eval("ChargeID")%>'></asp:Label>
                                        </td>
                                        <td class="gdvr exelcss">
                                            <asp:Label ID="lblChargeFor" runat="server" Text='<%# Eval("ChargesFor")%>'></asp:Label>
                                        </td>
                                        <td class="gdvr exelcss">
                                            <input maxlength="10" class="form-control" style="width: 100px; font-size: 12px;" id='txtSalePrice' value='<%# Convert.ToDouble(Eval("SellPrice")) %>' type="text"></td>
                                        <td class="gdvr exelcss">
                                            <input maxlength="10" class="form-control" style="width: 100px; font-size: 12px;" id='txtCostPrice' value='<%# Convert.ToDouble(Eval("CostPrice"))%>' type="text"></td>
                                        <td class="gdvr">
                                            <asp:Button ID="btnDelete" CommandName="del" runat="server" Text="Delete" CssClass="btn btn-danger btn-sm" ToolTip='<%# Eval("SrNo") %>' />
                                        </td>
                                    </tr>
                                </ItemTemplate>

                            </asp:Repeater>
                            <table width='100%' cellpadding='0' cellspacing='0' class='table' style='background-color: #ddd; margin-bottom: 0px;'>
                                <tr>

                                    <td class='gdvh'>Charge Type</td>
                                    <td class='gdvh'>Charge For</td>
                                    <td class='gdvh'>Sell Amt</td>
                                    <td class='gdvh'>Cost Amt</td>
                                    <td class='gdvh'></td>
                                </tr>
                                <tr>
                                    <td class='gdvh'>

                                        <select id="ddlPayType" class="form-control" runat="server">
                                            <option value="Fare" selected="selected">Fare</option>
                                            <option value="Tax">Tax</option>
                                            <option value="Markup">Markup</option>                                          
                                            <option value="CC">Card Charge</option>
                                            <option value="Admin">Admin Charge</option>                                            
                                            <option value="Others">Others</option>
                                            <option value="Refund">Refund</option>
                                           

                                        </select>
                                    </td>
                                    <td class='gdvh'>

                                        <select id="ddlChargeFor" class="form-control" runat="server">
                                            <option value="ADT">Adult</option>
                                            <option value="CNN">Child</option>
                                            <option value="INF">Infant</option>

                                            <option value="NA" selected="selected">NA</option>
                                        </select>
                                    </td>

                                    <td class='gdvh'>
                                        <input maxlength="10" class="form-control" style="width: 100px; font-size: 12px;" onkeyup="AllowDecimal(this)" id='txtSalePriceF' runat="server" type="text" /></td>
                                    <td class='gdvh'>
                                        <input maxlength="10" class="form-control" style="width: 100px; font-size: 12px;" onkeyup="AllowDecimal(this)" id='txtCostPriceF' runat="server" type="text" /></td>

                                    <td class='gdvh'>
                                        <asp:Button ID="btnAdd" CssClass="btn btn-primary btn-lg" runat="server" OnClientClick="return validate();" Text="Add Price" OnClick="btnAdd_Click" />
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
                </div>
            </div>


            <div class="panel panel-default p-20">
                <div class="panel-body">
                    <div class="col-md-12">
                        <div class="col-md-9">
                            <asp:TextBox ID="txtRemarks" Width="100%" ValidationGroup="vgremarks" placeholder="Remarks" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <asp:Button ID="btnSaveTransfer" CssClass="btn btn-primary btn-lg" runat="server" Text="Add Transfer" ValidationGroup="makehbooking" OnClick="btnSaveTransfer_Click" />
                        </div>
                        <div class="clearfix"></div>
                    </div>
                </div>

            </div>
        </div>
    </asp:Panel>
    <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label>
</asp:Content>

