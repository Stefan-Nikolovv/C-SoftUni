using Homies.Areas.Contracts;
using Homies.Data;
using Homies.Models;
using Microsoft.AspNetCore.Mvc;

namespace Homies.Controllers
{
    public class EventController : BaseController
    {
        private readonly IEventService eventService;

        public EventController(IEventService eventService)
        {
            this.eventService = eventService;
        }
        public async Task<IActionResult> All()
        {
             var allEvents = await eventService.GetAllEventsAsync();


            return View(allEvents);
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = await this.eventService.GetEvenetsToAddedWithTypesAsync();


            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddEventViewModel model)
        {
            string userId = GetUserId();
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(nameof(model), string.Empty);
            }
            await this.eventService.AddEventAsync(userId, model);
            return RedirectToAction("All", "Event");

        }
    }
}
