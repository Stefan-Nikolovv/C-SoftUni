using HouseRentingSystem.Data;
using HouseRentingSystem.Web.ViewModels.Category;
using HouseRentingSystems.Services.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseRentingSystems.Services.Data
{
    public class CategoryService : ICategoryService
    {
        private readonly HouseRentingDbContext dbContext;

        public CategoryService(HouseRentingDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<string>> AllCategoryNameAsync()
        {
            IEnumerable<string> allNames = await this.dbContext.Categories.Select(c => c.Name).ToListAsync();
            return allNames;
        }

        public async Task<bool> ExistsById(int id)
        {
           bool result = await dbContext.Categories.AnyAsync(c => c.Id == id);
            return result;
        }

        public async Task<IEnumerable<HouseSelectCategoryFromModel>> GetAll()
        {
            IEnumerable<HouseSelectCategoryFromModel> result = await this.dbContext
                                                                .Categories
                                                                .Select(x => new HouseSelectCategoryFromModel()
                                                                {
                                                                    Id = x.Id,
                                                                    Name = x.Name,
                                                                })
                                                                .ToArrayAsync();

            return result;
        }
    }
}
