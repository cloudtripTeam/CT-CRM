function getElementPosition(elemID) {
    var offsetTrail = document.getElementById(elemID); var offsetLeft = 0; var offsetTop = 0;
    while (offsetTrail) {
        offsetLeft += offsetTrail.offsetLeft; offsetTop += offsetTrail.offsetTop; offsetTrail = offsetTrail.offsetParent;
    }
    return { left: offsetLeft, top: offsetTop };
}
overdiv = "0";
function showFarePopup(FlightCode, TtFare, BFare, Tax, tdid) {
    
    if (navigator.family == "gecko") { pad = "0"; bord = "1 bordercolor=black"; }
    else { pad = "1"; bord = "0"; }
    desc = "<table width='180' cellspacing=0 cellpadding=0 border=1 bordercolor='#000000'><tr><td>\n"
	            + "<table cellspacing=0 cellpadding=3 border=0 width=100%>"
              + "<tr>"
                + "<td colspan='2' bgcolor='#ffffff' align='center'><img src='" + FlightCode + "'></td>"
              + "</tr>"
              + "<tr>"
                + "<td colspan='2' bgcolor='#ffffff' class='boldtextFlex'><strong>&nbsp;&nbsp;Price Details</strong></td>"
              + "</tr>"
              + "<tr>"
                + "<td height='3px' colspan='2' bgcolor='#999999'></td>"
              + "</tr>"
              + "<tr>"
                + "<td height='12px' width='50%' bgcolor='#ffffff' class='textFlex'>&nbsp;&nbsp;Total</td>"
                + "<td bgcolor='#ffffff' class='boldtextFares'>" + TtFare + "</td>"
              + "</tr>"
              + "<tr>"
                + "<td height='12px' width='50%' bgcolor='#ffffff' class='textFlex'>&nbsp;&nbsp;Base  Fare</td>"
                + "<td bgcolor='#ffffff' class='boldtextFares'>" + BFare + "</td>"
              + "</tr>"
              + "<tr>"
                + "<td height='12px' width='50%' bgcolor='#ffffff' class='textFlex'>&nbsp;&nbsp;Tax</td>"
                + "<td bgcolor='#ffffff' class='boldtextFares'>" + Tax + "</td>"
              + "</tr>"
            + "</table>"
	            + "</td></tr></table>";

    document.getElementById("farePopup").innerHTML = desc;
    var xx = getElementPosition(tdid).left;
    var yy = getElementPosition(tdid).top + 35;
    document.getElementById("farePopup").style.left = xx + 'px';
    document.getElementById("farePopup").style.top = (yy -150) + 'px';

}


var isNav = (navigator.appName.indexOf("Netscape") != -1); overdiv = "0";
function handlerMM(e) {
    x = (isNav) ? e.pageX : event.clientX + document.body.scrollLeft;
    y = (isNav) ? e.pageY : event.clientY + document.body.scrollTop;
}
if (isNav) { document.captureEvents(Event.MOUSEMOVE); }
document.onmousemove = handlerMM;
function selectedFare(id) { var td = document.getElementById(id); td.bgColor = '#ffcc99'; }

function hideFarePopup() {
    document.getElementById("farePopup").style.top = "-500px";
}


function showVisa(_id) {
  
    if (navigator.family == "gecko") { pad = "0"; bord = "1 bordercolor=black"; }
    else { pad = "1"; bord = "0"; }
    var desc = "<img src='images/Aus_visa.jpg' />";   
    document.getElementById("visaPopup").innerHTML = desc;
   
    var xx = Left(_id) - 200;
    var yy = Top(_id) + 20;
    document.getElementById("visaPopup").style.left = xx + 'px';
    document.getElementById("visaPopup").style.top = yy + 'px';
}

function hideVisa() {
    document.getElementById("visaPopup").style.top = "-500px";
}

function Left(obj) {
    var curleft = 0;
    if (obj.offsetParent) {
        while (obj.offsetParent) {
            curleft += obj.offsetLeft;
            obj = obj.offsetParent;
        }
    }
    else if (obj.x)
        curleft += obj.x;
    return curleft;
}

function Top(obj) {
    var curtop = 0;
    if (obj.offsetParent) {
        while (obj.offsetParent) {
            curtop += obj.offsetTop;
            obj = obj.offsetParent;
        }
    }
    else if (obj.y)
        curtop += obj.y;
    return curtop;
}