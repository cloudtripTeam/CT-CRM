<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="FlightSearch.aspx.cs" Inherits="Admin_FlightSearch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../css/serach-engine.css" rel="stylesheet" />
        <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.6/jquery.min.js" type="text/javascript"></script>
<script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js"
type="text/javascript"></script>
<link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css"
rel="Stylesheet"type="text/css"/>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#ContentPlaceHolder1_txtDepDate").datepicker({
                numberOfMonths: 1,
                minDate: 1,
                maxDate:365,
                dateFormat: "dd/mm/yy",
                onSelect: function (selected) {
                    $("#ContentPlaceHolder1_txtRetDate").datepicker("option", "minDate", selected);
                    $("#ContentPlaceHolder1_txtRetDate").datepicker().trigger('focus');
                }
                


            }).datepicker("setDate", new Date());
            $("#ContentPlaceHolder1_txtRetDate").datepicker({
                numberOfMonths: 1,
                dateFormat: "dd/mm/yy",
                onSelect: function (selected) {
                    $("#ContentPlaceHolder1_txtDepDate").datepicker("option", "maxDate", selected)
                }
            }).datepicker("setDate", new Date());
        });

    </script>
    


    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <input type="hidden" name="journeyType" value="R" id="journeyType" runat="server">
     <input id="setascurrdate" type="hidden" />
    <input id="hdeprdate" type="hidden" />
 <script type="text/javascript">
     
     function checkTripType(t) {
         if (t == "R") {
             $(returndate).show();
             $(ContentPlaceHolder1_journeyType).val("R");
             $(btnRound).removeClass('btn-default').addClass('btn-danger');
             $(btnOne).removeClass('btn-danger').addClass('btn-default');

         }
         if (t == "O") {
             $(returndate).hide();
             $(ContentPlaceHolder1_journeyType).val("O");
             $(btnOne).removeClass('btn-default').addClass('btn-danger');
             $(btnRound).removeClass('btn-danger').addClass('btn-default');
         }
     }

    </script>

    

<div class="page-search full-width-search search-type-b">
    
    <div class="page-search-content">

        <!-- // tab content tickets // -->
        <div class="search-tab-content tickets-tab">
            <div class="page-search-p">
                <!-- // -->
                 <div class="col-md-12 p-0">
                <div class="btn-group mb-10">
                                    <button id="btnRound" class="btn btn-sm bgcolor border-color btn-info" onclick="checkTripType('R')" type="button">Round Trip</button>
                                    <button id="btnOne" class="btn btn-sm btn-default" onclick="checkTripType('O')" type="button">One Way</button>
                                    
                                </div>
                     </div>
                <div class="search-large-i">
                <div class="srch-tab-line">
                    <div class="srch-tab-left">
                        <label>From</label>
                        <div class="input-a">
                            <input type="text" maxlength="3"  value="" id="txtDeparture" runat="server" placeholder="Departure City" required></div>
                    </div>
                    <div class="srch-tab-right">
                        <label>To</label>
                        <div class="input-a">
                            <input type="text" maxlength="3" value="" id="txtDestination" runat="server" placeholder="Destination City" required></div>
                    </div>
                    <div class="clear"></div>
                </div>
                    </div>
                <!-- \\ -->
                <!-- // -->
                 <div class="search-large-i">
                <div class="srch-tab-line">
                    <div class="srch-tab-left">
                        <label>Departure</label>
                        <div class="input-a">
                            <input type="text" value="" class="date-inpt" onclick="showCalender(this);" id="txtDepDate" runat="server" placeholder="dd/mm/yy">
                            <span class="date-icon"></span></div>
                    </div>
                    <div class="srch-tab-right" id="returndate">
                        <label>Arrivals</label>
                        <div class="input-a">
                            <input type="text" value="" class="date-inpt1" onclick="showCalender(this);" id="txtRetDate" runat="server" placeholder="dd/mm/yy">
                            <span class="date-icon"></span></div>
                    </div>
                    <div class="clear"></div>
                </div>
                     </div>
                <!-- \\ -->

                <!-- // -->
                 <div class="search-large-i">
                <div class="srch-tab-line">
                    <div class="srch-tab-3c transformed">
                        <label>Adult</label>
                        <div class="select-wrapper">
                            <select class="custom-select" id="ddlAdult" runat="server">
                                <option value="1">1</option>
                                <option value="2">2</option>
                                <option value="3">3</option>
                                <option value="4">4</option>
                                <option value="5">5</option>
                                <option value="6">6</option>
                                <option value="7">7</option>
                                <option value="8">8</option>
                                <option value="9">9</option>
                            </select>
                        </div>
                    </div>
                    <div class="srch-tab-3c transformed">
                        <label>Child</label>
                        <div class="select-wrapper">
                            <select class="custom-select" id="ddlChild" runat="server">
                                <option value="0">--</option>
                                <option value="1">1</option>
                                <option value="2">2</option>
                                <option value="3">3</option>
                                <option value="4">4</option>
                                <option value="5">5</option>
                                <option value="6">6</option>
                               
                            </select>
                        </div>
                    </div>
                    <div class="srch-tab-3c transformed">
                        <label>Infant</label>
                        <div class="select-wrapper">
                            <select class="custom-select" id="ddlInfant" runat="server">
                                <option value="0">--</option>
                                <option value="1">1</option>
                                <option value="2">2</option>
                                <option value="3">3</option>
                                <option value="4">4</option>
                              
                            </select>
                        </div>
                    </div>
                    <div class="clear"></div>
                </div>
                     </div>
                <!-- \\ -->

                <div class="clearfix"></div>
                <!-- // advanced // -->
              
                    <!-- // -->
                    <div class="srch-tab-line no-margin-bottom mt-10">
                        <div class="col-md-4 p-0">
                            <label>Class</label>
                            <div class="select-wrapper">
                                <select class="custom-select" id="ddlClass" runat="server">
                                    <option value="Y">Economy</option>
                                    <option value="W">Premium Economy</option>
                                    <option value="C">Business</option>
                                    <option value="F">First Class</option>
                                </select>
                            </div>
                        </div>
                        <div class="col-md-4 p-0">
                            <label>Airline</label>
                            <div class="select-wrapper">
                                <select class="custom-select" id="ddlAirline" runat="server">
                                    <option selected="selected" value="ALL">Any Airline</option>
                                    <option value="JP">Adria Airways</option>
                                    <option value="EI">Aerlingus</option>
                                    <option value="SU">Aeroflot</option>
                                    <option value="AR">Aerolineas Argen </option>
                                    <option value="AM">Aeromexico</option>
                                    <option value="5L">Aerosur </option>
                                    <option value="VV">Aerosvit</option>
                                    <option value="8U">Afriqiyah Airline</option>
                                    <option value="AH">Air Algerie</option>
                                    <option value="KC">Air Astana</option>
                                    <option value="BT">Air Baltic</option>
                                    <option value="AB">Air Berlin</option>
                                    <option value="2J">Air Burkina </option>
                                    <option value="AC">Air Canada</option>
                                    <option value="CA">Air China </option>
                                    <option value="A7">Air Comet </option>
                                    <option value="UX">Air Europa </option>
                                    <option value="AF">Air France </option>
                                    <option value="AI">Air India </option>
                                    <option value="JM">Air Jamaica</option>
                                    <option value="KM">Air Malta </option>
                                    <option value="MK">Air Mauritius </option>
                                    <option value="9U">Air Maldova</option>
                                    <option value="SW">Air Namibia </option>
                                    <option value="NZ">Air New Zealand </option>
                                    <option value="TP">TAP Air Portugal </option>
                                    <option value="HM">Air Seychelles </option>
                                    <option value="VT">Air Tahiti </option>
                                    <option value="TN">Air Tahiti Nui </option>
                                    <option value="TS">Air Transat</option>
                                    <option value="UM">Air Zimbabwe</option>
                                    <option value="AS">Alaska Air</option>
                                    <option value="LV">Albanian Air</option>
                                    <option value="AZ">Alitalia </option>
                                    <option value="NH">All Nippon </option>
                                    <option value="AA">American Air</option>
                                    <option value="OZ">Asiana Airlines</option>
                                    <option value="GR">Aurigny</option>
                                    <option value="OS">Austrian Airline </option>
                                    <option value="AV">Avianca </option>
                                    <option value="J2">Azerbaijan</option>
                                    <option value="UP">Bahamasair</option>
                                    <option value="PG">Bangkok Airways </option>
                                    <option value="BG">Biman Bangla</option>
                                    <option value="BD">Bmi British </option>
                                    <option value="BA">British Air</option>
                                    <option value="SN">Brussels Airline </option>
                                    <option value="FB">Bulgaria Air</option>
                                    <option value="BW">Caribbean Air</option>
                                    <option value="CX">Cathay Pacific </option>
                                    <option value="CI">China Airlines </option>
                                    <option value="MU">China Eastern </option>
                                    <option value="CZ">China Southern </option>
                                    <option value="QI">Cimber Sterli</option>
                                    <option value="CF">City Airline</option>
                                    <option value="DE">Condor </option>
                                    <option value="CO">Continental </option>
                                    <option value="OU">Croatia Air </option>
                                    <option value="CU">Cubana Airlines </option>
                                    <option value="CY">Cyprus Airways </option>
                                    <option value="OK">Czech Airlines </option>
                                    <option value="D3">Daallo</option>
                                    <option value="DL">Delta </option>
                                    <option value="T3">Eastern airways</option>
                                    <option value="MS">Egyptair </option>
                                    <option value="EK">Emirates Air </option>
                                    <option value="OV">Estonian Air</option>
                                    <option value="ET">Ethiopian Air </option>
                                    <option value="EY">Etihad Airways </option>
                                    <option value="BR">EVA Airways </option>
                                    <option value="AY">Finnair </option>
                                    <option value="BE">Flybe </option>
                                    <option value="GA">Garuda </option>
                                    <option value="GF">Gulf Air </option>
                                    <option value="HR">Hahn Air </option>
                                    <option value="HU">Hainan Airlin</option>
                                    <option value="YO">Heli-Air Monaco</option>
                                    <option value="EO">Hewa Bora Air </option>
                                    <option value="IB">Iberia </option>
                                    <option value="FI">Iceland Air </option>
                                    <option value="IC">Indian Air </option>
                                    <option value="IR">Iran Air </option>
                                    <option value="6H">Israir</option>
                                    <option value="JL">Japan Airline</option>
                                    <option value="JU">Jat Airways</option>
                                    <option value="9W">Jet Airways </option>
                                    <option value="KQ">Kenya Airways </option>
                                    <option value="KL">KLM </option>
                                    <option value="KE">Korean Air </option>
                                    <option value="KU">Kuwait Airways </option>
                                    <option value="LA">Lan Airlines </option>
                                    <option value="LO">Lot-Polish </option>
                                    <option value="LH">Lufthansa </option>
                                    <option value="CC">Macair</option>
                                    <option value="W5">Mahan Air </option>
                                    <option value="MA">Malev </option>
                                    <option value="MH">Malaysia </option>
                                    <option value="MP">Martinair </option>
                                    <option value="IG">Meridiana </option>
                                    <option value="MX">Mexicana </option>
                                    <option value="ME">Middle East</option>
                                    <option value="YM">Montenegro</option>
                                    <option value="CE">Nationwide Air </option>
                                    <option value="NW">Northwest </option>
                                    <option value="OA">Olympic </option>
                                    <option value="WY">Oman Aviation </option>
                                    <option value="PR">Philippine </option>
                                    <option value="QF">Qantas Airways </option>
                                    <option value="QR">Qatar Airways </option>
                                    <option value="AT">Royal Air Maroc </option>
                                    <option value="BI">Royal Brunei </option>
                                    <option value="RJ">Royal Jordanian </option>
                                    <option value="FV">Rossiya </option>
                                    <option value="SK">Sas </option>
                                    <option value="S4">Sata Intl </option>
                                    <option value="SV">Saudi Arabian </option>
                                    <option value="S7">Siberia Air</option>
                                    <option value="SA">South African </option>
                                    <option value="UL">SriLankan </option>
                                    <option value="SD">Sudan </option>
                                    <option value="LX">Swiss </option>
                                    <option value="RB">Syrian Arab</option>
                                    <option value="DT">Taag-Angola </option>
                                    <option value="VR">Tacv Carbo Verdes </option>
                                    <option value="JJ">Tam Linhas Aerea </option>
                                    <option value="TG">Thai Intl </option>
                                    <option value="UN">Transaero</option>
                                    <option value="TU">Tunis Air</option>
                                    <option value="TK">Turkish Airlines </option>
                                    <option value="PS">Ukraine Intl </option>
                                    <option value="UA">United</option>
                                    <option value="US">US Airways </option>
                                    <option value="HY">Uzbekistan </option>
                                    <option value="VN">Vietnam Air </option>
                                    <option value="VS">Virgin Atlantic </option>
                                    <option value="VK">Virgin Nigeria </option>
                                    <option value="WM">Windward Island</option>
                                    <option value="IY">Yemenia Yemen </option>
                                </select>
                            </div>
                        </div>
                         <div class="col-md-4 p-0">
                    <div class="srch-tab-line no-margin-bottom">

                        <div class="radio-info checkbox-inline mt-30">
                            <input type="checkbox" id="chkDirect" runat="server">
                            <label>Non Stop Flights Only </label>
                        </div>
                    </div>
                     </div>
                        <div class="clearfix"></div>
                    </div>
                
          
                <!-- \\ advanced \\ -->
            </div>
            <footer class="search-footer">
                   <asp:LinkButton ID="lnkbtnSearch" runat="server" CssClass="srch-btn pull-right"  OnClick="lnkbtnSearch_Click"  OnClientClick="return validate()">Search</asp:LinkButton>
               
             
                <div class="clearfix"></div>
            </footer>
        </div>
        <!-- // tab content // -->
    </div>
</div>
<script type="text/javascript">
    function validate() {
        var Ret = document.getElementById('<%=txtDeparture.ClientID %>').value;
        if (Ret == "") {
            alert("Enter Departure Destination !!!");
            document.getElementById('<%=txtDeparture.ClientID %>').focus();
            return false;
        }
        var Ret = document.getElementById('<%=txtDestination.ClientID %>').value;
        if (Ret == "") {
            alert("Enter Return Destination !!!");
            document.getElementById('<%=txtDestination.ClientID %>').focus();
            return false;
        }
        var DepD = document.getElementById('<%=txtDepDate.ClientID %>').value;
        if (DepD == "") {
            alert("Enter Departure Date !!!");
            document.getElementById('<%=txtDepDate.ClientID %>').focus();
            return false;
        }
        if (document.getElementById('ContentPlaceHolder1_journeyType').value == "R") {
            var RetD = document.getElementById('<%=txtRetDate.ClientID %>').value;
            if (RetD == "") {
                alert("Enter Return Date !!!");
                document.getElementById('<%=txtRetDate.ClientID %>').focus();
                return false;
            }
        }
    }
</script>


</asp:Content>

