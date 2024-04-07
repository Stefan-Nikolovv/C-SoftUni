using BookLibrary.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using BookLibrary.Web.ViewModels.User;
using static BookLibrary.Web.ViewModels.User.LoginFormModel;
using BookLibrary.Web.Infrastructure.Extentions;
using Microsoft.AspNetCore.Authorization;
using BookLibrary.Web.ViewModels.Book;
using Microsoft.AspNetCore.Hosting;
using BookLibrary.Services.Data.Interfaces;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace BookLibrary.Controllers
{
    public class UserController : Controller
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IUserService userService;
        private readonly IAuthorService authorService;
        public UserController(SignInManager<ApplicationUser> signInManager,
                              UserManager<ApplicationUser> userManager,
                              IWebHostEnvironment webHostEnvironment,
                              IUserService userService,
                              IAuthorService authorService
                              )
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.webHostEnvironment = webHostEnvironment;
            this.userService = userService;
            this.authorService = authorService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromForm] RegisterFormModel model)
        {
            string picture = ProcessUploadedFile(model.ProfilePicture);
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            ApplicationUser user = new ApplicationUser()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                ProfilePicture = picture
            };

            await userManager.SetEmailAsync(user, model.Email);
            await userManager.SetUserNameAsync(user, model.Email);

            IdentityResult result =
                await userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return View(model);
            }

            await signInManager.SignInAsync(user, false);


            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Login(string? returnUrl = null)
        {


            LoginFormModel model = new LoginFormModel()
            {
                ReturnUrl = returnUrl
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result =
                await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

            if (!result.Succeeded)
            {


                return View(model);
            }

            return Redirect(model.ReturnUrl ?? "/Home/Index");
        }
        [HttpGet]
        
        public IActionResult Logout(string? returnUrl = null)
        {
            
            signInManager.SignOutAsync();


            return Redirect(returnUrl ?? "/Home/Index");
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> EditProfile()
        {

            string? isUserIsLogged = this.User.GetId();

            if (isUserIsLogged == null)
            {
                return RedirectToAction("Login", "User");
            }


            EditUserProfileFormModel model = await this.userService.GetUserByIdForEditAsync(this.User.GetId()!);

            return View(model);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditProfile([FromForm] EditUserProfileFormModel model)
        {

            string newPic = ProcessUploadedFile(model.NewProfilePicture);
            string? isUserIsLogged = this.User.GetId();
           
            if (isUserIsLogged == null)
            {
                return RedirectToAction("Login", "User");
            }
            bool isUserIsAuthor = await this.authorService.AuthorExistsByUserId(this.User.GetId()!);

            if(!isUserIsAuthor)
            {
                return RedirectToAction("Become", "Author");
            }
            if(model.Password != null)
            {
                model.Password = PasswordHasher(model.Password);
            }
            try
            {
                
                model.ProfilePicture = newPic;
                await this.userService.EditUserByIdAsync(model, this.User.GetId());
            }
            catch (Exception)
            {

                throw;
            }


            return RedirectToAction("Mine", "Book");
        }
        private string ProcessUploadedFile(IFormFile file)
        {
            string uniqueFileName = null;

            if (file != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Profile");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
            }
           


            return uniqueFileName;
        }

        private string PasswordHasher(string password)
        {
            

            if(password != null) {
                byte[] salt = RandomNumberGenerator.GetBytes(128 / 8); // divide by 8 to convert bits to bytes
                Console.WriteLine($"Salt: {Convert.ToBase64String(salt)}");


                string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: password!,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 100000,
                    numBytesRequested: 256 / 8));
                password = hashed;
            }
            return password;
        }
    }
}


