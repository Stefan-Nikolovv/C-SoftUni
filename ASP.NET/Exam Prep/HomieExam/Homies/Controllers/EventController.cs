using Homies.Areas.Contracts;
using Homies.Data;
using Homies.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;

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
        [HttpGet]
        public async Task<IActionResult> Joined()
        {
            string userId = GetUserId();

            var model = await this.eventService.GetJoinedEventsByIdAsync(userId);

            
            

            if (model == null)
            {
                ModelState.AddModelError(nameof (model), string.Empty); 

            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Join(int id)
        {
            string userId = GetUserId();
           
            bool isEvenetExists = await this.eventService.GetEventIfExists(id);
            bool isEventIsJoined = await this.eventService.GetEventIsJoinedByTheUserAsync(userId, id);

            if (!isEvenetExists)
            {
                return BadRequest();
            }

            if(isEventIsJoined)
            {
                return RedirectToAction("Join", "Events");
            }

            await this.eventService.AddEventToUserByIdAsync(userId, id);

            return RedirectToAction("Joined", "Events");
        }

        [HttpPost]
        public async Task<IActionResult> Leave(int Id)
        {
            string userId = GetUserId();

            bool isEvenetExists = await this.eventService.GetEventIfExists(Id);
            if (!isEvenetExists)
            {
                return BadRequest();
            }
            bool isEventIsJoined = await this.eventService.GetEventIsJoinedByTheUserAsync(userId, Id);

            if (!isEventIsJoined)
            {
                return RedirectToAction("Join", "Events");
            }

            await this.eventService.RemoveEventToUserByIdAsync(userId, Id);

            return RedirectToAction("All", "Event");
        }

        
        public async Task<IActionResult> Details(int Id)
        {
            

            bool isEvenetExists = await this.eventService.GetEventIfExists(Id);
            if (!isEvenetExists)
            {
                return BadRequest();
            }
            var model = await this.eventService.GetDetailsEventByIdAsync(Id);

            if (model == null)
            {
                return BadRequest();
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int Id)
        {
            bool isEvenetExists = await this.eventService.GetEventIfExists(Id);
            if (!isEvenetExists)
            {
                return BadRequest();
            }

            var model = await this.eventService.GetEventForEditByIdAsync(Id);


            if (model == null)
            {
                return BadRequest();
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditEventViewModel model)
        {
            if (ModelState.IsValid)
            {
                ModelState.AddModelError(nameof(model), "Something went wrong");
            }
            bool isEvenetExists = await this.eventService.GetEventIfExists(model.Id);
           
            if (!isEvenetExists)
            {
                return BadRequest();
            }

            await this.eventService.EditEventAsync(model);

            return RedirectToAction("All", "Event");

        }
    }
}
