using BookLibrary.Data;
using BookLibrary.Services.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.Services.Data
{
    public class CategoryService : ICategoryService
    {
        private readonly BookDbContext dbContext;
        public CategoryService(BookDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<IEnumerable<string>> AllCategoryNameAsync()
        {
            IEnumerable<string> allNames = await this.dbContext.Categories.Select(c => c.Name).ToListAsync();
            return allNames;
        }
    }
}
