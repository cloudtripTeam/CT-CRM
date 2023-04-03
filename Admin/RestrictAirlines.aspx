<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="RestrictAirlines.aspx.cs" Inherits="Admin_RestrictAirlines" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%--<link href="../css/StyleSheet.css" rel="stylesheet" />--%>
    <link href="../../css/StyleSheet.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="container-fluid">
        <div style="height: 15px;"></div>
        <div class="panel panel-default">
            <div class="panel-heading">Restrict Airline</div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                <div class="panel-body" style="line-height: 34px;">
                <div class="row">
                    <div class="col-md-2">
                        <label>Destination</label>
                        <asp:TextBox ID="txtDetination" runat="server" CssClass="form-control" PlaceHolder="Destination Code"  MaxLength="3"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvDestination" runat="server" ErrorMessage="invalid destination" ForeColor="Red" ControlToValidate="txtDetination" ValidationGroup="restrictedAirlines"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="invalid destination" ForeColor="Red" ValidationExpression="^[a-zA-Z]{3}" ControlToValidate="txtDetination" ValidationGroup="restrictedAirlines" ></asp:RegularExpressionValidator>
                    </div>
                    <div class="col-md-6">
                        <label>Airline <smal>(multiple airline code can put using comma(,) delimeter)</smal></label>
                        <asp:TextBox ID="txtAirline" runat="server" CssClass="form-control" PlaceHolder="Airline Code"   ></asp:TextBox>
                        <asp:RegularExpressionValidator ID="revAirline" runat="server" ErrorMessage="invalid airline(s) code" ForeColor="Red" ValidationExpression="^[a-zA-Z0-9]{2}(,[a-zA-Z0-9]{2})*$" ControlToValidate="txtAirline" ValidationGroup="restrictedAirlines" ></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="rfvAirline" runat="server" ErrorMessage="invalid airline(s) code" ForeColor="Red" ControlToValidate="txtAirline" ValidationGroup="restrictedAirlines"></asp:RequiredFieldValidator>
                    </div>
                    
                    <div class="col-md-2">
                        <label>Company</label>
                        <asp:DropDownList ID="ddlCompany" AutoPostBack="true" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged">
                            
                        </asp:DropDownList>
                    </div>

                    <div class="col-md-2">
                        <label>Campaign (view)</label>
                        <asp:DropDownList ID="ddlCampaign" runat="server" CssClass="form-control">
                            
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row">


                    <div class="col-md-6">
                        <label>
                            <br />
                            &nbsp;
                        </label>
                        <asp:Button ID="btnInsert" runat="server"  CssClass="btn btn-primary btn-lg" Text="Insert" OnClick="btnSubmit_Click" ValidationGroup="restrictedAirlines" />
                        <asp:Button ID="btnSearch" runat="server"  CssClass="btn btn-primary btn-lg" Text="Search" OnClick="btnSearch_Click" />
                        
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
                                     <td class="gdvh">GDS</td>
                                     <td class="gdvh">Destination</td>
                                    <td class="gdvh">Company</td>
                                    <td class="gdvh">Last Modify</td>
                                    <td class="gdvh">Modify By</td>
                                    <td class="gdvh">&nbsp;<br />
                                    </td>

                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <tr id='tr<%# Eval("SNo")%>'>
                                    <td class='gdvr'><%# Container.ItemIndex+ 1 %></td>
                                    <td class='gdvr'><%# Eval("Airline")%> </td>
                                      <td class='gdvr'><%# Eval("Campaign")%> </td>
                                     <td class='gdvr'><%# Eval("Destination")%></td>
                                    <td class='gdvr'><%# Eval("Company")%></td>
                                    <td class='gdvr'><%# Eval("Last_Modification","{0:ddMMMyyyy HH:mm}")%></td>
                                    <td class='gdvr'><%# Eval("ModifiedBy")%></td>
                                    <td class='gdvr'>
                                        <img src='../images/CardDelete.png' style='cursor: pointer;' onclick="DeleteAirline(<%# Eval("SNo")%>);" />
                                    </td>
                                </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>

                </div>
            </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                <ProgressTemplate>
                    Please wait...
                </ProgressTemplate>
            </asp:UpdateProgress>
           
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
        
        function DeleteAirline(ID) {
            if (confirm("Are you sure delete this Own Fare??")) {
                popup('popProgressBar', 30, 30);
                $.ajax({
                    type: "POST",
                    url: "RestrictAirlines.aspx/DeleteAirline",
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
                            alert("Own Fare is not successfully deleted from database!!");
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

