using TAApplication.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;

/**
  Author:    Mason Austin
  Partner:   None
  Date:      10 / 19 / 2021
  Course: CS 4540, University of Utah, School of Computing
  Copyright: CS 4540 and Mason Austin - This work may not be copied for use in Academic Coursework.

  I, Mason Austin, certify that I wrote this code from scratch and did not copy it in part or whole from
  another source.  Any references used in the completion of the assignment are cited in my README file.

  File Contents:
    Context file for TA_DB. Currently has two tables of applications and courses.
*/
namespace TAApplication.Data
{
    public class TA_DB : DbContext
    {
        public TA_DB(DbContextOptions<TA_DB> options) : base(options)
        {
        }

        public DbSet<Application> Applications { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Availability> Availabilities { get; set; }
        public DbSet<EnrollmentData> EnrollmentDatas { get; set; }
    }
}
