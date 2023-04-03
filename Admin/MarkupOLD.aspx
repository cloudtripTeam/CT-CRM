<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true"
    CodeFile="MarkupOLD.aspx.cs" Inherits="Admin_Markup" %>

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
          <div class="panel panel-default">
            <div class="panel-heading">Mark-up Status<small style="color: greenyellow"><i> - Turn On/Off Mark-ups [FLTTROTT_CA, C2BCA, TRVJUNCTION_CA]</i></small></div>
            <div class="panel-body" style="line-height: 34px;">
                <input type="checkbox" name="checkboxisActive" class="checkbox" runat="server" id="checkboxisActive" clientidmode="Static" />
            </div>
        </div>
        <div style="height: 15px;"></div>
        <div class="panel panel-default">
            <div class="panel-heading">Markup Details</div>
            <div class="panel-body" style="line-height: 20px;">
                <div class="row">
                    <div class="col-md-3">
                        <label>Markup From</label>

                        <asp:TextBox runat="server" ID="txtMarkupFrom" MaxLength='3' CssClass="form-control" placeholder="ANY" />
                    </div>
                    <div class="col-md-3">
                        <label>Markup To</label>
                        <asp:TextBox runat="server" ID="txtMarkupTo" MaxLength='3' CssClass="form-control" placeholder="ANY" />

                    </div>
                    <div class="col-md-3">
                        <label>Airline</label>
                        <asp:TextBox runat="server" CssClass="form-control" MaxLength='2' ID="txtAirV" placeholder="ANY" />

                    </div>
                    <div class="col-md-3">
                         <label>Markup Class</label>

                        <asp:TextBox runat="server" CssClass="form-control" ID="txtMarkupClass" placeholder="ANY" />
                        
                    </div>
                </div>
                <div class="row">
                      <div class="col-md-3">
                        <label>Company</label>
                        <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <label>Campaign</label>
                        <asp:DropDownList ID="ddlCampDetails" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        
                        <label>Amount</label>
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtAmount" placeholder="0.00" />
                    </div>  
                    <div class="col-md-3">
                        <label>Category</label>
                        <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control">
                            <asp:ListItem Value="ANY">ANY</asp:ListItem>
                            <asp:ListItem Value="ECONOMY">ECONOMY</asp:ListItem>
                            <asp:ListItem Value="BUSINESS">BUSINESS</asp:ListItem>
                            <asp:ListItem Value="FIRSTCLASS">FIRSTCLASS</asp:ListItem>
                            <asp:ListItem Value="PREMIUM">PREMIUM</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    
                  
                    
                  
                </div>
                <div class="row">
                   
                    <div class="col-md-3">
                        <label>AmountType</label>
                        <asp:DropDownList ID="DdlAmountType" runat="server" CssClass="form-control">
                            <asp:ListItem Value="A">Amount</asp:ListItem>
                            <asp:ListItem Value="P">Percentage</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                       <label>Journey Type</label>
                        <asp:DropDownList ID="ddlJourneyType" runat="server" CssClass="form-control">
                            <asp:ListItem Value="ANY">ANY</asp:ListItem>
                            <asp:ListItem Value="RT">RETURN</asp:ListItem>
                            <asp:ListItem Value="O">ONEWAY</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                  <div class="col-md-3">
                        <label>Valid From Date</label>
                        <asp:TextBox runat="server" ID="txtFromDate" MaxLength='10' onclick="showCalender(this);" CssClass="form-control" placeholder="dd/mm/yyyy" />
                    </div>
                    <div class="col-md-3">
                        <label>Valid To Date</label>
                        <asp:TextBox runat="server" ID="txtToDate" MaxLength='10' onclick="showCalender(this);" CssClass="form-control" placeholder="dd/mm/yyyy" />
                    </div>
                </div>
                <div class="row">
                   <div class="col-md-6">
                       </div>
                    <div class="col-md-3" style="margin-top: 9px;text-align:right">
                                        
                          
                                 <asp:CheckBox ID="chbSeeInExcel" runat="server" Text="Enable Edit Mode" CssClass="enblecheckbox" />
                    </div>
                 
                    <div class="col-md-3" style="text-align:right" >
                       
                         <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-danger btn-lg" OnClientClick="return AddMarkup();" Text="Add" OnClick="btnAdd_Click" />
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-lg" OnClientClick="return SearchMarkup();" Text="Search" OnClick="btnSearch_Click" />
                    </div>
                </div>

               
                
                <br />
                 <div class="panel panel-default">
            <div class="panel-heading" > 
                <input id ="toggle" value="Show Logic" type="button" class="btn btn-primary btn-xs" />
           </div>
            <div class="panel-body target" style="display:none;background:#333;" >  <input id ="Close" value="&#10005;" type="button" class="btn btn-primary btn-xs" style="float:right" />
              <div class="row" style="" >
                    <p style="color:greenyellow">
                            <span>Rule Implentation logic priority list:</span><br />
                       <small><i>1. Campaign Id</i></small><br />
                        <small><i>2. Destination</i></small><br />
                        <small><i>3. Origin</i></small><br />
                        <small><i>4. Airline</i></small><br />
                        <small><i>5. Class</i></small><br />
                        <small><i>Final: Maximum Amount value will be implemented out of all rules satisfying above.</i></small><br />
                        <small><i>RT- Return, O- OneWay, ANY- ANY</i></small><br />
                            </p>

     
                </div>
            </div>


                      <%--Add Excel Section  Upload , Export and Delete --%>
            <div class="panel panel-default" id="pnlbulkMarkup" runat="server">
                <div class="panel-heading">
                    <div class="pull-left">
                        <h4 class="panel-title">
                            <a data-toggle="collapse" href="#bulkUpdate" class="btn btn-primary btn-xs" aria-expanded="true">Bulk Update Markup</a>
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
                                <asp:Button ID="btnUpload" runat="server" CssClass="btn btn-success btn-sm" OnClientClick="return SearchCall();" Text="Upload Markup Details" OnClick="btnUpload_Click" />
                            </div>
                            <div class="col-md-2 col-md-offset-3">
                                <asp:Button ID="Button1" runat="server" Text="Export Markup" CssClass="btn btn-primary btn-sm"
                                    OnClick="btnExport_Click" />
                            </div>
                            <div class="col-md-2">
                                <asp:Button ID="btnDeleteAll" runat="server" CssClass="btn btn-danger btn-sm" OnClientClick="return confirm('Are you sure delete all markup?')" Text="Delete All Markup"
                                    OnClick="btnDeleteAll_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>



            <%--End --%>
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
            <div class="panel-heading">Markup Details</div>
            <div class="panel-body" style="line-height: 34px; padding: 0px!important;">
                <asp:Repeater ID="rptrDetails" runat="server">
                    <HeaderTemplate>
                        <table width='100%' cellpadding='0' cellspacing='0' class='table' style='margin-bottom: 0px;'>
                            <tr>
                                <td class="gdvf"><asp:CheckBox ID="cbCheckAll" runat="server" Text="" OnClick="checkAll(this)" /></td>
                                <td class='gdvh'>SrNo</td>
                                <td class='gdvh'>Ori</td>
                                <td class='gdvh'>Des</td>
                                <td class='gdvh'>AirV</td>
                               <%-- <td class='gdvh'>Provider</td>--%>
                               <td class='gdvh'>Category</td>
                                <td class='gdvh'>Class</td>
                               <%-- <td class='gdvh'>FareType</td>--%>
                                <td class='gdvh'>J Type</td>
                                <td class='gdvh'>Amount</td>
                                <td class='gdvh'>Amt Type</td>
                                <td class='gdvh'>FromDate</td>
                                <td class='gdvh'>ToDate</td>
                                <td class='gdvh'>CompanyID</td>
                                <td class='gdvh'>CampID</td>
                                 <td class='gdvh'>isActive</td>
                                <td class='gdvh'>UpdatedBy</td>
                                <td class='gdvh'>Action</td>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr id='tr<%# Eval("MarkupID")%>'>
                            <td><asp:CheckBox id="chk" runat="server"/></td>
                            <td class='gdvr'><%# Container.ItemIndex+ 1 %></td>
                            <td class='gdvr'><%# Eval("MarkupFrom")%></td>
                            <td class='gdvr'><%# Eval("MarkupTo")%></td>
                            <td class='gdvr'><%# Eval("AirV")%></td>
                          <%--  <td class='gdvr'><%# Eval("Provider")%></td>--%>
                          <td class='gdvr'><%# Eval("Category")%></td>
                            <td class='gdvr'><%# Eval("MarkupClass")%></td>
                          <%--  <td class='gdvr'><%# Eval("FareType")%></td>--%>
                            <td class='gdvr'><%# Eval("JourneyType")%></td>
                            <td class='gdvr'><%# Eval("Amount")%></td>
                            <td class='gdvr'><%# Eval("AmountType")%></td>
                            <td class='gdvr'><%# Eval("ValidFromDate","{0:dd/MM/yyyy}")%></td>
                            <td class='gdvr'><%# Eval("ValidToDate","{0:dd/MM/yyyy}")%></td>
                            <td class='gdvr'><%# Eval("CompanyID")%></td>
                            <td class='gdvr'><%# Eval("CampID")%></td>
                            <td class='gdvr'><%# Eval("isactive")%></td>
                            <td class='gdvr' title='<%# Eval("ModifiedDate","{0:dd-MM-yyyy HH:mm}")%>'><%# Eval("ModifiedBy")%></td>
                            <td class='gdvr'>
                                <asp:HiddenField ID="hdMkid" Value='<%# Eval("MarkupID")%>' runat="server" />
                                <img src='../images/CardDelete.png' style='cursor: pointer;' onclick="DeleteMarkup(&#39;<%# Eval("MarkupID")%>&#39;);" />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
                <asp:Repeater ID="rptrEdit" runat="server">
                    <HeaderTemplate>
                        <table width='100%' cellpadding='0' cellspacing='0' class='table' style='margin-bottom: 0px;'>
                            <tr>
                                <td class='gdvh'>SrNo</td>
                                <td class='gdvh'>Origin</td>
                                <td class='gdvh'>Dest</td>
                                <td class='gdvh'>AirV</td>                                
                               <%-- <td class='gdvh'>Category</td>--%>
                                <td class='gdvh'>Class</td>
                                <%--<td class='gdvh'>FareType</td>--%>
                                <td class='gdvh'>Jrny Type</td>
                                <td class='gdvh'>Amount</td>
                                <td class='gdvh'>AmtType</td>
                                <td class='gdvh'>FromDate</td>
                                <td class='gdvh'>ToDate</td>
                                <td class='gdvh'>CompanyID</td>
                                <td class='gdvh'>CampID</td>
                                <td class='gdvh'>Updated By</td>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td class="gdvr"><%# Container.ItemIndex+ 1 %></td>
                            <td class="gdvr exelcss">
                                <input maxlength="3" onfocus="this.select();" style="width: 40px;  font-size: 12px;" id='txtMarkupFrom<%# Eval("MarkupID")%>' onblur="oblurUpdate(&#39;<%# Eval("MarkupID")%>&#39;,&#39;MarkupFrom&#39;,&#39;<%# Eval("MarkupFrom")%>&#39;);" value='<%# Eval("MarkupFrom")%>' type="text"></td>
                            <td class="gdvr exelcss">
                                <input maxlength="3" onfocus="this.select();" style="width: 40px; font-size: 12px;" id='txtMarkupTo<%# Eval("MarkupID")%>' onblur="oblurUpdate(&#39;<%# Eval("MarkupID")%>&#39;,&#39;MarkupTo&#39;,&#39;<%# Eval("MarkupTo")%>&#39;);" value='<%# Eval("MarkupTo")%>' type="text"></td>
                            <td class="gdvr exelcss">
                                <input maxlength="3" onfocus="this.select();" style="width: 40px; font-size: 12px;" id='txtAirV<%# Eval("MarkupID")%>' onblur="oblurUpdate(&#39;<%# Eval("MarkupID")%>&#39;,&#39;AirV&#39;,&#39;<%# Eval("AirV")%>&#39;);" value='<%# Eval("AirV")%>' type="text"></td>
                           
                           <%-- <td class="gdvr exelcss">
                                <input maxlength="12" style="width: 100px; font-size: 12px;" id='txtCategory<%# Eval("MarkupID")%>' onblur="oblurUpdate(&#39;<%# Eval("MarkupID")%>&#39;,&#39;Category&#39;,&#39;<%# Eval("Category")%>&#39;);" value='<%# Eval("Category")%>' type="text"></td>--%>
                            <td class="gdvr exelcss">
                                <input maxlength="3" onfocus="this.select();" style="width: 40px; font-size: 12px;" id='txtMarkupClass<%# Eval("MarkupID")%>' onblur="oblurUpdate(&#39;<%# Eval("MarkupID")%>&#39;,&#39;MarkupClass&#39;,&#39;<%# Eval("MarkupClass")%>&#39;);" value='<%# Eval("MarkupClass")%>' type="text"></td>
                            <%--<td class="gdvr exelcss">
                                <input maxlength="3" style="width: 40px; font-size: 12px;" id='txtFareType<%# Eval("MarkupID")%>' onblur="oblurUpdate(&#39;<%# Eval("MarkupID")%>&#39;,&#39;FareType&#39;,&#39;<%# Eval("FareType")%>&#39;);" value='<%# Eval("FareType")%>' type="text"></td>--%>
                            <td class="gdvr exelcss">
                                <input maxlength="6" onfocus="this.select();" style="width: 50px; font-size: 12px;" id='txtJourneyType<%# Eval("MarkupID")%>' onblur="oblurUpdate(&#39;<%# Eval("MarkupID")%>&#39;,&#39;JourneyType&#39;,&#39;<%# Eval("JourneyType")%>&#39;);" value='<%# Eval("JourneyType")%>' type="text"></td>
                            <td class="gdvr exelcss">
                                <input onfocus="this.select();" style="width: 70px; font-size: 12px;" id='txtAmount<%# Eval("MarkupID")%>' onblur="oblurUpdate(&#39;<%# Eval("MarkupID")%>&#39;,&#39;Amount&#39;,&#39;<%# Eval("Amount")%>&#39;);" value='<%# Eval("Amount")%>' type="text"></td>
                            <td class="gdvr exelcss">
                                <input onfocus="this.select();" maxlength='1' style="width: 40px; font-size: 12px;" id='txtAmountType<%# Eval("MarkupID")%>' onblur="oblurUpdate(&#39;<%# Eval("MarkupID")%>&#39;,&#39;AmountType&#39;,&#39;<%# Eval("AmountType")%>&#39;);" value='<%# Eval("AmountType")%>' type="text"></td>


                            <td class="gdvr exelcss">
                                <input onfocus="this.select();" maxlength="10" style="width: 70px; font-size: 12px;" id='txtValidFromDate<%# Eval("MarkupID")%>' onblur="oblurUpdate(&#39;<%# Eval("MarkupID")%>&#39;,&#39;ValidFromDate&#39;,&#39;<%# Eval("ValidFromDate","{0:dd/MM/yyyy}")%>&#39;);" value='<%# Eval("ValidFromDate","{0:dd/MM/yyyy}")%>' type="text"></td>
                            <td class="gdvr exelcss">
                                <input onfocus="this.select();" maxlength='10' style="width: 70px; font-size: 12px;" id='txtValidToDate<%# Eval("MarkupID")%>' onblur="oblurUpdate(&#39;<%# Eval("MarkupID")%>&#39;,&#39;ValidToDate&#39;,&#39;<%# Eval("ValidToDate","{0:dd/MM/yyyy}")%>&#39;);" value='<%# Eval("ValidToDate","{0:dd/MM/yyyy}")%>' type="text"></td>




                            <td class="gdvr"><%# Eval("CompanyID")%></td>
                            <td class="gdvr"><%# Eval("CampID")%></td>
                            <td class="gdvr" title='<%# Eval("ModifiedDate","{0:dd-MM-yyyy HH:mm}")%>'><%# Eval("ModifiedBy")%></td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
            <div class="panel-footer">
                <asp:Button ID="btnExport1" runat="server" CssClass="btn btn-primary btn-lg" OnClick="btnExport1_Click" Visible="false" Text="Download" />
                <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-danger btn-lg" Visible="false" Text="Delete" OnClientClick="SearchMarkup();" OnClick="btnDelete_Click" />
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
        function AddMarkup() {
            var strMsg = "";

            if ($("#<%= txtAmount.ClientID %>").val() == "") {
                strMsg += "Markup amount is empty not allowed!!\n\r";
            }
            if ($("#<%= ddlCompany.ClientID %>").val() == "") {
                strMsg += "Please any select company!!\n\r";
            }
            if (strMsg == "") {
                popup('popProgressBar', 30, 30);
            }
            else { alert(strMsg); }
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
