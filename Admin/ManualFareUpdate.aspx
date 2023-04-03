<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="ManualFareUpdate.aspx.cs" Inherits="Admin_ManualFareUpdate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">
        function DeleteConfirmation() {
            if (confirm("Are you sure delete ?") == true) return true;
            else
                return false;
        }
    </script>

    <table width="100%" border="0" align="center" cellpadding="0" style="border: #1c6fbd solid 1px;"
        cellspacing="0" bgcolor="#FFFFFF">
        <tr>
            <td>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td height="38" align="left" valign="top">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td height="38" align="center" valign="top">
                            <table width="80%" style="font-family: Arial; font-size: 12px">
                                <tr>
                                    <td valign="top">Operator::
                                        <asp:DropDownList ID="ddloperator" EnableViewState="true" runat="server">
                                            <asp:ListItem>All</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td valign="top">From::
                                        <asp:TextBox ID="txtFrom" runat="server"></asp:TextBox>
                                    </td>
                                    <td valign="top">To::
                                        <asp:TextBox ID="txtTo" runat="server"></asp:TextBox>
                                    </td>
                                    <td></td>
                                    <td></td>
                                    <td>
                                        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"
                                            BackColor="#22515F" ForeColor="White" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <tr>
                            <td align="left" valign="top"></td>
                        </tr>
                    </tr>
                    <tr>
                        <td align="center">
                            <table width="100%">
                                <tr>
                                    <td valign="top">
                                        <asp:GridView ID="gvFareSheet" runat="server" AutoGenerateColumns="False" Width="100%">
                                            <Columns>
                                                <asp:TemplateField>

                                                    <ItemTemplate>
                                                        <table cellpadding="0" cellspacing="0" width="100%" align="center" style="border-style: groove outset outset inset; border-width: 1px;">
                                                            <tr>
                                                                <td width="5px">
                                                                    <asp:Label ID="lblFlightID" runat="server" Visible="false" Text='<%#Eval("FrID")%>'></asp:Label>
                                                                </td>
                                                                <td align="center">
                                                                    <asp:TextBox ID="txtO" Width="30px" runat="server" Text='<%#Eval("AirIATA")%>'></asp:TextBox>
                                                                    <asp:TextBox ID="txtOName" runat="server" Text='<%#Eval("AirName")%>'></asp:TextBox>
                                                                </td>
                                                                <td align="center">
                                                                    <asp:TextBox ID="txtFrom" MaxLength="3" Width="30px" runat="server" Text='<%#Eval("FrIATA")%>'></asp:TextBox>
                                                                    <asp:TextBox ID="txtFromF" runat="server" Text='<%#Eval("FrName")%>'></asp:TextBox>
                                                                </td>
                                                                <td align="center">
                                                                    <asp:TextBox ID="txtTo" MaxLength="3" Width="30px" runat="server" Text='<%#Eval("ToIATA")%>'></asp:TextBox>
                                                                    <asp:TextBox ID="txtToF" runat="server" Text='<%#Eval("ToName")%>'></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="lblAdultTotal" runat="server" Text='<%#Eval("Price")%>' Width="60px"></asp:TextBox>
                                                                </td>


                                                                <td>
                                                                    <asp:TextBox ID="lblValidFrom" runat="server" Width="70px" Text='<%#Eval("VFrom","{0:d-MM-yyyy}")%>'></asp:TextBox>

                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="lblValidTo" runat="server" Width="70px" Text='<%#Eval("VTill","{0:d-MM-yyyy}")%>'></asp:TextBox>

                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="lblJType" runat="server" Width="20px" Text='<%#Eval("JType")%>'></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="lblArrday" runat="server" Width="20px" Text='<%#Eval("ArrDay")%>'></asp:TextBox>
                                                                </td>

                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkSelect" runat="server" />
                                                    </ItemTemplate>
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="chkBxHeader" runat="server" AutoPostBack="true" OnCheckedChanged="chkBxHeader_CheckedChanged" />
                                                    </HeaderTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <asp:Button ID="btnUpdate" runat="server" Visible="false" Text="Update" BackColor="#22515F"
                                            ForeColor="White" OnClick="btnUpdate_Click" />
                                        <asp:Label ID="lblMsg" runat="server" Visible="true"></asp:Label>
                                    </td>
                                </tr>
                            </table>


                        </td>
                    </tr>
                    <tr>
                        <td align="left" valign="top"></td>
                    </tr>
                    <tr>
                        <td>
                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>&nbsp;</td>
                                    <td align="right">&nbsp;</td>
                                    <td align="right">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" Visible="false"
                                            BackColor="#22515F" ForeColor="White" Text="Delete Record" OnClientClick="return DeleteConfirmation();" />
                                    </td>
                                    <td align="left">
                                        <asp:Button ID="btnADD" runat="server" BackColor="#22515F" ForeColor="White" Visible="false"
                                            Text="Add More Record" OnClick="btnADD_Click" />
                                    </td>
                                    <td align="right">&nbsp;</td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

