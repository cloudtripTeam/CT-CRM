<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="fareUpload.aspx.cs" Inherits="Admin_fareUpload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
       <script type="text/javascript">
           function checkAll(gvFareSheet, colIndex) {
               var GridView = gvFareSheet.parentNode.parentNode.parentNode;
               for (var i = 1; i < GridView.rows.length; i++) {
                   var chb = GridView.rows[i].cells[colIndex].getElementsByTagName("input")[0];
                   chb.checked = gvFareSheet.checked;
               }
           }

           function checkItem_All(objRef, colIndex) {
               var GridView = objRef.parentNode.parentNode.parentNode;
               var selectAll = GridView.rows[0].cells[colIndex].getElementsByTagName("input")[0];
               if (!objRef.checked) {
                   selectAll.checked = false;
               }
               else {
                   var checked = true;
                   for (var i = 1; i < GridView.rows.length; i++) {
                       var chb = GridView.rows[i].cells[colIndex].getElementsByTagName("input")[0];
                       if (!chb.checked) {
                           checked = false;
                           break;
                       }
                   }
                   selectAll.checked = checked;
               }
           }

           function DeleteConfirmation() {
               if (confirm("Are you sure delete ?") == true) return true;
               else
                   return false;
           }
           function ValidateC() {
          <%--  var CompanyID = document.getElementById('<%= ddlCompany.ClientID %>').value;
            if (CompanyID == 'Select') {
                alert("Select Company !!!");
                return false;
                document.getElementById("<%= ddlCompany.ClientID %>").focus();
            }--%>
           }
    </script>
    <style>
        td, th {
    padding: 8px 5px;
    text-align:center;
}
        tr{
            line-height: 15px;
        }
        tr:nth-child(odd) {background-color: #f2f2f2;}
        .tdtxtbx
        {
            padding:3px 5px;
            text-align: center;
        }
    </style>
    <div class="container">
        <div style="height: 15px;"></div>
        <div class="panel panel-default">
            <div class="panel-heading">Fare Upload</div>
            <div class="panel-body" style="line-height: 34px;">
                <div class="table-responsive">
                    <table class="table">
                        <thead>
                            <tr>
                               
                                <th class="text-left">Company</th>
                                <th class="text-left">Operator</th>
                            
                                <th class="text-left">Departure City</th>
                                <th class="text-left">Return City</th>
                                <th></th>

                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                               
                                <td>
                                    <asp:DropDownList ID="ddlCompany" class="form-control" runat="server" Width="100%">
                                        <asp:ListItem Selected="True" Value="Select">Select</asp:ListItem>
                                        <asp:ListItem Value="DIAL4TRV">Dial4Travel UK</asp:ListItem>
                                        <asp:ListItem Value="FLTTROTT">Flight Trotters UK</asp:ListItem>
                                        <asp:ListItem Value="TRVJUNCTION">Travel Junction</asp:ListItem>
                                        <asp:ListItem Value="USA">USA Travel</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddloperator" class="form-control" EnableViewState="true" runat="server" Width="100%">
                                        <asp:ListItem Value="" >All</asp:ListItem>
                                    </asp:DropDownList></td>
                           
                                <td>
                                    <asp:TextBox ID="txtFrom" class="form-control" runat="server" MaxLength="3" Width="100%"></asp:TextBox></td>
                                <td>
                                    <asp:TextBox ID="txtTo" class="form-control" runat="server" MaxLength="3" Width="100%"></asp:TextBox></td>
                                <td>
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="btn btn-primary btn-lg" /></td>

                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>

    <div class="container">
        <div style="height: 15px;"></div>
        <div class="panel panel-default">
            <div class="panel-heading">Fare Details</div>
            <div class="panel-body" style="line-height: 34px; padding: 0px!important;">
                <div class="row">
                    <asp:GridView ID="gvFareSheet" BorderColor="black" CssClass="Gridview" HeaderStyle-BackColor="#61A6F8" runat="server" AutoGenerateColumns="False" Width="100%" GridLines="None" HeaderStyle-HorizontalAlign="Center" ForeColor="black" HeaderStyle-ForeColor="White" RowStyle-HorizontalAlign="Center" EditRowStyle-HorizontalAlign="Center">
                        <Columns>
                           
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chkBxHeader" Text=" All " TextAlign="Left" runat="server" onclick="checkAll(this,0);" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSelect" runat="server" onclick="checkItem_All(this,0)" />
                                </ItemTemplate>

                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Air">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFlightID" runat="server"   Text='<%# Eval("FrID") %>' CssClass="lbl_item_data"></asp:Label>
                                       
                                    </ItemTemplate>
                                   
                                </asp:TemplateField>

                             <asp:TemplateField HeaderText="AirLine">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtO" Width="50px" runat="server" Text='<%# Eval("AirIATA") %>' CssClass="tdtxtbx" ></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                             <asp:TemplateField HeaderText="Dep Code">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtFrom" Width="50px" runat="server" Text='<%# Eval("FrIATA") %>' CssClass="tdtxtbx" ></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                             <asp:TemplateField HeaderText="Arr Code">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtTo" Width="50px" runat="server" Text='<%# Eval("ToIATA") %>' CssClass="tdtxtbx" ></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            <asp:TemplateField HeaderText="Fare">
                                    <ItemTemplate>
                                        <asp:TextBox ID="lblAdultTotal" Width="60px" runat="server" Text='<%# Eval("Price") %>' CssClass="tdtxtbx" ></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                               <asp:TemplateField HeaderText="Valid From">
                                    <ItemTemplate>
                                        <asp:TextBox ID="lblValidFrom" Width="100px" runat="server" Text='<%#Eval("VFrom","{0:d-MM-yyyy}")%>' CssClass="tdtxtbx" ></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                              <asp:TemplateField HeaderText="Valid Till">
                                    <ItemTemplate>
                                        <asp:TextBox ID="lblValidTo" Width="100px" runat="server" Text='<%#Eval("VTill","{0:d-MM-yyyy}")%>' CssClass="tdtxtbx" ></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                             <asp:TemplateField HeaderText="Journey">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtJType" Width="40px" runat="server" Text='<%# Eval("JType") %>' CssClass="tdtxtbx" ></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                             <asp:TemplateField HeaderText="Arr Day">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtArrDay" Width="40px" runat="server" Text='<%# Eval("ArrDay") %>' CssClass="tdtxtbx" ></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                              <asp:TemplateField HeaderText="Reco">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkIsOffer" Checked='<%#Eval("IsReco")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                           <%-- <asp:TemplateField>
                                <ItemTemplate>
                                    <div class="table-responsive">
                                        <table class="table" style="margin-bottom: -5px;">
                                            <tr>
                                               <%-- <td width="5px">
                                                    <asp:Label ID="lblFlightID" runat="server" Visible="false" Text='<%#Eval("FrID")%>'></asp:Label>
                                                </td>
                                                <td align="center">
                                                    <asp:TextBox ID="txtO" Width="40px" runat="server" Text='<%#Eval("AirIATA")%>'></asp:TextBox>

                                                </td>
                                                <td align="center">
                                                    <asp:TextBox ID="txtFrom" MaxLength="3" Width="40px" runat="server" Text='<%#Eval("FrIATA")%>'></asp:TextBox>

                                                </td>
                                                <td align="center">
                                                    <asp:TextBox ID="txtTo" MaxLength="3" Width="40px" runat="server" Text='<%#Eval("ToIATA")%>'></asp:TextBox>

                                                </td>--%>
                                        <%--        <td>
                                                    <asp:TextBox ID="lblAdultTotal" runat="server" Text='<%#Eval("Price")%>' Width="60px"></asp:TextBox>
                                                </td>

                                                <td>
                                                    <asp:TextBox ID="lblValidFrom" runat="server" Width="80px" Text='<%#Eval("VFrom","{0:d-MM-yyyy}")%>'></asp:TextBox>

                                                </td>
                                                <td>
                                                    <asp:TextBox ID="lblValidTo" runat="server" Width="80px" Text='<%#Eval("VTill","{0:d-MM-yyyy}")%>'></asp:TextBox>

                                                </td>--%>
                                             <%--   <td>
                                                    <asp:TextBox ID="txtJType" runat="server" Width="30px" Text='<%#Eval("JType")%>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtArrDay" runat="server" Width="30px" Text='<%#Eval("ArrDay")%>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chkIsOffer" Checked='<%#Eval("IsReco")%>' runat="server" />
                                                </td>

                                            </tr>
                                        </table>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>--%>

                        </Columns>


                    </asp:GridView>
                    <br />
                    <div  style="padding:15px 0 15px 15px;border-top: 1px solid #333;">
                        <asp:Label ID="lblMsg" runat="server" Visible="true"></asp:Label>
                        <asp:Button ID="btnUpdateFare" runat="server" Visible="false" Text="Update" OnClick="btnUpdateFare_Click" CssClass="btn btn-info" />
                        <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" Visible="false" Text="Delete Record" CssClass="btn btn-danger" OnClientClick="return DeleteConfirmation();" />
                        <asp:Button ID="btnADD" runat="server" Text="Add More Record" CssClass="btn btn-info" OnClick="btnADD_Click" OnClientClick="return ValidateC();" />
                    </div>


                </div>
            </div>
        </div>
    </div>
</asp:Content>

