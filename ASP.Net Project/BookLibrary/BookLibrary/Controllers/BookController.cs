using Microsoft.AspNetCore.Mvc;

namespace BookLibrary.Controllers
{
    public class BookController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> All()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Mine()
        {
            return View();
        }
    }
}
