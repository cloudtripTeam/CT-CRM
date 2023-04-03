<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true"
    CodeFile="DumpFlightFaresDetails.aspx.cs" Inherits="Admin_DumpFlightFaresDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
  <%--  <link href="../css/StyleSheet.css" rel="stylesheet" />--%>
    <link href="../../css/StyleSheet.css" rel="stylesheet" />
    <link rel="stylesheet" href="../css/jquery-ui.css" />
    <script src="../js/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../js/jquery-ui.js"></script>
    <script>
        $(function () {
            $("#txtTravelStartFrom").datepicker({ dateFormat: 'dd M yy' });
            $("#txtTravelStartTo").datepicker({ dateFormat: 'dd M yy' });
            $("#txtTravelEndFrom").datepicker({ dateFormat: 'dd M yy' });
            $("#txtTravelEndTo").datepicker({ dateFormat: 'dd M yy' });
            $("#txtDumpDateFrom").datepicker({ dateFormat: 'dd M yy' });
            $("#txtDumpDateTo").datepicker({ dateFormat: 'dd M yy' });
            $("#txtExpDateFrom").datepicker({ dateFormat: 'dd M yy' });
            $("#txtExpDateFrom").datepicker({ dateFormat: 'dd M yy' });
            $("#txtEditExpDate").datepicker({ dateFormat: 'dd M yy' });
            $("#txtEditTravelStart").datepicker({ dateFormat: 'dd M yy' });
            $("#txtEditTravelEnd").datepicker({ dateFormat: 'dd M yy' });
        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <input type="hidden" id="setascurrdate" />
    <input type="hidden" id="hdeprdate" />
    <div class="container">
        <div style="height: 15px;"></div>
        <div class="panel panel-default">
            <div class="panel-heading">Dump Flight Fares Details</div>
            <div class="panel-body" style="line-height: 34px;">
                <div class="row">
                    <div class="col-md-2">
                        <asp:Label runat="server">From Destination</asp:Label>
                        <input type="text" id="txtFrom" class="form-control" placeholder="ANY" />
                    </div>
                    <div class="col-md-2">
                        <asp:Label runat="server">To Destination</asp:Label>
                        <input type="text" id="txtTo" class="form-control" placeholder="ANY" />
                    </div>
                    <div class="col-md-3">
                        <asp:Label runat="server">Airline</asp:Label>
                        <select id="ddlAirline" class="form-control">
                            <option value="">Any Airline</option>
                            <option value="JP">Adria Airways</option>
                            <option value="A3">Aegean Airlines</option>
                            <option value="EI">Aer Lingus</option>
                            <option value="WV">Aero Vip</option>
                            <option value="SU">Aeroflot</option>
                            <option value="2K">Aerogal Aerolineas Galapagos</option>
                            <option value="AR">Aerolineas Argentinas</option>
                            <option value="AM">Aeromexico</option>
                            <option value="P5">Aerorepublica</option>
                            <option value="5L">AeroSur</option>
                            <option value="VV">Aerosvit Airlines</option>
                            <option value="8U">Afriqiyah</option>
                            <option value="ZI">Aigle Azur</option>
                            <option value="AH">Air Algerie</option>
                            <option value="KC">Air Astana</option>
                            <option value="UU">Air Austral</option>
                            <option value="AB">Air Berlin</option>
                            <option value="2J">Air Burkina </option>
                            <option value="AC">Air Canada</option>
                            <option value="TX">Air Caraibes</option>
                            <option value="CA">Air China </option>
                            <option value="A7">Air Comet </option>
                            <option value="UX">Air Europa </option>
                            <option value="AF">Air France </option>
                            <option value="AI">Air India </option>
                            <option value="JM">Air Jamaica</option>
                            <option value="NX">Air Macau</option>
                            <option value="KM">Air Malta  </option>
                            <option value="MK">Air Mauritius </option>
                            <option value="9U">Air Moldova</option>
                            <option value="SW">Air Namibia </option>
                            <option value="NZ">Air New Zealand </option>
                            <option value="JU">Air Serbia</option>
                            <option value="HM">Air Seychelles </option>
                            <option value="TN">Air Tahiti Nui </option>
                            <option value="TS">Air Transat</option>
                            <option value="BT">airBaltic</option>
                            <option value="AS">Alaska Airlines</option>
                            <option value="LV">Albanian Airlines</option>
                            <option value="AZ">Alitalia</option>
                            <option value="CT">Alitalia CityLiner</option>
                            <option value="NH">All Nippon Airways</option>
                            <option value="AA">American Airlines</option>
                            <option value="W3">Arik Air</option>
                            <option value="IZ">Arkia Israeli Airlines</option>
                            <option value="OZ">Asiana Airlines</option>
                            <option value="KP">ASKY Airlines</option>
                            <option value="KK">AtlasJet Airlines</option>
                            <option value="UI">Auric Air</option>
                            <option value="GR">Aurigny</option>
                            <option value="OS">Austrian Airlines</option>
                            <option value="O6">Avianca</option>
                            <option value="AV">Avianca</option>
                            <option value="J2">Azerbaijan Airlines</option>
                            <option value="UP">Bahamasair</option>
                            <option value="PG">Bangkok Airways </option>
                            <option value="B2">Belavia</option>
                            <option value="BG">Biman Bangladesh</option>
                            <option value="0B">Blue Air</option>
                            <option value="SI">Blue Islands</option>
                            <option value="BV">Blue Panorama Airlines</option>
                            <option value="BD">Bmi</option>
                            <option value="BM">BMI Regional</option>
                            <option value="BA">British Airways</option>
                            <option value="SN">Brussels Airline </option>
                            <option value="FB">Bulgaria Air</option>
                            <option value="QC">Camair-Co</option>
                            <option value="9K">Cape Air</option>
                            <option value="BW">Caribbean Airlines</option>
                            <option value="CX">Cathay Pacific </option>
                            <option value="KX">Cayman Airways</option>
                            <option value="CI">China Airlines </option>
                            <option value="MU">China Eastern Airlines</option>
                            <option value="CZ">China Southern Airlines</option>
                            <option value="QI">Cimber Sterling</option>
                            <option value="CF">City Airline</option>
                            <option value="WX">Cityjet</option>
                            <option value="DE">Condor Airlines</option>
                            <option value="CO">Continental Airlines</option>
                            <option value="CM">Copa Airlines</option>
                            <option value="OU">Croatia Airlines</option>
                            <option value="CU">Cubana Airlines </option>
                            <option value="CY">Cyprus Airways </option>
                            <option value="OK">Czech Airlines </option>
                            <option value="F7">Darwin Airline</option>
                            <option value="DL">Delta Air Lines</option>
                            <option value="T3">Eastern Airways</option>
                            <option value="U2">easyJet</option>
                            <option value="EZY">easyJet</option>
                            <option value="MS">EgyptAir</option>
                            <option value="LY">El AI</option>
                            <option value="EK">Emirates </option>
                            <option value="OV">Estonian Air</option>
                            <option value="ET">Ethiopian Airlines</option>
                            <option value="EY">Etihad Airways </option>
                            <option value="BR">EVA Air</option>
                            <option value="FN">Fastjet</option>
                            <option value="FJ">Fiji Airways</option>
                            <option value="AY">Finnair</option>
                            <option value="W2">FlexFlight</option>
                            <option value="Z7">FlyAfrica</option>
                            <option value="BE">Flybe</option>
                            <option value="FZ">FlyDubai</option>
                            <option value="GA">Garuda</option>
                            <option value="ST">Germania</option>
                            <option value="4U">Germanwings</option>
                            <option value="GF">Gulf Air </option>
                            <option value="H1">Hahn Air</option>
                            <option value="HR">Hahn Air </option>
                            <option value="HU">Hainan Airlines</option>
                            <option value="YO">Heli Air Monaco</option>
                            <option value="2L">Helvetic Airways</option>
                            <option value="EO">Hewa Bora Airways</option>
                            <option value="HX">Hong Kong Airlines</option>
                            <option value="IB">Iberia </option>
                            <option value="FI">Icelandair</option>
                            <option value="IC">Indian Airlines </option>
                            <option value="IR">Iran Air  </option>
                            <option value="6H">Israir Airlines</option>
                            <option value="JL">Japan Airlines</option>
                            <option value="9W">Jet Airways </option>
                            <option value="JQ">Jetstar Airways</option>
                            <option value="RQ">KAM Air</option>
                            <option value="KQ">Kenya Airways </option>
                            <option value="IT">Kingfisher Airlines</option>
                            <option value="KL">KLM</option>
                            <option value="KE">Korean Air </option>
                            <option value="KU">Kuwait Airways </option>
                            <option value="LR">LACSA</option>
                            <option value="TM">LAM</option>
                            <option value="LA">LAN Airlines</option>
                            <option value="XL">Lan Ecuador</option>
                            <option value="LI">LIAT</option>
                            <option value="LN">Libyan Airlines</option>
                            <option value="LO">LOT Polish Airlines</option>
                            <option value="LH">Lufthansa</option>
                            <option value="LG">Luxair</option>
                            <option value="W5">Mahan Air </option>
                            <option value="MH">Malaysia Airlines</option>
                            <option value="MA">Malev Hungarian</option>
                            <option value="MP">Martinair</option>
                            <option value="7M">MAY Air</option>
                            <option value="IG">Meridiana</option>
                            <option value="MX">Mexicana Airlines</option>
                            <option value="ME">Middle East Airlines</option>
                            <option value="MJ">Mihin Lanka</option>
                            <option value="ZB">Monarch</option>
                            <option value="YM">Montenegro Airlines</option>
                            <option value="CE">Nationwide Airlines</option>
                            <option value="XY">NATL Air</option>
                            <option value="HG">NIKI</option>
                            <option value="NW">Northwest Airlines</option>
                            <option value="DY">Norwegian</option>
                            <option value="OA">Olympic Air</option>
                            <option value="WY">Oman Air</option>
                            <option value="P6">Pascan Aviation</option>
                            <option value="2Z">Passaredo</option>
                            <option value="PC">Pegasus Airlines</option>
                            <option value="PR">Philippine Airlines</option>
                            <option value="PK">PIA</option>
                            <option value="QF">Qantas Airways</option>
                            <option value="QR">Qatar Airways</option>
                            <option value="FV">Rossiya</option>
                            <option value="RG">Rotana Jet</option>
                            <option value="AT">Royal Air Maroc</option>
                            <option value="BI">Royal Brunei Airlines</option>
                            <option value="RJ">Royal Jordanian</option>
                            <option value="WB">RwandAir</option>
                            <option value="RYR">RyanAir</option>
                            <option value="FR">RyanAir</option>
                            <option value="S7">S7 Airlines</option>
                            <option value="F2">Safarilink</option>
                            <option value="4Q">Safi Airways</option>
                            <option value="S4">SATA International</option>
                            <option value="SV">Saudi Arabian Airlines</option>
                            <option value="SK">Scandinavian Airlines</option>
                            <option value="DN">Senegal Airlines</option>
                            <option value="SC">Shandong Airlines</option>
                            <option value="FM">Shanghai Airlines</option>
                            <option value="ZH">Shenzhen Airlines</option>
                            <option value="SQ">Singapore Airlines</option>
                            <option value="QS">SmartWings</option>
                            <option value="SA">South African Airways</option>
                            <option value="UL">SriLankan Airlines</option>
                            <option value="SD">Sudan Airways</option>
                            <option value="LX">SWISS</option>
                            <option value="7E">Sylt Air</option>
                            <option value="RB">Syrian Air</option>
                            <option value="DT">TAAG Angola Airlines</option>
                            <option value="TA">Taca International</option>
                            <option value="VR">TACV Cabo Verde Airlines</option>
                            <option value="PZ">TAM Airlines</option>
                            <option value="JJ">TAM Airlines</option>
                            <option value="EQ">TAME</option>
                            <option value="TP">Tap Portugal </option>
                            <option value="RO">Tarom</option>
                            <option value="TG">Thai Airways</option>
                            <option value="MT">Thomas Cook Airlines</option>
                            <option value="BY">Thomson Airways</option>
                            <option value="TOM">Thomson Airways</option>
                            <option value="UN">Transaero Airlines</option>
                            <option value="5U">Transportes Aereos Guatemaltecos</option>
                            <option value="9N">Tropic Air</option>
                            <option value="TU">Tunisair</option>
                            <option value="TK">Turkish Airlines</option>
                            <option value="T5">Turkmenistan Airlines</option>
                            <option value="VO">Tyrolean Airways</option>
                            <option value="PS">Ukraine International Airlines</option>
                            <option value="UA">United Airlines</option>
                            <option value="US">US Airways</option>
                            <option value="HY">Uzbekistan Airways</option>
                            <option value="VJ">VietJetAir</option>
                            <option value="VN">Vietnam Airlines</option>
                            <option value="VS">Virgin Atlantic</option>
                            <option value="VA">Virgin Australia</option>
                            <option value="DJ">Virgin Australia</option>
                            <option value="VK">Virgin Nigeria</option>
                            <option value="VY">Vueling</option>
                            <option value="WS">WestJet</option>
                            <option value="WM">Winair</option>
                            <option value="X9">Wow Air</option>
                            <option value="MF">Xiamen Airlines</option>
                            <option value="IY">Yemen Airways</option>

                        </select>
                    </div>
                    <div class="col-md-3">
                        <asp:Label runat="server">Cabin Class</asp:Label>
                        <select id="ddlClassType" class="form-control">
                            <option value="">ANY</option>
                            <option value="ECONOMY">ECONOMY</option>
                            <option value="BUSINESS">BUSINESS</option>
                            <option value="FIRSTCLASS">FIRSTCLASS</option>
                            <option value="PREMIUM">PREMIUM</option>
                        </select>
                    </div>
                    <div class="col-md-2">
                        <asp:Label runat="server">Class</asp:Label>
                        <input type="text" class="form-control" id="txtClass" placeholder="ANY" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        <asp:Label runat="server">WebSite</asp:Label>
                        <select id="ddlWebsite" class="form-control">
                            <option value="">ANY</option>
                        </select>
                    </div>
                    <div class="col-md-2">
                        <asp:Label runat="server">Travel Start From</asp:Label>
                        <input type="text" class="form-control" id="txtTravelStartFrom" placeholder="DD MMM  YYYY" />
                    </div>
                    <div class="col-md-3">
                        <asp:Label runat="server">Travel Start To</asp:Label>
                        <input type="text" class="form-control" id="txtTravelStartTo" placeholder="DD MMM  YYYY" />
                    </div>
                    <div class="col-md-2">
                        <asp:Label runat="server">Travel End From</asp:Label>
                        <input type="text" class="form-control" id="txtTravelEndFrom" placeholder="DD MMM  YYYY" />
                    </div>
                    <div class="col-md-3">
                        <asp:Label runat="server">Travel End To</asp:Label>
                        <input type="text" class="form-control" id="txtTravelEndTo" placeholder="DD MMM  YYYY" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        <asp:Label runat="server">Dump Date From</asp:Label>
                        <input type="text" class="form-control" id="txtDumpDateFrom" placeholder="DD MMM  YYYY" />
                    </div>
                    <div class="col-md-2">
                        <asp:Label runat="server">Dump Date To</asp:Label>
                        <input type="text" class="form-control" id="txtDumpDateTo" placeholder="DD MMM  YYYY" />
                    </div>
                    <div class="col-md-3">
                        <asp:Label runat="server">Exp Date From</asp:Label>
                        <input type="text" class="form-control" id="txtExpDateFrom" placeholder="DD MMM  YYYY" />
                    </div>
                    <div class="col-md-3">
                        <asp:Label runat="server">Exp Date To</asp:Label>
                        <input type="text" class="form-control" id="txtExpDateTo" placeholder="DD MMM  YYYY" />
                    </div>
                    <div class="col-md-2">
                        <asp:Label runat="server"></asp:Label>
                        <br />
                        <button type="button" class="btn btn-danger" onclick="SearchOffer();">Search</button>
                    </div>
                </div>
            </div>
        </div>
        <div style="height: 15px;"></div>
        <div class="panel panel-default">
            <div class="panel-heading">Dump Flight Fares Details</div>
            <div class="panel-body" style="line-height: 34px;">
                <div class="row">
                    <div class="col-md-12">
                        <div id="divAllOfferDetails"></div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="container">
    </div>

    <div id="fadebackground">
    </div>
    <div align="center" id="popProgressBar" style="display: none;" class="popup-product">
        <table width="100%" align="center" height="100%" bgcolor="#ffffff">
            <tr>
                <td class="popup-header">Please wait while we process your request...
                </td>
            </tr>
            <tr>
                <td align="center" valign="middle">
                    <img src="../Images/Wait.gif" width="98px" height="100px" id="ImageProgressbar" />
                </td>
            </tr>
            <tr>
                <td align="center" height="40" style="background-color: #ffffff; color: #B9B9B9; vertical-align: middle; text-align: center; font-size: 18px; font-family: Verdana;"></td>
            </tr>
        </table>
    </div>
    <div align="center" id="divEditDetails" style="display: none;" class="popup-product">
        <table width="100%" align="center" height="100%" bgcolor="#ffffff">
            <tr>
                <td class="popup-header">Update Flight Fares Details
                </td>
                <td class="popup-header"><span style="float: right; padding-right: 10px; cursor: pointer;" onclick="popup('divEditDetails', 30, 30);">Close(X)</span>
                </td>
            </tr>
            <tr>
                <td align="center" valign="middle" colspan="2">
                    <table>
                        <tr>
                            <td style="padding: 5px 5px 5px 5px;">BaseFare</td>
                            <td style="padding: 5px 5px 5px 5px;">
                                <input type="text" id="txtEditBaseFare" class="form-control" placeholder="BaseFare" />
                            </td>
                            <td style="padding: 5px 5px 5px 5px;">Tax</td>
                            <td style="padding: 5px 5px 5px 5px;">
                                <input type="text" id="txtEditTax" class="form-control" placeholder="Tax" />
                            </td>
                        </tr>
                        <tr>
                            <td style="padding: 5px 5px 5px 5px;">From Destination</td>
                            <td style="padding: 5px 5px 5px 5px;">
                                <input type="text" id="txtEditFrom" class="form-control" placeholder="From" />
                            </td>
                            <td style="padding: 5px 5px 5px 5px;">To Destination</td>
                            <td style="padding: 5px 5px 5px 5px;">

                                <input type="text" id="txtEditTo" class="form-control" placeholder="To" />
                            </td>
                        </tr>
                        <tr>
                            <td style="padding: 5px 5px 5px 5px;">Cabin Class</td>
                            <td style="padding: 5px 5px 5px 5px;">
                                <select id="ddlEditClassType" class="form-control">
                                    <option value="">ANY</option>
                                    <option value="ECONOMY">ECONOMY</option>
                                    <option value="BUSINESS">BUSINESS</option>
                                    <option value="FIRSTCLASS">FIRSTCLASS</option>
                                    <option value="PREMIUM">PREMIUM</option>
                                </select>
                            </td>

                            <td style="padding: 5px 5px 5px 5px;">Class</td>
                            <td style="padding: 5px 5px 5px 5px;">
                                <input type="text" class="form-control" id="txtEditClass" placeholder="Class" />
                            </td>
                        </tr>
                        <tr>
                            <td style="padding: 5px 5px 5px 5px;">Airline</td>
                            <td style="padding: 5px 5px 5px 5px;">
                                <select id="ddlEditAirline" class="form-control">
                                    <option value="">Any Airline</option>
                                    <option value="JP">Adria Airways</option>
                                    <option value="A3">Aegean Airlines</option>
                                    <option value="EI">Aer Lingus</option>
                                    <option value="WV">Aero Vip</option>
                                    <option value="SU">Aeroflot</option>
                                    <option value="2K">Aerogal Aerolineas Galapagos</option>
                                    <option value="AR">Aerolineas Argentinas</option>
                                    <option value="AM">Aeromexico</option>
                                    <option value="P5">Aerorepublica</option>
                                    <option value="5L">AeroSur</option>
                                    <option value="VV">Aerosvit Airlines</option>
                                    <option value="8U">Afriqiyah</option>
                                    <option value="ZI">Aigle Azur</option>
                                    <option value="AH">Air Algerie</option>
                                    <option value="KC">Air Astana</option>
                                    <option value="UU">Air Austral</option>
                                    <option value="AB">Air Berlin</option>
                                    <option value="2J">Air Burkina </option>
                                    <option value="AC">Air Canada</option>
                                    <option value="TX">Air Caraibes</option>
                                    <option value="CA">Air China </option>
                                    <option value="A7">Air Comet </option>
                                    <option value="UX">Air Europa </option>
                                    <option value="AF">Air France </option>
                                    <option value="AI">Air India </option>
                                    <option value="JM">Air Jamaica</option>
                                    <option value="NX">Air Macau</option>
                                    <option value="KM">Air Malta  </option>
                                    <option value="MK">Air Mauritius </option>
                                    <option value="9U">Air Moldova</option>
                                    <option value="SW">Air Namibia </option>
                                    <option value="NZ">Air New Zealand </option>
                                    <option value="JU">Air Serbia</option>
                                    <option value="HM">Air Seychelles </option>
                                    <option value="TN">Air Tahiti Nui </option>
                                    <option value="TS">Air Transat</option>
                                    <option value="BT">airBaltic</option>
                                    <option value="AS">Alaska Airlines</option>
                                    <option value="LV">Albanian Airlines</option>
                                    <option value="AZ">Alitalia</option>
                                    <option value="CT">Alitalia CityLiner</option>
                                    <option value="NH">All Nippon Airways</option>
                                    <option value="AA">American Airlines</option>
                                    <option value="W3">Arik Air</option>
                                    <option value="IZ">Arkia Israeli Airlines</option>
                                    <option value="OZ">Asiana Airlines</option>
                                    <option value="KP">ASKY Airlines</option>
                                    <option value="KK">AtlasJet Airlines</option>
                                    <option value="UI">Auric Air</option>
                                    <option value="GR">Aurigny</option>
                                    <option value="OS">Austrian Airlines</option>
                                    <option value="O6">Avianca</option>
                                    <option value="AV">Avianca</option>
                                    <option value="J2">Azerbaijan Airlines</option>
                                    <option value="UP">Bahamasair</option>
                                    <option value="PG">Bangkok Airways </option>
                                    <option value="B2">Belavia</option>
                                    <option value="BG">Biman Bangladesh</option>
                                    <option value="0B">Blue Air</option>
                                    <option value="SI">Blue Islands</option>
                                    <option value="BV">Blue Panorama Airlines</option>
                                    <option value="BD">Bmi</option>
                                    <option value="BM">BMI Regional</option>
                                    <option value="BA">British Airways</option>
                                    <option value="SN">Brussels Airline </option>
                                    <option value="FB">Bulgaria Air</option>
                                    <option value="QC">Camair-Co</option>
                                    <option value="9K">Cape Air</option>
                                    <option value="BW">Caribbean Airlines</option>
                                    <option value="CX">Cathay Pacific </option>
                                    <option value="KX">Cayman Airways</option>
                                    <option value="CI">China Airlines </option>
                                    <option value="MU">China Eastern Airlines</option>
                                    <option value="CZ">China Southern Airlines</option>
                                    <option value="QI">Cimber Sterling</option>
                                    <option value="CF">City Airline</option>
                                    <option value="WX">Cityjet</option>
                                    <option value="DE">Condor Airlines</option>
                                    <option value="CO">Continental Airlines</option>
                                    <option value="CM">Copa Airlines</option>
                                    <option value="OU">Croatia Airlines</option>
                                    <option value="CU">Cubana Airlines </option>
                                    <option value="CY">Cyprus Airways </option>
                                    <option value="OK">Czech Airlines </option>
                                    <option value="F7">Darwin Airline</option>
                                    <option value="DL">Delta Air Lines</option>
                                    <option value="T3">Eastern Airways</option>
                                    <option value="U2">easyJet</option>
                                    <option value="EZY">easyJet</option>
                                    <option value="MS">EgyptAir</option>
                                    <option value="LY">El AI</option>
                                    <option value="EK">Emirates </option>
                                    <option value="OV">Estonian Air</option>
                                    <option value="ET">Ethiopian Airlines</option>
                                    <option value="EY">Etihad Airways </option>
                                    <option value="BR">EVA Air</option>
                                    <option value="FN">Fastjet</option>
                                    <option value="FJ">Fiji Airways</option>
                                    <option value="AY">Finnair</option>
                                    <option value="W2">FlexFlight</option>
                                    <option value="Z7">FlyAfrica</option>
                                    <option value="BE">Flybe</option>
                                    <option value="FZ">FlyDubai</option>
                                    <option value="GA">Garuda</option>
                                    <option value="ST">Germania</option>
                                    <option value="4U">Germanwings</option>
                                    <option value="GF">Gulf Air </option>
                                    <option value="H1">Hahn Air</option>
                                    <option value="HR">Hahn Air </option>
                                    <option value="HU">Hainan Airlines</option>
                                    <option value="YO">Heli Air Monaco</option>
                                    <option value="2L">Helvetic Airways</option>
                                    <option value="EO">Hewa Bora Airways</option>
                                    <option value="HX">Hong Kong Airlines</option>
                                    <option value="IB">Iberia </option>
                                    <option value="FI">Icelandair</option>
                                    <option value="IC">Indian Airlines </option>
                                    <option value="IR">Iran Air  </option>
                                    <option value="6H">Israir Airlines</option>
                                    <option value="JL">Japan Airlines</option>
                                    <option value="9W">Jet Airways </option>
                                    <option value="JQ">Jetstar Airways</option>
                                    <option value="RQ">KAM Air</option>
                                    <option value="KQ">Kenya Airways </option>
                                    <option value="IT">Kingfisher Airlines</option>
                                    <option value="KL">KLM</option>
                                    <option value="KE">Korean Air </option>
                                    <option value="KU">Kuwait Airways </option>
                                    <option value="LR">LACSA</option>
                                    <option value="TM">LAM</option>
                                    <option value="LA">LAN Airlines</option>
                                    <option value="XL">Lan Ecuador</option>
                                    <option value="LI">LIAT</option>
                                    <option value="LN">Libyan Airlines</option>
                                    <option value="LO">LOT Polish Airlines</option>
                                    <option value="LH">Lufthansa</option>
                                    <option value="LG">Luxair</option>
                                    <option value="W5">Mahan Air </option>
                                    <option value="MH">Malaysia Airlines</option>
                                    <option value="MA">Malev Hungarian</option>
                                    <option value="MP">Martinair</option>
                                    <option value="7M">MAY Air</option>
                                    <option value="IG">Meridiana</option>
                                    <option value="MX">Mexicana Airlines</option>
                                    <option value="ME">Middle East Airlines</option>
                                    <option value="MJ">Mihin Lanka</option>
                                    <option value="ZB">Monarch</option>
                                    <option value="YM">Montenegro Airlines</option>
                                    <option value="CE">Nationwide Airlines</option>
                                    <option value="XY">NATL Air</option>
                                    <option value="HG">NIKI</option>
                                    <option value="NW">Northwest Airlines</option>
                                    <option value="DY">Norwegian</option>
                                    <option value="OA">Olympic Air</option>
                                    <option value="WY">Oman Air</option>
                                    <option value="P6">Pascan Aviation</option>
                                    <option value="2Z">Passaredo</option>
                                    <option value="PC">Pegasus Airlines</option>
                                    <option value="PR">Philippine Airlines</option>
                                    <option value="PK">PIA</option>
                                    <option value="QF">Qantas Airways</option>
                                    <option value="QR">Qatar Airways</option>
                                    <option value="FV">Rossiya</option>
                                    <option value="RG">Rotana Jet</option>
                                    <option value="AT">Royal Air Maroc</option>
                                    <option value="BI">Royal Brunei Airlines</option>
                                    <option value="RJ">Royal Jordanian</option>
                                    <option value="WB">RwandAir</option>
                                    <option value="RYR">RyanAir</option>
                                    <option value="FR">RyanAir</option>
                                    <option value="S7">S7 Airlines</option>
                                    <option value="F2">Safarilink</option>
                                    <option value="4Q">Safi Airways</option>
                                    <option value="S4">SATA International</option>
                                    <option value="SV">Saudi Arabian Airlines</option>
                                    <option value="SK">Scandinavian Airlines</option>
                                    <option value="DN">Senegal Airlines</option>
                                    <option value="SC">Shandong Airlines</option>
                                    <option value="FM">Shanghai Airlines</option>
                                    <option value="ZH">Shenzhen Airlines</option>
                                    <option value="SQ">Singapore Airlines</option>
                                    <option value="QS">SmartWings</option>
                                    <option value="SA">South African Airways</option>
                                    <option value="UL">SriLankan Airlines</option>
                                    <option value="SD">Sudan Airways</option>
                                    <option value="LX">SWISS</option>
                                    <option value="7E">Sylt Air</option>
                                    <option value="RB">Syrian Air</option>
                                    <option value="DT">TAAG Angola Airlines</option>
                                    <option value="TA">Taca International</option>
                                    <option value="VR">TACV Cabo Verde Airlines</option>
                                    <option value="PZ">TAM Airlines</option>
                                    <option value="JJ">TAM Airlines</option>
                                    <option value="EQ">TAME</option>
                                    <option value="TP">Tap Portugal </option>
                                    <option value="RO">Tarom</option>
                                    <option value="TG">Thai Airways</option>
                                    <option value="MT">Thomas Cook Airlines</option>
                                    <option value="BY">Thomson Airways</option>
                                    <option value="TOM">Thomson Airways</option>
                                    <option value="UN">Transaero Airlines</option>
                                    <option value="5U">Transportes Aereos Guatemaltecos</option>
                                    <option value="9N">Tropic Air</option>
                                    <option value="TU">Tunisair</option>
                                    <option value="TK">Turkish Airlines</option>
                                    <option value="T5">Turkmenistan Airlines</option>
                                    <option value="VO">Tyrolean Airways</option>
                                    <option value="PS">Ukraine International Airlines</option>
                                    <option value="UA">United Airlines</option>
                                    <option value="US">US Airways</option>
                                    <option value="HY">Uzbekistan Airways</option>
                                    <option value="VJ">VietJetAir</option>
                                    <option value="VN">Vietnam Airlines</option>
                                    <option value="VS">Virgin Atlantic</option>
                                    <option value="VA">Virgin Australia</option>
                                    <option value="DJ">Virgin Australia</option>
                                    <option value="VK">Virgin Nigeria</option>
                                    <option value="VY">Vueling</option>
                                    <option value="WS">WestJet</option>
                                    <option value="WM">Winair</option>
                                    <option value="X9">Wow Air</option>
                                    <option value="MF">Xiamen Airlines</option>
                                    <option value="IY">Yemen Airways</option>

                                </select>
                            </td>
                            <td style="padding: 5px 5px 5px 5px;">Exp Date From</td>
                            <td style="padding: 5px 5px 5px 5px;">
                                <input type="text" class="form-control" id="txtEditExpDate" placeholder="DD MMM  YYYY" />
                            </td>
                        </tr>
                        <tr>
                            <td style="padding: 5px 5px 5px 5px;">Travel Start From</td>
                            <td style="padding: 5px 5px 5px 5px;">
                                <input type="text" class="form-control" id="txtEditTravelStart" placeholder="DD MMM  YYYY" />
                            </td>
                            <td style="padding: 5px 5px 5px 5px;">Travel End From</td>
                            <td style="padding: 5px 5px 5px 5px;">
                                <input type="text" class="form-control" id="txtEditTravelEnd" placeholder="DD MMM  YYYY" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" align="right" style="padding: 5px 5px 5px 5px;">
                                <input id="hfFaredetailId" type="hidden" />
                                <button type="button" class="btn btn-danger" onclick="UpdateDetails();">Update</button>
                            </td>
                        </tr>

                    </table>
                </td>
            </tr>
        </table>
    </div>
  
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
        function SearchOffer() {
            var objJson = {
                FromDestination: $("#txtFrom").val(),
                ToDestination: $("#txtTo").val(),
                Airline: $("#ddlAirline").val(),
                FClass: $("#txtClass").val(),
                ClassType: $("#ddlClassType").val(),
                WebSite: $("#ddlWebsite").val(),
                TravelStartFrom: $("#txtTravelStartFrom").val(),
                TravelStartTo: $("#txtTravelStartTo").val(),
                TravelEndFrom: $("#txtTravelEndFrom").val(),
                TravelEndTo: $("#txtTravelEndTo").val(),
                DumpDateFrom: $("#txtDumpDateFrom").val(),
                DumpDateTo: $("#txtDumpDateTo").val(),
                ExpDateFrom: $("#txtExpDateFrom").val(),
                ExpDateTo: $("#txtExpDateTo").val()
            };
            if ($("#txtFrom").val() != "" || $("#txtTo").val() != "" || $("#ddlAirline").val() != "" || $("#txtClass").val() != "" || $("#ddlClassType").val() != "" || $("#ddlWebsite").val() != "" || $("#txtTravelStartFrom").val() != "" || $("#txtTravelStartTo").val() != "" || $("#txtTravelEndFrom").val() != "" || $("#txtTravelEndTo").val() != "" || $("#txtDumpDateFrom").val() != "" || $("#txtDumpDateTo").val() != "" || $("#txtExpDateFrom").val() != "" || $("#txtExpDateTo").val()) {
                popup('popProgressBar', 30, 30);
                $.ajax({
                    type: "POST",
                    url: "DumpFlightFaresDetails.aspx/GetDumpAirOfferFare",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify(objJson),
                    responseType: "json",
                    success: function (data) {

                        var jsdata = JSON.parse(data.d);
                        var strHtml = "<table width='100%' cellpadding='0' cellspacing='0' class='table' >" +
                                  "<tr><td class='gdvh'>SrNo</td>" +
                                  "<td class='gdvh'>From</td>" +
                                  "<td class='gdvh'>To</td>" +
                                  "<td class='gdvh'>Airline_Code</td>" +
                                  "<td class='gdvh'>Class</td>" +
                                  "<td class='gdvh'>ClassType</td>" +
                                  "<td class='gdvh'>Total</td>" +
                                  "<td class='gdvh'>TFromDate</td>" +
                                  "<td class='gdvh'>TToDate</td>" +
                                  "<td class='gdvh'>ExpDate</td>" +
                                  "<td class='gdvh'>FillDate</td>" +
                                  "<td class='gdvh'></td></tr>";
                        $.each(jsdata, function (key, value) {
                            strHtml += "<tr><td class='gdvr'>" + (key + 1) + "</td>" +
                                       "<td class='gdvr'><span title='" + value.DestfromName + "'>" + value.From + "</span></td>" +
                                       "<td class='gdvr'><span title='" + value.DesttoName + "'>" + value.To + "</span></td>" +
                                       "<td class='gdvr'><span title='" + value.Airline_Name + "'>" + value.Airline_Code + "</span></td>" +
                                       "<td class='gdvr'>" + value.Class + "</td>" +
                                       "<td class='gdvr'>" + value.ClassType + "</td>" +
                                       "<td class='gdvr'><span title='BaseFare:" + value.BaseFare + "   Tax:" + value.Tax + "'>" + value.Total + "</span></td>" +
                                       "<td class='gdvr'>" + value.Travel_DateStart + "</td>" +
                                       "<td class='gdvr'>" + value.Travel_DateEnd + "</td>" +
                                       "<td class='gdvr'>" + value.ExpOffers_Date + "</td>" +
                                       "<td class='gdvr'>" + value.FillDate + "</td>" +
                                       "<td class='gdvr'><span onclick=\"EditDetails('" + value.FaredetailId + "','" + value.From + "','" + value.To + "','" + value.Airline_Code + "','" + value.Class + "','" + value.ClassType + "','" + value.BaseFare + "','" + value.Tax + "','" + value.Travel_DateStart + "','" + value.Travel_DateEnd + "','" + value.ExpOffers_Date + "');\">Edit</span></td></tr>";

                        });
                        if (jsdata.length == 0)
                            strHtml += "<tr><td class='gdvr' colspan='12'>No any offer found for given searching criteria</td></tr>";
                        $("#divAllOfferDetails").html(strHtml + "</table>");
                        popup('popProgressBar', 30, 30);
                    },
                    error: function (data) { popup('popProgressBar', 30, 30); }

                });
            }
            else { alert("Plese choose any search criteria!!"); }
        }
        function EditDetails(FaredetailId, From, To, Airline_Code, Class, ClassType, BaseFare, Tax, Travel_DateStart, Travel_DateEnd, ExpOffers_Date) {
            $("#hfFaredetailId").val(FaredetailId);
            $("#txtEditBaseFare").val(BaseFare);
            $("#txtEditTax").val(Tax);
            $("#txtEditFrom").val(From);
            $("#txtEditTo").val(To);
            $("#ddlEditAirline option[value='" + Airline_Code + "']").attr("selected", "selected");
            $("#txtEditClass").val(Class);
            $("#ddlEditClassType option[value='" + ClassType.toUpperCase() + "']").attr("selected", "selected");
            $("#txtEditExpDate").val(ExpOffers_Date);
            $("#txtEditTravelStart").val(Travel_DateStart);
            $("#txtEditTravelEnd").val(Travel_DateEnd);
            popup('divEditDetails', 60, 60);
        }
        function UpdateDetails() {
            var objJson = {
                FaredetailId: $("#hfFaredetailId").val(),
                BaseFare: $("#txtEditBaseFare").val(),
                Tax: $("#txtEditTax").val(),
                From: $("#txtEditFrom").val(),
                To: $("#txtEditTo").val(),
                Airline_Code: $("#ddlEditAirline").val(),
                CClass: $("#txtEditClass").val(),
                ClassType: $("#ddlEditClassType").val(),
                ExpOffers_Date: $("#txtEditExpDate").val(),
                Travel_DateStart: $("#txtEditTravelStart").val(),
                Travel_DateEnd: $("#txtEditTravelEnd").val()
            };

            $.ajax({
                type: "POST",
                url: "DumpFlightFaresDetails.aspx/UpdateAirOfferFare",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify(objJson),
                responseType: "json",
                success: function (data) {
                    if (data.d == "true") { SearchOffer(); popup('divEditDetails', 60, 60); alert("Offer is update successfully!!"); }
                    else { alert("Offer is not update successfully, Try again!"); }
                },
                error: function (data) { }

            });
        }
    </script>
</asp:Content>
