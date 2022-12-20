using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TAApplication.Areas.Identity.Data;
using TAApplication.Data;
using TAApplication.Models;

/**
  Author:    Mason Austin
  Partner:   None
  Date:      12 / 10 / 2021
  Course: CS 4540, University of Utah, School of Computing
  Copyright: CS 4540 and Mason Austin - This work may not be copied for use in Academic Coursework.

  I, Mason Austin, certify that I wrote this code from scratch and did not copy it in part or whole from
  another source.  Any references used in the completion of the assignment are cited in my README file.

  File Contents:
    This controller is used to provide functionality that should only be available for admins
    This includes changing roles for users and checking enrollment data for courses
*/


namespace TAApplication.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<TAUser> _userManager;
        private readonly TA_DB _context;

        public AdminController(
    UserManager<TAUser> userManager, TA_DB context)
        {
            _userManager = userManager;
            _context = context;
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult EnrollmentTrends()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public JsonResult GetEnrollmentData(string startDate, string endDate, string course)
        {
            // Turn start and end string into DateTimes. They are formated as "year-month-day"
            string[] parsedDate = startDate.Split('-');
            DateTime start = new DateTime(int.Parse(parsedDate[0]), int.Parse(parsedDate[1]), int.Parse(parsedDate[2]));
            parsedDate = endDate.Split('-');
            DateTime end = new DateTime(int.Parse(parsedDate[0]), int.Parse(parsedDate[1]), int.Parse(parsedDate[2]));

            // Get all enrollment data that fits the course and date time window
            var query = from a in _context.EnrollmentDatas
                        where a.Course == course && a.Date >= start && a.Date <= end
                        orderby a.Date ascending
                        select a;

            // Create an enrollmentEntry object to represent each query result.
            int numberOfEntries = (int)(end - start).TotalDays;
            enrollmentEntry[] returnData = new enrollmentEntry[numberOfEntries];
            List<EnrollmentData> queryResults = query.ToList();

            for(int i = 0; i < numberOfEntries; i++)
            {
                enrollmentEntry entry = new enrollmentEntry();

                // Should be an entry for every day.
                DateTime correctDate = start.AddDays(i);
                // Date needs to be parseable by highcharts
                if (correctDate.Day < 10)
                {
                    entry.date = correctDate.Year.ToString() + "-" + (correctDate.Month).ToString() + "-0" + correctDate.Day.ToString() + " 00:00:0";
                }
                else
                {
                    entry.date = correctDate.Year.ToString() + "-" + (correctDate.Month).ToString() + "-" + correctDate.Day.ToString() + " 00:00:0";
                }

                // This means there is a row for every single entry and no compensation is needed
                if (numberOfEntries == query.Count())
                {
                    EnrollmentData data = query.ElementAt(i);
                    entry.enrollment = data.Enrollment;
                }
                // This means some entries need to be made.
                // If no entry exists for a date, its enrollment should just be 0.
                else
                {
                    // Not out of range of array
                    if(i < query.Count())
                    {
                        EnrollmentData data = queryResults.ElementAt(i);
                        if (data.Date == correctDate)
                        {
                            entry.enrollment = data.Enrollment;
                        }
                        else
                        {
                            entry.enrollment = 0;
                        }
                    }
                    // Out of range
                    else
                    {
                        entry.enrollment = 0;
                    }
                }

                returnData[i] = entry;
            }

            return Json(returnData);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async  Task<IActionResult> ChangeRole(string userId, string role, string command)
        {
            if(userId == null)
            {
                return BadRequest("userId was not defined");
            }

            if (ModelState.IsValid)
            {
                TAUser user = await _userManager.FindByEmailAsync(userId);
                if (command=="added") 
                {
                    await _userManager.AddToRoleAsync(user, role);
                    return Ok(new { message = "User was added to role!", inputwas = $"{userId}, {role}, {command}" });
                }
                else if(command=="removed")
                {
                    await _userManager.RemoveFromRoleAsync(user, role);
                    return Ok(new { message = "User was removed from role!", inputwas = $"{userId}, {role}, {command}"});
                }
                else
                {
                    return BadRequest("command was invalid");
                }
                
            }
            else
            {
                return BadRequest(new { message = "Model was not valid"});
            }
        }

        public class enrollmentEntry
        {
            public string date { get; set; }
            public int enrollment { get; set; }
        }
    }
}
