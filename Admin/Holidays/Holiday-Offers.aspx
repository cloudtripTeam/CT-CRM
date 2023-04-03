<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="Holiday-Offers.aspx.cs" ValidateRequest="false"  Inherits="Admin_Holidays_Holiday_Offers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
   <%-- <link href="../css/StyleSheet.css" rel="stylesheet" />--%>
    <link href="../../css/StyleSheet.css" rel="stylesheet" />
    <link href="//cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/css/bootstrap-multiselect.css" rel="stylesheet" type="text/css" />
    <script src="//cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/js/bootstrap-multiselect.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $('[id*=ddlCategory]').multiselect({
                includeSelectAllOption: true
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="panel-group">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="pull-left">
                    <h4 class="panel-title">
                        <a data-toggle="collapse" href="#bookingdetails">Holiday Offers</a>
                    </h4>
                </div>
                <div class="pull-right"><a data-toggle="collapse" href="#bookingdetails"><span class="glyphicon glyphicon-search"></span></a></div>
                <div class="clearfix"></div>
            </div>

            <div id="bookingdetails" class="panel-collapse">
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-2">
                            <asp:TextBox class="form-control mb-10" ID="txtHotelName" runat="server" placeholder="Hotel Name" />
                        </div>
                        <div class="col-md-2">
                            <asp:TextBox class="form-control mb-10" ID="txtDestCode" MaxLength="3" runat="server" placeholder="Destination Code" />
                        </div>
                        <div class="col-md-2">

                            <asp:TextBox ID="txtRating" onkeyup="AllowNumeric(this)" class="form-control mb-10" runat="server" placeholder="Star Rating" />
                        </div>
                        <div class="col-md-2">
                            <asp:DropDownList ID="ddlOfferType" runat="server" class="form-control mb-10">
                                <asp:ListItem Value="">Select Offer Type</asp:ListItem>
                                <asp:ListItem Value="Recommended">Recommended</asp:ListItem>
                                <asp:ListItem Value="Sponsored">Sponsored</asp:ListItem>
                            </asp:DropDownList>

                        </div>
                      <div class="col-md-2">
                          <asp:DropDownList ID="ddlCompany" class="form-control mb-5" Width="160px" runat="server" >
                            </asp:DropDownList> 
                          </div>
                        <div class="col-md-2">
                                    <asp:ListBox ID="ddlCategory" runat="server" class="form-control mb-5" SelectionMode="Multiple">
                                <asp:ListItem Value="">Select Offer Category</asp:ListItem>
                                <asp:ListItem Value="Beach Holidays">Beach Holidays</asp:ListItem>
                                <asp:ListItem Value="Family Holidays">Family Holidays</asp:ListItem>
                                <asp:ListItem Value="Weddings">Weddings</asp:ListItem>
                                <asp:ListItem Value="Luxury Holidays">Luxury Holidays</asp:ListItem>
                                <asp:ListItem Value="Honeymoons">Honeymoons</asp:ListItem>
                                <asp:ListItem Value="All Inclusive Holidays">All Inclusive Holidays</asp:ListItem>
                                <asp:ListItem Value="Worldwide Tours">Worldwide Tours</asp:ListItem>
                                <asp:ListItem Value="Multi Center Holidays">Multi Center Holidays</asp:ListItem>
                                         <asp:ListItem Value="Hotel">Hotel</asp:ListItem>
                            </asp:ListBox>
                        </div>
                    </div>
                    <div class="clearfix"></div>
                    <div class="row">

                        <div class="col-md-2">

                            <asp:TextBox ID="txtNoofNights" class="form-control mb-10" onkeyup="AllowNumeric(this)" runat="server" placeholder="No of Nights" />
                        </div>
                        <div class="col-md-2">

                            <asp:TextBox class="form-control mb-10" ID="txtPrice" onkeyup="AllowDecimal(this)" runat="server" placeholder="Price" />
                        </div>
                        <div class="col-md-2">
                            <asp:DropDownList ID="ddlBasicBoard" runat="server" class="form-control mb-10">
                                <asp:ListItem Value="">Select Board Basis</asp:ListItem>
                                <asp:ListItem Value="Bed And Breakfast">Bed And Breakfast</asp:ListItem>
                                <asp:ListItem Value="Half Board">Half Board</asp:ListItem>
                                <asp:ListItem Value="Full Board">Full Board</asp:ListItem>
                                <asp:ListItem Value="Room Only">Room Only</asp:ListItem>
                                <asp:ListItem Value="All Inclusive">All Inclusive</asp:ListItem>
                            </asp:DropDownList>

                        </div>
                        <div class="col-md-3">
                            <asp:TextBox class="form-control mb-10" ID="txtDiscount" onkeyup="AllowNumeric(this)" runat="server" placeholder="Discount" />
                        </div>
                        <div class="col-md-3">
                            <asp:FileUpload ID="fpImage" runat="server" CssClass="btn btn-default btn-file form-control" />
                        </div>
                    </div>
                    <div class="clearfix"></div>

                    <div class="row">
                        <div class="col-md-9">
                            <asp:TextBox CssClass="textEditor1" ID="txtOverviews" TextMode="MultiLine" runat="server" placeholder="hotel overviews" />
                        </div>
                        <div class="col-md-3">
                            <span></span>
                            <br />
                            <asp:Literal ID="ltrMsg" runat="server"></asp:Literal>
                            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-danger btn-lg" OnClientClick="return validate();" Text="Save" OnClick="btnSave_Click"></asp:Button>
                            &nbsp;
                            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-danger btn-lg" Text="Search" OnClick="btnSearch_Click"></asp:Button>
                        </div>
                    </div>
                    <div class="clearfix"></div>
                    <div class="row">
                        <div class="col-md-12 well">
                              <div class="col-md-1">H. ID</div>
                            <div class="col-md-1">Dest Name</div>
                            <div class="col-md-2">Hotel Name</div>
                            <div class="col-md-1">Star Rating</div>
                            <div class="col-md-1">Holiday Type</div>
                            <div class="col-md-1">Nights</div>
                            <div class="col-md-1">Price</div>
                            <div class="col-md-2">Board Basic</div>
                            <div class="col-md-1">Discount</div>
                            <div class="col-md-1">Company</div>
                        </div>
                        <div class="clearfix"></div>

                        <%=Holidays %>
                    </div>

                </div>
            </div>
        </div>
    </div>

    <div style="height: 657px; width: 1366px; display: none;" id="fadebackground">
    </div>
    <div id="popProgressBar" style="height: 30%; width: 30%; top: 230px; left: 478px; display: none;" class="popup-product" align="center">
        <table align="center" bgcolor="#ffffff" height="100%" width="100%">
            <tbody>
                <tr>
                    <td class="popup-header">Please wait while we process your request...
                    </td>
                </tr>
                <tr>
                    <td align="center" valign="middle">
                        <img src="../Images/Wait.gif" id="ImageProgressbar">
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #ffffff; color: #B9B9B9; vertical-align: middle; text-align: center; font-size: 18px; font-family: Verdana;" align="center" height="40"></td>
                </tr>


            </tbody>
        </table>
    </div>
    <style>
        .textss {
            overflow: hidden;
            display: -webkit-box;
            -webkit-line-clamp: 1; /* number of lines to show */
            -webkit-box-orient: vertical;
        }
    </style>
    <script type="text/javascript">

        function popup(divProgressBar, width, height) {
            try {
                var height1 = $(window).height();
                var width1 = $(window).width();
                $('#' + divProgressBar).height(height + "%");
                $('#' + divProgressBar).width(width + "%");
                $('#' + divProgressBar).css({ top: ((height1 - ((height1 * parseInt(height)) / 100)) / 2).toFixed(0) + "px", left: ((width1 - ((width1 * parseInt(width)) / 100)) / 2).toFixed(0) + "px" });
                $('#fadebackground').height(height1 + "px");
                $('#fadebackground').width(width1 + "px");
                $('#fadebackground').toggle();
                $('#' + divProgressBar).toggle();
                return false;
            }
            catch (e) { return false; }
        }
        function SearchBooking() {
            popup('popProgressBar', 30, 30);
            return true;
        }

    </script>

    <script language="JavaScript">
        function AllowNumeric(txt) {
            if (/\D/g.test(txt.value))
                txt.value = txt.value.replace(/\D/g, '');
        }
        function AllowDecimal(txt) {
            if (/[^\d.]/g.test(txt.value))
                txt.value = txt.value.replace(/[^\d.]/g, '');
        }
    </script>
    <script type="text/javascript">
        function validate() {
            if (document.getElementById('<%=txtHotelName.ClientID%>').value == "") {
                alert("Enter Hotel Name.");
                document.getElementById('<%=txtHotelName.ClientID%>').focus();
                return false;
            }
            if (document.getElementById('<%=txtDestCode.ClientID%>').value == "") {
                alert("Enter Destination Code.");
                document.getElementById('<%=txtDestCode.ClientID%>').focus();
                return false;
            }
            if (document.getElementById('<%=txtRating.ClientID%>').value == "") {
                alert("Enter Star Rating.");
                document.getElementById('<%=txtRating.ClientID%>').focus();
                return false;
            }
            if (document.getElementById('<%=txtNoofNights.ClientID%>').value == "") {
                alert("Enter No of Nights.");
                document.getElementById('<%=txtNoofNights.ClientID%>').focus();
                return false;
            }
            if (document.getElementById('<%=txtPrice.ClientID%>').value == "") {
                alert("Enter Price.");
                document.getElementById('<%=txtPrice.ClientID%>').focus();
                return false;
            }
            if (document.getElementById('<%=txtDiscount.ClientID%>').value == "") {
                alert("Enter Discount.");
                document.getElementById('<%=txtDiscount.ClientID%>').focus();
                return false;
            }

            return true;
        }
    </script>

    <script lang="javascript" type="text/javascript">
        $('.textEditor1').jqte();
</script>


</asp:Content>

