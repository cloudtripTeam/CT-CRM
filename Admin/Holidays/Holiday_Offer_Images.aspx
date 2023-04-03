<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="Holiday_Offer_Images.aspx.cs" Inherits="Admin_Holidays_Holiday_Offer_Images" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="panel-group">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="pull-left">
                    <h4 class="panel-title">
                        <a data-toggle="collapse" href="#bookingdetails">Holiday Speical Offers</a>
                    </h4>
                </div>
                <div class="pull-right"><a data-toggle="collapse" href="#bookingdetails"><span class="glyphicon glyphicon-search"></span></a></div>
                <div class="clearfix"></div>
            </div>

            <div id="bookingdetails" class="panel-collapse collapse in">
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-2">
                            <asp:DropDownList ID="ddlOfferType" runat="server" class="form-control mb-10">
                                <asp:ListItem Value="">Select Offer Type</asp:ListItem>
                                <asp:ListItem Value="Included">Included</asp:ListItem>
                                <asp:ListItem Value="Special Offer">Special Offer</asp:ListItem>

                            </asp:DropDownList>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox class="form-control mb-10" ID="txtOfferType" runat="server" placeholder="Offer Type" />
                        </div>
                        <div class="col-md-3">

                            <asp:FileUpload ID="fpIcon" runat="server" CssClass="btn btn-default btn-file form-control" />
                        </div>
                        <div class="col-md-3">
                            <asp:CheckBox ID="chkMainPage" runat="server" Text="Is Main Page ?" TextAlign="Right" />

                        </div>
                        <div class="col-md-3">

                            <asp:Literal ID="ltrMsg" runat="server"></asp:Literal>
                            <%--<asp:Button ID="btnSave" runat="server" CssClass="btn btn-danger btn-lg" Text="Save" OnClick="btnSave_Click"></asp:Button>--%>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

