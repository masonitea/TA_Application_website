using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

/**
  Author:    Mason Austin
  Partner:   None
  Date:      10 / 19 / 2021
  Course: CS 4540, University of Utah, School of Computing
  Copyright: CS 4540 and Mason Austin - This work may not be copied for use in Academic Coursework.

  I, Mason Austin, certify that I wrote this code from scratch and did not copy it in part or whole from
  another source.  Any references used in the completion of the assignment are cited in my README file.

  File Contents:
    The identity user for the TA_DB. Currently 
*/

namespace TAApplication.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the TAUser class
    public class TAUser : IdentityUser
    {
        
        [Display(Name = "University Id",
        ShortName = "unid",
        Prompt = "u1234567",
        Description = "Please enter your unID")]
        [Required(ErrorMessage = "University ID name is required")]
        [RegularExpression(@"^[u][0-9]{7}$", ErrorMessage = "Please enter in the form: u1234567")]
        public string Unid { get; set; }

        [Display(Name = "First Name",
        ShortName = "firstname",
        Description = "Please enter your first name")]
        [Required(ErrorMessage = "First name is required")]
        [MaxLength(35, ErrorMessage ="First names cannot be longer than 35 characters")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name",
ShortName = "lastname",
Description = "Please enter your last name")]
        [Required(ErrorMessage = "Last name is required")]
        [MaxLength(35, ErrorMessage = "Names cannot be longer than 35 characters")]
        public string LastName { get; set; }

    }
}
