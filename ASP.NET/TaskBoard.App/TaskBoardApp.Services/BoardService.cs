using Microsoft.EntityFrameworkCore;
using TaskBoardApp.Data;
using TaskBoardApp.Services.Contracts;
using TaskBoardApp.Web.ViewModels.Board;
using TaskBoardApp.Web.ViewModels.Task;

namespace TaskBoardApp.Services
{
    public class BoardService : IBoardServices
    {
        private readonly TaskBoardDbContext dbContext;

        public BoardService(TaskBoardDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<IEnumerable<BoardAllViewModel>> allBoardsAsnc()
        {
            IEnumerable<BoardAllViewModel> boardAlls = await this.dbContext
                 .Boards.Select(board => new BoardAllViewModel()
                 {
                     Name = board.Name,
                     Tasks = board.Tasks
                     .Select(t => new TaskViewModel()
                     {
                         Id = t.Id.ToString(),
                         Title = t.Title,
                         Description = t.Description,
                         Owner = t.Owner.UserName
                     }).ToArray(),
                 }).ToArrayAsync();

            return boardAlls;
        }

        public async Task<IEnumerable<BoardSelectVieModel>> AllSelectedAsync()
        {
            IEnumerable<BoardSelectVieModel> boardSelectVieModels = await this.dbContext
                .Boards
                .Select(b => new BoardSelectVieModel()
                {
                    Id = b.Id,
                    Name = b.Name,
                })
                .ToArrayAsync();    

            return boardSelectVieModels;
        }
    }
}