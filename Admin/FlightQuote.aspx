<%@ Page Title="" ValidateRequest="false" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="FlightQuote.aspx.cs" Inherits="Admin_FlightQuote" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <div class="panel-group">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="pull-left">
                    <h4 class="panel-title">
                        <a data-toggle="collapse" href="#bookingdetails">Quotation Details</a>
                    </h4>
                </div>
                <div class="pull-right"><a data-toggle="collapse" href="#bookingdetails"><span class="glyphicon glyphicon-search"></span></a></div>
                <div class="clearfix"></div>
            </div>

            <div id="bookingdetails" class="panel-collapse">
                <div class="panel-body">                    
                      <div class="row">
                        <div class="col-md-12">
                            <div class="container">

                                <ul class="tabs">
                                    <li class="tab-link current" data-tab="tab-1">Flight By XP</li>
                                    <li class="tab-link" data-tab="tab-2">No XP</li>
                                </ul>

                                <div id="tab-1" class="tab-content current">
                                    <div class="col-md-12">
                                        <div class="col-md-6">
                                            <asp:TextBox ID="txtInvoice" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-danger btn-lg" OnClick="btnSearch_Click" />
                                        </div>
                                    </div>
                                     <div class="clearfix"></div>
                                    <div class="row" style="background-color: white;" id="dvXP" visible="false" runat="server">

                                        <div class="col-md-12 pv-20">
                                            <div class="col-md-9 col-sm-9"> <%=XPDetails %></div>
                                            <div class="col-md-3 col-sm-3 p-0"> 
                                                
                                                <ul class="" data-spy="affix" data-offset-top="205">
                                                <div class="col-md-12">
                                                    <asp:CheckBox Text="Include I Authorize" runat="server" ID="IncludeAuth" style="display:none"  />
                                            
                                            <div class="col-md-12">
                                                <label>Note : </label><asp:TextBox ID="txtNote" CssClass="form-control" TextMode="MultiLine" Rows="10" runat="server"></asp:TextBox>
                                            </div>
                                            
                                        </div>
                                        <div class="col-md-12">
                                            <div class="clearfix mt-20"></div>
                                            
                                            
                                            <div class="col-md-12">
                                                <label>Amount:</label>
                                                <asp:TextBox ID="txtPrice" CssClass="form-control mb-10" ValidationGroup="xp" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtPrice" ForeColor="Red" ValidationGroup="xp" runat="server" ErrorMessage="Enter Total Price." Display="None"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-md-12">
                                                <label>From Email ID:</label>
                                               <asp:TextBox ID="txtXPFrom" CssClass="form-control mb-10" ValidationGroup="xp" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtXPFrom" ForeColor="Red" ValidationGroup="xp" runat="server" ErrorMessage="Enter Sender Email ID." Display="None"></asp:RequiredFieldValidator>

                                            </div>
                                            <div class="col-md-12">
                                                <label>To Email ID:</label>
                                                <asp:TextBox ID="txtXPTo" CssClass="form-control mb-10" ValidationGroup="xp" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtXPTo" ForeColor="Red" ValidationGroup="xp" runat="server" ErrorMessage="Enter Receiver Email ID." Display="None"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-md-12">
                                               
                                                <asp:ValidationSummary ID="ValidationSummary1" ShowSummary="false" ShowMessageBox="true" ValidationGroup="xp" runat="server" />
                                                <asp:Button ID="btnSend" runat="server" Text="Send" OnClientClick="return SendQuotation();" ValidationGroup="xp" CssClass="btn btn-default btn-block" OnClick="btnSend_Click" />
                                            </div>
                                            
                                           <div class="col-md-12">
                                               
                                                <asp:ValidationSummary ID="ValidationSummary2" ShowSummary="false" ShowMessageBox="true" ValidationGroup="xp" runat="server" />
                                                <asp:Button ID="btnPdf" runat="server" Text="PDF Download"  ValidationGroup="xp" CssClass="btn btn-default btn-block" OnClick="btnPdf_Click" />
                                            </div>
                                            
                                            <div class="col-md-12"></div>


                                            <asp:Literal ID="ltrMsg" runat="server"></asp:Literal>
                                            <div class="clearfix mb-30"></div>
                                        </div>

                                                </ul>
                                                

                                            </div>
                                           
                                        </div>
                                       

                                    </div>
                                    <br />
                    <div class="clearfix"></div>
                                     <div class="col-md-12"></div>

                                    <div class="clearfix">

                                    </div>
                                </div>
                                <div id="tab-2" class="tab-content">

                                    <asp:TextBox CssClass="textEditor1" ID="txtOverviews" TextMode="MultiLine" runat="server" placeholder="hotel overviews" />
                                    <br />
                                   
                                    <br /><br />
                                   <asp:Button ID="btnSendXP" Visible="false"  runat="server" Text="Send" CssClass="btn btn-default" OnClick="btnSendXP_Click" />
                                   <asp:Button ID="btnDownloadInvoice"  runat="server" Text="Download E-Ticket" CssClass="btn btn-success" OnClick="btnDownloadInvoice_Click" />
                                   <asp:Button ID="btneticket"  runat="server" Text="Send E-Ticket" CssClass="btn btn-success" OnClick="btneticket_Click" />
                                   
                                    
                                </div>



                            </div>
                        </div>
                    </div>

                    <div class="clearfix"></div>
                    
                    <div class="row">
                        
                        
                    </div>

                </div>
            </div>
        </div>
    </div>
     <script lang="javascript" type="text/javascript">
         


         $('.textEditor1').jqte();

         $(document).ready(function () {

             $('ul.tabs li').click(function () {
                 var tab_id = $(this).attr('data-tab');

                 $('ul.tabs li').removeClass('current');
                 $('.tab-content').removeClass('current');

                 $(this).addClass('current');
                 $("#" + tab_id).addClass('current');
             })

         })

         function SendQuotation() {
             waitingDialog.show('Sending Quotation...');
             return true;
         }

       

        

        
         
     </script>
    <style>

        .affix {
            top: 20px;
            width:265px;
        }

        .textss {
            overflow: hidden;
            display: -webkit-box;
            -webkit-line-clamp: 3; /* number of lines to show */
            -webkit-box-orient: vertical;
        }
        ul.tabs{
			margin: 0px;
			padding: 0px;
			list-style: none;
            

		}
		ul.tabs li{
			background: none;
			color: #222;
			display: inline-block;
			padding: 10px 15px;
			cursor: pointer;
		}

		ul.tabs li.current{
			background: #1e4b86;
			color: #fff;
            border:solid;
            border-width:thin;
		}

		.tab-content{
			display: none;
			background: #1e4b86;
			padding: 15px;
		}

		.tab-content.current{
			display: inherit;
		}
    </style>
    
    
</asp:Content>

