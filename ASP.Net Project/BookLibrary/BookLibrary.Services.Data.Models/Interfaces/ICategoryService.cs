using BookLibrary.Web.ViewModels.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.Services.Data.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<string>> AllCategoryNameAsync();
        Task<IEnumerable<BookSelectCategoryFromModel>> GetAll();
        Task<bool> ExistsById(int id);
    }
}
