<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="PageDestination.aspx.cs" Inherits="Admin_PageDestination" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%--<link href="../css/StyleSheet.css" rel="stylesheet" />--%>
    <link href="../../css/StyleSheet.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <div style="height: 15px;"></div>
        <div class="panel panel-default">
            <div class="panel-heading">Page Destination</div>
            <div class="panel-body" style="line-height: 34px;">
                <div class="row">

                    <div class="col-md-2">
                        <span>Origin</span>
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtAirport" placeholder="Airport" />

                    </div>
                    <div class="col-md-2">
                        <span>Destintion</span>

                        <asp:TextBox runat="server" ID="txtDest" CssClass="form-control" placeholder="Destination" />
                    </div>
                    <div class="col-md-3">
                        <span>Cabin Class</span>
                        <asp:DropDownList runat="server" ID="ddlClass" CssClass="form-control">
                            <asp:ListItem Value="">ANY</asp:ListItem>
                            <asp:ListItem Value="ECONOMY">ECONOMY</asp:ListItem>
                            <asp:ListItem Value="BUSINESS">BUSINESS</asp:ListItem>
                            <asp:ListItem Value="FIRSTCLASS">FIRSTCLASS</asp:ListItem>
                            <asp:ListItem Value="PREMIUM">PREMIUM</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <span>Page</span>
                        <asp:DropDownList runat="server" ID="ddlPage" CssClass="form-control">
                            <asp:ListItem Value="">ANY</asp:ListItem>
                            <asp:ListItem Value="Home">Home</asp:ListItem>
                            <asp:ListItem Value="Destination">Destination</asp:ListItem>
                            <asp:ListItem Value="Flight">Flight</asp:ListItem>
                            <asp:ListItem Value="Airline">Airline</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2">
                        <span>Company ID</span>
                        <asp:DropDownList runat="server" ID="ddlCompany" CssClass="form-control">
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-8">
                        <asp:Label ID="lblMsg" runat="server" EnableViewState="false" ForeColor="Red"></asp:Label>
                    </div>

                    <div class="col-md-2">
                        <span></span>
                        <br />
                        <asp:Button runat="server" ID="btnAddPageDestination" CssClass="btn btn-danger btn-lg" OnClientClick="return AddPageDestination(); " Text="Add" OnClick="btnAddPageDestination_Click" />
                    </div>
                    <div class="col-md-2">
                        <span></span>
                        <br />
                        <asp:Button runat="server" ID="btnSearch" CssClass="btn btn-primary btn-lg" OnClientClick="return SearchPageDestination();" Text="Search" OnClick="btnSearch_Click" />

                    </div>
                </div>


            </div>
        </div>
    </div>

    <div class="container">
        <div style="height: 15px;"></div>
        <div class="panel panel-default">
            <div class="panel-heading">Page Destination Details</div>
            <div class="panel-body" style="line-height: 34px; padding: 0px!important;">
                <asp:DataList ID="dlDetails" Width="100%" runat="server" OnCancelCommand="dlDetails_CancelCommand" OnDeleteCommand="dlDetails_DeleteCommand" OnEditCommand="dlDetails_EditCommand" OnUpdateCommand="dlDetails_UpdateCommand" OnItemDataBound="dlDetails_ItemDataBound">
                    <HeaderTemplate>
                        <table width='100%' cellpadding='0' cellspacing='0' class='table' style='margin-bottom: 0px;'>
                            <tr>
                                <td style="width: 10%;" class='gdvh'>SrNo</td>
                                <td style="width: 10%;" class='gdvh'>Airport</td>
                                <td style="width: 10%;" class='gdvh'>Destination</td>
                                <td style="width: 10%;" class='gdvh'>CabinClass</td>
                                <td style="width: 10%;" class='gdvh'>Page</td>
                                <td style="width: 10%;" class='gdvh'>Company</td>
                                <td style="width: 10%;" class='gdvh'>ModifiedBy</td>
                                <td style="width: 15%;" class='gdvh'>ModifiedDate</td>
                                <td style="width: 15%;" class='gdvh'></td>
                            </tr>
                        </table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <table width='100%' cellpadding='0' cellspacing='0' class='table' style='margin-bottom: 0px;'>
                            <tr>
                                <td style="width: 10%;" class='gdvr'><%# Container.ItemIndex+ 1 %></td>
                                <td style="width: 10%;" class='gdvr'><%# Eval("Airport")%> </td>
                                <td style="width: 10%;" class='gdvr'><%# Eval("Dest")%> </td>
                                <td style="width: 10%;" class='gdvr'><%# Eval("CClass")%> </td>
                                <td style="width: 10%;" class='gdvr'><%# Eval("Page")%> </td>
                                <td style="width: 10%;" class='gdvr'><%# Eval("Company")%> </td>
                                <td style="width: 10%;" class='gdvr'><%# Eval("ModifyBy")%> </td>
                                <td style="width: 15%;" class='gdvr'><%# Eval("ModifyDate","{0:dd-MM-yyyy HH:mm}")%> </td>
                                <td style="width: 15%;" class='gdvr'>
                                    <asp:LinkButton ID="lbtnEdit" runat="server" CommandName="Edit">Edit</asp:LinkButton>
                                    <asp:LinkButton ID="lbtnDelete" runat="server" CommandName="Delete" CommandArgument='<%# Eval("SrNo")%>'>Delete</asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <table width='100%' cellpadding='0' cellspacing='0' class='table' style='margin-bottom: 0px;'>
                            <tr>
                                <td style="width: 10%;" class='gdvr'><%# Container.ItemIndex+ 1 %></td>
                                <td style="width: 10%;" class='gdvr'>
                                    <asp:TextBox runat="server" ID="txtEditAirport" CssClass="form-control" placeholder="Airport" Text='<%# Eval("Airport")%> ' />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtEditAirport"
                                        runat="server" CssClass="requare" ValidationGroup="GroupDetail" ErrorMessage="Please choose origin airport code!!"
                                        SetFocusOnError="true">*</asp:RequiredFieldValidator>
                                </td>
                                <td style="width: 10%;" class='gdvr'>
                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtEditDest" placeholder="Destination" Text='<%# Eval("Dest")%>' />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtEditDest"
                                        runat="server" CssClass="requare" ValidationGroup="GroupDetail" ErrorMessage="Please choose destination airport code!!"
                                        SetFocusOnError="true">*</asp:RequiredFieldValidator>
                                </td>

                                <td style="width: 10%;" class='gdvr'>
                                    <asp:DropDownList runat="server" ID="ddlEditClass" CssClass="form-control">
                                        <asp:ListItem Value="">ANY</asp:ListItem>
                                        <asp:ListItem Value="ECONOMY">ECONOMY</asp:ListItem>
                                        <asp:ListItem Value="BUSINESS">BUSINESS</asp:ListItem>
                                        <asp:ListItem Value="FIRSTCLASS">FIRSTCLASS</asp:ListItem>
                                        <asp:ListItem Value="PREMIUM">PREMIUM</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="ddlEditClass"
                                        runat="server" CssClass="requare" ValidationGroup="GroupDetail" ErrorMessage="Please choose cabin class!!"
                                        SetFocusOnError="true">*</asp:RequiredFieldValidator>
                                </td>
                                <td style="width: 10%;" class='gdvr'>
                                    <asp:DropDownList runat="server" ID="ddlEditPage" CssClass="form-control">
                                        <asp:ListItem Value="">ANY</asp:ListItem>
                                        <asp:ListItem Value="Home">Home</asp:ListItem>
                                        <asp:ListItem Value="Destination">Destination</asp:ListItem>
                                        <asp:ListItem Value="Flight">Flight</asp:ListItem>
                                        <asp:ListItem Value="Airline">Airline</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="ddlEditPage"
                                        runat="server" CssClass="requare" ValidationGroup="GroupDetail" ErrorMessage="Please choose page!!"
                                        SetFocusOnError="true">*</asp:RequiredFieldValidator>
                                </td>
                                <td style="width: 10%;" class='gdvr'><%# Eval("Company")%> </td>
                                <td style="width: 10%;" class='gdvr'><%# Eval("ModifyBy")%> </td>
                                <td style="width: 15%;" class='gdvr'><%# Eval("ModifyDate","{0:dd-MM-yyyy HH:mm}")%> </td>
                                <td style="width: 15%;" class='gdvr'>
                                    <asp:LinkButton ID="lbtnUpdate" runat="server" ValidationGroup="GroupDetail" CommandName="Update" CommandArgument='<%# Eval("SrNo")%>'>Update</asp:LinkButton>
                                    <asp:LinkButton ID="lbtnCancel" runat="server" CommandName="Cancel">Cancel</asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </EditItemTemplate>

                </asp:DataList>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hfUpdatedBy" runat="server" />
    <div id="fadebackground">
    </div>

    <div align="center" id="popProgressBar" style="display: none;" class="popup-product">
        <table width="100%" class="table" align="center" height="100%" bgcolor="#ffffff">
            <tr>
                <td class="popup-header">Please wait while we process your request...
                </td>
            </tr>
            <tr>
                <td align="center" valign="middle">
                    <img src="../Images/Wait.gif" id="ImageProgressbar" />
                </td>
            </tr>
            <tr>
                <td align="center" height="40" style="background-color: #ffffff; color: #B9B9B9; vertical-align: middle; text-align: center; font-size: 18px; font-family: Verdana;"></td>
            </tr>
        </table>
    </div>

    <script src="js/PageDestination.js"></script>
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


        function AddPageDestination() {
            var strMsg = "";

            if ($("#txtDest").val() == "") {
                strMsg += "Page Destination is empty not allowed!!\n\r";
            }
            if ($("#txtAirport").val() == "") {
                strMsg += "Airport is empty not allowed!!\n\r";
            }
            if ($("#ddlClass").val() == "") {
                strMsg += "Please any select cabin class!!\n\r";
            }
            if ($("#ddlPage").val() == "") {
                strMsg += "Please any select page!!\n\r";
            }
            if ($("#ddlCompany").val() == "") {
                strMsg += "Please any select company!!\n\r";
            }
            if (strMsg == "") {
                return true;
            }
            else { alert(strMsg); }
        }

        function SearchPageDestination() {
            popup('popProgressBar', 30, 30);
            return true;
        }
    </script>
</asp:Content>

