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
  Date:      10 / 19 / 2021
  Course: CS 4540, University of Utah, School of Computing
  Copyright: CS 4540 and Mason Austin - This work may not be copied for use in Academic Coursework.

  I, Mason Austin, certify that I wrote this code from scratch and did not copy it in part or whole from
  another source.  Any references used in the completion of the assignment are cited in my README file.

  File Contents:
    This controller handles all of the views and logic for Courses.
*/

namespace TAApplication.Controllers
{
    public class CoursesController : Controller
    {
        private readonly TA_DB _context;

        public CoursesController(TA_DB context)
        {
            _context = context;
        }

        [Authorize(Roles = "Administrator,Professor")]
        // GET: Courses
        public async Task<IActionResult> List()
        {
            return View(await _context.Courses.ToListAsync());
        }

        [Authorize(Roles = "Administrator,Professor,Applicant")]
        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .FirstOrDefaultAsync(m => m.CourseID == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        [Authorize(Roles = "Administrator")]
        // GET: Courses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CourseID,SemesterOffered,YearOffered,Name,Department,Number,Section,Description,ProfessorUnid,ProfessorName,OfferedDays,OfferedTimes,Location,CreditHours,Enrollment,Note")] Course course)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(course);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(List));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. " +
                "Try again, and if the problem persists " +
                "see your system administrator.");
            }
            return View(course);
        }

        [Authorize(Roles = "Administrator")]
        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Administrator")]
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var courseToUpdate = await _context.Courses.FirstOrDefaultAsync(s => s.CourseID == id);
            if (await TryUpdateModelAsync<Course>(
                courseToUpdate,
                "",
                s => s.SemesterOffered, s => s.YearOffered, s => s.Name, s => s.Department, s => s.Number, s => s.Section, s => s.Description, s => s.ProfessorUnid, s => s.ProfessorName, s => s.OfferedDays, s => s.OfferedTimes, s => s.Location, s => s.CreditHours, s => s.Enrollment, s => s.Note))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(List));
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }
            return View(courseToUpdate);
        }

        [Authorize(Roles = "Administrator")]
        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.CourseID == id);
            if (course == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
            }

            return View(course);
        }

        // POST: Courses/Delete/5
        [Authorize(Roles = "Administrator")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return RedirectToAction(nameof(List));
            }

            try
            {
                _context.Courses.Remove(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(List));
            }
            catch (DbUpdateException)
            {
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }

        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.CourseID == id);
        }

        /**
         * Saves the note to the course that has a matching id (if there is one)
         */
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> SaveNote(string courseId, string note)
        {
            
            if (courseId == null)
            {
                return BadRequest("courseId was not defined");
            }

            if (ModelState.IsValid)
            {
                int courseint = int.Parse(courseId);
                var course = await _context.Courses.FindAsync(courseint);
                course.Note = note;
                _context.Update(course);
                await _context.SaveChangesAsync();
                return Ok(new { message = "Note was changed!", inputwas = $"{courseId}, {note}" });
            }
            else
            {
                return BadRequest(new { message = "Model was not valid" });
            }          
        }
    }


}
