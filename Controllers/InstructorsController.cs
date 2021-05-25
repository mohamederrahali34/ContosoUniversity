using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Data;
using ContosoUniversity.Models;
using ContosoUniversity.Models.SchoolViewModels;
using ContosoUniversity.Models.SchoolViewModels;
namespace ContosoUniversity.Controllers
{
    public class InstructorsController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly SchoolContext _context;

        public InstructorsController(SchoolContext context)
        {
            _context = context;
        }

        // GET: Instructors
        public async Task<IActionResult> Index(int? ID,int? CourseID)
        {
            var viewModel = new Models.SchoolViewModels.InstructorIndexData();
            viewModel.Instructors = await _context.Instructors
                .Include(i => i.OfficeAssignement)
                .Include(i => i.courseAssignements)
                .ThenInclude(i => i.Course)
                .ThenInclude(i => i.Enrollments)
                .ThenInclude(i => i.Student) 
                .Include(i => i.courseAssignements)
                .ThenInclude(i => i.Course)
                .ThenInclude(i => i.Department)
                .AsNoTracking()
                .OrderBy(i => i.LastName)
                .ToListAsync();
            if(ID != null)
            {
                ViewData["InstructorID"] = ID.Value;
                Instructor instructor = viewModel.Instructors.Where(
                    i => i.ID == ID.Value).Single();
                viewModel.Courses = instructor.courseAssignements.Select(s => s.Course);
            }

            if(CourseID != null)
            {
                ViewData["CourseID"] = CourseID.Value;
                viewModel.Enrollments = viewModel.Courses.Where(
                    x=>x.CourseID == CourseID).Single().Enrollments;
            }
            return View(viewModel);
        }

        // GET: Instructors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instructor = await _context.Instructors
                .FirstOrDefaultAsync(m => m.ID == id);
            if (instructor == null)
            {
                return NotFound();
            }

            return View(instructor);
        }

        // GET: Instructors/Create
        public IActionResult Create()
        {
            var instructor = new Instructor();
            instructor.courseAssignements = new List<CourseAssignment>();
            PopulateAssignedCourseData(instructor);
            return View();
        }

        // POST: Instructors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,LastName,FirstMidName,HireDate")] Instructor instructor,string []selectedCourses)
        {
            if(selectedCourses != null)
            {
                instructor.courseAssignements = new List<CourseAssignment>();
                foreach(var course in selectedCourses)
                {
                    var courseToAdd = new CourseAssignment { InstructorID = instructor.ID, CourseID = int.Parse(course) };
                    instructor.courseAssignements.Add(courseToAdd);
                }
            }
            if (ModelState.IsValid)
            {
                _context.Add(instructor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateAssignedCourseData(instructor);
            return View(instructor);
        }

        // GET: Instructors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instructor = await _context.Instructors.
                Include(i => i.OfficeAssignement).
                Include(i => i.courseAssignements).
                ThenInclude(i => i.Course).
                AsNoTracking().SingleOrDefaultAsync(m => m.ID == id);

            if (instructor == null)
            {
                return NotFound();
            }
            PopulateAssignedCourseData(instructor);
            return View(instructor);
        }
        private void PopulateAssignedCourseData(Instructor instructor)
        {
            var allCourses = _context.Courses;
            var instructorCourses = new HashSet<int>(instructor.courseAssignements.Select(c => c.CourseID));
            var viewModel = new List<AssignedCourseData>();
            foreach(var course in allCourses)
            {
                viewModel.Add(
                    new AssignedCourseData
                    {
                        CourseID = course.CourseID ,
                        Title =course.Title ,
                        Assigned = instructorCourses.Contains(course.CourseID) 
                    });

            }
            ViewData["Courses"] = viewModel;    
        }

        // POST: Instructors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id,string []selectedCourses)
        {
            if (id == null)
            {
                return NotFound();
            }
            var instructorToUpdate = await _context.Instructors
              .Include(i => i.OfficeAssignement).
              Include(i => i.courseAssignements).
              ThenInclude(i =>i.Course)
              .SingleOrDefaultAsync(s => s.ID == id);
                if(await TryUpdateModelAsync<Instructor>(instructorToUpdate,"",
                    i =>i.FirstMidName,i=>i.LastName,i =>i.HireDate,i => i.OfficeAssignement))
            {
                if (String.IsNullOrWhiteSpace(instructorToUpdate.OfficeAssignement?.Location))
                {
                    instructorToUpdate.OfficeAssignement = null;
                }
                UpdateInstructorCourses(selectedCourses, instructorToUpdate); 
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to save changes." +
                        "Try again, and if the problem persists," +
                        "see your system administrator");
                }
                return RedirectToAction(nameof(Index));
            }
            UpdateInstructorCourses(selectedCourses, instructorToUpdate);
            PopulateAssignedCourseData(instructorToUpdate);
            return View(instructorToUpdate);
        }

        private void UpdateInstructorCourses(string [] selectedCoures,Instructor instructorToUpdate)
        {
            if(selectedCoures == null)
            {
                instructorToUpdate.courseAssignements = new List<CourseAssignment>();
                return;
            }

            var selecedCoursesHS = new HashSet<string>(selectedCoures);
            var instructorCourses = new HashSet<int>(instructorToUpdate.courseAssignements.Select(c => c.Course.CourseID));

            foreach(var course in _context.Courses)
            {
                if (selecedCoursesHS.Contains(course.CourseID.ToString()))
                {
                    if (!instructorCourses.Contains(course.CourseID))
                    {
                        instructorToUpdate.courseAssignements.Add(new CourseAssignment { InstructorID= instructorToUpdate.ID});
                    }
                }
                else
                {
                    if (instructorCourses.Contains(course.CourseID))
                    {
                        CourseAssignment courseToRemove = instructorToUpdate.courseAssignements.SingleOrDefault(i =>i.CourseID==course.CourseID);
                        _context.Remove(courseToRemove);
                    }
                }
            }
        }
        // GET: Instructors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instructor = await _context.Instructors
                .FirstOrDefaultAsync(m => m.ID == id);
            if (instructor == null)
            {
                return NotFound();
            }

            return View(instructor);
        }

        // POST: Instructors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var instructor = await _context.Instructors.FindAsync(id);
            _context.Instructors.Remove(instructor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InstructorExists(int id)
        {
            return _context.Instructors.Any(e => e.ID == id);
        }
    }
}
