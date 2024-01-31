using HouseRentingSystem.Data;
using HouseRentingSystem.Web.ViewModels.Home;
using HouseRentingSystems.Services.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseRentingSystems.Services.Data
{
    public class HouseService : IHouseServices
    {
        private readonly HouseRentingDbContext dbContext;

        public HouseService(HouseRentingDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public Task<IEnumerable<HouseIndexViewModel>> GetLastThreeHouseAsync()
        {
            throw new NotImplementedException();
        }
    }
}
