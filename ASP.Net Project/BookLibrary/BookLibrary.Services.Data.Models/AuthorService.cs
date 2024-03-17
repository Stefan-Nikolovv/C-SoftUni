using BookLibrary.Data;
using BookLibrary.Data.Models;
using BookLibrary.Services.Data.Interfaces;
using BookLibrary.Web.ViewModels.Author;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BookLibrary.Common.EntityValidationsConstants;

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

        public async Task<bool> AuthorExistsPhoneNumberAsync(string phoneNumber)
        {
            bool result = await this.dbContext
                    .Authors
                    .AnyAsync(a => a.PhoneNumber == phoneNumber);

            return result;
        }

        public async Task Create(string userId, BecomeAuthorFormModel model)
        {
            BookLibrary.Data.Models.Author author = new BookLibrary.Data.Models.Author()
            { 
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                UserId = Guid.Parse(userId)
            };
            await this.dbContext.Authors.AddAsync(author);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<string?> GetAuthorIdByUserIdAsync(string userId)
        {
            BookLibrary.Data.Models.Author? agentId = await this.dbContext.Authors.FirstOrDefaultAsync(a => a.UserId.ToString() == userId);
            if (agentId == null)
            {
                return null;
            }
            return agentId.Id.ToString();
        }

        public async Task<bool> UserHasLikesAsync(string userId)
        {
            ApplicationUser? result = await this.dbContext
                  .Users
                    .FirstOrDefaultAsync(a => a.Id.ToString() == userId);

            if (result == null)
            {
                return false;
            }

            return result.LikedBooks.Any();
        }
    }
}
