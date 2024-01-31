using HouseRentingSystem.Web.ViewModels.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseRentingSystems.Services.Data.Interfaces
{
    internal interface IHouseServices
    {
        Task<IEnumerable<HouseIndexViewModel>>  GetLastThreeHouseAsync();
    }
}
