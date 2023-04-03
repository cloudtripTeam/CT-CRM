<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="MyProfile.aspx.cs" Inherits="Admin_MyProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container">
        <div style="height: 15px;"></div>
        <div class="panel panel-default">
            <div class="panel-heading">User Details</div>
            <div class="panel-body" style="line-height: 34px;">
                <div class="row">
                    <div class="col-md-3">
                        <label>
                            User ID</label>
                        <asp:Label ID="txtUserID" CssClass="form-control" runat="server" TabIndex="1"></asp:Label>

                    </div>
                    <div class="col-md-3">
                        <label>
                            User Password
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Invalid Password"
                                ValidationGroup="RegComp" ControlToValidate="txtUserPassword">*</asp:RequiredFieldValidator></label>
                        <asp:TextBox ID="txtUserPassword" CssClass="form-control" runat="server" TabIndex="2" MinLength="5" MaxLength="30"></asp:TextBox>

                    </div>
                    <div class="col-md-3">
                        <label>
                            Email ID<asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="Insert Email ID"
                                ValidationGroup="RegComp" ControlToValidate="txtEmail">*</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalide Email Id"
                                ValidationGroup="RegComp" ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                        </label>
                        <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server" TabIndex="7"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <label>
                            Mobile No<asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="Invalid Mobile No"
                                ValidationGroup="RegComp" ControlToValidate="txtMobile">*</asp:RequiredFieldValidator></label>
                        <asp:TextBox ID="txtMobile" CssClass="form-control" runat="server" onkeypress="return isNumberKey(event)"
                            TabIndex="8" MaxLength="15"></asp:TextBox>


                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label>Title</label>
                        <asp:DropDownList ID="ddlTitle" CssClass="form-control" TabIndex="4" runat="server">
                            <asp:ListItem Value="MR" Text="MR" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="MIS" Text="MIS"></asp:ListItem>
                            <asp:ListItem Value="MISS" Text="MISS"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <label>
                            First Name  
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Invalid First Name"
                                ValidationGroup="RegComp" ControlToValidate="txtFName" SetFocusOnError="true">*</asp:RequiredFieldValidator></label>
                        <asp:TextBox ID="txtFName" CssClass="form-control" runat="server" TabIndex="5"></asp:TextBox>


                    </div>
                    <div class="col-md-3">
                        <label>Last Name</label>
                        <asp:TextBox ID="txtLName" CssClass="form-control" runat="server" TabIndex="6"></asp:TextBox>
                    </div>


                    <div class="col-md-3">
                        <label>
                            User Roll<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" SetFocusOnError="true"
                                ErrorMessage="Invalide User Role" ControlToValidate="ddlUserRole" ValidationGroup="RegComp">*</asp:RequiredFieldValidator></label>
                        <asp:DropDownList ID="ddlUserRole" CssClass="form-control" TabIndex="17" Width="258px" runat="server">
                           <%-- <asp:ListItem></asp:ListItem>
                            <asp:ListItem Value="SuperAdmin">SuperAdmin</asp:ListItem>
                            <asp:ListItem Value="Admin">ADMIN</asp:ListItem>
                            <asp:ListItem Value="Operator">Operator</asp:ListItem>--%>
                        </asp:DropDownList>

                    </div>
                </div>
                <div class="row">
                    
                    <div class="col-md-6">
                        <label>Address</label>
                        <asp:TextBox ID="txtAddress1" CssClass="form-control" runat="server" Height="65px" TextMode="MultiLine" TabIndex="11"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" SetFocusOnError="true"
                            ErrorMessage="Invalid Address 1" ControlToValidate="txtAddress1" ValidationGroup="RegComp">*</asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-9">
                        <asp:Label ID="lblMsg" runat="server" ForeColor="Red" EnableViewState="false"></asp:Label>
                    </div>
                    <div class="col-md-3">

                       
                        <asp:Button ID="btnUpdate" runat="server" class="btn btn-primary btn-lg" Text="Update" OnClientClick="return chkValidation();" OnClick="btnbtnUpdate_Click" />
                       
                        <asp:Button ID="btnCancel" runat="server" class="btn btn-primary btn-lg" Text="Cancel" OnClick="btnCancel_Click" />
                        <asp:ValidationSummary ID="vsCompReg" DisplayMode="BulletList" ShowSummary="false"
                            ShowMessageBox="true" ValidationGroup="RegComp" runat="server" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

