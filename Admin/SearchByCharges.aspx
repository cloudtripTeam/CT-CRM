<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="SearchByCharges.aspx.cs" Inherits="Admin_SearchByCharges" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <%--<link href="../css/StyleSheet.css" rel="stylesheet" />--%>
    <link href="../../css/StyleSheet.css" rel="stylesheet" />
    <script src="https://www.traveljunction.co.uk/temp/CalendarAnyYear.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

     <div class="p-20" style="background: #fff;">    
                <table class="table table-hover">
                    <thead>
                        <tr>
                            <th>Charges Type</th>                            
                            <th>From Date</th>
                            <th>To Date</th>
                            <th></th>
                            
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>
                               <asp:DropDownList ID="ddlChargesType" class="form-control mb-10"  runat="server">
                                    <asp:ListItem  Value="Fare">Fare</asp:ListItem>
                                            <asp:ListItem Value="Tax">Tax</asp:ListItem>
                                            <asp:ListItem Value="Markup">Markup</asp:ListItem>
                                            <asp:ListItem Value="Safi">Safi</asp:ListItem>
                                            <asp:ListItem Value="Atol">Atol</asp:ListItem>
                                            <asp:ListItem Value="CC">Card Charge</asp:ListItem>
                                            <asp:ListItem Value="Admin">Admin Charge</asp:ListItem>
                                           
                                            <asp:ListItem Value="PTS">PTS</asp:ListItem>
                                            <asp:ListItem Value="Issuance">Issuance Charge</asp:ListItem>
                                            <asp:ListItem Value="Others">Others</asp:ListItem>
                                            <asp:ListItem Value="Refund">Refund</asp:ListItem>
                                            <asp:ListItem Value="GTT"  Selected="True">GTT</asp:ListItem>
<asp:ListItem Value="FXL">FXL</asp:ListItem>
                                </asp:DropDownList></td>
                            
                            <td>
                                <asp:TextBox ID="txtFromDate" onclick="showCalender(this);" placeholder="From Date" CssClass="form-control" runat="server"></asp:TextBox></td>
                            <td>
                                <asp:TextBox ID="txtToDate" onclick="showCalender(this);" placeholder="Date Till" CssClass="form-control" runat="server"></asp:TextBox></td>
                           
                            <td>
                                <asp:Button ID="btnFind" Text="Search" CssClass="btn btn-default" runat="server" OnClick="btnFind_Click" /></td>
                            <input id="setascurrdate" type="hidden" />
                            <input id="hdeprdate" type="hidden" />
                        </tr>
                    </tbody>
                </table>
                <hr />
                <div class="row">
                    <table class="table table-hover">
                        <tr>
                            <td>
                                <asp:Button ID="btnExport" runat="server" Text="Export"
                                OnClick="btnExport_Click" Visible="false" />
                                <asp:Literal ID="ltrInvc" runat="server"></asp:Literal>
                                
                                <asp:GridView ID="gvChargesBy" Width="100%" runat="server" EmptyDataText="No Record Found." AutoGenerateColumns="false">
                                    <Columns>                                        
                                        <asp:BoundField DataField="Booking_ID" HeaderStyle-CssClass="gdvh" ItemStyle-CssClass="gdvr" HeaderText="Booking Ref No." />
<asp:BoundField DataField="PNR" HeaderStyle-CssClass="gdvh" ItemStyle-CssClass="gdvr" HeaderText="PNR" />
                                        <asp:BoundField DataField="Booking_Date_Time"  DataFormatString="{0:dd MMM yy}" HeaderStyle-CssClass="gdvh" ItemStyle-CssClass="gdvr"  HeaderText="Booking Date"  />
                                         <asp:BoundField DataField="Charge_ID" HeaderStyle-CssClass="gdvh" ItemStyle-CssClass="gdvr"  HeaderText="Charge Type"  />                                        
                                        <asp:BoundField DataField="Cost_Price" HeaderStyle-CssClass="gdvh" ItemStyle-CssClass="gdvr" HeaderText="Cost Price" DataFormatString = "{0:N2}" ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="Sell_Price" HeaderStyle-CssClass="gdvh" ItemStyle-CssClass="gdvr" HeaderText="Sell Price"  />
                                        <asp:BoundField DataField="Currency" HeaderStyle-CssClass="gdvh" ItemStyle-CssClass="gdvr" HeaderText="Currency"  />
                                         
                                        <asp:BoundField DataField="Booking_Status" HeaderStyle-CssClass="gdvh" ItemStyle-CssClass="gdvr" HeaderText="Booking Status"  />
                                        <asp:BoundField DataField="Company" HeaderStyle-CssClass="gdvh" ItemStyle-CssClass="gdvr" HeaderText="Company"  /> 
                                        <asp:BoundField DataField="Booking_By" HeaderStyle-CssClass="gdvh" ItemStyle-CssClass="gdvr" HeaderText="Booking By"  />
                                        <asp:BoundField DataField="ModifiedBy" HeaderStyle-CssClass="gdvh" ItemStyle-CssClass="gdvr" HeaderText="ModifiedBy"  />
                                       <asp:BoundField DataField="ModifiedDate"  DataFormatString="{0:dd MMM yy}" HeaderStyle-CssClass="gdvh" ItemStyle-CssClass="gdvr"  HeaderText="Modified Date"  />                                   
                                      
                                        
                                        
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>

                </div>
           
        </div>
    
    <style>
        .gdvr_right {
    font-size: 0.90em;
    color: #222;
    padding: 5px 3px 5px 5px;
    text-align: right;
    /* height: 20px; */
    border-bottom: 1px solid #e1e1e1;
}
         .DvDisp{
            display:none;
        }
    </style>
</asp:Content>

