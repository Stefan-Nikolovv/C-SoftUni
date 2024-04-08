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
    public class BookEntityConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder
                .Property(h => h.CreatedOn)
                .HasDefaultValueSql("GETDATE()");

            builder.Property(h => h.isActive)
               .HasDefaultValue(true);

            builder
               .HasOne(h => h.Category)
               .WithMany(b => b.Books)
               .HasForeignKey(h => h.CategoryId)
               .OnDelete(DeleteBehavior.Restrict);

            builder
               .HasOne(b => b.Author)
               .WithMany(a => a.ManagedBooks)
               .HasForeignKey(b => b.AuthorId)
               .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(b => b.Liker)
                .WithMany (a => a.LikedBooks)
                .HasForeignKey (b => b.LikerId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
