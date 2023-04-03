

function getAjax() {
    var xmlhttp = null;
    if (window.XMLHttpRequest) { xmlhttp = new XMLHttpRequest(); }
    // code for IE
    else if (window.ActiveXObject) { xmlhttp = new ActiveXObject("Microsoft.XMLHTTP"); }
    return xmlhttp;
}



//   =====================Flight Filter Starting ================================

//=========================================  19-11-2015 ===========================================

//function return Flt div List and Total number of Flt
function FlightDivFinder() {
    //
    //Global Variable Declaration Start
    var FlightDivList = new Array(150);
    var FlightListCount = 0;
    // 1- Globle Variable Declatarion End 
    var table1 = document.getElementById("table1");
    var cells1 = table1.getElementsByTagName("div");
    //Loop Fill All Flight Div In Array one-by-one
    for (var i = 0; i < cells1.length; i++) {
        var selectedID = cells1[i].getAttribute("id");
        if (selectedID != null) {
            var lenghtCount = selectedID.length;
            if (lenghtCount == 12 || lenghtCount == 13) {
                var str = selectedID;
                var n = str.search("DivFlight");
                if (n >= 0) {
                    FlightDivList[FlightListCount] = str;
                    FlightListCount++;
                }
            }
        }
    }
    return { x: FlightDivList, y: FlightListCount };
}

//Function Get List
function GetBoxListMasterPre(idPre, FlightDivList, FlightDivCount) {

    var MasterList = new Array(150);
    for (var Airport = 0; Airport < FlightDivCount; Airport++) {
        MasterList[Airport] = document.getElementById(idPre + FlightDivList[Airport]).innerHTML;

    }
    return MasterList;
}

//Function Delete Duplicate
function RemoveDuplicateMaster(masterList, count) {
    var Temp = "";
    var result = false;
    var FinalCount = 0;
    var TempList = new Array(count);
    for (var i = 0; i < count; i++) {

        result = false;
        for (var j = 0; j < FinalCount; j++) {
            if (masterList[i] == TempList[j]) {
                result = true;
                break;
            }
        }
        if (result == false) {
            TempList[FinalCount] = masterList[i];
            FinalCount++;
        }
    }
    return { a: TempList, b: FinalCount };
}



function RemoveDuplicateMasterNew(masterList, count) {
    var Temp = "";
    var result = false;
    var FinalCount = 0;
    var TempList = new Array(count);

    for (var i = 0; i < count; i++) {

        result = false;
        for (var j = 0; j < FinalCount; j++) {
            if (masterList[i] == TempList[j]) {
                result = true;
                break;
            }
        }
        if (result == false) {
            TempList[FinalCount] = masterList[i];
            FinalCount++;
        }
    }
    return { a: TempList, b: FinalCount };
}


function SortList(DepartAirportList, FlightListCount) {
    var temp = "";
    var F1 = 0;
    var F2 = 0;
    for (var i = 0; i < FlightListCount; i++) {
        for (var j = 0; j < FlightListCount - 1; j++) {
            F1 = DepartAirportList[j].charCodeAt(0);
            F2 = DepartAirportList[j + 1].charCodeAt(0);
            if (F1 > F2) {
                temp = DepartAirportList[j];
                DepartAirportList[j] = DepartAirportList[j + 1];
                DepartAirportList[j + 1] = temp;
            }
        }
    }
    return DepartAirportList;
}




function CreateControlBox(ContainerId, ContainsId) {

    //
    var ResultFlightDivFinder = FlightDivFinder();

    var FlightDivList = ResultFlightDivFinder.x;
    var FlightListCount = ResultFlightDivFinder.y;

    var DepartAirportList = new Array(150);



    DepartAirportList = GetBoxListMasterPre(ContainsId, FlightDivList, FlightListCount);

    var ResultRemoveDuplicate = RemoveDuplicateMaster(DepartAirportList, FlightListCount);

    var ResultCount = ResultRemoveDuplicate.b;
    var DistinctList = new Array(ResultCount);
    DistinctList = ResultRemoveDuplicate.a;

    ///444

    var CountBoxList = new Array(ResultCount);
    CountBoxList = GetCountBoxes(ContainsId, DistinctList, FlightDivList, ResultCount, FlightListCount);



    if (ContainerId == "AirlineName") {

        DistinctList = SortList(DistinctList, ResultCount);
    }


    var TotalMoreOneStop = 0;
    if (ContainerId == "AirLineStops") {

        for (var i = 0; i < ResultCount; i++) {
            if (DistinctList[i] != 0) {
                TotalMoreOneStop = TotalMoreOneStop + parseInt(CountBoxList[i]);
            }
        }
    }





    var Controls = "<form name=\"Form" + ContainerId + "\">";
    var MinMaxCountTemp = 0;
    var flag = 0;
    var nonstop = '';
    var stope = '';
    var stope1 = '';
    for (var i = 0; i < ResultCount; i++) {
        MinMaxCountTemp = 0
        if (ContainerId == "AirLineStops") {
            
            if (DistinctList[i] == 0) {

                nonstop = "<div class=\"i-check\"><input type=\"checkbox\" checked=\"checked\" class=\"checkboxs\" value=\"" + DistinctList[i] + "\" name=\"Box" + ContainsId + "\" onchange=\"MasterFilter()\" ><span class=\"checkbox\"> Non Stop" + "(" + CountBoxList[i] + ")" + "</span></div>";
            }
            else if (DistinctList[i] == 1) {

                stope = stope + "<div class=\"i-check\"><input type=\"checkbox\" checked=\"checked\" class=\"checkboxs\" value=\"" + DistinctList[i] + "\" name=\"Box" + ContainsId + "\" onchange=\"MasterFilter()\" ><span class=\"checkbox\">1 Stops" + "(" + CountBoxList[i] + ")" + "</span></div>";
            }
             else if (DistinctList[i] == 2) {

                 stope = stope + "<div class=\"i-check\"><input type=\"checkbox\" checked=\"checked\" class=\"checkboxs\" value=\"" + DistinctList[i] + "\" name=\"Box" + ContainsId + "\" onchange=\"MasterFilter()\" ><span class=\"checkbox\">2 Stops" + "(" + CountBoxList[i] + ")" + "</span></div>";
           }
           else if (DistinctList[i] == 3) {

               stope = stope + "<div class=\"i-check\"><input type=\"checkbox\" checked=\"checked\" class=\"checkboxs\" value=\"" + DistinctList[i] + "\" name=\"Box" + ContainsId + "\" onchange=\"MasterFilter()\" ><span class=\"checkbox\">3 Stops" + "(" + CountBoxList[i] + ")" + "</span></div>";
           }
           else if (DistinctList[i] == 4) {

               stope = stope + "<div class=\"i-check\"><input type=\"checkbox\" checked=\"checked\" class=\"checkboxs\" value=\"" + DistinctList[i] + "\" name=\"Box" + ContainsId + "\" onchange=\"MasterFilter()\" ><span class=\"checkbox\">4 Stops" + "(" + CountBoxList[i] + ")" + "</span></div>";
           }
            
            
           else if ((DistinctList[i] == 1 || DistinctList[i] == 2) && flag == 0 && 1==2) {
                flag = 1;
                stope = stope + "<div class=\"i-check\"><input type=\"checkbox\" checked=\"checked\" value=\"" + DistinctList[i] + "\" name=\"Box" + ContainsId + "\" onchange=\"MasterFilter()\" ><span class=\"checkbox\">with Stops" + "(" + CountBoxList[i] + ")" + "</span></div>";
           }
            else {
                
               stope = stope + "<div class=\"i-check\" style=\"display:none;\"><input type=\"checkbox\" checked=\"checked\" value=\"" + DistinctList[i] + "\" name=\"Box" + ContainsId + "\" onchange=\"MasterFilter()\" class=\"checkboxs\"><span class=\"checkbox\">" + DistinctList[i] + "+ Stop" + "(" + CountBoxList[i] + ")" + "</span></div>";
            }

        }
        else if (ContainerId == "AirpCode") {
            Controls = Controls + "<div class=\"i-check\"><input type=\"checkbox\" checked=\"checked\" value=\"" + DistinctList[i] + "\" name=\"Box" + ContainsId + "\" onchange=\"MasterFilter()\" class=\"checkboxs\"><span class=\"checkbox\">" + DistinctList[i] + "(" + CountBoxList[i] + ")" + "</span></div>";
        }
        else if (ContainerId == "ReturnAirpCode") {


            Controls = Controls + "<div class=\"i-check\"><input type=\"checkbox\" checked=\"checked\" value=\"" + DistinctList[i] + "\" name=\"Box" + ContainsId + "\" onchange=\"MasterFilter()\" class=\"checkboxs\"><span class=\"checkbox\">" + DistinctList[i] + "(" + CountBoxList[i] + ")" + "</span></div>";
        }
        else if (ContainerId == "AirlineName") {

            
            MinMaxCountTemp = GetMinCost(FlightDivList, FlightListCount, ContainsId, "GrandTotal", DistinctList[i]);
            Controls = Controls + "<div class=\"i-check\" onmouseover=\"ShowOnlyLink('" + DistinctList[i] + "')\" onmouseout=\"HideOnlyLink('" + DistinctList[i] + "')\"><input type=\"checkbox\" class=\"checkboxs\" checked=\"checked\" value=\"" + DistinctList[i] + "\" name=\"Box" + ContainsId + "\" onchange=\"MasterFilter()\" ><span class=\"checkbox\">" + DistinctList[i] + "</span><span style=\"width:30px;float:right;cursor:pointer;position: relative;top: -16px;\"><b>" + "£" + MinMaxCountTemp + "</b></span><span href='#aTop' id=\"divOnly" + DistinctList[i] + "\" style=\"width:30px;float:right;color:blue;cursor:pointer;display:none; top: -16px;position: relative;\" onClick=\"SelectOnlyAirline('" + DistinctList[i] + "')\">only</span></div>";
        }
    }
    Controls = Controls + nonstop;
    Controls = Controls + stope;
    Controls = Controls + stope1;


    Controls = Controls + "</form>";

    document.getElementById(ContainerId).innerHTML = Controls;

}
function ShowOnlyLink(inputId) {
    //

    inputId = "divOnly" + inputId;
    document.getElementById(inputId).style.display = "block";
}

function HideOnlyLink(inputId) {
    //
    inputId = "divOnly" + inputId;
    document.getElementById(inputId).style.display = "none";
}
function SelectOnlyAirline(inputId) {
    //
    var str = inputId;

    var res = str.replace("_", " ");
    res = res.replace("_", " ");
    res = res.replace("_", " ");
    
    AirLineNameHandler(res);
}

function GetCountBoxes(idPre, DistinctArray, FullArray, ResultCount, FlightListCount) {
   
    var CountArray = new Array(ResultCount);

    var counter = 0;

    for (var i = 0; i < ResultCount; i++) {
        counter = 0;
        for (var j = 0; j < FlightListCount; j++) {
            if (document.getElementById(idPre + FullArray[j]).innerHTML == DistinctArray[i]) {
                counter++;

            }
        }
        CountArray[i] = counter;
    }

    return CountArray;

}



//Function Create Airport box List

//Function Get Min Cost According To particular Area
function GetMinCost(AllDivList, TotalCount, AreaNamePreFix, CostPreFix, selectedAreaName) {
    //
    var FullCostList = new Array(TotalCount);
    var min = 50000;
    var TotalSelectedItem = 0;
    var CurrentRequerd = "";
    for (var i = 0; i < TotalCount; i++) {
        CurrentRequerd = document.getElementById(AreaNamePreFix + AllDivList[i]).innerHTML;
        if (selectedAreaName == CurrentRequerd) {
            FullCostList[TotalSelectedItem] = parseInt(document.getElementById(CostPreFix + AllDivList[i]).innerHTML);
            TotalSelectedItem++;
        }
    }
    //find min
    for (var j = 0; j < TotalSelectedItem; j++) {
        if (min > FullCostList[j]) {
            min = FullCostList[j];
        }
    }
    return min;
}



function GetMinCostStops(AllDivList, TotalCount, AreaNamePreFix, CostPreFix, selectedAreaName, StopCount) {
    var FullCostList = new Array(TotalCount);
    var min = 50000;
    var TotalSelectedItem = 0;
    var CurrentRequerd = "";
    var stops0 = 0;
    for (var i = 0; i < TotalCount; i++) {
        CurrentRequerd = document.getElementById(AreaNamePreFix + AllDivList[i]).innerHTML;

        try {
            stops0 = document.getElementById("AirLineStops" + AllDivList[i]).innerHTML;
            if (selectedAreaName == CurrentRequerd && stops0 == StopCount) {
                FullCostList[TotalSelectedItem] = parseInt(document.getElementById(CostPreFix + AllDivList[i]).innerHTML);
                TotalSelectedItem++;
            }
        }
        catch (ex) {

            alert('error');
        }


    }

    //find min

    for (var j = 0; j < TotalSelectedItem; j++) {
        if (FullCostList[j] != null || FullCostList[j] != '') {
            if (min > FullCostList[j]) {
                min = FullCostList[j];
            }
        }
    }
    if (min == 50000) {
        min = "";
    }

    return min;
}



function CreateAirPortBox() {

    //
    CreateControlBox("AirpCode", "AirpCode1");

    CreateControlBox("AirlineName", "AirLineName1");
    
    CreateControlBox("AirLineStops", "AirLineStops");



    var Way = document.getElementById("ContentPlaceHolder1_hfHotelWay").value;


    //144GetNewLogoBox();

    if (Way == "R") {


        try {
            CreateControlBox("ReturnAirpCode", "ReturnAirpCode1");

            document.getElementById("divReturnAirPortCodeList").style.display = "block";
            document.getElementById("divReturnTiming").style.display = "block";

        }
        catch (e) {

        }
    }
}


//===========================FILTER WORKING START =====================================
//================================MASTER Filtering==========================================

function MasterFilter() {
    
    //Global Variable Declaration Start
    var ResultFilterdFlight = new Array(150);
    // 1- Globle Variable Declatarion End 
    var ResultFlightDivFinder = FlightDivFinder();
    var FlightDivList = ResultFlightDivFinder.x;
    var FlightListCount = ResultFlightDivFinder.y;
    
    //Initialized Result Array With All true
    for (var r = 0; r < FlightListCount; r++) {
        ResultFilterdFlight[r] = true;
    }

    //For check box Click


    ResultFilterdFlight = CheckBooxesFilter(FlightDivList, FlightListCount, ResultFilterdFlight, "BoxAirLineName1", "AirLineName1");

    ResultFilterdFlight = CheckBooxesFilter(FlightDivList, FlightListCount, ResultFilterdFlight, "BoxAirpCode1", "AirpCode1");

    ResultFilterdFlight = CheckBooxesFilterStopSpecial(FlightDivList, FlightListCount, ResultFilterdFlight, "BoxAirLineStops", "AirLineStops");

    //  start work here
    
    //ResultFilterdFlight = MasterCostFilter(FlightDivList, FlightListCount, ResultFilterdFlight, "divSelectedStart", "divSelectedEnd", "GrandTotal")

    //ResultFilterdFlight = MasterTimeFilter(FlightDivList, FlightListCount, ResultFilterdFlight, "divSelectedStartDepart", "divSelectedEndDepart", "DepartureTime1");

    //  end work here

    var Way = document.getElementById("ContentPlaceHolder1_hfHotelWay").value;

    if (Way == "R") {
        try {

            ResultFilterdFlight = CheckBooxesFilter(FlightDivList, FlightListCount, ResultFilterdFlight, "BoxReturnAirpCode1", "ReturnAirpCode1");

            //  start work here
            //ResultFilterdFlight = MasterTimeFilter(FlightDivList, FlightListCount, ResultFilterdFlight, "divSelectedStartReturn", "divSelectedEndReturn", "ReturnTime1");
            //ResultFilterdFlight = MasterTimeFilter(FlightDivList, FlightListCount, ResultFilterdFlight, "divSelectedStartReturnDuration", "divSelectedEndReturnDuration", "ActualInTime");
            
        }
        catch (e) {
            alert('error on filter');

        }
    }
       
    ApplyFilteringResult(FlightListCount, FlightDivList, ResultFilterdFlight);

    
    //Final Result Output to Client
    
}

function ApplyFilteringResult(FlightListCount, FlightDivList, ResultFilterdFlight) {
    var SelectedResult = true;
    for (var f = 0; f < FlightListCount; f++) {
        SelectedResult = ResultFilterdFlight[f];
        if (SelectedResult == true) {
            //   document.getElementById(FlightDivList[f]).className = "visible";
            document.getElementById(FlightDivList[f]).style.display = "block";

        }
        else {
            // document.getElementById(FlightDivList[f]).className = "hidden";
            document.getElementById(FlightDivList[f]).style.display = "none";

        }
    }
}

//================================ END MASTER FILTERING==========================================



function CheckBooxesFilterStopSpecial(ProductList, ProductCount, ResultList, BoxName, PreFix) {
    //
    var TempFirst = "F";
    var p = 0;
    var SelectedAirLine = new Array(150);
    var AirLineBoxList = document.getElementsByName(BoxName); //"BoxAirLineName1"
    
    for (iSelectedAir = 0; iSelectedAir < AirLineBoxList.length; iSelectedAir++) {
        if (AirLineBoxList[iSelectedAir].checked) {
            SelectedAirLine[p] = AirLineBoxList[iSelectedAir].value;

            if (AirLineBoxList.length == 3 && iSelectedAir == 1) {
                p++;
                SelectedAirLine[p] = 2;
            }           
            p++;
        }
    }

    var fltPointer = "";
    var AirLineName = "";
    FlightProvideWithIndex = "";
    var LogoIdTemp = "";
    var MatchResult = false;
    for (var Logo = 0; Logo < ProductCount; Logo++) {
        LogoIdTemp = PreFix + ProductList[Logo];  //"DivFltLogoName"
        AirLineName = document.getElementById(LogoIdTemp).innerHTML;
        MatchResult = false;
        for (var matchLogo = 0; matchLogo < p; matchLogo++) {
            if (AirLineName == SelectedAirLine[matchLogo]) {
                MatchResult = true;
            }
        }
        if (MatchResult == false) {
            ResultList[Logo] = false;
        }
    }

    return ResultList;
}

//function Filter Stops
function CheckBooxesFilter(ProductList, ProductCount, ResultList, BoxName, PreFix) {

    var p = 0;
    var SelectedAirLine = new Array(150);
    var AirLineBoxList = document.getElementsByName(BoxName); //"BoxAirLineName1"
    for (iSelectedAir = 0; iSelectedAir < AirLineBoxList.length; iSelectedAir++) {
        if (AirLineBoxList[iSelectedAir].checked) {
            SelectedAirLine[p] = AirLineBoxList[iSelectedAir].value;
            p++;
        }
    }

    var fltPointer = "";
    var AirLineName = "";
    FlightProvideWithIndex = "";
    var LogoIdTemp = "";
    var MatchResult = false;
    for (var Logo = 0; Logo < ProductCount; Logo++) {
        LogoIdTemp = PreFix + ProductList[Logo];  //"DivFltLogoName"
        AirLineName = document.getElementById(LogoIdTemp).innerHTML;
        MatchResult = false;
        for (var matchLogo = 0; matchLogo < p; matchLogo++) {
            if (AirLineName == SelectedAirLine[matchLogo]) {
                MatchResult = true;
            }
        }
        if (MatchResult == false) {
            ResultList[Logo] = false;
        }
    }

    return ResultList;
}

//Cost Filter Master
function MasterCostFilter(ProductList, TotalProductCount, ResultList, MinCostDiv, MaxCostDiv, PreFix) {

    var MinCost = parseInt(document.getElementById(MinCostDiv).innerHTML);
    var MaxCost = parseInt(document.getElementById(MaxCostDiv).innerHTML);
    var CostDivId = "";
    var FltCost = 0;
    for (var jCost = 0; jCost < TotalProductCount; jCost++) {
        CostDivId = PreFix + ProductList[jCost];
        FltCost = parseInt(document.getElementById(CostDivId).innerHTML);
        if (MinCost <= FltCost && FltCost <= MaxCost) {

        }
        else {

            ResultList[jCost] = false;
        }
    }
    return ResultList;
}

// Time Filter Master
function MasterTimeFilter(ProductList, TotalProductCount, ResultList, MinTimeDiv, MaxTimeDiv, PreFix) {

    var OutFilterStart = ConvertInMinuts1(document.getElementById(MinTimeDiv).innerHTML);
    var OutFilterEnd = ConvertInMinuts1(document.getElementById(MaxTimeDiv).innerHTML);
    var OutTimingDivId = "";
    var OutTiming = "";
    var ConvertedTiming = 0;

    for (var jj = 0; jj < TotalProductCount; jj++) {
        OutTimingDivId = PreFix + ProductList[jj];
        OutTiming = document.getElementById(OutTimingDivId).innerHTML;
        ConvertedTiming = ConvertInMinuts(OutTiming);
        if (OutFilterStart <= ConvertedTiming && ConvertedTiming <= OutFilterEnd) {

        }
        else {
            ResultList[jj] = false;
        }
    }

    return ResultList;

}



function ConvertInMinuts(time) {
    time = time.trim();
    var TimeLength = time.length;
    if (TimeLength < 5) {
        //alert(TimeLength);
        // alert(time);
        time = "0" + time;
    }

    var H = time.substring(0, 2);
    var M = time.substring(3);

    var Hint = parseInt(H);
    var Mint = parseInt(M);
    var HMinuhts = Hint * 60;
    var Total = HMinuhts + Mint;
    return Total;
}


function ConvertInMinuts1(time) {
    time = time.trim();
    var H = time.substring(0, 2);
    var M = time.substring(3);

    var Hint = parseInt(H);
    var Mint = parseInt(M);
    var HMinuhts = Hint * 60;
    var Total = HMinuhts + Mint;
    return Total;
}
//==============================FILTER WORKING END=====================================================



//formated
function SetOutStartTiming(time) {

    var HMaster = 60;
    var TotalHoure = parseInt(time / HMaster);
    var TotalMinuts = time % HMaster

    var h = "";
    var m = "";
    if (TotalHoure <= 9) {
        h = "0" + TotalHoure.toString();
    }
    else {
        h = TotalHoure.toString();
    }
    if (TotalMinuts <= 9) {
        m = "0" + TotalMinuts.toString();
    }
    else {
        m = TotalMinuts.toString();
    }
    var fullStartText = h + ":" + m;
    return fullStartText;

}



function selectAllAirLine() {

    var AirLineBoxList = document.getElementsByName("BoxAirLineName1");
    for (iSelectedAir = 0; iSelectedAir < AirLineBoxList.length; iSelectedAir++) {
        AirLineBoxList[iSelectedAir].checked = true;
    }
    //call For Apply Filtering
    // MasterFiltering();
    MasterFilter();

}
function uncheckAllAirLine() {
    var AirLineBoxList = document.getElementsByName("BoxAirLineName1");
    for (iSelectedAir = 0; iSelectedAir < AirLineBoxList.length; iSelectedAir++) {
        AirLineBoxList[iSelectedAir].checked = false;
    }
    //call For Apply Filtering
    //MasterFiltering();
    MasterFilter();
}

function GetMaxFlightCost() {
    var FlightDivList = new Array(150);
    var table1 = document.getElementById("table1");
    var cells1 = table1.getElementsByTagName("div");
    // alert(cells1.length);

    var FlightListCount = 0;

    for (var i = 0; i < cells1.length; i++) {

        var selectedID = cells1[i].getAttribute("id");
        if (selectedID != null) {
            var lenghtCount = selectedID.length;
            if (lenghtCount == 12 || lenghtCount == 13) {
                var str = selectedID;
                var n = str.search("DivFlight");
                if (n >= 0) {
                    FlightDivList[FlightListCount] = str;
                    FlightListCount++;
                }
            }
        }
    }

    var CostId = "GrandTotal" + FlightDivList[FlightListCount - 1];
    var Cost = parseInt(document.getElementById(CostId).innerHTML);
    return Cost;

}
function getMinFlightCost() {
    var FlightDivList = new Array(150);
    var table1 = document.getElementById("table1");
    var cells1 = table1.getElementsByTagName("div");
    // alert(cells1.length);

    var FlightListCount = 0;


    for (var i = 0; i < cells1.length; i++) {

        var selectedID = cells1[i].getAttribute("id");
        if (selectedID != null) {
            var lenghtCount = selectedID.length;
            if (lenghtCount == 12 || lenghtCount == 13) {
                var str = selectedID;
                var n = str.search("DivFlight");
                if (n >= 0) {
                    FlightDivList[FlightListCount] = str;
                    FlightListCount++;
                }
            }
        }
    }

    var CostId = "GrandTotal" + FlightDivList[0];
    var Cost = parseInt(document.getElementById(CostId).innerHTML);
    return Cost;
}


function GetNewLogoBox() {

    var ResultFlightDivFinder = FlightDivFinder();
    var FlightDivList = ResultFlightDivFinder.x;
    var FlightListCount = ResultFlightDivFinder.y;



    var AirLogoCostSort = new Array(150);
    var AirLogoCost = new Array(150);
    var AirLogoCostMore = new Array(150);
    var AirLogoNames = new Array(150);

    var DepartAirportList = new Array(150);
    var ContainsId = "AirLineName2";
    DepartAirportList = GetBoxListMasterPre(ContainsId, FlightDivList, FlightListCount);
    AirLogoNames = GetBoxListMasterPre("AirLineName1", FlightDivList, FlightListCount);

    var ResultRemoveDuplicate = RemoveDuplicateMaster(DepartAirportList, FlightListCount);
    var ResultRemoveNameDuplicate = RemoveDuplicateMaster(AirLogoNames, FlightListCount);


    var ResultCount = ResultRemoveDuplicate.b;
    var DistinctList = new Array(ResultCount);
    DistinctList = ResultRemoveDuplicate.a;
    var MinMaxCountTemp = 0;

    var DistinctListName = new Array(ResultCount);
    DistinctListName = ResultRemoveNameDuplicate.a;

    var minStop = "";


    for (var i = 0; i < ResultCount; i++) {
        // MinMaxCountTemp = GetMinCost(FlightDivList, FlightListCount, ContainsId, "GrandTotal", DistinctList[i]);

        minStop = GetMinCostStops(FlightDivList, FlightListCount, ContainsId, "GrandTotal", DistinctList[i], 1);
        MinMaxCountTemp = GetMinCostStops(FlightDivList, FlightListCount, ContainsId, "GrandTotal", DistinctList[i], 0);

        if (MinMaxCountTemp == "" && minStop == "") {

            minStop = GetMinCostStops(FlightDivList, FlightListCount, ContainsId, "GrandTotal", DistinctList[i], 2);
        }

        if (MinMaxCountTemp == "" && minStop == "") {
            minStop = GetMinCostStops(FlightDivList, FlightListCount, ContainsId, "GrandTotal", DistinctList[i], 3);
        }

        // alert(MinMaxCountTemp);


        AirLogoCost[i] = MinMaxCountTemp;
        AirLogoCostMore[i] = minStop;
        //AirLogoNames[i] = document.getElementById("AirLineName1"+).innerHTML;
    }


    var temp = 0;
    var tempLogo = "";
    var F1 = 0;
    var F2 = 0;
    //    for (var i = 0; i < ResultCount; i++) {
    //        for (var j = 0; j < ResultCount - 1; j++) {
    //           
    //            if (AirLogoCost[i] > AirLogoCost[i + 1]) {
    //                temp = AirLogoCost[i];
    //                AirLogoCost[i] = AirLogoCost[i + 1];
    //                AirLogoCost[i + 1] = temp;
    //                tempLogo = DistinctList[i];
    //                DistinctList[i] = DistinctList[i + 1];
    //                DistinctList[i + 1] = tempLogo;
    //            }
    //        }
    //    }



    var ContainerId = "divLogoAirline";
    var Controls = "";  //123"<form name=\"Form" + ContainerId + "\">";
    Controls = Controls + "";
    var abc = "hhhh....";
    var pondPre = "";
    var pondPre2 = "";


    //Controls = Controls + "<div class=\"slide\" title=\"All\" onclick=\"selectAllAirLine();\" style=\" height:100px; padding:0px 0 0 0px; cursor:pointer\"><table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"border-botm\"><tr><td align=\"left\" valign=\"top\" align=\"center\">   <img src=\"images/plane1.png\" alt=\"Airline\" />  </td></tr><tr><td style=\"font-family: arial; font-size: 11px; font-weight: bold;\">All Airlines</td></tr> <tr><td align=\"center\" valign=\"top\" style=\"font-family:Arial; font-size:11px; color:#000; font-weight:bold; line-height:20px;text-decoration:underline;color:blue;\">&#160;All&#160;</td></tr> <tr> <td align=\"center\" valign=\"top\" style=\"font-family:Arial; font-size:11px; color:#000; font-weight:bold; line-height:20px;text-decoration:underline;color:blue;\">Airlines</td></tr> </table> </div>";
    Controls = Controls + "<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" class=\"row1-table\">";



    for (var i = 0; i < ResultCount; i++) {
        abc = ResultRemoveNameDuplicate[i];
        MinMaxCountTemp = AirLogoCost[i];
        if (MinMaxCountTemp != "") {
            pondPre = "&#163;";
        }
        else {
            pondPre = "";
        }

        if (AirLogoCostMore[i] != "") {
            pondPre2 = "&#163;";
        }
        else {
            pondPre2 = "";
        }        
        Controls = Controls + "<tr><td colspan=\"4\" height=\"3px\" style=\"border:none; background:#FFF; padding:0px;\"></td></tr><tr title=\"" + DistinctListName[i] + "\" onclick=\"AirLineNameHandler('" + DistinctListName[i] + "');\"><td width=\"15%\" align=\"center\" style=\"border:none; cursor:pointer;\"><img src=\"AirlineLogo/" + DistinctList[i] + "s.gif\" alt=\"Airline\" /></td><td width=\"35%\" align=\"left\" style=\"cursor:pointer;\" onclick=\"AirLineNameHandler('" + DistinctListName[i] + "');\">" + DistinctListName[i] + "</td><td width=\"27%\" align=\"center\"><span style=\"cursor:pointer;color:#A52A2A;\"><b>" + pondPre + "&#160;" + MinMaxCountTemp + "</b></span></td><td width=\"23%\" align=\"center\" style=\"border:none;cursor:pointer;\"><span style=\"color:#A52A2A;\"><b>" + pondPre2 + "&#160;" + AirLogoCostMore[i] + "</b></span></td></tr><tr><td colspan=\"4\" height=\"3px\" style=\"border:none; background:#FFF; padding:0px;\"></td></tr>";
        
    }
    Controls = Controls + "</table>";
   


    document.getElementById("divSlidersHeaderTop").innerHTML = Controls;

}

function more() {
    $("#divLogoStops").toggle();


    //document.getElementById("divViewMore").innerHTML = "Hide..";

}
function AirLineNameHandler(id) {
    selectAllAirLine();
    uncheckAllAirLineLogo(id);
    //document.getElementById("ctl00_ContentPlaceHolder1_txt1").focus();
}

function uncheckAllAirLineLogo(id) {
    //
    var AirLineBoxList = document.getElementsByName("BoxAirLineName1");
    for (iSelectedAir = 0; iSelectedAir < AirLineBoxList.length; iSelectedAir++) {
        if (AirLineBoxList[iSelectedAir].value != id) {
            AirLineBoxList[iSelectedAir].checked = false;
        }
    }
    MasterFilter();
}

//================================End Flight Filter===============================