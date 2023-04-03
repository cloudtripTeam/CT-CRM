
// Load the Visualization API and the corechart package.
google.charts.load('current', { 'packages': ['corechart', 'bar'] });

// Set a callback to run when the Google Visualization API is loaded.
google.charts.setOnLoadCallback(drawChartByStatus);
google.charts.setOnLoadCallback(drawChartByCompany);
google.charts.setOnLoadCallback(drawChartBySource);
google.charts.setOnLoadCallback(drawChartByQuery);


// Callback that creates and populates a data table,
// instantiates the pie chart, passes in the data and
// draws it.
function drawChartByStatus(jsdata) {

    // Create the data table.
    var data = new google.visualization.DataTable();
    data.addColumn('string', 'Status');
    data.addColumn('number', 'NoOfCalls');
    $.each(jsdata, function (key, value) {
        data.addRows([
          [value.Status, value.NoOfCalls]
        ]);
    });


    // Set chart options
    var options = {
        'title': 'Calls Report By Status',

        'is3D': true
    };

    // Instantiate and draw our chart, passing in some options.
    var chart = new google.visualization.PieChart(document.getElementById('chart_div'));
    chart.draw(data, options);
}

function drawChartByCompany(jsdata1) {
    // Create the data table.
    var data1 = new google.visualization.DataTable();
    data1.addColumn('string', 'Company');
    data1.addColumn('number', 'NoOfCalls');
    $.each(jsdata1, function (key, value) {
        data1.addRows([
          [value.Company, value.NoOfCalls]
        ]);
    });


    // Set chart options
    var options = {
        'title': 'Calls Report By Company',
        'width': 400,
        'height': 300,
        'is3D': true
    };

    // Instantiate and draw our chart, passing in some options.
    var chart = new google.visualization.PieChart(document.getElementById('chart_div_company'));
    chart.draw(data1, options);


}

function drawChartBySource(jsdata) {
    // Create the data table.
    var data1 = new google.visualization.DataTable();
    data1.addColumn('string', 'Source');
    data1.addColumn('number', 'NoOfCalls');
    $.each(jsdata, function (key, value) {
        data1.addRows([
          [value.Source, value.NoOfCalls]
        ]);
    });


    // Set chart options
    var options = {
        'title': 'Calls Report By Source',
        'width': 400,
        'height': 300,
        'is3D': true
    };

    // Instantiate and draw our chart, passing in some options.
    var chart = new google.visualization.PieChart(document.getElementById('chart_div_source'));
    chart.draw(data1, options);


}








function GetCallsReportByCompany() {

    $.ajax({
        type: "POST",
        url: "CallReports.aspx/getCallsByCompany",
        contentType: "application/json; charset=utf-8",
        dataType: "json",

        responseType: "json",
        success: function (data) {
            var jsdata1 = JSON.parse(data.d);
            drawChartByCompany(jsdata1);

        },
        error: function (data) { alert("error company") }
    });
}

function GetCallsReportBySource() {
    $.ajax({
        type: "POST",
        url: "CallReports.aspx/getCallsBySource",
        contentType: "application/json; charset=utf-8",
        dataType: "json",

        responseType: "json",
        success: function (data) {
            var jsdata = JSON.parse(data.d);
            drawChartBySource(jsdata);

        },
        error: function (data) { alert("error company campaign") }
    });
}


