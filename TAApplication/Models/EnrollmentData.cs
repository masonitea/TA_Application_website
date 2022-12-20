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
    public class EnrollmentData
    {
        // ID for the row
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EnrollmentDataID { get; set; }

        // The course's name 
        public string Course{ get; set; }

        // The date of the recorded enrollment
        public DateTime Date { get; set; }

        // The enrollment
        public int Enrollment { get; set; }
    }
}
