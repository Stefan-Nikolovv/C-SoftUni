using HouseRentingSystem.Web.Infrastructure.Extentions;
using HouseRentingSystem.Web.ViewModels.Agent;
using HouseRentingSystems.Services.Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace HouseRentingSystem.Controllers
{
    [Authorize]
    public class AgentController : Controller
    {
        private readonly IAgentService agentService;

        public AgentController(IAgentService agentService)
        {
            this.agentService = agentService;
        }
        [HttpGet]
        public async Task<IActionResult> Become()
        {
            string? userId = this.User.GetId();
            bool isAgent = await agentService.AgentExistsByUserId(userId);
            if(isAgent)
            {
                return this.RedirectToAction("Index", "Home");
            }

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Become(BecomeAgentFromModel model)
        {
            string? userId = this.User.GetId();
            bool isAgent = await agentService.AgentExistsByUserId(userId);
            if (isAgent)
            {
                return this.RedirectToAction("Index", "Home");
            }

            bool isPhoneNumberTaken = await this.agentService.AgentExistsPhoneNumberAsync(model.PhoneNumber);
            if (isPhoneNumberTaken) 
            {
                ModelState.AddModelError(nameof(model.PhoneNumber), "Agent with this number is already exists!");
            }
            if (!this.ModelState.IsValid)
            {
                return View(model);
            }

            bool userHasRents = await this.agentService.UserHasRentsAsync(userId);
            if (userHasRents)
            {
                return this.RedirectToAction("Index", "Home");
            }

            try
            {
                await this.agentService.Create(userId, model);
            }
            catch (Exception)
            {

                throw;
            }
            return RedirectToAction("All", "House");
        }

    }
}
