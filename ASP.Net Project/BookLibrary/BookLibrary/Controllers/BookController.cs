using BookLibrary.Services.Data.Interfaces;
using BookLibrary.Web.Infrastructure.Extentions;
using BookLibrary.Web.ViewModels.Book;
using BookLibraty.Services.Data.Models.Book;
using Humanizer.Bytes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace BookLibrary.Controllers
{
    [Authorize]
    public class BookController : Controller
    {
        private readonly IBookService bookService;
        private readonly ICategoryService categoryService;
        private readonly IAuthorService authorService;
        private readonly IWebHostEnvironment webHostEnvironment;
        public BookController(IBookService bookService, ICategoryService categoryService , 
            IAuthorService authorService,
            IWebHostEnvironment webHostEnvironment
            )
        {
            this.bookService = bookService;
            this.categoryService = categoryService;
            this.authorService = authorService;
            this.webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> All([FromQuery] AllBooksQueryModel queryModel)
        {
            AllBooksFilteredAndPagedServiceModel serviceModel = await this.bookService.GetAllBooksFilteredAndPaged(queryModel);
            queryModel.Books = serviceModel.Books;
            queryModel.TotalBooks = serviceModel.TotalBooksCount;
            queryModel.Categories = await this.categoryService.AllCategoryNameAsync();

            return View(queryModel);
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            bool isAuthor = await this.authorService.AuthorExistsByUserId(this.User.GetId()!);

            if (!isAuthor)
            {
                return RedirectToAction("Become", "Author");
            }
            BookFormModel formModel = new BookFormModel()
            {
                Categories = await this.categoryService.GetAll()
            };


            return View(formModel);
    
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm] BookFormModel model)
        {
            string uniqueFileName = ProcessUploadedFile(model);
            bool existCategory = await this.categoryService.ExistsById(model.CategoryId);
            bool isAuthor = await this.authorService.AuthorExistsByUserId(this.User.GetId()!);
            if (!isAuthor)
            {
                return RedirectToAction("Become", "Author");
            }

            if (!existCategory)
            {
                ModelState.AddModelError(nameof(model.CategoryId), "Selected Category does not exists!");
            }
            if (!ModelState.IsValid)
            {
                model.Categories = await this.categoryService.GetAll();


                return View(model);
            }
            
            try
            {
                string agentId = await this.authorService.GetAuthorIdByUserIdAsync(this.User.GetId());
               
                string id = await this.bookService.CreateAndReturnIdAsync(model, agentId , uniqueFileName);
                return this.RedirectToAction("Details", "Book", new { id = id });
            }
            catch (Exception)
            {

                this.ModelState.AddModelError(string.Empty, "Unxprcted error occurred while you are add house");
                model.Categories = await this.categoryService.GetAll();
                return View(model);
            }
          
      
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            bool bookExists = await this.bookService.ExistByIdAsync(id);

            if (!bookExists)
            {
                return RedirectToAction("All", "Book");
            }
            bool isUserAuthor = await this.authorService.AuthorExistsByUserId(this.User.GetId());

            if (!isUserAuthor)
            {
                return RedirectToAction("Become", "Author");
            }
            string authorId = await this.authorService.GetAuthorIdByUserIdAsync(this.User.GetId());

            bool isAuthorOwner = await this.bookService
                .isAuthorWithIdOwnerOfHouseWithIdAsync(id, authorId);

            if (!isAuthorOwner)
            {
                return RedirectToAction("Become", "Author");
            }

            BookFormModel bookForm = await this.bookService.GetBookForEditByIdAsync(id);
            
            bookForm.Categories = await this.categoryService.GetAll();
          
            return View(bookForm);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(string id, [FromForm] BookFormModel model)
        {

            string uniqueFileName = ProcessUploadedFile(model);
            bool bookExists = await this.bookService.ExistByIdAsync(id);

            if (!bookExists)
            {
                return RedirectToAction("All", "Book");
            }
            bool isUserAuthor = await this.authorService.AuthorExistsByUserId(this.User.GetId());

            if (!isUserAuthor)
            {
                return RedirectToAction("Become", "Author");
            }
            string authorId = await this.authorService.GetAuthorIdByUserIdAsync(this.User.GetId());

            bool isAgentOwner = await this.bookService
                .isAuthorWithIdOwnerOfHouseWithIdAsync(id, authorId);

            if (!isAgentOwner)
            {
                return RedirectToAction("Become", "Author");
            }

            if (!ModelState.IsValid)
            {
                model.Categories = await this.categoryService.GetAll();
                return this.View(model);
            }
            try
            {
                await this.bookService.EditBookByIdAsync(id, model, uniqueFileName);
            }
            catch (Exception)
            {

                ModelState.AddModelError(string.Empty, "Unexptected Error while trying to edit this house try again later!");
            }
            return RedirectToAction("Details", "Book", new { id });
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(string id)
        {
            bool bookExists = await this.bookService.ExistByIdAsync(id);

            if (!bookExists)
            {
                return RedirectToAction("All", "Book");
            }
            BookDetailsViewModel model = await this.bookService.GetBookDetailsAsync(id);
           
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            bool bookExists = await this.bookService.ExistByIdAsync(id);

            if (!bookExists)
            {
                return RedirectToAction("All", "Book");
            }
            bool isUserAuthor = await this.authorService.AuthorExistsByUserId(this.User.GetId());

            if (!isUserAuthor)
            {
                return RedirectToAction("Become", "Author");
            }
            string authorId = await this.authorService.GetAuthorIdByUserIdAsync(this.User.GetId());

            bool isAhtorOwner = await this.bookService
                .isAuthorWithIdOwnerOfHouseWithIdAsync(id, authorId);

            if (!isAhtorOwner)
            {
                return RedirectToAction("Become", "Author");
            }

            try
            {
                await this.bookService.GetBookByIdAndDelete(id);
                return RedirectToAction("Mine", "Book");
            }
            catch (Exception)
            {

                throw;
            }

        }
        [HttpPost]
        public async Task<IActionResult> Like(string id)
        {
            bool bookExists = await bookService.ExistByIdAsync(id);
            if (!bookExists)
            {
                return RedirectToAction("All", "Book");
            }

            bool isBookLiked = await bookService.IsLikedAsync(id);
            if (isBookLiked)
            {
                return RedirectToAction("All", "Book");
            }

            bool isUserAuthor =
                await authorService.AuthorExistsByUserId(User.GetId()!);
            if (!isUserAuthor)
            {
                return RedirectToAction("Index", "Home");
            }

            try
            {
                await bookService.LikeBookAsync(id, User.GetId()!);
            }
            catch (Exception)
            {

            }



            return RedirectToAction("Mine", "House");
        }
        [HttpPost]
        public async Task<IActionResult> Unlike(string id)
        {
            bool bookExists = await bookService.ExistByIdAsync(id);
            if (!bookExists)
            {
                return RedirectToAction("All", "Book");
            }

            bool isBookLiked = await bookService.IsLikedAsync(id);
            if (isBookLiked)
            {
                return RedirectToAction("All", "Book");
            }


            bool isCurrentUserRenterOfTheHouse =
                await bookService.IsLikedByUserWithIdAsync(id, User.GetId()!);
            if (!isCurrentUserRenterOfTheHouse)
            {

                return RedirectToAction("Mine", "House");
            }

            try
            {
                await bookService.UnLikeBookAsync(id);
            }
            catch (Exception)
            {

            }

            return RedirectToAction("Mine", "House");
        }
        [HttpGet]
        public async Task<IActionResult> Mine()
        {
            return View();
        }

        private string ProcessUploadedFile(BookFormModel model)
        {
            string uniqueFileName = null;

            if (model.Image != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Uploads");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Image.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Image.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }
    }
}
