using Microsoft.AspNetCore.Mvc;
using BookLibrary.Services.Data.Interfaces;
using BookLibrary.Web.Infrastructure.Extentions;
using BookLibrary.Web.ViewModels.Author;
namespace BookLibrary.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IAuthorService authorService;

        public AuthorController( IAuthorService authorService)
        {
            this.authorService = authorService; 
        }
        [HttpGet]
        public async Task<IActionResult> Become()
        {
            string? userId = this.User.GetId();
            bool isAuthor = await authorService.AuthorExistsByUserId(userId);
            if (isAuthor)
            {
                return this.RedirectToAction("Index", "Home");
            }

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Become(BecomeAuthorFormModel model)
        {
            string? userId = this.User.GetId();
            bool isAuthor = await authorService.AuthorExistsByUserId(userId);
            if (isAuthor)
            {
                return this.RedirectToAction("Index", "Home");
            }

            bool isPhoneNumberTaken = await this.authorService.AuthorExistsPhoneNumberAsync(model.PhoneNumber);
            if (isPhoneNumberTaken)
            {
                ModelState.AddModelError(nameof(model.PhoneNumber), "Author with this number is already exists!");
            }
            if (!this.ModelState.IsValid)
            {
                return View(model);
            }

            bool userHasRents = await this.authorService.UserHasLikesAsync(userId);
            if (userHasRents)
            {
                return this.RedirectToAction("Index", "Home");
            }

            try
            {
                await this.authorService.Create(userId, model);
            }
            catch (Exception)
            {

                throw;
            }
            return RedirectToAction("All", "Book");
        }
    }
}
