<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="BlockedIPs.aspx.cs" Inherits="Admin_BlockedIPs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
   <%-- <link href="../css/StyleSheet.css" rel="stylesheet" />--%>
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
                        <span>IP</span>
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtIP" placeholder="IP" />

                    </div>
                    
                    <div class="col-md-2">
                        <span>Website For</span>
                        <asp:DropDownList runat="server" ID="ddlWebsite" CssClass="form-control">
                            <asp:ListItem Value="">Select</asp:ListItem>
                            <asp:ListItem Value="BackOffice">BackOffice</asp:ListItem>
                            <asp:ListItem Value="WebSite">WebSite</asp:ListItem>
                            <asp:ListItem Value="CMS">Web CMS</asp:ListItem>
                          
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-4">
                        <span>Authentication</span>
                        <asp:RadioButtonList runat="server" ID="rbtnAuth" RepeatDirection="Horizontal" CssClass="form-control">
                            <asp:ListItem Value="True" Selected="True">True</asp:ListItem>
                            <asp:ListItem Value="False">False</asp:ListItem>                           
                        </asp:RadioButtonList>
                    </div>
                     <div class="col-md-2">
                        <span></span>
                        <br />
                        <asp:Button runat="server" ID="btnAddIP" CssClass="btn btn-danger btn-lg" OnClientClick="return AddIP(); " Text="Add" OnClick="btnAddIP_Click" />
                    </div>
                    <div class="col-md-2">
                        <span></span>
                        <br />
                        <asp:Button runat="server" ID="btnSearch" CssClass="btn btn-primary btn-lg" Text="Search" OnClick="btnSearch_Click" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <asp:Label ID="lblMsg" runat="server" EnableViewState="false" ForeColor="Red"></asp:Label>
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
                                <td style="width: 10%;" class='gdvh'>IP</td>
                                <td style="width: 10%;" class='gdvh'>WebSite For</td>
                                <td style="width: 10%;" class='gdvh'>Authentication</td>                               
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
                                <td style="width: 10%;" class='gdvr'><%# Eval("IP")%> </td>
                                <td style="width: 10%;" class='gdvr'><%# Eval("WebsiteFor")%> </td>
                                <td style="width: 10%;" class='gdvr'><%# Eval("IsAuthentication")%> </td>                               
                                <td style="width: 10%;" class='gdvr'><%# Eval("ModifiedBy")%> </td>
                                <td style="width: 15%;" class='gdvr'><%# Eval("ModifiedDate","{0:dd-MM-yyyy HH:mm}")%> </td>
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
                                    <asp:TextBox runat="server" ID="txtEditIP" CssClass="form-control" placeholder="IP" Text='<%# Eval("IP")%> ' />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtEditIP"
                                        runat="server" CssClass="requare" ValidationGroup="GroupDetail" ErrorMessage="IP is empty not allowed!!!!"
                                        SetFocusOnError="true">*</asp:RequiredFieldValidator>
                                </td>
                                <td style="width: 10%;" class='gdvr'>
                                   <%# Eval("WebsiteFor")%> 
                                </td>

                                <td style="width: 10%;" class='gdvr'>
                                    <asp:CheckBox ID="chbAuth" Checked='<%# Eval("IsAuthentication")%>' runat ="server"  />
                                </td>
                                 <td style="width: 10%;" class='gdvr'><%# Eval("ModifiedBy")%> </td>
                                <td style="width: 15%;" class='gdvr'><%# Eval("ModifiedDate","{0:dd-MM-yyyy HH:mm}")%> </td>
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


        function AddIP() {
            var strMsg = "";

            if ($("#<%= txtIP.ClientID%>").val() == "") {
                strMsg += "IP is empty not allowed!!\n\r";
            }
            if ($("#<%= ddlWebsite.ClientID%>").val() == "") {
                strMsg += "Please select Website for!!\n\r";
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

