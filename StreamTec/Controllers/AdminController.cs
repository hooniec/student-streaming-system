using Microsoft.AspNetCore.Mvc;
using StreamTec.Models;

namespace StreamTec.Controllers
{
    public class AdminController : Controller
    {
        private WelTecContext db = new WelTecContext();

        public IActionResult AdminHome()
        {
            return View(db.Streams.ToList());
        }


    }
}
