<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="FollowUp.aspx.cs" Inherits="Admin_FollowUp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
      <link href="../../css/StyleSheet.css" rel="stylesheet" />
    <script src="../../js/CalendarAnyYear.js"></script>
     <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
    <script src="../../js/enquiry.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container">
        <div style="height: 15px;"></div>
        <div class="panel panel-default">
            <div class="panel-heading">Follow Up</div>

            <div class="panel-body">
                <div class="row">
                   
                    
                    <div class="col-md-4">
                        <span>Follow up Date</span>
                        <asp:TextBox class="form-control" ID="txtFromDate" onclick="showCalender(this);" runat="server" />
                    </div>
                    <div class="col-md-6">
                        <span>Remarks</span>
                        <asp:TextBox class="form-control" ID="txtRem" runat="server" />
                    </div>
                    <div class="col-md-2">
<span><br /></span>
                        <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-success" Text="Add Follow Up" OnClientClick="return validate();" OnClick="btnSubmit_Click" />
                        <asp:HiddenField ID="hfXP" runat="server" />
                        <asp:Literal ID="ltrmsg" runat="server"></asp:Literal>
                    </div>
                </div>
                <div class="row">
                    <asp:Repeater ID="rpFollows" runat="server">
                        <HeaderTemplate>
                            <div class="row well-sm">
                                <div class="col-md-2">Booking ID</div>
                                <div class="col-md-2">Follow Date</div>
                                <div class="col-md-4">Remarks</div>
                                <div class="col-md-2">Created By</div>
                                <div class="col-md-2">reated Date</div>
                                

                            </div>
                   
                    
                    
                        </HeaderTemplate>
                        <ItemTemplate>
                            <div class="row">
                                <div class="col-md-2"><%#Eval("BKG_FLW_Ref_No")%></div>
                                <div class="col-md-2"><%#Eval("BKG_FLW_DateTime", "{0: dd MMM yyyy}")%></div>
                                <div class="col-md-4"><%#Eval("BKG_FLW_Remarks")%></div>
                                <div class="col-md-2"><%#Eval("BKG_FLW_Remarks_By")%></div>
                                <div class="col-md-2"><%#Eval("BKG_FLW_CrDate")%></div>
                                

                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
                

             

                <input id="setascurrdate" type="hidden" />
                <input id="hdeprdate" type="hidden" />
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
                                    <img src="../../Images/Wait.gif" id="ImageProgressbar">
                                </td>
                            </tr>
                            <tr>
                                <td style="background-color: #ffffff; color: #B9B9B9; vertical-align: middle; text-align: center; font-size: 18px; font-family: Verdana;" align="center" height="40"></td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>


    </div>
    <script  type="text/javascript" src="<%= ResolveUrl("~/javascripts/bootstrap.js") %>"></script>
   
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
        function validate() {
            var contno=document.getElementById('<%= txtFromDate.ClientID %>').value;
            if(contno=="")
            {
                alert("Enter date.");
                document.getElementById('<%= txtFromDate.ClientID %>').focus();
                return false;
            }
            var contnoo=document.getElementById('<%= txtRem.ClientID %>').value;
            if(contnoo=="")
            {
                alert("Enter Remarks.");
                document.getElementById('<%= txtRem.ClientID %>').focus();
                return false;
            }
        }
       

    </script>

    
</asp:Content>
