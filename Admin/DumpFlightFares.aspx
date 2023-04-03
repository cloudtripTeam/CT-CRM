<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true"
    CodeFile="DumpFlightFares.aspx.cs" Inherits="Admin_DumpFlightFares" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
   
    <script src="../js/jquery-1.8.2.js"></script>
    <script src="//code.jquery.com/ui/1.10.0/jquery-ui.js" type="text/javascript"></script>
    <script src="../js/jquery-ui.js"></script>
    <style type="text/css">
        label, input {
            display: block;
        }

            input.text {
                margin-bottom: 12px;
                width: 95%;
                padding: .4em;
            }

        dialog-form-loc fieldset {
            padding: 0;
            border: 0;
            margin-top: 25px;
            background-color: #fff;
        }

        h1 {
            font-size: 1.2em;
            margin: .6em 0;
        }

        div#users-contain {
            width: 100%;
            margin: 20px 0;
        }

            div#users-contain table {
                margin: 1em 0;
                border-collapse: collapse;
                width: 100%;
            }

                div#users-contain table td, div#users-contain table th {
                    border: 1px solid #eee;
                    padding: .6em 10px;
                    text-align: left;
                }

        .ui-dialog .ui-state-error {
            padding: .3em;
        }

        .validateTips {
            border: 1px solid transparent;
            padding: 0.3em;
        }

        #dialog-form {
            display: none;
        }

        #dialog-form-loc {
            display: none;
            padding: 10px;
        }

        .ui-dialog-titlebar-close {
            float: right;
            margin-top: 34px;
            margin-right: 15px;
        }

        .ui-dialog-buttonset {
            margin-top: 43px;
            padding-left: 22px;
        }

        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="height: 15px;"></div>
    <div class="container">
        <div class="row">
            <div class="col-md-2">
                <input id="btnSave" type="button" value="Save Data" class="btn btn-danger" style="float: left; margin: 0px;" />
            </div>
            <div class="col-md-2">
                <input id="btnGenerate" type="button" value="Generate" class="btn btn-danger" style="float: left; margin: 0px 0 0 10px;" />
            </div>
            <div class="col-md-2">
                <input id="btnViewFare" type="button" value="ViewFare" class="btn btn-danger" style="float: left; margin: 0px 0 0 10px;" />
            </div>
            <div class="col-md-2">
                <input id="btndwnload" type="button" value="Export to Excel" class="btn btn-danger" style="float: left; margin: 0px 0 0 10px;" />
            </div>
        </div>

        <div style="width: 100%; float: left; padding-top: 10px;">

            <asp:HiddenField ID="hdnDates" runat="server" />
            <asp:HiddenField ID="hdnLocation" runat="server" />
            <asp:HiddenField ID="hdnFare" runat="server" />
        </div>
    </div>
    <div class="container">
        <div style="height: 15px;"></div>
        <div class="panel panel-default">
            <div class="panel-heading">BookingDetails</div>
            <div class="panel-body" style="line-height: 34px;">
                <div class="row">

                    <div id="dialog-form-loc">
                        <div class="panel panel-primary" style="height: 265px;">
                            <div class="panel-heading">Location Scheduling</div>
                            <div class="panel-body" style="line-height: 34px;">
                                <form>
                                    <fieldset>

                                        <p>
                                            <label for="Depart_location">
                                                Depart Location Code</label>
                                            <input type="text" class="form-control" id="Depart_location" maxlength="3" />
                                        </p>
                                        <p>
                                            <label for="Return_location">
                                                Return Location Code</label>
                                            <input type="text" class="form-control" id="Return_location" maxlength="3" />
                                        </p>
                                    </fieldset>
                                </form>
                            </div>
                        </div>

                    </div>

                    <div class="row">
                        <div class="col-md-5">
                            <div id="users-contain" class="ui-widget">
                                <h1>Existing Location</h1>
                                <table id="locationtbl" class="ui-widget ui-widget-content">
                                    <thead>
                                        <tr class="ui-widget-header ">
                                            <th>Origin
                                            </th>
                                            <th>Destination
                                            </th>
                                            <th colspan="2" style="background-color: White; border: 0px;">
                                                <button id="create_location" class="btn btn-primary" onclick="return false;">
                                                    Add Location</button>
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <div id="dialog-form">
                            <div class="panel panel-primary" style="height: 400px;">
                                <div class="panel-heading">Date Scheduling</div>
                                <div class="panel-body" style="line-height: 34px;">

                                    <p class="validateTips">
                                        All form fields are required.
                                    </p>
                                    <form>
                                        <fieldset>
                                            <p>
                                                <label for="DepartDate">
                                                    Depart Date (dd/MM/yyyy)</label>
                                                <input type="text" class="form-control" id="DepartDate" maxlength="10" />
                                            </p>
                                            <p>
                                                <label for="Return">
                                                    Return Date (dd/MM/yyyy)</label>
                                                <input type="text" class="form-control" id="ReturnDate" maxlength="10" />
                                            </p>
                                            <p>
                                                <label for="interval">
                                                    Interval
                                                </label>
                                                <select id="inteval" class="form-control">
                                                    <option>0</option>
                                                    <option>1</option>
                                                    <option>2</option>
                                                    <option>3</option>
                                                    <option>4</option>
                                                    <option>5</option>
                                                    <option>6</option>
                                                    <option>7</option>
                                                    <option>8</option>
                                                    <option>9</option>
                                                    <option>10</option>
                                                    <option>11</option>
                                                    <option>12</option>
                                                    <option>13</option>
                                                    <option>14</option>
                                                    <option>15</option>
                                                    <option>16</option>
                                                    <option>17</option>
                                                    <option>18</option>
                                                    <option>19</option>
                                                    <option>20</option>
                                                </select>
                                            </p>
                                        </fieldset>
                                    </form>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-5">
                            <div id="users-contain" class="ui-widget">
                                <h1>Existing Dates</h1>
                                <table id="Datestbl" class="ui-widget ui-widget-content">
                                    <thead>
                                        <tr class="ui-widget-header ">
                                            <th>Departure <span style="font-size: 13px; font-weight: normal;">(dd/MM/yyyy)</span>
                                            </th>
                                            <th>Return <span style="font-size: 13px; font-weight: normal;">(dd/MM/yyyy)</span>
                                            </th>
                                            <th>Interval
                                            </th>
                                            <th style="background-color: White; border: 0px;" colspan="2">
                                                <button id="create_user" class="btn btn-primary" onclick="return false;">
                                                    Add Date
                                                </button>
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div id="users-contain" class="ui-widget" style="float: left; width: 120px; margin-top: 20px;">
                                <h1>Cabin Class</h1>
                                <table id="Classtbl" class="ui-widget ui-widget-content">
                                    <thead>
                                        <tr class="ui-widget-header ">
                                            <th>Class
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="ddlCurrency" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCurrency_SelectedIndexChanged">
                                                    <asp:ListItem Value="GBP">GBP</asp:ListItem>
                                                    <asp:ListItem Value="USD">USD</asp:ListItem>  
                                                     <asp:ListItem Value="CAD">CAD</asp:ListItem>
                                                     <asp:ListItem Value="EUR">EUR</asp:ListItem>
                                                     <asp:ListItem Value="INR">INR</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="drpClass" CssClass="form-control" runat="server">
                                                    <asp:ListItem Value="Y">Economy</asp:ListItem>
                                                    <asp:ListItem Value="F">First</asp:ListItem>
                                                    <asp:ListItem Value="C">Business</asp:ListItem>
                                                    <asp:ListItem Value="W">Pre.Economy</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Amount IN (%)
                                            </td>
                                        </tr>
                                         <tr>
                                            <td><asp:TextBox ID="txtAmtPer" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div style="width: 980px; float: left; margin: 0px; padding: 0px;">
                        <div id="users-contain" class="ui-widget" style="width: 100%; height: auto; overflow-y: auto; overflow-x: hidden; float: left; margin: 0px;">
                            <h1 id="FareDetailH1" style="display: none;">Fare Details</h1>
                            <table id="FareDetail" class="ui-widget ui-widget-content" style="display: none;">
                                <thead>
                                    <tr class="ui-widget-header ">
                                        <th>Origin
                                        </th>
                                        <th>Destination
                                        </th>
                                        <th>Depart Date
                                        </th>
                                        <th>Return Date
                                        </th>
                                        <th>Status
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                </tbody>
                            </table>
                        </div>
                    </div>

                    <div id="users-contain" class="ui-widget" style="min-height: 0px; max-height: 500px; float: left; overflow: scroll; margin: 0px; padding: 0px;">
                        <div style="float: left;">
                            <h1 id="Hid" style="display: none; font: bold 19px/18px calibri; float: left; margin: 0px; padding-bottom: 10px;">Fare Result</h1>
                        </div>
                        <table id="FareResult" class="ui-widget ui-widget-content" style="display: none;"
                            width="100%" border="0" cellpadding="0" cellspacing="0">
                            <thead>
                                <tr class="ui-widget-header ">
                                    <th>From
                                    </th>
                                    <th>Depart_Location
                                    </th>
                                    <th>To
                                    </th>
                                    <th>Return_Location
                                    </th>
                                    <th>Available_Seats
                                    </th>
                                    <th>Travel_DateStart
                                    </th>
                                    <th>Travel_DateEnd
                                    </th>
                                    <th>Airline_Name
                                    </th>
                                    <th>Airline_Code
                                    </th>
                                    <th>Class
                                    </th>
                                    <th>ClassType
                                    </th>
                                    <th>BaseFare
                                    </th>
                                    <th>Tax
                                    </th>
                                    <th>Total
                                    </th>
                                    <th>FilledBy
                                    </th>
                                    <th>FillDate
                                    </th>
                                    <th>Directflt
                                    </th>
                                    <th>Country
                                    </th>
                                    <th>Country_Code
                                    </th>
                                    <th>Continent_Name
                                    </th>
                                    <th>Continent_Code
                                    </th>
                                    <th>Markup
                                    </th>
                                    <th>Provider
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>



    <script type="text/javascript" language="javascript">

        $(function () {
            
            var new_dialog = function (type, row) {
                var dlg = $("#dialog-form").clone();
                var departdate = dlg.find(("#DepartDate")), retdate = dlg.find(("#ReturnDate")),
                interval = dlg.find(("#inteval"));
                type = type || 'Create';
                var config =
                {
                    autoOpen: true,
                    height: 400,
                    width: 350,
                    modal: true,
                    buttons:
                     {
                         "Create Dates": save_data,
                         "Cancel": function () {
                             dlg.dialog("close");
                         }
                     },
                    close: function () {
                        dlg.remove();
                    }
                };
                if (type === 'Edit') {
                    config.title = "Edit Date";
                    get_data();
                    delete (config.buttons['Create Dates']);
                    config.buttons['Edit Date'] = function () {
                        row.remove();
                        save_data();
                    };
                }

                dlg.dialog(config);
                function get_data() {
                    var _departdate = $(row.children().get(0)).text(),
                _retdate = $(row.children().get(1)).text(),
                _interval = $(row.children().get(2)).text();

                    departdate.val(_departdate);
                    retdate.val(_retdate);
                    interval.val(_interval);

                }
                function save_data() {
                    $("#Datestbl tbody").append("<tr>" + "<td>" + (departdate.val().toUpperCase()) + "</td>" + "<td>" + (retdate.val().toUpperCase()) + "</td>" + "<td>" + interval.find("option:selected").text() + "</td>" + "<td><a href='' class='edit'>Edit</a></td>" + "<td><span class='delete'><a href=''>Delete</a></span></td>" + "</tr>");
                    dlg.dialog("close");
                }
            };

            var loc_dialog = function (type, row) {
                
                var loc_dlg = $("#dialog-form-loc").clone();
                var loc_depart = loc_dlg.find(("#Depart_location")),
                    loc_return = loc_dlg.find(("#Return_location"));
                type = type || 'Create';
                var loc_config =
                    {
                        autoOpen: true,
                        height: 300,
                        background: '#fff',
                        width: 350,
                        modal: true,
                        buttons:
                     {
                         "Create Location": loc_save_data,
                         "Cancel": function () {
                             loc_dlg.dialog("close");
                         }
                     },
                        close: function () {
                            loc_dlg.remove();
                        }
                    };

                if (type === 'Edit') {
                    loc_config.title = "Edit Location";
                    loc_get_data();
                    delete (loc_config.buttons['Create Location']);
                    loc_config.buttons['Edit Location'] = function () {
                        row.remove();
                        loc_save_data();
                    };
                }

                loc_dlg.dialog(loc_config);


                function loc_get_data() {
                    var _locdepart = $(row.children().get(0)).text(),
                _locreturn = $(row.children().get(1)).text();

                    loc_depart.val(_locdepart);
                    loc_return.val(_locreturn);
                }

                function loc_save_data() {

                    $("#locationtbl tbody").append("<tr>" + "<td>" + (loc_depart.val().toUpperCase()) + "</td>" + "<td>" + (loc_return.val().toUpperCase()) + "</td>" + "<td><a href='' class='Edit'>Edit</a></td>" + "<td><span class='delete'><a href=''>Delete</a></span></td>" + "</tr>");
                    loc_dlg.dialog("close");
                }

            };

            $(document).on('click', 'span.delete', function () {
                $(this).closest('tr').find('td').fadeOut(1000,
                    function () {
                        $(this).parents('tr:first').remove();
                    });
                return false;
            });

            $(document).on('click', 'td a.edit', function () {
                new_dialog('Edit', $(this).parents('tr'));
                return false;
            });

            $(document).on('click', 'td a.Edit', function () {
                loc_dialog('Edit', $(this).parents('tr'));
                return false;
            });

            $("#create_user").button().click(new_dialog);
            $("#create_location").button().click(loc_dialog);
            $("#btnSave").button();
            $("#btnGenerate").button();
            $("#btnXport").button();
            $("#btnViewFare").button();
            $("#btndwnload").button();



            /*Execute on Page Load to show the data on User Interface*/
            $(document).ready(function () {
                
                $("#btnViewFare").css({
                    display: "none"
                });
                $("#btndwnload").css({
                    display: "none"
                });
                $("#FareResult").css({
                    display: "none"
                });

                var JSONlocation = $("#<%= hdnLocation.ClientID %>").val();
                var JSONDate = $("#<%= hdnDates.ClientID %>").val();

                if (Output.length > 0) {
                    $("#<%= hdnFare.ClientID %>").val(JSON.stringify(Output));
                }
                var JSONFares = $("#<%= hdnFare.ClientID %>").val();

                if (JSONlocation != null && JSONlocation != "") {
                    var objcount = JSON.parse(JSONlocation);
                    for (var count = 0; count < objcount.length; count++) {
                        $("#locationtbl tbody").append("<tr>" + "<td>" + (objcount[count].origin + ' ') + "</td>" + "<td>" + (objcount[count].destination + ' ') + "</td>" + "<td><a href='' class='Edit'>Edit</a></td>" + "<td><span class='delete'><a href=''>Delete</a></span></td>" + "</tr>");
                    }
                }

                if (JSONDate != null && JSONDate != "") {
                    var objcount = JSON.parse(JSONDate);
                    for (var count = 0; count < objcount.length; count++) {
                        $("#Datestbl tbody").append("<tr>" + "<td>" + (objcount[count].departdate + ' ') + "</td>" + "<td>" + (objcount[count].returndate + ' ') + "</td>" + "<td>" + objcount[count].interval + "</td>" + "<td><a href='' class='edit'>Edit</a></td>" + "<td><span class='delete'><a href=''>Delete</a></span></td>" + "</tr>");
                    }
                }


                if (JSONFares != null && JSONFares != "") {
                    var objcount = JSON.parse(JSONFares);
                    for (var count = 0; count < objcount.length; count++) {

                        if (objcount[count].status != "1") {

                            $("#FareDetail tbody").append("<tr>" + "<td>" + (objcount[count].origin) + "</td>" + "<td>" + (objcount[count].destination) + "</td>" +
                                    "<td>" + (objcount[count].departdate) + "</td>" + "<td>" + (objcount[count].returndate) + "</td>" +
                                    "<td><img src='../Images/pending.gif' alt='pending' style='display: block;' id='imgPending'/></td>" + "</tr>");
                        }
                        else {
                            $("#FareDetail tbody").append("<tr>" + "<td>" + (objcount[count].origin) + "</td>" + "<td>" + (objcount[count].destination) + "</td>" +
                                    "<td>" + (objcount[count].departdate) + "</td>" + "<td>" + (objcount[count].returndate) + "</td>" +
                                    "<td><img src='../Images/approval.gif' alt='approved' style='display: block;' id='imgApproved'/></td>" + "</tr>");
                        }
                    }
                }
            });
            $(document).ready(function () {
                ShowFareCombination();
            });
            var location = [];
            var dates = [];
            var Fares = [];
            var Output = [];
            var pageurl = "DumpFlightFares.aspx";

            $(document).ready(function () {
                $("#btnSave").click(function () {

                    location = [], dates = [], Fares = [], Output = [];

                    $("#FareDetailH1").css({
                        display: "none"
                    });
                    $("#FareDetail").css({
                        display: "none"
                    });


                    $("#btnViewFare").css({
                        display: "none"
                    });
                    $("#btndwnload").css({
                        display: "none"
                    });
                    $("#FareResult").css({
                        display: "none"
                    });
                    $("#Hid").css({
                        display: "none"
                    });
                    $("#FareDetailH1").css({
                        display: "none"
                    });



                    var rowCount = $("#FareDetail tbody tr").length;

                    if (rowCount > 0) {
                        $("#FareDetail").find("tr:gt(0)").remove();
                    }

                    $("#FareDetail tbody").append("<tr><td colspan='5'>Please Wait...<img src='../Images/wait_small.gif' alt='pending' style='display: block;' id='imgPending'/></td></tr>");


                    $("#locationtbl tbody tr").each(function (idx, item) {
                        location.push({
                            origin: item.childNodes[0].innerHTML,
                            destination: item.childNodes[1].innerHTML
                        });

                    });

                    $("#Datestbl tbody tr").each(function (idx, item) {
                        dates.push({
                            departdate: item.childNodes[0].innerHTML,
                            returndate: item.childNodes[1].innerHTML,
                            interval: item.childNodes[2].innerHTML
                        });

                    });

                    $("#<%= hdnLocation.ClientID %>").val(JSON.stringify(location));
                    $("#<%= hdnDates.ClientID %>").val(JSON.stringify(dates));

                    /*Call Function to save data using Ajax*/
                    SaveFlightData();

                }); //Button Save
            }); //Document Ready


            function SaveFlightData() {
                var txtcurrency= $("#<%= ddlCurrency.ClientID %>").val();
                $.ajax({
                    type: "POST",
                    url: pageurl + "/SaveFlightSearchdata",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({ loc: location, date: dates, currency: txtcurrency }),
                    responseType: "json",
                    success: function (result) { Savesuccess(result); },
                    error: Saveerror
                });
            }
            /*Search Location Save Success Function*/
            function Savesuccess(result) {

                alert("Data Saved Successfully!");

                var rowCount = $("#FareDetail tbody tr").length;

                if (rowCount > 0) {
                    $("#FareDetail").find("tr:gt(0)").remove();
                }

                var fareresult = JSON.parse(result.d);

                for (var count = 0; count < fareresult.length; count++) {

                    $("#FareDetail tbody").append("<tr>" + "<td>" + (fareresult[count].origin) + "</td>" + "<td>" + (fareresult[count].destination) + "</td>" +
                "<td>" + (fareresult[count].departdate) + "</td>" + "<td>" + (fareresult[count].returndate) + "</td>" +
                "<td><img src='../Images/pending.gif' alt='pending' style='display: block;' id='imgPending'/></td>" + "</tr>");

                    Fares.push({
                        origin: fareresult[count].origin,
                        destination: fareresult[count].destination,
                        departdate: fareresult[count].departdate,
                        returndate: fareresult[count].returndate,
                        status: "0"
                    });
                }
            }
            /*Search Location Error Function*/
            function Saveerror(result) {
            }


            $(document).ready(function () {
                $("#btnGenerate").click(function () {

                    /*Show Possible Combination on UI*/
                    //ShowFareCombination();

                    $("#FareDetailH1").css({
                        display: "block"
                    });
                    $("#FareDetail").css({
                        display: "block"
                    });
                    $("#FareDetailH1").css({
                        display: "block"
                    });

                    $("#<%= hdnFare.ClientID %>").val(JSON.stringify(Fares));

                    $("#FareDetail tbody tr").each(function (idx, tr) {
                        var trdata = $(tr);
                        trdata[0].children[4].innerHTML = "<img src='../Images/wait_small.gif' alt='approved' style='display: block;' id='imgWait'/>";
                    });

                    /*Call Web Service to download fare and save into database*/
                    DownloadFare();

                });
            });


            function ShowFareCombination() {

                $.ajax({
                    type: "POST",
                    url: pageurl + "/GetFares",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    responseType: "json",
                    success: function (result) { FareCombinationSUCCESS(result); },
                    error: FareCombinationERROR
                });
            }

            function FareCombinationSUCCESS(result) {


                $("#FareDetailH1").css({
                    display: "none"
                });
                $("#FareDetail").css({
                    display: "none"
                });

                var fareresult = JSON.parse(result.d);
                for (var count = 0; count < fareresult.length; count++) {

                    $("#FareDetail tbody").append("<tr>" + "<td>" + (fareresult[count].origin) + "</td>" + "<td>" + (fareresult[count].destination) + "</td>" +
                                "<td>" + (fareresult[count].departdate) + "</td>" + "<td>" + (fareresult[count].returndate) + "</td>" +
                                "<td><img src='../Images/pending.gif' alt='pending' style='display: block;' id='imgPending'/></td>" + "</tr>");

                    Fares.push({
                        origin: fareresult[count].origin,
                        destination: fareresult[count].destination,
                        departdate: fareresult[count].departdate,
                        returndate: fareresult[count].returndate,
                        status: "0"
                    });
                }


            }

            function FareCombinationERROR(result) {
            }


            function DownloadFare() {

                var classtype = $("#<%= drpClass.ClientID %> :selected").val();
                var txtAmtPer = $("#<%= txtAmtPer.ClientID %>").val();
                var txtcurrency= $("#<%= ddlCurrency.ClientID %>").val();
                $.ajax({
                    type: "POST",
                    url: pageurl + "/DownloadFare",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({ fare: Fares, cabin: classtype, AmtLessPercent: txtAmtPer, currency: txtcurrency }),
                    responseType: "json",
                    success: function (result) { success(result); },
                    error: error
                });

                CheckStatus();

            }

            function success(result) {

                var output = JSON.parse(result.d);
                $("#FareDetail tbody tr").each(function (idx, tr) {

                    var trdata = $(tr);
                    if (tr.children[0].innerHTML == output.origin && tr.children[1].innerHTML == output.destination && tr.children[2].innerHTML == output.departdate &&
                                    tr.children[3].innerHTML == output.returndate && output.status == "1") {

                        trdata[0].children[4].innerHTML = "<img src='../Images/approval.gif' alt='approved' style='display: block;' id='imgApproved'/>";

                    }
                    else {
                        trdata[0].children[4].innerHTML = "<img src='../Images/approval.gif' alt='approved' style='display: block;' id='imgApproved'/>";
                    }

                    Output.push({
                        origin: output[idx].origin,
                        destination: output[idx].destination,
                        departdate: output[idx].departdate,
                        returndate: output[idx].returndate,
                        status: output[idx].status
                    });

                });


                $("#btnViewFare").css({
                    display: "block"
                });
                $("#btndwnload").css({
                    display: "none"
                });
                $("#FareResult").css({
                    display: "none"
                });
            }

            function error(result) {

            }
            function CheckStatus() {
                setInterval(function () {
                    $.ajax({
                        type: "POST",
                        url: pageurl + "/CheckStatus",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        responseType: "json",
                        success: function (result) { Checksuccess(result); },
                        error: Checkerror
                    });
                }, 5000);
            }
            function Checksuccess(result) {
                var output = JSON.parse(result.d);
                $("#FareDetail tbody tr").each(function (idx, tr) {
                    var trdata = $(tr);

                    for (var count = 0; count < output.length; count++) {

                        if (tr.children[0].innerHTML == output[count].origin && tr.children[1].innerHTML == output[count].destination && tr.children[2].innerHTML == output[count].departdate &&
                                                    tr.children[3].innerHTML == output[count].returndate && output[count].status == "1") {

                            trdata[0].children[4].innerHTML = "<img src='../Images/approval.gif' alt='approved' style='display: block;' id='imgApproved'/>";
                        }
                        else if (tr.children[0].innerHTML == output[count].origin && tr.children[1].innerHTML == output[count].destination && tr.children[2].innerHTML == output[count].departdate &&
                                                    tr.children[3].innerHTML == output[count].returndate && output[count].status == "-1") {
                            trdata[0].children[4].innerHTML = "<img src='../Images/error.gif' alt='approved' style='display: block;'  title='" + output[count].stautsDescription + "'  id='imgApproved'/>";
                        }
                    }
                });
            }

            function Checkerror(result) {

            }
            $(document).ready(function () {
                $("#btnViewFare").click(function () {

                    $("#Hid").css({
                        display: "block"
                    });

                    $("#FareResult").css({
                        display: "block"
                    });

                    $("#btndwnload").css({
                        display: "block"
                    });

                    $.ajax({
                        type: "POST",
                        url: pageurl + "/ViewFares",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        responseType: "json",
                        success: function (result) { ViewFareSuccess(result); },
                        error: ViewFareError
                    });
                });

            });


            function ViewFareSuccess(result) {


                var rowCount = $("#FareResult tbody tr").length;

                if (rowCount > 0) {
                    $("#FareResult").find("tr:gt(0)").remove();
                }

                var fareresult = JSON.parse(result.d);

                for (var count = 0; count < fareresult.length; count++) {

                    $("#FareResult tbody").append("<tr>" +
                                "<td>" + fareresult[count].From + "</td>" +
                                "<td>" + fareresult[count].DestfromName + "</td>" +
                                "<td>" + fareresult[count].To + "</td>" +
                                "<td>" + fareresult[count].DesttoName + "</td>" +
                                "<td>" + fareresult[count].AvailSeats + "</td>" +
                                "<td>" + fareresult[count].Travel_DateStart + "</td>" +
                                "<td>" + fareresult[count].Travel_DateEnd + "</td>" +
                                "<td>" + fareresult[count].Airline_Name + "</td>" +
                                "<td>" + fareresult[count].Airline_Code + "</td>" +
                                "<td>" + fareresult[count].Class + "</td>" +
                                "<td>" + fareresult[count].ClassType + "</td>" +
                                "<td>" + fareresult[count].BaseFare + "</td>" +
                                "<td>" + fareresult[count].Tax + "</td>" +
                                "<td>" + fareresult[count].Total + "</td>" +
                                "<td>" + fareresult[count].FilledBy + "</td>" +
                                "<td>" + fareresult[count].FillDate + "</td>" +
                                "<td>" + fareresult[count].Directflt + "</td>" +
                                "<td>" + fareresult[count].Country + "</td>" +
                                "<td>" + fareresult[count].Country_Code + "</td>" +
                                "<td>" + fareresult[count].Continent_Name + "</td>" +
                                "<td>" + fareresult[count].Continent_Code + "</td>" +
                                "<td>" + fareresult[count].Markup + "</td>" +
                                "<td>" + fareresult[count].Provider + "</td>" +
                                "</tr>"
                                );

                }

            }

            function ViewFareError(result) {
            }




            $(document).ready(function () {
                $("#btndwnload").click(function () {
                    tableToExcel("FareResult", "FareResult");
                });

            });

            var tableToExcel = (function () {

                var uri = 'data:application/vnd.ms-excel;base64,'
                            , template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table>{table}</table></body></html>'
                            , base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) }
                            , format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) }
                return function (table, name) {
                    if (!table.nodeType) table = document.getElementById(table)
                    var ctx = { worksheet: name || 'Worksheet', table: table.innerHTML }
                    window.location.href = uri + base64(format(template, ctx))
                }
            })()
        });


    </script>
   
</asp:Content>
