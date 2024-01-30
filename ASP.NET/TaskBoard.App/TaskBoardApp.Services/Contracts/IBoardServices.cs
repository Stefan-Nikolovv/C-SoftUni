using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskBoardApp.Web.ViewModels.Board;
using TaskBoardApp.Web.ViewModels.Task;

namespace TaskBoardApp.Services.Contracts
{
    public interface IBoardServices
    {
        Task<IEnumerable<BoardAllViewModel>> allBoardsAsnc();
        Task<IEnumerable<BoardSelectVieModel>> AllSelectedAsync();
        Task<bool> ExistByIdAsync(int id);
        
    }
}
