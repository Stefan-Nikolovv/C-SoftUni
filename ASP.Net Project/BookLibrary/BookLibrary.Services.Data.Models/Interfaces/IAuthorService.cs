using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.Services.Data.Interfaces
{
    public interface IAuthorService
    {
        Task<bool> AuthorExistsByUserId(string userId);
        Task<string?> GetAuthorIdByUserIdAsync(string userId);
   
    }
}
