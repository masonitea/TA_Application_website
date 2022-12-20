/**
  Author:    Mason Austin
  Partner:   None
  Date:      11 / 17 / 2021
  Course: CS 4540, University of Utah, School of Computing
  Copyright: CS 4540 and Mason Austin - This work may not be copied for use in Academic Coursework.

  I, Mason Austin, certify that I wrote this code from scratch and did not copy it in part or whole from
  another source.  Any references used in the completion of the assignment are cited in my README file.

  File Contents:
    Javascript code for displaying the number of hours an applicant is available on the Application Index.
*/

/**
 Changes the role of a user using their id, role, and a command instructing whether it's to add or remove
 */
function display_applicant_hours() {
    var URL = 'Applications/getAvailableHours';
    var DATA = { userId: document.getElementById('PassingToJavaScript').value };
    $.get(URL, DATA)
        .fail(function () {
            console.log("oops");
        })
        // If successful, data should be the hours.
        .done(function (data) {
            document.getElementById('applicantHours').innerHTML = data;
        });
}


$(document).ready(display_applicant_hours);