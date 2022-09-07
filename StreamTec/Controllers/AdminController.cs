using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sql;
using Microsoft.EntityFrameworkCore;
using StreamTec.Models;
using System.Collections.Generic;
using System.Linq;
// Was causing ambgious error with .Include but dont think it is being used for anything else.
//using System.Data.Entity; 

namespace StreamTec.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private WelTecContext Context { get; }
        public AdminController(WelTecContext context)
        {
            Context = context;
        }

        public List<Enrollment> EnrollmentList()
        {
            var enrollments = Context.Enrollments.Include(s => s.Streams).Include(s => s.Students).ToList() ; 
           
            return enrollments;
        }

        public ActionResult AdminHome()
        {
            ViewData["Enrollments"] = EnrollmentList();

            return View();
        }

        
        public async Task<IActionResult> Search(string search)
        {
            var enrollments = from e in Context.Enrollments.Include(s => s.Students) select e;

            if (!String.IsNullOrEmpty(search))
            {
                enrollments = enrollments.Where(s => s.StudentId.ToString().Contains(search));
            }

            ViewData["Enrollments"] = enrollments.ToList();

            return View("AdminHome");
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id == null || Context.Enrollments == null)
            {
                return NotFound();
            }

            var enrol = await Context.Enrollments
                .FirstOrDefaultAsync(e => e.EnrollmentID == id);

            if (enrol == null)
            {
                return NotFound();
            }
            else
            {
                Context.Enrollments.Remove(enrol);
                await Context.SaveChangesAsync();
            }

            var enrollments = Context.Enrollments.Include(s => s.Streams).Include(s => s.Students).ToList();
            ViewData["Enrollments"] = enrollments.ToList();

            return View("AdminHome");
        }

        public async Task<IActionResult> Add(string stream, int student)
        {
            if (stream == null || student == null|| Context.Enrollments == null)
            {
                return NotFound();
            }

            Context.Enrollments.Add(new Enrollment { StudentId = student, StreamID = stream });
            Context.SaveChanges();
            var enrollments = Context.Enrollments.Include(s => s.Streams).Include(s => s.Students).ToList();
            ViewData["Enrollments"] = enrollments.ToList();

            return View("AdminHome");
        }
        public ActionResult Index()
        { 

            return View();
        }

        public ActionResult AddView()
        {
            return View("AdminAdd");
        }
    }
}
