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

              string id =  await this.houseService.CreateAndReturnIdAsync(model, agentId);
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
            bool houseExists = await this.houseService.ExistByIdAsync(id);
            
            if(!houseExists)
            {
                return RedirectToAction("All", "House");
            }
            HouseDetailsViewModel model = await this.houseService.GetHouseDetailsAsync(id);
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            bool houseExists = await this.houseService.ExistByIdAsync(id);

            if (!houseExists)
            {
                return RedirectToAction("All", "House");
            }
            bool isUserAgent = await this.agentService.AgentExistsByUserId(this.User.GetId());

            if (!isUserAgent)
            {
                return RedirectToAction("Become", "Agent");
            }
            string agentId = await this.agentService.GetAgentIdByUserIdAsync(this.User.GetId());

            bool isAgentOwner = await this.houseService
                .isAgentWithIdOwnerOfHouseWithIdAsync(id, agentId);

            if (!isAgentOwner)
            {
                return RedirectToAction("Become", "Agent");
            }
            HouseFormModel houseForm = await this.houseService.GetHouseForEditByIdAsync(id);
            houseForm.Categories = await this.categoryService.GetAll();
            return View(houseForm);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            bool houseExists = await this.houseService.ExistByIdAsync(id);

            if (!houseExists)
            {
                return RedirectToAction("All", "House");
            }
            bool isUserAgent = await this.agentService.AgentExistsByUserId(this.User.GetId());

            if (!isUserAgent)
            {
                return RedirectToAction("Become", "Agent");
            }
            string agentId = await this.agentService.GetAgentIdByUserIdAsync(this.User.GetId());

            bool isAgentOwner = await this.houseService
                .isAgentWithIdOwnerOfHouseWithIdAsync(id, agentId);

            if (!isAgentOwner)
            {
                return RedirectToAction("Become", "Agent");
            }

            try
            {
                HouseDeleteDetailsViewModel deleteDetailsViewModel = await this.houseService.GetHouseToDeleteHouseByIdAsync(id);
                return this.View(deleteDetailsViewModel);
            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id, HouseDeleteDetailsViewModel model)
        {
            bool houseExists = await this.houseService.ExistByIdAsync(id);

            if (!houseExists)
            {
                return RedirectToAction("All", "House");
            }
            bool isUserAgent = await this.agentService.AgentExistsByUserId(this.User.GetId());

            if (!isUserAgent)
            {
                return RedirectToAction("Become", "Agent");
            }
            string agentId = await this.agentService.GetAgentIdByUserIdAsync(this.User.GetId());

            bool isAgentOwner = await this.houseService
                .isAgentWithIdOwnerOfHouseWithIdAsync(id, agentId);

            if (!isAgentOwner)
            {
                return RedirectToAction("Become", "Agent");
            }

            try
            {
                  await this.houseService.GetHouseByIdAndDelete(id);
                return RedirectToAction("Mine", "House");
            }
            catch (Exception)
            {

                throw;
            }

        }
        [HttpPost]
        public async Task<IActionResult> Edit(string id, HouseFormModel model)
        {
            bool houseExists = await this.houseService.ExistByIdAsync(id);

            if (!houseExists)
            {
                return RedirectToAction("All", "House");
            }
            bool isUserAgent = await this.agentService.AgentExistsByUserId(this.User.GetId());

            if (!isUserAgent)
            {
                return RedirectToAction("Become", "Agent");
            }
            string agentId = await this.agentService.GetAgentIdByUserIdAsync(this.User.GetId());

            bool isAgentOwner = await this.houseService
                .isAgentWithIdOwnerOfHouseWithIdAsync(id, agentId);

            if (!isAgentOwner)
            {
                return RedirectToAction("Become", "Agent");
            }

            if (!ModelState.IsValid)
            {
                model.Categories = await this.categoryService.GetAll();
                return this.View(model);
            }
            try
            {
                await this.houseService.EditHouseByIdAsync( id, model);
            }
            catch (Exception)
            {

                ModelState.AddModelError(string.Empty, "Unexptected Error while trying to edit this house try again later!");
            }
            return RedirectToAction("Details", "House",new { id });
        }
        [HttpPost]
        public async Task<IActionResult> Rent(string id)
        {
            bool houseExists = await houseService.ExistByIdAsync(id);
            if (!houseExists)
            {
                

                return RedirectToAction("All", "House");
            }

            bool isHouseRented = await houseService.IsRentedAsync(id);
            if (isHouseRented)
            {
                

                return RedirectToAction("All", "House");
            }

            bool isUserAgent =
                await agentService.AgentExistsByUserId(User.GetId()!);
            if (isUserAgent)
            {
              

                return RedirectToAction("Index", "Home");
            }

            try
            {
                await houseService.RentHouseAsync(id, User.GetId()!);
            }
            catch (Exception)
            {
            
            }

           

            return RedirectToAction("Mine", "House");
        }




        [HttpPost]
        public async Task<IActionResult> Leave(string id)
        {
            bool houseExists = await houseService.ExistByIdAsync(id);
            if (!houseExists)
            {
                

                return RedirectToAction("All", "House");
            }

            bool isHouseRented = await houseService.IsRentedAsync(id);
            if (!isHouseRented)
            {
              

                return RedirectToAction("Mine", "House");
            }

            bool isCurrentUserRenterOfTheHouse =
                await houseService.IsRentedByUserWithIdAsync(id, User.GetId()!);
            if (!isCurrentUserRenterOfTheHouse)
            {
                

                return RedirectToAction("Mine", "House");
            }

            try
            {
                await houseService.LeaveHouseAsync(id);
            }
            catch (Exception)
            {

            }
                

       

            return RedirectToAction("Mine", "House");
        }

    }
}
