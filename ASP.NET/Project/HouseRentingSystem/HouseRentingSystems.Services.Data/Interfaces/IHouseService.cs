using HouseRentingSystem.Services.Data.Models.House;
using HouseRentingSystem.Services.Data.Models.Statistics;
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
        Task<string> CreateAndReturnIdAsync(HouseFormModel model, string agentId);

        Task<AllHousesFilteredAndPagedServiceModel> AllAsync(AllHousesQueryModel queryModel);
        Task<IEnumerable<HouseAllViewModel>> AllByAgentIdAsync(string agentId);
        Task<IEnumerable<HouseAllViewModel>> AllByUserIdAsync(string userId);
        Task<HouseDetailsViewModel> GetHouseDetailsAsync(string houseId);
        Task<bool> ExistByIdAsync(string houseId);
        Task<HouseFormModel> GetHouseForEditByIdAsync(string houseId);
        Task<bool> isAgentWithIdOwnerOfHouseWithIdAsync(string houseId, string ownerId);

        Task EditHouseByIdAsync(string id, HouseFormModel model);
        Task<HouseDeleteDetailsViewModel> GetHouseToDeleteHouseByIdAsync(string id);
        Task GetHouseByIdAndDelete(string id);
        Task<StatisticsServiceModel> GetStatisctisForHouses();
       
    } 
}
