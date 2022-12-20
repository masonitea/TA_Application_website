/**
  Author:    Mason Austin
  Partner:   None
  Date:      11 / 15 / 2021
  Course: CS 4540, University of Utah, School of Computing
  Copyright: CS 4540 and Mason Austin - This work may not be copied for use in Academic Coursework.

  I, Mason Austin, certify that I wrote this code from scratch and did not copy it in part or whole from
  another source.  Any references used in the completion of the assignment are cited in my README file.

  File Contents:
  Contains the Slot class.
  Slots are boxes representative of 15 minute windows that the user interacts with to their schedule.
*/

class Slot extends PIXI.Graphics
{
    #needsUpdate = false; // If this slot needs to be updated in the backend
    #selected = false; // Selected or not. Determines the color of the slot.
    #x = 0; // The X position
    #y = 0; // The Y position

    /**
     * Constructs a slot object from whether the slot is selected as well as the day + time.
     * @param {any} selected
     * @param {any} day
     * @param {any} time
     */
    constructor(selected, day, time)
    {
        // Initiate with super() and set selected if needed.
        super();
        if (selected) {
            this.#selected = true;
        }

        // Settings to make it interactive and so it makes the user's mouse a pointe rwhen hovered
        this.interactive = true;
        this.buttonMode = true;

        // Draw the object
        this.draw_self(day, time);

        // Add listeners so the user can click and drag to select slots.
        this.on('mousedown', this.pointer_down);
        this.on('mouseover', this.pointer_over);
        this.on('mouseup', this.pointer_up);
    }

    /**
     * Draws the slot using the given day and time from the constructor. 
     * Day and time are technically the column and row and are used to calculate the x/y position.
     * @param {any} column
     * @param {any} row
     */
    draw_self(column, row)
    {
        this.beginFill(this.get_color());
        this.#x = column * 160 + 40;
        this.#y = 10 * row + 50;
        this.drawRect(this.#x, this.#y, 150, 10);
        this.endFill();
    }

    /**
     * Changes the status of the slot and redraws it in the color of its new status 
     */
    change_status() {
        this.clear();
        this.#selected = !(this.#selected);
        this.#needsUpdate = !(this.#needsUpdate);
        this.beginFill(this.get_color());
        this.drawRect(this.#x, this.#y, 150, 10);
        this.endFill();
    }

    /**
     * If a pointer is down and over a slot object, the slot needs to be updated as well as the mouse_down variable 
     */
    pointer_down() {
        this.change_status();
        mouse_down = true;
    }

    /**
     *  If a pointer is already down and sliders over a slot, the slot should be updated. This ensure the "drag" functionality
     */
    pointer_over() {
        if (mouse_down) {
            this.change_status();
        }
    }

    /**
     * If the user releases their mouse, update the mouse_down variable to reflect that
     * */
    pointer_up() {
        mouse_down = false;
  
    }

    /**
     * Gets the color for the slot based off the #selected variable. 
     */
    get_color() {
        if (this.#selected) {
            return 0xffffe0;
        }
        else {
            return 0xadd8e6;
        }
    }

    /**
     * Returns true if the slot needs to be updated in the backend
     */
    needs_update() {
        return this.#needsUpdate;
    }

    /**
     * Returns the update required for this slot if it was to be updated in the backend.
     * */
    update_required() {
        if (this.#selected) {
            return 'add';
        }
        else {
            return 'remove';
        }
    }

    /**
     * Converts the x value back into the day. (1- Monday, 2- Tuesday, ect.) 
     */
    get_day() {
        return (this.#x - 40) / 160 + 1;
    }

    /**
    * Converts the y value back into the minutes. (0-60)
    */
    get_minutes() {
        var minutes = (this.#y - 50) / 10;
        while (minutes > 3) {
            minutes = minutes - 4;
        }
        minutes = minutes * 15;
        return minutes;
    }

    /**
     * Converts the y value back into the hour. (8-20))
     */
    get_hour() {
        var hour = (this.#y - 50) / 40;
        return Math.floor(hour) + 8;
    }

    /**
     * Inverts the update status of this slot
     */
    change_update_status() {
        this.#needsUpdate = !(this.#needsUpdate);
    }
}