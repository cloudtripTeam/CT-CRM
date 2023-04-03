<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="PayNow.aspx.cs" Inherits="Admin_Payment_PayNow" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        select:disabled {
            opacity: 0.6;
        }
        label{
            line-height:inherit;
        }
    </style>
     <link href="../../css/jquery.my-message.1.1.min.css" rel="stylesheet" />
    <link href="../../css/StyleSheet.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

      <style>
        
    .fl1-table tbody tr:nth-child(odd) {
         background-color: #4fc3a1;
            color: #fff;
    }
     .fl1-table tbody tr:first-child td {
        background-color: #adf0ff;
    }
    .fl1-table tr:nth-child(even) {
        background: transparent;
    }
    .fl1-table tr td:nth-child(odd) {
        
        border-right: 1px solid #E6E4E4;
    }
    .fl1-table tr td:nth-child(even) {
        border-right: 1px solid #E6E4E4;
    }
   
        .fl1-table {
    border-radius: 5px;
    font-size: 12px;
    font-weight: normal;
    border: none;
    border-collapse: collapse;
    width: 100%;
    max-width: 100%;
    white-space: nowrap;
    background-color: white;
}
        .fl1-table thead th {
    color: #ffffff;
    background: #4FC3A1;
}


.fl1-table thead th:nth-child(odd) {
    color: #ffffff;
    background: #00366c;
}

.fl1-table tr:nth-child(even) {
    background: #F8F8F8;
}
        .blink_me
        {
            animation: blinker 1s linear infinite;
        }
        @keyframes blinker 
        {
            50% {
                opacity: 0;
            }
        }
         .blink_me tr
        {
            animation: blinker 1s linear infinite;
           color:red;
        }
        @keyframes blinker 
        {
            50% {
                opacity: 0;
            }
        }

          .blink_me_charge
        {
            animation: blinker 1s linear infinite;
             color:red;
             font-size:14px;
             background:rgba(253, 174, 2, 0.48);
             border-radius:25px;
             padding:5px 10px;
             text-align:center;
             font-weight:bold;
        }
        @keyframes blinker 
        {
            50% {
                opacity: 0;
            }
        }
         .blink_me_charge tr
        {
            animation: blinker 1s linear infinite;
          
        }
        @keyframes blinker 
        {
            50% {
                opacity: 0;
            }
        }
      

        .fl1-table tbody tr:nth-child(odd) {
        background: none;
    }
     .fl1-table tbody tr:first-child td {
        background-color: none;
    }
    .fl1-table tr:nth-child(even) {
        background: transparent;
    }
    .fl1-table tr td:nth-child(odd) {
        
        border-right: 1px solid #E6E4E4;
    }
    .fl1-table tr td:nth-child(even) {
        border-right: 1px solid #E6E4E4;
    }
   
        .fl1-table {
    border-radius: 5px;
    font-size: 12px;
    font-weight: normal;
    border: none;
    border-collapse: collapse;
    width: 100%;
    max-width: 100%;
    white-space: nowrap;
    background-color: white;
}
        .fl1-table thead th {
    color: #ffffff;
    background: #4FC3A1;
}


.fl1-table thead th:nth-child(odd) {
    color: #ffffff;
    background: #00366c;
}

.fl1-table tr:nth-child(even) {
    background: none;
}
      
      .btn-group > .btn:first-child
      {
          width:100%;
      }
       .btn-group
      {
          width:100%;
      }


    </style>

    <div class="container-fluid">
        <div style="height: 15px;"></div>
        <div class="panel panel-default">
            <div class="panel-heading">Passenger details -
                <asp:Label ID="lblBookingRef" runat="server" Text=""></asp:Label></div>
            <div class="panel-body" style="line-height: 34px;">
                <div class="row">                   
                    <asp:Repeater ID="rptrPax" runat="server">
                        <HeaderTemplate>
                            <table class="fl1-table" style="margin-bottom: 0px; width: 100%; cellpadding: 0; cellspacing: 0;">
                                <tr>
                                    <td class='gdvh'>SrNo</td>
                                    <td class='gdvh'>Pax Type</td>
                                    <td class='gdvh'>Title</td>
                                    <td class='gdvh'>First Name</td>
                                    <td class='gdvh'>Last Name</td>
                                    <td class='gdvh'>Tickets</td>
                                    <td class='gdvh'>DOB</td>

                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr id='trP<%# Eval("SrNo") %>'>
                                <td class="gdvr"><%# Container.ItemIndex+ 1 %></td>
                                <td class="gdvr exelcss">
                                    <%# Eval("PaxType") %>
                                </td>
                                <td class="gdvr exelcss">
                                    <%# Eval("Title")%>
                                </td>
                                <td class="gdvr exelcss">
                                    <%# Eval("FName")%>
                                </td>
                                <td class="gdvr exelcss">
                                    <%# Eval("LName")%>
                                </td>
                                <td class="gdvr exelcss">
                                    <%# Eval("Tickets")%>
                                </td>

                                <td class="gdvr exelcss">
                                      <%#Convert.ToDateTime(Eval("DOB")).ToString("dd MMM yyyy")%>
                                </td>



                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            <tr>
                                <td colspan="7" class="gdvr"></td>

                            </tr>

                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
                
            </div>
        </div>
          <div class="panel panel-default">
            <div class="panel-heading">Sector details
               </div>
            <div class="panel-body" style="line-height: 34px;">
                <div class="row">
                    
                    <asp:Repeater ID="rptrSect" runat="server" >
                        <HeaderTemplate>
                            <table width='100%' cellpadding='0' cellspacing='0' class="fl1-table" style='margin-bottom: 0px;'>
                                <tr>
                                    <td class='gdvh'>Sr No</td>
                                    <td class='gdvh'>From</td>
                                    <td class='gdvh'>To</td>
                                    <td class='gdvh'>Air V</td>
                                    <td class='gdvh'>FLT No.</td>
                                    <td class='gdvh'>Class</td>
                                    <td class='gdvh'>From Date</td>
                                    <td class='gdvh'>From Time</td>
                                    <td class='gdvh'>To Date</td>
                                    <td class='gdvh'>To Time</td>
                                    <td class='gdvh'>Status</td>                                  

                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr id='trS<%# Eval("SrNo") %>'>
                                <td class="gdvr"><%# Container.ItemIndex+ 1 %></td>
                                <td class="gdvr exelcss">
                                    <%# Eval("FromDest")%>
                                </td>
                                <td class="gdvr exelcss">
                                    <%# Eval("ToDest")%>
                                </td>

                                <td class="gdvr exelcss">
                                    <%# Eval("CarierName")%>
                                </td>
                                <td class="gdvr exelcss">
                                    <%# Eval("FlightNo")%>
                                </td>
                                <td class="gdvr exelcss">
                                    <%# Eval("FClass")%>
                                </td>
                                <td class="gdvr exelcss">
                                    <%#Convert.ToDateTime(Eval("FromDateTime")).ToString("dd/MM/yyyy")%>
                                </td>
                                <td class="gdvr exelcss">
                                    <%# Convert.ToDateTime(Eval("FromDateTime")).ToString("hh:mm")%>
                                </td>
                                <td class="gdvr exelcss">
                                    <%#Convert.ToDateTime(Eval("ToDateTime")).ToString("dd/MM/yyyy")%>
                                </td>
                                <td class="gdvr exelcss">
                                    <%# Convert.ToDateTime(Eval("ToDateTime")).ToString("hh:mm")%>
                                </td>
                                <td class="gdvr exelcss">
                                    <%# Eval("FStatus")%></td>
                                
                            </tr>

                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
                </div>
              </div>

         <div class="panel panel-default">
            <div class="panel-heading">Transactions details
               </div>
            <div class="panel-body" style="line-height: 34px;">
                <div class="row">
                    
                    <asp:Repeater ID="rptrTrans" runat="server" >
                        <HeaderTemplate>
                            <table width='100%' cellpadding='0' cellspacing='0' class="fl1-table" style='margin-bottom: 0px;'>
                                <tr>
                                    <td class='gdvh'>Sr No</td>
                                    <td class='gdvh'>Transaction Ref.</td>
                                    <td class='gdvh'>Card No.</td>
                                    <td class='gdvh'>Holder Name</td>
                                    <td class='gdvh'>Expiry</td>
                                    <td class='gdvh'>Security</td>
                                    <td class='gdvh'>Address</td>
                                    <td class='gdvh'>City</td>
                                    <td class='gdvh'>PostCode</td>
                                    <td class='gdvh'>Date</td>                                  
                                    <td class='gdvh'>Action</td> 
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr id='trS<%# Eval("SrNo") %>'>
                                <td class="gdvr"><%# Container.ItemIndex+ 1 %></td>
                                <td class="gdvr exelcss">
                                    <%# Eval("TrnsNo")%>
                                </td>
                                <td class="gdvr exelcss">
                                    <%# Eval("CardNo").ToString().Length>=4?Eval("CardNo").ToString().Substring(Eval("CardNo").ToString().Length-4,4):Eval("CardNo").ToString()%>
                                </td>
                                 <td class="gdvr exelcss">
                                     <%# Eval("HolderName")%>
                                </td>
                               
                                <td class="gdvr exelcss">
                                    <%# Eval("ExpDate")%>
                                </td>
                                <td class="gdvr exelcss">
                                     <%# Eval("SecurityCode")%>
                                </td>
                                  <td class="gdvr exelcss">
                                    <%# Eval("CAddress")%>
                                </td>
                                <td class="gdvr exelcss">
                                    <%# Eval("City")%>
                                </td>
                                <td class="gdvr exelcss">
                                     <%# Eval("PostCode")%>
                                </td>
                              
                                <td class="gdvr exelcss">
                                   <%# Eval("ModifiedDate")%>
                                </td>
                               
                               <td class="gdvr exelcss">

                               </td>
                                   
                                
                            </tr>

                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
                </div>
              </div>


        <div class="panel panel-default">
            <div class="panel-heading">Payment Method <span style="font-size: 15px;
    color: #06f545;
    font-weight: bold;">Total Amount : <%= (subTotal) %></span><span class="pull-right" style="font-size: 15px;
   border: 2px solid #fff;
    padding: 0px 20px;border-radius:10px">Due Amount : <%= (subTotal - tranTotal) %></span></div>

            <div class="panel-body" style="line-height: 34px;">
                <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-4">
                            <div class="col-md-12">

                                 <div class="form-group">
                                        <label class="col-xs-12 col-md-4 col-lg-4 control-label pl-0">Payment Mode : </label>
                                        <div class="col-xs-12 col-md-4 col-lg-4 input-group">
                                             <asp:DropDownList ID="ddlPayment" CssClass="form-control" runat="server">
                                        <asp:ListItem Value="Card" >Card</asp:ListItem>
                                        <asp:ListItem Value="Cash">Cash</asp:ListItem>
                                        <asp:ListItem Value="Cheque">Cheque</asp:ListItem>
                                        <asp:ListItem Value="BNKTrans">Bank Transfer</asp:ListItem>
                                    </asp:DropDownList>
                                        </div>
                                    </div>


                              <div class="form-group">
                                        <label class="col-xs-12 col-md-4 col-lg-4 control-label pl-0">Charging Mode : </label>
                                        <div class="col-xs-12 col-md-4 col-lg-4 input-group">
                                              <asp:DropDownList ID="ddlGateway" CssClass="form-control" runat="server">
                                      
                                        <asp:ListItem Value="MPT">Manual Payment</asp:ListItem>
                                    </asp:DropDownList>
                                        </div>
                                    </div>

                                  <div class="form-group">
                                        <label class="col-xs-12 col-md-4 col-lg-4 control-label pl-0">Currency : </label>
                                        <div class="col-xs-12 col-md-4 col-lg-4 input-group">
                                             <asp:DropDownList ID="ddlCurrency" CssClass="form-control" runat="server">
                                       
                                        <asp:ListItem Value="USD" >USD</asp:ListItem>
                                         <asp:ListItem Value="GBP" >GBP</asp:ListItem>
                                         <asp:ListItem Value="CAD">CAD</asp:ListItem>
                                        <asp:ListItem Value="EUR">EUR</asp:ListItem>
                                        <asp:ListItem Value="INR">INR</asp:ListItem>
                                    </asp:DropDownList>
                                        </div>
                                    </div>

                              

                             
                            </div>
                        </div>
                        <div class="col-md-8">
                            <div id="dvCard" >
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-xs-12 col-md-4 col-lg-4 control-label pl-0">CH F Name : </label>
                                        <div class="col-xs-12 col-md-4 col-lg-4 input-group">
                                            <asp:TextBox ID="txtFName" runat="server" CssClass="form-control" ValidationGroup="CARD"></asp:TextBox>
                                        </div>
                                    </div>
                                     <div class="form-group">
                                        <label class="col-xs-12 col-md-4 col-lg-4 control-label pl-0">CH L Name : </label>
                                        <div class="col-xs-12 col-md-4 col-lg-4 input-group">
                                            <asp:TextBox ID="txtLName" runat="server" CssClass="form-control" ValidationGroup="CARD"></asp:TextBox>
                                        </div>
                                    </div>
                                     <div class="form-group">
                                        <label class="col-xs-12 col-md-4 col-lg-4 control-label pl-0">Card Num : </label>
                                        <div class="col-xs-12 col-md-4 col-lg-4 input-group">
                                            <asp:TextBox ID="TextBox_card_number"  runat="server" CssClass="form-control" ValidationGroup="CARD"></asp:TextBox>
                                        </div>
                                    </div>
                                     <div class="form-group">
                                        <label class="col-xs-12 col-md-4 col-lg-4 control-label pl-0">Expiry : </label>
                                        <div class="col-xs-12 col-md-4 col-lg-4 input-group">
                                            <asp:TextBox ID="TextBox_Expiry" runat="server" CssClass="form-control" ValidationGroup="CARD"></asp:TextBox>
                                        </div>
                                    </div>
                                     <div class="form-group">
                                        <label class="col-xs-12 col-md-4 col-lg-4 control-label pl-0">CVV : </label>
                                        <div class="col-xs-12 col-md-4 col-lg-4 input-group">
                                            <asp:TextBox ID="TextBox_cvv" runat="server" CssClass="form-control" ValidationGroup="CARD"></asp:TextBox>
                                        </div>
                                    </div>
                                   
                                    <div class="form-group">
                                        <label class="col-xs-12 col-md-4 col-lg-4 control-label pl-0">Address One:	</label>
                                        <div class="col-xs-12 col-md-4 col-lg-4 input-group">
                                            <asp:TextBox ID="txtAdd1" runat="server" CssClass="form-control" ValidationGroup="CARD"></asp:TextBox>
                                        </div>
                                    </div>
                                    
                                   </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-xs-12 col-md-4 col-lg-4 control-label pl-0">Address Two: </label>
                                        <div class="col-xs-12 col-md-4 col-lg-4 input-group">
                                            <asp:TextBox ID="txtAdd2" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-xs-12 col-md-4 col-lg-4 col-md-4 col-lg-4 control-label pl-0">City :	</label>
                                        <div class="col-xs-12 col-md-4 col-lg-4 input-group">
                                            <asp:TextBox ID="txtCity" runat="server" CssClass="form-control" ValidationGroup="CARD"></asp:TextBox>
                                        </div>
                                    </div>
                                     <div class="form-group">
                                        <label class="col-xs-12 col-md-4 col-lg-4 col-md-4 col-lg-4 control-label pl-0">State :	</label>
                                        <div class="col-xs-12 col-md-4 col-lg-4 input-group">
                                            <asp:TextBox ID="TextBox_state" runat="server" CssClass="form-control" ValidationGroup="CARD"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-xs-12 col-md-4 col-lg-4 control-label pl-0">Post Code : </label>
                                        <div class="col-xs-12 col-md-4 col-lg-4 input-group">
                                            <asp:TextBox ID="txtPostCode" runat="server" CssClass="form-control" ValidationGroup="CARD"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-xs-12 col-md-4 col-lg-4 control-label pl-0">Country : </label>
                                        <div class="col-xs-12 col-md-4 col-lg-4 input-group">
                                            <select class="form-control" id="selectCountry" name="selectCountry" runat="server">
                                                <option value="US" selected="selected">United States</option>
                                                <option value="CA" >Canada</option>
                                                <option value="GB" >United Kingdom</option>
                                            </select>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-xs-12 col-md-4 col-lg-4 control-label pl-0">Amount : </label>
                                        <div class="col-xs-12 col-md-4 col-lg-4 input-group">
                                            <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control" onkeypress="return onlyNumbers();" ValidationGroup="CARD"></asp:TextBox>
                                        </div>
                                    </div>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtFName" ForeColor="Red" ValidationGroup="CARD" runat="server" ErrorMessage="Enter First Name." Display="None"></asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtLName" ForeColor="Red" ValidationGroup="CARD" runat="server" ErrorMessage="Enter Last Name." Display="None"></asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtAdd1" ForeColor="Red" ValidationGroup="CARD" runat="server" ErrorMessage="Enter Address." Display="None"></asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtCity" ForeColor="Red" ValidationGroup="CARD" runat="server" ErrorMessage="Enter City." Display="None"></asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtPostCode" ForeColor="Red" ValidationGroup="CARD" runat="server" ErrorMessage="Enter Post Code." Display="None"></asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="txtAmount" ForeColor="Red" ValidationGroup="CARD" runat="server" ErrorMessage="Enter Amount." Display="None"></asp:RequiredFieldValidator>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator15" ControlToValidate="TextBox_Expiry" ForeColor="Red" ValidationGroup="CARD" runat="server" ErrorMessage="Enter Expiry." Display="None"></asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator18" ControlToValidate="TextBox_cvv" ForeColor="Red" ValidationGroup="CARD" runat="server" ErrorMessage="Enter CVV." Display="None"></asp:RequiredFieldValidator>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator19" ControlToValidate="TextBox_card_number" ValidationExpression = "^[\s\S]{0,16}$" ForeColor="Red" ValidationGroup="CARD" runat="server" ErrorMessage="Enter CVV." Display="None"></asp:RequiredFieldValidator>
                                    <asp:ValidationSummary ID="ValidationSummary1" ShowSummary="false" ShowMessageBox="true" ValidationGroup="CARD" runat="server" />
                                    <asp:Button ID="btnCard" runat="server" CssClass="btn btn-primary btn-sm" Text="Insert" ValidationGroup="CARD" OnClick="btnCard_Click" />
                                    <asp:HiddenField ID="hidBookingID" runat="server" />
                                </div>
                            </div>
                            <div id="dvManual" style="display: none">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label class="col-xs-12 col-md-4 col-lg-4 control-label pl-0">Card Holder Name : </label>
                                        <div class="col-xs-12 col-md-4 col-lg-4 input-group">
                                        <div class="col-xs-12 col-md-4 col-lg-4 input-group">
                                            <asp:TextBox ID="txtmnlFirstName" runat="server" CssClass="form-control" ValidationGroup="MCARD"></asp:TextBox>
                                        </div>
                                    </div>
                                    <%-- <div class="form-group">
                                        <label class="col-xs-12 col-md-4 col-lg-4 control-label pl-0">Card Holder Last Name : </label>
                                        <div class="col-xs-12 col-md-4 col-lg-4 input-group">
                                            <asp:TextBox ID="txtmnlLastName" runat="server" CssClass="form-control" ValidationGroup="MCARD"></asp:TextBox>
                                        </div>
                                    </div>  --%>

                                    <div class="form-group">
                                        <label class="col-xs-12 col-md-4 col-lg-4 control-label pl-0">Payment GateWay : </label>
                                        <div class="col-xs-12 col-md-4 col-lg-4 input-group">
                                            <select class="form-control" id="ddlPaymentgateway" name="selectgateway" runat="server">
                                                <option value="ATH" selected="selected">ATH</option>
                                                <option value="ATX">ATX</option>
                                                <option value="Other">Others</option>

                                            </select>
                                        </div>
                                    </div>


                                    <div class="form-group">
                                        <label class="col-xs-12 col-md-4 col-lg-4 control-label pl-0">Amount : </label>
                                        <div class="col-xs-12 col-md-4 col-lg-4 input-group">
                                            <asp:TextBox ID="txtmnlAmount" runat="server" CssClass="form-control" onkeypress="return onlyNumbers();" ValidationGroup="MCARD"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-xs-12 col-md-4 col-lg-4 col-md-4 col-lg-4 control-label pl-0">Transaction Ref :	</label>
                                        <div class="col-xs-12 col-md-4 col-lg-4 input-group">
                                            <asp:TextBox ID="txtmnlTRNRef" runat="server" CssClass="form-control" ValidationGroup="MCARD"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12"></div>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" ControlToValidate="txtmnlFirstName" ForeColor="Red" ValidationGroup="MCARD" runat="server" ErrorMessage="Enter First Name." Display="None"></asp:RequiredFieldValidator>

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator16" ControlToValidate="txtmnlAmount" ForeColor="Red" ValidationGroup="MCARD" runat="server" ErrorMessage="Enter Address." Display="None"></asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator17" ControlToValidate="txtmnlTRNRef" ForeColor="Red" ValidationGroup="MCARD" runat="server" ErrorMessage="Enter City." Display="None"></asp:RequiredFieldValidator>

                                    <asp:ValidationSummary ID="ValidationSummary5" ShowSummary="false" ShowMessageBox="true" ValidationGroup="MCARD" runat="server" />
                                    <asp:Button ID="btnmanulCard" runat="server" CssClass="btn btn-primary btn-lg" Text="Insert" ValidationGroup="MCARD" OnClick="btnmanulCard_Click" />
                                    <asp:HiddenField ID="HiddenField1" runat="server" />
                                </div>
                            </div>
                            <div id="dvCash" style="display: none">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label class="col-xs-12 col-md-4 col-lg-4 control-label pl-0">Amount : </label>
                                        <div class="col-xs-12 col-md-4 col-lg-4 input-group">
                                            <asp:TextBox ID="txtCSAmount" runat="server" CssClass="form-control" ValidationGroup="CASH"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-xs-12 col-md-4 col-lg-4 control-label pl-0">Given By : </label>
                                        <div class="col-xs-12 col-md-4 col-lg-4 input-group">
                                            <asp:TextBox ID="txtGivenBy" runat="server" CssClass="form-control" ValidationGroup="CASH"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12"></div>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="txtCSAmount" ForeColor="Red" ValidationGroup="CASH" runat="server" ErrorMessage="Enter Amount." Display="None"></asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="txtGivenBy" ForeColor="Red" ValidationGroup="CASH" runat="server" ErrorMessage="Enter Given By." Display="None"></asp:RequiredFieldValidator>
                                    <asp:ValidationSummary ID="ValidationSummary2" ShowSummary="false" ShowMessageBox="true" ValidationGroup="CASH" runat="server" />
                                    <asp:Button ID="btnCash" runat="server" CssClass="btn btn-primary btn-lg" Text="Insert" ValidationGroup="CASH" OnClick="btnCash_Click" />
                                </div>
                            </div>
                            <div id="dvCheque" style="display: none">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label class="col-xs-12 col-md-4 col-lg-4 control-label pl-0">Bank Name : </label>
                                        <div class="col-xs-12 col-md-4 col-lg-4 input-group">
                                            <asp:TextBox ID="txtBName" runat="server" CssClass="form-control" ValidationGroup="CHEQUE"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-xs-12 col-md-4 col-lg-4 control-label pl-0">Cheque No : </label>
                                        <div class="col-xs-12 col-md-4 col-lg-4 input-group">
                                            <asp:TextBox ID="txtCheque" runat="server" CssClass="form-control" ValidationGroup="CHEQUE"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-xs-12 col-md-4 col-lg-4 control-label pl-0">Amount : </label>
                                        <div class="col-xs-12 col-md-4 col-lg-4 input-group">
                                            <asp:TextBox ID="txtCHQAmount" runat="server" CssClass="form-control" ValidationGroup="CHEQUE"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-xs-12 col-md-4 col-lg-4 control-label pl-0">Status : </label>
                                        <div class="col-xs-12 col-md-4 col-lg-4 input-group">
                                            <asp:DropDownList ID="ddlStatus" CssClass="form-control" runat="server">
                                                <asp:ListItem Value="Ok">Ok</asp:ListItem>
                                                <asp:ListItem Value="Pending">Pending</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12"></div>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="txtBName" ForeColor="Red" ValidationGroup="CHEQUE" runat="server" ErrorMessage="Enter Bank Name." Display="None"></asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ControlToValidate="txtCheque" ForeColor="Red" ValidationGroup="CHEQUE" runat="server" ErrorMessage="Enter Cheque No." Display="None"></asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ControlToValidate="txtCHQAmount" ForeColor="Red" ValidationGroup="CHEQUE" runat="server" ErrorMessage="Enter Amount." Display="None"></asp:RequiredFieldValidator>
                                    <asp:ValidationSummary ID="ValidationSummary3" ShowSummary="false" ShowMessageBox="true" ValidationGroup="CHEQUE" runat="server" />
                                    <asp:Button ID="btnCheque" runat="server" CssClass="btn btn-primary btn-lg" Text="Insert" ValidationGroup="CHEQUE" OnClick="btnCheque_Click" />
                                </div>
                            </div>
                            <div id="dvBT" style="display: none">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label class="col-xs-12 col-md-4 col-lg-4 control-label pl-0">Bank Name : </label>
                                        <div class="col-xs-12 col-md-4 col-lg-4 input-group">
                                            <asp:TextBox ID="txtBTBName" runat="server" CssClass="form-control" ValidationGroup="BT"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-xs-12 col-md-4 col-lg-4 control-label pl-0">Amount : </label>
                                        <div class="col-xs-12 col-md-4 col-lg-4 input-group">
                                            <asp:TextBox ID="txtBTAmount" runat="server" CssClass="form-control" ValidationGroup="BT"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-xs-12 col-md-4 col-lg-4 control-label pl-0">Transaction No. : </label>
                                        <div class="col-xs-12 col-md-4 col-lg-4 input-group">
                                            <asp:TextBox ID="txtTransactionNo" runat="server" CssClass="form-control" ValidationGroup="BT"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12"></div>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ControlToValidate="txtBTAmount" ForeColor="Red" ValidationGroup="BT" runat="server" ErrorMessage="Enter Amount." Display="None"></asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ControlToValidate="txtTransactionNo" ForeColor="Red" ValidationGroup="BT" runat="server" ErrorMessage="Enter Transaction No." Display="None"></asp:RequiredFieldValidator>
                                    <asp:ValidationSummary ID="ValidationSummary4" ShowSummary="false" ShowMessageBox="true" ValidationGroup="BT" runat="server" />
                                    <asp:Button ID="btnBT" runat="server" CssClass="btn btn-primary btn-lg" Text="Insert" ValidationGroup="BT" OnClick="btnBT_Click" />
                                </div>
                            </div>
                        </div>

                    </div>

                </div>

                <div class="col-md-12">
                    &nbsp;
                        <asp:Label ID="lblMsg" runat="server" ForeColor="Red" EnableViewState="false"></asp:Label>
                </div>
                <div class="row">


                    <div class="col-md-12" style="display: none;">
                        <label>
                            <br />
                            &nbsp;
                        </label>
                        <asp:Button ID="btnInsert" runat="server" CssClass="btn btn-primary btn-lg" Text="Insert" ValidationGroup="INS" />

                    </div>
                </div>
            </div>
            <div class="clearfix"></div>



        </div>
    </div>

    <script>
        $(document).ready(function () {
            $(document.getElementById('<%=ddlPayment.ClientID%>')).on('change', function () {
                if (this.value == 'Card') {
                    $("#dvCard").show();
                    $("#dvCash").hide();
                    $("#dvCheque").hide();
                    $("#dvBT").hide();
                    $("#dvManual").hide();
                    $("#<%=ddlGateway.ClientID%>").prop("disabled", false);

                }
                else if (this.value == 'Cash') {
                    $("#dvCard").hide();
                    $("#dvCash").show();
                    $("#dvCheque").hide();
                    $("#dvBT").hide();
                    $("#dvManual").hide();
                    $("#<%=ddlGateway.ClientID%>").prop("disabled", true);


                }
                else if (this.value == 'Cheque') {
                    $("#dvCard").hide();
                    $("#dvCash").hide();
                    $("#dvCheque").show();
                    $("#dvBT").hide();
                    $("#dvManual").hide();
                    $("#<%=ddlGateway.ClientID%>").prop("disabled", true);
                }
                else if (this.value == 'BNKTrans') {
                    $("#dvCard").hide();
                    $("#dvCash").hide();
                    $("#dvCheque").hide();
                    $("#dvBT").show();
                    $("#dvManual").hide();
                    $("#<%=ddlGateway.ClientID%>").prop("disabled", true);
                }

            });
        });
    </script>

    <script>
        $(document).ready(function () {
            $(document.getElementById('<%=ddlGateway.ClientID%>')).on('change', function () {
                if (this.value == 'MPT') {
                    $("#dvCard").hide();

                    $("#dvManual").show();
                }
                else {
                    $("#dvCard").show();
                    $("#dvManual").hide();

                }


            });
        });
    </script>

    <script type="text/javascript">
        function onlyNumbers(evt) {
            var e = event || evt;
            var charCode = e.which || e.keyCode;

            if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }
    </script>

</asp:Content>

