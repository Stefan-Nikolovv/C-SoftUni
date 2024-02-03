using HouseRentingSystem.Web.Infrastructure.Extentions;
using HouseRentingSystem.Web.ViewModels.House;
using HouseRentingSystems.Services.Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HouseRentingSystem.Controllers
{
    [Authorize]
    public class HouseController : Controller
    {

        private readonly ICategoryService categoryService;
        private readonly IAgentService agentService;
        private readonly IHouseService houseService;
        public HouseController(ICategoryService categoryService, IAgentService agentService, IHouseService houseService)
        {
            this.categoryService = categoryService;
            this.agentService = agentService;
            this.houseService = houseService;

        }
        [AllowAnonymous]
        public async Task<IActionResult> All()
        {

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            
            bool isAgent = await this.agentService.AgentExistsByUserId(this.User.GetId()!);

            if (!isAgent)
            {
                return RedirectToAction("Become", "Agent");
            }
            HouseFormModel formModel = new HouseFormModel()
            {
                Categories = await this.categoryService.GetAll()
            };


            return View(formModel);
        }
        [HttpPost]
        public async Task<IActionResult> Add(HouseFormModel model)
        {
            bool existCategory = await this.categoryService.ExistsById(model.CategoryId);
            bool isAgent = await this.agentService.AgentExistsByUserId(this.User.GetId()!);

            if (!isAgent)
            {
                return RedirectToAction("Become", "Agent");
            }
            
            if (!existCategory)
            {
                ModelState.AddModelError(nameof(model.CategoryId), "Selected Category does not exists!");
            }
            if(!ModelState.IsValid)
            {
                model.Categories = await this.categoryService.GetAll();


                return View(model);
            }
            try
            {
                string agentId = await this.agentService.GetAgentIdByUserIdAsync(this.User.GetId());
                await this.houseService.CreateAsync(model, agentId);
            }
            catch (Exception)
            {

                this.ModelState.AddModelError(string.Empty, "Unxprcted error occurred while you are add house");
                model.Categories = await this.categoryService.GetAll();
                return View(model);
            }
            return this.RedirectToAction("All", "House");
        }
    }
}
