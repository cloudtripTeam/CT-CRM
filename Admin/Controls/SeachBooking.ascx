<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SeachBooking.ascx.cs" Inherits="Admin_Controls_SeachBooking" %>
<div class="panel-group">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="pull-left">
                    <h4 class="panel-title">
                        <a data-toggle="collapse" href="#bookingdetails">Booking Details</a>
                    </h4>
                </div>
                <div class="pull-right"><a data-toggle="collapse" href="#bookingdetails"><span class="glyphicon glyphicon-search"></span></a></div>
                <div class="clearfix"></div>
            </div>

            <div id="bookingdetails" class="panel-collapse collapse">
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-2">
                            <asp:TextBox class="form-control mb-10" ID="txtBookingID" runat="server" placeholder="Booking ID" />
                        </div>

                        <div class="col-md-2">
                            <asp:DropDownList ID="ddlStatus" runat="server" class="form-control mb-10">
                                <asp:ListItem Value="">Select Booking Status</asp:ListItem>
                                <asp:ListItem Value="Booked">Booked</asp:ListItem>
                                <asp:ListItem Value="Cancelled">Cancelled</asp:ListItem>
                                <asp:ListItem Value="Confirm">Confirm</asp:ListItem>
                                <asp:ListItem Value="Decline">Decline</asp:ListItem>
                                <asp:ListItem Value="Incomplete">Incomplete</asp:ListItem>
                                <asp:ListItem Value="Queue">Queue</asp:ListItem>
                                <asp:ListItem Value="Dupe">Dupe</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-2">

                            <asp:TextBox ID="txtPNRConfirmation" class="form-control mb-10" runat="server" placeholder="PNR" />
                        </div>
                        <div class="col-md-3">

                            <asp:TextBox class="form-control mb-10" ID="txtFromDate" onclick="showCalender(this);" runat="server" placeholder="Booking From Date" />
                        </div>
                        <div class="col-md-3">

                            <asp:TextBox class="form-control mb-10" ID="txtToDate" onclick="showCalender(this);" runat="server" placeholder="Booking To Date" />
                        </div>
                    </div>
                    <div class="clearfix"></div>
                    <div class="row">
                        <div class="col-md-2">
                           <%-- <asp:DropDownList ID="ddlProduct" class="form-control mb-10" runat="server">
                                <asp:ListItem Value="">ANY Product Type</asp:ListItem>
                                <asp:ListItem Value="ARF" Selected="True">Flight</asp:ListItem>
                                <asp:ListItem Value="HTL">Hotel</asp:ListItem>
                            </asp:DropDownList>--%>

                        </div>
                        <div class="col-md-2">
                            <asp:DropDownList ID="ddlCompany" class="form-control mb-10" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            <asp:DropDownList class="form-control" ID="ddlSourceMedia" runat="server">
                                <asp:ListItem Value="">ANY Source Media</asp:ListItem>
                            </asp:DropDownList>
                        </div>

                        <div class="col-md-3">

                            <asp:TextBox class="form-control mb-10" ID="txtEmailAddress" runat="server" placeholder="Email ID"></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                        </div>
                    </div>
                    <div class="clearfix"></div>
                    <div class="row">
                        <div class="col-md-2">
                            <asp:TextBox class="form-control mb-10" ID="txtPaxFirstName" runat="server" placeholder="First Name"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <asp:TextBox class="form-control mb-10" ID="txtPaxLastName" runat="server" placeholder="Last Name"></asp:TextBox>
                        </div>
                        <div class="col-md-2">

                            <asp:TextBox class="form-control mb-10" ID="txtPhoneNo" runat="server" placeholder="Phone No"></asp:TextBox>
                        </div>

                        <div class="col-md-3">
                            <asp:TextBox class="form-control mb-10" ID="txtMobileNo" runat="server" placeholder="Mobile No"></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <span></span>
                            <br />
                            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-danger btn-lg" OnClientClick="return SearchBooking();" Text="Search" OnClick="btnSearch_Click"></asp:Button>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>