using HouseRentingSystem.Areas.Admin.ViewModels.House;
using HouseRentingSystem.Web.Infrastructure.Extentions;
using HouseRentingSystems.Services.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HouseRentingSystem.Areas.Admin.Controllers
{
    public class HouseController : BaseAdminController
    {
        private readonly IAgentService agentService;
        private readonly IHouseService houseService;

        public HouseController(IAgentService agentService, IHouseService houseService)
        {
            this.agentService = agentService;
            this.houseService = houseService;
        }
        public async Task<IActionResult> Mine()
        {
            string? agentId = await this.agentService.GetAgentIdByUserIdAsync(this.User.GetId()!);  
            MyHousesViewModel viewModel = new MyHousesViewModel()
            {
                AddedHouses = await this.houseService.AllByAgentIdAsync(agentId!),
                RentedHouses = await this.houseService.AllByUserIdAsync(this.User.GetId()!),
            };
            return View(viewModel);
        }
    }
}
