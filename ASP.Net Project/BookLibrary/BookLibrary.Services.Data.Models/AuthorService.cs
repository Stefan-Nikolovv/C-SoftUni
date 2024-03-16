using BookLibrary.Data;
using BookLibrary.Data.Models;
using BookLibrary.Services.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.Services.Data
{
    public class AuthorService : IAuthorService
    {
        private readonly BookDbContext dbContext;
        public AuthorService(BookDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
    
        public async Task<bool> AuthorExistsByUserId(string userId)
        {
            bool result = await this.dbContext
                 .Authors
                 .AnyAsync(a => a.UserId.ToString() == userId);

            return result;
        }


        public async Task<string?> GetAuthorIdByUserIdAsync(string userId)
        {
            Author? authorId = await this.dbContext.Authors.FirstOrDefaultAsync(a => a.UserId.ToString() == userId);
            if (authorId == null)
            {
                return null;
            }
            return authorId.Id.ToString();
        }
    }
}
