using Microsoft.AspNetCore.Mvc;
using SeminarHub.Areas.Contracts;
using SeminarHub.Models;

namespace SeminarHub.Controllers
{
    public class SeminarController : BaseController
    {
        private readonly ISeminarService seminarService;

        public SeminarController(ISeminarService seminarService)
        {
            this.seminarService = seminarService;
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
          
            var model = await this.seminarService.GetSeminarCategoriesToAddedWithTypesAsync();
            
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddSeminarViewModel model)
        {
            string userId = GetUserId();

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(nameof(model), string.Empty);


                var cat = await this.seminarService.GetSeminarCategoriesToAddedWithTypesAsync();


                return View(cat);
            }

            try
            {
                await this.seminarService.AddSeminarAsync(model, userId);
                return RedirectToAction("All", "Seminar");
            }
            catch (Exception e)
            {
                return RedirectToAction("All", "Seminar");

            }
        }
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var model = await this.seminarService.GetAllSerminarsAsync();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Join(int Id)
        {
            string userId = GetUserId();

            bool isSeminarExists = await this.seminarService.GetSeminarIfExists(Id);
            bool isSeminarIsJoined = await this.seminarService.GetSeminarIsJoinedByTheUserAsync(userId, Id);
            if (!isSeminarExists)
            {
                return RedirectToAction("All", "Seminar");
            }
            if (isSeminarIsJoined)
            {
                return RedirectToAction("Joined", "Seminar");
            }

            try
            {
                await this.seminarService.AddSeminarToUserByIdAsync(userId, Id);
                return RedirectToAction("Joined", "Seminar");
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        public async Task<IActionResult> Leave(int Id)
        {
            string userId = GetUserId();

            bool isSeminarExists = await this.seminarService.GetSeminarIfExists(Id);
            if (!isSeminarExists)
            {
                return RedirectToAction("All", "Seminar");
            }

            bool isSeminarIsJoined = await this.seminarService.GetSeminarIsJoinedByTheUserAsync(userId, Id);

            if (!isSeminarIsJoined)
            {
                return RedirectToAction("Joined", "Seminar");
            }
            try
            {
                await this.seminarService.RemoveSeminarOfUserColletionByIdAsync(userId, Id);
                return RedirectToAction("Joined", "Seminar");
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet]
        public async Task<IActionResult> Joined()
        {
            string userId = GetUserId();

            var model = await this.seminarService.GetAllSeminarsJoinedByUserIdAsync(userId);

            if (model == null)
            {
                ModelState.AddModelError(nameof(model), string.Empty);

            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            string userId = GetUserId();
            bool isOwnerOfSeminar = await this.seminarService.GetIsUserIsCreatorOfSeminarByIdAsync(userId, Id);
            bool isSeminarExists = await this.seminarService.GetSeminarIfExists(Id);
            if (!isSeminarExists)
            {
                return RedirectToAction("All", "Seminar");
            }

            if(!isOwnerOfSeminar)
            {
                return RedirectToAction("All", "Seminar");
            }

            var model = await this.seminarService.GetSeminarForEditByIdAsync(Id);

            if (model == null)
            {
                return BadRequest();
            }
            return View(model);
            
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditSeminarViewModel model)
        {
            if (ModelState.IsValid)
            {
                ModelState.AddModelError(nameof(model), "Something went wrong");
            }
            bool isSeminarExists = await this.seminarService.GetSeminarIfExists(model.Id);
            if (!isSeminarExists)
            {
                return RedirectToAction("All", "Seminar");
            }
            await this.seminarService.EditSeminarAsync(model);
            return RedirectToAction("All", "Seminar");
        }
        [HttpGet]
        public async Task<IActionResult> Details(int Id)
        {
            bool isSeminarExists = await this.seminarService.GetSeminarIfExists(Id);
            if (!isSeminarExists)
            {
                return RedirectToAction("All", "Seminar");
            }
            var model = await this.seminarService.GetDetailsFoeSeminarByIdAsync(Id);

            if(model == null)
            {
                return RedirectToAction("All", "Seminar");
            }
            return View(model);
        }
        public async Task<IActionResult> Delete(int Id)
        {
            string userId = GetUserId();
            bool isOwnerOfSeminar = await this.seminarService.GetIsUserIsCreatorOfSeminarByIdAsync(userId, Id);
            bool isSeminarExists = await this.seminarService.GetSeminarIfExists(Id);
            if (!isSeminarExists)
            {
                return RedirectToAction("All", "Seminar");
            }

            if (!isOwnerOfSeminar)
            {
                return RedirectToAction("All", "Seminar");
            }


            try
            {
                await this.seminarService.DeleteSeminarByIdAsync(Id);
                return RedirectToAction("All", "Seminar");
            }
            catch (Exception)
            {

                return RedirectToAction("All", "Seminar");
            }
        }
    }
}
