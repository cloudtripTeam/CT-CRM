
// Load the Visualization API and the corechart package.
google.charts.load('current', { 'packages': ['corechart', 'bar'] });

// Set a callback to run when the Google Visualization API is loaded.
google.charts.setOnLoadCallback(drawChartTrafficByCampaign);
google.charts.setOnLoadCallback(drawChartTafficOnPage);
google.charts.setOnLoadCallback(drawChartByTrafficByCompany);
google.charts.setOnLoadCallback(drawChartByTrafficForDestination);
google.charts.setOnLoadCallback(drawChartByTrafficFromCountry);
google.charts.setOnLoadCallback(drawChartByTrafficFromCity);
// Callback that creates and populates a data table,
// instantiates the pie chart, passes in the data and
// draws it.



function drawChartByTrafficByCompany(jsdata) {
    // Create the data table.
    var data1 = new google.visualization.DataTable();
    data1.addColumn('string', 'Company');
    data1.addColumn('number', 'Hits');
    $.each(jsdata, function (key, value) {
        
        data1.addRows([
          [value.Company, value.NoOfHits]
        ]);
    });


    // Set chart options
    var options = {
        'title': 'Traffic Report By Company',
        'width': 400,
        'height': 300,
        'is3D': true
    };

    // Instantiate and draw our chart, passing in some options.
    var chart = new google.visualization.PieChart(document.getElementById('chart_div_company'));
    chart.draw(data1, options);


}

function drawChartByTrafficForDestination(jsdata) {
    // Create the data table.
    var data1 = new google.visualization.DataTable();
    data1.addColumn('string', 'Destination');
    data1.addColumn('number', 'NoOfHits');
    $.each(jsdata, function (key, value) {
        data1.addRows([
          [value.Destination, value.NoOfHits]
        ]);
    });


    // Set chart options
    var options = {
        'title': 'Traffic Report By Destination',
        'width': 400,
        'height': 300,
        'is3D': true
    };

    // Instantiate and draw our chart, passing in some options.
    var chart = new google.visualization.PieChart(document.getElementById('chart_div_destination'));
    chart.draw(data1, options);


}


function drawChartTafficOnPage(jsdata1) {
    // Create the data table.
    var data1 = new google.visualization.DataTable();
    data1.addColumn('string', 'Page');
    data1.addColumn('number', 'NoOfHits');
    $.each(jsdata1, function (key, value) {
        data1.addRows([
          [value.Page, value.NoOfHits]
        ]);
    });


    // Set chart options
    var options = {
        'title': 'Traffic Report on online booking pages',
        'width': 400,
        'height': 300,
        'is3D': true
    };

    // Instantiate and draw our chart, passing in some options.
    var chart = new google.visualization.PieChart(document.getElementById('chart_div_page'));
    chart.draw(data1, options);


}
function drawChartTrafficByCampaign(jsdata) {
   
    // Create the data table.
    var data = new google.visualization.DataTable();
    data.addColumn('string', 'Campaign');
    data.addColumn('number', 'NoOfHits');
    $.each(jsdata, function (key, value) {
       
        data.addRows([
          [value.Campaign, value.NoOfHits]
        ]);
    });


    // Set chart options
    var options = {
        'title': 'Traffic Report By Campaign',

        'is3D': true
    };

    // Instantiate and draw our chart, passing in some options.
    var chart = new google.visualization.PieChart(document.getElementById('chart_div_campaign'));
    chart.draw(data, options);
}

function drawChartByTrafficFromCountry(jsdata) {
    // Create the data table.
    var data1 = new google.visualization.DataTable();
    data1.addColumn('string', 'Country');
    data1.addColumn('number', 'NoOfHits');
    $.each(jsdata, function (key, value) {
        data1.addRows([
          [value.Destination, value.NoOfHits]
        ]);
    });


    // Set chart options
    var options = {
        'title': 'Traffic Report By Country',
        'width': 400,
        'height': 300,
        'is3D': true
    };

    // Instantiate and draw our chart, passing in some options.
    var chart = new google.visualization.PieChart(document.getElementById('chart_div_Country'));
    chart.draw(data1, options);


}