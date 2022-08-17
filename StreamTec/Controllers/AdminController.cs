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

        public List<Student> StudentList()
        {
            List<Student> students = (from stud in Context.Students.Take(5) select stud).ToList();
            return (students);
        }

        public List<Models.Stream> StreamList()
        {
            List<Models.Stream> strea = (from a in Context.Streams.Take(5)  select a).ToList();
            return (strea);
        }

        public ActionResult AdminHome()
        {
            ViewData["Streams"] = StreamList();
            ViewData["Students"] = StudentList();

            return View();
        }


    }
}
