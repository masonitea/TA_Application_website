using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Numerics;
using System.Runtime.Intrinsics.X86;
using System.Threading.Tasks;

/**
  Author:    Mason Austin
  Partner:   None
  Date:      10 / 19 / 2021
  Course: CS 4540, University of Utah, School of Computing
  Copyright: CS 4540 and Mason Austin - This work may not be copied for use in Academic Coursework.

  I, Mason Austin, certify that I wrote this code from scratch and did not copy it in part or whole from
  another source.  Any references used in the completion of the assignment are cited in my README file.

  File Contents:
    This model represents a Course and the table created in the database.
*/


namespace TAApplication.Models
{
 /**
 * The choices for Semesters
 */
    public enum Semester
    {
        Spring, Summer, Fall
    }

    /**
     * All information relevant to a Course. DB Model.
     */
    public class Course
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CourseID { get; set; }

        [Display(Name = "Semester Offered",
        ShortName = "semester",
        Description = "Please enter the semester this class will be offered")]
        [Required(ErrorMessage = "Semester is required")]
        public Semester SemesterOffered { get; set; }

        [Display(Name = "Year Offered",
        ShortName = "year",
        Description = "Please enter the year this class will be offered")]
        [Required(ErrorMessage = "Year is required")]
        [Range(1850, 2030, ErrorMessage = "Valid year range is 1850-2030. If the class needs to be later than 2030, contact an administrator.")]
        public int YearOffered { get; set; }

        [Display(Name = "Course Name",
        ShortName = "name",
        Description = "Please enter the Course's name")]
        [Required(ErrorMessage = "Course Name is required")]
        [MaxLength(50, ErrorMessage = "Course Names cannot be over 50 characters.")]
        public string Name { get; set; }

        [Display(Name = "Department",
        ShortName = "department",
        Prompt = "CS, CSE, Comp, ect.",
        Description = "Please enter the department this course belongs to")]
        [Required(ErrorMessage = "Department is required")]
        [MaxLength(42, ErrorMessage = "No department is longer than 42 characters")]
        public string Department { get; set; }

        [Display(Name = "Course Number",
        ShortName = "number",
        Prompt = "1030, 1050, 2420, ect.",
        Description = "Please enter the course's number")]
        [Required(ErrorMessage = "Course Number is required")]
        [Range(1000, 9999, ErrorMessage = "Course numbers are 4 digit numbers")]
        public int Number { get; set; }

        [Display(Name = "Course Section",
        ShortName = "section",
        Prompt = "001, 002, ect.",
        Description = "Please enter your course's section")]
        [Required(ErrorMessage = "Section is required")]
        [RegularExpression(@"^([0-9][0-9][1-9])$", ErrorMessage = "Please enter a valid course section (001-999)")]
        public string Section { get; set; }

        [Display(Name = "Course Description",
        ShortName = "description",
        Description = "Please enter a short summary describing the course's contents")]
        [Required(ErrorMessage = "Description is required")]
        [MaxLength(1000, ErrorMessage="Descriptions are 1000 characters at most.")]
        public string Description { get; set; }

        [Display(Name = "Professor's Unid",
        ShortName = "professorUnid",
        Prompt = "u1234567",
        Description = "Please enter this course's Professor Unid in the form u1234567")]
        [Required(ErrorMessage = "Professor unID is required")]
        [RegularExpression(@"^[u][0-9]{7}$", ErrorMessage = "Please enter in the form: u1234567")]
        public string ProfessorUnid { get; set; }

        [Display(Name = "Professor's Name",
        ShortName = "professorName",
        Description = "Please enter this course's Professor")]
        [Required(ErrorMessage = "Professor name is required")]
        [MaxLength(62, ErrorMessage ="Names cannot be longer than 62 characters")]
        public string ProfessorName { get; set; }

        [Display(Name = "Offered Days",
        ShortName = "days",
        Prompt = "MoTuWeThFr",
        Description = "Please enter the days offered. Only use the first two letters with no spaces inbetween")]
        [Required(ErrorMessage = "Offered Days is required")]
        [RegularExpression(@"^(Mo|Tu|We|Th|Fr)*$", ErrorMessage = "Please enter in the form: MoTuWeThFr")]
        public string OfferedDays { get; set; }

        [Display(Name = "Offered Time",
        ShortName = "time",
        Prompt = "11:30AM-12:30PM, 5:30PM-7:00PM, 8:00AM-9:00AM, ect.",
        Description = "Please enter the start and end time")]
        [Required(ErrorMessage = "Offered Time is required")]
        [RegularExpression(@"^(([0-9]{2}|[0-9])[:]([0-9]{2})([A]|[P])[M])[-](([0-9]{2}|[0-9])[:]([0-9]{2})([A]|[P])[M])$", ErrorMessage = "Please enter the start and end time separated by a -. Enter the times in the format: XX:XX(PM/AM)")]
        public string OfferedTimes { get; set; }

        [Display(Name = "Location Held",
        ShortName = "location",
        Prompt = "WEB L104, MEB 310, FASB 110, ect.",
        Description = "Please enter the building name and room number for this course's location for classes")]
        [Required(ErrorMessage = "Location is required")]
        [MaxLength(15, ErrorMessage = "Location is 15 characters or less. Have you used the building's full name instead of code")]
        public string Location { get; set; }

        [Display(Name = "Credit Hours",
        ShortName = "credits",
        Description = "Please enter the course's credit hours")]
        [Required(ErrorMessage = "Credit Hours is required")]
        [Range(1, 5, ErrorMessage = "Valid Credit Hours is from 1-5")]
        public int CreditHours { get; set; }

        [Display(Name = "Enrollment",
        ShortName = "enrollment",
        Description = "Please enter the number of students enrolled")]
        [Required(ErrorMessage = "Credit Hours is required")]
        [Range(1, 400, ErrorMessage = "Valid enrollment sizes are from 1 to 400")]
        public int Enrollment { get; set; }

        // for admin convenience 
        public string Note { get; set; }

    }
}
