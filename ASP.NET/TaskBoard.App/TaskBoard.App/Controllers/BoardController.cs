using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskBoardApp.Services.Contracts;
using TaskBoardApp.Web.ViewModels.Board;

namespace TaskBoard.App.Controllers
{
    [Authorize]
    public class BoardController : Controller
    {
        private readonly IBoardServices boardServices;
        public BoardController(IBoardServices boardServices) 
        {
            this.boardServices = boardServices;
        }

        public async Task<IActionResult> All()
        {
            IEnumerable<BoardAllViewModel> allBoards =
                await this.boardServices.allBoardsAsnc();
            return View(allBoards);
        }
    }
}
