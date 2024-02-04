using HouseRentingSystem.Services.Data.Models.House;
using HouseRentingSystem.Web.ViewModels.Home;
using HouseRentingSystem.Web.ViewModels.House;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseRentingSystems.Services.Data.Interfaces
{
    public interface IHouseService
    {
        Task<IEnumerable<HouseIndexViewModel>>  GetLastThreeHouseAsync();
        Task<IEnumerable<HouseIndexViewModel>> GetAllHouseAsync();
        Task CreateAsync(HouseFormModel model, string agentId);

        Task<AllHousesFilteredAndPagedServiceModel> AllAsync(AllHousesQueryModel queryModel);
        Task<IEnumerable<HouseAllViewModel>> AllByAgentIdAsync(string agentId);
        Task<IEnumerable<HouseAllViewModel>> AllByUserIdAsync(string userId);
        Task<HouseDetailsViewModel?> GetHouseDetailsAsync(string houseId);
    } 
}
