<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="Holiday_Special_Offers.aspx.cs" ValidateRequest="false" MaintainScrollPositionOnPostback="true" Inherits="Admin_Holidays_Holiday_Special_Offers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href='<%=BLL.WebsiteStaticData.WebsiteUrl %>css/StyleSheet.css' rel="stylesheet" />
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


    <section style="background: #FFF;">


        <div class="panel-group">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <div class="pull-left">
                        <h4 class="panel-title">
                            <a data-toggle="collapse">Holiday Speical Offers</a>
                        </h4>
                    </div>
                    <div class="pull-right"><a data-toggle="collapse" href="#bookingdetails"><span class="glyphicon glyphicon-search"></span></a></div>
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>
        <asp:Literal ID="ltrMsg" runat="server"></asp:Literal>
        <div class="col-md-12">
            <div class="panel-group" id="accordion">
                <div class="col-md-3 holiday-sedebar-bg p-30">

                    <a data-toggle="collapse" data-parent="#accordion" href="#collapse1" class="btn btn-default btn-block">Holiday Details</a>


                    <a data-toggle="collapse" data-parent="#accordion" href="#collapse2" class="btn btn-default btn-block">Holiday Speical Offers</a>

                    <a data-toggle="collapse" data-parent="#accordion" href="#collapse3" class="btn btn-default btn-block">Holiday Images</a>
                </div>
                <div class="col-md-9">
                    <div class="panel panel-default" style="margin-bottom: 30px;" id="pnlHDetails">
                        <div class="panel-heading">
                            <h4 class="panel-title">
                                <a data-toggle="collapse" data-parent="#accordion" href="#collapse1">Holiday Details</a>
                            </h4>
                        </div>
                        <div id="collapse1" class="panel-collapse collapse out">
                            <div class="panel-body">

                                <div class="col-md-12">

                                    <div class="row mb-10">

                                        <div class="col-md-3 holiday-sedebar-bg">Destination Name</div>
                                        <div class="col-md-9">
                                            <asp:TextBox class="form-control mb-10" ID="txtDestCode" MaxLength="3" runat="server" placeholder="Destination Code" />
                                        </div>
                                    </div>
                                    <div class="row mb-10">

                                        <div class="col-md-3 holiday-sedebar-bg">Hotel Name</div>
                                        <div class="col-md-9">
                                            <asp:TextBox class="form-control mb-10" ID="txtHotelName" runat="server" placeholder="Hotel Name" />
                                        </div>
                                    </div>
                                    <div class="row mb-10">

                                        <div class="col-md-3 holiday-sedebar-bg">Star Rating</div>
                                        <div class="col-md-9">
                                            <asp:TextBox ID="txtRating" MaxLength="1" onkeyup="AllowNumeric(this)" class="form-control mb-10" runat="server" placeholder="Star Rating" />
                                        </div>
                                    </div>
                                    <div class="row mb-10">

                                        <div class="col-md-3 holiday-sedebar-bg">Holiday Type</div>
                                        <div class="col-md-9">
                                            <asp:DropDownList ID="ddlOfferTypes" runat="server" class="form-control mb-10">
                                                <asp:ListItem Value="">Select Offer Type</asp:ListItem>
                                                <asp:ListItem Value="Recommended">Recommended</asp:ListItem>
                                                <asp:ListItem Value="Sponsored">Sponsored</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="row mb-10">

                                        <div class="col-md-3 holiday-sedebar-bg">Category</div>
                                        <div class="col-md-9">
                                           
                                            <asp:CheckBoxList ID="chkCategory" runat="server" Width="100%" RepeatDirection="Horizontal" RepeatColumns="4">
                                                <asp:ListItem>Beach Holidays</asp:ListItem>
                                                <asp:ListItem>Family Holidays</asp:ListItem>
                                                <asp:ListItem>Weddings</asp:ListItem>
                                                <asp:ListItem>Luxury Holidays</asp:ListItem>
                                                <asp:ListItem>Honeymoons</asp:ListItem>
                                                <asp:ListItem>All Inclusive Holidays</asp:ListItem>
                                                <asp:ListItem>Worldwide Tours</asp:ListItem>
                                                <asp:ListItem>Multi Center Holidays</asp:ListItem>
                                                   <asp:ListItem Value="Hotel">Hotel</asp:ListItem>
                                            </asp:CheckBoxList>
                                        </div>
                                    </div>
                                    <div class="row mb-10">

                                        <div class="col-md-3 holiday-sedebar-bg">Nights</div>
                                        <div class="col-md-9">
                                            <asp:TextBox ID="txtNoofNights" MaxLength="2" class="form-control mb-10" onkeyup="AllowNumeric(this)" runat="server" placeholder="No of Nights" />
                                        </div>
                                    </div>
                                    <div class="row mb-10">

                                        <div class="col-md-3 holiday-sedebar-bg">Price</div>
                                        <div class="col-md-9">
                                            <asp:TextBox class="form-control mb-10" ID="txtPrice" onkeyup="AllowDecimal(this)" runat="server" placeholder="Price" />
                                        </div>
                                    </div>
                                    <div class="row mb-10">

                                        <div class="col-md-3 holiday-sedebar-bg">Board Basic</div>
                                        <div class="col-md-9">
                                            <asp:DropDownList ID="ddlBasicBoard" runat="server" class="form-control mb-10">
                                                <asp:ListItem Value="">Select Board Basis</asp:ListItem>
                                                <asp:ListItem Value="Bed And Breakfast">Bed And Breakfast</asp:ListItem>
                                                <asp:ListItem Value="Half Board">Half Board</asp:ListItem>
                                                <asp:ListItem Value="Full Board">Full Board</asp:ListItem>
                                                <asp:ListItem Value="Room Only">Room Only</asp:ListItem>
                                                <asp:ListItem Value="All Inclusive">All Inclusive</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="row mb-10">

                                        <div class="col-md-3 holiday-sedebar-bg">Discount</div>
                                        <div class="col-md-9">
                                            <asp:TextBox class="form-control mb-10" ID="txtDiscount" onkeyup="AllowNumeric(this)" runat="server" placeholder="Discount" />
                                        </div>
                                    </div>

                                    <div class="row mb-10">

                                        <div class="col-md-3 holiday-sedebar-bg">Company</div>
                                        <div class="col-md-9">
                                             <asp:DropDownList ID="ddlCompany" class="form-control mb-5" Width="160px" runat="server" >
                            </asp:DropDownList> 
                                        </div>
                                    </div>

                                    <div class="row mb-10">

                                        <div class="col-md-3 holiday-sedebar-bg">Overview</div>
                                        <div class="col-md-9">
                                            <asp:TextBox CssClass="textEditor1"  ID="txtOverviews" TextMode="MultiLine" runat="server" placeholder="hotel overviews" />
                                        </div>
                                    </div>
                                    <div class="row mb-10">

                                        <div class="col-md-3 holiday-sedebar-bg">Images</div>
                                        <div class="col-md-9">
                                            <asp:FileUpload ID="fpImage" runat="server" CssClass="btn btn-default btn-file form-control" />
                                            <asp:Literal ID="ltrImages" Text='<%# Eval("HLD_OFR_Main_Image") %>' runat="server"></asp:Literal>
                                            <img src='<%#  Eval("HLD_OFR_Main_Image") %>' class="img-responsive" style="width: 100%; height: 100px;" />

                                        </div>
                                    </div>
                                    <div class="row md-10">
                                        <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-danger btn-lg" OnClientClick="return validate();" Text="Save" OnClick="btnUpdate_Click"></asp:Button>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>

                    <div class="panel panel-default" style="margin-bottom: 30px;">
                        <div class="panel-heading">
                            <h4 class="panel-title">
                                <a data-toggle="collapse" data-parent="#accordion" href="#collapse2">Holiday Speical Offers</a>
                            </h4>
                        </div>
                        <div id="collapse2" class="panel-collapse collapse in">
                            <div class="panel-body">

                                <div class="mb-25">
                                    <div class="col-md-3">
                                        <asp:DropDownList ID="ddlOfferType" runat="server" class="form-control mb-10">
                                            <asp:ListItem Value="">Select Offer Type</asp:ListItem>
                                            <asp:ListItem Value="Included">Included</asp:ListItem>
                                            <asp:ListItem Value="Special Offer">Special Offer</asp:ListItem>
                                             <asp:ListItem Value="Validity">Validity</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:TextBox ID="txtOffer" class="form-control mb-10" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">

                                        <asp:FileUpload ID="fpIcon" runat="server" CssClass="btn btn-default btn-file form-control" />
                                    </div>
                                    <div class="col-md-1">
                                        <asp:CheckBox ID="chkMainPage" runat="server" Text="Is Main Page ?" TextAlign="Right" />
                                    </div>
                                    <div class="col-md-2">


                                        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-danger btn-lg" Text="Save" OnClick="btnSave_Click"></asp:Button>

                                    </div>
                                </div>


                                <div class="clearfix"></div>

                                <asp:Repeater ID="rptrHolidaysDetails" runat="server" OnItemCommand="rptrHolidaysDetails_ItemCommand">
                                    <HeaderTemplate>
                                        <div class="col-md-12 well">
                                            <div class="row">
                                                <div class="col-md-3">Offer Type</div>
                                                <div class="col-md-3">Offer</div>
                                                <div class="col-md-2">Is On Main Page </div>
                                                <div class="col-md-2">Icon</div>
                                                <div class="col-md-2"></div>
                                            </div>
                                        </div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="col-md-12">
                                            <div class="row">
                                                <div class="col-md-3"><%# Eval("HLD_SPL_OFR_Offer_Type") %></div>
                                                <div class="col-md-3"><%# Eval("HLD_SPL_OFR_Offer") %></div>
                                                <div class="col-md-2"><%# Eval("HLD_SPL_OFR_Show_On_Main_Page") %></div>
                                                <div class="col-md-2">
                                                    <img src='<%#  Eval("HLD_SPL_OFR_Icon") %>' class="img-responsive" style="width: 20px; height: 20px;" /></div>
                                                <div class="col-md-2">
                                                    <asp:Literal ID="ltrDID" Visible="false" Text='<%# Eval("HLD_SPL_OFR_Holiday_ID") %>' runat="server"></asp:Literal>
                                                    <asp:Button ID="btnODelete" CommandName="delete" runat="server" Text="Delete" CssClass="btn btn-danger" />
                                                </div>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>



                            </div>
                        </div>

                    </div>

                    <div class="panel panel-default" style="margin-bottom: 30px;">
                        <div class="panel-heading">
                            <h4 class="panel-title">
                                <a data-toggle="collapse" data-parent="#accordion" href="#collapse3">Holiday Images</a>
                            </h4>
                        </div>
                        <div id="collapse3" class="panel-collapse collapse in">
                            <div class="panel-body">
                                <div class="mb-25">
                                    <div class="col-md-4">
                                        <asp:TextBox class="form-control mb-10" ID="txtImageSec" runat="server" placeholder="Image Sequence" />
                                    </div>

                                    <div class="col-md-4">

                                        <asp:FileUpload ID="fupImages" runat="server" CssClass="btn btn-default btn-file form-control" />
                                    </div>

                                    <div class="col-md-4">
                                        <asp:Button ID="btnImages" runat="server" CssClass="btn btn-danger btn-lg" Text="Save" OnClick="btnImages_Click"></asp:Button>

                                    </div>

                                </div>
                                <div class="clearfix"></div>
                                <asp:Repeater ID="rptrHolidayImages" runat="server" OnItemCommand="rptrHolidayImages_ItemCommand">
                                    <HeaderTemplate>
                                        <div class="col-md-12 well">
                                            <div class="row">
                                                <div class="col-md-4">Images Sequence</div>
                                                <div class="col-md-4">Image Name</div>
                                                <div class="col-md-4"></div>
                                            </div>
                                        </div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="col-md-12">
                                            <div class="row">
                                                <div class="col-md-4"><%# Eval("HLD_OFR_IMG_Image_Sequence") %></div>
                                                <div class="col-md-4"><%# Eval("HLD_OFR_IMG_Image_URL") %></div>
                                                <div class="col-md-4">
                                                    <asp:Literal ID="ltrDIDImage" Visible="false" Text='<%# Eval("HLD_OFR_IMG_Holiday_ID") %>' runat="server"></asp:Literal>
                                                    <asp:Button ID="btnImageDelete" CommandName="delete" runat="server" Text="Delete" CssClass="btn btn-danger" /></div>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>

                    </div>
                </div>
            </div>

        </div>


        <div class="clearfix"></div>
    </section>
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

    
    <script lang="javascript" type="text/javascript">
        $('.textEditor1').jqte();
</script>
</asp:Content>

