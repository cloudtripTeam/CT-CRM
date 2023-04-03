<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="OwnItineraryFT.aspx.cs" Inherits="Admin_Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%--<link href="../css/StyleSheet.css" rel="stylesheet" />--%>
    <link href="../../css/StyleSheet.css" rel="stylesheet" />
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script type="text/javascript">
        $(function () {
            var dates = $("#txtvalidFrom,#txtvalidTo").datepicker({
                defaultDate: "+0w",
                changeMonth: true,
                changeYear: true,
                numberOfMonths: 1,
                mindate: 0,
                dateFormat: "dd/mm/yy",

                onSelect: function (selectedDate) {
                    var option = this.id == "txtvalidFrom" ? "minDate" : "maxDate",
                        instance = $(this).data("datepicker"),
                        date = $.datepicker.parseDate(
                            instance.settings.dateFormat ||
                            $.datepicker._defaults.dateFormat,
                            selectedDate, instance.settings);
                    dates.not(this).datepicker("option", option, date);
                }
            });
        });
    </script>
    <script type="text/javascript" src="../../Scripts/Sswitch.js"></script>
    <script>
        $(document).ready(function () {
            $(".checkbox").Sswitch({
                onSwitchChange: function () {
                    var chkValue = $('.checkbox').prop("checked");

                    var from = $("#TxtFrom").val();
                    var to = $("#TxtTo").val();
                    var airline = $("#TxtAir").val();
                    var journey = $("#ddlJourneyType").val();
                    var campaign = $("#ddlCampaign").val();
                    var uniqueId = $("#txtUniqueId").val();


                    var Param =
                    {
                        UseStatus: chkValue,
                        from: from,
                        to: to,
                        airline: airline,
                        journey: journey,
                        campaign: campaign,
                        uniqueId: uniqueId
                    }

                    $.ajax({
                        type: "POST",
                        url: "OwnItineraryFT.aspx/UpdateUseStatusFT",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data: JSON.stringify(Param),
                        responseType: "json",
                        success: function (data) {
                            debugger
                            if (data.d == "true") {
                                alert("All Itinerary Set to " + chkValue + "!");
                            }
                            else {
                                alert("All Itinerary Not Updated!");
                            }
                        },
                        error: function (data) { }
                    });

                }
            });
        });
    </script>
    <script>

        $(document).ready(function () {

            $("#toggle").click(function () {
                $(".target").toggle('slow', function () {

                });
            });
            $("#Close").click(function () {
                $(".target").hide('slow');
            });
        });

    </script>
    <style>
        input {
            margin: 0 1px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <div style="height: 15px;"></div>
        <div class="panel panel-default">
            <div class="panel-heading">Own Itinerary Status <small style="color: greenyellow"><i>- Turn On/Off All Own Itinerary</i></small></div>
            <div class="panel-body">
                <input type="checkbox" name="checkboxisActive" class="checkbox" runat="server" id="checkboxisActive" clientidmode="Static" />
            </div>
        </div>
        <div class="panel panel-default">


            <div class="panel-heading">Search Own Itinerary</div>
            <div class="panel-body" style="line-height: 34px;">
                <div class="row">
                    <div class="col-md-2">
                        <label>From</label>
                        <asp:TextBox ID="TxtFrom" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <label>To</label>
                        <asp:TextBox ID="TxtTo" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <label>Airline</label>
                        <asp:TextBox ID="TxtAir" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <label>Journey</label>
                        <asp:DropDownList ID="ddlJourneyType" runat="server" CssClass="form-control" ClientIDMode="Static">
                            <asp:ListItem Value="1">Return</asp:ListItem>
                            <asp:ListItem Value="0">One Way</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2">
                        <label>Valid From</label>
                        <asp:TextBox ID="txtvalidFrom" AutoCompleteType="Disabled" runat="server" onclick="showCalender(this.id);" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <label>Valid To</label>
                        <asp:TextBox ID="txtvalidTo" AutoCompleteType="Disabled" runat="server" onclick="showCalender(this.id);" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <label>Campaign ID</label>

                        <asp:DropDownList ID="ddlCampaign" runat="server" CssClass="form-control" ClientIDMode="Static">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2">
                        <label>Unique ID</label>
                        <asp:TextBox ID="txtUniqueId" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>

                    </div>
                    <div class="col-md-2">
                        <label style="display: block">&nbsp;</label>
                        <asp:Button ID="btnSearch" runat="server" class="btn btn-primary btn-lg" Text="Search" OnClick="btnSearch_Click" />
                    </div>
                </div>




                <div class="row">
                    <div class="col-md-12">
                        <asp:Label ID="lblMsg" runat="server" ForeColor="Red" EnableViewState="false"></asp:Label>
                    </div>
                </div>

            </div>

            <br />
            <div class="panel panel-default">
                <div class="panel-heading">
                    <input id="toggle" value="Show Logic" type="button" class="btn btn-primary btn-xs" />
                </div>
                <div class="panel-body target" style="display: none; background: #333; line-height: 22px">
                    <input id="Close" value="&#10005;" type="button" class="btn btn-primary btn-xs" style="float: right" />
                    <div class="row" style="">
                        <p style="color: greenyellow">
                            <span>Add Itinerary logic:</span><br />
                            <small><i>1. Departure date should be between vFrom and vTill</i></small><br />
                            <small><i>2. DayCount: If arrival time is on next day, put 1 else 0 for same day arrival.</i></small><br />
                            <small><i>3. Bag: If no baggage put 0 or else number of pieces. i.e 1 or 2.</i></small><br />
                            <small><i>4. OptCode: If operating Carrier is applicable, put Airline Code here. </i></small>
                            <br />
                            <small><i>5. IsRet: False in outbound and True in inbound itinerary.</i></small><br />
                            <small><i>6. Status is always false if fresh itinerary is being feed.</i></small><br />
                            <small><i>7. S Id is the serial number in order in which the flights will be displayed.</i></small><br />
                            <br />
                            <span>Few Tips:</span><br />
                            <small><i>1. Few Columns are common, Just change the values in the last leg of itinerary and all of these columns will get updated.</i></small><br />

                            <small><i>Air, ADT Base Fare, ADT Tax Fare, Origin, Dest, Camp ID, Offer Type, vFrom, vTill, Status</i></small><br />

                            <small><i>2.  Air: Its the column which will be shown as airline name in Comparision sites.</i></small><br />
                            <small><i>3. Use Copy Row button in the extreme right to copy the legs value, no need to feed same common data again.</i></small><br />
                            <small><i>4. Make sure to check flight timings format and values if not showing on comparision sites.</i></small><br />

                            <small><i>5. Please check airport code in our search engine before entering into the table.</i></small><br />

                        </p>


                    </div>
                </div>
            </div>

            <div class="panel panel-default" id="pnlbulkfare" runat="server">
                <div class="panel-heading">
                    <div class="pull-left">
                        <h4 class="panel-title">
                            <a data-toggle="collapse" href="#bulkUpdate" class="btn btn-primary btn-xs" aria-expanded="true">Bulk Update Itinerary</a>
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
                                <asp:Button ID="uploadcsvbtn" runat="server" CssClass="btn btn-success btn-sm" OnClientClick="return SearchCall();" Text="Upload Itinerary Details" OnClick="uploadcsvbtn_Click" />
                                <br />
                                <%-- Note Excel Sheet name Must be "Sheet 1"--%>
                            </div>
                            <div class="col-md-2 col-md-offset-3">
                                <asp:Button ID="btnExport" runat="server" CssClass="btn btn-primary btn-sm" Text="Export All Itineraries" OnClick="btnExport_Click" />
                            </div>
                            <div class="col-md-2">
                                <asp:Button ID="btnDeleteAll" CssClass="btn btn-danger btn-sm" runat="server" OnClientClick="return confirm('Are you sure delete all Itineraries?')" Text="Delete All Itineraries"
                                    OnClick="btnDeleteAll_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>


        </div>
    </div>
    <style>
        td {
            /*word-break: break-word;*/
        }

        th {
            /*word-break: break-word;*/
            text-align: center;
        }

        input {
            margin: 0 2px;
        }
    </style>
    <div class="">
        <div style="height: 15px;"></div>
        <div class="panel panel-default">
            <div class="panel-heading">Flight Details</div>
            <div class="panel-body scrolling-wrapper" style="padding: 0px!important; text-align: center!important; overflow-x: scroll;">
                <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="View1" runat="server">
                        <asp:GridView ID="gvDetails2" BorderColor="black" runat="server" AutoGenerateColumns="false" DataKeyNames="Id" CssClass="Gridview" HeaderStyle-BackColor="#61A6F8"
                            ShowFooter="true" HeaderStyle-Font-Bold="true" OnRowDataBound="gvDetails_RowBound" OnRowCommand="gvDetails2_RowCommand" OnRowDeleting="gvDetails2_RowDeleting" HeaderStyle-HorizontalAlign="Center" ForeColor="black" HeaderStyle-ForeColor="White" RowStyle-HorizontalAlign="Center" EditRowStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center" Style="font-size: 10px;" Width="100%">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="true" OnCheckedChanged="OnCheckedChanged" ClientIDMode="Static" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="true" OnCheckedChanged="OnCheckedChanged" />
                                        <asp:ImageButton ID="imgbtnDelete" CommandName="Delete" runat="server" ImageUrl="~/Images/delete_new.png" ToolTip="Delete PAX" Height="20px" Width="20px" OnClientClick="return confirm('Are you sure you want to delete this Row?');" />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:ImageButton ID="imgbtnAdd" runat="server" CssClass="footer_add_img" ImageUrl="~/Images/add_new.png?ver=1.1" CommandName="AddNew" Width="50px" Height="20px" ToolTip="Add new PAX" ValidationGroup="validaiton2" />

                                    </FooterTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Air">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_AirlineId" runat="server" Text='<%# Eval("ValCarrier") %>' CssClass="lbl_item_data"></asp:Label>
                                        <asp:TextBox ID="txt_AirlineId" Width="20px" runat="server" Text='<%# Eval("ValCarrier") %>' Visible="false"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtftr_AirlineId" runat="server" Width="25px" />
                                    </FooterTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="JT">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_JourneyType" runat="server" Text='<%# Eval("JourneyType") %>' CssClass="lbl_item_data"></asp:Label>
                                        <asp:TextBox ID="txt_JourneyType" Width="22px" runat="server" Text='<%# Eval("JourneyType") %>' Visible="false"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>

                                        <asp:TextBox ID="txtftr_JourneyType" runat="server" Width="22px" />

                                    </FooterTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="U Id">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_UniqueId" runat="server" Text='<%# Eval("UniqueId") %>' CssClass="lbl_item_data"></asp:Label>
                                        <asp:TextBox ID="txt_UniqueId" Width="30px" runat="server" Text='<%# Eval("UniqueId") %>' Visible="false"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>

                                        <asp:TextBox ID="txtftr_UniqueId" runat="server" Width="30px" />

                                    </FooterTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="S Id">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_SerialId" runat="server" Text='<%# Eval("SerialId") %>' CssClass="lbl_item_data"></asp:Label>
                                        <asp:TextBox ID="txt_SerialId" Width="20px" runat="server" Text='<%# Eval("SerialId") %>' Visible="false"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtftr_SerialId" runat="server" Width="20px" />
                                    </FooterTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="AirV">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_AirV" runat="server" Text='<%# Eval("AirV") %>' CssClass="lbl_item_data"></asp:Label>
                                        <asp:TextBox ID="txt_AirV" Width="25px" runat="server" Text='<%# Eval("AirV") %>' Visible="false"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtftr_AirV" runat="server" Width="25px" />
                                    </FooterTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Flt No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_FltNum" runat="server" Text='<%# Eval("FltNum") %>' CssClass="lbl_item_data"></asp:Label>
                                        <asp:TextBox ID="txt_FltNum" Width="30px" runat="server" Text='<%# Eval("FltNum") %>' Visible="false"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtftr_FltNum" runat="server" Width="30px" />
                                    </FooterTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="D Arpt Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_D_AirportCode" runat="server" Text='<%# Eval("D_AirportCode") %>' CssClass="lbl_item_data"></asp:Label>
                                        <asp:TextBox ID="txt_D_AirportCode" Width="28px" runat="server" Text='<%# Eval("D_AirportCode") %>' Visible="false"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtftr_D_AirportCode" runat="server" Width="28px" />
                                    </FooterTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="D Cty Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_D_AirportCityCode" runat="server" Text='<%# Eval("D_AirportCityCode") %>' CssClass="lbl_item_data"></asp:Label>
                                        <asp:TextBox ID="txt_D_AirportCityCode" Width="28px" runat="server" Text='<%# Eval("D_AirportCityCode") %>' Visible="false"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtftr_D_AirportCityCode" runat="server" Width="28px" />
                                    </FooterTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Dep Term">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_D_Terminal" runat="server" Text='<%# Eval("D_Terminal") %>' CssClass="lbl_item_data"></asp:Label>
                                        <asp:TextBox ID="txt_D_Terminal" Width="25px" runat="server" Text='<%# Eval("D_Terminal") %>' Visible="false"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtftr_D_Terminal" runat="server" Width="25px" />
                                    </FooterTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="D Cntry Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_D_AirportCountryCode" runat="server" Text='<%# Eval("D_AirportCountryCode") %>' CssClass="lbl_item_data"></asp:Label>
                                        <asp:TextBox ID="txt_D_AirportCountryCode" Width="25px" runat="server" Text='<%# Eval("D_AirportCountryCode") %>' Visible="false"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtftr_D_AirportCountryCode" runat="server" Width="25px" />
                                    </FooterTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Dep Time">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_D_Time" runat="server" Text='<%# Eval("D_Time") %>' CssClass="lbl_item_data"></asp:Label>
                                        <asp:TextBox ID="txt_D_Time" Width="40px" runat="server" Text='<%# Eval("D_Time") %>' Visible="false"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtftr_D_Time" runat="server" Width="30px" />
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dep Time Stamp">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_D_DateTimeStamp" runat="server" Text='<%# Eval("D_DateTimeStamp") %>' CssClass="lbl_item_data"></asp:Label>
                                        <asp:TextBox ID="txt_D_DateTimeStamp" Width="40px" runat="server" Text='<%# Eval("D_DateTimeStamp") %>' Visible="false"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtftr_D_DateTimeStamp" runat="server" Width="30px" />
                                    </FooterTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="A Arpt Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_A_AirportCode" runat="server" Text='<%# Eval("A_AirportCode") %>' CssClass="lbl_item_data"></asp:Label>
                                        <asp:TextBox ID="txt_A_AirportCode" Width="28px" runat="server" Text='<%# Eval("A_AirportCode") %>' Visible="false"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtftr_A_AirportCode" runat="server" Width="28px" />
                                    </FooterTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="A Cty Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_A_AirportCityCode" runat="server" Text='<%# Eval("A_AirportCityCode") %>' CssClass="lbl_item_data"></asp:Label>
                                        <asp:TextBox ID="txt_A_AirportCityCode" Width="28px" runat="server" Text='<%# Eval("A_AirportCityCode") %>' Visible="false"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtftr_A_AirportCityCode" runat="server" Width="28px" />
                                    </FooterTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="A Cnty Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_A_AirportCountryCode" runat="server" Text='<%# Eval("A_AirportCountryCode") %>' CssClass="lbl_item_data"></asp:Label>
                                        <asp:TextBox ID="txt_A_AirportCountryCode" Width="25px" runat="server" Text='<%# Eval("A_AirportCountryCode") %>' Visible="false"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtftr_A_AirportCountryCode" runat="server" Width="25px" />
                                    </FooterTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Arr Term">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_A_Terminal" runat="server" Text='<%# Eval("A_Terminal") %>' CssClass="lbl_item_data"></asp:Label>
                                        <asp:TextBox ID="txt_A_Terminal" Width="25px" runat="server" Text='<%# Eval("A_Terminal") %>' Visible="false"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtftr_A_Terminal" runat="server" Width="25px" />
                                    </FooterTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Arr Time">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_A_Time" runat="server" Text='<%# Eval("A_Time") %>' CssClass="lbl_item_data"></asp:Label>
                                        <asp:TextBox ID="txt_A_Time" Width="40px" runat="server" Text='<%# Eval("A_Time") %>' Visible="false"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtftr_A_Time" runat="server" Width="30px" />
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="A Time Stamp">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_A_DateTimeStamp" runat="server" Text='<%# Eval("A_DateTimeStamp") %>' CssClass="lbl_item_data"></asp:Label>
                                        <asp:TextBox ID="txt_A_DateTimeStamp" Width="40px" runat="server" Text='<%# Eval("A_DateTimeStamp") %>' Visible="false"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtftr_A_DateTimeStamp" runat="server" Width="30px" />
                                    </FooterTemplate>
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Equip Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_EquipType" runat="server" Text='<%# Eval("EquipType") %>' CssClass="lbl_item_data"></asp:Label>
                                        <asp:TextBox ID="txt_EquipType" Width="30px" runat="server" Text='<%# Eval("EquipType") %>' Visible="false"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtftr_EquipType" runat="server" Width="30px" />
                                    </FooterTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Day Cou">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_IsNextDayCount" runat="server" Text='<%# Eval("IsNextDayCount") %>' CssClass="lbl_item_data"></asp:Label>
                                        <asp:TextBox ID="txt_IsNextDayCount" Width="20px" runat="server" Text='<%# Eval("IsNextDayCount") %>' Visible="false"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtftr_IsNextDayCount" runat="server" Width="20px" />
                                    </FooterTemplate>
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="IsRet">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_IsReturn" runat="server" Text='<%# Eval("IsReturn") %>' CssClass="lbl_item_data"></asp:Label>
                                        <asp:TextBox ID="txt_IsReturn" Width="33px" runat="server" Text='<%# Eval("IsReturn") %>' Visible="false"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtftr_IsReturn" runat="server" Width="33px" />
                                    </FooterTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Bag">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_BaggageInfo" runat="server" Text='<%# Eval("BaggageInfo") %>' CssClass="lbl_item_data"></asp:Label>
                                        <asp:TextBox ID="txt_BaggageInfo" Width="20px" runat="server" Text='<%# Eval("BaggageInfo") %>' Visible="false"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtftr_BaggageInfo" runat="server" Width="20px" />
                                    </FooterTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="ADT Base Fare">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_Pax_A_BaseFare" runat="server" Text='<%# Eval("Pax_A_BaseFare") %>' CssClass="lbl_item_data"></asp:Label>
                                        <asp:TextBox ID="txt_Pax_A_BaseFare" Width="35px" runat="server" Text='<%# Eval("Pax_A_BaseFare") %>' Visible="false"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtftr_Pax_A_BaseFare" runat="server" Width="35px" />
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ADT Tax Fare">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_Pax_A_Taxes" runat="server" Text='<%# Eval("Pax_A_Taxes") %>' CssClass="lbl_item_data"></asp:Label>
                                        <asp:TextBox ID="txt_Pax_A_Taxes" Width="35px" runat="server" Text='<%# Eval("Pax_A_Taxes") %>' Visible="false"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtftr_Pax_A_Taxes" runat="server" Width="35px" />
                                    </FooterTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Origin">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_Origin" runat="server" Text='<%# Eval("Origin") %>' CssClass="lbl_item_data"></asp:Label>
                                        <asp:TextBox ID="txt_Origin" Width="28px" runat="server" Text='<%# Eval("Origin") %>' Visible="false"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtftr_Origin" runat="server" Width="28px" />
                                    </FooterTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Dest">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_Destination" runat="server" Text='<%# Eval("Destination") %>' CssClass="lbl_item_data"></asp:Label>
                                        <asp:TextBox ID="txt_Destination" Width="28px" runat="server" Text='<%# Eval("Destination") %>' Visible="false"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtftr_Destination" runat="server" Width="28px" />
                                    </FooterTemplate>
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Camp ID">
                                    <ItemTemplate>



                                        <asp:Label ID="lbl_Camp_ID" runat="server" CssClass="lbl_item_data" Text='<%# Eval("Camp_ID") %>'></asp:Label>


                                        <asp:TextBox ID="txt_Camp_ID" Width="40px" runat="server" Text='<%# Eval("Camp_ID") %>' Visible="false"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtftr_Camp_ID" runat="server" Width="50px" />
                                    </FooterTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Offer Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_OfferType" runat="server" Text='<%# Eval("OfferType") %>' CssClass="lbl_item_data"></asp:Label>
                                        <asp:TextBox ID="txt_OfferType" Width="44px" runat="server" Text='<%# Eval("OfferType") %>' Visible="false"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtftr_OfferType" runat="server" Width="44px" />
                                    </FooterTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Opt Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_OptCode" runat="server" Text='<%# Eval("OperatingCarrierCode") %>' CssClass="lbl_item_data"></asp:Label>
                                        <asp:TextBox ID="txt_OptCode" Width="28px" runat="server" Text='<%# Eval("OperatingCarrierCode") %>' Visible="false"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtftr_OptCode" runat="server" Width="28px" />
                                    </FooterTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="vFrom">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_vFrom" runat="server" Text='<%# Eval("ValidFrom").ToString().Substring(0,10) %>' CssClass="lbl_item_data"></asp:Label>
                                        <asp:TextBox ID="txt_vFrom" Width="60px" runat="server" Text='<%# Eval("ValidFrom").ToString().Substring(0,10) %>' Visible="false"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtftr_vFrom" runat="server" Width="60px" />
                                    </FooterTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="vTill">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_vTo" runat="server" Text='<%# Eval("ValidTo").ToString().Substring(0,10) %>' CssClass="lbl_item_data"></asp:Label>
                                        <asp:TextBox ID="txt_vTo" Width="60px" runat="server" Text='<%# Eval("ValidTo").ToString().Substring(0,10) %>' Visible="false"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtftr_vTo" runat="server" Width="60px" />
                                    </FooterTemplate>
                                </asp:TemplateField>





                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_Status" runat="server" Text='<%# Eval("Status") %>' ToolTip='<%# Eval("ModifiedBy") %>' CssClass="lbl_item_data"></asp:Label>
                                        <asp:TextBox ID="txt_Status" Width="33px" runat="server" Text='<%# Eval("Status") %>' Visible="false"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtftr_Status" runat="server" Width="30px" />
                                    </FooterTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Crncy">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_Crncy" runat="server" Text='<%# Eval("Currency") %>' CssClass="lbl_item_data"></asp:Label>
                                        <asp:TextBox ID="txt_Crncy" Width="33px" runat="server" Text='<%# Eval("Currency") %>' Visible="false"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtftr_Crncy" runat="server" Width="30px" />
                                    </FooterTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="" ItemStyle-Width="20px">
                                    <EditItemTemplate>
                                    </EditItemTemplate>


                                    <ItemTemplate>

                                        <asp:LinkButton runat="server" ToolTip='<%#Eval("Id") %>' ID="linkbtn" OnClick="copy_row" Style="color: #188fff;"><img src="../images/copy.png" width="15" /></asp:LinkButton>

                                    </ItemTemplate>
                                    <FooterTemplate>

                                        <asp:Label ID="lbl_RowCopy" runat="server" Width="30px" CssClass="lbl_item_data"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>


                    </asp:View>
                    <asp:View ID="View2" runat="server">
                        <asp:Label Text="No Record Found!" runat="server" ID="lbltxtMsg" Style="margin-top: 14px; font-size: 17px; color: red;" />
                    </asp:View>
                </asp:MultiView>
            </div>

            <asp:Label ID="lblresult_psg" runat="server" Visible="false"></asp:Label>

            <asp:Button ID="btnUpdate" runat="server" Text="UPDATE" OnClick="Update" Visible="false" Style="color: White; padding: 5px 51px; background: #1d4b88; border-radius: 5px; margin-top: 10px; box-shadow: inset 0px 0px 10px 5px rgba(0,0,0,.18);" />

            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="lblMessage" runat="server" EnableViewState="false" ForeColor="Red"></asp:Label>
                </div>
            </div>
        </div>
    </div>



</asp:Content>

