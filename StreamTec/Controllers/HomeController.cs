using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using StreamTec.Models;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Security.Claims;
using System.Security.Permissions;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Identity;


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
            return View();
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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(Student student)
        {
            int testID = 2208266;
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
                                new Claim(ClaimTypes.Name, obj.StudentId.ToString()),
                                new Claim(ClaimTypes.Email, obj.Email),
                                new Claim(ClaimTypes.Role, "Admin"),
                                new Claim(ClaimTypes.Role, "Student"),
                            };
                            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                            var authProperties = new AuthenticationProperties
                            {
                                //AllowRefresh = bool,
                                // Refreshing the authentication session should be allowed.

                                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(1),
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
                        }else if (obj != null)
                        {
                            var claims = new List<Claim>
                            {
                                new Claim(ClaimTypes.Name, obj.StudentId.ToString()),
                                new Claim(ClaimTypes.Email, obj.Email),
                                new Claim(ClaimTypes.Role, "Student"),
                            };
                            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                            var authProperties = new AuthenticationProperties
                            {
                                //AllowRefresh = bool,
                                // Refreshing the authentication session should be allowed.

                                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(1),
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}