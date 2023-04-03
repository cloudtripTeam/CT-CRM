<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="Holiday_Offer_City.aspx.cs" Inherits="Admin_Holidays_Holiday_Offer_City" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="panel-group">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="pull-left">
                    <h4 class="panel-title">
                        <a data-toggle="collapse" href="#bookingdetails">Holiday City Details</a>
                    </h4>
                </div>
                <div class="pull-right"><a data-toggle="collapse" href="#bookingdetails"><span class="glyphicon glyphicon-search"></span></a></div>
                <div class="clearfix"></div>
            </div>

            <div id="bookingdetails" class="panel-collapse">
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-2">
                            <asp:TextBox class="form-control mb-10" ID="txtCityCode" runat="server" MaxLength="3" placeholder="City Code" />
                        </div>
                        <div class="col-md-2">
                            <asp:TextBox class="form-control mb-10" ID="txtFlightTime" runat="server" placeholder="Flight Time" />
                        </div>
                        <div class="col-md-2">

                            <asp:TextBox ID="txtTimeZone" class="form-control mb-10" runat="server" placeholder="Time Zone" />
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtAirlines" class="form-control mb-10" runat="server" placeholder="Airines List" />

                        </div>
                        <div class="col-md-3">

                            <asp:TextBox ID="txtWhereFly" class="form-control mb-10" runat="server" placeholder="Where to Fly" />
                        </div>
                    </div>

                    <div class="clearfix"></div>

                    <div class="row">
                        <div class="col-md-9">
                            <asp:TextBox CssClass="textEditor1" ID="txtOverviews" TextMode="MultiLine" runat="server" placeholder="hotel overviews" />
                            <asp:Literal ID="ltrID" runat="server"></asp:Literal>
                        </div>
                        <div class="col-md-3">
                            <span></span>
                            <br />
                            <asp:Literal ID="ltrMsg" runat="server"></asp:Literal>
                            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-danger btn-lg" OnClientClick="return validate();" Text="Save" OnClick="btnSave_Click"></asp:Button>
                            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-danger btn-lg" Text="Search" OnClick="btnSearch_Click"></asp:Button>
                            <asp:Button ID="btnUpdates" runat="server" CssClass="btn btn-danger btn-lg" Visible="false" Text="Update" OnClick="btnUpdates_Click"></asp:Button>
                            <asp:Button ID="btnCancle" runat="server" CssClass="btn btn-danger btn-lg" Visible="false" Text="Cancle" OnClick="btnCancle_Click"></asp:Button>
                            
                        </div>
                    </div>
                    <div class="clearfix"></div>
                    
                    <div class="row">
                        
                        <asp:GridView ID="grdCITY" runat="server" Width="100%" AutoGenerateColumns="false" ShowFooter="false" ShowHeader="true" OnRowDeleting="grdCITY_RowDeleting">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <div class="col-md-12 well">
                                            <div class="col-md-1">City</div>
                                            <div class="col-md-2">Flight Time</div>
                                            <div class="col-md-2">Time Zone</div>
                                            <div class="col-md-2">Airline</div>
                                            <div class="col-md-2">Where Fly</div>
                                            <div class="col-md-2">Details</div>
                                            <div class="col-md-1"></div>
                                        </div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="col-md-12 well">
                                            
                                            <div class="col-md-1"><asp:Literal ID="ltrCode" Text='<%# Eval("HLD_OFR_City_Code") %>' runat="server"></asp:Literal></div>
                                            <div class="col-md-2"><asp:Literal ID="ltrFTime" Text='<%# Eval("HLD_OFR_City_FlightTime") %>' runat="server"></asp:Literal></div>
                                            <div class="col-md-2"><asp:Literal ID="ltrTimeZone" Text='<%# Eval("HLD_OFR_City_TimeZone") %>' runat="server"></asp:Literal></div>
                                            <div class="col-md-2"><asp:Literal ID="ltrAirlines" Text='<%# Eval("HLD_OFR_City_RecoAirlines") %>' runat="server"></asp:Literal></div>
                                            <div class="col-md-2 textss"><asp:Literal ID="ltrWhere" Text='<%# Eval("HLD_OFR_City_WhereFly") %>' runat="server"></asp:Literal></div>
                                            <div class="col-md-2 textss"><asp:Literal ID="ltrDetails" Text='<%# Eval("HLD_OFR_City_Details") %>' runat="server"></asp:Literal><asp:Literal ID="ltrID" Text='<%# Eval("HLD_OFR_City_ID") %>' Visible="false" runat="server"></asp:Literal></div>
                                            <div class="col-md-1">
                                                <asp:Button ID="btnDelete" CommandName="delete" CssClass="btn btn-danger btn-xs" runat="server" Text="Edit" /></div>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>

                </div>
            </div>
        </div>
    </div>
     <script lang="javascript" type="text/javascript">
         function validate() {
             if (document.getElementById('<%=txtCityCode.ClientID%>').value == "") {
                         alert("Enter City Code.");
                         document.getElementById('<%=txtCityCode.ClientID%>').focus();
                  return false;
              }
              if (document.getElementById('<%=txtTimeZone.ClientID%>').value == "") {
                         alert("Enter Time Zone.");
                         document.getElementById('<%=txtTimeZone.ClientID%>').focus();
                  return false;
              }
              if (document.getElementById('<%=txtFlightTime.ClientID%>').value == "") {
                         alert("Enter Flight Time.");
                         document.getElementById('<%=txtFlightTime.ClientID%>').focus();
                  return false;
              }
              if (document.getElementById('<%=txtWhereFly.ClientID%>').value == "") {
                         alert("Enter Where Fly.");
                         document.getElementById('<%=txtWhereFly.ClientID%>').focus();
                return false;
            }
            if (document.getElementById('<%=txtAirlines.ClientID%>').value == "") {
                         alert("Enter Airlines.");
                         document.getElementById('<%=txtAirlines.ClientID%>').focus();
                return false;
            }



            return true;
        }


         $('.textEditor1').jqte();
</script>
    <style>
        .textss {
            overflow: hidden;
            display: -webkit-box;
            -webkit-line-clamp: 3; /* number of lines to show */
            -webkit-box-orient: vertical;
        }
    </style>
</asp:Content>

