using HouseRentingSystem.Web.ViewModels.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseRentingSystems.Services.Data.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<HouseSelectCategoryFromModel>> GetAll();
        Task<bool> ExistsById(int id);
        Task<IEnumerable<string>> AllCategoryNameAsync();
    }
}
