using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
/**
Author:    Mason Austin
Partner:   None
Date:      11 / 14 / 2021
Course: CS 4540, University of Utah, School of Computing
Copyright: CS 4540 and Mason Austin - This work may not be copied for use in Academic Coursework.

I, Mason Austin, certify that I wrote this code from scratch and did not copy it in part or whole from
another source.  Any references used in the completion of the assignment are cited in my README file.

File Contents:
This model represents the Availability for a user.
*/
namespace TAApplication.Models
{
    /**
     * This model makes it so the table will have a row for every single entry for availability.
     * 
     */
    public class Availability
    {
        // ID for the row
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AvailabilityID { get; set; }

        // This will be the user's identity ID
        [ForeignKey("TAUser")]
        public virtual string UserId { get; set; }

        // This will be the day + time the user is available.
        public DateTime Slot { get; set; }
    }
}
