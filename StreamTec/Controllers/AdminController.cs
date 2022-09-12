using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sql;
using Microsoft.EntityFrameworkCore;
using StreamTec.Models;
using System.Collections.Generic;
using System.Linq;
using Stream = StreamTec.Models.Stream;
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
        [Authorize(Roles = "Admin")]
        public List<Enrollment> EnrollmentList()
        {
            var enrollments = Context.Enrollments.Include(s => s.Streams).Include(s => s.Students).ToList() ; 
           
            return enrollments;
        }

        public List<Stream> StreamList()
        {
            var streams = Context.Streams.ToList();

            return streams;
        }

        [Authorize(Roles = "Admin")]
        public ActionResult AdminHome()
        {
            ViewData["Enrollments"] = EnrollmentList();
            ViewData["Streams"] = StreamList();

            return View();
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Search(string search)
        {
            var enrollments = from e in Context.Enrollments.Include(s => s.Students) select e;

            if (!String.IsNullOrEmpty(search))
            {
                enrollments = enrollments.Where(s => s.StudentId.Contains(search));
            }

            ViewData["Enrollments"] = enrollments.ToList();
            ViewData["Streams"] = StreamList();
            return View("AdminHome");
        }
        [Authorize(Roles = "Admin")]
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
            ViewData["Streams"] = StreamList();
            return View("AdminHome");
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add(string stream, string student)
        {
            var studentObj = Context.Enrollments.Where(s => s.Students.StudentId.Equals(student) && s.StreamID.Equals(stream));
            var streamObj = Context.Enrollments.Where(s => s.Students.StudentId.Equals(student) && s.StreamID.Equals(stream));

            if (streamObj == null || studentObj == null)
            {

                return NotFound();
            }
            else
            {
                Context.Enrollments.Add(new Enrollment { StudentId = student, StreamID = stream });
                Context.SaveChanges();
            }

            var enrollments = Context.Enrollments.Include(s => s.Streams).Include(s => s.Students).ToList();
            ViewData["Enrollments"] = enrollments.ToList();
            ViewData["Streams"] = StreamList();
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
