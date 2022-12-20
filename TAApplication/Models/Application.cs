using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/**
  Author:    Mason Austin
  Partner:   None
  Date:      10 / 02 / 2021
  Course: CS 4540, University of Utah, School of Computing
  Copyright: CS 4540 and Mason Austin - This work may not be copied for use in Academic Coursework.

  I, Mason Austin, certify that I wrote this code from scratch and did not copy it in part or whole from
  another source.  Any references used in the completion of the assignment are cited in my README file.

  File Contents:
    This model represents an application. This includes information relevant to their identity as well as what makes them a good TA.
*/


namespace TAApplication.Models
{
    /**
     * The choices for the types of degrees the applicant is pursuing
     */
    public enum Degree
    {
        Doctorate, Masters, Bachelors, None
    }

    /**
     * The choices for English Fluency
     */ 
    public enum EnglishFluency
    {
        Native, Fluent, Adequate, Poor, None
    }

    /**
     * All information relevant to an applicant. DB Model.
     */
    public class Application
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ApplicationID { get; set; }

        [Display(Name = "First Name",
        ShortName = "firstName",
        Description = "Please enter your first name...")]
        [Required(ErrorMessage = "First name is required")]
        [MaxLength(50, ErrorMessage = "Max characters is 50")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name",
        ShortName = "lastName",
        Description = "Please enter your last name...")]
        [Required(ErrorMessage = "Last name is required")]
        [MaxLength(50, ErrorMessage= "Max characters is 50")]
        public string LastName { get; set; }

        [Display(Name = "University of Utah ID",
        ShortName = "Unid",
        Prompt = "u1234567",
         Description = "Please use the form u1234567…")]
        [Required(ErrorMessage = "uID is required")]
        [RegularExpression(@"^[u][0-9]{7}$", ErrorMessage = "Please enter in the form: u1234567")]
        public string uID { get; set; }

        [Display(Name = "Phone Number",
        ShortName = "phoneNumber",
         Description = "Please enter preferred contact number")]
        [Required(ErrorMessage = "Phone Number is Required")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Entered phone format is not valid.")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Address",
        ShortName = "address",
        Prompt = "Street, City, State",
        Description = "Please enter in format: street, city, state")]
        [Required(ErrorMessage = "Address is required")]
        [MaxLength(130, ErrorMessage ="Your address is too large! Max characters is 130")]
        public string Address { get; set; }

        [Display(Name = "Degree",
        ShortName = "degree",
        Prompt = "Choose your degree...",
        Description = "Please enter the degree you are currently pursuing")]
        [Required(ErrorMessage = "Degree is required")]
        public Degree Degree { get; set; }

        [Display(Name = "Program",
        ShortName = "program",
        Prompt = "CS, CSE, ME, ect.",
        Description = "Please enter your declared program's abbreviation")]
        [Required(ErrorMessage = "Program is required")]
        [MaxLength(10, ErrorMessage ="Please enter your program's abbreviation")]
        public string Program { get; set; }

        [Display(Name = "GPA",
        ShortName = "gpa",
        Description = "Please enter your cumulative GPA")]
        [Required(ErrorMessage = "GPA is required")]
        [Range(+1, +4, ErrorMessage ="GPA Must be between 1 and 4")]
        public double GPA { get; set; }

        [Display(Name = "Hours",
        ShortName = "hours",
        Description = "Please enter your weekly available hours")]
        [Required(ErrorMessage = "Hours are required")]
        [Range(0, 30, ErrorMessage ="Valid values are from 0 to 30")]
        public int Hours { get; set; }

        [Display(Name = "Personal Statement",
        ShortName = "statement")]
        [Required(ErrorMessage = "Personal Statement is required")]
        [MaxLength(4000)]
        public string Statement { get; set; }

        [Display(Name = "English Fluency",
        ShortName = "engFluency",
        Description = "Please enter your fluency in English")]
        [Required(ErrorMessage = "English Fluency is required")]
        public EnglishFluency EnglishFluency { get; set; }

        [Display(Name = "Semesters",
        ShortName = "semesters",
        Description = "Please enter completed semesters")]
        [Required(ErrorMessage = "Semesters is required")]
        [Range(0,30, ErrorMessage = "Valid values are from 0 to 30")]
        public int Semesters { get; set; }

        [Display(Name = "LinkedIn Profile",
        ShortName = "linkedIn",
        Description = "Please enter your LinkedIn Profile URL")]
        [Required(ErrorMessage = "LinkedIn is required")]
        [RegularExpression(@"^https?://((www|\w\w)\.)?linkedin.com/((in/[^/]+/?)|(pub/[^/]+/((\w|\d)+/?){3}))$", ErrorMessage = "Please enter a valid LinkedIn URL")]
        public string LinkedIn { get; set; }

        // in development
        public string Resume { get; set; }

        // These values are handled during the create and edit methods in the ApplicationsController
        [ScaffoldColumn(false)]
        public DateTime CreationDate { get; set; }
        [ScaffoldColumn(false)]
        public DateTime ModificationDate { get; set; }

#nullable enable
        [ScaffoldColumn(false)]
        public string? Owner { get; set; }
    }
}
