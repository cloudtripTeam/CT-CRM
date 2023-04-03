<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="AgentTargetMaster.aspx.cs" Inherits="Admin_AgentTargetMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
        <style type="text/css">
        .checkbox
        {
            padding-left: 20px;
        }
        .checkbox label
        {
            margin-left: 80px;
            display: inline-block;
            vertical-align: middle;
            position: relative;
            padding-left: 5px;
            width: 100px;
        }
        .checkbox label::before
        {
            content: "";
            display: inline-block;
            position: absolute;
            width: 17px;
            height: 17px;
            left: 0;
            margin-left: -20px;
            border: 1px solid #cccccc;
            border-radius: 3px;
            background-color: #fff;
            -webkit-transition: border 0.15s ease-in-out, color 0.15s ease-in-out;
            -o-transition: border 0.15s ease-in-out, color 0.15s ease-in-out;
            transition: border 0.15s ease-in-out, color 0.15s ease-in-out;
        }
        .checkbox label::after
        {
            display: inline-block;
            position: absolute;
            width: 16px;
            height: 16px;
            left: 0;
            top: 0;
            margin-left: -20px;
            padding-left: 3px;
            padding-top: 1px;
            font-size: 11px;
            color: #555555;
        }
        .checkbox input[type="checkbox"]
        {
            opacity: 0;
            z-index: 1;
        }
        .checkbox input[type="checkbox"]:checked + label::after
        {
            font-family: "FontAwesome";
            content: "\f00c";
        }
         
        .checkbox-primary input[type="checkbox"]:checked + label::before
        {
            background-color: #337ab7;
            border-color: #337ab7;
        }
        .checkbox-primary input[type="checkbox"]:checked + label::after
        {
            color: #fff;
        }
    </style>
    <div class="p-20" style="background: #fff;">
        <table class="table table-hover">
            <tr>
                <td style="width:10%">Department : </td>
                <td style="width:10%">
                    <asp:DropDownList ID="ddlRoll" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlRoll_SelectedIndexChanged"></asp:DropDownList></td>
                <td style="width:5%">Agents :</td>
                <td style="width:75%">
                 <div class="checkbox checkbox-primary">   <asp:CheckBoxList ID="chkAgentList" RepeatColumns="5" runat="server" RepeatDirection="Horizontal" TextAlign="Right" RepeatLayout="Flow" Font-Bold="False" Font-Names="Arial" Width="100%" CssClass="styled"></asp:CheckBoxList></div> </td>
            </tr>
        </table>
        <table class="table table-hover">
            <thead>
                <tr>
                    <th>Profit</th>
                    <th>No. Of Booking</th>
                    <th>No.Of Sectors</th>
                    <th>Month</th>
                    <th>Year</th>
                </tr>
            </thead>
            <tbody>
                
                <tr>
                    <td>
                        <asp:TextBox ID="txtProfit" CssClass="form-control" runat="server" onkeyup="checkDec(this);"></asp:TextBox></td>
                    <td>
                        <asp:TextBox ID="txtNoBooking" CssClass="form-control" runat="server"></asp:TextBox></td>
                    <td>
                        <asp:TextBox ID="txtSectors" CssClass="form-control" runat="server"></asp:TextBox></td>
                    <td>
                        <asp:DropDownList ID="ddlMonth" CssClass="form-control" runat="server">
                            <asp:ListItem Value="0">Month</asp:ListItem>
                            <asp:ListItem Value="01">Jan</asp:ListItem>
                            <asp:ListItem Value="02">Feb</asp:ListItem>
                            <asp:ListItem Value="03">Mar</asp:ListItem>
                            <asp:ListItem Value="04">Apr</asp:ListItem>
                            <asp:ListItem Value="05">May</asp:ListItem>
                            <asp:ListItem Value="06">Jun</asp:ListItem>
                            <asp:ListItem Value="07">Jul</asp:ListItem>
                            <asp:ListItem Value="08">Aug</asp:ListItem>
                            <asp:ListItem Value="09">Sep</asp:ListItem>
                            <asp:ListItem Value="10">Oct</asp:ListItem>
                            <asp:ListItem Value="11">Nov</asp:ListItem>
                            <asp:ListItem Value="12">Dec</asp:ListItem>
                        </asp:DropDownList></td>
                    <td>
                        <asp:DropDownList ID="ddlYear" CssClass="form-control" runat="server">
                            <asp:ListItem Value="0">Year</asp:ListItem>
                            <asp:ListItem Value="2017">2017</asp:ListItem>
                            <asp:ListItem Value="2018">2018</asp:ListItem>
                            <asp:ListItem Value="2019">2019</asp:ListItem>
                            <asp:ListItem Value="2020">2020</asp:ListItem>
                            <asp:ListItem Value="2021">2021</asp:ListItem>
                            <asp:ListItem Value="2022">2022</asp:ListItem>
                        </asp:DropDownList></td>
                </tr>
                
                <tr>
                <td style="text-align: center; vertical-align: middle;" colspan="6">
                    <asp:Button ID="btnSubmit" OnClientClick="return validate()" CssClass="btn btn-default" runat="server" Text="Add" OnClick="btnSubmit_Click" /><asp:Button ID="btnSearch" CssClass="btn btn-default" runat="server" Text="Search" OnClick="btnSearch_Click" /><asp:Literal ID="ltrMsg" runat="server"></asp:Literal></td>
                    </tr>
                <tr>
                    <td colspan="6">
                        <table class="table table-hover">
                        <asp:Repeater ID="rptAgentTarget" runat="server" OnItemCommand="rptAgentTarget_ItemCommand">
                            <HeaderTemplate>
                                <thead>
                                <tr>
                                    <th>Agent</th>
                                    <th>Profit</th>
                                    <th>No. Of Bookings</th>
                                    <th>No. Of Sectors</th>
                                    <th>Assign By</th>
                                    <th>Month</th>
                                    <th>Year</th>
                                    <th></th>
                                </tr>
                                    </thead>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tbody>
                                    <tr>
                                        <td><asp:TextBox ID="Label1" CssClass="form-control" runat="server" Text='<%# Bind("Agent_Tar_Agent_ID") %>'></asp:TextBox></td>
                                        <td><asp:TextBox ID="Label2" CssClass="form-control" runat="server" Text='<%# Bind("Agent_Tar_Profit") %>'></asp:TextBox></td>
                                        <td><asp:TextBox ID="Label3" CssClass="form-control" runat="server" Text='<%# Bind("Agent_Tar_No_Booking") %>'></asp:TextBox></td>
                                        <td><asp:TextBox ID="Label4" CssClass="form-control" runat="server" Text='<%# Bind("Agent_Tar_No_Sectors") %>'></asp:TextBox></td>
                                        <td><asp:Label ID="Label5" ReadOnly="true" CssClass="form-control" runat="server" Text='<%# Bind("Agent_Tar_Assign_By") %>'></asp:Label></td>
                                        <td><asp:TextBox ID="Label6" CssClass="form-control" runat="server" Text='<%# Bind("Agent_Tar_Month") %>'></asp:TextBox></td>
                                        <td><asp:TextBox ID="Label7" CssClass="form-control" runat="server" Text='<%# Bind("Agent_Tar_Year") %>'></asp:TextBox></td>
                                        <td>
                                            <asp:HiddenField ID="hfid" Value='<%# Bind("Agent_Tar_ID") %>' runat="server" />
                                            <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-default" CommandName="update" /></td>
                                    </tr>
                                </tbody>
                            </ItemTemplate>
                        </asp:Repeater>
                            </table>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <script>
        function validate() {
            if (document.getElementById('<%=txtProfit.ClientID %>').value == "") {
                alert("Enter Profit Target !!!");
                document.getElementById('<%=txtProfit.ClientID %>').focus();
                return false;
            }
        }
        function checkDec(el) {
            var ex = /^[0-9]+\.?[0-9]*$/;
            if (ex.test(el.value) == false) {
                el.value = el.value.substring(0, el.value.length - 1);
            }
        }
    </script>
</asp:Content>

