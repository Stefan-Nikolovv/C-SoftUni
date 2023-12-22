using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskBoardApp.Data;
using TaskBoardApp.Services.Contracts;
using TaskBoardApp.Web.ViewModels.Task;

namespace TaskBoard.App.Controllers
{
    //private readonly
    [Authorize]
    public class TaskController : Controller
    {
        private readonly IBoardServices boardServices;

        public TaskController(IBoardServices boardServices)
        {
            this.boardServices = boardServices;
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
         
    }
}
        
    
           
    
