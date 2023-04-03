<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="SupplierMaster.aspx.cs" Inherits="Admin_SupplierMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="">
        <div style="height: 15px;"></div>
        <div class="panel panel-default">
            <div class="panel-heading">Supplier Details</div>

            <div class="panel-body" style="line-height: 34px;">
                <div class="row">
                    <div class="col-md-1">
                        <label>Supplier Code</label>
                        <asp:TextBox ID="txtCode" runat="server" ValidationGroup="INS" CssClass="form-control" PlaceHolder="Supplier Code"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtCode" ForeColor="Red" ValidationGroup="INS" runat="server" ErrorMessage="Supplier Code Require."></asp:RequiredFieldValidator>
                    </div>

                     <div class="col-md-1">
                        <label>Atol No</label>
                        <asp:TextBox ID="txtAtol" runat="server" CssClass="form-control" ValidationGroup="INS" PlaceHolder="Supplier Name"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtAtol" ForeColor="Red" ValidationGroup="INS" runat="server" ErrorMessage="Atol No Require."></asp:RequiredFieldValidator>
                    </div>

                    <div class="col-md-2">
                        <label>Supplier Name</label>
                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control" ValidationGroup="INS" PlaceHolder="Supplier Name"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtName" ForeColor="Red" ValidationGroup="INS" runat="server" ErrorMessage="Supplier Name Require."></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-md-2">
                        <label>Email ID</label>
                        <asp:TextBox ID="txtEmailID" runat="server" CssClass="form-control" PlaceHolder="Email ID"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <label>Phone No</label>
                        <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control" PlaceHolder="Phone No"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <label>Contact Person</label>
                        <asp:TextBox ID="txtCPerson" runat="server" CssClass="form-control" PlaceHolder="Contact Person"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <label>Remarks</label>
                        <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" PlaceHolder="Remarks"></asp:TextBox>
                    </div>

                </div>

                <div class="col-md-12">
                    &nbsp;
                        <asp:Label ID="lblMsg" runat="server" ForeColor="Red" EnableViewState="false"></asp:Label>
                </div>
                <div class="row">


                    <div class="col-md-6">
                        <label>
                            <br />
                            &nbsp;
                        </label>
                        <asp:Button ID="btnInsert" runat="server" CssClass="btn btn-primary btn-lg" Text="Insert" ValidationGroup="INS" OnClick="btnInsert_Click" />
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-lg" Text="Search" OnClick="btnSearch_Click" />

                    </div>
                </div>
            </div>
            <div class="clearfix"></div>
            <div class="panel-body">
                <div class="row">
                    <asp:Repeater ID="rptSupplier" runat="server" OnItemCommand="rptSupplier_ItemCommand">
                        <HeaderTemplate>
                            <div class="col-md-12 well-sm">
                                <div class="col-md-1">Supplier ID</div>
                                <div class="col-md-2">Supplier Name</div>
                                <div class="col-md-2">Email ID</div>
                                <div class="col-md-1">Phone No</div>
                                <div class="col-md-1">Atol No</div>
                                <div class="col-md-2">Cont. Person</div>
                                <div class="col-md-2">Remarks</div>
                                <div class="col-md-1"></div>
                            </div>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <div class="col-md-12">
                                <div class="row">
                                    <div class="col-md-1">

                                        <asp:TextBox ID="txtCode" runat="server" CssClass="form-control" Enabled="false" Text='<%# Eval("SUP_Code") %>'></asp:TextBox>

                                    </div>
                                    <div class="col-md-2">

                                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control" Text='<%# Eval("SUP_Name") %>'></asp:TextBox>

                                    </div>
                                    <div class="col-md-2">

                                        <asp:TextBox ID="txtEmailID" runat="server" CssClass="form-control" Text='<%# Eval("SUP_EmailID") %>'></asp:TextBox>
                                    </div>
                                    <div class="col-md-1">

                                        <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control" Text='<%# Eval("SUP_Phone") %>'></asp:TextBox>
                                    </div>
                                     <div class="col-md-1">

                                        <asp:TextBox ID="txtAtol" runat="server" CssClass="form-control" Text='<%# Eval("Sup_AtolNo") %>'></asp:TextBox>
                                    </div>
                                    <div class="col-md-2">

                                        <asp:TextBox ID="txtCPerson" runat="server" CssClass="form-control" Text='<%# Eval("SUP_ContactPerson") %>'></asp:TextBox>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:HiddenField ID="hfID" Value='<%# Eval("SUP_ID") %>' runat="server" />
                                        <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" Text='<%# Eval("Remarks") %>'></asp:TextBox>
                                    </div>
                                    <div class="col-md-1">
                                        <asp:Button ID="btnSearch" runat="server" CommandName="Update" CssClass="btn btn-danger btn-lg" Text="Update" />
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>


        </div>
    </div>

</asp:Content>

