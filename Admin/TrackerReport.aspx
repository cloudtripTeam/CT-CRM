<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true"
    CodeFile="TrackerReport.aspx.cs" Inherits="Admin_TrackerReport" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />

<%--    <link href="../css/StyleSheet.css" rel="stylesheet" />--%>
    <link href="../../css/StyleSheet.css" rel="stylesheet" />
    <script src="https://www.traveljunction.co.uk/temp/CalendarAnyYear.js"></script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <input type="hidden" id="setascurrdate" />
    <input type="hidden" id="hdeprdate" />
    <asp:Panel ID="pnlReport" runat="server">
        <div class="container">
            <div style="height: 15px;"></div>
            <div class="panel panel-default">
                <div class="panel-heading">Online Tracker Report</div>
                <div class="panel-body" style="line-height: 34px;">
                    <div class="row">
                        <div class="col-md-2">
                            <label>Hit From</label>
                            <asp:TextBox ID="txtHitFrom" CssClass="form-control" runat="server" onclick="showCalender(this);"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <label>HIt To</label>
                            <asp:TextBox ID="txtHitTo" CssClass="form-control" runat="server" onclick="showCalender(this);"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <label>Compaany</label>
                            <asp:DropDownList ID="ddlCompany" CssClass="form-control" onchange="SearchTracker()" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-6">
                            <label>Campaign</label>
                            <asp:CheckBoxList ID="ddlSourceMedia" RepeatColumns="3" runat="server">
                            </asp:CheckBoxList>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-10">
                            <asp:Label ID="lblMsg" runat="server" EnableViewState="false" Font-Bold="true" ForeColor="Red"></asp:Label>
                        </div>


                        <div class="col-md-2">
                            <asp:Button ID="btnSearchTrack" runat="server" style="height: 38px;padding: 6px 12px;float: right;"
                                OnClick="btnSearchTrack_Click" OnClientClick="SearchTracker()" CssClass="btn btn-primary btn-lg" Text="Search" />

                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="container">
            <div style="height: 15px;"></div>
            <div class="panel panel-default">
                <div class="panel-heading">Page Tracker Report</div>
               <div class="panel-body" style="line-height: 34px; padding:1px 1px 4px 8px;">
                    <asp:Button ID="btnExport" Visible="false" runat="server" class="btn btn-primary btn-sm"  style="float: right;margin:-38px 5px -36px;" Text="Export Report" OnClick="btnExport_Click" />
                    <asp:Repeater ID="rptr" runat="server" OnItemDataBound="rptr_ItemDataBound" OnItemCommand="rptr_ItemCommand">
                        <HeaderTemplate>
                            <table style="width: 100%" class="tblDetails">
                                <thead>
                                    <tr>
                                        <th>Origin</th>
                                        <th>Destination</th>
                                        <th>Campaign</th>
                                        <th>Counter</th>
                                        <th>CPC Cost</th>
                                        <th>Total</th>
                                        <th>DatenTime</th>
                                        <th></th>
                                    </tr>
                                </thead>

                                <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>

                            <tr>
                                <td class="gdvr madeLink">
                                    <asp:Literal ID="lblOrigin" runat="server" Text='<%#Eval("Origin")%>'></asp:Literal>
                                </td>
                                <td class="gdvr madeLink">
                                    <asp:Literal ID="lblDestination" runat="server" Text='<%#Eval("Destination")%>'></asp:Literal>
                                </td>
                                <td class="gdvr madeLink">
                                    <asp:Literal ID="lblReqSource" runat="server" Text='<%#Eval("ReqSource")%>'></asp:Literal>
                                </td>
                                <td class="gdvr madeLink">
                                    <asp:Label ID="lbtNo" runat="server" Text='<%#Eval("NoOfHits")%>'></asp:Label>
                                </td>

                                <td class="gdvr madeLink">
                                    <asp:Literal ID="lbtCPC" runat="server" Text=''></asp:Literal>
                                </td>
                                <td class="gdvr madeLink">
                                    <asp:Literal ID="lbtTotal" runat="server" Text=''></asp:Literal>
                                </td>
                                <td class="gdvr madeLink">
                                    <asp:Literal ID="lbtDatenTime" runat="server" Text='<%#Eval("DatenTime")%>'></asp:Literal>
                                </td>
                                <td class="gdvr madeLink">
                                    <button type="button" class="btn btn-sm btn-info" data-toggle="collapse" data-target="#op<%# Container.ItemIndex + 1 %>">View Details</button>
                                    <div class="collapse" id="op<%# Container.ItemIndex + 1 %>">
                                        <asp:Repeater ID="rptrDetails" runat="server">
                                            <HeaderTemplate>
                                                <table style="width: 100%" class="tblDetails">
                                                    <tr>
                                                        <thead>
                                                            <th>Origin</th>
                                                            <th>Destination</th>
                                                            <th>Depart Date</th>
                                                            <th>Return Date</th>
                                                            <th>IP Address</th>
                                                            <th>Page</th>
                                                            <th>Site</th>
                                                            <th>Req Source</th>
                                                        </thead>
                                                    </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <asp:Literal ID="ltrOrigin" runat="server" Text='<%# Eval("Origin") %>'></asp:Literal>
                                                        </td>
                                                        <td>
                                                            <asp:Literal ID="ltrDestination" runat="server" Text='<%# Eval("Destination") %>'></asp:Literal></td>
                                                        <td>
                                                            <asp:Literal ID="Literal1" runat="server" Text='<%# Convert.ToDateTime(Eval("DepartDate")).ToString("dd MMM yyyy") %>'></asp:Literal></td>
                                                        <td>
                                                            <asp:Literal ID="Literal2" runat="server" Text='<%# Convert.ToDateTime(Eval("ReturnDate")).ToString("dd MMM yyyy") %>'></asp:Literal></td>

                                                        <td>
                                                            <asp:Literal ID="ltrIP" runat="server" Text='<%# Eval("IPAddress") %>'></asp:Literal></td>
                                                        <td>
                                                            <asp:Literal ID="ltrPage" runat="server" Text='<%# Eval("Page") %>'></asp:Literal></td>
                                                        <td>
                                                            <asp:Literal ID="ltrSite" runat="server" Text='<%# Eval("Site") %>'></asp:Literal></td>
                                                        <td>
                                                            <asp:Literal ID="ltrRequest" runat="server" Text='<%# Eval("ReqSource") %>'></asp:Literal></td>
                                                    </tr>

                                                </tbody>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </table>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                        <div />
                                </td>

                            </tr>
                            <tr>
                                <td colspan="8"></td>
                            </tr>



                        </ItemTemplate>

                        <FooterTemplate>
                            <tr>
                                <td colspan="3">Total
                                </td>
                                <td>
                                    <asp:Literal ID="lbthits" runat="server" Text=''></asp:Literal>
                                </td>
                                <td></td>
                                <td>
                                    <asp:Literal ID="lbtCost" runat="server" Text=''></asp:Literal>
                                </td>
                            </tr>
                            </tbody>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>

                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlDetails" runat="server" Visible="false">
        <div class="container">
            <div style="height: 15px;"></div>
            <div class="panel panel-default">
                <div class="panel-heading">
                    Page Tracker Details<span style="float: right;">
                        <asp:LinkButton ID="LinkButton1" CssClass="panel-heading" ForeColor="White" runat="server" OnClick="LinkButton1_Click">Back</asp:LinkButton></span>
                </div>
                <div class="panel-body" style="line-height: 34px; padding: 0px!important;">
                    <asp:Repeater ID="rptrTrack" runat="server">
                        <HeaderTemplate>
                            <table cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td class="gdvh">Sr No</td>
                                    <td class="gdvh">IpAddress</td>
                                    <td class="gdvh">DatenTime</td>
                                    <td class="gdvh">ReqSource</td>
                                    <td class="gdvh">Page</td>
                                    <td class="gdvh">Origin</td>
                                    <td class="gdvh">Destination</td>
                                    <td class="gdvh">DepartDate</td>
                                    <td class="gdvh">ReturnDate</td>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <tr>
                                    <td class='gdvr'>
                                        <span><%# Container.ItemIndex+1%></span>
                                    </td>
                                    <td class='gdvr'><%# Eval("IPAddress")%></td>
                                    <td class='gdvr'><%# Eval("DatenTime")%></td>
                                    <td class='gdvr'><%# Eval("ReqSource")%></td>
                                    <td class='gdvr'><%# Eval("Page")%></td>
                                    <td class='gdvr'><%# Eval("Origin")%></td>
                                    <td class='gdvr'><%# Eval("Destination")%></td>
                                    <td class='gdvr'><%#  Eval("DepartDate","{0:dd MMM yyyy}")%></td>
                                    <td class='gdvr'><%# Eval("ReturnDate","{0:dd MMM yyyy}")%></td>
                                </tr>
                                <tr>
                                    <td class='gdvr2' colspan='9'><%# Eval("Remarks")%></td>
                                </tr>
                                <%--<tr><td class='gdvr' colspan='9' id='<%# Eval("BookingID")%><%# Eval("ProdID")%>' style='display:none; background-color: rgba(253, 174, 2, 0.478431);'></td></tr>--%>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
    </asp:Panel>

    <script type="text/javascript">
        function SearchTracker() {
            waitingDialog.show('Please Wait...');

        }
    </script>
</asp:Content>
