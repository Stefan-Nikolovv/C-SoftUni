using Library.Contracts;
using Library.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class BookController : BaseController
    {
        private readonly IBookService bookService;

        public BookController(IBookService bookService)
        {
                this.bookService = bookService; 
        }
        public async Task<IActionResult> All()
        {
          var allBooks = await bookService.GetAllBooksAsync();
            return View(allBooks);
        }
        [HttpGet]
        public async Task<IActionResult> Mine()
        {
            string userId = GetUserId();
            var allMineBooks = await this.bookService.GellAllMineBooksAsync(userId);
            return View(allMineBooks);
        }
        public async Task<IActionResult> AddToCollection(int Id)
        {
            var book = await this.bookService.GetBookByIdAsync(Id);
            if(book == null) {
                return RedirectToAction("All", "Book");
            }
            try
            {
                await this.bookService.AddToUserColletionAsync(GetUserId(), book);
                return RedirectToAction("Mine", "Book");
            }
            catch (Exception)
            {
                return RedirectToAction("All", "Book");
            }
           
        }
        public async Task<IActionResult> RemoveFromCollection(int Id)
        {
            var book = await bookService.GetBookByIdAsync(Id);
            if(book == null)
            {
                return RedirectToAction("Mine", "Book");
            }
            string userId = GetUserId();

            await this.bookService.RemoveBookFromUsersCollectionAsync(userId, book);
            return RedirectToAction("All", "Book");
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = await bookService.GetAddBookViewModelWIthCategories();


            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddBookViewModel model)
        {
            decimal rating;

            if(!decimal.TryParse(model.Rating, out rating) || rating < 0 || rating > 10)
            {
              
                ModelState.AddModelError(nameof(model.Rating), "Rating must be a number between 0 and 10");
            }
     
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            await bookService.AddBookAsync(model);
            return RedirectToAction("Mine", "Book");
        }
        public async Task<IActionResult> Edit(int Id)
        {
            var book = await bookService.GetBookForEditByIdAsync(Id);
            if(book == null)
            {
                return RedirectToAction("All", "Book");
            }
            return View(book);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int Id, AddBookViewModel model)
        {
            decimal rating;

            if (!decimal.TryParse(model.Rating, out rating) || rating < 0 || rating > 10)
            {

                ModelState.AddModelError(nameof(model.Rating), "Rating must be a number between 0 and 10");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            await bookService.AddEdittedBookAsync(Id, model);
            return RedirectToAction("Mine", "Book");
        }
    }
}
