<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="ManualFlightFare.aspx.cs" Inherits="Admin_ManualFlightFare" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <style type="text/css">
        .style1
        {
            width: 90%;
            margin: 0 auto;
            background-color: #CCCCCC;
        }
    </style>
    <div>
        <table class="style1">
            <tr>
                <td align="center" style="height: 50px; background-color: #364E6F; font-family: 'Arial'; font-weight: bold; color: #FFFFFF; font-size: 20px;"
                    valign="middle">Fare only
                </td>
            </tr>
            <tr>
                <td></td>
            </tr>
            <tr>
                <td>
                    <table width="100%" border="0" align="center" cellpadding="0" style="border: #1c6fbd solid 1px;"
                        cellspacing="0" bgcolor="#FFFFFF">
                        <tr>
                            <td>
                                <table width="100%" border="0" cellspacing="0" cellpadding="0" style="margin-top: 10px; margin-left: 10px; margin-right: 10px; margin-right: 10px">
                                    <tr>
                                        <td height="38" align="left" valign="top">
                                            <table width="100%" border="0" cellpadding="0" cellspacing="0" style="font-family: Arial; font-size: 12px;">
                                                <tr>
                                                    <td align="left">Find Flight List
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtFrom" runat="server"></asp:TextBox>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtTo" runat="server"></asp:TextBox>
                                                    </td>
                                                    <td align="left">
                                                        <asp:DropDownList ID="ddlAirline" runat="server">
                                                            <asp:ListItem Value="0">--All----</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="left"> <asp:DropDownList ID="ddlCompany" runat="server">
                                                           
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Button ID="btnFind" runat="server" BackColor="#22515F" ForeColor="White" Text="Find Airline"
                                                            OnClick="btnFind_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="38" align="center" valign="top">

                                            <asp:Repeater ID="rptTravelperiod" runat="server">
                                                <ItemTemplate>
                                                    <table width="96%" border="0" cellpadding="0" cellspacing="0" style="font-family: Arial; font-size: 12px;">
                                                        <tr>
                                                            <td align="left" style="width: 50px;">
                                                                <input id="chkoptT" runat="server" type="checkbox" value='<%#Eval("AirIATA") %>' /></label>
                                                            </td>
                                                            <td align="left" style="width: 150px;">
                                                                <asp:Label ID="lblOName" Text='<%# Bind("AirName") %>' runat="server"></asp:Label>
                                                            </td>
                                                            <td style="width: 100px;">
                                                                <asp:Label ID="lblFD" Text='<%# Bind("VFrom", "{0:MM/dd/yyyy}") %>' runat="server"></asp:Label>
                                                            </td>
                                                            <td style="width: 100px;">
                                                                <asp:Label ID="lblTD" Text='<%# Bind("VTill","{0:MM/dd/yyyy}") %>' runat="server"></asp:Label>
                                                            </td>
                                                            <td style="width: 100px;">
                                                                <asp:Label ID="lblFare" Text='<%# Bind("Price") %>' runat="server"></asp:Label>
                                                            </td>
                                                            <td style="width: 150px;">
                                                                <asp:Label ID="lblFN" Text='<%# Bind("FrName") %>' runat="server"></asp:Label>
                                                            </td>
                                                            <td style="width: 150px;">
                                                                <asp:Label ID="lblTN" Text='<%# Bind("ToName") %>' runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </asp:Repeater>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="top"></td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <table width="100%">
                                                <tr>
                                                    <td width="100%" valign="top" align="left" style="font-family: Arial; font-size: 12px;">
                                                        <fieldset>
                                                            <legend>Fare Details </legend>
                                                            <asp:Repeater ID="rptAirline" runat="server">
                                                                <ItemTemplate>
                                                                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td align="left" style="width: 100px;">
                                                                                <input id="chkopt" runat="server" checked="checked" type="checkbox" value='<%#Eval("AirIATA") %>' />&nbsp;<label><%#Eval("AirIATA")%></label>
                                                                            </td>
                                                                            <td align="left" style="width: 200px;">
                                                                                <asp:Label ID="lblON" Text='<%# Bind("AirName") %>' runat="server"></asp:Label>
                                                                            </td>
                                                                            <td style="width: 200px;">
                                                                                <asp:TextBox ID="txtAirline" Width="50px" runat="server" Text="0"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </fieldset>
                                                    </td>
                                                    <td valign="top">&nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table width="90%" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td></td>

                                                    <td align="right">
                                                        <asp:Button ID="btnADD" runat="server" BackColor="#22515F" ForeColor="White" Text="Replace / Add Fare"
                                                            OnClick="btnADD_Click" Height="30px" />

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td></td>

                                                    <td align="right"></td>
                                                </tr>
                                            </table>
                                            <br />
                                            <br />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

