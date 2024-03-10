using HouseRentingSystem.Data;
using HouseRentingSystem.Web.ViewModels.User;


namespace HouseRentingSystems.Services.Data.Interfaces
{

    public interface IUserService
    {
        Task<string> GetFullNameByEmailAsync(string email);

        Task<string> GetFullNameByIdAsync(string userId);

        Task<IEnumerable<UserViewModel>> AllAsync();
    }
}
