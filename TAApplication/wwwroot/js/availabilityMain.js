/**
  Author:    Mason Austin
  Partner:   None
  Date:      11 / 15 / 2021
  Course: CS 4540, University of Utah, School of Computing
  Copyright: CS 4540 and Mason Austin - This work may not be copied for use in Academic Coursework.

  I, Mason Austin, certify that I wrote this code from scratch and did not copy it in part or whole from
  another source.  Any references used in the completion of the assignment are cited in my README file.

  File Contents:
  This is the main file for the schedule PIXI application.
  It is responsible for creating/initiating the stage, putting objects on the stage, and communicating with the backend database.
*/

class AvailabilityTracker {
    // userId tof the logged in user
    #userId; 

    /**
     * Sets up the variables, stage, listeners, settings, and finally draws all of the objects  
     */
    main() {
        // Initiate stage + variables
        this.#userId = document.getElementById('PassingToJavaScript').value;
        setup_pixi_stage(1000, 680, 0x000000);

        // Listener to ensure mouse always goes up if released off a slot
        app.renderer.view.addEventListener('mouseup', this.checkUnsavedChanges);

        // Allows the use of zIndex which will determine the order objects are layered
        app.stage.sortableChildren = true;

        // Sets up the objects
        this.setupOverlay();
        this.setupDayLabels();
        this.setupSlots();
    }

    /**
     * This will search all slots to see if any slot needs to be updated.
     * If a slot does need to be updated, it will create a label (if one doesn't already exist) warning the user of the unsaved data.
     * If a slot does not need to be updated, it will do nothing or delete the existing label.
     */
    checkUnsavedChanges() {
        // Alert the app the mouse is up
        mouse_down = false;

        // Iterate through children trying to find slots that need to be updated. 
        // Also stores the index of the unsavedChanges label if it exists
        let unsavedChanges = false;
        let i = 0;
        let existingChangesIndex = -1;
        for (i = 0; i < app.stage.children.length; i++) {
            var target = app.stage.children[i];
            if (target instanceof Slot) {
                if (target.needs_update()) {
                    unsavedChanges = true;
                }
            }
            else if (target instanceof PIXI.Text) {
                if (target.id == "unsavedChanges") {
                    existingChangesIndex = i;
                }
            }
        }

        // Create a label if one doesn't exist
        if (unsavedChanges) {
            if (existingChangesIndex == -1) 
            {
                var unsavedChangesLabel = new PIXI.Text('WARNING: Your changes to your schedule are currently not saved.', { font: '10px Arial', fill: 'red', align: 'left' });
                unsavedChangesLabel.x = 40;
                unsavedChangesLabel.y = 600;
                unsavedChangesLabel.id = "unsavedChanges";
                app.stage.addChild(unsavedChangesLabel);
            }
        }
        // Delete the label if it does exist.
        else {
            if (existingChangesIndex != -1) {
                app.stage.removeChild(app.stage.children[existingChangesIndex]);
            }
        }
    }

    /**
     * Sets up labels for the columns in the schedule app. These labels are the day of the week.
     */
    setupDayLabels() {
        var mondayLabel = new PIXI.Text('Monday', { font: '10px Arial', fill: 'white', align: 'left' });
        mondayLabel.x = 42;
        mondayLabel.y = 10;
        mondayLabel.id = "monday";
        app.stage.addChild(mondayLabel);

        var tuesdayLabel = new PIXI.Text('Tuesday', { font: '10px Arial', fill: 'white', align: 'left' });
        tuesdayLabel.x = 202;
        tuesdayLabel.y = 10;
        tuesdayLabel.id = "tuesday";
        app.stage.addChild(tuesdayLabel);

        var wednesdayLabel = new PIXI.Text('Wednesday', { font: '10px Arial', fill: 'white', align: 'left' });
        wednesdayLabel.x = 362;
        wednesdayLabel.y = 10;
        wednesdayLabel.id = "wednesday";
        app.stage.addChild(wednesdayLabel);

        var thursdayLabel = new PIXI.Text('Thursday', { font: '10px Arial', fill: 'white', align: 'left' });
        thursdayLabel.x = 522;
        thursdayLabel.y = 10;
        thursdayLabel.id = "thursday";
        app.stage.addChild(thursdayLabel);

        var fridayLabel = new PIXI.Text('Friday', { font: '10px Arial', fill: 'white', align: 'left' });
        fridayLabel.x = 682;
        fridayLabel.y = 10;
        fridayLabel.id = "friday";
        app.stage.addChild(fridayLabel);
    }

    /**
     * Sets up the dividers for the program that overlay the slots.
     * Also creates an appropriate label for each divider.
     */
    setupOverlay() {
        for (let i = 0; i < 13; i++) {
            // The divider
            let divider = new Divider(50 + i * 40)
            divider.zIndex = 99;
            app.stage.addChild(divider);

            // The label for the divider
            let dividerLabel = new PIXI.Text(divider.get_label(), { font: '3px Arial', fill: 'white', align: 'left' });
            dividerLabel.x = 860;
            dividerLabel.y = 35 + i * 40;
            dividerLabel.id = "divider" + dividerLabel.y;
            app.stage.addChild(dividerLabel);
        }
    }

    /**
     * Sets up all of the slots that the user interacts with to change their schedule.
     * Uses an AJAX query to get the user's existing data
     */
    setupSlots() {
        // Need to use the AvailabilityController's GetSchedule method to prepopulate the user's existing schedule into the app
        var URL = 'Availability/GetSchedule';
        var DATA = { userId: this.#userId };
        $.get(URL, DATA)
            .fail(function () {
                console.log("oops");
            })
            // If successful, the data is an array of JSON objects with the fields "day" and "time" which are both integers.
            .done(function (data) {
                var p = data.length;

                // Draw a completely unavailable schedule
                for (let i = 0; i < 5; i++) {
                    for (let j = 0; j < 48; j++) {
                        let slot = new Slot(false, i, j);
                        slot.zIndex = -99;
                        app.stage.addChild(slot);
                    }
                }

                // Draw all the available slots over the completely unavailable schedule. The slots b
                for (let z = 0; z < p; z++) {
                    let slot = new Slot(true, data[z].day, data[z].time);
                    slot.zIndex = -99;
                    app.stage.addChild(slot);
                }

                // Label for the slots telling the user how to interact with them.
                let slotsLabel = new PIXI.Text('Click and drag to set/un-set available times.', { font: '3px Arial', fill: 'white', align: 'left' });
                slotsLabel.x = 40;
                slotsLabel.y = 550;
                slotsLabel.id = "slots";
                app.stage.addChild(slotsLabel);
            });
    }

    /**
     * Saves the schedule setup by the user by creating an array of strings that contain the data of the slot.
     * This array is passed to an AJAX query to save the schedule to the backend.
     * Each string is setup as follows: "day;hour;minutes;method". The ';' character is for the purpose of splitting.
     */
    saveSchedule() {
        // All the slots the AJAX request wants
        var slots = new Array();

        // Iterate through the children to populate the "slots" array
        for (let i = 0; i < app.stage.children.length; i++) {
            var target = app.stage.children[i];
            // The child must be a Slot object that needs an update
            if (target instanceof Slot) {
                if (target.needs_update()) {
                    // Create the string containing "target"'s data and set the update status to false.
                    var slotInfo = target.get_day() + ';' + target.get_hour() + ';' + target.get_minutes() + ';' + target.update_required();
                    slots.push(slotInfo);
                    target.change_update_status();
                    console.log(slotInfo);
                }
            }
        }

        // AJAX request to save the data.
        var URL = "/Availability/SetSchedule";
        var DATA = { slotsToChange: slots, userId: this.#userId };
        jQuery.ajaxSettings.traditional = true;
        $.post(URL, DATA)
            .fail(function () {
                console.log("oops");
            })
            .done(function (result) {
                console.log(`good job: ${result.message}`);
            });

        // Set the unsaved changes label.
        this.checkUnsavedChanges();
    }
}