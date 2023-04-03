<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="BlackoutReturn.aspx.cs" Inherits="Admin_BlackoutReturn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   <%-- <link href="../css/StyleSheet.css" rel="stylesheet" />--%>
    <link href="../../css/StyleSheet.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <div class="container">
        <div style="height: 15px;"></div>
        <div class="panel panel-default">
            <div class="panel-heading">Blackout Return Date</div>

            <div class="panel-body" style="line-height: 34px;">
                <div class="row">
                    <div class="col-md-2">
                        <label>Destination</label>
                        <asp:TextBox ID="txtDetination" runat="server" CssClass="form-control" PlaceHolder="Destination Code"  MaxLength="3"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvDestination" runat="server" ErrorMessage="invalid destination" ForeColor="Red" ControlToValidate="txtDetination" ValidationGroup="backoutRtDt"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="invalid destination" ForeColor="Red" ValidationExpression="^[a-zA-Z]{3}" ControlToValidate="txtDetination" ValidationGroup="backoutRtDt" ></asp:RegularExpressionValidator>
                    </div>
                    <div class="col-md-6">
                        <label>Airline <smal>(multiple airline code can put using comma(,) delimeter)</smal></label>
                        <asp:TextBox ID="txtAirline" runat="server" CssClass="form-control" PlaceHolder="Airline Code"   ></asp:TextBox>
                        <asp:RegularExpressionValidator ID="revAirline" runat="server" ErrorMessage="invalid airline(s) code" ForeColor="Red" ValidationExpression="^[a-zA-Z0-9]{2}(,[a-zA-Z0-9]{2})*$" ControlToValidate="txtAirline" ValidationGroup="backoutRtDt" ></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="rfvAirline" runat="server" ErrorMessage="invalid airline(s) code" ForeColor="Red" ControlToValidate="txtAirline" ValidationGroup="backoutRtDt"></asp:RequiredFieldValidator>
                    </div>
                    
                    <div class="col-md-2">
                        <label>From Date</label>
                       <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" PlaceHolder="Blackout From"  MaxLength="10"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Invalid Blackout From Date" ForeColor="Red" ControlToValidate="txtFromDate" ValidationGroup="backoutRtDt"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Invalid Blackout From Date" ForeColor="Red" ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$" ControlToValidate="txtFromDate" ValidationGroup="backoutRtDt" ></asp:RegularExpressionValidator>
                    </div>
                    <div class="col-md-2">
                        <label>To Date</label>
                       <asp:TextBox ID="txtTodate" runat="server" CssClass="form-control" PlaceHolder="Blackout From"  MaxLength="10"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Invalid Blackout To Date" ForeColor="Red" ControlToValidate="txtTodate" ValidationGroup="backoutRtDt"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Invalid Blackout To Daten" ForeColor="Red" ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$" ControlToValidate="txtTodate" ValidationGroup="backoutRtDt" ></asp:RegularExpressionValidator>
                    </div>
                </div>
                <div class="row">


                    <div class="col-md-6">
                        <label>
                            <br />
                            &nbsp;
                        </label>
                        <asp:Button ID="btnInsert" runat="server"  CssClass="btn btn-primary btn-lg" Text="Insert" OnClick="btnSubmit_Click" ValidationGroup="backoutRtDt" />
                        <asp:Button ID="btnSearch" runat="server" OnClientClick="return SearchCall();" CssClass="btn btn-primary btn-lg" Text="Search" OnClick="btnSearch_Click" />
                        
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
                    <asp:Repeater ID="rptrDetails" runat="server" >
                        <HeaderTemplate>
                            <table cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td class='gdvh'>SrNo</td>
                                    <td class="gdvh">Airline</td>
                                     <td class="gdvh">Destination</td>
                                    <td class="gdvh">From Date</td>
                                    <td class="gdvh">To Date</td>
                                    <td class="gdvh">Last Modify</td>
                                    <td class="gdvh">Modify By</td>
                                    <td class="gdvh">&nbsp;<br />
                                    </td>

                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <tr id='tr<%# Eval("SN")%>'>
                                    <td class='gdvr'><%# Container.ItemIndex+ 1 %></td>
                                    <td class='gdvr'><%# Eval("AirlineCode")%></td>
                                     <td class='gdvr'><%# Eval("Destination")%></td>
                                    <td class='gdvr'><%# Eval("FromDate","{0:dd/MM/yyyy}")%></td>
                                     <td class='gdvr'><%# Eval("ToDate","{0:dd/MM/yyyy}")%></td>
                                    <td class='gdvr'><%# Eval("ModifyDatenTime","{0:ddMMMyyyy HH:mm}")%></td>
                                    <td class='gdvr'><%# Eval("ModifyBy")%></td>
                                    <td class='gdvr'>
                                        <img src='../images/CardDelete.png' style='cursor: pointer;' onclick="DeleteBlackout(<%# Eval("SN")%>);" />
                                    </td>
                                </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>

                </div>
            </div>
        </div>
    </div>
    <input id="setascurrdate" type="hidden" />
    <input id="hdeprdate" type="hidden" />
    <asp:HiddenField ID="hfSrNo" runat="server" />
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

        function SearchCall() {
            popup("popProgressBar", 40, 40);
            return true;
        }

        function DeleteBlackout(ID) {
            if (confirm("Are you sure delete")) {
                popup('popProgressBar', 30, 30);
                $.ajax({
                    type: "POST",
                    url: "BlackoutReturn.aspx/DeleteBlackout",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({ ID: ID }),
                    responseType: "json",
                    success: function (data) {
                        var jsdata = JSON.parse(data.d);
                        popup('popProgressBar', 30, 30);
                        if (data.d == "true") {
                            $("#tr" + ID).hide();

                        }
                        else
                            alert("Record deleted from database");
                    },
                    error: function (data) { popup('popProgressBar', 30, 30); }
                });
            }
        }
    </script>
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
</asp:Content>

