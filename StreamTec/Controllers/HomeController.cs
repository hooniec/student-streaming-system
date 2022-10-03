using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using StreamTec.Models;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Security.Claims;
using System.Net.Mail;
using MimeKit;

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

        public IActionResult Help()
        {
            return View("Help");
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
                else
                {
                    return View(student);
                }
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }

            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(Student student)
        {
            string testID = "2208266";
            string testEmail = "ethan@email.com";
            try
            {
                // Validate a student details
                if (ModelState.IsValid)
                {
                    using (_context)
                    {   
                        var obj = _context.Students.Where(s => s.StudentId.Equals(student.StudentId) && s.Email.Equals(student.Email)).FirstOrDefault();
                        if (obj.StudentId == testID && obj.Email == testEmail)
                        {
                            var claims = new List<Claim>
                            {
                                new Claim(ClaimTypes.Name, obj.StudentId),
                                new Claim(ClaimTypes.Email, obj.Email),
                                new Claim(ClaimTypes.Role, "Admin"),
                                new Claim(ClaimTypes.Role, "Student"),
                            };
                            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                            var authProperties = new AuthenticationProperties
                            {
                                AllowRefresh = true,
                                // Refreshing the authentication session should be allowed.

                                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                                // The time at which the authentication ticket expires. A 
                                // value set here overrides the ExpireTimeSpan option of 
                                // CookieAuthenticationOptions set with AddCookie.

                                IsPersistent = true,
                                // Whether the authentication session is persisted across 
                                // multiple requests. When used with cookies, controls
                                // whether the cookie's lifetime is absolute (matching the
                                // lifetime of the authentication ticket) or session-based.

                                //IssuedUtc = < DateTimeOffset >,
                                // The time at which the authentication ticket was issued.

                                RedirectUri = "~/Home/Index"
                                // The full path or absolute URI to be used as an http 
                                // redirect response value.
                            };

                            // Add a student details to session                            
                            HttpContext.Session.SetString("_StudentId", obj.StudentId);
                            HttpContext.Session.SetString("_Email", obj.Email.ToString());
                            TempData["_Email"] = obj.Email;

                            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                            
                            return RedirectToAction("Index", "Stream");
                        }else if (obj != null)
                        {
                            var claims = new List<Claim>
                            {
                                new Claim(ClaimTypes.Name, obj.StudentId),
                                new Claim(ClaimTypes.Email, obj.Email),
                                new Claim(ClaimTypes.Role, "Student"),
                            };
                            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                            var authProperties = new AuthenticationProperties
                            {
                                AllowRefresh = true,
                                // Refreshing the authentication session should be allowed.

                                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                                // The time at which the authentication ticket expires. A 
                                // value set here overrides the ExpireTimeSpan option of 
                                // CookieAuthenticationOptions set with AddCookie.

                                IsPersistent = true,
                                // Whether the authentication session is persisted across 
                                // multiple requests. When used with cookies, controls
                                // whether the cookie's lifetime is absolute (matching the
                                // lifetime of the authentication ticket) or session-based.

                                //IssuedUtc = < DateTimeOffset >,
                                // The time at which the authentication ticket was issued.

                                RedirectUri = "~/Home/Index"
                                // The full path or absolute URI to be used as an http 
                                // redirect response value.
                            };

                            // Add a student details to session                            
                            HttpContext.Session.SetString("_StudentId", obj.StudentId.ToString());
                            HttpContext.Session.SetString("_Email", obj.Email.ToString());

                            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                            return RedirectToAction("Index", "Stream");
                        }
                        else
                        {
                            return View(student);
                        }                        
                    }
                }
                //TempData["message"] = "Invalid student details";
                return View(student);
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

        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);
            TempData["message"] = "Successfully logged out";

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> AfterSubmit()
        {
            // SendEmail();
            HttpContext.Session.Clear();
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);
            TempData["message"] = "Successfully Submitted";

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> SubmitTimetable(string studentId, List<string> completedStreamList)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (_context)
                    {
                        foreach (var stream in completedStreamList)
                        {
                            var enrollment = new Enrollment { StudentId = studentId, StreamID = stream };
                            _context.Add(enrollment);

                            //var streamObj = _context.Streams.Where(s => s.StreamID.Equals(stream)).FirstOrDefault();
                            //streamObj.Capacity -= 1;
                        }
                        _context.SaveChanges();

                        return Json("Success");
                    }
                }
                else
                {
                    return Json("Error");
                }
            }
            catch (Exception)
            {
                return Json("Caught error");
            }
        }

        [HttpPost]
        public IActionResult SendEmail(string studentId, List<string> completedStreamList)
        {
            //string studentEmail = HttpContext.Session.GetString("_Email");
            var studentEmail = _context.Students.Where(s => s.StudentId.Equals(studentId)).FirstOrDefault().Email;

            var msg = new MimeMessage();
            msg.From.Add(new MailboxAddress("WelTec Stream System", "streamtec.weltec@gmail.com"));
            msg.To.Add(new MailboxAddress("Student", studentEmail));
            msg.Subject = "Here is your completed timetable for student ID: " + studentId + ".";

            var builder = new BodyBuilder();

            foreach (var stream in completedStreamList)
            {
                var obj = _context.Streams.Where(s => s.StreamID.Equals(stream)).FirstOrDefault();

                builder.TextBody += obj.StreamID + " : " + obj.Day + " ( Start - " + obj.StartTime + ", End - " + obj.EndTime + " ) \n";
            }

            msg.Body = builder.ToMessageBody();

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("streamtec.weltec@gmail.com", "yacdyxmmtfldmkrh");

                client.Send(msg);
                client.Disconnect(true);
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