<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="Noticeboard.aspx.cs" Inherits="Admin_Noticeboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="container" id="noticeboard" runat="server">
        <div style="height: 15px;"></div>
        <div class="panel panel-default">
            <div class="panel-heading"><h2> Notice Board</h2></div>
            <div class="panel-body" style="line-height: 34px;">
                <div class="row">
                    <div class="col-md-6">
                        <label>
                            Notice
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="Invalid Notice message, it must not be empty"
                                ValidationGroup="RegComp" ControlToValidate="txtNotice">*</asp:RequiredFieldValidator></label>
                        <asp:TextBox ID="txtNotice" CssClass="form-control" TextMode="MultiLine" runat="server" TabIndex="1"></asp:TextBox>

                    </div>
                    <div class="col-md-3">
                        <label>
                            Applicable Date
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Invalid Applicable Date"
                                ValidationGroup="RegComp" ControlToValidate="txtApplicableDate">*</asp:RequiredFieldValidator></label>
                        <asp:TextBox ID="txtApplicableDate" placeholder="dd/MM/yyyy" CssClass="form-control" runat="server" TabIndex="2" MaxLength="10"></asp:TextBox>

                    </div>
                    <div class="col-md-3">
                        <label>
                            Expiry Date<asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="Invalid Expiry Date"
                                ValidationGroup="RegComp" ControlToValidate="txtExpiryDate">*</asp:RequiredFieldValidator>

                        </label>
                        <asp:TextBox ID="txtExpiryDate" CssClass="form-control" placeholder="dd/MM/yyyy" runat="server" TabIndex="3"></asp:TextBox>
                    </div>

                </div>


                <div class="row">
                    <div class="col-md-9">
                        <asp:Label ID="lblMsg" runat="server" ForeColor="Red" EnableViewState="false"></asp:Label>
                    </div>
                    <div class="col-md-3">

                        <asp:Button ID="btnAdd" runat="server" class="btn btn-primary btn-lg" Text="Add" ValidationGroup="RegComp" OnClick="btnAdd_Click" />

                      

                        <asp:ValidationSummary ID="vsCompReg" DisplayMode="BulletList" ShowSummary="false"
                            ShowMessageBox="true" ValidationGroup="RegComp" runat="server" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="container">
        <div style="height: 15px;"></div>
        <div class="panel panel-default">
            <div class="panel-heading">Notice Board Details</div>

          
            <asp:Label  ID="ltrMsg" Font-Bold="true" ForeColor="Red" runat="server"></asp:Label>
                <asp:GridView ID="gdvnoticeboard" Width="100%" CssClass="table table-striped" OnRowDataBound="gdvnoticeboard_RowDataBound" runat="server" AutoGenerateColumns="false"
                    EmptyDataText="Sorry, there are no records found as per your searhing criteria."
                    GridLines="None" OnRowCommand="gdvnoticeboard_RowCommand" OnRowDeleting="gdvnoticeboard_RowDeleting" OnRowEditing="gdvnoticeboard_RowEditing">
                    
                    <Columns>
                        
                        <asp:TemplateField>
                            <HeaderTemplate>
                            
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td style="width: 70%">
                                        <asp:TextBox ID="txtMessages" CssClass="form-control" Width="100%" Text='<%# Eval("NoticeMessage")%>' runat="server"></asp:TextBox>
                                    </td>
                                    <td style="width: 5%"><%# Eval("NoticeBy")%></td>
                                    <td style="width: 3%"><%# Convert.ToDateTime(Eval("Applicable_Date")).ToString("ddMMMyy")%></td>
                                    <td style="width: 3%"><%# Convert.ToDateTime(Eval("Expiry_Date")).ToString("ddMMMyy") %></td>
                                    <td style="width: 5%"><%# Convert.ToDateTime(Eval("Created_Date")).ToString("dd/MM/yy") %></td>
                                    <td style="width: 2%">
                                        <asp:ImageButton ID="btnUpdate" AlternateText="Update" runat="server" CommandName="edit" Width="25px" Height="25px" ImageUrl="~/images/edit.png" /></td>
                                    <td style="width: 2%">
                                        <asp:ImageButton ID="btnDelete" AlternateText="Delete" CommandName="delete" ImageUrl="~/images/delete.png" Width="25px" Height="25px" runat="server" /></td>
                                </tr>


                             



                            </ItemTemplate>
                            <FooterTemplate>
                               
                            </FooterTemplate>
                        </asp:TemplateField>
                    
                    </Columns>
                </asp:GridView>

                
           
        </div>
    </div>
</asp:Content>

