using BookLibrary.Models;
using BookLibrary.Services.Data.Interfaces;
using BookLibrary.Web.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BookLibrary.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBookService bookService;

        public HomeController(IBookService bookService)
        {
            this.bookService = bookService;
        }

        public async Task<IActionResult> Index()
        {
            
            IEnumerable<IndexViewModel> viewModel =
                await bookService.LastThreeBooksAsync();



            return View(viewModel);
        }

        public IActionResult Home()
        {

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
