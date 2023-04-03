<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="makeBooking.aspx.cs" Inherits="Admin_FltBooking_makeBooking" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
   <%-- <link href="../css/StyleSheet.css" rel="stylesheet" />--%>
    <link href="../../css/StyleSheet.css" rel="stylesheet" />
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <div style="height: 15px;"></div>
        <div class="panel panel-default">
            <div class="panel-heading">Make Flight Reservation</div>
            <div class="panel-body" style="line-height: 34px;">
                <div class="row">
                    <div class="col-md-3">
                        <label>PNR</label>

                        <asp:TextBox runat="server" ID="txtPnr" MaxLength='7' CssClass="form-control" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtPnr" InitialValue="" ValidationGroup="makebooking" runat="server" ErrorMessage="provide pnr"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-md-3">
                        <label>Destination</label>
                        <asp:TextBox runat="server" CssClass="form-control" MaxLength='3' ID="txtDestination" placeholder="Destination Code" />


                    </div>
                    <div class="col-md-3">
                        <label>Company</label>
                        <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control">

                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="ddlCompany" InitialValue="" ValidationGroup="makebooking" runat="server" ErrorMessage="provide company name"></asp:RequiredFieldValidator>
                    </div>                   

                    <div class="col-md-2">
                        <label>&nbsp;</label><br />
                        <asp:Button ID="btnSearch" ValidationGroup="makebooking" OnClientClick="return RetrievePNR();" runat="server" CssClass="btn btn-primary btn-lg" Text="Retrieve PNR" OnClick="btnSearch_Click" />
                    </div>                    

                </div>


                <div class="row">
                    <div class="col-md-12">
                        <asp:Label ID="lblMsg" runat="server" EnableViewState="false" ForeColor="Red"></asp:Label>
                    </div>
                </div>
            </div>
        </div>        

    </div>

 

    <asp:Panel ID="pnlBooking" Visible="false" runat="server">



        <div class="container">
            <div style="height: 15px;"></div>


            <div class="panel panel-default">
                <div class="panel-heading">Travellers Summary</div>
                <div class="panel-body" style="line-height: 34px;">
                    <div class="row">
                        <div class="col-md-3">
                            <label>PNR</label>
                            <label id="lblPNR" class="form-control" runat="server"></label>

                        </div>
                        <div class="col-md-3">
                            <label>Destination</label>
                            <label id="lblDestination" class="form-control" runat="server"></label>


                        </div>
                        <div class="col-md-3">
                            <label>Currency</label>
                            <asp:DropDownList ID="ddlCurrency" runat="server" CssClass="form-control">
                                <asp:ListItem Value="GBP" Selected="True">GBP</asp:ListItem>
                                <asp:ListItem Value="USD">USD</asp:ListItem>
                            </asp:DropDownList>

                        </div>
                         <div class="col-md-3">
                            <label>Booking Status</label>
                            <asp:DropDownList ID="ddlStatus" ValidationGroup="cxp" runat="server" CssClass="form-control">
                                <asp:ListItem Value="--Select--" Selected="True">--Select--</asp:ListItem>
                                <asp:ListItem Value="Option">Option</asp:ListItem>
                                <asp:ListItem Value="Booked">Booked</asp:ListItem>
                                <asp:ListItem Value="Queue">Queue</asp:ListItem>
                            
                            </asp:DropDownList>
                             <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator8" controltovalidate="ddlStatus" InitialValue="--Select--" ValidationGroup="cxp" errormessage="Please Select Booking Status!" />
                        </div>
                        

                    </div>



                </div>
            </div>


            <div class="panel panel-default">
                <div class="panel-heading">Flight Travellers Details</div>
                <div class="panel-body" style="line-height: 34px; padding: 0px!important;">
                    <asp:Repeater ID="rptrPax" runat="server">
                        <HeaderTemplate>
                            <table class="table" style="margin-bottom: 0px;width:100%; cellpadding:0; cellspacing:0;">
                                <tr>
                                    <td class='gdvh'>SrNo</td>
                                    <td class='gdvh'>PaxType</td>
                                    <td class='gdvh'>Title</td>
                                    <td class='gdvh'>First Name</td>
                                    <td class='gdvh'>Last Name</td>
                                    <td class='gdvh'>DOB</td>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td class="gdvr"><%# Container.ItemIndex+ 1 %></td>
                                <td class="gdvr exelcss">
                                    <input maxlength="15" class="form-control" style="width: 100px; font-size: 12px;" id='txtPaxType' runat="server" value='<%# Eval("PassengerType")%>' type="text"></td>
                                <td class="gdvr exelcss">
                                    <input maxlength="15" class="form-control" style="width: 100px; font-size: 12px;" id='txtTitle' runat="server" value='<%# Eval("Title")%>' type="text"></td>
                                <td class="gdvr exelcss">
                                    <input maxlength="40" class="form-control" style="width: 250px; font-size: 12px;" id='txtFirstName' runat="server" value='<%# Eval("FirstName")%>' type="text"></td>
                                <td class="gdvr exelcss">
                                    <input maxlength="40" class="form-control" style="width: 250px; font-size: 12px;" id='txtLastName' runat="server" value='<%# Eval("LastName")%>' type="text"></td>

                                <td class="gdvr exelcss">
                                    <input maxlength="10" class="form-control" style="width: 100px; font-size: 12px;" id='txtDOB' runat="server" value='<%# Eval("DOB")%>' type="text"></td>


                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>

            </div>

            <div class="panel panel-default" id="pnlAddress" runat="server" visible="false">
            <div class="panel-heading">Traveller Address Details</div>
            <div class="panel-body" style="line-height: 34px;">
                
                <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-4">
                            <label>Mobile</label>
                            <asp:TextBox runat="server" ID="txtMobile" CssClass="form-control" />
                            <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator10" controltovalidate="txtMobile" ValidationGroup="cxp" errormessage="Please enter Mobile!" />
                        </div>
                        <div class="col-md-4">
                            <label>Phone</label>
                            <asp:TextBox runat="server" ID="txtPhone" CssClass="form-control" />
                        </div>
                         <div class="col-md-4">
                            <label>Email ID</label>
                            <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" />
                             <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator11" required="required" controltovalidate="txtEmail" ValidationGroup="cxp" errormessage="Please enter Email ID!" />
                        </div>
                         <div class="col-md-4">
                            <label>Address</label>
                            <asp:TextBox runat="server" ID="txtAddress" TextMode="MultiLine" CssClass="form-control" />
                            
                        </div>
                         <div class="col-md-4">
                            <label>City</label>
                            <asp:TextBox runat="server" ID="txtCity" CssClass="form-control" />
                             <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator12" controltovalidate="txtCity" ValidationGroup="cxp" errormessage="Please enter City!" />
                        </div>
                        <div class="col-md-4">
                            <label>Country</label>
                            <asp:DropDownList ID="DdlCountry" runat="server" CssClass="form-control">
                                                        <asp:ListItem Value="">Select country Name</asp:ListItem>
                                                        <asp:ListItem Value="AF">Afghanistan</asp:ListItem>
                                                        <asp:ListItem Value="AX">Aland Islands</asp:ListItem>
                                                        <asp:ListItem Value="AL">Albania</asp:ListItem>
                                                        <asp:ListItem Value="DZ">Algeria</asp:ListItem>
                                                        <asp:ListItem Value="AS">American Samoa</asp:ListItem>
                                                        <asp:ListItem Value="AD">Andorra</asp:ListItem>
                                                        <asp:ListItem Value="AO">Angola</asp:ListItem>
                                                        <asp:ListItem Value="AI">Anguilla</asp:ListItem>
                                                        <asp:ListItem Value="AQ">Antarctica</asp:ListItem>
                                                        <asp:ListItem Value="AG">Antigua and Barbuda</asp:ListItem>
                                                        <asp:ListItem Value="AR">Argentina</asp:ListItem>
                                                        <asp:ListItem Value="AM">Armenia</asp:ListItem>
                                                        <asp:ListItem Value="AW">Aruba</asp:ListItem>
                                                        <asp:ListItem Value="AU">Australia</asp:ListItem>
                                                        <asp:ListItem Value="AT">Austria</asp:ListItem>
                                                        <asp:ListItem Value="AZ">Azerbaijan</asp:ListItem>
                                                        <asp:ListItem Value="BS">Bahamas</asp:ListItem>
                                                        <asp:ListItem Value="BH">Bahrain</asp:ListItem>
                                                        <asp:ListItem Value="BD">Bangladesh</asp:ListItem>
                                                        <asp:ListItem Value="BB">Barbados</asp:ListItem>
                                                        <asp:ListItem Value="BY">Belarus</asp:ListItem>
                                                        <asp:ListItem Value="BE">Belgium</asp:ListItem>
                                                        <asp:ListItem Value="BZ">Belize</asp:ListItem>
                                                        <asp:ListItem Value="BJ">Benin</asp:ListItem>
                                                        <asp:ListItem Value="BM">Bermuda</asp:ListItem>
                                                        <asp:ListItem Value="BT">Bhutan</asp:ListItem>
                                                        <asp:ListItem Value="BO">Bolivia</asp:ListItem>
                                                        <asp:ListItem Value="BA">Bosnia and Herzegovina</asp:ListItem>
                                                        <asp:ListItem Value="BW">Botswana</asp:ListItem>
                                                        <asp:ListItem Value="BV">Bouvet Island</asp:ListItem>
                                                        <asp:ListItem Value="BR">Brazil</asp:ListItem>
                                                        <asp:ListItem Value="IO">British Indian Ocean Territory</asp:ListItem>
                                                        <asp:ListItem Value="BN">Brunei Darussalam</asp:ListItem>
                                                        <asp:ListItem Value="BG">Bulgaria</asp:ListItem>
                                                        <asp:ListItem Value="BF">Burkina Faso</asp:ListItem>
                                                        <asp:ListItem Value="BI">Burundi</asp:ListItem>
                                                        <asp:ListItem Value="KH">Cambodia</asp:ListItem>
                                                        <asp:ListItem Value="CM">Cameroon</asp:ListItem>
                                                        <asp:ListItem Value="CA">Canada</asp:ListItem>
                                                        <asp:ListItem Value="CV">Cape Verde</asp:ListItem>
                                                        <asp:ListItem Value="KY">Cayman Islands</asp:ListItem>
                                                        <asp:ListItem Value="CF">Central African Republic</asp:ListItem>
                                                        <asp:ListItem Value="TD">Chad</asp:ListItem>
                                                        <asp:ListItem Value="CL">Chile</asp:ListItem>
                                                        <asp:ListItem Value="CN">China</asp:ListItem>
                                                        <asp:ListItem Value="CX">Christmas Island</asp:ListItem>
                                                        <asp:ListItem Value="CC">Cocos (Keeling) Islands</asp:ListItem>
                                                        <asp:ListItem Value="CO">Colombia</asp:ListItem>
                                                        <asp:ListItem Value="KM">Comoros</asp:ListItem>
                                                        <asp:ListItem Value="CG">Congo</asp:ListItem>
                                                        <asp:ListItem Value="CD">Congo The Democratic Republic of the</asp:ListItem>
                                                        <asp:ListItem Value="CK">Cook Islands</asp:ListItem>
                                                        <asp:ListItem Value="CR">Costa Rica</asp:ListItem>
                                                        <asp:ListItem Value="CI">C&#244;te d'Ivoire</asp:ListItem>
                                                        <asp:ListItem Value="HR">Croatia</asp:ListItem>
                                                        <asp:ListItem Value="CU">Cuba</asp:ListItem>
                                                        <asp:ListItem Value="CY">Cyprus</asp:ListItem>
                                                        <asp:ListItem Value="CZ">Czech Republic</asp:ListItem>
                                                        <asp:ListItem Value="DK">Denmark</asp:ListItem>
                                                        <asp:ListItem Value="DJ">Djibouti</asp:ListItem>
                                                        <asp:ListItem Value="DM">Dominica</asp:ListItem>
                                                        <asp:ListItem Value="DO">Dominican Republic</asp:ListItem>
                                                        <asp:ListItem Value="EC">Ecuador</asp:ListItem>
                                                        <asp:ListItem Value="EG">Egypt</asp:ListItem>
                                                        <asp:ListItem Value="SV">El Salvador</asp:ListItem>
                                                        <asp:ListItem Value="GQ">Equatorial Guinea</asp:ListItem>
                                                        <asp:ListItem Value="ER">Eritrea</asp:ListItem>
                                                        <asp:ListItem Value="EE">Estonia</asp:ListItem>
                                                        <asp:ListItem Value="ET">Ethiopia</asp:ListItem>
                                                        <asp:ListItem Value="FK">Falkland Islands (Malvinas)</asp:ListItem>
                                                        <asp:ListItem Value="FO">Faroe Islands</asp:ListItem>
                                                        <asp:ListItem Value="FJ">Fiji</asp:ListItem>
                                                        <asp:ListItem Value="FI">Finland</asp:ListItem>
                                                        <asp:ListItem Value="FR">France</asp:ListItem>
                                                        <asp:ListItem Value="GF">French Guiana</asp:ListItem>
                                                        <asp:ListItem Value="PF">French Polynesia</asp:ListItem>
                                                        <asp:ListItem Value="TF">French Southern Territories</asp:ListItem>
                                                        <asp:ListItem Value="GA">Gabon</asp:ListItem>
                                                        <asp:ListItem Value="GM">Gambia</asp:ListItem>
                                                        <asp:ListItem Value="GE">Georgia</asp:ListItem>
                                                        <asp:ListItem Value="DE">Germany</asp:ListItem>
                                                        <asp:ListItem Value="GH">Ghana</asp:ListItem>
                                                        <asp:ListItem Value="GI">Gibraltar</asp:ListItem>
                                                        <asp:ListItem Value="GR">Greece</asp:ListItem>
                                                        <asp:ListItem Value="GL">Greenland</asp:ListItem>
                                                        <asp:ListItem Value="GD">Grenada</asp:ListItem>
                                                        <asp:ListItem Value="GP">Guadeloupe</asp:ListItem>
                                                        <asp:ListItem Value="GU">Guam</asp:ListItem>
                                                        <asp:ListItem Value="GT">Guatemala</asp:ListItem>
                                                        <asp:ListItem Value="GG">Guernsey</asp:ListItem>
                                                        <asp:ListItem Value="GN">Guinea</asp:ListItem>
                                                        <asp:ListItem Value="GW">Guinea-Bissau</asp:ListItem>
                                                        <asp:ListItem Value="GY">Guyana</asp:ListItem>
                                                        <asp:ListItem Value="HT">Haiti</asp:ListItem>
                                                        <asp:ListItem Value="HM">Heard Island and McDonald Islands</asp:ListItem>
                                                        <asp:ListItem Value="VA">Holy See (Vatican City State)</asp:ListItem>
                                                        <asp:ListItem Value="HN">Honduras</asp:ListItem>
                                                        <asp:ListItem Value="HK">Hong Kong</asp:ListItem>
                                                        <asp:ListItem Value="HU">Hungary</asp:ListItem>
                                                        <asp:ListItem Value="IS">Iceland</asp:ListItem>
                                                        <asp:ListItem Value="IN">India</asp:ListItem>
                                                        <asp:ListItem Value="ID">Indonesia</asp:ListItem>
                                                        <asp:ListItem Value="IR">Iran Islamic Republic of</asp:ListItem>
                                                        <asp:ListItem Value="IQ">Iraq</asp:ListItem>
                                                        <asp:ListItem Value="IE">Ireland</asp:ListItem>
                                                        <asp:ListItem Value="IM">Isle of Man</asp:ListItem>
                                                        <asp:ListItem Value="IL">Israel</asp:ListItem>
                                                        <asp:ListItem Value="IT">Italy</asp:ListItem>
                                                        <asp:ListItem Value="JM">Jamaica</asp:ListItem>
                                                        <asp:ListItem Value="JP">Japan</asp:ListItem>
                                                        <asp:ListItem Value="JE">Jersey</asp:ListItem>
                                                        <asp:ListItem Value="JO">Jordan</asp:ListItem>
                                                        <asp:ListItem Value="KZ">Kazakhstan</asp:ListItem>
                                                        <asp:ListItem Value="KE">Kenya</asp:ListItem>
                                                        <asp:ListItem Value="KI">Kiribati</asp:ListItem>
                                                        <asp:ListItem Value="KP">Korea Democratic People's Republic of</asp:ListItem>
                                                        <asp:ListItem Value="KR">Korea Republic of</asp:ListItem>
                                                        <asp:ListItem Value="KW">Kuwait</asp:ListItem>
                                                        <asp:ListItem Value="KG">Kyrgyzstan</asp:ListItem>
                                                        <asp:ListItem Value="LA">Lao People's Democratic Republic</asp:ListItem>
                                                        <asp:ListItem Value="LV">Latvia</asp:ListItem>
                                                        <asp:ListItem Value="LB">Lebanon</asp:ListItem>
                                                        <asp:ListItem Value="LS">Lesotho</asp:ListItem>
                                                        <asp:ListItem Value="LR">Liberia</asp:ListItem>
                                                        <asp:ListItem Value="LY">Libyan Arab Jamahiriya</asp:ListItem>
                                                        <asp:ListItem Value="LI">Liechtenstein</asp:ListItem>
                                                        <asp:ListItem Value="LT">Lithuania</asp:ListItem>
                                                        <asp:ListItem Value="LU">Luxembourg</asp:ListItem>
                                                        <asp:ListItem Value="MO">Macao</asp:ListItem>
                                                        <asp:ListItem Value="MK">Macedonia The Former Yugoslav Republic of</asp:ListItem>
                                                        <asp:ListItem Value="MG">Madagascar</asp:ListItem>
                                                        <asp:ListItem Value="MW">Malawi</asp:ListItem>
                                                        <asp:ListItem Value="MY">Malaysia</asp:ListItem>
                                                        <asp:ListItem Value="MV">Maldives</asp:ListItem>
                                                        <asp:ListItem Value="ML">Mali</asp:ListItem>
                                                        <asp:ListItem Value="MT">Malta</asp:ListItem>
                                                        <asp:ListItem Value="MH">Marshall Islands</asp:ListItem>
                                                        <asp:ListItem Value="MQ">Martinique</asp:ListItem>
                                                        <asp:ListItem Value="MR">Mauritania</asp:ListItem>
                                                        <asp:ListItem Value="MU">Mauritius</asp:ListItem>
                                                        <asp:ListItem Value="YT">Mayotte</asp:ListItem>
                                                        <asp:ListItem Value="MX">Mexico</asp:ListItem>
                                                        <asp:ListItem Value="FM">Microneia Federated States of</asp:ListItem>
                                                        <asp:ListItem Value="MD">Moldova</asp:ListItem>
                                                        <asp:ListItem Value="MC">Monaco</asp:ListItem>
                                                        <asp:ListItem Value="MN">Mongolia</asp:ListItem>
                                                        <asp:ListItem Value="ME">Montenegro</asp:ListItem>
                                                        <asp:ListItem Value="MS">Montserrat</asp:ListItem>
                                                        <asp:ListItem Value="MA">Morocco</asp:ListItem>
                                                        <asp:ListItem Value="MZ">Mozambique</asp:ListItem>
                                                        <asp:ListItem Value="MM">Myanmar</asp:ListItem>
                                                        <asp:ListItem Value="NA">Namibia</asp:ListItem>
                                                        <asp:ListItem Value="NR">Nauru</asp:ListItem>
                                                        <asp:ListItem Value="NP">Nepal</asp:ListItem>
                                                        <asp:ListItem Value="NL">Netherlands</asp:ListItem>
                                                        <asp:ListItem Value="AN">Netherlands Antilles</asp:ListItem>
                                                        <asp:ListItem Value="NC">New Caledonia</asp:ListItem>
                                                        <asp:ListItem Value="NZ">New Zealand</asp:ListItem>
                                                        <asp:ListItem Value="NI">Nicaragua</asp:ListItem>
                                                        <asp:ListItem Value="NE">Niger</asp:ListItem>
                                                        <asp:ListItem Value="NG">Nigeria</asp:ListItem>
                                                        <asp:ListItem Value="NU">Niue</asp:ListItem>
                                                        <asp:ListItem Value="NF">Norfolk Island</asp:ListItem>
                                                        <asp:ListItem Value="MP">Northern Mariana Islands</asp:ListItem>
                                                        <asp:ListItem Value="NO">Norway</asp:ListItem>
                                                        <asp:ListItem Value="OM">Oman</asp:ListItem>
                                                        <asp:ListItem Value="PK">Pakistan</asp:ListItem>
                                                        <asp:ListItem Value="PW">Palau</asp:ListItem>
                                                        <asp:ListItem Value="PS">Palestinian Territory Occupied</asp:ListItem>
                                                        <asp:ListItem Value="PA">Panama</asp:ListItem>
                                                        <asp:ListItem Value="PG">Papua New Guinea</asp:ListItem>
                                                        <asp:ListItem Value="PY">Paraguay</asp:ListItem>
                                                        <asp:ListItem Value="PE">Peru</asp:ListItem>
                                                        <asp:ListItem Value="PH">Philippines</asp:ListItem>
                                                        <asp:ListItem Value="PN">Pitcairn</asp:ListItem>
                                                        <asp:ListItem Value="PL">Poland</asp:ListItem>
                                                        <asp:ListItem Value="PT">Portugal</asp:ListItem>
                                                        <asp:ListItem Value="PR">Puerto Rico</asp:ListItem>
                                                        <asp:ListItem Value="QA">Qatar</asp:ListItem>
                                                        <asp:ListItem Value="RE">R&#233;union</asp:ListItem>
                                                        <asp:ListItem Value="RO">Romania</asp:ListItem>
                                                        <asp:ListItem Value="RU">Russian Federation</asp:ListItem>
                                                        <asp:ListItem Value="RW">Rwanda</asp:ListItem>
                                                        <asp:ListItem Value="BL">Saint Barth&#233;lemy</asp:ListItem>
                                                        <asp:ListItem Value="SH">Saint Helena</asp:ListItem>
                                                        <asp:ListItem Value="KN">Saint Kitts and Nevis</asp:ListItem>
                                                        <asp:ListItem Value="LC">Saint Lucia</asp:ListItem>
                                                        <asp:ListItem Value="MF">Saint Martin</asp:ListItem>
                                                        <asp:ListItem Value="PM">Saint Pierre and Miquelon</asp:ListItem>
                                                        <asp:ListItem Value="VC">Saint Vincent and the Grenadines</asp:ListItem>
                                                        <asp:ListItem Value="WS">Samoa</asp:ListItem>
                                                        <asp:ListItem Value="SM">San Marino</asp:ListItem>
                                                        <asp:ListItem Value="ST">Sao Tome and Principe</asp:ListItem>
                                                        <asp:ListItem Value="SA">Saudi Arabia</asp:ListItem>
                                                        <asp:ListItem Value="SN">Senegal</asp:ListItem>
                                                        <asp:ListItem Value="RS">Serbia</asp:ListItem>
                                                        <asp:ListItem Value="SC">Seychelles</asp:ListItem>
                                                        <asp:ListItem Value="SL">Sierra Leone</asp:ListItem>
                                                        <asp:ListItem Value="SG">Singapore</asp:ListItem>
                                                        <asp:ListItem Value="SK">Slovakia</asp:ListItem>
                                                        <asp:ListItem Value="SI">Slovenia</asp:ListItem>
                                                        <asp:ListItem Value="SB">Solomon Islands</asp:ListItem>
                                                        <asp:ListItem Value="SO">Somalia</asp:ListItem>
                                                        <asp:ListItem Value="ZA">South Africa</asp:ListItem>
                                                        <asp:ListItem Value="GS">South Georgia and the South Sandwich Islands</asp:ListItem>
                                                        <asp:ListItem Value="ES">Spain</asp:ListItem>
                                                        <asp:ListItem Value="LK">Sri Lanka</asp:ListItem>
                                                        <asp:ListItem Value="SD">Sudan</asp:ListItem>
                                                        <asp:ListItem Value="SR">Suriname</asp:ListItem>
                                                        <asp:ListItem Value="SJ">Svalbard and Jan Mayen</asp:ListItem>
                                                        <asp:ListItem Value="SZ">Swaziland</asp:ListItem>
                                                        <asp:ListItem Value="SE">Sweden</asp:ListItem>
                                                        <asp:ListItem Value="CH">Switzerland</asp:ListItem>
                                                        <asp:ListItem Value="SY">Syrian Arab Republic</asp:ListItem>
                                                        <asp:ListItem Value="TW">Taiwan Province of China</asp:ListItem>
                                                        <asp:ListItem Value="TJ">Tajikistan</asp:ListItem>
                                                        <asp:ListItem Value="TZ">Tanzania United Republic of</asp:ListItem>
                                                        <asp:ListItem Value="TH">Thailand</asp:ListItem>
                                                        <asp:ListItem Value="TL">Timor-Leste</asp:ListItem>
                                                        <asp:ListItem Value="TG">Togo</asp:ListItem>
                                                        <asp:ListItem Value="TK">Tokelau</asp:ListItem>
                                                        <asp:ListItem Value="TO">Tonga</asp:ListItem>
                                                        <asp:ListItem Value="TT">Trinidad and Tobago</asp:ListItem>
                                                        <asp:ListItem Value="TN">Tunisia</asp:ListItem>
                                                        <asp:ListItem Value="TR">Turkey</asp:ListItem>
                                                        <asp:ListItem Value="TM">Turkmenistan</asp:ListItem>
                                                        <asp:ListItem Value="TC">Turks and Caicos Islands</asp:ListItem>
                                                        <asp:ListItem Value="TV">Tuvalu</asp:ListItem>
                                                        <asp:ListItem Value="UG">Uganda</asp:ListItem>
                                                        <asp:ListItem Value="UA">Ukraine</asp:ListItem>
                                                        <asp:ListItem Value="AE">United Arab Emirates</asp:ListItem>
                                                        <asp:ListItem Selected="True" Value="GB">United Kingdom</asp:ListItem>
                                                        <asp:ListItem Value="US">United States</asp:ListItem>
                                                        <asp:ListItem Value="UM">United States Minor Outlying Islands</asp:ListItem>
                                                        <asp:ListItem Value="UY">Uruguay</asp:ListItem>
                                                        <asp:ListItem Value="UZ">Uzbekistan</asp:ListItem>
                                                        <asp:ListItem Value="VU">Vanuatu</asp:ListItem>
                                                        <asp:ListItem Value="VE">Venezuela</asp:ListItem>
                                                        <asp:ListItem Value="VN">Viet Nam</asp:ListItem>
                                                        <asp:ListItem Value="VG">Virgin Islands British</asp:ListItem>
                                                        <asp:ListItem Value="VI">Virgin Islands U.S.</asp:ListItem>
                                                        <asp:ListItem Value="WF">Wallis and Futuna</asp:ListItem>
                                                        <asp:ListItem Value="EH">Western Sahara</asp:ListItem>
                                                        <asp:ListItem Value="YE">Yemen</asp:ListItem>
                                                        <asp:ListItem Value="ZM">Zambia</asp:ListItem>
                                                        <asp:ListItem Value="ZW">Zimbabwe</asp:ListItem>
                                                    </asp:DropDownList>

                        </div>
                       
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-12">
                        <asp:Label ID="Label1" runat="server" EnableViewState="false" ForeColor="Red"></asp:Label>
                    </div>
                </div>
            </div>
        </div>


            <div class="panel panel-default">
                <div class="panel-heading">Flight Sector Details</div>
                <div class="panel-body" style="line-height: 34px; padding: 0px!important;">
                    <asp:Repeater ID="rptrSect" runat="server">
                        <HeaderTemplate>
                            <table width='100%' cellpadding='0' cellspacing='0' class='table' style='margin-bottom: 0px;'>
                                <tr>
                                    <td class='gdvh'>SrNo</td>
                                    <td class='gdvh'>From</td>
                                    <td class='gdvh'>To</td>
                                    <td class='gdvh'>AirV</td>
                                    <td class='gdvh'>FLT No.</td>
                                    <td class='gdvh'>Class</td>
                                    <td class='gdvh'>FromDate</td>
                                    <td class='gdvh'>FromTime</td>
                                    <td class='gdvh'>ToDate</td>
                                    <td class='gdvh'>ToTime</td>
                                    <td class='gdvh'>Cabin</td>
                                    <td class='gdvh'>Status</td>

                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td class="gdvr"><%# Container.ItemIndex+ 1 %></td>
                                <td class="gdvr exelcss">
                                    
                                    <asp:TextBox ID="txtFrom" MaxLength="3" Width="60px" Font-Size="12px" CssClass="form-control" SetFocusOnError="true" Text='<%# Eval("Departure.AirportCode")%>' ValidationGroup="cxp"  runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator1" controltovalidate="txtFrom" ValidationGroup="cxp" errormessage="Please enter Departure!" />
                                </td>
                                <td class="gdvr exelcss">

                                    <asp:TextBox ID="txtTo" MaxLength="3" Width="60px" Font-Size="12px" CssClass="form-control"  SetFocusOnError="true"  Text='<%# Eval("Arrival.AirportCode")%>' ValidationGroup="cxp"  runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator2" controltovalidate="txtTo" ValidationGroup="cxp" errormessage="Please enter Destination!" />
                                </td>
                                
                                <td class="gdvr exelcss">
                                    
                                    <asp:TextBox ID="txtAirV" MaxLength="2" Width="60px" Font-Size="12px" CssClass="form-control"  SetFocusOnError="true"  Text='<%# Eval("AirV")%>' ValidationGroup="cxp"  runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator3" controltovalidate="txtAirV" ValidationGroup="cxp" errormessage="Please enter Airline!" />
                                </td>
                                <td class="gdvr exelcss">
                                   
                                    <asp:TextBox ID="txtFLTNO" MaxLength="6" Width="80px" Font-Size="12px" CssClass="form-control"  SetFocusOnError="true"  Text='<%# Eval("FltNum")%>' ValidationGroup="cxp"  runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator4" controltovalidate="txtFLTNO" ValidationGroup="cxp" errormessage="Please enter Flight No!" />
                                </td>
                                <td class="gdvr exelcss">
                                   
                                    <asp:TextBox ID="txtClass" MaxLength="1" Width="40px" Font-Size="12px" CssClass="form-control"  Text='<%# Eval("Class")%>' ValidationGroup="cxp"  runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator5" SetFocusOnError="true" controltovalidate="txtClass" ValidationGroup="cxp" errormessage="Please enter Class!" />
                                </td>
                                <td class="gdvr exelcss">
                                    
                                    <asp:TextBox ID="FromDate" MaxLength="10" Width="100px" Font-Size="12px" CssClass="form-control"  SetFocusOnError="true"  Text='<%#Convert.ToDateTime(Eval("Departure.Date")).ToString("dd/MM/yyyy")%>' ValidationGroup="cxp"  runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator6" SetFocusOnError="true"  controltovalidate="FromDate" ValidationGroup="cxp" errormessage="Please enter Dep Date!" />
                                </td>
                                <td class="gdvr exelcss">
                                    <asp:TextBox ID="FromTime" MaxLength="5" Width="80px" Font-Size="12px" CssClass="form-control"  SetFocusOnError="true"  Text='<%# Eval("Departure.Time")%>' ValidationGroup="cxp"  runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator7" controltovalidate="FromTime" ValidationGroup="cxp" errormessage="Please enter Dep Time!" />
                                </td>
                                <td class="gdvr exelcss">
                                    <asp:TextBox ID="ToDate" MaxLength="10" Width="100px" Font-Size="12px" CssClass="form-control"  SetFocusOnError="true"  Text='<%# Convert.ToDateTime(Eval("Arrival.Date")).ToString("dd/MM/yyyy") %>' ValidationGroup="cxp"  runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator8" controltovalidate="ToDate" ValidationGroup="cxp" errormessage="Please Arrival Date!" />
                                </td>
                                <td class="gdvr exelcss">
                                    
                                    <asp:TextBox ID="ToTime" MaxLength="5" Width="80px" Font-Size="12px" CssClass="form-control"  SetFocusOnError="true"  Text='<%# Eval("Arrival.Time")%>' ValidationGroup="cxp"  runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator9" controltovalidate="ToTime" ValidationGroup="cxp" errormessage="Please enter Arrival Time!" />
                                </td>


                                <td class="gdvr exelcss">
                                    <asp:Label ID="lblCabin" MaxLength="5" Width="80px" Font-Size="12px" CssClass="form-control" SetFocusOnError="true" Text='<%# Eval("CabinClass.Name")%>' runat="server"></asp:Label>
                                </td>

                                <%-- <td class="gdvr exelcss" style="width: 80px;">
                                    <input type="text" class="ddlCabinClass" style="display: none!important" name="name" value='<%# Eval("CabinClass.Name")%>' />

                                    <select id='ddlCabin' class="form-control" style="width: 130px!important; padding: 6px 7px">
                                        <option value="">ANY</option>
                                        <option value="Economy">Economy</option>
                                        <option value="Business">Business</option>
                                        <option value="FirstClass">FirstClass</option>
                                        <option value="Premium">Premium Economy</option>
                                    </select>
                                </td>--%>

                                <td class="gdvr exelcss">
                                    <input maxlength="6" class="form-control" style="width: 60px; font-size: 12px;" id='txtStatus' runat="server" value='<%# Eval("Status")%>' type="text"></td>

                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
            </div>

            <div class="panel panel-default">
                <div class="panel-heading">Pricebreak up</div>
                <div class="panel-body" style="line-height: 34px; padding: 0px!important;">
                    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:Repeater ID="rptPrice" runat="server" OnItemCommand="rptPrice_ItemCommand">
                                <HeaderTemplate>
                                    <table width='100%' cellpadding='0' cellspacing='0' height="100px" class='table' style='margin-bottom: 0px'>
                                        <tr>

                                            <td class='gdvh'>Charge Type</td>
                                            <td class='gdvh'>Charge For</td>
                                            <td class='gdvh'>Sell Amt</td>
                                            <td class='gdvh'>Cost Amt</td>
                                            <td class='gdvh'></td>

                                        </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td class="gdvr exelcss">
                                            <asp:Label ID="lbtChargeID" runat="server" Text='<%# Eval("AMT_CHG_MST_Charge_ID")%>'></asp:Label>
                                        </td>
                                        <td class="gdvr exelcss">
                                            <asp:Label ID="lblChargeFor" runat="server" Text='<%# Eval("AMT_CHG_DTL_Charges_For")%>'></asp:Label>
                                        </td>
                                        <td class="gdvr exelcss">
                                            <input maxlength="10" class="form-control" style="width: 100px; font-size: 12px;" id='txtSalePrice' value='<%# Convert.ToDouble(Eval("AMT_CHG_DTL_Sell_Price")) %>' type="text"></td>
                                        <td class="gdvr exelcss">
                                            <input maxlength="10" class="form-control" style="width: 100px; font-size: 12px;" id='txtCostPrice' value='<%# Convert.ToDouble(Eval("AMT_CHG_DTL_Cost_Price"))%>' type="text"></td>
                                        <td class="gdvr">
                                            <asp:Button ID="btnDelete" CommandName="del" runat="server" Text="Delete" CssClass="btn btn-danger btn-sm" ToolTip='<%# Eval("AMT_CHG_MST_Charge_ID") +"|"+Eval("AMT_CHG_DTL_Charges_For") %>' />
                                        </td>
                                    </tr>
                                </ItemTemplate>

                            </asp:Repeater>
                            <table width='100%' cellpadding='0' cellspacing='0' class='table' style=' background-color:#ddd; margin-bottom: 0px;'>
                                <tr>

                                    <td class='gdvh'>Charge Type</td>
                                    <td class='gdvh'>Charge For</td>
                                    <td class='gdvh'>Sell Amt</td>
                                    <td class='gdvh'>Cost Amt</td>
                                    <td class='gdvh'></td>
                                </tr>
                                <tr>
                                    <td class='gdvh'>

                                        <select id="ddlPayType" class="form-control" runat="server">
                                            <option value="Fare" selected="selected">Fare</option>
                                            <option value="Tax">Tax</option>
                                            <option value="Markup">Markup</option>
                                            <option value="Safi">Safi</option>
                                            <option value="Atol">Atol</option>
                                            <option value="CC">Card Charge</option>
                                            <option value="Admin">Admin Charge</option>
                                              <option value="CC">Card Charge</option>
                                            <option value="PTS">PTS</option>
                                            <option value="Issuance">Issuance Charge</option>
                                            <option value="Others">Others</option>
                                            <option value="Admin2">Admin2</option>
                                              <option value="FXL">FXL</option>
                                           
                                        </select>
                                    </td>
                                    <td class='gdvh'>

                                        <select id="ddlChargeFor" class="form-control" runat="server">
                                            <option value="ADT" >Adult</option>
                                            <option value="CNN">Child</option>
                                            <option value="INF">Infant</option>                                          
                                            <option value="NA" selected="selected">NA</option>
                                        </select>
                                    </td>

                                    <td class='gdvh'>
                                        <input maxlength="10" class="form-control" style="width: 100px; font-size: 12px;" onkeyup="AllowDecimal(this)" id='txtSalePriceF' runat="server" type="text" /></td>
                                    <td class='gdvh'>
                                        <input maxlength="10" class="form-control" style="width: 100px; font-size: 12px;" onkeyup="AllowDecimal(this)" id='txtCostPriceF' runat="server" type="text" /></td>

                                    <td class='gdvh'>
                                        <asp:Button ID="btnAdd" runat="server" OnClientClick="return validate();" Text="Add" OnClick="btnAdd_Click" />
                                    </td>

                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <div class="row text-center">

                        <asp:UpdateProgress ID="UpdateProgress2" AssociatedUpdatePanelID="UpdatePanel2" DisplayAfter="1"
                            runat="server">
                            <ProgressTemplate>
                                <asp:Literal ID="Literal2" runat="server" Text="Wait"></asp:Literal>
                            </ProgressTemplate>
                        </asp:UpdateProgress>

                    </div>
                </div>
            </div>
            <div class="panel panel-default p-20">
                <div class="col-md-12">
                    <div class="col-md-9">
                        <asp:TextBox ID="txtRemarks" Width="100%" placeholder="Remarks" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <asp:Button ID="btnSave" CssClass="btn btn-primary btn-lg" runat="server" ValidationGroup="cxp" Text="Create" OnClick="btnSave_Click" /></div>
                     <asp:ValidationSummary ID="valsum" runat="server" ValidationGroup="cxp" ForeColor="Red" ShowMessageBox="true" ShowSummary="false" />
                </div>
                <div class="clearfix"></div>
            </div>

        </div>
    </asp:Panel>

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
        function RetrievePNR() {

            if (document.getElementById('<%=txtPnr.ClientID%>').value == "") {
                alert("Enter PNR.!");
                document.getElementById('<%=txtPnr.ClientID%>').focus();
                return false;
            }
            if (document.getElementById('<%=txtDestination.ClientID%>').value == "") {
                alert("Enter Destination Code.!");
                document.getElementById('<%=txtDestination.ClientID%>').focus();
                return false;
            }
            if (document.getElementById('<%=ddlCompany.ClientID%>').value == "") {
                alert("Select Company.!");
                document.getElementById('<%=ddlCompany.ClientID%>').focus();
                return false;
            }

            waitingDialog.show('Please Wait, retrieving pnr details...');
            return true;
        }

        function MirrorValue() {
            document.getElementById('<%=txtSalePriceF.ClientID%>').value = parseFloat(document.getElementById('<%= txtCostPriceF.ClientID %>').value);
        }
        function AllowDecimal(txt) {
            if (/[^\d.]/g.test(txt.value))
                txt.value = txt.value.replace(/[^\d.]/g, '');
        }
        function validate() {
            if (document.getElementById('<%=txtSalePriceF.ClientID%>').value == "") {
                alert("Enter Sale Price.");
                document.getElementById('<%=txtSalePriceF.ClientID%>').focus();
                return false;
            }
            if (document.getElementById('<%=txtCostPriceF.ClientID%>').value == "") {
                alert("Enter Cost Price.");
                document.getElementById('<%=txtCostPriceF.ClientID%>').focus();
                return false;
            }
        }
        function validatexp()
        {
              if (document.getElementById('<%=ddlStatus.ClientID%>').value == "") {
                alert("Select Booking Status.");
                document.getElementById('<%=ddlStatus.ClientID%>').focus();
                return false;
            }

        }
        if ($("#Text1").val() == "") {
            args.IsValid = false;
        }

    </script>
</asp:Content>

