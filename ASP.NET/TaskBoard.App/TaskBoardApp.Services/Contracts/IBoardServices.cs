using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskBoardApp.Web.ViewModels.Board;

namespace TaskBoardApp.Services.Contracts
{
    public interface IBoardServices
    {
        Task<IEnumerable<BoardAllViewModel>> allBoardsAsnc();
        Task<IEnumerable<BoardSelectVieModel>> AllSelectedAsync();
    }
}
