/**
  Author:    Mason Austin
  Partner:   None
  Date:      11 / 15 / 2021
  Course: CS 4540, University of Utah, School of Computing
  Copyright: CS 4540 and Mason Austin - This work may not be copied for use in Academic Coursework.

  I, Mason Austin, certify that I wrote this code from scratch and did not copy it in part or whole from
  another source.  Any references used in the completion of the assignment are cited in my README file.

  File Contents:
  This class represents the dividers that divide the columns of slots into groups of 4 slots, representing one hour.
*/

class Divider extends PIXI.Graphics
{
    // The label to go at the end of the divider (I.E. 8:00 A.M.)
    #label = "";

    /**
     * Constructs a divider drawing, sets the label, and sets the events
     * @param {any} y The y position of the divider
     */
    constructor(y)
    {
        // Call super() and draw the object.
        super();
        this.draw_self(y);

        // 50 pixels of padding (from the top), 40 pixels inbetween each divider.
        // This information is used to create the label.
        y = (y - 50) / 40;
        switch (y) {
            case 0:
                this.#label = "8:00 A.M."
                break;
            case 1:
                this.#label = "9:00 A.M."
                break;
            case 2:
                this.#label = "10:00 A.M."
                break;
            case 3:
                this.#label = "11:00 A.M."
                break;
            case 4:
                this.#label = "12:00 P.M."
                break;
            case 5:
                this.#label = "1:00 P.M."
                break;
            case 6:
                this.#label = "2:00 P.M."
                break;
            case 7:
                this.#label = "3:00 P.M."
                break;
            case 8:
                this.#label = "4:00 P.M."
                break;
            case 9:
                this.#label = "5:00 P.M."
                break;
            case 10:
                this.#label = "6:00 P.M."
                break;
            case 11:
                this.#label = "7:00 P.M."
                break
            case 12:
                this.#label = "8:00 P.M."
                break;
            default:
                this.#label = "ERROR";
        }

        // Set the events
        this.on('mousedown', this.pointer_down);
        this.on('mouseup', this.pointer_up);
    }

    /**
     * Constructs a white line to divide slots that is 820 pixels long and 1 pixel tall.
     * @param {any} y The y location of the divider.
     */
    draw_self(y)
    {
        this.lineStyle(1, 0xffffff);
        this.beginFill(0xffffff);
        this.drawRect(30, y, 820, 1);
        this.endFill();
    }

    /**
     * Returns the appropriate label to label this divider 
     */
    get_label() {
        return this.#label;
    }

    /**
    * Ensures that if the user's mouse goes down on a divider (Not likely that a 1px line is clicked but plausible.), it still goes down.
    **/
    pointer_down() {
        mouse_down = true;
    }

    /**
    * Ensures that if the user's mouse goes up on a divider (Not likely that a 1px line is stopped on but plausible.), it still goes up.
    */
    pointer_up() {
        mouse_down = false;
    }
}