<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="KAYAK_Compare.aspx.cs" Inherits="Admin_KAYAK_Compare" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="p-20" style="background: #fff;">
        <table class="table table-hover">
            <thead>
                <tr>
                    <th>From</th>
                    <th>To</th>
                    <th>Title</th>
                    <th>Description</th>
                    <th></th>
                    <th></th>

                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>
                        <asp:TextBox ID="txtFrom" placeholder="From Airport" MaxLength="3" CssClass="form-control" runat="server"></asp:TextBox></td>
                    <td>
                        <asp:TextBox ID="txtTo" placeholder="To Airport" CssClass="form-control" MaxLength="3" runat="server"></asp:TextBox></td>
                    <td>
                        <asp:TextBox ID="txtTitle" placeholder="Title" MaxLength="40" CssClass="form-control" runat="server"></asp:TextBox></td>
                    <td>
                        <asp:TextBox ID="txtDescription" MaxLength="100" placeholder="Description" CssClass="form-control" runat="server"></asp:TextBox></td>
                    <td>
                        <asp:Button ID="btnInsert" OnClientClick="return validate()" Text="Insert Record" CssClass="btn btn-default" runat="server" OnClick="btnInsert_Click" />
                    </td>
                    <td style="text-align:left;">
                        <asp:Button ID="btnFind" Text="Find Data" CssClass="btn btn-default" runat="server" OnClick="btnFind_Click" />
                    </td>

                </tr>
                <tr>
                    <td colspan="6">
                        <asp:Literal ID="ltrMsg" runat="server"></asp:Literal>
                    </td>
                </tr>
            </tbody>
        </table>
        <hr />
        <table class="table table-hover">
                
                <asp:GridView ID="gvKAYAK" Width="100%" GridLines="None" runat="server" AutoGenerateColumns="false" OnRowCommand="gvKAYAK_RowCommand">
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <tr>
                                    <th style="text-align: left;">From</th>
                                    <th style="text-align: left;">To</th>
                                    <th style="text-align: left;">Title</th>
                                    <th style="text-align: left;">Description</th>
                                    <th></th>
                                    <th></th>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tbody>
                                    <tr>
                                        <td style="padding-bottom:5px !important;">
                                            <asp:TextBox ID="txtFromU" placeholder="From Airport" Text='<%# Eval("KAYAK_From") %>' CssClass="form-control" runat="server"></asp:TextBox></td>
                                        <td style="padding-bottom:5px !important;">
                                            <asp:TextBox ID="txtToU" placeholder="To Airport" CssClass="form-control" Text='<%# Eval("KAYAK_To") %>' runat="server"></asp:TextBox></td>
                                        <td style="padding-bottom:5px !important;">
                                            <asp:TextBox ID="txtTitleU" placeholder="Title" MaxLength="40" CssClass="form-control" Text='<%# Eval("KAYAK_Title") %>' runat="server"></asp:TextBox></td>
                                        <td style="padding-bottom:5px !important;">
                                            <asp:TextBox ID="txtDescriptionU" placeholder="Description" MaxLength="100" CssClass="form-control" Text='<%# Eval("KAYAK_Description") %>' runat="server"></asp:TextBox></td>
                                        <td style="text-align: center;padding-bottom:5px !important;">
                                            <asp:HiddenField ID="hfID" Value='<%# Eval("KAYAK_ID") %>' runat="server" />
                                            <asp:Button ID="btnUpdate" Text="Update" CssClass="btn btn-info" runat="server" CommandName="update" />
                                        </td >
                                        <td style="text-align: center;padding-bottom:5px !important;">
                                            <asp:Button ID="btnDelete" Text="Delete" CssClass="btn btn-danger" runat="server" CommandName="delete" />
                                        </td>
                                    </tr>
                                
                                </tbody>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                    
            </table>
    </div>
    <script>
        function validate() {
            if (document.getElementById("<%=txtFrom.ClientID %>").value == "") {
                alert("Please enter dep iata code.");
                document.getElementById("<%=txtFrom.ClientID %>").focus();
                return false;
            }
            if (document.getElementById("<%=txtTo.ClientID %>").value == "") {
                alert("Please enter destination iata code.");
                document.getElementById("<%=txtTo.ClientID %>").focus();
                return false;
            }
            if (document.getElementById("<%=txtTitle.ClientID %>").value == "") {
                alert("Enter Title.");
                document.getElementById("<%=txtTitle.ClientID %>").focus();
                return false;
            }
            if (document.getElementById("<%=txtDescription.ClientID %>").value == "") {
                alert("Enter Description.");
                document.getElementById("<%=txtDescription.ClientID %>").focus();
                return false;
            }
        }
    </script>
</asp:Content>

