using HouseRentingSystem.Data;
using HouseRentingSystem.Web.ViewModels.Home;
using HouseRentingSystems.Services.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseRentingSystems.Services.Data
{
    public class HouseService : IHouseService
    {
        private readonly HouseRentingDbContext dbContext;

        public HouseService(HouseRentingDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<HouseIndexViewModel>> GetAllHouseAsync()
        {
            IEnumerable<HouseIndexViewModel> result = await this.dbContext.Houses.Select(x => new HouseIndexViewModel
            {
                Id = x.Id.ToString(),
                Title = x.Title,
                ImageUrl = x.ImageUrl,
            }).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<HouseIndexViewModel>> GetLastThreeHouseAsync()
        {
            IEnumerable<HouseIndexViewModel> result = await this.dbContext
                                                                .Houses
                                                                .OrderByDescending(x => x.CreatedOn)
                                                                .Take(3)
                                                                .Select(x => new HouseIndexViewModel
                                                                {
                                                                    Id = x.Id.ToString(),
                                                                    Title = x.Title,
                                                                    ImageUrl = x.ImageUrl,
                                                                })
                                                                .ToArrayAsync();
            return result;
        }
    }
}
