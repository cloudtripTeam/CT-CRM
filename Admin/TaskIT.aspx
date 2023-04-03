<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="TaskIT.aspx.cs" Inherits="Admin_TaskIT" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%--<link href="../css/StyleSheet.css" rel="stylesheet" />--%>
    <link href="../../css/StyleSheet.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.6/jquery.min.js" type="text/javascript"></script>
    <script src="//ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js"
        type="text/javascript"></script>
    <link href="//ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css"
        rel="Stylesheet" type="text/css" />

    <style>
        body {
            font: normal 14px verdana;
        }

        #tblEmpDetails {
            border: 1px solid black;
            padding: 0px;
            margin: 0px;
            border-collapse: collapse;
        }

            #tblEmpDetails td {
                padding: 5px;
            }

        .normalRow {
            background-color: #EFEFEF;
        }



        .altRow {
            background-color: #EEEEEE;
        }
    </style>


    <asp:HiddenField runat="server" ID="hdnUserId" />
    <div class="container">
        <div class="panel panel-default">
            <div class="panel-heading">Ticket Information</div>
            <div class="panel-body" style="line-height: 34px;">
                <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label for="usr">Task Detail:</label>
                                <textarea id="txtTicketDetail" class="form-control" rows="4" runat="server"></textarea>
                                <asp:RequiredFieldValidator ErrorMessage="Please Enter Task Detail" ForeColor="Red" ControlToValidate="txtTicketDetail" runat="server"  ValidationGroup="svg"/>
                            </div>
                        </div>

                    </div>
                    <div class="col-md-12">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label for="usr">Company Name:</label>
                                <asp:DropDownList CssClass="form-control" ID="ddlBrand" runat="server">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ErrorMessage="Please Select Company Detail" ForeColor="Red" ControlToValidate="ddlBrand" runat="server"  ValidationGroup="svg"/>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label for="usr">Agent Name:</label>
                                <asp:DropDownList CssClass="form-control" ID="ddlAgentName" runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <label for="usr" style="display: block">From Date</label>
                            <input type="date" name="Date" value="" runat="server" id="txtDateTime" style="width: 220px; height: 37px; padding: 10px;" />
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="btns">
                            <asp:Button ID="ButtonUpdate" runat="server" Style="width: 100px" CssClass="btn-primary btn" Text="Update" OnClick="ButtonUpdate_Click" />
                            <asp:Button ID="Search" runat="server" Style="width: 100px" CssClass="btn-primary btn" Text="Search" OnClick="btnSearch_Click" />
                            <asp:Button ID="ButtonCreate" runat="server" Style="width: 100px" CssClass="btn-primary btn" Text="Create" OnClick="btnCreate_Click" ValidationGroup="svg" />
                            <asp:Button ID="btnCancel" runat="server" Style="width: 100px" CssClass="btn-danger btn" Text="Cancel" OnClick="btnCancel_Click" />
                             <asp:Button ID="btnExport" runat="server" Style="width: 100px;float:right" CssClass="btn-info btn"  Text="Export" OnClick="btnExport_Click" Visible="false" />
                        </div>
                    </div>
                </div>
            </div>

            
        </div>

        <div class="panel" style="line-height: 34px; padding: 0px!important; border: none" runat="server" id="divMsg">
            <asp:Label Text="" runat="server" ID="lblMsg" Style="margin-left: 25px;" />
        </div>
       
        <div class="panel" style="line-height: 34px; padding: 0px!important; border: none">
            <asp:Repeater ID="rptrDetails" runat="server" OnItemCommand="rptrDetails_ItemCommand">
                <HeaderTemplate>
                    <table width='100%' cellpadding='0' cellspacing='0' class='table' style='margin-bottom: 0px;' id="tblEmpDetails">
                        <tr>
                            <td class='gdvh'>SrNo</td>
                            <td class='gdvh' style="width:400px!important">Task Detail</td>
                            <td class='gdvh'>Company Name</td>
                            <td class='gdvh'>Created By</td>
                            <td class='gdvh'>Assigned To</td>
                            <td class='gdvh'>Created Date</td>
                            <td class='gdvh'>Action</td>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr class="<%#(Container.ItemIndex+1)%2==0?"altRow":"NormalRow"%>">
                        <td class='gdvr'><%# Container.ItemIndex+ 1 %></td>
                        <td class='gdvr'><%# Eval("Ticket_Detail")%></td>
                        <td class='gdvr'><%# Eval("Company_Name")%></td>
                        <td class='gdvr'><%# Eval("Created_By")%></td>
                        <td class='gdvr'><%# Eval("Assigned_To")%></td>
                        <td class='gdvr'><%# Eval("Created_Date")%></td>
                        <td>

                            <asp:ImageButton ID="imgBtnEdit" CommandName="Edit" ToolTip="Edit a record" CommandArgument='<%#Eval("Id") %>'
                                runat="server" ImageUrl="../images/Image_gridview/edit_new_image.png" />

                            <asp:ImageButton ToolTip="Delete a record" OnClientClick="javascript:return confirm('Are you sure to delete record?')" ID="imgBtnDelete"
                                CommandName="Delete" CommandArgument='<%#Eval("Id") %>' runat="server" ImageUrl="../images/delete_new.png" />

                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>

