<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="SetPermission.aspx.cs" Inherits="Admin_SetPermission" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript">
        function ChangeDiv(ctr) {
            $("#Li" + $("#<%=hfTab.ClientID %>").val()).removeClass('active')
            $("#Li" + ctr).addClass('active')
            for (var i = 1; i <= 4; i++) {
                $("#div" + i.toString()).hide();
            }
            $("#div" + ctr).show();
            $("#<%=hfTab.ClientID %>").val(ctr);
            $("#<%=lblMsg.ClientID %>").html("");

        }
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }
        window.onload = function () { ChangeDiv($("#<%=hfTab.ClientID %>").val()); }       
    </script>
    <style type="text/css">
        .tabcontents {
            background-color: #fff;
            padding: 10px;
            border-left: #D7D7D7 solid 1px;
            border-right: #D7D7D7 solid 1px;
            border-bottom: #D7D7D7 solid 1px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">

        <div class="row">
            <div class="col-md-12">
                
                <ul class="nav nav-tabs">
                    <li id="Li1" onclick="ChangeDiv('1')" class="active"><a href="#">
                        <h4>Page Permission</h4>
                    </a></li>
                    <li id="Li2" onclick="ChangeDiv('2')" class="tabnonselected"><a href="#">
                        <h4>Company Permission</h4>
                    </a></li>
                </ul>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="tabcontents">
                        <div class="col-md-12">
                            <asp:Label ID="Label1" runat="server" EnableViewState="false" ForeColor="Red"></asp:Label>
                        </div>
                        <div style="clear: both;">
                        </div>
                        <div id="div1">

                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <div class="row">
                                            <div class="col-md-3">Please Choose User Role</div>
                                            <div class="col-md-2">
                                                <asp:DropDownList ID="ddlRollMaster" CssClass="form-control" runat="server"></asp:DropDownList>
                                            </div>

                                            <div class="col-md-2">
                                                <asp:Button ID="btnProceed" runat="server" CssClass="btn btn-danger btn-lg" Text="Continue" OnClick="btnProceed_Click"></asp:Button>
                                                <asp:Button ID="btnSetPermission" runat="server" CssClass="btn btn-danger btn-lg" Text="Set Permission" OnClick="btnSetPermission_Click"></asp:Button>
                                            </div>
                                            <div class="col-md-1">
                                                <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-danger btn-lg" Text="Cancel" OnClick="btnCancel_Click"></asp:Button>
                                            </div>
                                            <div class="col-md-4"></div>
                                        </div>
                                        <div class="row">
                                            <asp:Label ID="lblMsg" runat="server" EnableViewState="false" ForeColor="Red"></asp:Label>
                                        </div>
                                        <div class="clearfix"></div>
                                        <div class="panel" style="padding-top: 0px; margin-top: 20px;">
                                            <div class="panel-body " style="border: 1px solid #ddd; padding: 0px!important;">
                                                <asp:Panel ID="pnlDetails" runat="server">
                                                    <asp:Repeater ID="rptPermissionDetails" runat="server">
                                                        <HeaderTemplate>
                                                            <div class="table-responsive">
                                                                <table class="table  table-hover">
                                                                    <thead class="thead-inverse">
                                                                        <tr>
                                                                            <th>S#</th>
                                                                            <th>GroupName</th>
                                                                            <th>PageName</th>
                                                                            <th>>OptionName</th>
                                                                            <th>IsAuth</th>
                                                                        </tr>
                                                                    </thead>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <th scope="row"><%# Container.ItemIndex+ 1 %></th>
                                                                <td><%# Eval("GroupName") %></td>
                                                                <td><%# Eval("PageName") %></td>
                                                                <td><%# Eval("OptionName") %></td>
                                                                <td>
                                                                    <asp:CheckBox ID="chbIsAuth" runat="server" Checked='<%# Eval("IsAuthentication") %>' />
                                                                    <asp:HiddenField ID="hfOptionID" runat="server" Value='<%# Eval("OptionID") %>' />
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            </table>
                                                            </div>
                                                        </FooterTemplate>
                                                    </asp:Repeater>
                                                </asp:Panel>
                                            </div>
                                        </div>

                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="div2">
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <div class="row">
                                            <div class="col-md-3">Please Choose User</div>
                                            <div class="col-md-2">
                                                <asp:DropDownList ID="ddlUser" CssClass="form-control" runat="server"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="ddlUser" InitialValue="Select" ValidationGroup="compAuth" runat="server" ErrorMessage="Please Select a User"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="col-md-2">
                                                <asp:Button ID="btnUserProceed" runat="server" CssClass="btn btn-danger btn-lg" Text="Continue" OnClick="btnUserProceed_Click"></asp:Button>
                                                <asp:Button ID="btnCompanyPermission" ValidationGroup="compAuth" runat="server" CssClass="btn btn-danger btn-lg" Text="Set Permission" OnClick="btnCompanyPermission_Click"></asp:Button>
                                            </div>
                                            <div class="col-md-1">
                                                <asp:Button ID="btnCompnyCancel" runat="server" CssClass="btn btn-danger btn-lg" Text="Cancel" OnClick="btnCompnyCancel_Click"></asp:Button>
                                            </div>
                                            <div class="col-md-4"></div>
                                        </div>
                                        <div class="row">
                                            <asp:Label ID="Label2" runat="server" EnableViewState="false" ForeColor="Red"></asp:Label>
                                        </div>
                                        <div class="clearfix"></div>
                                        <div class="panel" style="padding-top: 0px; margin-top: 20px;">
                                            <div class="panel-body " style="border: 1px solid #ddd; padding: 0px!important;">
                                                <asp:Panel ID="pnlCompany" runat="server">
                                                    <asp:Repeater ID="rptrCompany" runat="server">
                                                        <HeaderTemplate>
                                                            <div class="table-responsive">
                                                                <table class="table  table-hover">
                                                                    <thead class="thead-inverse">
                                                                        <tr>
                                                                            <th>S#</th>
                                                                            <th>User</th>
                                                                            <th>Company</th>
                                                                            <th>IsAuth</th>
                                                                        </tr>
                                                                    </thead>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <th scope="row"><%# Container.ItemIndex+ 1 %></th>
                                                                <td><%# ddlUser.SelectedValue %></td>
                                                                <td><%# Eval("CompanyName") %></td>

                                                                <td>
                                                                    <asp:CheckBox ID="chbIsAuth" runat="server" Checked='<%# Convert.ToBoolean(Eval("IsAuthentication") )%>' />
                                                                    <asp:HiddenField ID="hfCompanyID" runat="server" Value='<%# Eval("CompanyID") %>' />
                                                                    <asp:HiddenField ID="hfUserID" runat="server" Value='<%# ddlUser.SelectedValue %>' />
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>

                                                        <FooterTemplate>
                                                            </table>
                                                            </div>
                                                        </FooterTemplate>
                                                    </asp:Repeater>
                                                </asp:Panel>
                                            </div>
                                        </div>

                                    </td>
                                </tr>
                            </table>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hfTab" runat="server" Value="1" />
</asp:Content>

