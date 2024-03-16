using BookLibrary.Services.Data.Interfaces;
using BookLibrary.Web.Infrastructure.Extentions;
using BookLibrary.Web.ViewModels.Book;
using BookLibraty.Services.Data.Models.Book;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookLibrary.Controllers
{
    [Authorize]
    public class BookController : Controller
    {
        private readonly IBookService bookService;
        private readonly ICategoryService categoryService;
        private readonly IAuthorService authorService;
        public BookController(IBookService bookService, ICategoryService categoryService , IAuthorService authorService)
        {
            this.bookService = bookService;
            this.categoryService = categoryService;
            this.authorService = authorService;
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
                return RedirectToAction("Become", "Agent");
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
            if (model.Image == null || model.Image.Length == 0)
                return BadRequest("No file uploaded.");


          
            try
            {
                string agentId = await this.authorService.GetAuthorIdByUserIdAsync(this.User.GetId());

                string id = await this.bookService.CreateAndReturnIdAsync(model, agentId);
                return this.RedirectToAction("Details", "House", new { id = id });
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
                return RedirectToAction("All", "House");
            }
            bool isUserAuthor = await this.authorService.AuthorExistsByUserId(this.User.GetId());

            if (!isUserAuthor)
            {
                return RedirectToAction("Become", "Agent");
            }
            string authorId = await this.authorService.GetAuthorIdByUserIdAsync(this.User.GetId());

            bool isAuthorOwner = await this.bookService
                .isAuthorWithIdOwnerOfHouseWithIdAsync(id, agentId);

            if (!isAgentOwner )
            {
                return RedirectToAction("Become", "Agent");
            }
            HouseFormModel houseForm = await this.houseService.GetHouseForEditByIdAsync(id);
            houseForm.Categories = await this.categoryService.GetAll();
            return View(houseForm);
        }
        [HttpGet]
        public async Task<IActionResult> Mine()
        {
            return View();
        }
    }
}
