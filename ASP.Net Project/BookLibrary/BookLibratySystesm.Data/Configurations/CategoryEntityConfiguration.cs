using BookLibrary.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace BookLibratySystesm.Data.Configurations
{
    public class CategoryEntityConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(this.GenerateCategories());
        }
        private Category[] GenerateCategories()
        {
            ICollection<Category> categories = new HashSet<Category>();

            Category category;

            category = new Category()
            {
                Id = 1,
                Name = "Mystery"
            };
            categories.Add(category);

            category = new Category()
            {
                Id = 2,
                Name = "Horror"
            };
            categories.Add(category);

            category = new Category()
            {
                Id = 3,
                Name = "Fantasy"
            };
            categories.Add(category);

            return categories.ToArray();
        }
    }
}
