/**
  Author:    Mason Austin
  Partner:   None
  Date:      11 / 15 / 2021
  Course: CS 4540, University of Utah, School of Computing
  Copyright: CS 4540 and Mason Austin - This work may not be copied for use in Academic Coursework.

  I, Mason Austin, certify that I wrote this code from scratch and did not copy it in part or whole from
  another source.  Any references used in the completion of the assignment are cited in my README file.

  File Contents:
  This is the file that contains a method that calls the method to save the AvailabilityTracker's application's data to the backend.
  It also responsible for visually updating the user on the process of the saving.
*/

/**
 * Saves the user's schedule as well as display a spinner and confirmation text. 
 */
function save_schedule() {
    document.getElementById('scheduleSpinner').style.display = "block";
    tracker.saveSchedule();
    document.getElementById('scheduleSpinner').style.display = "none";
    document.getElementById('savedText').innerHTML = "Schedule Saved!";
    setTimeout(fade_out, 5000);
}

function fade_out() {
    document.getElementById('savedText').innerHTML = "";
}
