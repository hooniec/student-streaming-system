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
    /// <summary>
    /// AdminController contains the methods to function admin jobs for streaming system.
    /// </summary>
    public class AdminController : Controller
    {
        /// <summary>
        /// Load the database context as Context
        /// </summary>
        private WelTecContext Context { get; }

        /// <summary>
        /// Creating AdminController object with context.
        /// </summary>
        /// <param name="context">Database to manage data</param>
        public AdminController(WelTecContext context)
        {
            Context = context;
        }

        /// <summary>
        /// Find all the data rows of Enrollments table to return them as list.
        /// </summary>
        /// <returns>A list of enrollment's data</returns>
        public List<Enrollment> EnrollmentList()
        {
            var enrollments = Context.Enrollments.Include(s => s.Streams).Include(s => s.Students).ToList() ; 
           
            return enrollments;
        }

        /// <summary>
        /// Find all the data rows of Streams table to return them as list.
        /// </summary>
        /// <returns>A list of stream's data</returns>
        public List<Stream> StreamList()
        {
            var streams = Context.Streams.ToList();

            return streams;
        }

        /// <summary>
        /// Find all the data rows of Students table to return them as list.
        /// </summary>
        /// <returns>A list of student's data</returns>
        public List<Student> StudentList()
        {
            var studentsList = Context.Students.ToList();

            return studentsList;
        }

        /// <summary>
        /// Put tables data into ViewData to return view with those data.
        /// </summary>
        /// <returns>AdminHome View</returns>
        [Authorize(Roles = "Admin")]
        public ActionResult AdminHome()
        {
            ViewData["Enrollments"] = EnrollmentList();
            ViewData["Streams"] = StreamList();
            ViewData["Students"] = StudentList();
            ViewData["EnrollmentCount"] = EnrollmentList().Count();
            return View();
        }

        /// <summary>
        /// Sorting functionality for student ID and stream ID to admin
        /// </summary>
        /// <param name="sortOrder">A string keyword for sorting</param>
        /// <returns>AdminHome view with sorted data</returns>
        [Authorize(Roles = "Admin")]
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
            ViewData["EnrollmentCount"] = enrollments.ToList().Count();
            return View("AdminHome");
        }

        /// <summary>
        /// Search functionality by student ID and stream ID given by Admin to display relevant result.
        /// </summary>
        /// <param name="stuID">A string keyword for student ID</param>
        /// <param name="strID">A string keyword for stream ID</param>
        /// <returns>AdminHome view with result</returns>
        [Authorize(Roles = "Admin")]
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
            ViewData["EnrollmentCount"] = enrollments.ToList().Count();
            return View("AdminHome");
        }

        /// <summary>
        /// Delete a Enrollment row specified by Admin
        /// </summary>
        /// <param name="id">A int of enrollment ID</param>
        /// <returns>AdminHome view with updated result</returns>
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
            ViewData["Students"] = StudentList();
            ViewData["EnrollmentCount"] = enrollments.ToList().Count();
            return View("AdminHome");
        }

        /// <summary>
        /// Add functionality allows admin to add Enrollment row
        /// </summary>
        /// <param name="stream">A string of stream ID</param>
        /// <param name="student">A string of student ID</param>
        /// <returns>AdminHome view with updated result and acknowledge message</returns>
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add(string stream, string student)
        {
 
            if (stream == null || student == null)
            {
                TempData["message"] = "Please select stream and student to add";
                return View("AdminHome");
            }
            else
            {
                var studentObj = Context.Students.Where(s => s.StudentId.Equals(student)).FirstOrDefault();
                var streamObj = Context.Streams.Where(s => s.StreamID.Equals(stream)).FirstOrDefault();

                if (studentObj != null && streamObj != null)
                {
                    Context.Enrollments.Add(new Enrollment { StudentId = student, StreamID = stream });
                    Context.SaveChanges();
                }
                else
                {
                    TempData["message"] = "Please select stream and student to add";
                    return View("AdminHome");
                }
            }

            var enrollments = Context.Enrollments.Include(s => s.Streams).Include(s => s.Students).ToList();
            ViewData["Enrollments"] = enrollments.ToList();
            ViewData["Streams"] = StreamList();
            ViewData["Students"] = StudentList();
            ViewData["EnrollmentCount"] = enrollments.ToList().Count();
            return View("AdminHome");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            ViewData["Streams"] = StreamList();
            ViewData["Students"] = StudentList();
            return View("AdminHome");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult AddView()
        {
            return View("AdminAdd");
        }
    }
}
