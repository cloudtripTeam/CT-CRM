<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="ticketIssuedByDetails.aspx.cs" Inherits="Admin_ticketIssuedByDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

     <asp:Button ID="btnExport" runat="server" Text="Export"
                                OnClick="btnExport_Click" Visible="false" />
    <asp:Repeater ID="rptIssuedBy" runat="server">
                <HeaderTemplate>
                    

                    <table class="table table-striped  table-hover table-bordered">
                        <thead>
                            <tr>
                                <th class="gdvh">S#</th>
                                <th class="gdvh">Booking Ref</th>
                                <th class="gdvh" >Sell Amount</th>
                                <th class="gdvh">Cost Amount</th>
                                <th class="gdvh">Supplier</th>
                                

                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td class='gdvr'><span><%# Container.ItemIndex+1%></span></td>
                        <td class='gdvr'>
                            <asp:Literal ID="Label1" runat="server" Text='<%#Bind("Booking_ID") %>'></asp:Literal>
                        </td>
                      
                        <td class='gdvr'>
                            <asp:Literal ID="Label10" runat="server" Text='<%# Eval("CostPrice") %>'></asp:Literal></td>

                        <td class='gdvr'>
                            <asp:Literal ID="Label11" runat="server" Text='<%# Eval("SellPrice") %>'></asp:Literal></td>
                        <td class='gdvr'>
                            <asp:Literal ID="Label13" runat="server" Text='<%# Eval("SUP_Name") %>'></asp:Literal></td>

                       

                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    <tr>
                        <td></td>
                        <td>Total</td>
                        <td>  <asp:Literal ID="ltrTotalcost" runat="server"></asp:Literal></td>
                       <td>  <asp:Literal ID="ltrTotalSell" runat="server"></asp:Literal></td>
                        <td></td>
                    </tr>
                    </tbody>
                    </table>
                </FooterTemplate>
            </asp:Repeater>

</asp:Content>

