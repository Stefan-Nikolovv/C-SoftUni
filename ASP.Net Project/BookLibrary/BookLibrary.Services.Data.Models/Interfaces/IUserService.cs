using BookLibrary.Web.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.Services.Data.Interfaces
{
    public interface IUserService
    {
        Task<MineUserViewModel> GetMineUser(string Id);
        Task<EditUserProfileFormModel> GetUserByIdForEditAsync(string Id);
        Task EditUserByIdAsync(EditUserProfileFormModel model, string userId);
    }
}
