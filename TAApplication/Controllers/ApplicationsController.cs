using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TAApplication.Data;
using TAApplication.Models;

/**
  Author:    Mason Austin
  Partner:   None
  Date:      10 / 02 / 2021
  Course: CS 4540, University of Utah, School of Computing
  Copyright: CS 4540 and Mason Austin - This work may not be copied for use in Academic Coursework.

  I, Mason Austin, certify that I wrote this code from scratch and did not copy it in part or whole from
  another source.  Any references used in the completion of the assignment are cited in my README file.

  File Contents:
    This controller is used to handle Applications. Currently, there are 6 views
    with the functionality to create, edit, and delete applications.
*/


namespace TAApplication.Controllers
{
    public class ApplicationsController : Controller
    {
        private readonly TA_DB _context;

        public ApplicationsController(TA_DB context)
        {
            _context = context;
        }

        [AllowAnonymous]
        // GET: Application Base
        public async Task<IActionResult> Index()
        {
            return View(await _context.Applications.ToListAsync());
        }

        [Authorize(Roles = "Administrator")]
        // GET: Applications
        public ActionResult List(string sortOrder, string searchString)
        {
            // View bags which are selected by clicking the table titles. These determine the sorting of the table
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DegreeSortParm = sortOrder == "Degree" ? "degree_desc" : "Degree";
            ViewBag.ProgramSortParm = sortOrder == "Program" ? "program_desc" : "Program";
            ViewBag.GPASortParm = sortOrder == "GPA" ? "gpa_desc" : "GPA";
            ViewBag.HoursSortParm = sortOrder == "Hours" ? "hours_desc" : "Hours";
            var students = from s in _context.Applications
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.LastName.Contains(searchString)
                                       || s.FirstName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    students = students.OrderByDescending(s => s.FirstName);
                    break;
                case "Degree":
                    students = students.OrderBy(s => s.Degree);
                    break;
                case "degree_desc":
                    students = students.OrderByDescending(s => s.Degree);
                    break;
                case "Program":
                    students = students.OrderBy(s => s.Program);
                    break;
                case "program_desc":
                    students = students.OrderByDescending(s => s.Program);
                    break;
                case "GPA":
                    students = students.OrderBy(s => s.GPA);
                    break;
                case "gpa_desc":
                    students = students.OrderByDescending(s => s.GPA);
                    break;
                case "Hours":
                    students = students.OrderBy(s => s.Hours);
                    break;
                case "hours_desc":
                    students = students.OrderByDescending(s => s.Hours);
                    break;
                default:
                    students = students.OrderBy(s => s.FirstName);
                    break;
            }
            return View(students.ToList());
        }

        [Authorize(Roles = "Administrator, Applicant")]
        // GET: Applications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var application = await _context.Applications
                .FirstOrDefaultAsync(m => m.ApplicationID == id);
            if (application == null)
            {
                return NotFound();
            }

            return View(application);
        }

        [Authorize(Roles = "Administrator, Applicant")]
        // GET: Applications/Create
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Administrator, Applicant")]
        // POST: Applications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,uID,PhoneNumber,Address,Degree,Program,GPA,Hours,Statement,EnglishFluency,Semesters,LinkedIn,Resume,CreationDate,ModificationDate,Owner")] Application application)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    application.Resume = "NotImplemented";
                    application.GPA = Math.Round(application.GPA, 2);
                    application.CreationDate = DateTime.Now;
                    application.ModificationDate = DateTime.Now;
                    application.Owner = User.Identity.Name;
                    _context.Add(application);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            return View(application);
        }

        [Authorize(Roles = "Administrator, Applicant")]
        // GET: Applications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var application = await _context.Applications.FindAsync(id);
            if (application == null)
            {
                return NotFound();
            }
            return View(application);
        }

        // POST: Applications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Administrator, Applicant")]
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var applicationToUpdate = await _context.Applications.FirstOrDefaultAsync(s => s.ApplicationID == id);
            if (await TryUpdateModelAsync<Application>(
                applicationToUpdate,
                "",
                s => s.FirstName, s => s.LastName, s => s.uID, s => s.PhoneNumber, s => s.Address, s => s.Degree, s => s.Program, s => s.GPA, s => s.Hours, s => s.Statement, s => s.EnglishFluency, s => s.Semesters, s => s.LinkedIn, s => s.Resume, s => s.CreationDate, s => s.ModificationDate, s => s.Owner))
            {
                try
                {
                    applicationToUpdate.GPA = Math.Round(applicationToUpdate.GPA, 2);
                    applicationToUpdate.ModificationDate = DateTime.Now;
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }
            return View(applicationToUpdate);
        }

        [Authorize(Roles = "Administrator, Applicant")]
        // GET: Applications/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var application = await _context.Applications
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ApplicationID == id);
            if (application == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
            }

            return View(application);
        }

        // POST: Applications/Delete/5
        [Authorize(Roles = "Administrator, Applicant")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var application = await _context.Applications.FindAsync(id);
            if (application == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _context.Applications.Remove(application);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }

        /**
         * Retrieves the user's total available hours from the Availability database.
         */
        [HttpGet]
        [Authorize(Roles = "Administrator,Professor,Applicant")]
        public double getAvailableHours (string userId)
        {
            // Query to find all entries from the userId
            var query = from a in _context.Availabilities
                        where a.UserId == userId
                        select a;

            // Each query entry is 15 minutes and there are 60 minutes in an hour
            return (((double)query.Count())*15) / 60;
        }

        private bool ApplicationExists(int id)
        {
            return _context.Applications.Any(e => e.ApplicationID == id);
        }
    }

}
