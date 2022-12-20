using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TAApplication.Data;
using TAApplication.Models;

/**
Author:    Mason Austin
Partner:   None
Date:      11 / 14 / 2021
Course: CS 4540, University of Utah, School of Computing
Copyright: CS 4540 and Mason Austin - This work may not be copied for use in Academic Coursework.

I, Mason Austin, certify that I wrote this code from scratch and did not copy it in part or whole from
another source.  Any references used in the completion of the assignment are cited in my README file.

File Contents:
This controller handles the view for changing a schedule. This controller has methods for getting/setting the user's schedule to accomplish this.
*/

namespace TAApplication.Controllers
{
    public class AvailabilityController : Controller
    {
        private readonly TA_DB _context;

        public AvailabilityController(
    TA_DB context)
        {
            _context = context;
        }

        [Authorize(Roles = "Administrator,Professor,Applicant")]
        public IActionResult Index()
        {
            return View();
        }

        /**
         * Given a userId, this method will find every time window the user is available in the Availability Database
         * Returns an array of JSON objects that contain the day and time from the DateTime in numeric form so the AvailabilityTracker can understand it.
         * Day: 0=Monday, 1=Tuesday, 2=Wednesday, 3=Thursday, 4=Friday
         * Time:  0=8:00 AM, 1=8:15 AM, 2=8:30 AM, ect. up to 8:00 P.M.
         */
        [HttpGet]
        [Authorize(Roles = "Administrator,Professor,Applicant")]
        public JsonResult GetSchedule(string userId)
        {
            // Query to find all entries from the userId
            var query = from a in _context.Availabilities
                        where a.UserId == userId
                        select new
                        {
                            time = a.Slot
                        };

            // Will be returned with all the user's slots
            List<scheduleSlot> slots = new List<scheduleSlot>();
            // Populate the slots array
            foreach (var x in query)
            {
                // The datetime from the row
                DateTime slotTime = x.time;
                scheduleSlot slot = new scheduleSlot();

                // Get the day. 
                switch (slotTime.DayOfWeek)
                {
                    case System.DayOfWeek.Monday:
                        slot.day = 0;
                        break;
                    case System.DayOfWeek.Tuesday:
                        slot.day = 1;
                        break;
                    case System.DayOfWeek.Wednesday:
                        slot.day = 2;
                        break;
                    case System.DayOfWeek.Thursday:
                        slot.day = 3;
                        break;
                    case System.DayOfWeek.Friday:
                        slot.day = 4;
                        break;
                }

                // Get the time.
                slot.time = ((slotTime.Hour - 8) * 60 + slotTime.Minute) / 15;
                slots.Add(slot);
            }

            // Return the built array
            return Json(slots);
        }

        /**
         * Will manage the user's availability based on the slots to change.
         * Slot string format: day;hour;minutes;method
         */
        [HttpPost]
        [Authorize(Roles = "Administrator,Professor,Applicant")]
        public async Task<IActionResult> SetSchedule(IEnumerable<string> slotsToChange, string userId)
        {
            // Ensure the id is valid
            if (userId == null)
            {
                return BadRequest("userId was not defined");
            }
            // Check if the schedule really needs to be updated
            if (slotsToChange.Count() == 0)
            {
                return Ok(new { message = "Availability did not need any changes" });
            }

            // Update schedule if model is valid
            if (ModelState.IsValid)
            {
                // Update every single slot
                foreach (string slot in slotsToChange)
                {
                    // Split to get an array to access the day, hour, minutes, and method (respectively) independently
                    string[] items = slot.Split(';');
                    DateTime slotTime = new DateTime(2000, 5, int.Parse(items[0]), int.Parse(items[1]), int.Parse(items[2]), 0);

                    // Add the slot if method matches
                    if (items[3] == "add")
                    {
                        // Create new availability object and add it
                        Availability availability = new Availability();
                        availability.UserId = userId;
                        availability.Slot = slotTime;

                        _context.Add(availability);
                    }
                    // Remove the slot if the method is not "add"
                    else
                    {
                        // Find the slot and remove it.
                        var query = from a in _context.Availabilities
                                    where a.UserId == userId && a.Slot == slotTime
                                    select a;

                        foreach (Availability x in query)
                        {
                            _context.Remove(x);
                        }
                    }
                }

                // Save changes and return
                await _context.SaveChangesAsync();
                return Ok(new { message = "Schedule was saved!" });
            }
            // Return error for invalid model
            else
            {
                return BadRequest(new { message = "Model was not valid" });
            }
        }

        /**
         * Contains the data needs for one schedule slot that the PIXI app would want to draw.
         * Day: 0=Monday, 1=Tuesday, 2=Wednesday, 3=Thursday, 4=Friday
         * Time:  0=8:00 AM, 1=8:15 AM, 2=8:30 AM, ect. up to 8:00 P.M.
         */
        public class scheduleSlot
        {
            public int day { get; set; }
            public int time { get; set; }
        }
    }
}