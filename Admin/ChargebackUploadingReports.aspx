﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="ChargebackUploadingReports.aspx.cs" Inherits="ChargebackUploadingReports" %>

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
     <script type="text/javascript">       
         function validation() {
             if (document.getElementById('txtChegRecivDate').value == "") {
                 alert("Please Enter Chargeback Received Date");
                 document.getElementById('txtChegRecivDate').focus();
                 return false;
             }
             if (document.getElementById('txtDisputeDate').value == "") {
                 alert("Please Enter Dispute Date");
                 document.getElementById('txtDisputeDate').focus();
                 return false;

             }
             if (document.getElementById('txtDisbuteAmunt').value == "") {
                 alert("Please Enter Disbute Amunt");
                 document.getElementById('txtDisbuteAmunt').focus();
                 return false;

             }
             if (document.getElementById('drpCardType').value == "0") {
                 alert("Please Select Card Type：");
                 document.getElementById('drpCardType').focus();
                 return false;
             }
             if (document.getElementById('drpChargebakCode').value == "0") {
                 alert("Please Select Chargeback Reason：");
                 document.getElementById('drpChargebakCode').focus();
                 return false;
             }
                                          
             if (document.getElementById('txtCaseNo').value == "") {
                 alert("Please Enter Case Number");
                 document.getElementById('txtCaseNo').focus();
                 return false;
             }
       
         }
       
         function numeric(evt)
         {
             var charCode = (evt.which) ? evt.which : event.keyCode
             if (charCode > 31 && ((charCode >= 48 && charCode <= 57) || charCode == 46))
                 return true;
             else {
                 alert('Please Enter Numeric values.');
                 return false;
             }
         }
     </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <input id="setascurrdate" type="hidden" />
    <input id="hdeprdate" type="hidden" />
   
    <div class="container">
        <div style="height: 15px;"></div>
        <div class="panel panel-default">         
            <div class="panel-heading">Chargeback uploading & Reports</div>
      <div class="container">
        <div class="panel-body" style="line-height: 34px;">
              <div class="col-md-10 col-md-offset-1">
                <div class="row">
                
                    <div class="col-md-6">
                    <div class="form-group form-inline">
                        <div class="col-md-6"><label>Chargeback Received Date<span style="color:red;">*</span></label></div>
                        <div class="col-md-6">
                        <asp:TextBox ID="txtChegRecivDate" ClientIDMode="Static" AutoComplete="off" runat="server" onclick="showCalender(this);" placeholder="dd/mm/yyyy"  CssClass="form-control" ></asp:TextBox>
<%--                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtChegRecivDate" ForeColor="Red" ValidationGroup="INS" runat="server" ErrorMessage="Chargeback Received Date"></asp:RequiredFieldValidator>--%>
                           </div>                   
                     </div>
                        </div>
                    <div class="col-md-6">
                     <div class="form-group form-inline">
                        <div class="col-md-6"><label>Chargeback Dispute Date<span style="color:red;">*</span></label></div>
                         <div class="col-md-6"><asp:TextBox ID="txtDisputeDate" ClientIDMode="Static" AutoComplete="off" runat="server" onclick="showCalender(this);" CssClass="form-control" placeholder="dd/mm/yyyy" ValidationGroup="INS" ></asp:TextBox>
                      </div>
                   </div>
                </div>

                    </div>
                 <div class="row">
                     <div class="col-md-6">
                        <div class="form-group form-inline">
                       
                            <div class="col-md-6">
                            <label>Disputed Amount<span style="color:red;">*</span></label>
                                </div>
                        <div class="col-md-6">
                            <asp:TextBox ID="txtDisbuteAmunt" AutoComplete="off" ClientIDMode="Static" onkeypress="return numeric(event)" runat="server" CssClass="form-control" PlaceHolder="Disputed Amount"></asp:TextBox>
                            </div>
                            </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group form-inline">
                       <div class="col-md-6"> <label>Chargeback Type<%--<span style="color:red;">*</span>--%></label></div>
                       <div class="col-md-6">
                            <asp:DropDownList ID="drpChageTpe" runat="server"  ClientIDMode="Static"  CssClass="form-control" Width="102%" ValidationGroup="INS" PlaceHolder="Chargeback Type">
                            <asp:ListItem Value="Fraud" Text="Fraud"></asp:ListItem>
                            <asp:ListItem Value="Non Fraud" Text="Non Fraud"></asp:ListItem>
                        </asp:DropDownList>
                             </div>
                          </div>
                       </div>
                 
                 </div>
                <div class="row">              
                    <div class="col-md-6">
                        <div class="form-group form-inline">
                        <div class="col-md-6">
                            <label>Card Type<span style="color:red;">*</span></label>
                            </div>
                            <div class="col-md-6">
                              
                        <asp:DropDownList ID="drpCardType" runat="server" ClientIDMode="Static" CssClass="form-control" Width="102%" AutoPostBack="true"  OnSelectedIndexChanged="drpCardType_SelectedIndexChanged">
                             <asp:ListItem Selected="True" Value="0" Text="---------Select------" ></asp:ListItem>
                            <asp:ListItem Value="Amex" Text="Amex" ></asp:ListItem>
                             <asp:ListItem Value="VISA" Text="VISA" ></asp:ListItem>
                             <asp:ListItem Value="Discover" Text="Discover" ></asp:ListItem>
                             <asp:ListItem Value="Master Card" Text="Master Card" ></asp:ListItem>
                        </asp:DropDownList>                               
                                </div>
                            </div>
                    </div>
                      <div class="col-md-6">
                        <div class="form-group form-inline">
                        <div class="col-md-6">
                            <label>Chargeback Reason Code<span style="color:red;">*</span></label>
                            </div>
                            <div class="col-md-6">
                                   <asp:DropDownList ID="drpChargebakCode" Width="197px" runat="server" ClientIDMode="Static" CssClass="form-control" >
                                      <asp:ListItem Selected="True" Value="0" Text="---------Select------" ></asp:ListItem>
                                       </asp:DropDownList>  
                                
                                </div>
                            </div>
                    </div>
               
                </div>
                 <div class="row">
                      <div class="col-md-6">
                        <div class="form-group form-inline">
                        <div class="col-md-6">
                            <label>Chargeback Status<span style="color:red;">*</span></label>
                            </div>
                            <div class="col-md-6">
                        <asp:DropDownList ID="drpChrgBackStus" runat="server" CssClass="form-control" Width="102%" PlaceHolder="Chargeback Statu">
                            <asp:ListItem Value="Pending" Text="Pending" ></asp:ListItem>
                             <asp:ListItem Value="In Progress" Text="In Progress" ></asp:ListItem>
                             <asp:ListItem Value="Won" Text="Won" ></asp:ListItem>
                             <asp:ListItem Value="Loss" Text="Loss" ></asp:ListItem>
                        </asp:DropDownList>
                                </div>
                            </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group form-inline">
                        <div class="col-md-6">
                            <label>Document Type</label>
                            </div>
                            <div class="col-md-6">
                           <asp:DropDownList ID="drpDocumentType" ClientIDMode="Static" runat="server" onclick="showCalender(this);"  Width="102%" CssClass="form-control" >
                            <asp:ListItem Value="ARC" Text="ARC"></asp:ListItem>
                            <asp:ListItem Value="Airline" Text="Airline"></asp:ListItem>
                            <asp:ListItem Value="Merchant" Text="Merchant"></asp:ListItem>
                                                 
                           </asp:DropDownList>
                                </div>
                            </div>
                        <asp:HiddenField Value="hdeprdate" runat="server"  />
                    </div>              
                    </div>
                  <div class="row">
                        <div class="col-md-6">
                    <div class="form-group form-inline">
                        <div class="col-md-6"><label>Case Number<span style="color:red;">*</span></label></div>
                        <div class="col-md-6">
                        <asp:TextBox ID="txtCaseNo" ClientIDMode="Static" AutoComplete="off" placeholder="Case Number" runat="server" CssClass="form-control" ></asp:TextBox>
<%--                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtChegRecivDate" ForeColor="Red" ValidationGroup="INS" runat="server" ErrorMessage="Chargeback Received Date"></asp:RequiredFieldValidator>--%>
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
                        
                        <asp:Button ID="btnInsert" runat="server" CssClass="btn btn-primary btn-lg" Text="Insert" ValidationGroup="INS" OnClientClick="return validation();" OnClick="btnInsert_Click" />
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
         <asp:Repeater ID="rptrEdit" OnItemCommand="rptrEdit_ItemCommand" runat="server">
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
                            <td><asp:Button runat="server" CommandName="ED" CommandArgument='<%# Eval("ID")%>' Text="Edit"/></td>   
                            </tr>
                    </ItemTemplate>                  
                </asp:Repeater>
             </div>
            </div>
           </div>
</asp:Content>
