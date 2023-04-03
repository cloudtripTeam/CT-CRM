<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" 
    CodeFile="OwnFaresJazz_US.aspx.cs" Inherits="Admin_OwnFaresJazz_US" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%--<link href="../css/StyleSheet.css" rel="stylesheet" />--%>
    <link href="../../css/StyleSheet.css" rel="stylesheet" />
    <script src="https://www.traveljunction.co.uk/temp/CalendarAnyYear.js"></script>
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
     <script type="text/javascript" src="../Scripts/Sswitch.js"></script>
     <script>
         $(document).ready(function () {
             $(".checkbox").Sswitch({
                 onSwitchChange: function () {

                     var chkBoxValue = $('.checkbox').prop("checked");
                     var _Origin = $("#<%= txtOrigin.ClientID%>").val(); 
                     var _Destination = $("#<%= txtDestination.ClientID%>").val(); 
                     var _Airline = $("#<%= txtAieline.ClientID%>").val(); 
                     var _AirlineClass = $("#<%= txtAirlineClass.ClientID%>").val(); 
                     var _TripType = $("#<%= ddlTripType.ClientID%>").val(); 
                     var _OfferType = $("#<%= ddlOfferType.ClientID%>").val(); 
                     var _Pax = $("#<%= ddlPax.ClientID%>").val(); 
                     var _FlightType = $("#<%= ddlFlightType.ClientID%>").val(); 
                     var _Campaign = $("#<%= ddlCampaign.ClientID%>").val(); 

                     var Param =
                     {
                         UseStatus: chkBoxValue,
                         Origin: _Origin,
                         Destination: _Destination,
                         Airline: _Airline,
                         AirlineClass: _AirlineClass,
                         TripType: _TripType,
                         OfferType: _OfferType,
                         Pax: _Pax,
                         FlightType: _FlightType,
                         Campaign: _Campaign
                     }
                     $.ajax({
                         type: "POST",
                         url: "OwnFaresJazz_US.aspx/UpdateDiscountOwnFareJazz_US",
                         contentType: "application/json; charset=utf-8",
                         dataType: "json",
                         data: JSON.stringify(Param),
                         responseType: "json",
                         success: function (data) {

                             if (data.d == "true") {
                                 alert("Own fares Set to " + chkBoxValue + "!");
                             }
                             else {
                                 alert("Own fares Not Updated!");
                             }
                         },
                         error: function (data) { }
                     });


                 }
             });
         });
     </script>
      <style>
      table,
      td,
      th {
      padding: 5px!important;
        border: 1px solid #333;
        text-align: center!important;
        border-bottom: 1px solid #333!important;
          border-top: 1px solid #333!important;
      }
      .tbl {
            color: Black;
            border-color: none!important;
            width: 100%;
            border-collapse: collapse;
            font-size: 12px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <div style="height: 15px;"></div>
        <div class="panel panel-default">
            <div class="panel-heading">All Own Fares Status<small style="color:greenyellow"><i>  </i></small></div>
            <div class="panel-body" style="line-height: 34px;">
                <input type="checkbox" name="checkboxisActive" class="checkbox" runat="server" id="checkboxisActive" clientidmode="Static" />
            </div>
        </div>
        <div class="panel panel-default">
            <div class="panel-heading">Search/Add Own Fare Offers</div>

            <div class="panel-body" style="line-height: 18px;">
                <div class="row">
                    <div class="col-md-2">
                        <label>Origin</label>
                        <asp:TextBox ID="txtOrigin" runat="server" CssClass="form-control" PlaceHolder="Origin" MaxLength="3"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <label>Destination</label>
                        <asp:TextBox ID="txtDestination" runat="server" CssClass="form-control" PlaceHolder="Destination" MaxLength="3"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <label>Travel Start Date</label>
                        <asp:TextBox ID="txtTravelDateStart" onclick="showCalender(this);" AutoComplete="off"  runat="server" CssClass="form-control" PlaceHolder="dd/mm/yyyy"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <label>Travel End Date</label>
                        <asp:TextBox ID="txtTravelDateEnd" onclick="showCalender(this);"  AutoComplete="off" runat="server" CssClass="form-control" PlaceHolder="dd/mm/yyyy"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <label>Airline</label>
                        <asp:TextBox ID="txtAieline" runat="server" CssClass="form-control" AutoComplete="off" MaxLength="2" PlaceHolder="Airline Code"></asp:TextBox>
                    </div>

                    <div class="col-md-2">
                        <label>Airline Class</label>
                        <asp:TextBox ID="txtAirlineClass" runat="server" AutoComplete="off" CssClass="form-control" PlaceHolder="Airline Code"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="revAirline" runat="server" ErrorMessage="invalid airline(s) class" ForeColor="Red" ValidationExpression="^[a-zA-Z]{1}(,[a-zA-Z]{1})*$" ControlToValidate="txtAirlineClass" ValidationGroup="ownfares"></asp:RegularExpressionValidator>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        <label>Cabin Class</label>
                        <asp:DropDownList ID="ddlCabinClass" runat="server" CssClass="form-control">
                            <asp:ListItem Value="">ANY</asp:ListItem>
                            <asp:ListItem Value="Y">ECONOMY</asp:ListItem>
                            <asp:ListItem Value="BUSINESS">BUSINESS</asp:ListItem>
                            <asp:ListItem Value="FIRSTCLASS">FIRSTCLASS</asp:ListItem>
                            <asp:ListItem Value="PREMIUM">PREMIUM</asp:ListItem>
                        </asp:DropDownList>
                    </div>

                  

                   

                    <div class="col-md-2">
                        <label>Base Fare</label>
                        <asp:TextBox ID="txtBaseFare" runat="server" CssClass="form-control" PlaceHolder="0.00" Text="0"></asp:TextBox>
                        <span>&nbsp;</span>
                    </div>

                    <div class="col-md-2">
                        <label>Tax</label>
                        <asp:TextBox ID="txtTax" runat="server" CssClass="form-control" PlaceHolder="0.00" Text="0"></asp:TextBox>
                    </div>
                       <div class="col-md-2">
                        <label>Markup</label>
                        <asp:TextBox ID="txtMarkup" runat="server" CssClass="form-control" PlaceHolder="0.00" Text="0"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <label>Journey Type</label><asp:DropDownList CssClass="form-control" ID="ddlTripType" runat="server">
                            <asp:ListItem Value="R" Text="Return"></asp:ListItem>
                            <asp:ListItem Value="O" Text="OneWay"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    
                    <div class="col-md-2">
                        <label>
                            Offer Type
                        </label>
                        <asp:DropDownList ID="ddlOfferType" CssClass="form-control" runat="server">
                            <asp:ListItem Text="Book" Value="Book" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Call" Value="Call"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row">
                      <div class="col-md-2">
                        <label>Commission</label>
                        <asp:TextBox ID="txtCommssion" runat="server" CssClass="form-control" PlaceHolder="0.00" Text="0"></asp:TextBox>
                             <span>&nbsp;</span>
                    </div>
                    <div class="col-md-2">
                        <label>Campaign Name</label>
                        <asp:DropDownList ID="ddlCampaign" CssClass="form-control" runat="server"></asp:DropDownList>
                    </div>
                    <div class="col-md-2">
                        <label>Active</label>
                       <asp:DropDownList ID="ddlActive" CssClass="form-control" runat="server">
                            <asp:ListItem Text="True" Value="true"></asp:ListItem>
                            <asp:ListItem Text="False" Value="false"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2">
                        <label>Pax</label>
                        <asp:DropDownList ID="ddlPax" runat="server" CssClass="form-control">
                            <asp:ListItem Value="1">1</asp:ListItem>
                            <asp:ListItem Value="2">2</asp:ListItem>
                            <asp:ListItem Value="3">3</asp:ListItem>
                            <asp:ListItem Value="4">4</asp:ListItem>
                            <asp:ListItem Value="5">5</asp:ListItem>
                            <asp:ListItem Value="6">6</asp:ListItem>
                            <asp:ListItem Value="7">7</asp:ListItem>
                            <asp:ListItem Value="8">8</asp:ListItem>
                            <asp:ListItem Value="9">9</asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <div class="col-md-2">
                        <label>Flight Type</label>
                        <asp:DropDownList ID="ddlFlightType" runat="server" CssClass="form-control">
                            <asp:ListItem Value="-1">All</asp:ListItem>
                            <asp:ListItem Value="0">Direct</asp:ListItem>
                            <asp:ListItem Value="1">One-Stop</asp:ListItem>
                            <asp:ListItem Value="2">Two-Stop</asp:ListItem>
                        </asp:DropDownList>
                    </div> 
                       <div class="col-md-2">
                        <label>Days To Departure</label>
                        <asp:TextBox runat="server" CssClass="form-control" AutoComplete="off" type="number" Text="0"  ID="txtDTD" MaxLength="3" data-minLength="1" />
                    </div>
                    </div>
                <div class="row">
                          <div class="col-md-2">
                            <label>Pass Key</label>
                            <asp:TextBox runat="server" ID="txtPasskey" ClientIDMode="Static" MaxLength='10' type="password" AutoComplete="new-password" CssClass="form-control"  />
                          </div>

                      <div class="col-md-2">
                        <label>Modify By</label>
                        <asp:TextBox ID="txtModifyBy" runat="server" CssClass="form-control" Text=""></asp:TextBox>
                    </div>

                 
                    <div style="float: right; margin-top: 35px; margin-right: 5px;">
                        <asp:Button ID="btnSubmit" runat="server" OnClientClick="return CheckValidation();" CssClass="btn btn-success btn-lg" Text="Insert" OnClick="btnSubmit_Click" />
                        <asp:Button ID="btnSearch" runat="server" OnClientClick="return SearchCall();" CssClass="btn btn-primary btn-lg" Text="Search" OnClick="btnSearch_Click" />
                        <asp:Button ID="btnUpdate" runat="server" OnClientClick="return CheckValidation();" CssClass="btn btn-success btn-lg" Visible="false" Text="Update" OnClick="btnUpdate_Click" />
                        <asp:Button ID="btnCancel" runat="server" OnClientClick="return SearchCall();" CssClass="btn btn-danger btn-lg" Text="Cancel" Visible="false" OnClick="btnCancel_Click" />
                        <br />
                        <asp:Label ID="lblMsg" runat="server" ForeColor="Red" EnableViewState="false"></asp:Label>
                    </div>
                </div>
                <br />


                  <div class="panel panel-default" runat="server" id="logicdiv" >
            <div class="panel-heading"> 
                <input id ="toggle" value="Show Logic" type="button" class="btn btn-primary btn-xs" />
           </div>
            <div class="panel-body target" style="display:none;background:#333;" >  <input id ="Close" value="&#10005;" type="button" class="btn btn-primary btn-xs" style="float:right" />
              <div class="row" style="line-height: 20px;" >
                    <p style="color:greenyellow">
                            <span>Flight Type Information:</span><br />
                       <small><i>1. For All Flight Type: -1</i></small><br />
                        <small><i>2. Direct Flights: 0</i></small><br />
                        <small><i>3. One-Stop: 1</i></small><br />
                        <small><i>4. Two-Stop: 2</i></small><br />
                        
                        <small><i>Own fare will get implemented only if it satisfies the above conditions.</i></small><br />
                        <small><i>Note: Fare is Order by TotalAmount ( BaseFare + Taxes + Markup ) in Ascending order, Lowest TotalAmount value will be picked out of all.[From 16 Oct 2020]</i></small><br />
                     <small><i>DTD - On 1st of month, if Date of Departure is on 07th, then all the rules with values DTD less than 7 is considered as valid.</i></small><br />
                        <small><i>PAX Count - If search has 2 Pax then all the Rules with PAX less than or equal to 2 will be considered as valid.</i></small><br />
                     
                    </p>



     
                </div>
            </div>
        </div>

                <div class="panel panel-default" id="pnlbulkfare" runat="server">
                    <div class="panel-heading">
                        <div class="pull-left">
                            <h4 class="panel-title">
                                <a data-toggle="collapse" href="#bulkUpdate" class="btn btn-primary btn-xs">Bulk Update</a>
                            </h4>
                        </div>
                       <%-- <div class="pull-right"><a data-toggle="collapse" href="#bulkUpdate"><span class="glyphicon glyphicon-search"></span></a></div>--%>
                        <div class="clearfix"></div>
                    </div>

                    <div id="bulkUpdate" class="panel-collapse collapse">
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-5">
                                    <asp:FileUpload ID="FileUpload1" runat="server" Width="200px" CssClass="active" Height="36px" />
                                </div>

                                <div class="col-md-2">
                                    <asp:Button ID="btnUpload" runat="server" class="btn btn-success btn-xs" OnClientClick="return SearchCall();" Text="Upload Own Fares" OnClick="btnUpload_Click" />
<%--                                    <br />
                                    Note Excel Sheet name Must be "Sheet 1"--%>
                                </div>
                                <div class="col-md-2">
                                    <asp:Button ID="btnExport" class="btn btn-primary btn-xs" runat="server" Text="Export Own Fares"
                                        OnClick="btnExport_Click" />
                                </div>
                                <div class="col-md-3">
                                    <asp:Button ID="btnDeleteAll" class="btn btn-danger btn-xs" Enabled="false" runat="server" OnClientClick="return confirm('Are you sure delete all own fares?')" Text="Delete All own fares"
                                        OnClick="btnDeleteAll_Click" />
                                </div>

                            </div>

                            <div class="row">
                                <div class="col-md-12">
                                    <%-- <asp:Label ID="lblmsg" EnableViewState="false" ForeColor="Red" runat="server" Text=""></asp:Label>--%>
                                </div>

                            </div>
                            <div class="clearfix"></div>


                        </div>
                    </div>


                </div>
                <div class="clearfix"></div>
            </div>

                <div class="panel" style="padding-top: 0px;">
                    <div class="panel-body" style="border: 1px solid #ddd; padding: 0px!important;overflow-x:auto">
                        <asp:Repeater ID="rptrDetails" runat="server" OnItemCommand="rptrDetails_ItemCommand">
                            <HeaderTemplate>
                                <table class="table" cellspacing="0" rules="all" border="1">
                                    <tr>
                                        <th scope="col" style="width: 40px" class='gdvh'>Sr No</th>
                                        <th scope="col" style="width: 40px" class='gdvh'>
                                            <input id="chkDelAll" onclick="SelectAll(this.id)" type="checkbox" value="delall" />
                                           <input id="DelSelected" type="button" onclick="DelSelectedFares()" value="Del" />
                                       

                                        </th>
                                        <th class="gdvh">Origin</th>
                                        <th class="gdvh">Dest</th>
                                        <th class="gdvh">Start Date</th>
                                        <th class="gdvh">End Date</th>
                                        <th class="gdvh">Airline</th>
                                        <th class="gdvh">Cab in</th>
                                        <th class="gdvh">Air Class</th>
                                        <th class="gdvh">Base Fare</th>
                                        <th class="gdvh">Tax</th>
                                        <th class="gdvh">Markup</th>
                                        <th class="gdvh">Trip Type</th>
                                        <th class="gdvh">Offer Type</th>
                                        <th class="gdvh">Campaign</th>
                                        <th class="gdvh">Pax</th>
                                        <th class="gdvh">Flight Type</th>
                                        <th class="gdvh">DTD</th>
                                        <th class="gdvh">Active</th>
                                       <%-- <th scope="col" style="width: 40px" class="gdvh">Mod By</th>--%>
                                        <th scope="col" style="width: 40px" class="gdvh">Action</th>
                                         
                                    </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <tr id='tr<%# Eval("SrNo")%>'>
                                        <td style="width: 40px" class='gdvr'><%# Container.ItemIndex+ 1 %></td>
                                        <td style="width: 40px" class='gdvr'>
                                            <input id="chbx" class="checkBoxClass" type="checkbox" value="<%# Eval("SrNo")%>" />
                                        </td>
                                         <td class='gdvr'>
                                                <input style="width: 40px; font-size: 12px;" id='txtOrigin<%# Eval("SrNo")%>' onfocus="this.select();" onblur="oblurUpdate(&#39;<%# Eval("SrNo")%>&#39;,&#39;Origin&#39;,&#39;<%# Eval("Origin")%>&#39;);" value='<%# Eval("Origin")%>' type="text">
                                            </td>
                                            <td class='gdvr'>
                                                <input style="width: 40px; font-size: 12px;" id='txtDestination<%# Eval("SrNo")%>' onfocus="this.select();" onblur="oblurUpdate(&#39;<%# Eval("SrNo")%>&#39;,&#39;Destination&#39;,&#39;<%# Eval("Destination")%>&#39;);" value='<%# Eval("Destination")%>' type="text">
                                            </td>

                                        <td style="width: 40px" class='gdvr'>
                                            <input style="width: 70px; font-size: 12px;" id='txtFromDateStart<%# Eval("SrNo")%>' onfocus="this.select();" onblur="oblurUpdate(&#39;<%# Eval("SrNo")%>&#39;,&#39;FromDateStart&#39;,&#39;<%# Eval("FromDateStart","{0:ddMMMyyyy}")%>&#39;);" value='<%# Eval("FromDateStart","{0:ddMMMyyyy}")%>' type="text">
                                        </td>
                                        <td class='gdvr'>
                                            <input style="width: 70px; font-size: 12px;" id='txtFromDateEnd<%# Eval("SrNo")%>' onfocus="this.select();" onblur="oblurUpdate(&#39;<%# Eval("SrNo")%>&#39;,&#39;FromDateEnd&#39;,&#39;<%# Eval("FromDateEnd","{0:ddMMMyyyy}")%>&#39;);" value='<%# Eval("FromDateEnd","{0:ddMMMyyyy}")%>' type="text">
                                        </td>
                                          <td class='gdvr'>
                                            <input style="width: 30px; font-size: 12px;"  maxlength="2"  id='txtAirline<%# Eval("SrNo")%>' onfocus="this.select();" onblur="oblurUpdate(&#39;<%# Eval("SrNo")%>&#39;,&#39;Airline&#39;,&#39;<%# Eval("Airline")%>&#39;);" value='<%# Eval("Airline")%>' type="text">
                                        </td>
                                         
                                        <td style="width: 30px;" class='gdvr'><%# Eval("CabinClass")%></td>
                                        <td class='gdvr'>
                                                <input style="width: 30px; font-size: 12px;" maxlength="2" id='txtAirlineClass<%# Eval("SrNo")%>' onfocus="this.select();" onblur="oblurUpdate(&#39;<%# Eval("SrNo")%>&#39;,&#39;AirlineClass&#39;,&#39;<%# Eval("AClass")%>&#39;);" value='<%# Eval("AClass")%>' type="text">
                                            </td>

                                        <td class='gdvr'>
                                            <input style="width: 50px; font-size: 12px;" id='txtBaseFare<%# Eval("SrNo")%>' onfocus="this.select();" onblur="oblurUpdate(&#39;<%# Eval("SrNo")%>&#39;,&#39;BaseFare&#39;,&#39;<%# Eval("BaseFare")%>&#39;);" value='<%# Eval("BaseFare","{0:f2}")%>' type="text">
                                        </td>
                                        <td class='gdvr'>
                                            <input style="width: 50px; font-size: 12px;" id='txtTax<%# Eval("SrNo")%>' onfocus="this.select();" onblur="oblurUpdate(&#39;<%# Eval("SrNo")%>&#39;,&#39;Tax&#39;,&#39;<%# Eval("Tax")%>&#39;);" value='<%# Eval("Tax","{0:f2}")%>' type="text">
                                        </td>
                                        <td class='gdvr'>
                                            <input style="width: 50px; font-size: 12px;" id='txtMarkup<%# Eval("SrNo")%>' onfocus="this.select();" onblur="oblurUpdate(&#39;<%# Eval("SrNo")%>&#39;,&#39;Markup&#39;,&#39;<%# Eval("Markup")%>&#39;);" value='<%# Eval("Markup","{0:f2}")%>' type="text">
                                        </td>
                                      <td class='gdvr'>
                                                <input style="width: 35px; font-size: 12px;" id='txtTripType<%# Eval("SrNo")%>' onfocus="this.select();" onblur="oblurUpdate(&#39;<%# Eval("SrNo")%>&#39;,&#39;TripType&#39;,&#39;<%# Eval("TripType")%>&#39;);" value='<%# Eval("TripType")%>' type="text">
                                            </td>
                                        <td class='gdvr'><span class='gdvr' style="font-size: 10px;" title="" ><%# Eval("OfferType")%></span></td>
                                        <td class='gdvr'><span class='gdvr' style="font-size: 10px;" title="<%# Eval("ModifyDateTime")%> <%# Eval("ModifyBy")%>" ><%# Eval("Campaign")%></span></td>
                                        <td class='gdvr'>
                                            <input maxlength="1" style="width: 30px; font-size: 12px;" id='txtPaxCount<%# Eval("SrNo")%>' onfocus="this.select();" onblur="oblurUpdate(&#39;<%# Eval("SrNo")%>&#39;,&#39;PaxCount&#39;,&#39;<%# Eval("PaxCount")%>&#39;);" value='<%# Eval("PaxCount")%>' type="text">
                                        </td>
                                          <td class='gdvr'>
                                            <input style="width: 30px; font-size: 12px;" id='txtFlightType<%# Eval("SrNo")%>' onfocus="this.select();" onblur="oblurUpdate(&#39;<%# Eval("SrNo")%>&#39;,&#39;FlightType&#39;,&#39;<%# Eval("FlightType")%>&#39;);" value='<%# Eval("FlightType")%>' type="text">
                                        </td>

                                         <td class='gdvr'>
                                            <input style="width: 30px; font-size: 12px;" id='txtDTD<%# Eval("SrNo")%>' onfocus="this.select();" onblur="oblurUpdate(&#39;<%# Eval("SrNo")%>&#39;,&#39;DTD&#39;,&#39;<%# Eval("DTD")%>&#39;);" value='<%# Eval("DTD")%>' type="text">
                                        </td>
                                      
                                          <td class='gdvr'>
                                            <input style="width: 35px; font-size: 12px;" id='txtActive<%# Eval("SrNo")%>' onfocus="this.select();" onblur="oblurUpdate(&#39;<%# Eval("SrNo")%>&#39;,&#39;Active&#39;,&#39;<%# Eval("Active")%>&#39;);" value='<%# Eval("Active")%>' type="text">
                                        </td>
                                        <%--<td class='gdvr'><span class='gdvr' title="<%# Eval("ModifyDateTime")%> <%# Eval("ModifyBy")%>"> <%# Eval("ModifyBy").ToString().Substring(0,3)%></span></td>--%>
                                        <td class='gdvr' style="color: white; width: 70px;">
                                            <asp:LinkButton ID="LinkButton1" runat="server" style="padding-right:0px;float:left" CommandName="EditDetails" CommandArgument='<%# Eval("SrNo")%>'>
                                                <img src='../images/CardEdit.png' style='cursor: pointer;width:20px' title="Edit"/>
                                            </asp:LinkButton>
                                            
                                            <asp:LinkButton runat="server" ToolTip='<%#Eval("SrNo") %>' ID="linkbtn" OnClick="CopyFares" style="padding-right:0px;float:left;"  >
                                                 <img src='../images/Copy.png' style='cursor: pointer;width:20px'  />
                                            </asp:LinkButton>
                                        
                                        </td>
                                    </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                <tr>
                                    <td class='gdvh'></td>
                                    <td class='gdvh'>
                                          <input id="DelSel" type="button" onclick="DelSelectedFares()" value="Del" />
                                        </td>
                                    <td class="gdvh"></td>
                                    <td class="gdvh"></td>
                                    <td class="gdvh"></td>
                                    <td class="gdvh"></td>
                                    <td class="gdvh"></td>
                                    <td class="gdvh"></td>
                                    <td class="gdvh"></td>
                                    <td class="gdvh"></td>
                                    <td class="gdvh"></td>
                                    <td class="gdvh"></td>
                                    <td class="gdvh"></td>
                                    <td class="gdvh"></td>
                                    <td class="gdvh"></td>
                                    <td class="gdvh"></td>
                                    <td class="gdvh"></td>
                                    <td class="gdvh"></td>
                                   <%-- <td class="gdvh"></td>--%>
                                     <td class="gdvh"></td>
                                    <td class="gdvh">&nbsp;<br />
                                    </td>
                                </tr>
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
    <asp:GridView ID="gv" runat="server"></asp:GridView>
    <script type="text/javascript">
        $(function () {
            $('input:text:first').focus();
            var $inp = $('input:text');
            $inp.bind('keydown', function (e) {
                var key = e.which;
                if (key == 40) {
                    if ($inp.index(this) > 12) {
                        var nxtIdx = $inp.index(this) + 14;
                        $(":input:text:eq(" + nxtIdx + ")").focus();
                        $(":input:text:eq(" + nxtIdx + ")").select();
                    }
                }
                else if (key == 38) {
                    if ($inp.index(this) > 12) {
                        var nxtIdx = $inp.index(this) - 14;
                        $(":input:text:eq(" + nxtIdx + ")").focus();
                        $(":input:text:eq(" + nxtIdx + ")").select();
                    }
                }
            });
        });
    </script>

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
            if ($("#txtPasskey").val() == "") {
                alert("Pass Key Required");
                return false;
            }
            waitingDialog.show('Please Wait...');
            return true;
        }
        function CheckValidation() {

            var strMsg = "";
            if ($("#txtOrigin").val() == "") {
                strMsg += "Please enter origin Code!!\n\r";
            }

            if ($("#ContentPlaceHolder1_txtDestination").val() == "") {//txtActive
                strMsg += "Please enter Destination Code!!\n\r";
            }

            if ($("#ContentPlaceHolder1_ddlActive").val() != "true" && $("#ContentPlaceHolder1_ddlActive").val() != "false") {
                strMsg += "Active status can only be either true or false!!\n\r";
            }

            if ($("#ContentPlaceHolder1_txtTravelDateStart").val() == "") {
                strMsg += "Please enter Travel Date Start !!\n\r";
            }

            if ($("#ContentPlaceHolder1_txtTravelDateEnd").val() == "") {
                strMsg += "Please enter Travel Date End!!\n\r";
            }


            if ($("#ContentPlaceHolder1_txtAieline").val() == "") {
                strMsg += "Please enter airline Code!!\n\r";
            }

            if ($("#ContentPlaceHolder1_txtAirlineClass").val() == "") {
                strMsg += "Please enter airline class !!\n\r";

            }
            if (!CheckIntegerValue($("#ContentPlaceHolder1_txtDTD").val())) {
                strMsg += "Please select valid days to departure (Numbers only)  !!\n\r";
            }

            if ($("#ddlCabinClass").val() == "") {
                strMsg += "Please select cabin class !!\n\r";
            }

            if (strMsg == "") {
                return true;
            }
            else {
                alert(strMsg);
                return false;
            }
        }

        function CheckIntegerValue(val) {
            if (/^\d+$/.test(val)) {
                return true;
            }
            else {
                return false;
            }
        }


        function DeleteFare(ID) {
            if (confirm("Are you sure delete this Own Fare?")) {
                popup('popProgressBar', 30, 30);
                $.ajax({
                    type: "POST",
                    url: "OwnFaresJazz_US.aspx/DeleteFaresJazzUS",
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
                        else {
                            alert("Own Fare is not successfully deleted from database!!");
                        }
                    },
                    error: function (data) { popup('popProgressBar', 30, 30); }
                });
            }
        }

        function oblurUpdate(ID, UpdateField, OldValue) {
            var isUpdate = false;
            try {
                if (UpdateField == "BaseFare" || UpdateField == "Tax" || UpdateField == "Markup" || UpdateField == "Commission"
                    || UpdateField == "AirlineClass" || UpdateField == "Airline") {
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
                        if ($("#txt" + UpdateField + ID).val().length == "") {
                            $("#txt" + UpdateField + ID).focus();
                            alert("Invalid amount");
                            return;
                        }
                        break;
                    case "FromDateStart":
                    case "FromDateEnd":
                        if ($("#txt" + UpdateField + ID).val().length == "") {
                            $("#txt" + UpdateField + ID).focus();
                            alert("Invalid Date");
                            return;
                        }
                    case "Active":
                        if ($("#txt" + UpdateField + ID).val().length == "") {
                            $("#txt" + UpdateField + ID).focus();
                            alert("Invalid Active status, it could be either true or false");
                            return;
                        }
                        break;
                }

                var Param =
                {
                    ID: ID,
                    UpdateField: UpdateField,
                    Value: $("#txt" + UpdateField + ID).val()
                }

                if (CheckValidFlightType(($("#txtFlightType" + ID).val()))) {
                    $.ajax({
                        type: "POST",
                        url: "OwnFaresJazz_US.aspx/UpdateFareJazz_US",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data: JSON.stringify(Param),
                        responseType: "json",
                        success: function (data) {
                            if (data.d != "true") {
                                alert("Record not successfully Updeted in database!!");
                            }
                        },
                        error: function (data) { alert(data.d) }
                    });
                }
                else {
                    alert("Please enter Flight Type only -1,0,1,2 for All,Direct,One-Stop and Two-Stop respectively");
                    ($("#txtFlightType" + ID).val(''));
                }
            }
        }

        function CheckValidFlightType(flightType) {
            var isValid = false;
            switch (flightType) {
                case "-1":
                    isValid = true;
                case "0":
                    isValid = true;
                case "1":
                    isValid = true;
                case "2":
                    isValid = true;
                    break;
            }
            return isValid;
        }

        function DelSelectedFares() {
            var n = $("input:checked").length;
            if (n > 0)
            {
                if (confirm("Are you sure to delete all selected Own Fares?")) {
                    popup('popProgressBar', 30, 30);
                    $("input[type=checkbox]:checked").each(function ()
                    {
                        debugger
                        var ID = $(this).val();
                        if (ID != "dellall" && ID != "on")
                        {
                            $.ajax({
                                type: "POST",
                                url: "OwnFaresJazz_US.aspx/DeleteFaresJazzUS",
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                data: JSON.stringify({ ID: ID }),
                                responseType: "json",
                                success: function (data) {
                                    var jsdata = JSON.parse(data.d);
                                    if (data.d == "true") {
                                        $("#tr" + ID).hide();
                                        //alert("Own Fare is successfully deleted from database!!");
                                    }
                                    else {
                                        //alert("Own Fare is not successfully deleted from database!!");
                                    }
                                },
                                error: function (data) {
                                    popup('popProgressBar', 30, 30);
                                }
                            });
                        }
                    });
                }
                popup('popProgressBar', 30, 30);
            }
            else {
                alert("Please select fares to delete")
            }
        }

        function SelectAll(ID) {
            if (document.getElementById(ID).checked) {
                $(".checkBoxClass").prop('checked', true);
            }
            else {
                $(".checkBoxClass").prop('checked', false);
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
                        <img src="../Images/Wait.gif" id="ImageProgressbar" />
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #ffffff; color: #B9B9B9; vertical-align: middle; text-align: center; font-size: 18px; font-family: Verdana;" align="center" height="40"></td>
                </tr>
            </tbody>
        </table>
    </div>
</asp:Content>

