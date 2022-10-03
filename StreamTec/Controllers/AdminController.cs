using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sql;
using Microsoft.EntityFrameworkCore;
using StreamTec.Models;
using System.Collections.Generic;
using System.Linq;
using Stream = StreamTec.Models.Stream;
// Was causing ambgious error with .Include but dont think it is being used for anything else.
// using System.Data.Entity; 

namespace StreamTec.Controllers
{
    
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

        public List<Stream> StreamList()
        {
            var streams = Context.Streams.ToList();

            return streams;
        }

        public List<Student> StudentList()
        {
            var studentsList = Context.Students.ToList();

            return studentsList;
        }

        public ActionResult AdminHome()
        {
            ViewData["Enrollments"] = EnrollmentList();
            ViewData["Streams"] = StreamList();
            ViewData["Students"] = StudentList();
            return View();
        }

        public async Task<IActionResult> Sorting(string sortOrder)
        {
            ViewData["StuSortParm"] = String.IsNullOrEmpty(sortOrder) ? "stuID_desc" : "";
            ViewData["StrSortParm"] = sortOrder == "strID" ? "strID_desc" : "strID";

            var enrollments = from e in Context.Enrollments.Include(s => s.Students).Include(s => s.Streams) select e;

            switch (sortOrder)
            {
                case "stuID_desc":
                    enrollments = enrollments.OrderByDescending(s => s.StudentId);
                    break;
                case "strID":
                    enrollments = enrollments.OrderBy(s => s.StreamID);
                    break;
                case "strID_desc":
                    enrollments = enrollments.OrderByDescending(s => s.StreamID);
                    break;
                default:
                    enrollments = enrollments.OrderBy(s => s.StudentId);
                    break;
            }

            ViewData["Enrollments"] = enrollments.ToList();
            ViewData["Streams"] = StreamList();
            ViewData["Students"] = StudentList();
            return View("AdminHome");
        }

        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Search(string stuID, string strID)
        {
            var enrollments = from e in Context.Enrollments.Include(s => s.Students) select e;

            if (!String.IsNullOrEmpty(stuID) && !String.IsNullOrEmpty(strID))
            {
                enrollments = enrollments.Where(s => s.StudentId.Contains(stuID) && s.StreamID.Contains(strID));
                
            }
            else if (!String.IsNullOrEmpty(stuID))
            {
                enrollments = enrollments.Where(s => s.StudentId.Contains(stuID));
            }
            else if (!String.IsNullOrEmpty(strID))
            {
                enrollments = enrollments.Where(s => s.StreamID.Contains(strID));
            }
            else
            {
                enrollments = from e in Context.Enrollments.Include(s => s.Students) select e;
            }

            ViewData["Enrollments"] = enrollments.ToList();
            ViewData["Streams"] = StreamList();
            ViewData["Students"] = StudentList();
            return View("AdminHome");
        }
        //[Authorize(Roles = "Admin")]
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
            ViewData["Students"] = StudentList();
            return View("AdminHome");
        }

        //[Authorize(Roles = "Admin")]
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
            ViewData["Students"] = StudentList();
            return View("AdminHome");
        }
        public ActionResult Index()
        { 

            return View("AdminHome");
        }

        public ActionResult AddView()
        {
            return View("AdminAdd");
        }
    }
}
