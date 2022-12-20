/**
  Author:    Mason Austin
  Partner:   None
  Date:      12 / 10 / 2021
  Course: CS 4540, University of Utah, School of Computing
  Copyright: CS 4540 and Mason Austin - This work may not be copied for use in Academic Coursework.

  I, Mason Austin, certify that I wrote this code from scratch and did not copy it in part or whole from
  another source.  Any references used in the completion of the assignment are cited in my README file.

  File Contents:
  Gets a figure named with the id 'chart' and will turn it into a Highchart for the purpose of displaying enrollment data over time.
  Has a method for adding data to the chart.
*/

/** Create the high chart **/
Highcharts.chart('chart', {
    title: {
        text: 'Enrollments Over Time'
    },

    yAxis: {
        title: {
            text: 'Number of Students'
        }
    },

    xAxis: {
        title: {
            text: 'Dates'
        },
        tickInterval: 1000 * 60 * 60 * 24 * 7, // one week
        type: 'datetime',
        labels: {
            format: '{value:%b %e}'
        },
    },

    legend: {
        layout: 'vertical',
        align: 'right',
        verticalAlign: 'middle'
    },

    plotOptions: {
        series: {
            label: {
                connectorAllowed: false
            },
        }
    },

    responsive: {
        rules: [{
            condition: {
                maxWidth: 500
            },
            chartOptions: {
                legend: {
                    layout: 'horizontal',
                    align: 'center',
                    verticalAlign: 'bottom'
                }
            }
        }]
    }

});

/**
 * Adds a series to the Highcharts by getting data from the database VIA ajax.
 */
function add_series() {
    // Hide the chart, show a spinner
    document.getElementById('chart').style.display = "none";
    document.getElementById('spinner').style.display = "block";

    // AJAX get request data
    var URL = "GetEnrollmentData";
    var URLDATA = { startDate: $("#startDate").val(), endDate: $("#endDate").val(), course: $("#course").val() };

    // Get the data from the endpoint.
    $.get(URL, URLDATA)
        .fail(function () {
            console.log("oops");
        })
        // If successful, the data is an array of JSON objects with the fields "date" (string in form mm-dd-year) and "enrollment" (int)
        .done(function (data) {
            var chartData = new Array();
            for (let i = 0; i < data.length; i++) {
                chartData[i] = [Date.parse(data[i].date), data[i].enrollment];
            }
            $("#chart").highcharts().addSeries(
                {
                    name: $("#course").val(),
                    data: chartData
                });
        });

    // Hide the spinner, redisplay the chart
    document.getElementById('spinner').style.display = "none";
    document.getElementById('chart').style.display = "block";
}