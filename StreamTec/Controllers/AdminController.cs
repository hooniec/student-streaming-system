using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sql;
using Microsoft.EntityFrameworkCore;
using StreamTec.Models;
using System.Collections.Generic;
using System.Linq;

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
     
            //var distinctEnrollments = enrollments.DistinctBy(s => s.Students).ToList();  
           
            return enrollments;
        }
        //https://docs.microsoft.com/en-us/aspnet/mvc/overview/getting-started/getting-started-with-ef-using-mvc/creating-a-more-complex-data-model-for-an-asp-net-mvc-application
        //https://docs.microsoft.com/en-us/aspnet/mvc/overview/getting-started/getting-started-with-ef-using-mvc/reading-related-data-with-the-entity-framework-in-an-asp-net-mvc-application
        public ActionResult AdminHome()
        {
            ViewData["Enrollments"] = EnrollmentList();

            return View();
        }

        
        public async Task<IActionResult> AdminSearch(string search)
        {
            var enrollments = from e in Context.Enrollments.Include(s => s.Students) select e;

            if (!String.IsNullOrEmpty(search))
            {
                enrollments = enrollments.Where(s => s.StudentId.ToString().Contains(search));
            }

            ViewData["Enrollments"] = enrollments.ToList();

            return View("AdminHome");
        }

        public ActionResult Index()
        { 

            return View();
        }
    }
}
