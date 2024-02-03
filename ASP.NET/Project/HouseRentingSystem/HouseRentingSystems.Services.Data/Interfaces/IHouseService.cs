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
    } 
}
