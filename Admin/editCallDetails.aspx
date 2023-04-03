<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="editCallDetails.aspx.cs" Inherits="Admin_editCallDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
   <%-- <link href="../css/StyleSheet.css" rel="stylesheet" />--%>
    <link href="../../css/StyleSheet.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.6/jquery.min.js" type="text/javascript"></script>
    <script src="//ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js"
        type="text/javascript"></script>
    <link href="//ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css"
        rel="Stylesheet" type="text/css" />
    <script>
        $(document).ready(function () {
            $('#<%=txtOBDate.ClientID %>').datepicker({
                minDate: 0,
                maxDate: "+360D",
                numberOfMonths: 2,
                dateFormat: "dd/mm/yy",
                onSelect: function (selected) {
                    $('#<%=txtIBDate.ClientID %>').datepicker("option", "minDate", selected)
                }
            });
            $('#<%=txtIBDate.ClientID %>').datepicker({
                minDate: 0,
                maxDate: "+360D",
                numberOfMonths: 2,
                dateFormat: "dd/mm/yy",
                onSelect: function (selected) {
                    $('#<%=txtOBDate.ClientID %>').datepicker("option", "maxDate", selected)
                }
            });

        });
    </script>


    <div class="container-fluid">
        <div style="height: 15px;"></div>
        <div class="panel panel-default">
            <div class="panel-heading">
                Edit Calls/Enquiries&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblRef" runat="server" Text=""></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(<asp:Label ID="lblAgent" runat="server" Text=""></asp:Label>)
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="panel-body" style="line-height: 34px;">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label for="usr">Pax Name:</label>
                                        <input type="text" class="form-control" id="txtPaxName" runat="server" />
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="row">
                                        <div class="form-group">
                                            <div class="col-md-4">
                                                <label for="usr">ADT:</label>
                                                <input type="number" value="1" min="0" max="10" class="form-control" id="txtAdults" runat="server" data-bind="value: replyNumber" />
                                            </div>
                                            <div class="col-md-4">
                                                <label for="usr">CHD:</label>
                                                <input type="number" value="0" min="0" max="10" class="form-control" id="txtChilds" runat="server" data-bind="value: replyNumber" />
                                            </div>
                                            <div class="col-md-4">
                                                <label for="usr">INF:</label>
                                                <input type="number" value="0" min="0" max="5" class="form-control" id="Infants" runat="server" data-bind="value: replyNumber" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label for="usr">Contact No:</label>
                                        <input type="number" class="form-control" id="txtContact" min="0" runat="server" />
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label for="usr">Email ID:</label>
                                        <input type="email" class="form-control" id="txtEmail" runat="server" />
                                    </div>
                                </div>
                            </div>

                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label for="usr">Origin:</label>
                                        <input type="text" class="form-control" maxlength="3" id="txtOrigin" runat="server" />
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label for="usr">Destination:</label>
                                        <input type="text" class="form-control" maxlength="3" id="txtDestination" runat="server" />
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label for="usr">Outbound Date:</label>
                                        <input type="text" class="form-control" id="txtOBDate" runat="server" />
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label for="usr">Inbound Date:</label>
                                        <input type="text" class="form-control" id="txtIBDate" runat="server" />
                                    </div>
                                </div>
                            </div>

                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label for="usr">Brand Name:</label>
                                        <asp:DropDownList CssClass="form-control" ID="ddlBrand" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBrand_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label for="usr">Campaign Name:</label>
                                        <asp:DropDownList CssClass="form-control" ID="ddlSourceMedia" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label for="usr">Reason:</label>
                                        <asp:DropDownList ID="ddlReason" CssClass="form-control" runat="server">
                                            <asp:ListItem Text="New Inquiry" Value="New Inquiry"></asp:ListItem>
                                            <asp:ListItem Text="Special Request" Value="Special Request"></asp:ListItem>
                                            <asp:ListItem Text="Refund" Value="Refund"></asp:ListItem>
                                            <asp:ListItem Text="Cancellation" Value="Cancellation"></asp:ListItem>
                                            <asp:ListItem Text="Amendment" Value="Amendment"></asp:ListItem>
                                            <asp:ListItem Text="Call Transfer" Value="Call Transfer"></asp:ListItem>
                                            <asp:ListItem Text="Complain" Value="Complain"></asp:ListItem>
                                             <asp:ListItem Text="Customer Service" Value="Customer Service"></asp:ListItem>
                                            
                                            <asp:ListItem Text="Others" Value="Others"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label for="usr">Medium:</label>
                                        <asp:DropDownList ID="ddlSource" CssClass="form-control" runat="server">
                                            <asp:ListItem Text="Phone" Value="Phone"></asp:ListItem>
                                            <asp:ListItem Text="Inquiry mail" Value="Inquiry mail"></asp:ListItem>
                                            <asp:ListItem Text="Chat" Value="Chat"></asp:ListItem>
                                            <asp:ListItem Text="Facebook" Value="Facebook"></asp:ListItem>
                                            <asp:ListItem Text="Others" Value="Others"></asp:ListItem>
                                        </asp:DropDownList>

                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label for="usr">Status:</label>
                                        <asp:DropDownList ID="ddlStatus" CssClass="form-control" runat="server">
                                            <asp:ListItem Text="Any" Value=""></asp:ListItem>
                                            <asp:ListItem Text="Booked" Value="Booked"></asp:ListItem>
                                            <asp:ListItem Text="Follow Up" Value="Follow Up"></asp:ListItem>
                                            <asp:ListItem Text="Resolved" Value="Resolved"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3" id="dvBookingId" style="display: none">
                                    <div class="form-group">
                                        <label for="usr">Booking ID:</label>
                                        <input type="text" class="form-control" id="bookingID" runat="server" />
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label for="usr">Type</label>
                                        <select class="form-control" id="ddlType" runat="server">
                                            <option selected="selected" value="CL">Call</option>
                                            <option value="EN">Enquiry</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label for="usr">Airline:</label>
                                        <input type="text" class="form-control" maxlength="2" id="txtAirline" runat="server" />
                                    </div>
                                </div>
                                <%--<div class="col-md-3">
                                    <div class="form-group">
                                        <label for="usr">Agent</label>
                                        <input type="text" class="form-control" id="txtAgent" runat="server" />
                                    </div>
                                </div>--%>
                                <%--<div class="col-md-3">
                                    <div class="form-group">
                                        <label for="usr">Creation Date:</label>
                                        <input type="text" class="form-control" id="txtCreationDate" runat="server" />
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label for="usr">Creation Date To:</label>
                                        <input type="text" class="form-control" id="txtCreationdateTo" runat="server" />
                                    </div>
                                </div>--%>
                            </div>
                        </div>

                       <%-- <div class="row">
                            <div class="col-md-12">
                                
                            </div>
                        </div>--%>

                        <div class="col-md-12">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label for="usr">Remarks:</label>
                                    <textarea type="text" class="form-control" id="txtRemarks" runat="server"></textarea>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label for="usr"></label>

                                    <asp:Button ID="btnUpdate" CssClass="btn btn-primary btn-block" OnClientClick="return validate()" runat="server" Text="Update" OnClick="btnUpdate_Click" />
                                    <asp:Label ID="lblMsg" ForeColor="Red" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>




    <asp:HiddenField ID="hfUpdatedBy" runat="server" />
    <asp:HiddenField ID="hfID" runat="server" />

    <script type="text/javascript">

        function validate() {
            var bname = document.getElementById('<%= ddlBrand.ClientID %>').value;
            if (bname == "" || bname == "--Select--") {
                alert("Select Brand.");
                document.getElementById('<%= ddlBrand.ClientID %>').focus();
                return false;
            }
            var contno = document.getElementById('<%= txtContact.ClientID %>').value;
            if (contno == "") {
                alert("Enter Contact No.");
                document.getElementById('<%= txtContact.ClientID %>').focus();
                return false;
            }
        }

        $(document).ready(function ()
        {
            var ddlValue = $('#<%= ddlStatus.ClientID %>').val();
            if (ddlValue == "Booked") {
                $("#dvBookingId").show();
            }
            else {
                $("#dvBookingId").hide();
            }
        });


        $('#<%= ddlStatus.ClientID %>').change(function ()
        {
            var ddlValue = $('#<%= ddlStatus.ClientID %>').val();
            if (ddlValue == "Booked") {
                $("#dvBookingId").show();
            }
            else {
                $("#dvBookingId").hide();
            }
        });
    </script>
</asp:Content>

