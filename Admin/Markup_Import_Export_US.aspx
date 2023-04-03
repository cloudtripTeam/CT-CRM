<%@ Page Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true"
    CodeFile="Markup_Import_Export_US.aspx.cs" Inherits="OnlineMarkup_Markup_Import_Export_US"
    Title="Untitled Page" Debug="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <div style="height: 15px;"></div>
        <div class="panel panel-default" id="pnlbulkfare" runat="server">
            <div class="panel-heading">Excel MarkUp</div>
            <div class="panel-body" style="line-height: 34px;">
                <table width="100%" cellpadding="0px" cellspacing="0px">
                    <tr>
                        <td >
                            <asp:FileUpload ID="FileUpload1" runat="server" Width="200px" CssClass="active" Height="36px" />
                        </td>
                        <td >
                            <asp:Button ID="btnUpload" runat="server" Text="Upload Markup" OnClick="btnUpload_Click" />
                        </td>
                         <td >
                            <asp:Button ID="btnExport" runat="server" Text="Export Markup"
                                OnClick="btnExport_Click" />
                        </td>
                         <td >
                            <asp:Button ID="btnDeleteAll" runat="server" OnClientClick="return confirm('Are you sure delete all markup?')" Text="Delete All Markup"
                                OnClick="btnDeleteAll_Click" />
                        </td>
                    </tr>                    
                    <tr>
                        <td style="text-align: left; padding-left: 20px;" colspan="3">
                            <asp:Label ID="lblMsg" EnableViewState="false" ForeColor="Red" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>

</asp:Content>
