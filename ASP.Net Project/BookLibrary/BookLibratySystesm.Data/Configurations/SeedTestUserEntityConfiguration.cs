using BookLibrary.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibratySystesm.Data.Configurations
{
    public class SeedTestUserEntityConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            return;
            builder.HasData(
             new ApplicationUser
             {
                 Id = Guid.Parse("86e01957-e0c1-446c-adcc-183c15c8a1dd"),
                 FirstName = "John",
                 LastName = "Doe",
                 ProfilePicture = "john_doe.jpg"
             },
             new ApplicationUser
             {
                 Id = Guid.Parse("4656d230-5678-4a18-8c1b-332fc5937411"),
                 FirstName = "Jane",
                 LastName = "Doe",
                 ProfilePicture = "jane_doe.jpg"
             }
         );
        }

        
    }
}
