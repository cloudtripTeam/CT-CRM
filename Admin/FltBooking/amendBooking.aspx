<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="amendBooking.aspx.cs" Inherits="Admin_FltBooking_amendBooking" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../js/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../js/jquery-ui.js"></script>
    <link href="../../css/jquery.my-message.1.1.min.css" rel="stylesheet" />
    <link href="../../css/StyleSheet.css" rel="stylesheet" />
    <script src="../../js/CalendarAnyYear.js"></script>
    <style>
        
    .fl1-table tbody tr:nth-child(odd) {
         background-color: #4fc3a1;
            color: #fff;
    }
     .fl1-table tbody tr:first-child td {
        background-color: #adf0ff;
         height: 0px;
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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:HiddenField ID="hfBookingID" runat="server" Value="" />
    <asp:HiddenField ID="hfProdID" runat="server" Value="" />
    <asp:HiddenField ID="hfUpdatedBy" runat="server" />
    <asp:HiddenField ID="hfOldStatus" runat="server" />
    <asp:HiddenField ID="hfOldCompany" runat="server" />
    <asp:HiddenField ID="hfOldBookingBy" runat="server" />
    <asp:HiddenField ID="hfOldAssignedBy" runat="server" />
    <asp:HiddenField ID="hfOldvalidating" runat="server" />
    <script src="../../js/jquery.my-message.1.1.js"></script>


    <div class="container">
        <div style="height: 15px;"></div>
        <div align="center" id="popProgressBar" style="display: none;" class="popup-product">
            <table width="100%" class="table" align="center" height="100%" bgcolor="#ffffff">
                <tr>
                    <td class="popup-header">Please wait while we process your request...
                    </td>
                </tr>
                <tr>
                    <td align="center" valign="middle">
                        <img runat="server" src="~/images/wait.gif" id="ImageProgressbar" />
                    </td>
                </tr>
                <tr>
                    <td align="center" height="40" style="background-color: #ffffff; color: #B9B9B9; vertical-align: middle; text-align: center; font-size: 18px; font-family: Verdana;"></td>
                </tr>
            </table>
        </div>
    </div>
     <button type="button" style="position: fixed; top: 0px;z-index:11" class="btn btn-secondary btn-sm" data-toggle="modal" data-target="#myModal">View Remarks</button>
    <asp:Panel ID="pnlBooking" Visible="true" runat="server">
        
        <div class="container-fluid">
            <div style="height: 15px;"></div>

            <div class="panel panel-default">
                <div class="panel-heading">
                    Booking Summary  -
                    <asp:Label ID="lblBookingID" runat="server" Text=""></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblBookkingDate" runat="server" Text=""></asp:Label>
                </div>
                <div class="panel-body" style="line-height: 34px;">
                    <div class="row">
                        <div class="col-md-2">
                            <label>PNR</label>

                            <asp:TextBox ID="txtPnr" class="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-1">
                            <label>Destination</label>
                            <asp:TextBox ID="txtDestination" class="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-1">
                            <label>Val Carrier</label>
                            <asp:TextBox ID="txtValidatingCarrier" CssClass="form-control" MaxLength="2" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-1">
                            <label>Currency</label>
                            <asp:DropDownList ID="ddlCurrency" runat="server" CssClass="form-control">
                                <asp:ListItem Value="GBP" Selected="True">GBP</asp:ListItem>
                                <asp:ListItem Value="USD">USD</asp:ListItem>
                                <asp:ListItem Value="CAD">CAD</asp:ListItem>
                                <asp:ListItem Value="EUR">EUR</asp:ListItem>
                            </asp:DropDownList>

                        </div>
                        <div class="col-md-1">
                            <label>GDS</label>
                            <asp:DropDownList runat="server" ID="ddlGDS" CssClass="form-control">
                                <asp:ListItem Text="Select GDS" Value="" />
                                <asp:ListItem Text="Amadeus" Value="Amadeus" />
                                <asp:ListItem Text="Worldspan" Value="Worldspan" />
                                <asp:ListItem Text="Sabre" Value="Sabre" />
                                <asp:ListItem Text="Apollo" Value="Apollo" />
                                <asp:ListItem Text="Galileo" Value="Galileo" />
                            </asp:DropDownList>
                            
                        </div>
                        <div class="col-md-2">
                            <label>Supplier</label>
                            <asp:DropDownList ID="ddlSupplier" runat="server" CssClass="form-control">
                            </asp:DropDownList>

                        </div>

                        <div class="col-md-2">
                            <label>B Status</label>
                            <asp:DropDownList ID="ddlStatus" ValidationGroup="cxp" runat="server" CssClass="form-control">
                                <asp:ListItem Value="" Selected="True">--Select--</asp:ListItem>
                                <asp:ListItem Value="Incomplete">Incomplete</asp:ListItem>
                                <asp:ListItem Value="Option">Option</asp:ListItem>
                                <asp:ListItem Value="Booked">Booked</asp:ListItem>
                                 <asp:ListItem Value="Cancelled">Cancelled</asp:ListItem>
                                <asp:ListItem Value="Decline">Decline</asp:ListItem>
                                <asp:ListItem Value="Documents">Documents</asp:ListItem>
                                <asp:ListItem Value="Payments">Payments</asp:ListItem>
                                <asp:ListItem Value="Queue">Queue</asp:ListItem>
                                <asp:ListItem Value="Follow UP">Follow UP</asp:ListItem>
                                <asp:ListItem Value="Issued">Issued</asp:ListItem>
                                <asp:ListItem Value="ReIssued">ReIssued</asp:ListItem>
                                <asp:ListItem Value="Cancelled">Cancelled</asp:ListItem>
                                <asp:ListItem Value="Refund">Refund</asp:ListItem>
                                <asp:ListItem Value="TKTNotFound">TKTNotFound</asp:ListItem>
                                <asp:ListItem Value="Deposit Forfeited">Deposit Forfeited</asp:ListItem>
                                <asp:ListItem Value="FutureCredit">Future Credit</asp:ListItem>
                                <asp:ListItem Value="ChargeBack">Charge Back</asp:ListItem>
                                <asp:ListItem Value="Dupe">Dupe</asp:ListItem>
                                <asp:ListItem Value="Customer_Denied">Customer Denied</asp:ListItem>
                                <asp:ListItem Value="Left_voice_mail">Left Voicemail</asp:ListItem>


                            </asp:DropDownList>
                          
                        </div>
                          <div class="col-md-2">
                            <label>Assigned</label>
                            <asp:DropDownList ID="ddlAssignedTo" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                        </div>
                        <div class="row">

                        <div class="col-md-2">
                            <label>Company</label> 
                             <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control">
                             </asp:DropDownList>
                           
                        </div>
                        <div class="col-md-2">
                            <label>Source</label> 
                             <asp:DropDownList ID="ddlSourceMedia" runat="server" CssClass="form-control">
                                 <asp:ListItem Value="" Text="Select Media"></asp:ListItem>
                                 <asp:ListItem Value="Chat" Text="Chat"></asp:ListItem>
                                 <asp:ListItem Value="Phone" Text="Phone"></asp:ListItem>
                                 <asp:ListItem Value="Email" Text="Email"></asp:ListItem>
                                 <asp:ListItem Value="Newsletters" Text="Newsletters"></asp:ListItem>
                                 <asp:ListItem Value="Other" Text="Other"></asp:ListItem>
                             </asp:DropDownList>

                        </div>

                        <div class="col-md-2">
                            <label>Supplier Ref</label>
                             <asp:TextBox ID="txtSuppleirRef" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <label>Invoice Date</label>
                             <asp:TextBox ID="txtInvoiceDate" ReadOnly="true" CssClass="form-control" runat="server"></asp:TextBox>
                            <input id="setascurrdate" type="hidden" />
                            <input id="hdeprdate" type="hidden" />
                        </div>
                        <div class="col-md-2">
                            <label>Booking By</label>
                            <asp:DropDownList ID="ddlBookingBy" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                             <div class="col-md-2" id="dvLockBooking" runat="server" visible="false">
                            <label>Lock Booking</label>
                            <asp:CheckBox ID="chkIsLocked" Visible="false" Text="" runat="server" />
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="ddlCompany" InitialValue="" ValidationGroup="cxp" runat="server" ErrorMessage="provide company name"></asp:RequiredFieldValidator>
                        </div>  
                        </div>
                     
                    
                    <div class="row  pull-center">
                        
                        
                        
                        <div class="col-md-8">
                            <p class="glow">&nbsp;</p>
                            <span class="btn btn-primary btn-sm" style="cursor: pointer;" onclick="Update_Summary();">Update Summary</span>
                      
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator8" style="line-height:18px" ControlToValidate="ddlStatus" InitialValue="--Select--" ValidationGroup="cxp" ErrorMessage="Please Select Booking Status!" />

                        
                    </div>
                        </div>
                </div>
            </div>
        


            <div class="panel panel-default">
                <div class="panel-heading">PAX Details</div>
                <div class="panel-body" style="line-height: 34px; padding: 0px!important;">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:Repeater ID="rptrPax" runat="server" OnItemDataBound="rptrPax_ItemDataBound">
                                <HeaderTemplate>
                                    <table class="fl1-table" style="margin-bottom: 0px; width: 100%; cellpadding: 0; cellspacing: 0;">
                                        <tr>
                                            <td class='gdvh'>SrNo</td>
                                            <td class='gdvh'>PaxType</td>
                                            <td class='gdvh'>Title</td>
                                            <td class='gdvh'>First Name</td>
                                            <td class='gdvh'>Mid Name</td>
                                            <td class='gdvh'>Last Name</td>
                                            <td class='gdvh'>Tickets</td>
                                            <td class='gdvh'>DOB</td>
                                            <td class='gdvh'>Action</td>
                                        </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr id='trP<%# Eval("SrNo") %>'>
                                        <td class="gdvr"><%# Container.ItemIndex+ 1 %></td>
                                        <td class="gdvr exelcss">
                                            <input maxlength="15" class="form-control" id='txtPaxType<%# Eval("SrNo")%>'  value='<%# Eval("PaxType")%>' type="text">
                                        </td>
                                        <td class="gdvr exelcss">
                                            <input maxlength="15" class="form-control" id='txtTitle<%# Eval("SrNo")%>'  value='<%# Eval("Title")%>' type="text">
                                        </td>
                                        <td class="gdvr exelcss">
                                            <input maxlength="40"  class="form-control" id='txtFirstName<%# Eval("SrNo")%>'  value='<%# Eval("FName")%>' type="text">
                                        </td>
                                        <td class="gdvr exelcss">
                                            <input maxlength="40"  class="form-control" id='txtMidName<%# Eval("SrNo")%>'  value='<%# Eval("MName")%>' type="text">
                                        </td>
                                        <td class="gdvr exelcss">
                                            <input maxlength="40"  class="form-control" id='txtLastName<%# Eval("SrNo")%>'  value='<%# Eval("LName")%>' type="text">
                                        </td>
                                        <td class="gdvr exelcss">
                                            <input maxlength="100" class="form-control" id='txtTickets<%# Eval("SrNo")%>'  value='<%# Eval("Tickets")%>' type="text">
                                        </td>

                                        <td class="gdvr exelcss">
                                            <input maxlength="10" style="width: 100px; font-size: 10px;" class="form-control" id='txtDOB<%# Eval("SrNo")%>' value='<%# Eval("DOB")%>' type="text">

                                            <asp:HiddenField ID="hidBID" runat="server" Value='<%# Eval("BookingID")%>' />
                                            <asp:HiddenField ID="hidPID" runat="server" Value='<%# Eval("ProdID")%>' />
                                            <asp:HiddenField ID="hidSNO" runat="server" Value='<%# Eval("SrNo")%>' />

                                        </td>
                                        <td class="gdvr exelcss">
                                            <asp:Button ID="btnUpdate" UseSubmitBehavior="false" runat="server" Text="Update" CssClass="btn btn-success btn-sm" ToolTip='<%# Eval("SrNo") %>' OnClientClick="return UpdatePax(this)" />
                                            <asp:Button ID="btnDelete" UseSubmitBehavior="false" runat="server" Text="Delete" CssClass="btn btn-danger btn-sm" ToolTip='<%# Eval("SrNo") %>' OnClientClick="return DelPax(this)" />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <tr>
                                        <td colspan="9" class="gdvr"></td>
                                    </tr>
                                    </table>
                                </FooterTemplate>
                            </asp:Repeater>

                            <table width='100%' cellpadding='0' cellspacing='0' class='table ' style='background-color: #ddd; margin-bottom: 0px;'>
                                <tr>
                                    <td class='gdvh'>SrNo</td>
                                    <td class='gdvh'>PaxType</td>
                                    <td class='gdvh'>Title</td>
                                    <td class='gdvh'>First Name</td>
                                    <td class='gdvh'>Mid Name</td>
                                    <td class='gdvh'>Last Name</td>
                                    <td class='gdvh'>Tickets</td>
                                    <td class='gdvh'>DOB</td>
                                    <td class='gdvh'></td>
                                </tr>
                                <tr>
                                    <td class='gdvh'></td>
                                    <td class='gdvh'>
                                        <asp:DropDownList ID="ddlType" Style="width: 80px; font-size: 12px;" CssClass="form-control" runat="server">
                                            <asp:ListItem Value="ADT" Text="Adult"></asp:ListItem>
                                            <asp:ListItem Value="CNN" Text="Child"></asp:ListItem>
                                            <asp:ListItem Value="INF" Text="Infant"></asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td class='gdvh'>
                                        <asp:TextBox ID="txtTitle" MaxLength="15"  CssClass="form-control" runat="server"></asp:TextBox></td>
                                    <td class='gdvh'>
                                        <asp:TextBox ID="txtFName" CssClass="form-control" runat="server"></asp:TextBox></td>
                                    <td class='gdvh'>
                                        <asp:TextBox ID="txtMName" CssClass="form-control" runat="server"></asp:TextBox></td>
                                    <td class='gdvh'>
                                        <asp:TextBox ID="txtLName"  CssClass="form-control" runat="server"></asp:TextBox></td>
                                    <td class='gdvh'>
                                        <asp:TextBox ID="txtTickets"  CssClass="form-control" runat="server"></asp:TextBox></td>
                                    <td class='gdvh'>
                                        <asp:TextBox ID="txtDOB" MaxLength="10"  CssClass="form-control" runat="server"></asp:TextBox></td>
                                    <td class='gdvh'>
                                        <asp:Button ID="btnAddpax" runat="server" CssClass="btn btn-success" Text="Add Pax" OnClick="btnAddpax_Click" OnClientClick="return validatePax()" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2"></td>
                                    <td colspan="4" class="text-center"></td>
                                    <td colspan="2">
                                        <asp:Literal ID="ltrPax" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>


                    <div class="row text-center">
                        <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="1"
                            runat="server">
                            <ProgressTemplate>
                                <asp:Literal ID="message" runat="server" Text="Wait..."></asp:Literal>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </div>
                </div>
            </div>

            <div class="panel panel-default" id="pnlAddress" runat="server">
                <div class="panel-heading">PAX Address Details</div>
                <div class="panel-body" style="line-height: 34px;">

                    <div class="row">
                        <div class="col-md-12">
                            <div class="col-md-4">
                                <label>Mobile</label>
                                <asp:TextBox runat="server" ID="txtMobile" CssClass="form-control" />
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator10" ControlToValidate="txtMobile" ValidationGroup="cxp" ErrorMessage="Please enter Mobile!" />
                            </div>
                            <div class="col-md-4">
                                <label>Phone</label>
                                <asp:TextBox runat="server" ID="txtPhone" CssClass="form-control" />
                            </div>
                            <div class="col-md-4">
                                <label>Email ID</label>
                                <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" />
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator11" ControlToValidate="txtEmail" ValidationGroup="cxp" ErrorMessage="Please enter Email ID!" />
                            </div>
                            <div class="col-md-4">
                                <label>Address</label>
                                <asp:TextBox runat="server" ID="txtAddress" TextMode="MultiLine" CssClass="form-control" />

                            </div>
                            <div class="col-md-4">
                                <label>City</label>
                                <asp:TextBox runat="server" ID="txtCity" CssClass="form-control" />
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator12" ControlToValidate="txtCity" ValidationGroup="cxp" ErrorMessage="Please enter City!" />
                            </div>
                            <div class="col-md-4">
                                <label>Country</label>
                                <asp:DropDownList ID="DdlCountry" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="">Select country Name</asp:ListItem>
                                    <asp:ListItem Value="AF">Afghanistan</asp:ListItem>
                                    <asp:ListItem Value="AX">Aland Islands</asp:ListItem>
                                    <asp:ListItem Value="AL">Albania</asp:ListItem>
                                    <asp:ListItem Value="DZ">Algeria</asp:ListItem>
                                    <asp:ListItem Value="AS">American Samoa</asp:ListItem>
                                    <asp:ListItem Value="AD">Andorra</asp:ListItem>
                                    <asp:ListItem Value="AO">Angola</asp:ListItem>
                                    <asp:ListItem Value="AI">Anguilla</asp:ListItem>
                                    <asp:ListItem Value="AQ">Antarctica</asp:ListItem>
                                    <asp:ListItem Value="AG">Antigua and Barbuda</asp:ListItem>
                                    <asp:ListItem Value="AR">Argentina</asp:ListItem>
                                    <asp:ListItem Value="AM">Armenia</asp:ListItem>
                                    <asp:ListItem Value="AW">Aruba</asp:ListItem>
                                    <asp:ListItem Value="AU">Australia</asp:ListItem>
                                    <asp:ListItem Value="AT">Austria</asp:ListItem>
                                    <asp:ListItem Value="AZ">Azerbaijan</asp:ListItem>
                                    <asp:ListItem Value="BS">Bahamas</asp:ListItem>
                                    <asp:ListItem Value="BH">Bahrain</asp:ListItem>
                                    <asp:ListItem Value="BD">Bangladesh</asp:ListItem>
                                    <asp:ListItem Value="BB">Barbados</asp:ListItem>
                                    <asp:ListItem Value="BY">Belarus</asp:ListItem>
                                    <asp:ListItem Value="BE">Belgium</asp:ListItem>
                                    <asp:ListItem Value="BZ">Belize</asp:ListItem>
                                    <asp:ListItem Value="BJ">Benin</asp:ListItem>
                                    <asp:ListItem Value="BM">Bermuda</asp:ListItem>
                                    <asp:ListItem Value="BT">Bhutan</asp:ListItem>
                                    <asp:ListItem Value="BO">Bolivia</asp:ListItem>
                                    <asp:ListItem Value="BA">Bosnia and Herzegovina</asp:ListItem>
                                    <asp:ListItem Value="BW">Botswana</asp:ListItem>
                                    <asp:ListItem Value="BV">Bouvet Island</asp:ListItem>
                                    <asp:ListItem Value="BR">Brazil</asp:ListItem>
                                    <asp:ListItem Value="IO">British Indian Ocean Territory</asp:ListItem>
                                    <asp:ListItem Value="BN">Brunei Darussalam</asp:ListItem>
                                    <asp:ListItem Value="BG">Bulgaria</asp:ListItem>
                                    <asp:ListItem Value="BF">Burkina Faso</asp:ListItem>
                                    <asp:ListItem Value="BI">Burundi</asp:ListItem>
                                    <asp:ListItem Value="KH">Cambodia</asp:ListItem>
                                    <asp:ListItem Value="CM">Cameroon</asp:ListItem>
                                    <asp:ListItem Value="CA">Canada</asp:ListItem>
                                    <asp:ListItem Value="CV">Cape Verde</asp:ListItem>
                                    <asp:ListItem Value="KY">Cayman Islands</asp:ListItem>
                                    <asp:ListItem Value="CF">Central African Republic</asp:ListItem>
                                    <asp:ListItem Value="TD">Chad</asp:ListItem>
                                    <asp:ListItem Value="CL">Chile</asp:ListItem>
                                    <asp:ListItem Value="CN">China</asp:ListItem>
                                    <asp:ListItem Value="CX">Christmas Island</asp:ListItem>
                                    <asp:ListItem Value="CC">Cocos (Keeling) Islands</asp:ListItem>
                                    <asp:ListItem Value="CO">Colombia</asp:ListItem>
                                    <asp:ListItem Value="KM">Comoros</asp:ListItem>
                                    <asp:ListItem Value="CG">Congo</asp:ListItem>
                                    <asp:ListItem Value="CD">Congo The Democratic Republic of the</asp:ListItem>
                                    <asp:ListItem Value="CK">Cook Islands</asp:ListItem>
                                    <asp:ListItem Value="CR">Costa Rica</asp:ListItem>
                                    <asp:ListItem Value="CI">C&#244;te d'Ivoire</asp:ListItem>
                                    <asp:ListItem Value="HR">Croatia</asp:ListItem>
                                    <asp:ListItem Value="CU">Cuba</asp:ListItem>
                                    <asp:ListItem Value="CY">Cyprus</asp:ListItem>
                                    <asp:ListItem Value="CZ">Czech Republic</asp:ListItem>
                                    <asp:ListItem Value="DK">Denmark</asp:ListItem>
                                    <asp:ListItem Value="DJ">Djibouti</asp:ListItem>
                                    <asp:ListItem Value="DM">Dominica</asp:ListItem>
                                    <asp:ListItem Value="DO">Dominican Republic</asp:ListItem>
                                    <asp:ListItem Value="EC">Ecuador</asp:ListItem>
                                    <asp:ListItem Value="EG">Egypt</asp:ListItem>
                                    <asp:ListItem Value="SV">El Salvador</asp:ListItem>
                                    <asp:ListItem Value="GQ">Equatorial Guinea</asp:ListItem>
                                    <asp:ListItem Value="ER">Eritrea</asp:ListItem>
                                    <asp:ListItem Value="EE">Estonia</asp:ListItem>
                                    <asp:ListItem Value="ET">Ethiopia</asp:ListItem>
                                    <asp:ListItem Value="FK">Falkland Islands (Malvinas)</asp:ListItem>
                                    <asp:ListItem Value="FO">Faroe Islands</asp:ListItem>
                                    <asp:ListItem Value="FJ">Fiji</asp:ListItem>
                                    <asp:ListItem Value="FI">Finland</asp:ListItem>
                                    <asp:ListItem Value="FR">France</asp:ListItem>
                                    <asp:ListItem Value="GF">French Guiana</asp:ListItem>
                                    <asp:ListItem Value="PF">French Polynesia</asp:ListItem>
                                    <asp:ListItem Value="TF">French Southern Territories</asp:ListItem>
                                    <asp:ListItem Value="GA">Gabon</asp:ListItem>
                                    <asp:ListItem Value="GM">Gambia</asp:ListItem>
                                    <asp:ListItem Value="GE">Georgia</asp:ListItem>
                                    <asp:ListItem Value="DE">Germany</asp:ListItem>
                                    <asp:ListItem Value="GH">Ghana</asp:ListItem>
                                    <asp:ListItem Value="GI">Gibraltar</asp:ListItem>
                                    <asp:ListItem Value="GR">Greece</asp:ListItem>
                                    <asp:ListItem Value="GL">Greenland</asp:ListItem>
                                    <asp:ListItem Value="GD">Grenada</asp:ListItem>
                                    <asp:ListItem Value="GP">Guadeloupe</asp:ListItem>
                                    <asp:ListItem Value="GU">Guam</asp:ListItem>
                                    <asp:ListItem Value="GT">Guatemala</asp:ListItem>
                                    <asp:ListItem Value="GG">Guernsey</asp:ListItem>
                                    <asp:ListItem Value="GN">Guinea</asp:ListItem>
                                    <asp:ListItem Value="GW">Guinea-Bissau</asp:ListItem>
                                    <asp:ListItem Value="GY">Guyana</asp:ListItem>
                                    <asp:ListItem Value="HT">Haiti</asp:ListItem>
                                    <asp:ListItem Value="HM">Heard Island and McDonald Islands</asp:ListItem>
                                    <asp:ListItem Value="VA">Holy See (Vatican City State)</asp:ListItem>
                                    <asp:ListItem Value="HN">Honduras</asp:ListItem>
                                    <asp:ListItem Value="HK">Hong Kong</asp:ListItem>
                                    <asp:ListItem Value="HU">Hungary</asp:ListItem>
                                    <asp:ListItem Value="IS">Iceland</asp:ListItem>
                                    <asp:ListItem Value="IN">India</asp:ListItem>
                                    <asp:ListItem Value="ID">Indonesia</asp:ListItem>
                                    <asp:ListItem Value="IR">Iran Islamic Republic of</asp:ListItem>
                                    <asp:ListItem Value="IQ">Iraq</asp:ListItem>
                                    <asp:ListItem Value="IE">Ireland</asp:ListItem>
                                    <asp:ListItem Value="IM">Isle of Man</asp:ListItem>
                                    <asp:ListItem Value="IL">Israel</asp:ListItem>
                                    <asp:ListItem Value="IT">Italy</asp:ListItem>
                                    <asp:ListItem Value="JM">Jamaica</asp:ListItem>
                                    <asp:ListItem Value="JP">Japan</asp:ListItem>
                                    <asp:ListItem Value="JE">Jersey</asp:ListItem>
                                    <asp:ListItem Value="JO">Jordan</asp:ListItem>
                                    <asp:ListItem Value="KZ">Kazakhstan</asp:ListItem>
                                    <asp:ListItem Value="KE">Kenya</asp:ListItem>
                                    <asp:ListItem Value="KI">Kiribati</asp:ListItem>
                                    <asp:ListItem Value="KP">Korea Democratic People's Republic of</asp:ListItem>
                                    <asp:ListItem Value="KR">Korea Republic of</asp:ListItem>
                                    <asp:ListItem Value="KW">Kuwait</asp:ListItem>
                                    <asp:ListItem Value="KG">Kyrgyzstan</asp:ListItem>
                                    <asp:ListItem Value="LA">Lao People's Democratic Republic</asp:ListItem>
                                    <asp:ListItem Value="LV">Latvia</asp:ListItem>
                                    <asp:ListItem Value="LB">Lebanon</asp:ListItem>
                                    <asp:ListItem Value="LS">Lesotho</asp:ListItem>
                                    <asp:ListItem Value="LR">Liberia</asp:ListItem>
                                    <asp:ListItem Value="LY">Libyan Arab Jamahiriya</asp:ListItem>
                                    <asp:ListItem Value="LI">Liechtenstein</asp:ListItem>
                                    <asp:ListItem Value="LT">Lithuania</asp:ListItem>
                                    <asp:ListItem Value="LU">Luxembourg</asp:ListItem>
                                    <asp:ListItem Value="MO">Macao</asp:ListItem>
                                    <asp:ListItem Value="MK">Macedonia The Former Yugoslav Republic of</asp:ListItem>
                                    <asp:ListItem Value="MG">Madagascar</asp:ListItem>
                                    <asp:ListItem Value="MW">Malawi</asp:ListItem>
                                    <asp:ListItem Value="MY">Malaysia</asp:ListItem>
                                    <asp:ListItem Value="MV">Maldives</asp:ListItem>
                                    <asp:ListItem Value="ML">Mali</asp:ListItem>
                                    <asp:ListItem Value="MT">Malta</asp:ListItem>
                                    <asp:ListItem Value="MH">Marshall Islands</asp:ListItem>
                                    <asp:ListItem Value="MQ">Martinique</asp:ListItem>
                                    <asp:ListItem Value="MR">Mauritania</asp:ListItem>
                                    <asp:ListItem Value="MU">Mauritius</asp:ListItem>
                                    <asp:ListItem Value="YT">Mayotte</asp:ListItem>
                                    <asp:ListItem Value="MX">Mexico</asp:ListItem>
                                    <asp:ListItem Value="FM">Microneia Federated States of</asp:ListItem>
                                    <asp:ListItem Value="MD">Moldova</asp:ListItem>
                                    <asp:ListItem Value="MC">Monaco</asp:ListItem>
                                    <asp:ListItem Value="MN">Mongolia</asp:ListItem>
                                    <asp:ListItem Value="ME">Montenegro</asp:ListItem>
                                    <asp:ListItem Value="MS">Montserrat</asp:ListItem>
                                    <asp:ListItem Value="MA">Morocco</asp:ListItem>
                                    <asp:ListItem Value="MZ">Mozambique</asp:ListItem>
                                    <asp:ListItem Value="MM">Myanmar</asp:ListItem>
                                    <asp:ListItem Value="NA">Namibia</asp:ListItem>
                                    <asp:ListItem Value="NR">Nauru</asp:ListItem>
                                    <asp:ListItem Value="NP">Nepal</asp:ListItem>
                                    <asp:ListItem Value="NL">Netherlands</asp:ListItem>
                                    <asp:ListItem Value="AN">Netherlands Antilles</asp:ListItem>
                                    <asp:ListItem Value="NC">New Caledonia</asp:ListItem>
                                    <asp:ListItem Value="NZ">New Zealand</asp:ListItem>
                                    <asp:ListItem Value="NI">Nicaragua</asp:ListItem>
                                    <asp:ListItem Value="NE">Niger</asp:ListItem>
                                    <asp:ListItem Value="NG">Nigeria</asp:ListItem>
                                    <asp:ListItem Value="NU">Niue</asp:ListItem>
                                    <asp:ListItem Value="NF">Norfolk Island</asp:ListItem>
                                    <asp:ListItem Value="MP">Northern Mariana Islands</asp:ListItem>
                                    <asp:ListItem Value="NO">Norway</asp:ListItem>
                                    <asp:ListItem Value="OM">Oman</asp:ListItem>
                                    <asp:ListItem Value="PK">Pakistan</asp:ListItem>
                                    <asp:ListItem Value="PW">Palau</asp:ListItem>
                                    <asp:ListItem Value="PS">Palestinian Territory Occupied</asp:ListItem>
                                    <asp:ListItem Value="PA">Panama</asp:ListItem>
                                    <asp:ListItem Value="PG">Papua New Guinea</asp:ListItem>
                                    <asp:ListItem Value="PY">Paraguay</asp:ListItem>
                                    <asp:ListItem Value="PE">Peru</asp:ListItem>
                                    <asp:ListItem Value="PH">Philippines</asp:ListItem>
                                    <asp:ListItem Value="PN">Pitcairn</asp:ListItem>
                                    <asp:ListItem Value="PL">Poland</asp:ListItem>
                                    <asp:ListItem Value="PT">Portugal</asp:ListItem>
                                    <asp:ListItem Value="PR">Puerto Rico</asp:ListItem>
                                    <asp:ListItem Value="QA">Qatar</asp:ListItem>
                                    <asp:ListItem Value="RE">R&#233;union</asp:ListItem>
                                    <asp:ListItem Value="RO">Romania</asp:ListItem>
                                    <asp:ListItem Value="RU">Russian Federation</asp:ListItem>
                                    <asp:ListItem Value="RW">Rwanda</asp:ListItem>
                                    <asp:ListItem Value="BL">Saint Barth&#233;lemy</asp:ListItem>
                                    <asp:ListItem Value="SH">Saint Helena</asp:ListItem>
                                    <asp:ListItem Value="KN">Saint Kitts and Nevis</asp:ListItem>
                                    <asp:ListItem Value="LC">Saint Lucia</asp:ListItem>
                                    <asp:ListItem Value="MF">Saint Martin</asp:ListItem>
                                    <asp:ListItem Value="PM">Saint Pierre and Miquelon</asp:ListItem>
                                    <asp:ListItem Value="VC">Saint Vincent and the Grenadines</asp:ListItem>
                                    <asp:ListItem Value="WS">Samoa</asp:ListItem>
                                    <asp:ListItem Value="SM">San Marino</asp:ListItem>
                                    <asp:ListItem Value="ST">Sao Tome and Principe</asp:ListItem>
                                    <asp:ListItem Value="SA">Saudi Arabia</asp:ListItem>
                                    <asp:ListItem Value="SN">Senegal</asp:ListItem>
                                    <asp:ListItem Value="RS">Serbia</asp:ListItem>
                                    <asp:ListItem Value="SC">Seychelles</asp:ListItem>
                                    <asp:ListItem Value="SL">Sierra Leone</asp:ListItem>
                                    <asp:ListItem Value="SG">Singapore</asp:ListItem>
                                    <asp:ListItem Value="SK">Slovakia</asp:ListItem>
                                    <asp:ListItem Value="SI">Slovenia</asp:ListItem>
                                    <asp:ListItem Value="SB">Solomon Islands</asp:ListItem>
                                    <asp:ListItem Value="SO">Somalia</asp:ListItem>
                                    <asp:ListItem Value="ZA">South Africa</asp:ListItem>
                                    <asp:ListItem Value="GS">South Georgia and the South Sandwich Islands</asp:ListItem>
                                    <asp:ListItem Value="ES">Spain</asp:ListItem>
                                    <asp:ListItem Value="LK">Sri Lanka</asp:ListItem>
                                    <asp:ListItem Value="SD">Sudan</asp:ListItem>
                                    <asp:ListItem Value="SR">Suriname</asp:ListItem>
                                    <asp:ListItem Value="SJ">Svalbard and Jan Mayen</asp:ListItem>
                                    <asp:ListItem Value="SZ">Swaziland</asp:ListItem>
                                    <asp:ListItem Value="SE">Sweden</asp:ListItem>
                                    <asp:ListItem Value="CH">Switzerland</asp:ListItem>
                                    <asp:ListItem Value="SY">Syrian Arab Republic</asp:ListItem>
                                    <asp:ListItem Value="TW">Taiwan Province of China</asp:ListItem>
                                    <asp:ListItem Value="TJ">Tajikistan</asp:ListItem>
                                    <asp:ListItem Value="TZ">Tanzania United Republic of</asp:ListItem>
                                    <asp:ListItem Value="TH">Thailand</asp:ListItem>
                                    <asp:ListItem Value="TL">Timor-Leste</asp:ListItem>
                                    <asp:ListItem Value="TG">Togo</asp:ListItem>
                                    <asp:ListItem Value="TK">Tokelau</asp:ListItem>
                                    <asp:ListItem Value="TO">Tonga</asp:ListItem>
                                    <asp:ListItem Value="TT">Trinidad and Tobago</asp:ListItem>
                                    <asp:ListItem Value="TN">Tunisia</asp:ListItem>
                                    <asp:ListItem Value="TR">Turkey</asp:ListItem>
                                    <asp:ListItem Value="TM">Turkmenistan</asp:ListItem>
                                    <asp:ListItem Value="TC">Turks and Caicos Islands</asp:ListItem>
                                    <asp:ListItem Value="TV">Tuvalu</asp:ListItem>
                                    <asp:ListItem Value="UG">Uganda</asp:ListItem>
                                    <asp:ListItem Value="UA">Ukraine</asp:ListItem>
                                    <asp:ListItem Value="AE">United Arab Emirates</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="GB">United Kingdom</asp:ListItem>
                                    <asp:ListItem Value="US">United States</asp:ListItem>
                                    <asp:ListItem Value="UM">United States Minor Outlying Islands</asp:ListItem>
                                    <asp:ListItem Value="UY">Uruguay</asp:ListItem>
                                    <asp:ListItem Value="UZ">Uzbekistan</asp:ListItem>
                                    <asp:ListItem Value="VU">Vanuatu</asp:ListItem>
                                    <asp:ListItem Value="VE">Venezuela</asp:ListItem>
                                    <asp:ListItem Value="VN">Viet Nam</asp:ListItem>
                                    <asp:ListItem Value="VG">Virgin Islands British</asp:ListItem>
                                    <asp:ListItem Value="VI">Virgin Islands U.S.</asp:ListItem>
                                    <asp:ListItem Value="WF">Wallis and Futuna</asp:ListItem>
                                    <asp:ListItem Value="EH">Western Sahara</asp:ListItem>
                                    <asp:ListItem Value="YE">Yemen</asp:ListItem>
                                    <asp:ListItem Value="ZM">Zambia</asp:ListItem>
                                    <asp:ListItem Value="ZW">Zimbabwe</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="pull-right">
                            <div class="col-md-12">
                               <div class="col-md-12">
                                <span class="btn btn-primary btn-sm" style="cursor: pointer;" onclick="Update_Contacts();">Update Contact</span>
                            </div>
                                 </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <asp:Label ID="Label1" runat="server" EnableViewState="false" ForeColor="Red"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>


            <div class="panel panel-default">
                <div class="panel-heading">Sector Details </div>
                <div class="panel-body" style="line-height: 34px; padding: 0px!important;">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <asp:Repeater ID="rptrSect" runat="server" OnItemDataBound="rptrSect_ItemDataBound">
                                <HeaderTemplate>
                                    <table width='100%' cellpadding='0' cellspacing='0' class="fl1-table" style='margin-bottom: 0px;'>
                                        <tr>
                                            <td class='gdvh'>SrNo</td>
                                            <td class='gdvh'>From</td>
                                            <td class='gdvh'>To</td>
                                            <td class='gdvh'>AirV</td>
                                            <td class='gdvh'>FLT No.</td>
                                            <td class='gdvh'>Class</td>
                                            <td class='gdvh'>From Date</td>
                                            <td class='gdvh'>From Time</td>
                                            <td class='gdvh'>To Date</td>
                                            <td class='gdvh'>To Time</td>
                                            <td class='gdvh'>Air Ref</td>
                                            <td class='gdvh'>Baggage</td>
                                            <td class='gdvh'>Trip Type</td>
                                            <td class='gdvh'>Status</td>
                                            <td class='gdvh'>Cabin</td>
                                            <td class='gdvh'>Action</td>
                                        </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr id='trS<%# Eval("SrNo") %>'>
                                        <td class="gdvr"><%# Container.ItemIndex+ 1 %></td>
                                        <td class="gdvr exelcss" >
                                            <input maxlength="3" style="padding: 6px 3px; font-size: 12px;" class="form-control" id='txtFrom<%# Eval("SrNo")%>' onblur="oblurUpdateSectors(&#39;<%# Eval("SrNo")%>&#39;,&#39;From&#39;,&#39;<%# Eval("FromDest")%>&#39;);" value='<%# Eval("FromDest")%>' type="text">
                                        </td>
                                        <td class="gdvr exelcss">
                                            <input maxlength="3" style="padding: 6px 3px; font-size: 12px;" class="form-control" id='txtTo<%# Eval("SrNo")%>' onblur="oblurUpdateSectors(&#39;<%# Eval("SrNo")%>&#39;,&#39;To&#39;,&#39;<%# Eval("ToDest")%>&#39;);" value='<%# Eval("ToDest")%>' type="text">
                                        </td>

                                        <td class="gdvr exelcss" >
                                            <input maxlength="2" style="padding: 6px 3px; font-size: 12px;" class="form-control" id='txtAirV<%# Eval("SrNo")%>' onblur="oblurUpdateSectors(&#39;<%# Eval("SrNo")%>&#39;,&#39;AirV&#39;,&#39;<%# Eval("CarierName")%>&#39;);" value='<%# Eval("CarierName")%>' type="text">
                                        </td>
                                        <td class="gdvr exelcss" >
                                            <input maxlength="8" style="padding: 6px 3px; font-size: 12px;" class="form-control" id='txtFLTNO<%# Eval("SrNo")%>' onblur="oblurUpdateSectors(&#39;<%# Eval("SrNo")%>&#39;,&#39;FLTNO&#39;,&#39;<%# Eval("FlightNo")%>&#39;);" value='<%# Eval("FlightNo")%>' type="text">
                                        </td>
                                        <td class="gdvr exelcss" >
                                            <input maxlength="1" style="padding: 6px 3px; font-size: 12px;" class="form-control" id='txtClass<%# Eval("SrNo")%>' onblur="oblurUpdateSectors(&#39;<%# Eval("SrNo")%>&#39;,&#39;Class&#39;,&#39;<%# Eval("FClass")%>&#39;);" value='<%# Eval("FClass")%>' type="text">
                                        </td>
                                        <td class="gdvr exelcss" >
                                            <input maxlength="10" style="padding: 6px 3px; font-size: 12px;" class="form-control" id='txtFromDate<%# Eval("SrNo")%>' onblur="oblurUpdateSectors(&#39;<%# Eval("SrNo")%>&#39;,&#39;FromDate&#39;,&#39;<%# Convert.ToDateTime(Eval("FromDateTime")).ToString("dd/MM/yyyy HH:mm")%>&#39;);" autocomplete="off" value='<%#Convert.ToDateTime(Eval("FromDateTime")).ToString("dd/MM/yyyy")%>' type="text">
                                        </td>
                                        <td class="gdvr exelcss" >
                                            <input maxlength="5" style="padding: 6px 3px; font-size: 12px;" class="form-control" id='txtFromTime<%# Eval("SrNo")%>' onblur="oblurUpdateSectors(&#39;<%# Eval("SrNo")%>&#39;,&#39;FromTime&#39;,&#39;<%#  Convert.ToDateTime(Eval("FromDateTime")).ToString("dd/MM/yyyy HH:mm")%>&#39;);" value='<%# Convert.ToDateTime(Eval("FromDateTime")).ToString("HH:mm")%>' type="text">
                                        </td>
                                        <td class="gdvr exelcss" >
                                            <input maxlength="10" style="padding: 6px 3px; font-size: 12px;" class="form-control" id='txtToDate<%# Eval("SrNo")%>' onblur="oblurUpdateSectors(&#39;<%# Eval("SrNo")%>&#39;,&#39;ToDate&#39;,&#39;<%#  Convert.ToDateTime(Eval("ToDateTime")).ToString("dd/MM/yyyy HH:mm")%>&#39;);" autocomplete="off" value='<%#Convert.ToDateTime(Eval("ToDateTime")).ToString("dd/MM/yyyy")%>' type="text">
                                        </td>
                                        <td class="gdvr exelcss" >
                                            <input maxlength="5" style="padding: 6px 3px; font-size: 12px;" class="form-control" id='txtToTime<%# Eval("SrNo")%>' onblur="oblurUpdateSectors(&#39;<%# Eval("SrNo")%>&#39;,&#39;ToTime&#39;,&#39;<%#  Convert.ToDateTime(Eval("ToDateTime")).ToString("dd/MM/yyyy HH:mm")%>&#39;);" value='<%# Convert.ToDateTime(Eval("ToDateTime")).ToString("HH:mm")%>' type="text">
                                        </td>

                                        <td class="gdvr exelcss" >
                                            <input maxlength="10" class="form-control" style="padding: 6px 3px; font-size: 12px;" id='txtAirlineConfirmationCode<%# Eval("SrNo")%>' onblur="oblurUpdateSectors(&#39;<%# Eval("SrNo")%>&#39;,&#39;AirlineConfirmationCode&#39;,&#39;<%# Eval("AirlineConfirmationCode")%>&#39;);" value='<%# Eval("AirlineConfirmationCode")%>' type="text">
                                        </td>

                                        <td class="gdvr exelcss">
                                            <input class="form-control" style="padding: 6px 3px; font-size: 12px;" id='txtBaggageAllownce<%# Eval("SrNo")%>' onblur="oblurUpdateSectors(&#39;<%# Eval("SrNo")%>&#39;,&#39;BaggageAllownce&#39;,&#39;<%# Eval("BaggageAllownce")%>&#39;);" value='<%# Eval("BaggageAllownce")%>' type="text">
                                        </td>

                                        <td class="gdvr exelcss">
                                            <input class="form-control" maxlength="1" onkeypress="return event.charCode >= 48 &amp;&amp; event.charCode <= 49" style="padding: 4px 2px 3px 9px;" id='txtTripType<%# Eval("SrNo")%>' onblur="(&#39;<%# Eval("SrNo")%>&#39;,&#39;TripType&#39;,&#39;<%# Eval("TripID")%>&#39;);" value='<%# Eval("TripID")%>' type="text">
                                        </td>


                                        <td class="gdvr exelcss" >
                                            <input maxlength="6" class="form-control" style="padding: 6px 3px; font-size: 12px;" id='txtStatus<%# Eval("SrNo")%>' onblur="oblurUpdateSectors(&#39;<%# Eval("SrNo")%>&#39;,&#39;Status&#39;,&#39;<%# Eval("FStatus")%>&#39;);" value='<%# Eval("FStatus")%>' type="text">
                                        </td>

                                        <td class="gdvr exelcss" >
                                            <input type="text" class="ddlCabinClass" style="display: none!important" name="name" id='<%# Eval("SrNo")%>' value='<%# Eval("CabinClass")%>' />

                                            <select id='ddlCabin<%# Eval("SrNo")%>' onblur="oblurUpdateSectors(&#39;<%# Eval("SrNo")%>&#39;,&#39;CabinClass&#39;,&#39;<%# Eval("CabinClass")%>&#39;);" class="form-control" style="width: 160px!important; padding: 6px 7px">
                                                <option value="">ANY</option>
                                                <option value="Economy">Economy</option>
                                                <option value="Business">Business</option>
                                                <option value="FirstClass">FirstClass</option>
                                                <option value="Premium">Premium Economy</option>
                                            </select>
                                        </td>

                                        <td class="gdvr exelcss">
                                            <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn btn-danger btn-sm" ToolTip='<%# Eval("SrNo") %>' OnClientClick="DelSector(this); return false; " />
                                        </td>
                                    </tr>

                                </ItemTemplate>
                                <FooterTemplate>
                                    </table>
                                </FooterTemplate>
                            </asp:Repeater>

                            <table width='100%' cellpadding='0' cellspacing='0' class='table' style='background-color: #ddd; margin-bottom: 0px;'>
                                <tr>
                                    <td class='gdvh'>SrNo</td>
                                    <td class='gdvh'>From</td>
                                    <td class='gdvh'>To</td>
                                    <td class='gdvh'>AirV</td>
                                    <td class='gdvh'>FLT No.</td>
                                    <td class='gdvh'>Class</td>
                                    <td class='gdvh'>From Date</td>
                                    <td class='gdvh'>From Time</td>
                                    <td class='gdvh'>To Date</td>
                                    <td class='gdvh'>To Time</td>
                                    <td class='gdvh'>Air Ref</td>
                                    <td class='gdvh'>Baggage</td>
                                    <td class='gdvh'>Trip Type</td>
                                    <td class='gdvh'>Status</td>
                                    <td class='gdvh'>Cabin</td>
                                    <td class='gdvh'></td>
                                </tr>
                                <tr>
                                    <td class='gdvh'></td>
                                    <td class='gdvh' >
                                        <asp:TextBox ID="txtFrom" Style="padding: 6px 3px; font-size: 12px;" CssClass="form-control" MaxLength="3" runat="server"></asp:TextBox></td>
                                    <td class='gdvh'>
                                        <asp:TextBox ID="txtTo" Style="padding: 6px 3px; font-size: 12px;" CssClass="form-control" MaxLength="3" runat="server"></asp:TextBox></td>
                                    <td class='gdvh'>
                                        <asp:TextBox ID="txtAirline" Style="padding: 6px 3px; font-size: 12px;" CssClass="form-control" MaxLength="3" runat="server"></asp:TextBox></td>
                                    <td class='gdvh' >
                                        <asp:TextBox ID="txtFlightNo" Style="padding: 6px 3px; font-size: 12px;" CssClass="form-control" MaxLength="8" runat="server"></asp:TextBox></td>
                                    <td class='gdvh' >
                                        <asp:TextBox ID="txtClass" Style="padding: 6px 3px; font-size: 12px;" CssClass="form-control" MaxLength="1" runat="server"></asp:TextBox></td>
                                    <td class='gdvh' >
                                        <asp:TextBox ID="txtFromDate" autocomplete="off" onclick="showCalender(this);" Style="padding: 6px 3px; font-size: 12px;" CssClass="form-control" runat="server"></asp:TextBox></td>
                                    <td class='gdvh' >
                                        <asp:TextBox ID="txtFromTime" MaxLength="5" Style="padding: 6px 3px; font-size: 12px;" CssClass="form-control" runat="server"></asp:TextBox></td>
                                    <td class='gdvh' >
                                        <asp:TextBox ID="txtToDate" autocomplete="off" onclick="showCalender(this);" Style="padding: 6px 3px; font-size: 12px;" CssClass="form-control" runat="server"></asp:TextBox></td>
                                    <td class='gdvh'>
                                        <asp:TextBox ID="txtToTime" MaxLength="5" Style="padding: 6px 3px; font-size: 12px;" CssClass="form-control" runat="server"></asp:TextBox></td>

                                    <td class='gdvh' >
                                        <asp:TextBox ID="txtAirlineConfirmationCode" Style="padding: 6px 3px; font-size: 12px;" CssClass="form-control" runat="server" MaxLength="10"></asp:TextBox>
                                    </td>
                                    <td class='gdvh' >
                                        <asp:TextBox ID="txtBaggageAllownce" Style="padding: 6px 3px; font-size: 12px;" CssClass="form-control" runat="server"></asp:TextBox>
                                    </td>
                                    <td class='gdvh' >
                                        <asp:TextBox ID="txtTripID" MaxLength="1" onkeypress="return event.charCode >= 48 &amp;&amp; event.charCode <= 49" Style="padding: 6px 3px; font-size: 12px;" CssClass="form-control" runat="server"></asp:TextBox>
                                    </td>

                                    <td class='gdvh' >
                                        <asp:TextBox ID="txtStatus" MaxLength="5" Style="padding: 6px 3px; font-size: 12px;" CssClass="form-control" runat="server"></asp:TextBox></td>

                                    <td class='gdvh' >
                                        <asp:DropDownList runat="server" ID="ddlCabin" class="form-control" Style="width: 160px!important; padding: 6px 7px">
                                            <asp:ListItem Text="ANY" Value="" />
                                            <asp:ListItem Text="Economy" Value="Economy" />
                                            <asp:ListItem Text="Business" Value="Business" />
                                            <asp:ListItem Text="FirstClass" Value="FirstClass" />
                                            <asp:ListItem Text="Premium" Value="Premium Economy" />
                                        </asp:DropDownList>
                                    </td>


                                    <td class='gdvh'>
                                        <asp:Button ID="btnAddSectors" runat="server" Text="Add" CssClass="btn btn-success" OnClientClick="return validateSectors()" OnClick="btnAddSectors_Click" /></td>
                                </tr>
                                <tr>
                                    <td colspan="4"></td>
                                    <td colspan="4"></td>
                                    <td colspan="4">
                                        <asp:Literal ID="ltrSectors" runat="server"></asp:Literal></td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="row text-center">

                        <asp:UpdateProgress ID="UpdateProgress3" AssociatedUpdatePanelID="UpdatePanel3" DisplayAfter="1"
                            runat="server">
                            <ProgressTemplate>
                                <asp:Literal ID="messageSect" runat="server" Text="Wait..."></asp:Literal>
                            </ProgressTemplate>
                        </asp:UpdateProgress>

                    </div>

                </div>
            </div>

            <div class="panel panel-default">
                <div class="panel-heading">Price break up</div>
                <div class="panel-body" style="line-height: 34px; padding: 0px!important;">

                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:Repeater ID="rptPrice" runat="server" OnItemCommand="rptPrice_ItemCommand" OnItemDataBound="rptPrice_ItemDataBound">
                                <HeaderTemplate>
                                    <table width='100%' cellpadding='0' cellspacing='0' height="100px" class="fl1-table" style='margin-bottom: 0px'>
                                        <tr>
                                            <td class='gdvh'>Charge Type</td>
                                            <td class='gdvh'>Charge For</td>
                                            <td class='gdvh'>Sell Amt</td>
                                            <td class='gdvh'>Cost Amt</td>
                                            <td class='gdvh'></td>
                                        </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td class="gdvr exelcss">
                                            <asp:Label ID="lbtChargeID" runat="server" Text='<%# Eval("ChargeID")%>'></asp:Label>
                                        </td>
                                        <td class="gdvr exelcss">
                                            <asp:Label ID="lblChargeFor" runat="server" Text='<%# Eval("ChargesFor")%>'></asp:Label>
                                        </td>
                                        <td class="gdvr exelcss">
                                            <input maxlength="10" class="form-control" style="font-size: 12px;" id='txtSalePrice' value='<%# Convert.ToDouble(Eval("SellPrice")) %>' type="text"></td>
                                        <td class="gdvr exelcss">
                                            <input maxlength="10" class="form-control" style=" font-size: 12px;" id='txtCostPrice' value='<%# Convert.ToDouble(Eval("CostPrice"))%>' type="text"></td>
                                        <td class="gdvr">
                                            <asp:Button ID="btnDelete" CommandName="del" runat="server" Text="Delete" CssClass="btn btn-danger btn-sm" ToolTip='<%# Eval("SrNo") %>' />
                                        </td>
                                    </tr>
                                </ItemTemplate>

                            </asp:Repeater>
                            <table width='100%' cellpadding='0' cellspacing='0' class='table' style='background-color: #ddd; margin-bottom: 0px;'>
                                <tr>

                                    <td class='gdvh'>Charge Type</td>
                                    <td class='gdvh'>Charge For</td>
                                    <td class='gdvh'>Sell Amt</td>
                                    <td class='gdvh'>Cost Amt</td>
                                    <td class='gdvh'></td>
                                    <td class='gdvh'></td>
                                </tr>
                                <tr>
                                    <td class='gdvh'>

                                        <select id="ddlPayType" class="form-control" runat="server">
                                            <option value="Fare" selected="selected">Fare</option>
                                            <option value="Tax">Tax</option>
                                            <option value="Markup">Markup</option>
                                            <option value="Safi">Safi</option>
                                            <option value="Atol">Atol</option>
                                            <option value="CC">Card Charge</option>
                                            <option value="Admin">Admin Charge</option>
                                            <option value="PTS">PTS</option>
                                            <option value="Issuance">Issuance Charge</option>
                                            <option value="Others">Others</option>
                                            <option value="Refund">Refund</option>
                                            <option value="GTT">GTT</option>
                                            <option value="FXL">FXL</option>
                                            <option value="XPN">NEW XP</option>
                                            <option value="BRB">BRB</option>


                                        </select>
                                    </td>
                                    <td class='gdvh'>

                                        <select id="ddlChargeFor" class="form-control" runat="server">
                                            <option value="ADT">Adult</option>
                                            <option value="CNN">Child</option>
                                            <option value="INF">Infant</option>

                                            <option value="NA" selected="selected">NA</option>
                                        </select>
                                    </td>

                                    <td class='gdvh'>
                                        <input maxlength="10" class="form-control"  onkeyup="AllowDecimal(this)" id='txtSalePriceF' runat="server" type="text" /></td>
                                    <td class='gdvh'>
                                        <input maxlength="10" class="form-control"  onkeyup="AllowDecimal(this)" id='txtCostPriceF' runat="server" type="text" /></td>

                                    <td class='gdvh'>
                                        <asp:Button ID="btnAdd" CssClass="btn btn-primary btn-sm" runat="server" OnClientClick="return validate();" Text="Add Price" OnClick="btnAdd_Click" />

                                    </td>
                                    <td class='gdvh'>
                                        <asp:Button ID="btnDeletePrice" CssClass="btn btn-danger btn-sm" runat="server" OnClientClick="return ConfirmDelete();" Text="Delete All" OnClick="btnDeletePrice_Click" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <div class="row text-center">

                        <asp:UpdateProgress ID="UpdateProgress2" AssociatedUpdatePanelID="UpdatePanel2" DisplayAfter="1"
                            runat="server">
                            <ProgressTemplate>
                                <asp:Literal ID="Literal2" runat="server" Text="Wait"></asp:Literal>
                            </ProgressTemplate>
                        </asp:UpdateProgress>

                    </div>
                </div>
            </div>

            <div class="panel panel-default">
                <div class="panel-heading">Transaction Details</div>
                <div class="panel-body" style="line-height: 34px; padding: 0px!important;">
                    <asp:Repeater ID="rptrTransaction" runat="server" OnItemDataBound="rptrTransaction_ItemDataBound">
                        <HeaderTemplate>
                            <table width='100%' cellpadding='0' cellspacing='0' class="fl1-table" style='margin-bottom: 0px;'>
                                <tr>
                                    <td class='gdvh'>TrnsNo</td>
                                    <td class='gdvh'>Trns Amount</td>
                                    <td class='gdvh'>Currency Type</td>
                                    <td class='gdvh'>Pay ID</td>
                                    <td class='gdvh'>Trns Type</td>
                                    <td class='gdvh'>Payment Status</td>
                                    <td class='gdvh'>Trns DateTime</td>
                                    <td class='gdvh'>Trns By</td>
                                     <td class='gdvh'>Action</td>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td class='gdvr'>
                                    <asp:Literal ID="TrnsNo" Text='<%# Eval("TrnsNo")%>' runat="server"></asp:Literal>
                                </td>
                                <td class='gdvr'>
                                    <input maxlength="10" class="form-control" style="font-size: 12px;" onkeyup="AllowDecimal(this)" id='txtTrnsAmount<%# Eval("TrnsNo") %>' onblur="oblurTransactionUpdate(&#39;<%# Eval("TrnsNo")%>&#39;,&#39;TrnsAmount&#39;,&#39;<%# Eval("TrnsAmount")%>&#39;);" value='<%# Convert.ToDouble(Eval("TrnsAmount")) %>' type="text" /></td>
                                <td class='gdvr'><%# Eval("TrnsCurrencyType") %></td>
                                <td class='gdvr'><%# Eval("TrnsAuthNo") %></td>
                                <td class='gdvr'><%# Eval("TrnsType") %></td>
                                <td class='gdvr'><%# Eval("TrnsPaymentStatus") %></td>
                                <td class='gdvr'><%# Eval("TrnsDateTime") %></td>
                                <td class='gdvr'><%# Eval("TrnsBy") %></td>
                                <td class='gdvr'><a href="../../Admin/Chargemail.aspx?BID=<%# Request.QueryString["BID"].ToString() %>&PID=001&TRS=<%# Eval("TrnsNo") %>" target="_blank">SEND Charge Mail</a></td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
            </div>

               <div class="panel panel-default">
                <div class="panel-heading">Auth Documents</div>
                <div class="panel-body" style="line-height: 34px; padding: 0px!important;">

                  
                            <asp:Repeater ID="rptrAuthdoc" runat="server" OnItemDataBound="rptPrice_ItemDataBound">
                                <HeaderTemplate>
                                    <table width='100%' cellpadding='0' cellspacing='0' height="100px" class="fl1-table" style='margin-bottom: 0px'>
                                        <tr>
                                           <td class='gdvh'>Doc Type</td>
                                    <td class='gdvh'>Document</td>
                                    <td class='gdvh'>Action</td>
                                        </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td class="gdvr exelcss">
                                            <asp:Label ID="lbtChargeID" runat="server" Text='<%# Eval("DocType")%>'></asp:Label>
                                        </td>
                                        <td class="gdvr exelcss">
                                            <asp:Label ID="lblChargeFor" runat="server" Text='<%# Eval("DocPath")%>'></asp:Label>
                                        </td>
                                       <td class="gdvr exelcss"><a class="btn btn-success" role="button" href='<%# Eval("DocPath")%>' download="PAX_Document">Download</a></td>
                                    </tr>
                                </ItemTemplate>

                            </asp:Repeater>
                          
                        
                    <table width='100%' cellpadding='0' cellspacing='0'   style='background-color: #ddd; margin-bottom: 0px;'>
                                <tr>

                                    <td class='gdvh'>Document Type</td>
                                    <td class='gdvh'>Document Select</td>
                                    <td class='gdvh'>Action</td>
                                   
                                </tr>
                                <tr>
                                    <td class='gdvh'>

                                        <asp:DropDownList  id="DocType" class="form-control" runat="server">
                                            <asp:ListItem value="AuthDoc" Selected="True">Auth Doc</asp:ListItem>
                                            <asp:ListItem value="Receipt">Receipt</asp:ListItem>
                                            <asp:ListItem value="Card">Card Copy</asp:ListItem>
                                            <asp:ListItem value="PhotoId">Photo Id</asp:ListItem>
                                            <asp:ListItem value="Others">Others</asp:ListItem>
                                           
                                        </asp:DropDownList>
                                    </td>
                                    <td class='gdvh'>
                                       
                                       <asp:FileUpload id="FileUpLoad1" runat="server" />  
                                    </td>

                                  
                                    <td class='gdvh'>
                                        <asp:Button ID="Button2" CssClass="btn btn-danger btn-sm" runat="server"  Text="Upload Now" OnClick="UploadBtn_Click" />
                                    </td>
                                </tr>
                            </table>
                </div>
            </div>

            
                     
            <div class="panel panel-default p-20" style="width:50%;float:left;height: 110px;">

                <div class="panel-body">
                    <div class="col-md-12">
                        <div class="col-md-9">
                            <asp:TextBox ID="txtRemarks" Width="100%" ValidationGroup="vgremarks" placeholder="Remarks" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <span class="btn btn-primary btn-sm" style="cursor: pointer;" onclick="AddRemarks();">Add Remarks</span>
                           
                            <asp:ValidationSummary ID="valsum" runat="server" ValidationGroup="vgremarks" ForeColor="Red" ShowMessageBox="true" ShowSummary="false" />
                            <asp:Button ID="btnHide" runat="server" CssClass="btn btn-warning" Visible="false" Text="Delete Dupe" OnClick="btnHide_Click" />
                        </div>
                      
                    </div>
                </div>

            </div>

            <div class="panel panel-default p-20" id="dvDate" runat="server" visible="false" style="width:49%;float:right;height: 110px;">

                <div class="panel-body">
                    <div class="col-md-12">
                       
                        <div class="col-md-9">
                            <asp:TextBox ID="txtBookingDate" onclick="showCalender(this);" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <span class="btn btn-primary btn-sm" style="cursor: pointer;" onclick="ChangeXPDate();">Change Date</span>

                        </div>
                      
                    </div>
                </div>

            </div>
        </div>
    </asp:Panel>
     
    <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label>

    <script type="text/javascript">

        var message = new MyMessage.message({
            iconFontSize: "20px",
            messageFontSize: "12px",
            showTime: 5000,
            align: "center",
            positions: {
                top: "100px",
                bottom: "100px",
                right: "100px",
                left: "100px"
            },
            message: "This is a message",
            type: "normal", // success,error,warning
        });


        function MirrorValue() {
            document.getElementById('<%=txtSalePriceF.ClientID%>').value = parseFloat(document.getElementById('<%= txtCostPriceF.ClientID %>').value);
        }
        function AllowDecimal(txt) {
            if (/[^\d.]/g.test(txt.value))
                txt.value = txt.value.replace(/[^\d.]/g, '');
        }
        function validate() {
            if (document.getElementById('<%=txtSalePriceF.ClientID%>').value == "") {
                alert("Enter Sale Price.");
                document.getElementById('<%=txtSalePriceF.ClientID%>').focus();
                return false;
            }
            if (document.getElementById('<%=txtCostPriceF.ClientID%>').value == "") {
                alert("Enter Cost Price.");
                document.getElementById('<%=txtCostPriceF.ClientID%>').focus();
                return false;
            }
        }
        function validatexp() {
            if (document.getElementById('<%=ddlStatus.ClientID%>').value == "") {
                alert("Select Booking Status.");
                document.getElementById('<%=ddlStatus.ClientID%>').focus();
                return false;
            }

        }
        if ($("#Text1").val() == "") {
            args.IsValid = false;
        }

        //Note :- Assigned By Works as Booking By   ddlBookingBy
        // Booking By Works as Assigned By  ddlBookingAssignedTo


        function Update_Summary() {
            var supplierRef = $("#<%=txtSuppleirRef.ClientID%>").val();
            var issuedDate = "";
            var oldCom = $("#<%=hfOldCompany.ClientID%>").val();
            var role = $("#<%=hidRole.ClientID%>").val();
            var updateCompany = '';
            var sourcemedia = '';
            var oldval = $("#<%=hfOldvalidating.ClientID%>").val();
            //var oldBookingBy = $("#<%=hfOldBookingBy.ClientID%>").val();

            var bookingBy = $("#<%=ddlBookingBy.ClientID%>").val();
            var oldAssignedBy = $("#<%=hfOldAssignedBy.ClientID%>").val();

            var assignedTo = $("#<%=ddlAssignedTo.ClientID%>").val();

            if (assignedTo != oldAssignedBy) {
                assignedTo = $("#<%=ddlAssignedTo.ClientID%>").val();
            }
            else {
                assignedTo = "";
            }

            if ($("#<%=ddlCompany.ClientID%>").val() != oldCom) {
                updateCompany = $("#<%=ddlCompany.ClientID%>").val();
            }

            if (!$("#<%=ddlSourceMedia.ClientID%>").disabled) {
                sourcemedia = $("#<%=ddlSourceMedia.ClientID%>").val();
            }


            var gds_Val = $("#<%=ddlGDS.ClientID%>").val();



            var EditGenDetails = {
                BookingID: $("#<%=hfBookingID.ClientID%>").val(),
                ProdID: $("#<%=hfProdID.ClientID%>").val(),
                PNR: $("#<%=txtPnr.ClientID%>").val(),
                BookingStatus: $("#<%=ddlStatus.ClientID%>").val(),
                UpdatedBy: $("#<%=hfUpdatedBy.ClientID%>").val(),
                Supplier: $("#<%=ddlSupplier.ClientID%>").val(),
                Destination: $("#<%=txtDestination.ClientID%>").val(),
                AtolType: "",
                Company: updateCompany,
                SupplierRef: supplierRef,
                Role: role,
                BookingBy: bookingBy,
                SourceMedia: sourcemedia,
                IssuedDate: issuedDate,
                ValidatingCarrier: $("#<%=txtValidatingCarrier.ClientID%>").val(),
                AssignedTo: assignedTo,
                GDS: gds_Val
            };
            waitingDialog.show('Please Wait...');
            $.ajax({
                type: "POST",
                url: "amendBooking.aspx/Update_Summary",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify(EditGenDetails),
                responseType: "json",
                success: function (data) {

                    if (data.d == "true") {
                        message.add("Booking summary updated", "success");
                    }
                    else {
                        message.setting("showTime", "8000");
                        message.add("Booking summary update failed", "warning");
                    }
                },
                error: function (data) { message.setting("showTime", "8000"); message.add("Booking summary update failed", "error"); }
            });
            waitingDialog.hide();

        }

        function Update_Contacts() {
            var EditContacts = {
                BookingID: $("#<%=hfBookingID.ClientID%>").val(),
                ProdID: $("#<%=hfProdID.ClientID%>").val(),
                Phone: $("#<%=txtPhone.ClientID%>").val(),
                Mobile: $("#<%=txtMobile.ClientID%>").val(),
                Email: $("#<%=txtEmail.ClientID%>").val(),
                Country: $("#<%=DdlCountry.ClientID%>").val(),
                City: $("#<%=txtCity.ClientID%>").val(),
                Address: $("#<%=txtAddress.ClientID%>").val(),
                UpdatedBy: $("#<%=hfUpdatedBy.ClientID%>").val()
            };
            waitingDialog.show('Please Wait...');
            $.ajax({
                type: "POST",
                url: "amendbooking.aspx/Update_Address",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify(EditContacts),
                responseType: "json",
                success: function (data) {

                    if (data.d == "true") {
                        message.add("Traveller Address  updated", "success");
                    }
                    else {
                        message.setting("showTime", "8000");
                        message.add("Traveller Address  update failed", "warning");
                    }
                },
                error: function (data) {
                    message.setting("showTime", "8000");
                    message.add("Traveller Address  update failed", "error");
                }
            });
            waitingDialog.hide();
        }

        function AddRemarks() {
            var AddComment = {
                BookingID: $("#<%=hfBookingID.ClientID%>").val(),
                ProdID: $("#<%=hfProdID.ClientID%>").val(),
                Remarks: $("#<%=txtRemarks.ClientID%>").val(),
                UpdatedBy: $("#<%=hfUpdatedBy.ClientID%>").val()
            };
            waitingDialog.show('Please Wait...');
            $.ajax({
                type: "POST",
                url: "amendbooking.aspx/AddRemarks",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify(AddComment),
                responseType: "json",
                success: function (data) {

                    if (data.d == "true") {
                        message.add("Booking Remarks added", "success");
                    }
                    else {
                        message.setting("showTime", "8000");
                        message.add("Booking Remarks failed", "warning");
                    }
                },
                error: function (data) {
                    message.setting("showTime", "8000");
                    message.add("Booking Remarks failed", "error");
                }
            });
            waitingDialog.hide();
        }

        function ChangeXPDate() {
            var changeDT = {
                BookingID: $("#<%=hfBookingID.ClientID%>").val(),
                UpdatedBy: $("#<%=hfUpdatedBy.ClientID%>").val(),
                BookingDate: $("#<%=txtBookingDate.ClientID%>").val()
            };
            waitingDialog.show('Please Wait...');
            $.ajax({
                type: "POST",
                url: "amendbooking.aspx/ChangeXPDate",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify(changeDT),
                responseType: "json",
                success: function () {

                    if (data.d == "true") {
                        message.add("XP Date Changed", "success");
                    }
                    else {
                        message.setting("showTime", "8000");
                        message.add("Changing failed", "warning");
                    }
                },
                error: function () {
                    message.setting("showTime", "8000");
                    message.add("Changing failed.", "error");
                }
            });
            waitingDialog.hide();
        }

        function DelPax(SNo) {
            var sid = $(SNo).attr("title");
            var PaxID = { SrNo: $(SNo).attr("title") }
            waitingDialog.show('Please Wait...');
            $.ajax({
                type: "POST",
                url: "amendbooking.aspx/DeletePax",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify(PaxID),
                responseType: "json",
                success: function (data) {
                    if (data.d == "true") {
                        message.add("Traveller deleted", "success");
                        $("#trP" + sid).hide();
                    }
                    else {
                        message.setting("showTime", "8000");
                        message.add("Traveller deletion failed", "warning");
                    }
                },
                error: function (data) {
                    message.setting("showTime", "8000");
                    message.add("Traveller deletion failed", "error");
                }
            });
            waitingDialog.hide();

        }

        function DelSector(SNo) {

            var sid = $(SNo).attr("title");
            var SecID = { SrNo: $(SNo).attr("title") }
            waitingDialog.show('Please Wait...');
            $.ajax({
                type: "POST",
                url: "amendbooking.aspx/DeleteSector",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify(SecID),
                responseType: "json",
                success: function (data) {

                    if (data.d == "true") {
                        message.add("Sector deleted", "success");
                        $("#trS" + sid).hide();
                    }
                    else {
                        message.setting("showTime", "8000");
                        message.add("Sector deletion failed", "warning");
                    }
                },
                error: function (data) {
                    message.setting("showTime", "8000");
                    message.add("Sector deletion failed", "error");
                }
            });
            waitingDialog.hide();

        }

        function oblurUpdateSectors(ID, UpdateField, OldValue) {
            var isUpdate = false;
            var _val = "";
            if (UpdateField == "CabinClass") {
                _val = $("#ddlCabin" + ID).val();
            }
            else {
                _val = $("#txt" + UpdateField + ID).val();
            }


            if (UpdateField != "CabinClass") {
                try {
                    if ($("#txt" + UpdateField + ID).val().toUpperCase() != OldValue.toUpperCase()) {
                        isUpdate = true;
                    }
                    else {
                        isUpdate = false;
                    }
                }
                catch (error) {
                    isUpdate = false;
                }
            }
            else {
                isUpdate = true;
            }



            if (isUpdate) {
                switch (UpdateField) {
                    case "From":
                        if ($("#txt" + UpdateField + ID).val().length != 3) {
                            $("#txt" + UpdateField + ID).focus();
                            alert("Origin only three charector is allowed");
                            return;
                        }
                        break;
                    case "To":
                        if ($("#txt" + UpdateField + ID).val().length != 3) {
                            $("#txt" + UpdateField + ID).focus();
                            alert("Destination only three charector is allowed");
                            return;
                        }
                        break;
                    case "AirV":
                        if ($("#txt" + UpdateField + ID).val() != "ANY") {
                            if ($("#txt" + UpdateField + ID).val().length != 2) {
                                $("#txt" + UpdateField + ID).focus();
                                alert("Airline only two charector or 'ANY' is allowed");
                                return;
                            }
                        }
                        break;


                    case "Class":
                        if ($("#txt" + UpdateField + ID).val().length != "1" && $("#txt" + UpdateField + ID).val().toUpperCase() != "ANY") {
                            alert("Class only one charector or 'ANY' is allowed");
                            $("#txt" + UpdateField + ID).focus();
                        }
                        break;

                    case "AirlineConfirmationCode":
                        $("#txt" + UpdateField + ID).val();
                        break;
                    case "TripType":
                        if ($("#txt" + UpdateField + ID).val() > "1") {
                            alert("TripType only one charector only. 0 for Outbound  1 for Inbound ");
                            $("#txt" + UpdateField + ID).focus();
                            $("#txt" + UpdateField + ID).val('');
                            return;
                        }
                        break;

                    case "FromDate":
                        _val = $("#txt" + UpdateField + ID).val() + " " + $("#txtFromTime" + ID).val();
                        if (_val == OldValue)
                            return;
                        break;

                    case "ToDate":
                        _val = $("#txt" + UpdateField + ID).val() + " " + $("#txtToTime" + ID).val();
                        if (_val == OldValue)
                            return;
                        break;

                    case "CabinClass":
                        _val = $("#ddlCabin" + ID).val();
                        if (_val == OldValue)
                            return;
                        break;

                    case "BaggageAllownce":
                        _val = $("#txtBaggageAllownce" + ID).val();
                        if (_val == OldValue)
                            return;
                        break;




                    case "FromTime":
                        if ($("#txt" + UpdateField + ID).val().split(':') == -1) {
                            $("#txt" + UpdateField + ID).focus();
                            alert("Enter Date only dd/mm/yyyy format");
                            return;
                        }
                        else {
                            _val = $("#txtFromDate" + ID).val() + " " + $("#txt" + UpdateField + ID).val();
                            if (_val == OldValue) {
                                return;
                            }
                        }
                        break;

                    case "ToTime":
                        if ($("#txt" + UpdateField + ID).val().split(':') == -1) {
                            $("#txt" + UpdateField + ID).focus();
                            alert("Enter Date only dd/mm/yyyy format");
                            return;
                        }
                        else {

                            _val = $("#txtToDate" + ID).val() + " " + $("#txt" + UpdateField + ID).val();
                            if (_val == OldValue)
                                return;
                        }
                        break;
                }

                var Param =
                {
                    BookingID: $("#<%=hfBookingID.ClientID%>").val(),
                    ProdID: $("#<%=hfProdID.ClientID%>").val(),
                    ID: ID,
                    UpdateField: UpdateField,
                    Value: _val,
                    UpdatedBy: $("#<%= hfUpdatedBy.ClientID%>").val()
                }


                $.ajax({
                    type: "POST",
                    url: "amendbooking.aspx/UpdateSector",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify(Param),
                    responseType: "json",
                    success: function (data) {
                        if (data.d != "true") {
                            alert("Sector details is not successfully Updeted in database!!");
                        }
                    },
                    error: function (data) {
                    }
                });
            }
        }

        function ConfirmDelete() {
            var x = confirm("Are you sure you want to delete all?");
            if (x)
                return true;
            else
                return false;
        }



        function UpdatePax(SrNo)
        {
            var rowID = $(SrNo).attr("title");
            var EditPaxDetails =
            {
                BookingID: $("#<%=hfBookingID.ClientID%>").val(),
                ProdID: $("#<%=hfProdID.ClientID%>").val(),
                PaxID : rowID,
                PaxType: $("#txtPaxType" + rowID).val(),
                PaxTitle: $("#txtTitle" + rowID).val(),
                PaxFirstName : $("#txtFirstName" + rowID).val(),
                PaxMiddleName: $("#txtMidName" + rowID).val(),
                PaxLastName : $("#txtLastName" + rowID).val(),
                PaxTickets : $("#txtTickets" + rowID).val(),
                PaxDOB : $("#txtDOB" + rowID).val(),
                UpdatedBy: $("#<%=hfUpdatedBy.ClientID%>").val(),
        };
        waitingDialog.show('Please Wait...');
        $.ajax({
            type: "POST",
            url: "amendBooking.aspx/Update_Pax_Details",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify(EditPaxDetails),
            responseType: "json",
            success: function (data)
            {
                if (data.d == "true")
                {
                    message.add("Pax details updated successfully", "success");
                }
                else
                {
                    message.setting("showTime", "8000");
                    message.add("Pax details updated failed", "warning");
                }
            },
            error: function (data) { message.setting("showTime", "8000"); message.add("Pax details updated failed", "error"); }
        });
        waitingDialog.hide();

        }






        function oblurTransactionUpdate(ID, UpdateField, OldValue) {
            var isUpdate = false;

            try {

                if ($("#txt" + UpdateField + ID).val().toUpperCase() != OldValue.toUpperCase()) {
                    isUpdate = true;
                }
                else {
                    isUpdate = false;
                }
               // alert(isUpdate);
               // alert($("#txt" + UpdateField + ID).val().toUpperCase());
               // alert(OldValue.toUpperCase());


            }
            catch (error) {
                isUpdate = false;
            }
            if (isUpdate) {

                switch (UpdateField) {
                    case "TrnsAmount":
                        if ($("#txt" + UpdateField + ID).val().length > 15) {
                            $("#txt" + UpdateField + ID).focus();
                            alert("Transaction amount should not exceed to 10 character");
                            return;
                        }
                        break;

                }
                var Param = {
                    BookingID: $("#<%=hfBookingID.ClientID%>").val(),
                    TrnID: ID,
                    UpdateField: UpdateField,
                    Value: $("#txt" + UpdateField + ID).val(),
                    UpdatedBy: $("#<%= hfUpdatedBy.ClientID%>").val(),
                    Role: $("#<%= hidRole.ClientID%>").val()
                }
                $.ajax({
                    type: "POST",
                    url: "amendbooking.aspx/UpdateTransaction",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify(Param),
                    responseType: "json",
                    success: function (data) {
                        if (data.d != "true") {
                            alert("Transaction amount not updated");
                        }
                    },
                    error: function (data) { }
                });
            }
        }

        function isValidDate(dtStr) {
            var daysInMonth = DaysArray(12);
            var aa = dtStr.split('/');
            if (dtStr.length == 10 && aa.length == 3) {

                var strDay = aa[0];
                var strMonth = aa[1];
                var strYear = aa[2];

                if (strDay.charAt(0) == "0" && strDay.length > 1) strDay = strDay.substring(1);
                if (strMonth.charAt(0) == "0" && strMonth.length > 1) strMonth = strMonth.substring(1);
                for (var i = 1; i <= 3; i++) {
                    if (strYear.charAt(0) == "0" && strYear.length > 1) strYear = strYear.substring(1);
                }
                var month = parseInt(strMonth);
                var day = parseInt(strDay);
                var year = parseInt(strYear);

                if (strMonth.length < 1 || month < 1 || month > 12) {
                    return false;
                }
                if (strDay.length < 1 || day < 1 || day > 31 || (month == 2 && day > daysInFebruary(year)) || day > daysInMonth[month]) {
                    return false;
                }
                if (strYear.length != 4 || year == 0 || year < 1900 || year > 2100) {
                    return false;
                }
                return true;
            }
            else {
                return false;
            }
        }
        function DaysArray(n) {
            for (var i = 1; i <= n; i++) {
                this[i] = 31;
                if (i == 4 || i == 6 || i == 9 || i == 11) { this[i] = 30; }
                if (i == 2) { this[i] = 29; }
            }
            return this;
        }
        function daysInFebruary(year) {
            return (((year % 4 == 0) && ((!(year % 100 == 0)) || (year % 400 == 0))) ? 29 : 28);
        }

    </script>


    <script type="text/javascript">
        function validatePax() {

            if (document.getElementById('<%=txtTitle.ClientID %>').value == "") {
                alert('Enter Pax Title.');
                document.getElementById('<%=txtTitle.ClientID %>').focus();
                return false;
            }
            if (document.getElementById('<%=txtFName.ClientID %>').value == "") {
                alert('Enter Pax First Name.');
                document.getElementById('<%=txtFName.ClientID %>').focus();
                return false;
            }
            if (document.getElementById('<%=txtLName.ClientID %>').value == "") {
                alert('Enter Pax Last Name.');
                document.getElementById('<%=txtLName.ClientID %>').focus();
                return false;

            }

        }



        function validateSectors() {
            if (document.getElementById('<%=txtFrom.ClientID %>').value == "") {
                alert('Enter Source');
                document.getElementById('<%=txtFrom.ClientID %>').focus();
                return false;

            }
            if (document.getElementById('<%=txtTo.ClientID %>').value == "") {
                alert('Enter Destination');
                document.getElementById('<%=txtTo.ClientID %>').focus();
                return false;

            }
            if (document.getElementById('<%=txtAirline.ClientID %>').value == "") {
                alert('Enter Airline');
                document.getElementById('<%=txtAirline.ClientID %>').focus();
                return false;

            }
            if (document.getElementById('<%=txtFromDate.ClientID %>').value == "") {
                alert('Enter Dep Date');
                document.getElementById('<%=txtFromDate.ClientID %>').focus();
                return false;

            }
            if (document.getElementById('<%=txtFromTime.ClientID %>').value == "") {
                alert('Enter Dep Time');
                document.getElementById('<%=txtFromTime.ClientID %>').focus();
                return false;

            }
            if (document.getElementById('<%=txtClass.ClientID %>').value == "") {
                alert('Enter Cabin Class');
                document.getElementById('<%=txtClass.ClientID %>').focus();
                return false;

            }
        }

        $(document).ready(function () {
            $('.ddlCabinClass').each(function () {
                var currentElement = $(this);
                for (var i = 0; i < currentElement.length; i++) {
                    $("#ddlCabin" + currentElement[i].id).val(currentElement[i].value);
                }
            });
        });


    </script>




    <asp:HiddenField ID="hidRole" runat="server" />
   

    <div id="myModal" class="modal fade" role="dialog">
        <div class="modal-dialog" style="margin: 150px auto; position: absolute; left: 13%; width: 74%;">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">

                    <h4 class="modal-title">Remarks...</h4>
                </div>
                <div class="modal-body" style="max-height: 500px; min-height: 200px; overflow-y: scroll;">
                    <table width="100%" cellpadding="0" cellspacing="0" class="table">
                        <tbody>
                            <%=bookingRemarks %>
                        </tbody>
                    </table>


                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>

        </div>
    </div>

    <script>

             $(document).ready(function () {


                 //if (UserInfo.toUpperCase() == ("LOKESH")) {
                 (function () {
                     window.__insp = window.__insp || [];
                     __insp.push(['wid', 608345626]);
                     var ldinsp = function () {
                         if (typeof window.__inspld != "undefined") return; window.__inspld = 1; var insp = document.createElement('script'); insp.type = 'text/javascript'; insp.async = true; insp.id = "inspsync"; insp.src = ('https:' == document.location.protocol ? 'https' : 'http') + '://cdn.inspectlet.com/inspectlet.js?wid=608345626&r=' + Math.floor(new Date().getTime() / 3600000); var x = document.getElementsByTagName('script')[0]; x.parentNode.insertBefore(insp, x);
                     };
                     setTimeout(ldinsp, 0);
                 })();
                 //}
             });
    </script>
</asp:Content>

