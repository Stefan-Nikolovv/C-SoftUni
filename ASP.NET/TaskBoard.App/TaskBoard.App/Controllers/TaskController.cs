using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using TaskBoard.App.Extentions;
using TaskBoardApp.Data;
using TaskBoardApp.Services.Contracts;
using TaskBoardApp.Web.ViewModels.Board;
using TaskBoardApp.Web.ViewModels.Task;

namespace TaskBoard.App.Controllers
{

    [Authorize]
    public class TaskController : Controller
    {
        private readonly IBoardServices boardServices;
        private readonly ITaskService taskService;

        public TaskController(IBoardServices boardServices, ITaskService taskService)
        {
            this.boardServices = boardServices;
            this.taskService = taskService;
        }
        public async Task<IActionResult> Create()
        {
            TaskFormModel model = new TaskFormModel()
            {
                Board = await
                this.boardServices.AllSelectedAsync()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TaskFormModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Board = await
                this.boardServices.AllSelectedAsync();
                return View(model);
            }
            bool boardExist = await this.boardServices.ExistByIdAsync(model.BoardId);
            if (!boardExist)
            {
                model.Board = await
               this.boardServices.AllSelectedAsync();
                ModelState.AddModelError(nameof(model.BoardId), "Board doesn't exist");
                return View(model);
            }
            string userId = this.User.GetId();

            await this.taskService.AddAsync(userId, model);
            return this.RedirectToAction("All", "Board");
        }
        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            try
            {
                TaskCreateViewModel viewModel = await this.taskService.GetByIdAsync(id);
                return View(viewModel);
            }
            catch (Exception)
            {
                return this.RedirectToAction("All", "Board");
            }
        }
        public async Task<IActionResult> Edit(string id)
        {
            var task = await this.taskService.GetFormByIdAsync(id);

            TaskFormModel taskFormModel = new TaskFormModel()
            {
                Title = task.Title,
                Description = task.Description,
                BoardId = task.BoardId,
                Board = await GetBoards()
            };

            return View(taskFormModel);
        }

        private async Task<IEnumerable<BoardSelectVieModel>> GetBoards()
        {
            return await this.boardServices.AllSelectedAsync();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id , TaskFormModel model)
        {
           
            string userId = this.User.GetId();
            if (!ModelState.IsValid)
            {
                model.Board = await
                this.boardServices.AllSelectedAsync();
                return View(model);
            }
            bool boardExist = await this.boardServices.ExistByIdAsync(model.BoardId);
            if (!boardExist)
            {
                model.Board = await
               this.boardServices.AllSelectedAsync();
                ModelState.AddModelError(nameof(model.BoardId), "Board doesn't exist");
                return View(model);
            }
            try
            {
                await this.taskService.EditFormByIdAsync(id, model);
                return this.RedirectToAction("All", "Board");
            }
            catch (Exception)
            {

                return this.RedirectToAction("All", "Board");
            }
        }
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                TaskViewModel viewModel = await this.taskService.GetByIdAsync(id);
                return View(viewModel);
            }
            catch (Exception)
            {
                return this.RedirectToAction("All", "Board");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(TaskViewModel task)
        {
            try
            {
                await this.taskService.DeleteFormByIdAsync(task.Id);
                
            }
            catch (Exception)
            {
               
            }
            return RedirectToAction("All", "Board");
        }
    }
}
        
    
           
    
