<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="RouteList.aspx.cs" Inherits="Admin_BlockedIPs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../../css/StyleSheet.css" rel="stylesheet" />
    <style>
        td, th {
            padding-left: 10px;
        }

        .btnbook, btnbook:visited .btnbook:active .btnbook:focus {
            border: none;
            padding: 0px;
            background-color: #fff;
        }

        a:hover, a:focus {
            text-decoration: underline;
        }

        .ptbtn-32 {
            padding-top: 32px;
        }

        .txtcolor {
            color: red;
        }
    </style>

</asp:Content>
 
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="container">
      
        <div class="panel panel-default">
            <div class="panel-heading">Route List</div>
            <div class="panel-body" style="line-height: 34px;">
                <div class="row">
                    <asp:HiddenField ID="HiddenField1" runat="server" />
                    <div class="col-md-4">
                        <span>Description</span><span class="txtcolor">*</span>
                        <asp:TextBox CssClass="form-control textbox" MaxLength="3" runat="server" ID="txtDescription"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <span>ISO Code</span><span class="txtcolor">*</span>
                        <asp:TextBox CssClass="form-control textbox" MaxLength="3" runat="server" ID="txtIsoCode"></asp:TextBox>
                    </div>
                    <div class="col-md-4 ptbtn-32">
                        <asp:Button ID="btnSubmit" CssClass="btn btn-primary btn-lg" runat="server" Text="Save" OnClick="Button1_Click" />&nbsp;
                        <%--<asp:Button ID="btnSubmit1" CssClass="btn btn-primary btn-lg" runat="server" Text="Export" OnClick="btnExport_Click" />--%>&nbsp;
                       
                        <asp:Button ID="Button1"  CssClass="btn btn-primary btn-lg"  runat="server" OnClick="btnExport_Click" Text="Export" />
                       
                    </div>
                </div>
            </div>
        </div>
         <div class="panel panel-default" id="pnlbulkMarkup" runat="server">
                <div class="panel-heading">
                    <div class="pull-left">
                        <h4 class="panel-title">
                            <a data-toggle="collapse" href="#bulkUpdate" class="btn btn-primary btn-xs" aria-expanded="true">Bulk Update Routes</a>
                        </h4>
                    </div>
                    <div class="clearfix"></div>
                </div>

                <div id="bulkUpdate" class="panel-collapse collapse">
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-3">
                                <asp:FileUpload ID="FileUpload1" runat="server" Width="200px" CssClass="active" Height="36px" />
                            </div>

                            <div class="col-md-2 ">
                                <asp:Button ID="btnUpload" runat="server" CssClass="btn btn-success btn-sm" OnClick="btnUpload_Click" Text="Upload Rules Details"/>
                            </div>
                           
                        </div>
                    </div>
                </div>
            </div>
    </div>

    <div class="container">
        <div cssclass=" pb-3" style="height: 20px;">
            <asp:Label CssClass="txtcolor" ID="txtSuccessMsg" runat="server" Text=""></asp:Label>
        </div>
        <div class="panel panel-default">
            <div class="panel-heading">Route List Details</div>

            <div class="panel-body" style="line-height: 34px; padding: 0px!important;">
                
                <asp:GridView CssClass="with:100%;" ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" Width="100%" ForeColor="Black" GridLines="Horizontal" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDeleting="GridView1_RowDeleting">
                    <Columns>

                        <asp:TemplateField HeaderText="Description">
                            <EditItemTemplate>
                                <asp:TextBox MaxLength="3" ID="txtDescrip" runat="server" Text='<%# Bind("Description") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblDescriip" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ISO Code">
                            <EditItemTemplate>
                                <asp:TextBox MaxLength="3" ID="txtISOCode" runat="server" Text='<%# Bind("ISOCode") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblISOCode" runat="server" Text='<%# Bind("ISOCode") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Edit">
                            <EditItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update" Text="Update"></asp:LinkButton>
                                &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Delete">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="False" CommandName="Delete" Text="Delete"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <EditItemTemplate>
                                <asp:TextBox Visible="false" ID="TextBox1" runat="server" Text='<%# Bind("Pid") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label Visible="false" ID="Label1" runat="server" Text='<%# Bind("Pid") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                    <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                    <HeaderStyle BackColor="#333333" Font-Bold="True" Font-Size="0.90em" ForeColor="White" />
                    <PagerStyle BackColor="White" ForeColor="Black" Font-Size="0.90em" HorizontalAlign="Right" />
                    <RowStyle Font-Size="0.90em" />
                    <SortedAscendingCellStyle BackColor="#F7F7F7" />
                    <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                    <SortedDescendingCellStyle BackColor="#E5E5E5" />
                    <SortedDescendingHeaderStyle BackColor="#242121" />
                </asp:GridView>
                    
             
                    
            </div>
        </div>
    </div>
</asp:Content>

