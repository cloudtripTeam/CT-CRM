<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="SeoContents.aspx.cs" Inherits="Admin_SeoContents" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <%--<link href="../css/StyleSheet.css" rel="stylesheet" />--%>
    <link href="../../css/StyleSheet.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div class="container">
        <div style="height: 15px;"></div>
        <div class="panel panel-default">
            <div class="panel-heading">SEO Content Manager</div>

            <div class="panel-body" style="line-height: 34px;">
                <div class="row">
                    <div class="col-md-2">
                        <label>Company</label>
                         <asp:DropDownList ID="ddlCompanyID" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                    <div class="col-md-4">
                        <label>URL</label>
                        <asp:TextBox ID="txtUrl" runat="server" CssClass="form-control" PlaceHolder="Page URL" ></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <label>Content Type</label>
                        <asp:DropDownList ID="ddlContentType" runat="server" CssClass="form-control">
                            <asp:ListItem Text="Title" Value="Title"></asp:ListItem>
                             <asp:ListItem Text="Meta" Value="Meta" Selected="True"></asp:ListItem>
                             <asp:ListItem Text="Link" Value="Link"></asp:ListItem>
                             <asp:ListItem Text="Script" Value="Script"></asp:ListItem>
                             <asp:ListItem Text="Page Content" Value="PageContent"></asp:ListItem>

                        </asp:DropDownList>
                    </div>
                    <div class="col-md-4">
                        <label>Content</label>
                        <asp:TextBox ID="txtContent" TextMode="MultiLine" runat="server" CssClass="form-control" PlaceHolder="Content"></asp:TextBox>
                         
                    </div>
                   
                </div>
                <div class="col-md-12">
                    <asp:TextBox CssClass="textEditor1" ID="txtPageContents" TextMode="MultiLine" runat="server"  placeholder="Page Contents" />
                    </div>
                <div class="col-md-12">
                    &nbsp;
                        <asp:Label ID="lblMsg" runat="server" ForeColor="Red" EnableViewState="false"></asp:Label>
                </div>
                 <div class="row">


                    <div class="col-md-6">
                        <label>
                            <br />
                            &nbsp;
                        </label>
                        <asp:Button ID="btnInsert" runat="server"  CssClass="btn btn-primary btn-lg" Text="Insert" OnClick="btnSubmit_Click" ValidationGroup="restrictedAirlines" />
                        <asp:Button ID="btnSearch" runat="server" OnClientClick="return SearchContent();" CssClass="btn btn-primary btn-lg" Text="Search" OnClick="btnSearch_Click" />
                        
                    </div>
                </div>
            </div>
            <div class="clearfix"></div>
            <div class="panel" style="padding-top: 0px; margin-top: 20px;">
                <div class="panel-body" style="border: 1px solid #ddd; padding: 0px!important;">
                    <asp:Repeater ID="rptrDetails" runat="server" OnItemCommand="rptrDetails_ItemCommand">
                        <HeaderTemplate>
                            <table cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td class='gdvh'>SrNo</td>
                                      <td class='gdvh'> <input id="DelSelected" type="button"  onclick="DelSelectedContents()" value="Del" /></td>                                   
                                    <td class="gdvh">Company</td>
                                    <td class="gdvh">Page Url</td>
                                    <td class="gdvh">Content Type</td>
                                    <td class="gdvh">Content</td>
                                    <td class="gdvh">Modify By</td>
                                    <td class="gdvh">Modify Date</td>                                   
                                    <td class="gdvh">&nbsp;<br />
                                    </td>

                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <tr id='tr<%# Eval("SNo")%>'>

                                    <td class='gdvr'><%# Container.ItemIndex+ 1 %></td>
                                    <td class='gdvr'>
                                        <input id="chbx" type="checkbox" value="<%# Eval("SNo")%>" /></td>
                                    <td class='gdvr'><%# Eval("Company")%></td>
                                    <td class='gdvr'><%# Eval("PageUrl")%></td>
                                    <td class='gdvr'><%# Eval("ContentType")%></td>
                                    <td class='gdvr'><pre><code><%# Server.HtmlEncode(Eval("Content").ToString())%></code></pre></td>
                                    <td class='gdvr'><%# Eval("ModifyBy")%></td>
                                    <td class='gdvr'><%# Eval("ModifyDate")%></td>
                                    
                                    <td class='gdvr'>
                                        <%--<asp:LinkButton ID="LinkButton1" runat="server" CommandName="EditDetails" CommandArgument='<%# Eval("SNo")%>'>Edit</asp:LinkButton>--%>
                                <img src='../images/CardDelete.png' style='cursor: pointer;' onclick="DeleteContent(&#39;<%# Eval("SNo")%>&#39;);" />
                            </td>
                                </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            <tr>
                                    <td class='gdvh'></td>
                                      <td class='gdvh'> <input id="DelSel" type="button" value="Del" onclick="DelSelectedContents();" /></td>                                   
                                    <td class="gdvh"></td>
                                    <td class="gdvh"></td>
                                    <td class="gdvh"></td>
                                    <td class="gdvh"></td>
                                    <td class="gdvh"></td>
                                    <td class="gdvh"></td>
                                    <td class="gdvh"></td>

                                </tr>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>

                </div>
            </div>
        </div>
    </div>    
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

        function SearchContent() {
            popup("popProgressBar", 40, 40);
            return true;
        }
        function CheckValidation() {
           
            var strMsg = "";
            if ($("#txtOrigin").val() == "") {
                strMsg += "Please enter origin Code!!\n\r";
            }

            if ($("ContentPlaceHolder1_txtDestination").val() == "") {
                strMsg += "Please enter Destination Code!!\n\r";
            }

            if ($("ContentPlaceHolder1_txtTravelDateStart").val() == "") {
                strMsg += "Please enter Travel Date Start !!\n\r";
            }

            if ($("ContentPlaceHolder1_txtTravelDateEnd").val() == "") {
                strMsg += "Please enter Travel Date End!!\n\r";
            }


            if ($("ContentPlaceHolder1_txtAieline").val() == "") {
                strMsg += "Please enter airline Code!!\n\r";
            }

            if ($("ContentPlaceHolder1_txtAirlineClass").val() == "") {
                strMsg += "Please enter airline class !!\n\r";
                
            }
            else if(/^[a-zA-Z]{1}(,[a-zA-Z]{1})*$/.test(txtAirlineClass))
            {
            
                strMsg += "Please enter valid airline class !!\n\r";
            }

            if ($("#ddlCabinClass").val() == "") {
                strMsg += "Please select cabin class !!\n\r";
            }
            if (strMsg == "") { return true; }
            else { alert(strMsg); return false; }
        }
        function DeleteContent(ID) {
            if (confirm("Are you sure delete this Content?")) {
                popup('popProgressBar', 30, 30);
                $.ajax({
                    type: "POST",
                    url: "SeoContents.aspx/DeleteContents",
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
                            alert("Content is not successfully deleted from database!!");
                    },
                    error: function (data) { popup('popProgressBar', 30, 30); }
                });
            }
        }

        function oblurUpdate(ID, UpdateField, OldValue) {
            var isUpdate = false;
           
            try {
                if (UpdateField == "BaseFare" || UpdateField == "Tax" || UpdateField == "Markup" || UpdateField == "Commission") {
                    
                        if (parseFloat($("#txt" + UpdateField + ID).val()) != parseFloat(OldValue)) {
                            isUpdate = true;
                           
                            
                        }
                        else {
                            isUpdate = false;
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
                    case "BaseFare":
                    case "Tax":
                    case "Markup":
                    case "Commission":
                        if ($("#txt" + UpdateField + ID).val().length =="") {
                            $("#txt" + UpdateField + ID).focus();
                            alert("Invalid amount");
                            return;
                        }
                        break;   
                   
                }
                var Param = {
                    ID: ID,
                    UpdateField: UpdateField,
                    Value: $("#txt" + UpdateField + ID).val()
                   
                }
                $.ajax({
                    type: "POST",
                    url: "OwnFares.aspx/UpdateFare",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify(Param),
                    responseType: "json",
                    success: function (data) {
                        if (data.d != "true") {
                            alert("Record not successfully Updeted in database!!");
                        }
                    },
                    error: function (data) { }
                });
            }

        }

        function DelSelectedContents() {
            var n = $("input:checked").length;
            if (n > 0) {
                if (confirm("Are you sure to delete all selected Contents?")) {
                    popup('popProgressBar', 30, 30);

                    $("input[type=checkbox]:checked").each(function () {
                        var ID = $(this).val();

                        $.ajax({
                            type: "POST",
                            url: "SeoContents.aspx/DeleteContents",
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
                                    alert("Content is not successfully deleted from database!!");
                            },
                            error: function (data) { popup('popProgressBar', 30, 30); }
                        });
                    });
                    popup('popProgressBar', 30, 30);
                }
            }
            else { alert("Please select Contents to delete")}


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

      <script>
          $(document).ready(function () {
              $(document.getElementById('<%=ddlContentType.ClientID%>')).on('change', function () {
                  if (this.value == 'PageContent') {
                      $(document.getElementById('<%=txtContent.ClientID%>')).hide();
                      $(".jqte").show();
                   

                }
                else  {
                      $(document.getElementById('<%=txtContent.ClientID%>')).show();
                      $(".jqte").hide();
                    
                }
               

            });
          });

          $(document).ready(function () {
              
                  if (document.getElementById('<%=ddlContentType.ClientID%>').value == 'PageContent') {
                      $(document.getElementById('<%=txtContent.ClientID%>')).hide();
                      $(".jqte").show();


                  }
                  else {
                      $(document.getElementById('<%=txtContent.ClientID%>')).show();
                      $(".jqte").hide();

                  }

             
          });


    </script>
     <script lang="javascript" type="text/javascript">
         $('.textEditor1').jqte();
</script>
</asp:Content>

