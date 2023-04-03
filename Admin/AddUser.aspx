<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="AddUser.aspx.cs" Inherits="Admin_AddUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container-fluid">
        <div style="height: 15px;"></div>
        <div class="panel panel-default">
            <div class="panel-heading">User Details</div>
            <div class="panel-body" style="line-height: 34px;">
                <div class="row">
                    <div class="col-md-3">
                        <label>
                            User ID
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="Insert User ID"
                                ValidationGroup="RegComp" ControlToValidate="txtUserID">*</asp:RequiredFieldValidator></label>
                        <asp:TextBox ID="txtUserID" CssClass="form-control" runat="server" TabIndex="1"></asp:TextBox>

                    </div>
                    <div class="col-md-3">
                        <label>
                            User Password
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Invalid Password"
                                ValidationGroup="RegComp" ControlToValidate="txtUserPassword">*</asp:RequiredFieldValidator></label>
                        <asp:TextBox ID="txtUserPassword" CssClass="form-control" runat="server" TabIndex="2" MaxLength="30"></asp:TextBox>

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
                    <div class="col-md-3">
                        <label>Check IP when user login</label>
                        <asp:CheckBox ID="chbIsCheckIp" runat="server" CssClass="form-control" Checked="true" Text="IsCheck IP"></asp:CheckBox>
                    </div>
                    <div class="col-md-3">
                        <label>User active check when login</label>
                        <asp:CheckBox ID="chbIsUserActive" runat="server" CssClass="form-control" Checked="true" Text="Is user is Active"></asp:CheckBox>
                    </div>
                    <div class="col-md-6">
                        <label>Address</label>
                        <asp:TextBox ID="txtAddress1" CssClass="form-control" runat="server" Height="34px" TextMode="MultiLine" TabIndex="11"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" SetFocusOnError="true"
                            ErrorMessage="Invalid Address 1" ControlToValidate="txtAddress1" ValidationGroup="RegComp">*</asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-9">
                        <asp:Label ID="lblMsg" runat="server" ForeColor="Red" EnableViewState="false"></asp:Label>
                    </div>
                    <div class="col-md-3">

                        <asp:Button ID="btnAdd" runat="server" class="btn btn-primary btn-lg" Text="Add" ValidationGroup="RegComp" OnClick="btnAdd_Click" />
                        <asp:Button ID="btnUpdate" runat="server" class="btn btn-primary btn-lg" Text="Update" OnClientClick="return chkValidation();" OnClick="btnbtnUpdate_Click" />
                        <asp:Button ID="btnSearch" runat="server" class="btn btn-primary btn-lg" Text="Search" OnClick="btnSearch_Click" />
                        <asp:Button ID="btnCancel" runat="server" class="btn btn-primary btn-lg" Text="Cancel" OnClick="btnCancel_Click" />
                        <asp:ValidationSummary ID="vsCompReg" DisplayMode="BulletList" ShowSummary="false"
                            ShowMessageBox="true" ValidationGroup="RegComp" runat="server" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="container-fluid">
        <div style="height: 15px;"></div>
        <div class="panel panel-default">
            <div class="panel-heading">User Details</div>
            <div class="panel-body" style="line-height: 34px; padding: 0px!important;">
                <asp:GridView ID="gdvUserDetail" CssClass="gdvStyle" runat="server" AutoGenerateColumns="false"
                    EmptyDataText="Sorry, there are no records found as per your searhing criteria."
                    GridLines="None" OnRowCommand="gdvUserDetail_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="S. No." HeaderStyle-CssClass="GdvHleft" ItemStyle-CssClass="GdvRleft">
                            <ItemTemplate>
                                <asp:Label ID="lblSNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="User ID" HeaderStyle-CssClass="GdvHmiddle" ItemStyle-CssClass="GdvRmiddle">
                            <ItemTemplate>
                                <%# Eval("UserID")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="User Name" HeaderStyle-CssClass="GdvHmiddle" ItemStyle-CssClass="GdvRmiddle">
                            <ItemTemplate>
                                <%# Eval("UserTitle")%>&nbsp; <%# Eval("UserFirstName")%>&nbsp; <%# Eval("UserLastName")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="User Role" HeaderStyle-CssClass="GdvHmiddle" ItemStyle-CssClass="GdvRmiddle">
                            <ItemTemplate>
                                <%# Eval("UserRole")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="IsActive" HeaderStyle-CssClass="GdvHmiddle" ItemStyle-CssClass="GdvRmiddle">
                            <ItemTemplate>
                                <%# Eval("UserisActive")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Ip Check" HeaderStyle-CssClass="GdvHmiddle" ItemStyle-CssClass="GdvRmiddle">
                            <ItemTemplate>
                                <%# Eval("IsIpCheck")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="GdvHright" ItemStyle-CssClass="GdvRright">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnEdit" runat="server" CssClass="linkbutton3" CommandArgument='<%# Eval("UserID")%>' CommandName="EditUser">Edit</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>


</asp:Content>

