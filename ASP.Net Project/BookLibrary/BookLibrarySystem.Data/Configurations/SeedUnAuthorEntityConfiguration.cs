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
    public class SeedUnAuthorEntityConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            return;
            builder.HasData
                (
                new Author
                {
                    Id = Guid.Parse("9cb8174d-f8af-44ff-bf18-0a4a7e7d46e1"),
                    FirstName = "Test",
                    LastName = "Test",
                    PhoneNumber = "0999292112",
                    UserId = Guid.Parse("86e01957-e0c1-446c-adcc-183c15c8a1dd"),

                }
                );
        }
    }
}
