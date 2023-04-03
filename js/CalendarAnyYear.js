
var mnType = 0;
function getElementPosition(elemID) {
    var offsetTrail = document.getElementById(elemID);
    var offsetLeft = 0;
    var offsetTop = 0;
    while (offsetTrail) {
        offsetLeft += offsetTrail.offsetLeft;
        offsetTop += offsetTrail.offsetTop;
        offsetTrail = offsetTrail.offsetParent;
    }
    return { left: offsetLeft, top: offsetTop };
}
function showCalender(boxid) {
    document.getElementById('hdeprdate').value = boxid.id;

    showCalendarControl(boxid.id);
}

function prefixCtrlID() {
    var currID = document.getElementById('hdeprdate').value;
    var prefix = currID.substring(0, (currID.lastIndexOf("_") + 1));
    return prefix;
}

function getDayString(gDateValue) {
    if (gDateValue == "0") {
        dayName = "Sun"
    }
    else if (gDateValue == "1") {
        dayName = "Mon"
    }
    else if (gDateValue == "2") {
        dayName = "Tue"
    }
    else if (gDateValue == "3") {
        dayName = "Wed"
    }
    else if (gDateValue == "4") {
        dayName = "Thu"
    }
    else if (gDateValue == "5") {
        dayName = "Fri"
    }
    else if (gDateValue == "6") {
        dayName = "Sat"
    }
    return dayName;

}
function GetMonth(mon) {
    switch (mon) {
        case 0: return "JAN"; break;
        case 1: return "FEB"; break;
        case 2: return "MAR"; break;
        case 3: return "APR"; break;
        case 4: return "MAY"; break;
        case 5: return "JUN"; break;
        case 6: return "JUL"; break;
        case 7: return "AUG"; break;
        case 8: return "SEP"; break;
        case 9: return "OCT"; break;
        case 10: return "NOV"; break;
        case 11: return "DEC";
    }
}
function positionInfo(object) {

    var p_elm = object;

    this.getElementLeft = getElementLeft;
    function getElementLeft() {
        var x = 0;
        var elm;
        if (typeof (p_elm) == "object") {
            elm = p_elm;
        } else {
            elm = document.getElementById(p_elm);
        }
        while (elm != null) {
            x += elm.offsetLeft;
            elm = elm.offsetParent;
        }
        return parseInt(x, 10);
    }

    this.getElementWidth = getElementWidth;
    function getElementWidth() {
        var elm;
        if (typeof (p_elm) == "object") {
            elm = p_elm;
        } else {
            elm = document.getElementById(p_elm);
        }
        return parseInt(elm.offsetWidth, 10);
    }

    this.getElementRight = getElementRight;
    function getElementRight() {
        return getElementLeft(p_elm) + getElementWidth(p_elm);
    }

    this.getElementTop = getElementTop;
    function getElementTop() {
        var y = 0;
        var elm;
        if (typeof (p_elm) == "object") {
            elm = p_elm;
        } else {
            elm = document.getElementById(p_elm);
        }
        while (elm != null) {
            y += elm.offsetTop;
            elm = elm.offsetParent;
        }
        return parseInt(y, 10);
    }

    this.getElementHeight = getElementHeight;
    function getElementHeight() {
        var elm;
        if (typeof (p_elm) == "object") {
            elm = p_elm;
        }
        else {
            elm = document.getElementById(p_elm);
        }
        return parseInt(180, 10);
    }

    this.getElementBottom = getElementBottom;
    function getElementBottom() {
        return getElementTop(p_elm) + getElementHeight(p_elm);
    }
}

function CalendarControl() {

    var calendarId = 'CalendarControl';
    var currentYear = 0;
    var currentMonth = 0;
    var currentDay = 0;

    var selectedYear = 0;
    var selectedMonth = 0;
    var selectedDay = 0;

    var months = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];
    var dateField = null;
       
    function changeMonthInc(change) {
        currentMonth += change;
        currentDay = 0;
        if (currentMonth > 12) {
            currentMonth = 1;
            currentYear++;
        }
        else if (currentMonth < 1) {
            currentMonth = 12;
            currentYear--;
        }
    }
    function getProperty(p_property) {
        var p_elm = calendarId;
        var elm = null;

        if (typeof (p_elm) == "object") {
            elm = p_elm;
        } else {
            elm = document.getElementById(p_elm);
        }

        if (elm != null) {
            if (elm.style) {
                elm = elm.style;
                if (elm[p_property]) {
                    return elm[p_property];
                }
                else {
                    return null;
                }
            }
            else {
                return null;
            }
        }
    }

    function setElementProperty(p_property, p_value, p_elmId) {
        var p_elm = p_elmId;
        var elm = null;

        if (typeof (p_elm) == "object") {
            elm = p_elm;
        }
        else {
            elm = document.getElementById(p_elm);
        }

        if ((elm != null) && (elm.style != null)) {
            elm = elm.style;
            elm[p_property] = p_value;
        }
    }

    function setProperty(p_property, p_value) {
        setElementProperty(p_property, p_value, calendarId);
    }

    function getDaysInMonth(year, month) {
        return [31, ((!(year % 4) && ((year % 100) || !(year % 400))) ? 29 : 28), 31, 30, 31, 30, 31, 31, 30, 31, 30, 31][month - 1];
    }

    function getDayOfWeek(year, month, day) {
        var date = new Date(year, month - 1, day)
        return date.getDay();
    }

    this.clearDate = clearDate;
    function clearDate() {
        dateField.value = '';
        hide();
    }

    this.setDate = setDate;
    function setDate(year, month, day) {
        setDateboxid = document.getElementById('hdeprdate').value;
        if (month.toString().length == 1) {
            month = "0" + month.toString();
        }
        if (day.toString().length == 1) {
            day = "0" + day.toString();
        }
        if (document.getElementById('hdeprdate').value == (prefixCtrlID() + "TxtDepartureDate")) {

            document.getElementById(setDateboxid).value = day + '/' + month + '/' + year;
            var afterDate = new Date(year + "/" + month + "/" + day);
            afterDate.setDate(afterDate.getDate() + 7);
            month = parseInt((afterDate.getMonth()) + 1).toString()
            var WMonth = "";
            if (month < 10) {
                WMonth = "0" + month.toString();
            }
            else {
                WMonth = month.toString();
            }
            var WDay = "";
            if (afterDate.getDate() < 10) {
                WDay = "0" + afterDate.getDate();
            }
            else {
                WDay = afterDate.getDate()
            }
        }
        else {
            document.getElementById(setDateboxid).value = day + '/' + month + '/' + year;
        }


        hide();
        return;
    }

    this.changeMonth = changeMonth;
    function changeMonth(change) {
        currentMonth += change;
        currentDay = 0;
        if (currentMonth > 12) {
            currentMonth = 1;
            currentYear++;
        }
        else if (currentMonth < 1) {
            currentMonth = parseInt(12 + currentMonth, 10);
            currentYear--;
        }
        calendar = document.getElementById(calendarId);

        calendar.innerHTML = calendarDrawTable();

    }

    this.changeYear = changeYear;
    function changeYear(change) {
        currentYear += change;
        currentDay = 0;
        calendar = document.getElementById(calendarId);
        calendar.innerHTML = calendarDrawTable();
    }

    function getCurrentYear() {
        var year = new Date().getFullYear();
        if (year < 1900) year += 1900;
        var splitDDate = document.getElementById(document.getElementById('hdeprdate').value).value;
        var splitDDate1 = splitDDate.split('/');
        if (splitDDate == "" || splitDDate1[2] == "yyyy") {
            return year;
        }
        else {
            return parseInt(splitDDate1[2]);
        }     

    }

    function getCurrentMonth() {
        var splitDDate = document.getElementById(document.getElementById('hdeprdate').value).value;  //document.getElementById(prefixCtrlID() + "TxtDepartureDate").value;
        var splitDDate1 = splitDDate.split('/');
        if (splitDDate == "" || splitDDate1[1] == "mm") {
            return new Date().getMonth() + 1;
        }
        else {
            return parseInt(splitDDate1[1].toString(), 10);
        }
    }

    function getCurrentDay() {      
        var splitDDate = document.getElementById(document.getElementById('hdeprdate').value).value;
        var splitDDate1 = splitDDate.split('/');
        if (splitDDate == "" || splitDDate1[0] == "dd") {
            return new Date().getDate();
        }
        else {
            return parseInt(splitDDate1[0].toString(), 10);
        }       
    }
    function calendarDrawTable() {
        var dayOfMonth = 1;
        var validDay = 0;
        var startDayOfWeek;
        var daysInMonth;
        var i;
        var table = "<table  bgcolor='#0fa1ff' cellspacing='0'  cellpadding='0' border='0'>";
        var NoCal = 1;
        try {

            if (NoCal == "") {
                NoCal = 1
            }
        } catch (ex) {
            NoCal = 1;
        }
        if (NoCal == 1) {
            table = table + "<tr><td colspan='2' style='background-color:#0FA1FF; color:#fff;'><table cellspacing='0' cellpadding='0' border='0' width='100%'style='background-color:#0FA1FF; color:#fff;'><tr><td class='previous' style='border-bottom: 1px solid #0fa1ff; background-color:#0FA1FF; color:#fff;' align='left'>";
            table = table + "<a href='javascript:changeCalendarControlMonth(-12);'>&lt;&lt;</a>";
            table = table + "</td><td class='previous' style='border-bottom: 1px solid #0fa1ff' align='left'>";
            table = table + "<a href='javascript:changeCalendarControlMonth(-1);'>&lt;</a>";
            table = table + "</td><td style='border-bottom: 1px solid #0fa1ff; font-weight: bold; color: #ffffff; text-align: center;'>Date</td><td class='next' align='right' style='border-bottom: 1px solid #0fa1ff'>";
            table = table + "<a href='javascript:changeCalendarControlMonth(1);'>&gt;</a>";
            table = table + "</td><td class='next' align='right' style='border-bottom: 1px solid #0fa1ff'>";
            table = table + "<a href='javascript:changeCalendarControlMonth(12);'>&gt;&gt;</a>";
            table = table + "</td></tr></table></td></tr>";
        }
        else {
            table = table + "<tr><td colspan='2'><table cellspacing='0' cellpadding='0' border='0' width='100%'><tr><td class='previous' style='border-bottom: 1px solid #0fa1ff' align='left'>";
            table = table + "<a href='javascript:changeCalendarControlMonth(-12);'>&lt;&lt;</a>";
            table = table + "</td><td class='previous' style='border-bottom: 1px solid #0fa1ff' align='left'>";
            table = table + "<a href='javascript:changeCalendarControlMonth(-1);'>&lt;</a>";
            table = table + "</td><td style='border-bottom: 1px solid #0fa1ff; font-weight: bold; color: #ffffff; text-align: center;'>Date</td><td class='next' align='right' style='border-bottom: 1px solid #0fa1ff'>";
            table = table + "<a href='javascript:changeCalendarControlMonth(1);'>&gt;</a>";
            table = table + "</td><td class='next' align='right' style='border-bottom: 1px solid #0fa1ff'>";
            table = table + "<a href='javascript:changeCalendarControlMonth(12);'>&gt;&gt;</a>";
            table = table + "</td></tr></table></td></tr>";
        }
        table = table + "<tr>";
        for (i = 1; i <= NoCal; i++) {
            if (i != 1) {
                changeMonthInc(1)
            }
            dayOfMonth = 1;
            validDay = 0;
            startDayOfWeek = getDayOfWeek(currentYear, currentMonth, dayOfMonth);
            daysInMonth = getDaysInMonth(currentYear, currentMonth);
            css_class = null; 
            table = table + "<td><table cellspacing='0' cellpadding='0' border='0'>";
            table = table + "<tr class=''>";
            table = table + "<td bgcolor='#ffffff'></td>" + "<td colspan='5' class='title' bgcolor='#ffffff' style='color:#0fa1ff; padding:3px 0 3px 0;'>" + months[currentMonth - 1] + "&nbsp;&nbsp;&nbsp;" + currentYear + "</td>" + "<td bgcolor='#ffffff'></td>";
            table = table + "</tr>";
            table = table + "<tr bgcolor=lightblue><th bgcolor=red>S</th><th>M</th><th>T</th><th>W</th><th>T</th><th>F</th><th>S</th></tr>";

            for (var week = 0; week < 6; week++) {
                table = table + "<tr>";
                for (var dayOfWeek = 0; dayOfWeek < 7; dayOfWeek++) {
                    if (week == 0 && startDayOfWeek == dayOfWeek) {
                        validDay = 1;
                    }
                    else if (validDay == 1 && dayOfMonth > daysInMonth) {
                        validDay = 0;
                    }

                    if (validDay) {
                        if (dayOfMonth == selectedDay && currentYear == selectedYear && currentMonth == selectedMonth) {
                            css_class = 'current';
                        }
                        else if (dayOfWeek == 0 || dayOfWeek == 6) {
                            css_class = 'weekend';
                        }
                        else {
                            css_class = 'weekday';
                        }
                        var DMY = new Date();
                        var perDate = new Date(DMY.getFullYear(), DMY.getMonth(), DMY.getDate());
                       

                        var txtDDateIs = document.getElementById(document.getElementById('hdeprdate').value).value;
                        dapdatearr = txtDDateIs.split('/');
                        var currentYearss;
                        if (dapdatearr[0] > 1) {
                            document.getElementById('setascurrdate').value = document.getElementById(document.getElementById('hdeprdate').value).value;
                            dayOfMonthss = dayOfMonth - (dapdatearr[0] - DMY.getDate()) - 1;
                            currentMonthss = currentMonth - (dapdatearr[1] - DMY.getMonth());
                            currentYearss = currentYear - (dapdatearr[2] - DMY.getFullYear());
                        } else {
                            dayOfMonthss = dayOfMonth;
                            currentMonthss = currentMonth - 1;
                            currentYearss = currentYear;
                        }
                        var CurDate = new Date(currentYearss, currentMonthss, dayOfMonthss);
                        var LastDate = new Date(DMY.getFullYear(), DMY.getMonth() + 11, DMY.getDate());
                        table = table + "<td><a class='" + css_class + "' href=\"javascript:setCalendarControlDate(" + currentYear + "," + currentMonth + "," + dayOfMonth + ")\">" + dayOfMonth + "</a></td>";
                        dayOfMonth++;
                    }
                    else {
                        table = table + "<td class='empty'>&nbsp;</td>";
                    }
                }
                table = table + "</tr>";
            }
            table = table + "</table></td>";
        }
        table = table + "</tr>"
        table = table + "<tr class=''><th colspan='" + NoCal + "' style='padding: 3px;'><a href='javascript:hideCalendarControl();'><font color=black><b>Close</b></font></a></th></tr>"
        table = table + "</table>";
        return table
    }
    this.show = show;
    function show(boxid) {
        can_hide = 0;
        if (dateField == boxid) {
            return;
        }
        else {
            dateField = boxid;
        }

        if (boxid) {
            try {

                selectedMonth = '';
                selectedYear = '';
                selectedDay = '';
            } catch (e) { }
        }

        if (!(selectedYear && selectedMonth && selectedDay)) {
            selectedMonth = getCurrentMonth();
            selectedDay = getCurrentDay();
            selectedYear = getCurrentYear();

        }

        currentMonth = selectedMonth;
        currentDay = selectedDay;
        currentYear = selectedYear;

        if (document.getElementById) {

            calendar = document.getElementById(calendarId);
            calendar.innerHTML = calendarDrawTable(currentYear, currentMonth, boxid);
            setProperty('display', 'block');

            var fieldPos = new positionInfo('100');
            var calendarPos = new positionInfo(calendarId);

            var x = getElementPosition(document.getElementById('hdeprdate').value).left;
            var y = getElementPosition(document.getElementById('hdeprdate').value).top + 25;
            setProperty('left', x + "px");
            setProperty('top', y + "px");

            if (document.all) {
                setElementProperty('display', 'block', 'CalendarControlIFrame');
                setElementProperty('left', x + "px", 'CalendarControlIFrame');
                setElementProperty('top', y + "px", 'CalendarControlIFrame');
                setElementProperty('width', calendarPos.getElementWidth() + "px", 'CalendarControlIFrame');
                setElementProperty('height', calendarPos.getElementHeight() + "px", 'CalendarControlIFrame');
            }
        }
    }

    this.hide = hide;
    function hide() {

        setProperty('display', 'none');
        setElementProperty('display', 'none', 'CalendarControlIFrame');
        dateField = null;

    }

    this.visible = visible;
    function visible() {
        return dateField
    }
    this.can_hide = can_hide;
    var can_hide = 0;
}

var calendarControl = new CalendarControl();
function showCalendarControl(boxid) {
    calendarControl.show(boxid);
}
function clearCalendarControl() {
    calendarControl.clearDate();
}

function hideCalendarControl() {
    if (calendarControl.visible()) {
        calendarControl.hide();
    }
}

function setCalendarControlDate(year, month, day) {

    calendarControl.setDate(year, month, day);
}

function changeCalendarControlYear(change) {

    calendarControl.changeYear(change);
}

function changeCalendarControlMonth(change) {

    calendarControl.changeMonth(change);
}
document.write("<iframe id='CalendarControlIFrame'  frameBorder='0' scrolling='no'></iframe>");
document.write("<div id='CalendarControl' id='cal' style='z-index:4;'></div>");








