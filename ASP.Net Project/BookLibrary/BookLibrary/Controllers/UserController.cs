using Microsoft.AspNetCore.Mvc;

namespace BookLibrary.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }
    }
}
