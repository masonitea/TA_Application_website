/**
  Author:    Mason Austin
  Partner:   None
  Date:      10 / 19 / 2021
  Course: CS 4540, University of Utah, School of Computing
  Copyright: CS 4540 and Mason Austin - This work may not be copied for use in Academic Coursework.

  I, Mason Austin, certify that I wrote this code from scratch and did not copy it in part or whole from
  another source.  Any references used in the completion of the assignment are cited in my README file.

  File Contents:
    Javascript code for changing databases through post methods and sweet alerts.
    Currently there is two methods: save_note() used on Courses/List and change_role() used on Admin/Index
*/

/**
 Changes the role of a user using their id, role, and a command instructing whether it's to add or remove
 */
function change_role() {
    var DATA = { userId: event.target.dataset.id, role: event.target.dataset.role, command: event.target.dataset.command };
    Swal.fire({
        title: 'Are you sure?',
        text: event.target.dataset.id + " will have the role " + event.target.dataset.role + " " + event.target.dataset.command,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Change Role'
    }).then((result) => {
        if (result.isConfirmed) {
            var URL = "/Admin/ChangeRole";

            $.post(URL, DATA)
                .fail(function () {
                    console.log("oops");
                })
                .done(function (result) {
                    Swal.fire({
                        title: 'Roles have been changed!',
                        text: '',
                        icon: 'success'
                    }).then((result) => {
                        if (result.isConfirmed) {
                            var x = document.getElementById('spinner').style.display = "block";
                            window.location.reload();
                        }
                        else {
                            var x = document.getElementById('spinner').style.display = "block";
                            window.location.reload();
                        }
                    })
                    console.log(`good job: ${result.message}`);
                });
        }
        else {
            Swal.fire({
                title: 'Roles have not been changed',
                text: '',
                icon: 'info'
            }).then((result) => {
                if (result.isConfirmed) {
                    var x = document.getElementById('spinner').style.display = "block";
                    window.location.reload();
                }
                else {
                    var x = document.getElementById('spinner').style.display = "block";
                    window.location.reload();
                }
            })
        }
    })
}

/**
 * Saves the new course note by taking the course_id and using that to grab the input (note text) from the html and find the course.
 */
function save_note() {
    var note = event.target.dataset.noteid;
    var val = document.getElementById(note).value;
    var DATA = { courseId: event.target.dataset.id, note: val };

    Swal.fire({
        title: 'Are you sure?',
        text: 'The old note will be replaced',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Save Note'
    }).then((result) => {
        if (result.isConfirmed) {
            var URL = "/Courses/SaveNote";

            var spinnerId = 'spinner_' + DATA.courseId;
            var x = document.getElementById(spinnerId).style.display = "block";
            $.post(URL, DATA)
                .fail(function () {
                    console.log("oops");
                })
                .done(function (result) {
                    Swal.fire({
                        title: 'Note has been saved!',
                        text: '',
                        icon: 'success'
                    })
                    console.log(`good job: ${result.message}`);
                })
                .always(function () {
                    var x = document.getElementById(spinnerId).style.display = "none";
                });
        }
        else {
            Swal.fire({
                title: 'Note has not been saved',
                text: '',
                icon: 'info'
            })
        }
    })
}