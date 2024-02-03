using HouseRentingSystem.Data;
using HouseRentingSystem.Data.Models;
using HouseRentingSystem.Web.ViewModels.Home;
using HouseRentingSystem.Web.ViewModels.House;
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

        public async Task CreateAsync(HouseFormModel model, string agentId)
        {
            House modelForm = new House()
            {
                Title = model.Title,
                Address = model.Address,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                PricePerMonth = model.PricePerMonth,
                CategoryId  = model.CategoryId,
                AgentId = Guid.Parse(agentId),
               
            };
            await this.dbContext.Houses.AddAsync(modelForm);
             await dbContext.SaveChangesAsync();
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
