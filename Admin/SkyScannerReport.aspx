<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="SkyScannerReport.aspx.cs" Inherits="Admin_Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%--<link href="../css/StyleSheet.css" rel="stylesheet" />--%>
    <link href="../../css/StyleSheet.css" rel="stylesheet" />
    <script src="https://www.traveljunction.co.uk/temp/CalendarAnyYear.js"></script>
 <%--   <script src="//netdna.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>   --%>
    <script src="../js/bootstrap-waitingfor.js"></script>
    <style type="text/css">
        .exelcss input[type=text], textarea {
            border: 1px solid #ccc !important;
            text-transform: uppercase;
        }

            .exelcss input[type=text]:focus {
                border: 1px solid #ff0000 !important;
            }
    </style>
        <script type="text/javascript">
            function checkAll(cb) {
                var ctrls = document.getElementsByTagName('input');
                for (var i = 0; i < ctrls.length; i++) {
                    var cbox = ctrls[i];
                    if (cbox.type == "checkbox") {
                        cbox.checked = cb.checked;
                    }
                }
            }
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
    <style> table,
      td,
      th {
      padding: 5px!important;
        border: 1px solid #333;
        text-align: center!important;
        border-bottom: 1px solid #333!important;
       
      }
        
         .enblecheckbox
            {
                display: inline-block;
    /* width: 100%; */
    background-color: #4daf48;
    color: #fff;
    padding: 5px 20px 2px 20px;
    /* display: block; */
    border-radius: 5px;
    margin-top: -37px;
    cursor: pointer;
            }
         .form-control
         {
             margin-bottom:20px;
         }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
         
        <div style="height: 15px;"></div>
        <div class="panel panel-default">
            <div class="panel-heading">Markup Details</div>
            <div class="panel-body" style="line-height: 20px;">
                <div class="row">
                    <div class="col-md-3">
                        <label>From Date</label>

                        <asp:TextBox runat="server" ID="txtMarkupFrom" onclick="showCalender(this);"  AutoComplete="off" CssClass="form-control" placeholder="Date From" />
                    </div>
                    <div class="col-md-3">
                        <label>To Date</label>
                        <asp:TextBox runat="server" ID="txtMarkupTo" onclick="showCalender(this);"  AutoComplete="off" CssClass="form-control" placeholder="Date Till" />

                    </div>
               
                </div>
           
                <div class="row">
                   <div class="col-md-6">
                       </div>
                    <div class="col-md-3" style="margin-top: 9px;text-align:right">
                                        
                          
                                 
                    </div>
                 
                    <div class="col-md-3" style="text-align:right" >
                       
                            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-lg" OnClientClick="return SearchMarkup();" Text="Search" OnClick="btnSearch_Click" />
                    </div>
                </div>

               
                
         
                
                 <div class="row">
                    <div class="col-md-12">
                        <asp:Label ID="lblMsg" runat="server" EnableViewState="false" ForeColor="Red"></asp:Label>
                    </div>
                </div>

            </div>
        </div>
    </div>
    <div class="container">
        <div style="height: 15px;"></div>
        <div class="panel panel-default">
            <div class="panel-heading">Sky Scanner Report</div>
            <div class="panel-body" style="line-height: 34px; padding: 0px!important;font-size:10px">
                <asp:Repeater ID="rptrDetails" runat="server">
                    <HeaderTemplate>
                        <table width='100%' cellpadding='0' cellspacing='0' class='table' style='margin-bottom: 0px;'>
                            <tr>
                                
                                <td class='gdvh'>Sale Date</td>
                                <td class='gdvh'>SkyScanner Id</td>
                                <td class='gdvh'>Amount</td>
                                <td class='gdvh'>Currency</td>
                                <td class='gdvh'>Device</td>
                                <td class='gdvh'>Platform</td>
                                <td class='gdvh'>Market</td>
                                <td class='gdvh'>Cabin</td>
                                 <td class='gdvh'>Trip</td>
                                 <td class='gdvh'>TripType</td>
                                <td class='gdvh'>From</td>
                                <td class='gdvh'>To</td>
                                <td class='gdvh'>Flights</td>
                                <td class='gdvh'>Booking Id</td>
                                <td class='gdvh'>Status</td>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td class='gdvr'><%# Eval("BOK_DTL_Prod_Booking_Date_Time","{0:dd-MM-yyyy HH:mm}")%></td>
                            <td class='gdvr'><%# Eval("BOK_DTL_Source_Unique_id")%></td>
                          <td class='gdvr'><%# Eval("BOK_DTL_Prod_Total_Amount")%></td>
                            <td class='gdvr'><%# Eval("BOK_MST_Currency_Type")%></td>
                             <td class='gdvr'><%# Eval("BOK_MST_Device_Type")%></td>
                            <td class='gdvr'><%# Eval("Platform")%></td>
                             <td class='gdvr'><%# Eval("Market")%></td>
                             <td class='gdvr'><%# Eval("SEC_MST_CabinClass")%></td>
                             <td class='gdvr'><%# Eval("Trip")%></td>
                             <td class='gdvr'><%# Eval("Trip_Type")%></td>
                             <td class='gdvr'><%# Eval("To")%></td>
                             <td class='gdvr'><%# Eval("From")%></td>
                            <td class='gdvr'><%# Eval("Flights")%></td>
                            <td class='gdvr'><%# Eval("BOK_MST_Booking_ID")%></td>
                            <td class='gdvr'><%# Eval("BOK_DTL_Prod_Booking_Initial_Status")%></td>
                           
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
               
            </div>
            <div class="panel-footer">
                <asp:Button ID="btnExport1" runat="server" CssClass="btn btn-primary btn-lg" OnClick="btnExport1_Click" Visible="false" Text="Download" />
              
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hfUpdatedBy" runat="server" />
    <input id="setascurrdate" type="hidden" />
    <input id="hdeprdate" type="hidden" />
    <div id="fadebackground" style="margin-bottom: 20px;">
    </div>

    <div align="center" id="popProgressBar" style="display: none;" class="popup-product">
        <table width="100%" class="table" align="center" height="100%" bgcolor="#ffffff">
            <tr>
                <td class="popup-header">Please wait while we process your request...
                </td>
            </tr>
            <tr>
                <td align="center" valign="middle">
                    <img runat="server" src="~/images/wait.gif" id="ImageProgressbar" />
                </td>
            </tr>
            <tr>
                <td align="center" height="40" style="background-color: #ffffff; color: #B9B9B9; vertical-align: middle; text-align: center; font-size: 18px; font-family: Verdana;"></td>
            </tr>
        </table>
    </div>
    
    <script type="text/javascript">

       
        function SearchMarkup() {
            waitingDialog.show('Please Wait...');
            return true;
        }
     

        function DeleteMarkup(ID) {
            if (confirm("Are you sure delete this markup??")) {
                waitingDialog.show("Please wait...");
                $.ajax({
                    type: "POST",
                    url: "Markup.aspx/DeleteMarkup",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({ ID: ID }),
                    responseType: "json",
                    success: function (data) {
                        var jsdata = JSON.parse(data.d);
                       
                        if (data.d == "true") {
                            $("#tr" + ID).hide();
                        }
                        else
                            alert("Markup is not successfully deleted from database!!");
                    },
                    error: function (data) { popup('popProgressBar', 30, 30); }
                });
                waitingDialog.hide();
            }
        }

        function oblurUpdate(ID, UpdateField, OldValue) {
            var isUpdate = false;
            try {
                if (UpdateField == "Amount") {
                    if (isFloat($("#txt" + UpdateField + ID).val())) {
                        if (parseFloat($("#txt" + UpdateField + ID).val()) != parseFloat(OldValue)) {
                            isUpdate = true;
                        }
                        else {
                            isUpdate = false;
                        }
                    }
                    else {
                        $("#txt" + UpdateField + ID).focus();
                        return;
                    }
                }
                else {
                    if ($("#txt" + UpdateField + ID).val().toUpperCase() != OldValue.toUpperCase()) {
                        isUpdate = true;
                    }
                    else {
                        isUpdate = false;
                    }
                }


            }
            catch (error) {
                isUpdate = false;
            }
            if (isUpdate) {

                switch (UpdateField) {
                    case "MarkupFrom":
                        if ($("#txt" + UpdateField + ID).val().length != 3) {
                            $("#txt" + UpdateField + ID).focus();
                            alert("Markup from only three charector is allowed");
                            return;
                        }
                        break;
                    case "MarkupTo":
                        if ($("#txt" + UpdateField + ID).val().length != 3) {
                            $("#txt" + UpdateField + ID).focus();
                            alert("Markup to only three charector is allowed");
                            return;
                        }
                        break;
                    case "AirV":
                        if ($("#txt" + UpdateField + ID).val() != "ANY") {
                            if ($("#txt" + UpdateField + ID).val().length != 2) {
                                $("#txt" + UpdateField + ID).focus();
                                alert("Airline only two charector or 'ANY' is allowed");
                                return;
                            }
                        }
                        break;
                    case "Provider":
                        if ($("#txt" + UpdateField + ID).val().toUpperCase() != "1A" && $("#txt" + UpdateField + ID).val().toUpperCase() != "1P" && $("#txt" + UpdateField + ID).val().toUpperCase() != "1S" && $("#txt" + UpdateField + ID).val().toUpperCase() != "ANY") {
                            $("#txt" + UpdateField + ID).focus();
                            alert("Provider only '1A','1P','1S','ANY' is allowed");
                            return;
                        }
                        break;
                    case "Category":
                        if ($("#txt" + UpdateField + ID).val().toUpperCase() != "PREMIUM" && $("#txt" + UpdateField + ID).val().toUpperCase() != "ECONOMY" && $("#txt" + UpdateField + ID).val().toUpperCase() != "ANY" && $("#txt" + UpdateField + ID).val().toUpperCase() != "FIRSTCLASS" && $("#txt" + UpdateField + ID).val().toUpperCase() != "BUSINESS") {
                            $("#txt" + UpdateField + ID).focus();
                            alert("Category only 'PREMIUM','ECONOMY','FIRSTCLASS','BUSINESS','ANY' is allowed");
                            return;
                        }
                        break;
                    case "MarkupClass":
                        if ($("#txt" + UpdateField + ID).val().length != "1" && $("#txt" + UpdateField + ID).val().toUpperCase() != "ANY") {
                            alert("Class only one charector or 'ANY' is allowed");
                            $("#txt" + UpdateField + ID).focus();
                        }
                        break;
                    case "FareType":
                        if ($("#txt" + UpdateField + ID).val().toUpperCase() != "RA" && $("#txt" + UpdateField + ID).val().toUpperCase() != "RP" && $("#txt" + UpdateField + ID).val().toUpperCase() != "RT" && $("#txt" + UpdateField + ID).val().toUpperCase() != "ANY") {
                            $("#txt" + UpdateField + ID).focus();
                            alert("Fare type only 'RA','RP','RT','ANY' is allowed");
                            return;
                        }
                        break;
                    case "JourneyType":
                        if ($("#txt" + UpdateField + ID).val().toUpperCase() != "RT" && $("#txt" + UpdateField + ID).val().toUpperCase() != "O" && $("#txt" + UpdateField + ID).val().toUpperCase() != "ANY") {
                            $("#txt" + UpdateField + ID).focus();
                            alert("Journey type only 'RT','O','ANY' is allowed");
                            return;
                        }
                        break;
                    case "AmountType":
                        if ($("#txt" + UpdateField + ID).val().toUpperCase() != "A" && $("#txt" + UpdateField + ID).val().toUpperCase() != "P" && $("#txt" + UpdateField + ID).val().toUpperCase() != "ANY") {
                            $("#txt" + UpdateField + ID).focus();
                            alert("Amount type only 'A','P','ANY' is allowed");
                            return;
                        }
                        break;
                    //case "ValidFromDate":
                    //    if (isValidDate($("#txt" + UpdateField + ID).val())  && $("#txt" + UpdateField + ID).val() != "01/01/1900") {
                    //        $("#txt" + UpdateField + ID).focus();
                    //        alert("Enter Date only dd/mm/yyyy format");
                    //        return;
                    //    }
                    //    break;
                    //case "ValidToDate":
                    //    if (isValidDate($("#txt" + UpdateField + ID).val()) && $("#txt" + UpdateField + ID).val() != "01/01/2100") {
                    //        $("#txt" + UpdateField + ID).focus();
                    //        alert("Enter Date only dd/mm/yyyy format");
                    //        return;
                    //    }
                    //    break;
                }
                var Param = {
                    ID: ID,
                    UpdateField: UpdateField,
                    Value: $("#txt" + UpdateField + ID).val(),
                    UpdatedBy: $("#<%= hfUpdatedBy.ClientID%>").val()
                }
                $.ajax({
                    type: "POST",
                    url: "Markup.aspx/UpdateMarkupExcel",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify(Param),
                    responseType: "json",
                    success: function (data) {
                        if (data.d != "true") {
                            alert("Markup is not successfully Updated in database!!");
                        }
                    },
                    error: function (data) { }
                });
            }

        }
        function isFloat(value) {
            return value != "" && !isNaN(value);
        }
        function isValidDate(dtStr) {
           
            var daysInMonth = DaysArray(12);
            var aa = dtStr.split('/');
            if (dtStr.length == 10 && aa.length==3) {
               
                var strDay = aa[0];
                var strMonth = aa[1];
                var strYear = aa[2];

                if (strDay.charAt(0) == "0" && strDay.length > 1) strDay = strDay.substring(1);
                if (strMonth.charAt(0) == "0" && strMonth.length > 1) strMonth = strMonth.substring(1);
                for (var i = 1; i <= 3; i++) {
                    if (strYear.charAt(0) == "0" && strYear.length > 1) strYear = strYear.substring(1);
                }
                var month = parseInt(strMonth);
                var day = parseInt(strDay);
                var year = parseInt(strYear);

                if (strMonth.length < 1 || month < 1 || month > 12) {
                    return false;
                }
                if (strDay.length < 1 || day < 1 || day > 31 || (month == 2 && day > daysInFebruary(year)) || day > daysInMonth[month]) {
                    return false;
                }
                if (strYear.length != 4 || year == 0 || year < 1900 || year > 2100) {
                    return false;
                }
                return true;
            }
            else {
                return false;
            }
        }
        function DaysArray(n) {
            for (var i = 1; i <= n; i++) {
                this[i] = 31;
                if (i == 4 || i == 6 || i == 9 || i == 11) { this[i] = 30; }
                if (i == 2) { this[i] = 29; }
            }
            return this;
        }
        function daysInFebruary(year) {
            return (((year % 4 == 0) && ((!(year % 100 == 0)) || (year % 400 == 0))) ? 29 : 28);
        }
        
    </script>

    <script type="text/javascript">
        $(function () {
            $('input:text:first').focus();
            var $inp = $('input:text');
            $inp.bind('keydown', function (e) {
                //var key = (e.keyCode ? e.keyCode : e.charCode);
                var key = e.which;

                if (key == 40) {

                    if ($inp.index(this) > 6) {

                        var nxtIdx = $inp.index(this) + 9;

                        $(":input:text:eq(" + nxtIdx + ")").focus();
                        $(":input:text:eq(" + nxtIdx + ")").select();
                    }
                }
                else if (key == 38) {
                    if ($inp.index(this) > 6) {

                        var nxtIdx = $inp.index(this) - 9;
                        $(":input:text:eq(" + nxtIdx + ")").focus();
                        $(":input:text:eq(" + nxtIdx + ")").select();
                    }

                }
            });
        });
    </script>

    <script type="text/javascript" src="../../Scripts/Sswitch.js"></script>
    <script>
        $(document).ready(function () {
            $(".checkbox").Sswitch({
                onSwitchChange: function () {

                    var p = $('.checkbox').prop("checked");

                    var Param = {
                        UseStatus: p

                    }
                    $.ajax({
                        type: "POST",
                        url: "Markup.aspx/UpdateDiscountMarkup_UK",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data: JSON.stringify(Param),
                        responseType: "json",
                        success: function (data) {

                            if (data.d == "true") {
                                alert("Rules Set to " + p + "!");
                            }
                            else {
                                alert("Rules Not Updated!");
                            }
                        },
                        error: function (data) { }
                    });
                }
            });
        });
    </script>
</asp:Content>

