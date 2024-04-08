using BookLibrary.Data.Models;
using BookLibratySystesm.Data.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BookLibrary.Data
{
    public class BookDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        private readonly bool seedDb;
        public BookDbContext(DbContextOptions<BookDbContext> options, bool seedDb = true)
            : base(options)
        {
            this.seedDb = seedDb;
        }

        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Book> Books { get; set; } = null!;
        public DbSet<Author> Authors { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
           
            builder.ApplyConfiguration(new BookEntityConfiguration());
            //Assembly configAssembly = Assembly.GetAssembly(typeof(BookDbContext)) ??
            //                             Assembly.GetExecutingAssembly();
            if (this.seedDb)
            {
                builder.ApplyConfiguration(new CategoryEntityConfiguration());
                builder.ApplyConfiguration(new SeedBookEntityConfiguration());
            }

            //builder.ApplyConfigurationsFromAssembly(configAssembly);
            base.OnModelCreating(builder);
        }
    }
}
