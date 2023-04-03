<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="Send_Eticket.aspx.cs" Inherits="Admin_Send_Eticket" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="panel-group">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="pull-left">
                    <h4 class="panel-title">Send E-Ticket to <%=hidBookingID.Value %>
                    </h4>
                </div>

                <div class="clearfix"></div>
            </div>

            <div id="bookingdetails" class="panel-collapse">
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-12">

                            <div id="tab-1" class="tab-content current">

                                <div class="clearfix"></div>
                                <div class="row" style="background-color: white;" id="dvXP" visible="false" runat="server">

                                    <div class="col-md-12 pv-20">

                                        <div class="col-md-9 col-sm-9" id="etblticket"   width="600">

                                           

                                            <%=terms %>

                                        </div>


                                        <div class="col-md-3 col-sm-3 p-0">

                                            <ul class="" data-spy="affix" data-offset-top="205">
                                                <%--<li>
                                                        <div class="col-md-12">

                                                            <div class="col-md-12">
                                                                <label>Note : </label>
                                                                <asp:TextBox ID="txtNote" CssClass="form-control" TextMode="MultiLine" Rows="10" runat="server"></asp:TextBox>
                                                            </div>

                                                        </div>
                                                    </li>--%>
                                                <li>
                                                    <div class="col-md-12">
                                                        <div class="clearfix mt-20"></div>

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
                                                            <label>Contact Number:</label>
                                                            <asp:TextBox ID="txtMobile" CssClass="form-control mb-10" ValidationGroup="xp" runat="server"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtMobile" ForeColor="Red" ValidationGroup="xp" runat="server" ErrorMessage="invalid mobile number" Display="None"></asp:RequiredFieldValidator>
                                                        </div>

                                                        <div class="col-md-12">

                                                            <asp:FileUpload ID="fileAttached" runat="server" />
                                                            <asp:CustomValidator ID="CustomValidator1" runat="server" ValidationGroup="xp"   ClientValidationFunction="ValidateFileUpload" ErrorMessage="Please attache eticket"></asp:CustomValidator>
                                                            <div class="clearfix mt-20"></div>
                                                        </div>

                                                        <div class="col-md-12">

                                                            <asp:CheckBox ID="ckboxEticket" Checked="true" runat="server" Text="E-Ticket" /><asp:CheckBox ID="ckboxSMS" Checked="false" runat="server" Text="SMS" /><br/><asp:CheckBox Enabled="false" ID="ckboxFeedback" runat="server" Text="FeedBack" /><asp:CheckBox Enabled="false" ID="ckboxVoucher" runat="server" Text="Voucher" />
                                                            <div class="clearfix mt-20"></div>
                                                        </div>

                                                        <div class="col-md-12">

                                                            <asp:ValidationSummary ID="ValidationSummary1" ShowSummary="false" ShowMessageBox="true" ValidationGroup="xp" runat="server" />
                                                            <asp:Button ID="btnSend" runat="server" Text="Send" ValidationGroup="xp" CssClass="btn btn-default btn-block" OnClick="btnSend_Click" />
                                                        </div>


                                                        <asp:Label ID="ltrMsg" ForeColor="Red" runat="server"></asp:Label>
                                                        <div class="clearfix mb-30"></div>
                                                    </div>
                                                </li>
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


                        </div>
                    </div>

                    <div class="clearfix"></div>

                    <div class="row">
                    </div>

                </div>
            </div>
        </div>
        <asp:HiddenField ID="hfCompany" runat="server" />
        <input type="hidden" id ="divHidden" enableviewstate="true"  runat="server" />
    </div>

    <style>
        .affix {
            top: 20px;
            width: 265px;
        }

        .textss {
            overflow: hidden;
            display: -webkit-box;
            -webkit-line-clamp: 3; /* number of lines to show */
            -webkit-box-orient: vertical;
        }

        ul.tabs {
            margin: 0px;
            padding: 0px;
            list-style: none;
        }

            ul.tabs li {
                background: none;
                color: #222;
                display: inline-block;
                padding: 10px 15px;
                cursor: pointer;
            }

                ul.tabs li.current {
                    background: #1e4b86;
                    color: #fff;
                    border: solid;
                    border-width: thin;
                }

        .tab-content {
            display: none;
            background: #1e4b86;
            padding: 15px;
        }

            .tab-content.current {
                display: inherit;
            }
    </style>

    <asp:HiddenField ID="hidBookingID" runat="server" />
  <script type="text/javascript">
      function ValidateFileUpload(Source, args)
      {
          var fuData = document.getElementById('<%= fileAttached.ClientID %>'); 
          var FileUploadPath = fuData.value;
 
          if (FileUploadPath == '') {
              args.IsValid = false;
          }
          else { args.IsValid = true; }
      }
      </script>
</asp:Content>

