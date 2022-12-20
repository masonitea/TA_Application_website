/**
  Author:    Mason Austin
  Partner:   None
  Date:      11 / 15 / 2021
  Course: CS 4540, University of Utah, School of Computing
  Copyright: CS 4540 and Mason Austin - This work may not be copied for use in Academic Coursework.

  I, Mason Austin, certify that I wrote this code from scratch and did not copy it in part or whole from
  another source.  Any references used in the completion of the assignment are cited in my README file.

  File Contents:
  Sets up a PIXI application. Code taken from the professor's examples on his website.
*/


/**
 * Global access to the PIXI Application 
 */
var app = null;
var mouse_down = false;

/**
 *  Create PIXI stage
 */
function setup_pixi_stage(width, height, bg_color) {
    app = new PIXI.Application({ backgroundColor: bg_color });
    app.renderer.resize(width, height);
    $("#canvas_div").append(app.view);
}

