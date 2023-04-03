<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="ATOL_Report_Summary.aspx.cs" Inherits="Admin_ATOL_Report_Summary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
   <%-- <link href="../css/StyleSheet.css" rel="stylesheet" />--%>
    <link href="../../css/StyleSheet.css" rel="stylesheet" />
    <script src="https://www.traveljunction.co.uk/temp/CalendarAnyYear.js"></script>
<%--    <link href="//cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/css/bootstrap-multiselect.css" rel="stylesheet" type="text/css" />
    <script src="//cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/js/bootstrap-multiselect.js" type="text/javascript"></script>--%>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="panel-group">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="row">
                    <div class="col-md-6 pull-left">
                        <h4 class="panel-title">
                            <a data-toggle="collapse" href="#bookingdetails">Atol Summary Report Financial Year <%=DateTime.Today.AddYears(-1).Year %> - <%=DateTime.Today.Year %></a>
                        </h4>
                    </div>
                    <div class="col-md-6 pull-left">
                    <div class="pull-right"><a data-toggle="collapse" href="#bookingdetails"><span class="glyphicon glyphicon-search" style="color: #fff;"></span></a></div>
                    <div class="clearfix"></div>
                        </div>
                </div>
                 <br />
                <div class="row">
                        <div class="col-md-6">

                            <asp:Button ID="btnfive" Visible="true" CssClass="btn btn-default" runat="server" Text="Fouth Quarter" OnClick="btnfive_Click" />
                            <asp:Literal ID="Literal1" Visible="true" runat="server"></asp:Literal>
                        </div>
                    <div class="col-md-2">
                            <asp:TextBox ID="txtDateFrom" CssClass="form-control" onclick="showCalender(this);" placeholder="From Date"  runat="server"></asp:TextBox>
                            
                        </div>
                        <div class="col-md-2">
                             <asp:TextBox ID="txtDateTo" CssClass="form-control" onclick="showCalender(this);" placeholder="To Date"  runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                           <asp:Button ID="btnCustome" CssClass="btn btn-default" runat="server" Text="Search" OnClick="btnCustome_Click" />
                        </div>
                    </div>
                <br />
                <br />
                <div class="row">
                    <div class="col-md-6 pull-left">
                    <div class="pull-left">
                        <h4 class="panel-title">
                            <a data-toggle="collapse" href="#bookingdetails">Atol Summary Report Financial Year <%=DateTime.Today.Year %> - <%=DateTime.Today.AddYears(1).Year %></a>
                        </h4>
                    </div>
                        </div>
                    </div>
                <br />
                    <div class="row">
                        <div class="col-md-3">

                            <asp:Button ID="btnfirst" CssClass="btn btn-default" runat="server" Text="First Quarter" OnClick="btnfirst_Click" />
                            <asp:Literal ID="ltFirst" runat="server"></asp:Literal>
                        </div>
                        <div class="col-md-3">
                            <asp:Button ID="btnsecond" CssClass="btn btn-default" runat="server" Text="Second Quarter" OnClick="btnsecond_Click" />
                            <asp:Literal ID="ltSecond" runat="server"></asp:Literal>
                        </div>
                        <div class="col-md-3">
                            <asp:Button ID="btnthird" CssClass="btn btn-default" runat="server" Text="Third Quarter" OnClick="btnthird_Click" />
                            <asp:Literal ID="ltThird" runat="server"></asp:Literal>
                        </div>
                        <div class="col-md-3">
                            <asp:Button ID="btnfourth" CssClass="btn btn-default" runat="server" Text="Fourth Quarter" OnClick="btnfourth_Click" />
                            <asp:Literal ID="ltFourth" runat="server"></asp:Literal>
                        </div>

                        <input id="setascurrdate" type="hidden" />
                        <input id="hdeprdate" type="hidden" />
                        <asp:DropDownList ID="ddlCompany" runat="server" Visible="false"></asp:DropDownList>
                    </div>
                <br />
                <div class="row">
                    <div class="col-md-6 pull-left">
                    <div class="pull-left">
                        <h4 class="panel-title">
                            <a data-toggle="collapse" href="#bookingdetails">Atol Summary Report Custome Date</a>
                        </h4>
                    </div>
                        </div>
                    </div>
                <br />
                <div class="row">
                        
                        <div class="col-md-3">
                           
                        </div>

                    </div>

                <br />
            </div>
        </div>
    </div>
    <div class="clearfix"></div>
    <div class="panel" style="padding-top: 0px; margin-top: 20px;">
        <div class="panel-body " style="border: 1px solid #ddd; padding: 0px!important;">
            <asp:Label ID="lblmessage" runat="server" Text="" ForeColor="Red"></asp:Label>
            <asp:Button ID="btnPdf" runat="server" Visible="false" Text="Export to PDF" OnClick="btnPdf_Click" />
            <%=output %>
        </div>
    </div>
</asp:Content>

