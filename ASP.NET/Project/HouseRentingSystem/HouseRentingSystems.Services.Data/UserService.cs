using HouseRentingSystem.Data;
using HouseRentingSystem.Data.Models;
using HouseRentingSystem.Web.ViewModels.User;
using HouseRentingSystems.Services.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseRentingSystems.Services.Data
{
    public class UserService : IUserService
    {
        private readonly HouseRentingDbContext dbContext;

        public UserService(HouseRentingDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<UserViewModel>> AllAsync()
        {
       
         
            List<UserViewModel> allUsers = await this.dbContext
               .Users
               .Select (u => new UserViewModel
               {
                   Id = u.Id.ToString(),
                   Email = u.Email,
                   PhoneNumber = u.PhoneNumber,
                   FullName = u.FirstName + " " + u.LastName,
               })
               .ToListAsync();

            foreach (UserViewModel user in allUsers)
            {
                Agent? aggent = await this.dbContext
                    .Agents
                    .FirstOrDefaultAsync(a => a.UserId.ToString() == user.Id);

                if (aggent != null)
                {
                    user.PhoneNumber = aggent.PhoneNumber;
                }
                else
                {
                    user.PhoneNumber =string.Empty;
                }
            }

            return allUsers;
        }
            

        public async Task<string> GetFullNameByEmailAsync(string email)
        {
            User? user = await this.dbContext.Users
                .FirstOrDefaultAsync(x => x.Email == email);

            if (user == null)
            {
                return string.Empty;
            }

            return $"{user.FirstName} {user.LastName}";
        }

        public async Task<string> GetFullNameByIdAsync(string userId)
        {
            User? user = await this.dbContext.Users
                .FirstOrDefaultAsync(x => x.Id.ToString() == userId);

            if (user == null)
            {
                return string.Empty;
            }

            return $"{user.FirstName} {user.LastName}";
        }
    }
}
