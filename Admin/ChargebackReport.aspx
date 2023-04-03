<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="ChargebackReport.aspx.cs" Inherits="ChargebackReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <link href="../../css/StyleSheet.css" rel="stylesheet" />
    <script src="https://www.traveljunction.co.uk/temp/CalendarAnyYear.js"></script>
    <link rel="stylesheet" href="https://pro.fontawesome.com/releases/v5.10.0/css/all.css"/>
     <style type="text/css">
        .exelcss input[type=text], textarea {
            border: 1px solid #ccc !important;
            text-transform: uppercase;
        }
            .exelcss input[type=text]:focus {
                border: 1px solid #ff0000 !important;
            }
         
         #CalendarControl td {
          text-align: center;
          padding: 2.5px !important;
         }
            
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <input id="setascurrdate" type="hidden" />
    <input id="hdeprdate" type="hidden" />
      <div class="container">
        <div style="height: 15px;"></div>
        <div class="panel panel-default">         
            <div class="panel-heading">Chargeback Reports</div>
      <div class="container">
          <asp:HiddenField runat="server"  ID="hdnate" />
        <div class="panel-body" style="line-height: 34px;">
              <div class="col-md-12">
                <div class="row">
                   <div class="col-md-4">
                        <div class="form-group form-inline">
                       
                            <div class="col-md-4">
                            <label>XPID</label>
                                </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtDisbuteAmunt" AutoComplete="off" ClientIDMode="Static" onkeypress="return numeric(event)" runat="server" CssClass="form-control" PlaceHolder="Disputed Amount"></asp:TextBox>
                            </div>
                            </div>
                    </div>
                    <div class="col-md-4">
                    <div class="form-group form-inline">
                        <div class="col-md-4"><label> Received Date</label></div>
                        <div class="col-md-4">
                        <asp:TextBox ID="txtChegRecivDate" ClientIDMode="Static" AutoComplete="off" runat="server" onclick="showCalender(this);" placeholder="dd/mm/yyyy"  CssClass="form-control" ></asp:TextBox>
<%--                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtChegRecivDate" ForeColor="Red" ValidationGroup="INS" runat="server" ErrorMessage="Chargeback Received Date"></asp:RequiredFieldValidator>--%>
                           </div>                   
                     </div>
                        </div>
                    <div class="col-md-4">
                     <div class="form-group form-inline">
                        <div class="col-md-4"><label>Dispute Date</label></div>
                         <div class="col-md-4"><asp:TextBox ID="txtDisputeDate" ClientIDMode="Static" AutoComplete="off" runat="server" onclick="showCalender(this);" CssClass="form-control" placeholder="dd/mm/yyyy" ValidationGroup="INS" ></asp:TextBox>
                      </div>
                   </div>
                </div>
                 
                    </div>
                  </div>
                <div class="col-md-12">
                    &nbsp;
                        <asp:Label ID="lblMsg" runat="server" ForeColor="Red" EnableViewState="false"></asp:Label>
                </div>
                <div class="row col-md-offset-4"">
                    <div class="col-md-6">
                                        
                         <asp:Button ID="btnExport" runat="server" Text="Export" CssClass="btn btn-primary btn-lg"   OnClick="btnExport_Click"  />
                         <asp:Button ID="btnSerch" runat="server" Text="Search" CssClass="btn btn-primary btn-lg"   OnClick="btnSerch_Click"  />
                    </div>
                </div>
            </div>
          
        </div>
            <div class="clearfix"></div>
          </div>     
    </div>
       <div class="container">
        <div style="height: 15px;"></div>
        <div class="panel panel-default">
            <div class="panel-heading">Details</div>
            <div class="panel-body" style="line-height: 10px; padding: 0px!important;">             
         <asp:Repeater ID="rptr" runat="server">
               <HeaderTemplate>
                        <table width='100%' cellpadding='0' cellspacing='0' class="table"  style='margin-bottom: 0px;'>
                            <tr>
                                 <td class='gdvh'>Booking ID</td>
                                <td class='gdvh'>Chargeback Received Date</td>
                                <td class='gdvh'>Chargeback Dispute Date</td>
                                <td class='gdvh'>Chargeback Type</td>
                                <td class='gdvh'>Card Type</td>
                                <td class='gdvh'>Disputed Amount</td>
                                <td class='gdvh'>Chargeback Reason Code</td>
                                <td class='gdvh'>Chargeback Status Pending</td>
                                <td class='gdvh'>Document Type</td>                                                            
                                <td class='gdvh'>Case No</td>                                                            
                            </tr>
                        
                    </HeaderTemplate>
                    <ItemTemplate>
                            <tr id='tr<%# Eval("ID")%>'>
                         
                            <td class='gdvr'><%# Eval("Booking_ID")%></td>                                                                         
                            <td class='gdvr'><%# Eval("Chargeback_Received")%></td>
                            <td class='gdvr'><%# Eval("ChargebackDisputereport")%></td>
                            <td class='gdvr'><%# Eval("Chargeback_Type")%></td>
                            <td class='gdvr'><%# Eval("Chargeback_Dispute")%></td>
                            <td class='gdvr'><%# Eval("Disputed_Amount")%></td>                    
                            <td class='gdvr'><%# Eval("Chargeback_Reason")%></td>
                            <td class='gdvr'><%# Eval("Chargeback_Status")%></td>                                                                       
                            <td class='gdvr'><%# Eval("DocumentType")%></td>    
                            <td class='gdvr'><%# Eval("CaseNo")%></td>                                                              
                          <%--  <td><asp:Button runat="server" CommandName="ED" CommandArgument='<%# Eval("ID")%>' Text="Edit"/></td>   --%>
                            </tr>
                    </ItemTemplate>                  
                </asp:Repeater>
             </div>
            </div>
           </div>
</asp:Content>

