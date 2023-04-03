<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="SearchInRemarks.aspx.cs" Inherits="Admin_SearchInRemarks" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <%--<link href="../css/StyleSheet.css" rel="stylesheet" />--%>
    <link href="../../css/StyleSheet.css" rel="stylesheet" />
    <script src="https://www.traveljunction.co.uk/temp/CalendarAnyYear.js"></script>
     <link href="//cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/css/bootstrap-multiselect.css" rel="stylesheet" type="text/css" />
    <script src="//cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/js/bootstrap-multiselect.js" type="text/javascript"></script>
    <script type="text/javascript">
       
        $(function () {
            $('[id*=ddlCompany]').multiselect({
                includeSelectAllOption: true
            });
        });
    </script>
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">  
    <div class="p-20" style="background: #fff;">    
                <table class="table table-hover">
                    <thead>
                        <tr>
                            <th>Remarks</th>
                            
                            <th>Agent</th>
                            <th>Remarks From Date</th>
                            <th>Remarks To Date</th>
                          <th>Booking Ref. No.</th>
                            <th>Booking Booking</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                           <td>
                               <asp:TextBox ID="txtRemarks" placeholder="Remarks" CssClass="form-control"  MaxLength="250" runat="server"></asp:TextBox></td>
                            <td>
                                <asp:TextBox ID="txtAgent" placeholder="Agent" CssClass="form-control" runat="server"></asp:TextBox></td>
                            <td>
                                <asp:TextBox ID="txtFromDate" onclick="showCalender(this);" placeholder="From Date" CssClass="form-control" runat="server"></asp:TextBox></td>
                            <td>
                                <asp:TextBox ID="txtToDate" onclick="showCalender(this);" placeholder="Date Till" CssClass="form-control" runat="server"></asp:TextBox></td>
                             <td>
                                <asp:TextBox ID="txtInvNo" placeholder="reference no" CssClass="form-control" runat="server"></asp:TextBox></td>
                            
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
                              
                                <asp:Literal ID="ltrInvc" runat="server"></asp:Literal>
                                
                                <asp:GridView ID="gvAssignedBooking" Width="100%" runat="server" EmptyDataText="No Record Found." AutoGenerateColumns="false" >
                                    <Columns>
                                        <asp:BoundField DataField="Ref_No" HeaderStyle-CssClass="gdvh" ItemStyle-CssClass="gdvr"  HeaderText="Booking Ref"  />
                                        <asp:BoundField DataField="Remarks" HeaderStyle-CssClass="gdvh" ItemStyle-CssClass="gdvr" HeaderText="Remarks" />
                                       <asp:BoundField DataField="Remarks_By" HeaderStyle-CssClass="gdvh" ItemStyle-CssClass="gdvr"  HeaderText="Remarks By"  />   
                                        <asp:BoundField DataField="Booking_Date" HeaderStyle-CssClass="gdvh" ItemStyle-CssClass="gdvr"  HeaderText="Booking Date"  />
                                        <asp:BoundField DataField="Remarks_DatenTime"  DataFormatString="{0:dd MMM yy}" HeaderStyle-CssClass="gdvh" ItemStyle-CssClass="gdvr"  HeaderText="DatenTime"  />
                                       
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

