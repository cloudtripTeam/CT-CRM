<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="ConvertFare.aspx.cs" Inherits="Admin_ConvertFare" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%--<link href="../css/StyleSheet.css" rel="stylesheet" />--%>
    <link href="../../css/StyleSheet.css" rel="stylesheet" />
    <script src="https://www.traveljunction.co.uk/temp/CalendarAnyYear.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <div style="height: 15px;"></div>
        <div class="panel panel-default">
            <div class="panel-heading">Own Fare Offers</div>

            <div class="panel-body" style="line-height: 34px;">
                <div class="row">
                    <div class="col-md-2">
                        <label>Origin</label>
                        <asp:TextBox ID="txtOrigin" runat="server" CssClass="form-control" PlaceHolder="Origin" MaxLength="3"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <label>Destination</label>
                        <asp:TextBox ID="txtDestination" runat="server" CssClass="form-control" PlaceHolder="Destination" MaxLength="3"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <label>TravelDateStart</label>
                        <asp:TextBox ID="txtTravelDateStart" onclick="showCalender(this);" runat="server" CssClass="form-control" PlaceHolder="dd/mm/yyyy"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <label>TravelDateEnd</label>
                        <asp:TextBox ID="txtTravelDateEnd" onclick="showCalender(this);" runat="server" CssClass="form-control" PlaceHolder="dd/mm/yyyy"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <label>Airline</label>
                        <asp:TextBox ID="txtAieline" runat="server" CssClass="form-control" MaxLength="2" PlaceHolder="Airline Code"></asp:TextBox>
                    </div>

                    <div class="col-md-2">
                        <label>Airline Class</label>
                        <asp:TextBox ID="txtAirlineClass" runat="server" CssClass="form-control" MaxLength="1" PlaceHolder="Airline Code"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        <label>CabinClass</label>
                        <asp:DropDownList ID="ddlCabinClass" runat="server" CssClass="form-control">
                            <asp:ListItem Value="">ANY</asp:ListItem>
                            <asp:ListItem Value="Y">ECONOMY</asp:ListItem>
                            <asp:ListItem Value="BUSINESS">BUSINESS</asp:ListItem>
                            <asp:ListItem Value="FIRSTCLASS">FIRSTCLASS</asp:ListItem>
                            <asp:ListItem Value="PREMIUM">PREMIUM</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2">
                        <label>BaseFare</label>
                        <asp:TextBox ID="txtBaseFare" runat="server" CssClass="form-control" PlaceHolder="0.00" Text="0"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <label>Tax</label>
                        <asp:TextBox ID="txtTax" runat="server" CssClass="form-control" PlaceHolder="0.00" Text="0"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <label>Markup</label>
                        <asp:TextBox ID="txtMarkup" runat="server" CssClass="form-control" PlaceHolder="0.00" Text="0"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <label>Commission</label>
                        <asp:TextBox ID="txtCommssion" runat="server" CssClass="form-control" PlaceHolder="0.00" Text="0"></asp:TextBox>
                    </div>

                   
                </div>
                <div class="col-md-12">
                    &nbsp;
                        <asp:Label ID="lblMsg" runat="server" ForeColor="Red" EnableViewState="false"></asp:Label>
                </div>
            </div>
            <div class="clearfix"></div>
            <div class="panel" style="padding-top: 0px; margin-top: 20px;">
                <div class="panel-body " style="border: 1px solid #ddd; padding: 0px!important;">
                    

                </div>
            </div>
        </div>
    </div>
    

</asp:Content>

