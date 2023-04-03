<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="CompanyCardUsage.aspx.cs" Inherits="Admin_CompanyCardUsage" %>

<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.6/jquery.min.js" type="text/javascript"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css"
        rel="Stylesheet" type="text/css" />
    <script>
        $(document).ready(function () {
            $('#<%=txtTravelDate.ClientID %>').datepicker({
                maxDate: "+365D",
                numberOfMonths: 2,
                dateFormat: "mm/dd/yy"
            });
            $('#<%=txtTranslDate.ClientID %>').datepicker({
                maxDate: "+365D",
                numberOfMonths: 2,
                dateFormat: "mm/dd/yy"
            });
            $('#<%=txtTranslDateTo.ClientID %>').datepicker({
                maxDate: "+365D",
                numberOfMonths: 2,
                dateFormat: "mm/dd/yy"
            });

        });
    </script>
    <div class="panel panel-default">
        <div class="panel-heading">Compnay Card Uses</div>
        <div class="panel-body">
            <div class="col-med-12">
                
                <div class="col-md-2">
                    <div class="form-group">
                        <label>Card No(Last 4 Digit)</label>
                        <input type="text" class="form-control" maxlength="4" runat="server" id="txt4Digit" />
                    </div>
                </div>
                <div class="col-md-4">
                   

                       <div class="form-group">
                        <label>Merchant Reference</label>
                        <input type="text" class="form-control" runat="server" id="txtMerchantRef" />
                    </div>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <label>Merchant</label>
                        <input type="text" class="form-control" runat="server" id="txtMerchant" />
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="col-md-4">
                        <div class="form-group">
                            <label>Currency</label>
                            <select class="form-control" runat="server" id="ddlCurrency" runat="server">
                                <option value="">SELECT</option>
                                <option value="GBP">GBP</option>
                                <option value="USD">USD</option>
                                <option value="INR">INR</option>
                                <option value="CAD">CAD</option>
                            </select>
                        </div>
                    </div>
                    <div class="col-md-8">
                        <div class="form-group">
                            <label>Amount</label>
                            <input type="text" class="form-control" runat="server" id="txtAmount" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-med-12">
                <div class="col-md-2">
                    <div class="form-group">
                        <label>Travel Date</label>
                        <input type="text" class="form-control" runat="server" id="txtTravelDate" />
                    </div>
                </div>
                <div class="col-md-2">
                  <div class="form-group">
                        <label>Pax Name</label>
                        <input type="text" class="form-control" runat="server" id="txtPaxName" />
                    </div>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <label>Our Reference(If Any)</label>
                        <input type="text" class="form-control" runat="server" id="txtOurRef" />
                    </div>
                </div>
                
                <div class="col-md-6">
                    <div class="form-group">
                        <label>Comment</label>
                        <textarea rows="1" class="form-control" runat="server" id="txtNotes" />
                    </div>
                </div>
                <div class="col-md-12 text-center">
                    <div class="col-md-3">
                         <div class="form-group">
                        <label>Date From :</label>
                             <input type="text" class="form-control" runat="server" id="txtTranslDate" />
                             </div>
                    </div>
                     <div class="col-md-3">
                         <div class="form-group">
                        <label>Date To :</label>
                             <input type="text" class="form-control" runat="server" id="txtTranslDateTo" />
                             </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <br />
                    <asp:Button ID="btnSubmit" CssClass="btn btn-default" Width="150px" OnClientClick="return validate()" runat="server" Text="Submit" OnClick="btnSubmit_Click" />&nbsp;&nbsp;&nbsp;<asp:Button ID="btnSearch" CssClass="btn btn-default" Width="150px" runat="server" Text="Search" OnClick="btnSearch_Click" />
                    <asp:Literal ID="ltrMsg" runat="server"></asp:Literal>

                        </div>
                        </div>
                </div>
            </div>
            <div class="col-med-12">

                <asp:Repeater ID="rptCCU" runat="server">
                    <HeaderTemplate>

                        <table class="table" style="width: 100%;">
                            <thead>
                                <tr>
                                    <th>Trans Date</th>
                                    <th>Travel Date</th>
                                    <th>Pax Name</th>
                                    <th>Merchant</th>
                                    <th>Currency</th>
                                    <th>Amount</th>
                                    <th>Card No</th>
                                    <th>Merchant Ref</th>
                                    <th>Our Ref</th>
                                    <th>User Name</th>
                                    <th>Remarks</th>
                                </tr>
                            </thead>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tbody>
                            <tr>
                                <td><%# Eval("CCU_TransactionDate") %></td>
                                <td><%# Eval("CCU_TravelDate") %></td>
                                <td><%# Eval("CCU_PaxName") %></td>
                                <td><%# Eval("CCU_Merchant") %></td>
                                <td><%# Eval("CCU_Currency") %></td>
                                <td><%# Eval("CCU_Amount") %></td>
                                <td><%# Eval("CCU_LastFour") %></td>
                                <td><%# Eval("CCU_MerchantRef") %></td>
                                <td><%# Eval("CCU_OurRef") %></td>
                                <td><%# Eval("CCU_UserName") %></td>
                                <td><%# Eval("CCU_Notes") %></td>
                            </tr>
                        </tbody>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                        
                    </FooterTemplate>
                </asp:Repeater>


            </div>
        </div>
        <div>
        </div>
    </div>
    <script>
        function validate()
        {
            var cardNo = document.getElementById('<%= txt4Digit.ClientID %>').value;
            if (cardNo == "")
            {
                alert("Must  enter last 4 digit on card");
                document.getElementById('<%=txt4Digit.ClientID %>').focus();
                return false;
            }
            var merref = document.getElementById('<%= txtMerchantRef.ClientID %>').value;
            if (merref == "") {
                alert("Must  enter merchant ref");
                document.getElementById('<%=txtMerchantRef.ClientID %>').focus();
                return false;
            }
            var mer = document.getElementById('<%= txtMerchant.ClientID %>').value;
            if (mer == "") {
                alert("Must  enter merchant name");
                document.getElementById('<%=txtMerchant.ClientID %>').focus();
                return false;
            }
            var amt = document.getElementById('<%= txtAmount.ClientID %>').value;
            if (amt == "") {
                alert("Must  enter amount");
                document.getElementById('<%=txtAmount.ClientID %>').focus();
                return false;
            }

        }
    </script>
</asp:Content>

