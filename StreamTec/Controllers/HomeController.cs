using Microsoft.AspNetCore.Mvc;
using StreamTec.Models;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;

namespace StreamTec.Controllers
{
    public class HomeController : Controller
    {
        private readonly WelTecContext _context;

        public HomeController(WelTecContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View("Index");
        }

        public IActionResult Timetable()
        {
            return View(_context.Streams.ToList());
        }

        // Register Action for registring a student
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("StudentId,Email")] Student student)
        {
            try
            {
                // Add a student details to database and redirect user to homepage
                if (ModelState.IsValid)
                {
                    _context.Add(student);
                    await _context.SaveChangesAsync();
                    TempData["message"] = string.Format("Successfully Registered with Student ID: {0}", student.StudentId);
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Student student)
        {
            try
            {
                // Validate a student details
                if (ModelState.IsValid)
                {
                    using (_context)
                    {
                        var obj = _context.Students.Where(s => s.StudentId.Equals(student.StudentId) && s.Email.Equals(student.Email)).FirstOrDefault();
                        if (obj != null)
                        {
                            // Add a student details to session
                            HttpContext.Session.SetString("_StudentId", obj.StudentId.ToString());
                            HttpContext.Session.SetString("_Email", obj.Email.ToString());
                            return RedirectToAction("Timetable", "Home");
                        }
                    }
                }
                TempData["message"] = "Invalid student detilas";
                return RedirectToAction("Index", "Home");
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}