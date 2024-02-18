using Microsoft.AspNetCore.Mvc;
using SeminarHub.Models;
using System.Diagnostics;

namespace SeminarHub.Controllers
{
    public class HomeController : Controller
    {
      
        public IActionResult Index()
        {

            if (User?.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("All", "Seminar");
            }
            return View();
        }

       
    }
}