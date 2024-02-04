using HouseRentingSystem.Services.Data.Models.House;
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
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> All([FromQuery] AllHousesQueryModel queryModel)
        {
            AllHousesFilteredAndPagedServiceModel serviceModel = await this.houseService.AllAsync(queryModel);

            queryModel.Houses = serviceModel.Houses;
            queryModel.TotalHouses = serviceModel.TotalHousesCount;
            queryModel.Categories = await this.categoryService.AllCategoryNameAsync();

            return View(queryModel);
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
        [HttpGet]
        public async Task<IActionResult> Mine()
        {
            List<HouseAllViewModel> myHouses = new List<HouseAllViewModel>();

            string userId = this.User.GetId();
            bool isUserIsAgent = await this.agentService.AgentExistsByUserId(userId);

            if (isUserIsAgent)
            {
                string agentId = await this.agentService.GetAgentIdByUserIdAsync(userId);

                myHouses.AddRange(await this.houseService.AllByAgentIdAsync(agentId));
            }
            else
            {
                myHouses.AddRange(await this.houseService.AllByUserIdAsync(userId));
            }
            return View(myHouses);
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(string id)
        {
            HouseDetailsViewModel? model = await this.houseService.GetHouseDetailsAsync(id);
            if(model == null)
            {
                return RedirectToAction("All", "House");
            }
            return View(model);
        }
    }
}
