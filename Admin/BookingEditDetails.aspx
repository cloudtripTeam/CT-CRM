<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="BookingEditDetails.aspx.cs" Inherits="Admin_BookingEditDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
   <%-- <link href="../css/StyleSheet.css" rel="stylesheet" />--%>
    <link href="../../css/StyleSheet.css" rel="stylesheet" />
    
    <link rel="stylesheet" href="../css/jquery-ui.css" />
    <script src="../js/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../js/jquery-ui.js"></script>

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
        var ReqData = null;
        var GeneralDetails = null;
        var PaxDetails = null;
        var SectorDetails = null;
        var ChargesDetails = null;
        var TransactionDetails = null;
        window.onload = function (e) {
            ReqData = { BookingID: $("#<%=hfBookingID.ClientID%>").val(), ProdID: $("#<%=hfProdID.ClientID%>").val() }
            ShowHideDestTab('1');
            GetPassengerDetails('2');
            GetSectorDetails('3');
            GetChargeDetails('4');
            GetTrnsDetails('5');

        }
        function ShowHideDestTab(ID) {
            for (var i = 1; i <= 5; i++) {
                $("#divFare" + i).hide();
                document.getElementById("li" + i).className = '';
            }
            $("#divFare" + ID).show();
            document.getElementById("li" + ID).className = 'first-li12';
            if ($("#divFare" + ID).html() == "") {
                switch (ID) {
                    case "1":
                        GetBookingSummary(ID);
                        break;
                    case "2":
                        GetPassengerDetails(ID);
                        break;
                    case "3":
                        GetSectorDetails(ID);
                        break;
                    case "4":
                        GetChargeDetails(ID);
                        break;
                    case "5":
                        GetTrnsDetails(ID);
                        break;
                    default: break;
                }
            }
        }
        function GetBookingSummary(ID) {
            $.ajax({
                type: "POST",
                url: "BookingEditDetails.aspx/GetBookingSummary",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify(ReqData),
                responseType: "json",
                success: function (data) {
                    var jsdata = JSON.parse(data.d);
                    GeneralDetails = jsdata;
                    var strHtml = "<table width='100%' class='table' cellspacing='0' border='0' cellpadding='0'>" +
                                "<tr><td><b> Booking ID</b></td><td>" + jsdata.BM.BookingID + "</td>" +
                                "<td><b>Currencey</b></td><td>" + jsdata.BM.CurrencyType + "</td></tr>" +
                                "<tr><td><b>PNR</b></td><td>" + jsdata.BD.PNR + "</td>" +
                                "<td><b>ProductType</b></td><td>" + jsdata.BD.ProductType + "</td></tr>" +
                                "<tr><td><b>Booking Status</b></td><td>" + jsdata.BD.BookingStatus + "</td>" +
                                "<td><b>valCarrier</b></td><td>" + jsdata.SM.ValCarrier + "</td></tr>" +
                                "<tr><td><b>Provider</b></td><td>" + jsdata.BD.Provider + "</td>" +
                                "<td><b>Cabin Class</b></td><td>" + jsdata.SM.CabinClass + "</td></tr>" +
                                "<tr><td><b>Origin</b></td><td>" + jsdata.SM.Origin + "</td>" +
                                "<td><b>Company</b></td><td>" + jsdata.BM.BookingByCompany + "</td></tr>" +
                                "<tr><td><b>Destination</b></td><td>" + jsdata.SM.Destination + "</td>" +
                                "<td><b>Source Media</b></td><td>" + jsdata.BD.SourceMedia + "</td></tr>" +
                                "<tr><td><b>Trip Type</b></td><td>" + jsdata.SM.JType + "</td>" +
                                "<td><b>Email ID</b></td><td>" + "xxxxx@xxx.xx" + "</td></tr>" +
                                "<tr><td><b>Booking Type</b></td><td>" + jsdata.BD.BookingByType + "</td>" +
                                "<td><b>Phone No</b></td><td>" + "xxxxxxxxxx" + "</td></tr>" +
                                "<tr><td><b>Last Ticking Date</b></td><td>" + jsdata.SM.LastTktDate + "</td>" +
                                "<td><b>Mobile No</b></td><td>" + "xxxxxxxxxx" + "</td></tr>" +
                                "<tr><td><b>Booking Date</b></td><td>" + jsdata.BD.BookingDateTime + "</td>" +
                                "<td><b>Supplier</b></td><td>" + jsdata.BD.Supplier + "</td></tr>" +
                                "<tr><td><b>Remarks</b></td><td>" + jsdata.BD.BookingRemarks + "</td>" +
                                "<td colspan='2'><span class='btn' style='float: right; padding-right: 10px; cursor: pointer;' onclick=\"popup('divGeneralDetails', 60, 50);\">Edit General Details</span></td></tr></table>";
                    $("#divFare" + ID).html(strHtml);
                    $("#txtEditPnr").val(jsdata.BD.PNR);
                    $("#txtEditOrigin").val(jsdata.SM.Origin);
                    $("#txtEditDesitination").val(jsdata.SM.Destination);
                  
                    $("#txtEditRemarks").val();
                    $('#ddlEditBookingStatus').find("option").remove();
                    $('#ddlEditBookingStatus').append($("<option></option>").val(jsdata.BD.BookingStatus).html(jsdata.BD.BookingStatus));

                    switch (jsdata.BD.BookingStatus.toUpperCase()) {
                        case "INCOMPLETE":
                            $('#ddlEditBookingStatus').append($("<option></option>").val("Follow Up").html("Follow Up"));
                            $('#ddlEditBookingStatus').append($("<option></option>").val("OPTION").html("OPTION"));
                            $('#ddlEditBookingStatus').append($("<option></option>").val("Cancelled").html("Cancelled"));
                            break;
                        case "DECLINE":
                            $('#ddlEditBookingStatus').append($("<option></option>").val("Incomplete").html("Incomplete"));
                            $('#ddlEditBookingStatus').append($("<option></option>").val("Follow Up").html("Follow Up"));
                            $('#ddlEditBookingStatus').append($("<option></option>").val("OPTION").html("OPTION"));
                            $('#ddlEditBookingStatus').append($("<option></option>").val("Cancelled").html("Cancelled"));
                            $('#ddlEditBookingStatus').append($("<option></option>").val("QUEUE").html("QUEUE"));
                            $('#ddlEditBookingStatus').append($("<option></option>").val("ISSUED").html("ISSUED"));
                            txtPnr.ReadOnly = false;
                            break;
                        case "FOLLOW UP":
                            $('#ddlEditBookingStatus').append($("<option></option>").val("OPTION").html("OPTION"));
                            $('#ddlEditBookingStatus').append($("<option></option>").val("Cancelled").html("Cancelled"));
                            $('#ddlEditBookingStatus').append($("<option></option>").val("BOOKED").html("BOOKED"));
                            break;
                        case "OPTION":
                            $('#ddlEditBookingStatus').append($("<option></option>").val("CONFIRM").html("CONFIRM"));
                            $('#ddlEditBookingStatus').append($("<option></option>").val("Cancelled").html("Cancelled"));
                            break;
                        case "CONFIRM":
                            $('#ddlEditBookingStatus').append($("<option></option>").val("QUEUE").html("QUEUE"));
                            $('#ddlEditBookingStatus').append($("<option></option>").val("ISSUED").html("ISSUED"));
                            $('#ddlEditBookingStatus').append($("<option></option>").val("BOOKED").html("BOOKED"));
                            $('#ddlEditBookingStatus').append($("<option></option>").val("Cancelled").html("Cancelled"));
                            break;
                        case "BOOKED":
                            $('#ddlEditBookingStatus').append($("<option></option>").val("Payment RCVD").html("Payment RCVD"));
                            $('#ddlEditBookingStatus').append($("<option></option>").val("QUEUE").html("QUEUE"));
                            $('#ddlEditBookingStatus').append($("<option></option>").val("Payment Decline").html("Payment Decline"));
                            $('#ddlEditBookingStatus').append($("<option></option>").val("Cancelled").html("Cancelled"));
                            break;
                        case "PAYMENT RCVD":
                            $('#ddlEditBookingStatus').append($("<option></option>").val("QUEUE").html("QUEUE"));
                            $('#ddlEditBookingStatus').append($("<option></option>").val("Cancelled").html("Cancelled"));
                            break;
                        case "QUEUE":
                            $('#ddlEditBookingStatus').append($("<option></option>").val("QUEUE").html("QUEUE"));
                            $('#ddlEditBookingStatus').append($("<option></option>").val("ISSUED").html("ISSUED"));
                            $('#ddlEditBookingStatus').append($("<option></option>").val("Cancelled").html("Cancelled"));
                            break;
                        default:
                            break;
                    }
                },
                error: function (data) { }
            });
        }

        function GetPassengerDetails(ID) {
            $.ajax({
                type: "POST",
                url: "BookingEditDetails.aspx/GetPassengerDetails",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify(ReqData),
                responseType: "json",
                success: function (data) {
                    var jsdata = JSON.parse(data.d);
                    PaxDetails = jsdata;
                    BindPax('-1');
                },
                error: function (data) { }
            });
        }
        function BindPax(SrNo) {
            var strHtml = "<table width='100%' cellpadding='0' cellspacing='0' class='table' >" +
                            "<tr><td class='gdvh'>SrNo</td>" +
                            "<td class='gdvh'>PaxType</td>" +
                            "<td class='gdvh'>Title</td>" +
                            "<td class='gdvh'>FirstName</td>" +
                            "<td class='gdvh'>Last Name</td>" +
                            "<td class='gdvh'>DOB</td>" +
                            "<td class='gdvh'>Gender</td><td class='gdvh'></td></tr>";
            $.each(PaxDetails.PD, function (key, value) {
                if (SrNo == value.SrNo) {
                    strHtml += "<tr><td class='gdvr'>" + (key + 1) + "</td>" +
                            "<td class='gdvr'>" + value.PaxType + "</td>" +
                            "<td class='gdvr'>" +
                                 "<select  id='ddlEditTitle'>" +
                                     "<option value='MR' " + (value.Title.toUpperCase() == "MR" ? " selected='selected'" : "") + ">Mr</option>" +
                                     "<option value='Mstr'" + (value.Title.toUpperCase() == "MSTR" ? " selected='selected'" : "") + ">Master</option>" +
                                     "<option value='Mrs'" + (value.Title.toUpperCase() == "MRS" ? " selected='selected'" : "") + ">Mrs.</option>" +
                                     "<option value='MISS'" + (value.Title.toUpperCase() == "MISS" ? " selected='selected'" : "") + ">Miss.</option>" +
                                     "<option value='DR'" + (value.Title.toUpperCase() == "DR" ? " selected='selected'" : "") + ">Dr.</option>" +
                                 "</select>" +
                            "</td>" +
                            "<td class='gdvr'><input type='text' class='form-control' id='txtEditPaxFName' value='" + value.FName + "' /></td>" +
                            "<td class='gdvr'><input type='text' class='form-control' id='txtEditPaxLName' value='" + value.LName + "' /></td>" +
                            "<td class='gdvr'><input type='text' class='form-control' id='txtEditPaxDOB' value='" + value.DOB + "' /></td>" +
                            "<td class='gdvr'>" + value.PaxSex + "</td>" +
                            "<td class='gdvr'><span onclick=\"UpdatePax('" + value.SrNo + "')\">Update</span>&nbsp;<span onclick=\"BindPax('-1')\">Cancel</span></td></tr>";

                }
                else {
                    strHtml += "<tr><td class='gdvr'>" + (key + 1) + "</td>" +
                                    "<td class='gdvr'>" + value.PaxType + "</td>" +
                                    "<td class='gdvr'>" + value.Title + "</td>" +
                                    "<td class='gdvr'>" + value.FName + "</td>" +
                                    "<td class='gdvr'>" + value.LName + "</td>" +
                                    "<td class='gdvr'>" + value.DOB + "</td>" +
                                     "<td class='gdvr'>" + value.PaxSex + "</td>" +
                                    "<td class='gdvr'><span onclick=\"BindPax('" + value.SrNo + "')\">Edit</span></td></tr>";
                }
            });
            if (PaxDetails.length == 0)
                strHtml += "<tr><td class='gdvr' colspan='7'>No any Pax!!</td></tr>";
            $("#divFare2").html(strHtml + "</table>");
            $("#txtEditPaxDOB").datepicker({ dateFormat: 'dd M yy' });
        }
        function GetSectorDetails(ID) {
            $.ajax({
                type: "POST",
                url: "BookingEditDetails.aspx/GetSectorDetails",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify(ReqData),
                responseType: "json",
                success: function (data) {
                    var jsdata = JSON.parse(data.d);
                    SectorDetails = jsdata;
                    BindSectorDetails('show');
                },
                error: function (data) { }
            });
        }
        function BindSectorDetails(bType) {
            var strHtml = "<table width='100%' cellpadding='0' cellspacing='0' class='table' >" +
                               "<tr><td class='gdvh'>SrNo</td>" +
                               "<td class='gdvh'>CarierName</td>" +
                               "<td class='gdvh'>FlightNo</td>" +
                               "<td class='gdvh'>Class</td>" +
                               "<td class='gdvh'>FromDest</td>" +
                               (bType != "edit" ? "<td class='gdvh'>FromDateTime</td>" : "<td class='gdvh'>FromDate</td><td class='gdvh'>FromTime</td>") +
                               "<td class='gdvh'>ToDest</td>" +
                               (bType != "edit" ? "<td class='gdvh'>ToDateTime</td>" : "<td class='gdvh'>ToDate</td><td class='gdvh'>ToTime</td>") +
                               "<td class='gdvh'>TFrom</td>" +
                               "<td class='gdvh'>TTo</td>" +
                               "<td class='gdvh'>Status</td></tr>";
            $.each(SectorDetails.SD, function (key, value) {
                if (bType == "edit") {
                    var FromDate = value.FromDateTime.split(' ');
                    var FDate = "", FTime = "";
                    if (FromDate.length >= 4) {
                        FDate = FromDate[0] + " " + FromDate[1] + " " + FromDate[2];
                        FTime = FromDate[3].replace(":", "")
                    }
                    var ToDate = value.ToDateTime.split(' ');
                    var TDate = "", TTime = "";
                    if (ToDate.length >= 4) {
                        TDate = ToDate[0] + " " + ToDate[1] + " " + ToDate[2];
                        TTime = ToDate[3].replace(":", "")
                    }
                    strHtml += "<tr><td class='gdvr'>" + (key + 1) + "</td>" +
                               "<td class='gdvr'><input type='text' class='form-control' id='txtEditCarierName" + value.SrNo + "' value='" + value.CarierName + "' /></td>" +
                               "<td class='gdvr'><input type='text' class='form-control' id='txtEditFlightNo" + value.SrNo + "' value='" + value.FlightNo + "' /></td>" +
                               "<td class='gdvr'><input type='text' class='form-control' id='txtEditFClass" + value.SrNo + "' value='" + value.FClass + "' /></td>" +
                               "<td class='gdvr'><input type='text' class='form-control' id='txtEditFromDest" + value.SrNo + "' value='" + value.FromDest + "' /></td>" +
                               "<td class='gdvr'><input type='text' class='form-control' id='txtEditFromDate" + value.SrNo + "' value='" + FDate + "' /></td>" +
                               "<td class='gdvr'><input type='text' class='form-control' id='txtEditFromTime" + value.SrNo + "' value='" + FTime + "' /></td>" +
                               "<td class='gdvr'><input type='text' class='form-control' id='txtEditToDest" + value.SrNo + "' value='" + value.ToDest + "' /></td>" +
                               "<td class='gdvr'><input type='text' class='form-control' id='txtEditToDate" + value.SrNo + "' value='" + TDate + "' />" +
                               "<td class='gdvr'><input type='text' class='form-control' id='txtEditToTime" + value.SrNo + "' value='" + TTime + "' /></td>" +
                               "<td class='gdvr'><input type='text' class='form-control' id='txtEditTerminalFrom" + value.SrNo + "' value='" + value.TerminalFrom + "' /></td>" +
                               "<td class='gdvr'><input type='text' class='form-control' id='txtEditTerminalTo" + value.SrNo + "' value='" + value.TerminalTo + "' /></td>" +
                               "<td class='gdvr'>" + value.FStatus + "</td></tr>";
                }
                else {
                    strHtml += "<tr><td class='gdvr'>" + (key + 1) + "</td>" +
                              "<td class='gdvr'>" + value.CarierName + "</td>" +
                              "<td class='gdvr'>" + value.FlightNo + "</td>" +
                              "<td class='gdvr'>" + value.FClass + "</td>" +
                              "<td class='gdvr'>" + value.FromDest + "</td>" +
                              "<td class='gdvr'>" + value.FromDateTime + "</td>" +
                              "<td class='gdvr'>" + value.ToDest + "</td>" +
                              "<td class='gdvr'>" + value.ToDateTime + "</td>" +
                              "<td class='gdvr'>" + value.TerminalFrom + "</td>" +
                              "<td class='gdvr'>" + value.TerminalTo + "</td>" +
                              "<td class='gdvr'>" + value.FStatus + "</td></tr>";
                }

            });
            if (SectorDetails.length == 0) {
                strHtml += "<tr><td class='gdvr' colspan='11'>No any Sectors!!</td></tr>";
            }
            else {
                if (bType == "edit") {
                    strHtml += "<tr><td class='gdvr' colspan='13'><span onclick=\"UpdateSectorDetails();\">Update</span>&nbsp;<span onclick=\"BindSectorDetails('show');\">Cancel</span></td></tr>";
                }
                else {
                    strHtml += "<tr><td class='gdvr' colspan='13'><span onclick=\"BindSectorDetails('edit');\">Edit</span></td></tr>";
                }
            }
            $("#divFare3").html(strHtml + "</table>");
            $.each(SectorDetails.SD, function (key, value) {
                $("#txtEditFromDate" + value.SrNo).datepicker({ dateFormat: 'dd M yy' });
                $("#txtEditToDate" + value.SrNo).datepicker({ dateFormat: 'dd M yy' });
            });
        }
        function GetChargeDetails(ID) {
            $.ajax({
                type: "POST",
                url: "BookingEditDetails.aspx/GetChargeDetails",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify(ReqData),
                responseType: "json",
                success: function (data) {
                    var jsdata = JSON.parse(data.d);
                    ChargesDetails = jsdata;
                    BindChargeDetails('show')
                },
                error: function (data) { }
            });
        }
        function BindChargeDetails(bType) {
            var strHtml = "<table width='100%' cellpadding='0' cellspacing='0' class='table' >" +
                              "<tr><td class='gdvh'>SrNo</td>" +
                              "<td class='gdvh'>ChargeID</td>" +
                              "<td class='gdvh'>ChargesFor</td>" +
                              "<td class='gdvh'>SellAmount</td>" +
                              "<td class='gdvh'>CostAmount</td>" +
                              "<td class='gdvh'>Remarks</td></tr>";
            var SellPrice = 0.0;
            var CostPrice = 0.0;
            $.each(ChargesDetails.ACD, function (key, value) {
                if (bType == "edit") {
                    strHtml += "<tr><td class='gdvr'>" + (key + 1) + "</td>" +
                              "<td class='gdvr'>" + value.ChargeID + "</td>" +
                              "<td class='gdvr'>" + value.ChargesFor +
                                //"<select class='form-control' id='ddlPaxTypeAdd'>"+
                                //    "<option value='NA' " + (value.ChargesFor.toUpperCase() == "NA" ? " selected='selected'" : "") + ">NA</option>" +
                                //    "<option value='ALL' " + (value.ChargesFor.toUpperCase() == "ALL" ? " selected='selected'" : "") + ">All</option>" +
                                //    "<option value='ADT' " + (value.ChargesFor.toUpperCase() == "ADT" ? " selected='selected'" : "") + ">Adult</option>" +
                                //    "<option value='CHD' " + (value.ChargesFor.toUpperCase() == "CHD" ? " selected='selected'" : "") + ">Child</option>" +
                                //    "<option value='INF' " + (value.ChargesFor.toUpperCase() == "INF" ? " selected='selected'" : "") + ">Infant</option>" +
                                //"</select>" +
                              "</td>" +
                              "<td class='gdvr'><input type='text' class='form-control' id='txtEditSellPrice" + value.SrNo + "' value='" + value.SellPrice + "' /></td>" +
                              "<td class='gdvr'><input type='text' class='form-control' id='txtEditCostPrice" + value.SrNo + "' value='" + value.CostPrice + "' /></td>" +
                              "<td class='gdvr'><input type='text' class='form-control' id='txtEditRemarks" + value.SrNo + "' value='" + value.ChrgesRemarks + "' /></td></tr>";
                }
                else {
                    strHtml += "<tr><td class='gdvr'>" + (key + 1) + "</td>" +
                               "<td class='gdvr'>" + value.ChargeID + "</td>" +
                               "<td class='gdvr'>" + value.ChargesFor + "</td>" +
                               "<td class='gdvr'>" + value.SellPrice + "</td>" +
                               "<td class='gdvr'>" + value.CostPrice + "</td>" +
                               "<td class='gdvr'>" + value.ChrgesRemarks + "</td></tr>";
                }
                SellPrice += parseFloat(value.SellPrice);
                CostPrice += parseFloat(value.CostPrice);

            });
            if (ChargesDetails.length == 0)
                strHtml += "<tr><td class='gdvr' colspan='8'>No any Charges!!</td></tr>";
            else {
                strHtml += "<tr><td class='gdvf'>Total</td>" +
                             "<td class='gdvf'></td>" +
                             "<td class='gdvf'></td>" +
                             "<td class='gdvf'>" + SellPrice.toFixed(2) + "</td>" +
                             "<td class='gdvf'>" + CostPrice.toFixed(2) + "</td>" +
                             "<td class='gdvf'>" + (bType == "edit" ? "<span onclick=\"UpdateChargeDetails();\">Update</span>&nbsp;<span onclick=\"BindChargeDetails('show');\">Cancel</span>" : "<span onclick=\"BindChargeDetails('edit');\">Edit</span>") + "</td></tr>";
            }
            $("#divFare4").html(strHtml + "</table>");
        }
        function GetTrnsDetails(ID) {
            $.ajax({
                type: "POST",
                url: "BookingEditDetails.aspx/GetTrnsDetails",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify(ReqData),
                responseType: "json",
                success: function (data) {
                    var jsdata = JSON.parse(data.d);
                    TransactionDetails = jsdata;
                    if (jsdata.TM != null) {
                        var strHtml = "<table width='100%' class='table' cellspacing='0' border='0' cellpadding='0'>" +
                                 "<tr><td><b> TrnsNo</b></td><td>" + jsdata.TM.TrnsNo + "</td>" +
                                 "<td><b>TrnsType</b></td><td>" + jsdata.TM.TrnsType + "</td></tr>" +
                                 "<tr><td><b>PaymentStatus</b></td><td>" + jsdata.TM.TrnsPaymentStatus + "</td>" +
                                 "<td><b>TrnsAmount</b></td><td>" + jsdata.TM.TrnsAmount + "</td></tr>" +
                                 "<tr><td><b>CurrencyType</b></td><td>" + jsdata.TM.TrnsCurrencyType + "</td>" +
                                 "<td><b>TrnsBy</b></td><td>" + jsdata.TM.TrnsBy + "</td></tr>" +
                                 "<tr><td><b>TrnsDateTime</b></td><td>" + jsdata.TM.TrnsDateTime + "</td>" +
                                 "<td><b>FuturePayID</b></td><td>" + jsdata.TM.TrnsAuthNo + "</td></tr></table>";
                        $("#divFare" + ID).html(strHtml);
                    }
                    else {
                        $("#divFare" + ID).html("<table width='100%' class='table' cellspacing='0' border='0' cellpadding='0'>" +
                                 "<tr><td style='color:#ff0000;'><b>No any transaction is found!!</b></td></tr></table>");
                    }

                },
                error: function (data) { }
            });
        }

        function UpdateGeneralDetails() {
            if ($("#txtEditRemarks").val() != "") {
                var EditGenDetails = {
                    BookingID: $("#<%=hfBookingID.ClientID%>").val(),
                    ProdID: $("#<%=hfProdID.ClientID%>").val(),
                    PNR: $("#txtEditPnr").val(),
                    Origin: $("#txtEditOrigin").val(),
                    Destination: $("#txtEditDesitination").val(),
                    EmailID: "",
                    MobileNo: "",
                    PhoneNo: "",
                    Remarks: $("#txtEditRemarks").val(),
                    BookingStatus: $('#ddlEditBookingStatus').val(),
                    UpdatedBy: $("#<%=hfUpdatedBy.ClientID%>").val()
                };
                $.ajax({
                    type: "POST",
                    url: "BookingEditDetails.aspx/EditBookingSummary",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify(EditGenDetails),
                    responseType: "json",
                    success: function (data) {

                        if (data.d == "true") {
                            popup('divGeneralDetails', 60, 50);
                            GetBookingSummary('1')
                        }
                        else {
                            alert("Not update successfully!!");
                        }
                    },
                    error: function (data) { debugger; }
                });
            }
            else { alert("Please Enter remarks!!"); }
        }
        function UpdatePax(SrNo) {
            var strMsg = "";
            if ($("#ddlEditTitle").val() == "")
                strMsg += "Please choose passenger title!!\n\r";
            if ($("#txtEditPaxFName").val() == "")
                strMsg += "Please enter passenger first name!!\n\r";
            if ($("#txtEditPaxLName").val() == "")
                strMsg += "Please enter passenger last name!!\n\r";
            if ($("#txtEditPaxDOB").val() == "")
                strMsg += "Please enter passenger date of birth!!\n\r";
            if (strMsg == "") {
                var EditPaxDetails = {
                    SrNo: SrNo,
                    Title: $("#ddlEditTitle").val(),
                    FName: $("#txtEditPaxFName").val(),
                    Lname: $('#txtEditPaxLName').val(),
                    DOB: $('#txtEditPaxDOB').val(),
                    UpdatedBy: $("#<%=hfUpdatedBy.ClientID%>").val()
                };
                $.ajax({
                    type: "POST",
                    url: "BookingEditDetails.aspx/UpdatePassenger",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify(EditPaxDetails),
                    responseType: "json",
                    success: function (data) {
                        if (data.d == "true") {
                            GetPassengerDetails('2')
                        }
                        else {
                            alert("Not update successfully!!");
                        }
                    },
                    error: function (data) { debugger; }
                });
            }
            else { alert(strMsg); }
        }
        function UpdateSectorDetails() {
            var strMsg = "";
            var EditSectors = [];
            var ctr = 1;
            $.each(SectorDetails.SD, function (key, value) {
                var EditSectorDetails = {
                    SrNo: value.SrNo,
                    BookingID: value.BookingID,
                    ProdID: value.ProdID,
                    CarierName: $("#txtEditCarierName" + value.SrNo).val(),
                    FlightNo: $("#txtEditFlightNo" + value.SrNo).val(),
                    FClass: $("#txtEditFClass" + value.SrNo).val(),
                    FromDest: $("#txtEditFromDest" + value.SrNo).val(),
                    FromDateTime: $("#txtEditFromDate" + value.SrNo).val() + " " + $("#txtEditFromTime" + value.SrNo).val(),
                    ToDest: $("#txtEditToDest" + value.SrNo).val(),
                    ToDateTime: $("#txtEditToDate" + value.SrNo).val() + " " + $("#txtEditToTime" + value.SrNo).val(),
                    TerminalFrom: $("#txtEditTerminalFrom" + value.SrNo).val(),
                    TerminalTo: $("#txtEditTerminalTo" + value.SrNo).val(),
                    ModifiedBy: $("#<%=hfUpdatedBy.ClientID%>").val()
                    }
                    if ($("#txtEditCarierName" + value.SrNo).val() == "")
                        strMsg += "Please enter sector " + ctr.toString() + " carier name!!\n\r";

                    if ($("#txtEditFlightNo" + value.SrNo).val() == "")
                        strMsg += "Please enter sector " + ctr.toString() + " flight no!!\n\r";

                    if ($("#txtEditFromDest" + value.SrNo).val() == "")
                        strMsg += "Please enter sector " + ctr.toString() + " departure airport code!!\n\r";

                    if ($("#txtEditFromDate" + value.SrNo).val() == "")
                        strMsg += "Please enter sector " + ctr.toString() + " departure date!!\n\r";

                    if (!isTime($("#txtEditFromTime" + value.SrNo).val()))
                        strMsg += "Please enter valid sector " + ctr.toString() + " departure time!!\n\r";

                    if ($("#txtEditToDate" + value.SrNo).val() == "")
                        strMsg += "Please enter sector " + ctr.toString() + " return date!!\n\r";

                    if (!isTime($("#txtEditToTime" + value.SrNo).val()))
                        strMsg += "Please enter sector " + ctr.toString() + " return time!!\n\r";

                    if ($("#txtEditToDest" + value.SrNo).val() == "")
                        strMsg += "Please enter sector " + ctr.toString() + " return airport code!!\n\r";
                    EditSectors.push(EditSectorDetails);
                    
                    ctr++;
                });
                if (strMsg == "") {
                    $.ajax({
                        type: "POST",
                        url: "BookingEditDetails.aspx/UpdateSectorDetails",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data: JSON.stringify({ ESectors: EditSectors }),
                        responseType: "json",
                        success: function (data) {
                            if (data.d == "true") {
                                GetSectorDetails('3')
                                alert("update successfully!!");
                            }
                            else {
                                alert("Not update successfully!!");
                            }
                        },
                        error: function (data) { debugger; }
                    });
                }
                else { alert(strMsg); }
            }
            function UpdateChargeDetails() {
                var strMsg = "";
                var EditCharge = [];
                var ctr = 1;
                $.each(ChargesDetails.ACD, function (key, value) {
                    var Charges = {
                        SrNo: value.SrNo,
                        BookingID: value.BookingID,
                        ProdID: value.ProdID,
                        ChargeID: value.ChargeID,
                        ChargesFor: value.ChargesFor,
                        SellPrice: $("#txtEditSellPrice" + value.SrNo).val(),
                        CostPrice: $("#txtEditCostPrice" + value.SrNo).val(),
                        ChrgesRemarks: $("#txtEditRemarks" + value.SrNo).val(),
                        ModifiedBy: $("#<%=hfUpdatedBy.ClientID%>").val()
                }
                if ($("#txtEditSellPrice" + value.SrNo).val() == "")
                    strMsg += "Please enter charge row " + ctr.toString() + " sell amount!!\n\r";

                if ($("#txtEditCostPrice" + value.SrNo).val() == "")
                    strMsg += "Please enter charge row " + ctr.toString() + " cost amount!!\n\r";

                EditCharge.push(Charges);
                ctr++;
            });
            if (strMsg == "") {
                $.ajax({
                    type: "POST",
                    url: "BookingEditDetails.aspx/UpdateChargeDetails",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({ ECharges: EditCharge }),
                    responseType: "json",
                    success: function (data) {
                        if (data.d == "true") {
                            GetChargeDetails('4')
                            alert("update successfully!!");
                        }
                        else {
                            alert("Not update successfully!!");
                        }
                    },
                    error: function (data) { debugger; }
                });
            }
            else { alert(strMsg); }
        }
        function isTime(dtStr) {
            if (dtStr.length == 4) {
                var strHours = dtStr.substring(0, 2);
                var strMint = dtStr.substring(2);
                var Hours = parseInt(strHours);
                var Mint = parseInt(strMint);
                if (strHours.length < 1 || Hours < 0 || Hours > 23) {
                    return false;
                }
                if (strMint.length < 1 || Mint < 0 || Mint > 59) {
                    return false;
                }
                return true;
            }
            else {
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table class="table" border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td width="100%">
                <div class="destination_tab">
                    <div class="col-md-2">
                    <ul>
                        <li class="first-li12" id="li1" onclick="ShowHideDestTab('1');">Summary</li>
                        <li class="" id="li2" onclick="ShowHideDestTab('2');">Passenger Details</li>
                        <li class="" id="li3" onclick="ShowHideDestTab('3');">Sector Details</li>
                        <li class="" id="li4" onclick="ShowHideDestTab('4');">Amount Details</li>
                        <li class="" id="li5" onclick="ShowHideDestTab('5');">Trns Details</li>
                    </ul>
                </div>
               
                <div class="h-ct-midd-container col-md-10">
                    <div id="divFare1" style="display: block;"></div>
                    <div id="divFare2" style="display: none;"></div>
                    <div id="divFare3" style="display: none;"></div>
                    <div id="divFare4" style="display: none;"></div>
                    <div id="divFare5" style="display: none;"></div>
                </div>
                    </div>
            </td>
            <%--  <td rowspan="5" width="20%">
                <table class="table" border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td></td>
                    </tr>
                    <tr>
                        <td>Add Remarks</td>
                    </tr>
                    <tr>
                        <td>Add Passenger</td>
                    </tr>
                    <tr>
                        <td>Edit Passenger</td>
                    </tr>
                    <tr>
                        <td>Add Sector</td>
                    </tr>
                    <tr>
                        <td>Edit Sector</td>
                    </tr>
                    <tr>
                        <td>Add Charges Details</td>
                    </tr>
                    <tr>
                        <td>Edit Charges Details</td>
                    </tr>
                </table>
            </td>--%>
        </tr>
    </table>
    <asp:HiddenField ID="hfBookingID" runat="server" Value="" />
    <asp:HiddenField ID="hfProdID" runat="server" Value="" />
    <asp:HiddenField ID="hfUpdatedBy" runat="server" />
    <div id="fadebackground">
    </div>
    <div align="center" id="divGeneralDetails" style="display: none;" class="popup-product">
        <table width="100%" align="center" height="100%" bgcolor="#ffffff">
            <tr>
                <td class="popup-header">Update Flight Fares Details
                </td>
                <td class="popup-header"><span style="float: right; padding-right: 10px; cursor: pointer;" onclick="popup('divGeneralDetails', 30, 30);">Close(X)</span>
                </td>
            </tr>
            <tr>
                <td align="center" valign="middle" colspan="2">
                    <table class="table" border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td><b>PNR</b></td>
                            <td>
                                <input id="txtEditPnr" disabled="disabled" type="text" class="form-control" /></td>
                            <td><b>Booking Status</b></td>
                            <td>
                                <select id="ddlEditBookingStatus" disabled="disabled" class="form-control">
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td><b>Origin</b></td>
                            <td>
                                <input id="txtEditOrigin" disabled="disabled" type="text" class="form-control" /></td>
                            <td><b>Desitination</b></td>
                            <td>
                                <input id="txtEditDesitination" disabled="disabled" type="text" class="form-control" />
                            </td>
                        </tr>
                        <%--<tr>
                            <td><b>EmailID</b></td>
                            <td>
                                <input id="txtEditEmailID"  disabled="disabled" type="password" class="form-control" /></td>
                            <td><b>PhoneNo</b></td>
                            <td>
                                <input id="txtEditPhoneNo" disabled="disabled" type="password" class="form-control" /></td>
                        </tr>--%>
                        <tr>
                            <td><b>MobileNo</b></td>
                            <td>
                                <%--<input id="txtEditMobileNo" disabled="disabled" type="password" class="form-control" />--%>

                            </td>
                            <td><b>Remarks</b></td>
                            <td colspan="3">
                                <textarea id="txtEditRemarks" cols="40" rows="2" class="form-control"></textarea></td>
                        </tr>
                        <tr>
                            <td colspan="2"><span style="float: right; padding-right: 10px; cursor: pointer;" onclick="UpdateGeneralDetails();">Update</span></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

