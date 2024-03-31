using BookLibrary.Data;
using BookLibrary.Data.Models;
using BookLibrary.Services.Data.Interfaces;
using BookLibrary.Web.ViewModels.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.Services.Data
{
    public class UserService : IUserService
    {
        private readonly BookDbContext dbContext;
        public UserService(BookDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task EditUserByIdAsync(EditUserProfileFormModel model, string userId)
        {
            ApplicationUser applicationUser = await this.dbContext
                   .Users
                  .FirstAsync(h => h.Id.ToString() == userId);

            if (applicationUser != null)
            {
                applicationUser.FirstName = model.FirstName;
                applicationUser.LastName = model.LastName;
                applicationUser.Email = model.Email;
                if(model.Password != null)
                {
                    applicationUser.PasswordHash = model.Password;
                }
                if(model.ProfilePicture != null)
                {
                    applicationUser.ProfilePicture = model.ProfilePicture;
                }
            }

            await dbContext.SaveChangesAsync();
        }

        public async Task<string> GetFullNameByEmailAsync(string userEmail)
        {
            ApplicationUser? user = await this.dbContext
         .Users
         .FirstOrDefaultAsync(u => u.Email == userEmail);
            if (user == null)
            {
                return string.Empty;
            }

            return $"{user.FirstName} {user.LastName}";
        }

        public async Task<string> GetFullNameByIdAsync(string userId)
        {
            ApplicationUser? user = await this.dbContext
                .Users
                .FirstOrDefaultAsync(u => u.Id.ToString() == userId);
            if (user == null)
            {
                return string.Empty;
            }

            return $"{user.FirstName} {user.LastName}";
        }

        public async Task<MineUserViewModel> GetMineUser(string Id)
        {
            ApplicationUser applicationUser = await this.dbContext
                  .Users
                 .FirstAsync(h => h.Id.ToString() == Id);


            return new MineUserViewModel()
            {
                FirstName = applicationUser.FirstName,
                LastName = applicationUser.LastName,
                ProfilePicture = applicationUser.ProfilePicture,
            };
        }

        public async Task<EditUserProfileFormModel> GetUserByIdForEditAsync(string Id)
        {
            ApplicationUser applicationUser = await this.dbContext
                  .Users
                 .FirstAsync(h => h.Id.ToString() == Id);

            return new EditUserProfileFormModel()
            {
                FirstName = applicationUser.FirstName,
                LastName = applicationUser.LastName,
                ProfilePicture = applicationUser.ProfilePicture,
                Email = applicationUser.Email
            };
        }
    }
}
