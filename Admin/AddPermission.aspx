<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="AddPermission.aspx.cs" Inherits="Admin_AddPermission" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript">
        function ChangeDiv(ctr) {
            $("#Li" + $("#<%=hfTab.ClientID %>").val()).removeClass('tabselected').addClass('tabnonselected')
            $("#Li" + ctr).removeClass('tabnonselected').addClass('tabselected')
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
        .tabs {
            padding: 7px 5px;
            font-size: 0;
            margin: 0;
            list-style-type: none;
            text-align: left; /*set to left, center, or right to align the tabs as desired*/
        }

            .tabs .tabnonselected a {
                float: left;
                font: normal 12px Verdana;
                text-decoration: none;
                position: relative;
                padding: 11px 16px;
                border: 1px solid #CCC;
                border-bottom-color: #B7B7B7;
                color: #000;
                cursor: pointer;
                background: #F0F0F0;
                border-radius: 3px 3px 0 0;
                margin-left: 2px;
            }

                .tabs .tabnonselected a:hover {
                    border: 1px solid #B7B7B7;
                    background: #F0F0F0 url(../Images/tabbg.gif) 0 -36px repeat-x;
                }

            .tabs .tabselected a {
                float: left;
                font: normal 12px Verdana;
                text-decoration: none;
                position: relative;
                padding: 11px 16px;
                color: #fff;
                border-radius: 3px 3px 0 0;
                margin-left: 2px;
                top: 0px;
                cursor: pointer;
                font-weight: bold;
                background: #4ca9e3;
                border: 1px solid #B7B7B7;
                border-bottom-color: white;
                border-bottom: none;
            }

        .tabcontents {
            background-color: #fff;
            padding: 10px;
            border: 1px solid #D7D7D7;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">

        <div class="row">
            <div class="col-md-12">
                <ul class="tabs">
                    <li id="Li1" onclick="ChangeDiv('1')" class="tabnonselected"><a>Page Group</a></li>
                    <li id="Li2" onclick="ChangeDiv('2')" class="tabnonselected"><a>Page Details</a></li>
                    <li id="Li3" onclick="ChangeDiv('3')" class="tabnonselected" style="display:none;"><a>Page Option</a></li>
                    <li id="Li4" onclick="ChangeDiv('4')" class="tabnonselected"><a>Add Roll</a></li>
                </ul>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="tabcontents">
                        <div style="width: 100%;">
                            <asp:Label ID="lblMsg" runat="server" EnableViewState="false" ForeColor="Red"></asp:Label>
                        </div>
                        <div style="clear: both;">
                        </div>
                        <div id="div1">
                            <asp:GridView ID="gdvGroupDetail" CssClass="gdvStyle" runat="server" AutoGenerateColumns="false"
                                EmptyDataText="Sorry, there are no records found." OnRowCommand="gdvGroupDetail_RowCommand"
                                OnRowDeleting="gdvGroupDetail_RowDeleting" OnRowEditing="gdvGroupDetail_RowEditing"
                                OnRowUpdating="gdvGroupDetail_RowUpdating" ShowFooter="true" OnRowCancelingEdit="gdvGroupDetail_RowCancelingEdit"
                                GridLines="None">
                                <Columns>
                                    <asp:TemplateField HeaderStyle-CssClass="GdvHleft" ItemStyle-CssClass="GdvRleft"
                                        FooterStyle-CssClass="GdvHleft">
                                        <HeaderTemplate>
                                            S. No.
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="GdvHmiddle" ItemStyle-CssClass="GdvRmiddle"
                                        FooterStyle-CssClass="GdvHmiddle">
                                        <HeaderTemplate>
                                            Group Name
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hfGroupID" runat="server" Value='<%# Eval("GroupID")%>' />
                                            <%# Eval("GroupName")%>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:HiddenField ID="hfGroupID" runat="server" Value='<%# Eval("GroupID")%>' />
                                            <asp:TextBox ID="txtEditGroupName" Width="200px" CssClass="form-control" runat="server" Text='<%# Eval("GroupName")%>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterTemplate>

                                            <asp:TextBox ID="txtGroupName" Width="200px" CssClass="form-control" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtGroupName"
                                                runat="server" CssClass="requare" ValidationGroup="GroupDetail" ErrorMessage="Invalid Group Name"
                                                SetFocusOnError="true">*</asp:RequiredFieldValidator>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="GdvHmiddle" ItemStyle-CssClass="GdvRmiddle"
                                        FooterStyle-CssClass="GdvHmiddle">
                                        <HeaderTemplate>
                                            Group Detail
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# Eval("GroupDetail")%>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtEditGroupDetail" Width="200px" CssClass="form-control" runat="server" Text='<%# Eval("GroupDetail")%>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtGroupDetail" Width="200px" CssClass="form-control" runat="server"></asp:TextBox>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="GdvHmiddle" ItemStyle-CssClass="GdvRmiddle"
                                        FooterStyle-CssClass="GdvHmiddle">
                                        <HeaderTemplate>
                                            Group Sequence
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# Eval("GroupSequence")%>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtEditGroupSequence" Width="200px" CssClass="form-control" onkeypress="return isNumberKey(event)" runat="server"
                                                Text='<%# Eval("GroupSequence")%>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtGroupSequence" Width="200px" CssClass="form-control" onkeypress="return isNumberKey(event)" runat="server"></asp:TextBox>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="GdvHright" ItemStyle-CssClass="GdvRright"
                                        FooterStyle-CssClass="GdvHright">
                                        <HeaderTemplate>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnEdit" CssClass="text-primary" runat="server" CommandName="Edit">Edit</asp:LinkButton>&nbsp;|&nbsp;
                                        <asp:LinkButton ID="lbtnDelete" CssClass="text-danger" runat="server" CommandName="Delete">Delete</asp:LinkButton>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:LinkButton ID="lbtnUpdate" CssClass="text-primary" runat="server" CommandName="Update">Update</asp:LinkButton>&nbsp;|&nbsp;
                                        <asp:LinkButton ID="lbtnCancel" CssClass="text-danger" runat="server" CommandName="Cancel">Cancel</asp:LinkButton>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lbtnAdd" runat="server" CssClass="text-danger" ValidationGroup="GroupDetail" CommandName="AddGroup">Add Group</asp:LinkButton>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:ValidationSummary ID="ValidationSummary1" DisplayMode="BulletList" ShowSummary="false"
                                ShowMessageBox="true" ValidationGroup="GroupDetail" runat="server" />
                        </div>
                        <div id="div2" style="display: none;">
                            <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" style="margin-top: 10px; margin-bottom: 10px;">
                                <tr>
                                    <td colspan="4">
                                        <b>Page Details Module</b>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td width="180px" height="30px">Page Name
                                    </td>
                                    <td width="260px">
                                        <asp:TextBox ID="txtPageName" CssClass="form-control" runat="server" Width="222px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtPageName"
                                            runat="server" ValidationGroup="PageAdd" ErrorMessage="Invalid Page Name" SetFocusOnError="true">*</asp:RequiredFieldValidator>
                                    </td>
                                    <td width="180px" height="30px">Page Url
                                    </td>
                                    <td width="260px">
                                        <asp:TextBox ID="txtPageUrl" CssClass="form-control" runat="server" Width="222px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtPageUrl"
                                            runat="server" ValidationGroup="PageAdd" ErrorMessage="Invalid Page url" SetFocusOnError="true">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td height="30px">Option Description
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPageDetail" CssClass="form-control" runat="server" Width="222px"></asp:TextBox>
                                    </td>
                                    <td>Group Detail
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlGroup" CssClass="form-control" runat="server" Width="230px">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="ddlGroup"
                                            runat="server" ValidationGroup="PageAdd" ErrorMessage="Please Choose Page Group."
                                            SetFocusOnError="true" InitialValue="Choose Group">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:Label ID="Label1" runat="server" EnableViewState="false" ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" align="center">
                                        <asp:Button ID="btnAdd" Text="Add" runat="server" CssClass="btn btn-primary" ValidationGroup="PageAdd"
                                            AlternateText="Add" OnClick="btnAdd_Click" />
                                        <asp:Button ID="btnSearch" Text="Search" CssClass="btn btn-danger" runat="server"
                                            ValidationGroup="OptionDetail" AlternateText="Search" OnClick="btnSearch_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">&nbsp;
                                    <asp:ValidationSummary ID="ValidationSummary2" DisplayMode="BulletList" ShowSummary="false"
                                        ShowMessageBox="true" ValidationGroup="PageAdd" runat="server" />
                                        <asp:ValidationSummary ID="ValidationSummary3" DisplayMode="BulletList" ShowSummary="false"
                                            ShowMessageBox="true" ValidationGroup="OptionDetail" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="padding-top: 10px;">
                                        <asp:GridView ID="gdvOptionDetail" CssClass="gdvStyle" runat="server" AutoGenerateColumns="false"
                                            EmptyDataText="Sorry, there are no records found as per your searhing criteria."
                                            GridLines="None" OnRowCancelingEdit="gdvOptionDetail_RowCancelingEdit" OnRowDataBound="gdvOptionDetail_RowDataBound"
                                            OnRowDeleting="gdvOptionDetail_RowDeleting" OnRowEditing="gdvOptionDetail_RowEditing"
                                            OnRowUpdating="gdvOptionDetail_RowUpdating">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S. No." HeaderStyle-CssClass="GdvHleft" ItemStyle-CssClass="GdvRleft">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Label ID="lblEditSNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Page Name" HeaderStyle-CssClass="GdvHmiddle" ItemStyle-CssClass="GdvRmiddle">
                                                    <ItemTemplate>
                                                        <%# Eval("PageName")%>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:HiddenField ID="hfEditPageID" runat="server" Value='<%# Eval("PageID")%>' />
                                                        <asp:TextBox ID="txtEditPageName" runat="server" Width="130px" Text='<%# Eval("PageName")%>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Page Url" HeaderStyle-CssClass="GdvHmiddle" ItemStyle-CssClass="GdvRmiddle">
                                                    <ItemTemplate>
                                                        <%# Eval("PageUrl")%>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtEditPageUrl" runat="server" Width="190px" Text='<%# Eval("PageUrl")%>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Page detail" HeaderStyle-CssClass="GdvHmiddle" ItemStyle-CssClass="GdvRmiddle">
                                                    <ItemTemplate>
                                                        <%# Eval("PageDesc")%>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtEditPageDescription" runat="server" Text='<%# Eval("PageDesc")%>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="User Group" HeaderStyle-CssClass="GdvHmiddle" ItemStyle-CssClass="GdvRmiddle">
                                                    <ItemTemplate>
                                                        <%# Eval("GroupName")%>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:DropDownList ID="ddlEditGroup" runat="server" Width="120px">
                                                        </asp:DropDownList>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="" HeaderStyle-CssClass="GdvHright" ItemStyle-CssClass="GdvRright">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtnEdit" runat="server" CssClass="linkbutton3" CommandName="Edit">Edit</asp:LinkButton>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:LinkButton ID="lbtnEdit" runat="server" CssClass="linkbutton3" CommandName="Update">Update</asp:LinkButton>&nbsp;|&nbsp;
                                                    <asp:LinkButton ID="lbtnDelete" runat="server" CssClass="linkbutton3" CommandName="Cancel">Cancel</asp:LinkButton>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="div3" style="display: none;">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td>Group Detail
                                    </td>
                                    <td>
                                        <asp:DropDownList CssClass="form-control" ID="ddlGroupName" AutoPostBack="true" Width="200px" runat="server" OnSelectedIndexChanged="ddlGroupName_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="ddlGroupName"
                                            runat="server" ValidationGroup="PageOption" ErrorMessage="Invalid Page Group" SetFocusOnError="true">*</asp:RequiredFieldValidator>
                                    </td>
                                    <td>Choose Page
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlPageName" CssClass="form-control" Width="200px" runat="server">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="ddlPageName"
                                            runat="server" ValidationGroup="PageOption" ErrorMessage="Invalid Page Name" SetFocusOnError="true">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Page Option Code
                                    </td>
                                    <td>
                                        <asp:TextBox CssClass="form-control" ID="txtOptionCode" Width="200px" runat="server">
                                        </asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtOptionCode"
                                            runat="server" ValidationGroup="PageOption" ErrorMessage="Empty Option Code Is Not Allowed!!" SetFocusOnError="true">*</asp:RequiredFieldValidator>
                                    </td>
                                    <td>Page Option Name
                                    </td>
                                    <td>
                                        <asp:TextBox CssClass="form-control" ID="txtOptionName" Width="200px" runat="server">
                                        </asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="txtOptionName"
                                            runat="server" ValidationGroup="PageOption" ErrorMessage="Empty Option Code Is Not Allowed!!" SetFocusOnError="true">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Page Option Description
                                    </td>
                                    <td>
                                        <asp:TextBox CssClass="form-control" TextMode="MultiLine" ID="txtOptionDescription" Width="200px" runat="server">
                                        </asp:TextBox>

                                    </td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td colspan="4">&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="text-align: left; padding-left: 10px;">
                                        <asp:Label ID="lblMsg3" EnableViewState="false" ForeColor="Red" runat="server" Font-Bold="true"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnAddOption" CssClass="btn btn-danger" Text="ADD" runat="server" ValidationGroup="PageOption"
                                            OnClick="btnAddOption_Click" />
                                        <asp:Button ID="btnSearchOption" runat="server" Text="Search" CssClass="btn btn-primary"
                                            OnClick="btnSearchOption_Click" />
                                        <asp:ValidationSummary ID="ValidationSummary4" DisplayMode="BulletList" ShowSummary="false"
                                            ShowMessageBox="true" ValidationGroup="PageOption" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:GridView ID="gdvPageOptionDetail" CssClass="gdvStyle" runat="server" AutoGenerateColumns="false"
                                            EmptyDataText="Sorry, there are no records found as per your searhing criteria."
                                            GridLines="None" OnRowCancelingEdit="gdvPageOptionDetail_RowCancelingEdit" OnRowDataBound="gdvPageOptionDetail_RowDataBound"
                                            OnRowDeleting="gdvPageOptionDetail_RowDeleting" OnRowEditing="gdvPageOptionDetail_RowEditing"
                                            OnRowUpdating="gdvPageOptionDetail_RowUpdating">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S. No." HeaderStyle-CssClass="GdvHleft" ItemStyle-CssClass="GdvRleft">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Label ID="lblEditSNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Page Name" HeaderStyle-CssClass="GdvHmiddle" ItemStyle-CssClass="GdvRmiddle">
                                                    <ItemTemplate>
                                                        <%# Eval("PageName")%>
                                                        <asp:HiddenField ID="hfEditPageOptionID" runat="server" Value='<%# Eval("OptionID")%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Option Code" HeaderStyle-CssClass="GdvHmiddle" ItemStyle-CssClass="GdvRmiddle">
                                                    <ItemTemplate>
                                                        <%# Eval("OptionName")%>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>                                                       
                                                        <asp:Label ID="txtEditOptionCode" runat="server" Text='<%# Eval("OptionName")%>'></asp:Label>                                                      
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Option Name" HeaderStyle-CssClass="GdvHmiddle" ItemStyle-CssClass="GdvRmiddle">
                                                    <ItemTemplate>
                                                        <%# Eval("OptionFullName")%>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtEditOptionFullName" runat="server" Width="190px" Text='<%# Eval("OptionFullName")%>'></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="txtEditOptionFullName"
                                                            runat="server" ValidationGroup="EditPageOption" ErrorMessage="Empty Option Name Is Not Allowed!!" SetFocusOnError="true">*</asp:RequiredFieldValidator>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Option Description" HeaderStyle-CssClass="GdvHmiddle" ItemStyle-CssClass="GdvRmiddle">
                                                    <ItemTemplate>
                                                        <%# Eval("OptionDescription")%>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtEditOptionDescription" TextMode="MultiLine" runat="server" Text='<%# Eval("OptionDescription")%>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="" HeaderStyle-CssClass="GdvHright" ItemStyle-CssClass="GdvRright">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtnEdit" runat="server" CssClass="linkbutton3" CommandName="Edit">Edit</asp:LinkButton>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:LinkButton ID="lbtnEdit" runat="server" CssClass="linkbutton3" CommandName="Update">Update</asp:LinkButton>&nbsp;|&nbsp;
                                                    <asp:LinkButton ID="lbtnDelete" runat="server" CssClass="linkbutton3" CommandName="Cancel">Cancel</asp:LinkButton>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <asp:ValidationSummary ID="ValidationSummary5" DisplayMode="BulletList" ShowSummary="false"
                                            ShowMessageBox="true" ValidationGroup="EditPageOption" runat="server" />
                                        <%--<asp:GridView ID="gdvGroupID" CssClass="gdvStyle" ShowHeader="false" runat="server"
                                        AutoGenerateColumns="false" OnRowDataBound="gdvGroupID_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <table width="100%" class="gdvStyle" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td class="GdvHleft">
                                                                <%# Eval("GroupName")%>
                                                                <asp:HiddenField ID="hfGroupID" Value='<%# Eval("GroupID")%>' runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="inlineText">
                                                                <asp:CheckBoxList ID="chblGDetail" Width="100%" CellPadding="10" CellSpacing="20"
                                                                    runat="server" RepeatColumns="4">
                                                                </asp:CheckBoxList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="div4">
                            <asp:GridView ID="gdvRollMst" CssClass="gdvStyle" runat="server" AutoGenerateColumns="false"
                                EmptyDataText="Sorry, there are no records found." OnRowCommand="gdvRollMst_RowCommand"
                                OnRowDeleting="gdvRollMst_RowDeleting" OnRowEditing="gdvRollMst_RowEditing"
                                OnRowUpdating="gdvRollMst_RowUpdating" ShowFooter="true" OnRowCancelingEdit="gdvRollMst_RowCancelingEdit"
                                GridLines="None">
                                <Columns>
                                    <asp:TemplateField HeaderStyle-CssClass="GdvHleft" ItemStyle-CssClass="GdvRleft"
                                        FooterStyle-CssClass="GdvHleft">
                                        <HeaderTemplate>
                                            S. No.
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="GdvHmiddle" ItemStyle-CssClass="GdvRmiddle"
                                        FooterStyle-CssClass="GdvHmiddle">
                                        <HeaderTemplate>
                                            Roll Name
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hfRollID" runat="server" Value='<%# Eval("MstID")%>' />
                                            <%# Eval("MstName")%>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:HiddenField ID="hfRollID" runat="server" Value='<%# Eval("MstID")%>' />
                                            <asp:TextBox ID="txtEditRollName" Width="200px" CssClass="form-control" runat="server" Text='<%# Eval("MstName")%>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtRollName" Width="200px" CssClass="form-control" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtRollName"
                                                runat="server" CssClass="requare" ValidationGroup="RollDetail" ErrorMessage="Invalid Roll Name"
                                                SetFocusOnError="true">*</asp:RequiredFieldValidator>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="GdvHmiddle" ItemStyle-CssClass="GdvRmiddle"
                                        FooterStyle-CssClass="GdvHmiddle">
                                        <HeaderTemplate>
                                            Roll Detail
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# Eval("MstDescription")%>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtEditRollDetail" Width="200px" CssClass="form-control" runat="server" Text='<%# Eval("MstDescription")%>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtRollDetail" Width="200px" CssClass="form-control" runat="server"></asp:TextBox>
                                        </FooterTemplate>
                                    </asp:TemplateField>                                    
                                    <asp:TemplateField HeaderStyle-CssClass="GdvHright" ItemStyle-CssClass="GdvRright"
                                        FooterStyle-CssClass="GdvHright">
                                        <HeaderTemplate>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnEdit" CssClass="text-primary" runat="server" CommandName="Edit">Edit</asp:LinkButton>&nbsp;|&nbsp;
                                        <asp:LinkButton ID="lbtnDelete" CssClass="text-danger" runat="server" CommandName="Delete">Delete</asp:LinkButton>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:LinkButton ID="lbtnUpdate" CssClass="text-primary" runat="server" CommandName="Update">Update</asp:LinkButton>&nbsp;|&nbsp;
                                        <asp:LinkButton ID="lbtnCancel" CssClass="text-danger" runat="server" CommandName="Cancel">Cancel</asp:LinkButton>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lbtnAdd" runat="server" CssClass="text-danger" ValidationGroup="RollDetail" CommandName="AddRoll">Add Roll</asp:LinkButton>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:ValidationSummary ID="ValidationSummary6" DisplayMode="BulletList" ShowSummary="false"
                                ShowMessageBox="true" ValidationGroup="RollMstDetail" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hfTab" runat="server" Value="1" />
</asp:Content>

