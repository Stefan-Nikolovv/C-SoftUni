using Forum.Services.Contracts;
using Forum.ViewModels.Post;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace Forum.App.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService postService;

        public PostController(IPostService postService)
        {
            this.postService = postService;
        }
        public async Task<IActionResult> All()
        {
            IEnumerable<PostListViewModel> allPosts = 
                await this.postService.ListAllAsync();

            return View(allPosts);
        }
     
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(PostFormModel modul)
        {

            if (!ModelState.IsValid)
            {
                return View(modul);
            }
            try
            {
                await this.postService.addPostAsync(modul);
            }
            catch (Exception )
            {
                ModelState.AddModelError(string.Empty, "Unexpected error while saving you content");
                return View(modul);
            }

            return RedirectToAction("All");
        }
        public async Task<IActionResult> Edit(string id)
        {
            try
            {
                PostFormModel postModel =
                    await this.postService.EditPostModelAsync(id);

                return View(postModel);
            }
            catch (Exception)
            {
                return this.RedirectToAction("All", "Post");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(string id, PostFormModel post)
        {
            if (!ModelState.IsValid)
            {
                return View(post);
            }
            try
            {
                await this.postService.EditByIdAsync(id, post);
            }
            catch(Exception)
            {
                ModelState.AddModelError(string.Empty, "Someting whent wrong when you editing this post!");
                return View(post);
            }
            return RedirectToAction("All", "Post");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await postService.DeleteByIdAsync(id);

            }
            catch (Exception)
            {

                
            }
            return RedirectToAction("All", "Post");

        }
    }
}
