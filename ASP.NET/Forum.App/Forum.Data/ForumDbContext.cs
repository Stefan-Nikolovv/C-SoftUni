using Forum.Data.Configuration;
using Forum.Data.Models;
using Forum.Data.Seeding;
using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
using System.Reflection.Emit;

namespace Forum.Data
{
    public class ForumDbContext : DbContext
    {
        public ForumDbContext(DbContextOptions options)
        : base(options)
        {
        }

        public DbSet<Post> Posts { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PostEntityConfiguration());
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
