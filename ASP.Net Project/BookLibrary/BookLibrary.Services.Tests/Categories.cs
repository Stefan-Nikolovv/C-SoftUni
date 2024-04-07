using BookLibrary.Data;
using BookLibrary.Data.Models;
using BookLibrary.Services.Data;
using BookLibrary.Services.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace BookLibrary.Services.Tests
{
    public class CategoryServiceTests
    {
        private DbContextOptions<BookDbContext> dbOptions;
        private BookDbContext dbContext;

        private ICategoryService CategoryService;
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            this.dbOptions = new DbContextOptionsBuilder<BookDbContext>()
                .UseInMemoryDatabase("BookLibraryInMemory" + Guid.NewGuid().ToString())
                .Options;
            this.dbContext = new BookDbContext(this.dbOptions, false);

            this.dbContext.Database.EnsureCreated();
           

            this.CategoryService = new CategoryService(this.dbContext);
        }
        [Test]
        public async Task AllCategoryNameAsync_ReturnsAllCategoryNames()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<BookDbContext>()
                .UseInMemoryDatabase(databaseName: "AllCategoryNameAsync_Test_Database")
                .Options;

            using (var context = new BookDbContext(options))
            {
                context.Categories.AddRange(new List<Category>
                {
                    new Category { Id = 1, Name = "Category 1" },
                    new Category { Id = 2, Name = "Category 2" },
                    new Category { Id = 3, Name = "Category 3" }
                });
                context.SaveChanges();
            }

            using (var context = new BookDbContext(options))
            {
                var categoryService = new CategoryService(context);

                // Act
                var result = await categoryService.AllCategoryNameAsync();

                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(3, result.Count());
                //Assert.Contains("Category 1", result);
                //Assert.Contains("Category 2", result);
                //Assert.Contains("Category 3", result);
            }
        }

        [Test]
        public async Task ExistsById_ReturnsTrueIfCategoryExists()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<BookDbContext>()
                .UseInMemoryDatabase(databaseName: "ExistsById_Test_Database")
                .Options;

            using (var context = new BookDbContext(options))
            {
                context.Categories.AddRange(new List<Category>
                {
                    new Category { Id = 1, Name = "Category 1" },
                    new Category { Id = 2, Name = "Category 2" },
                    new Category { Id = 3, Name = "Category 3" }
                });
                context.SaveChanges();
            }

            using (var context = new BookDbContext(options))
            {
                var categoryService = new CategoryService(context);

                // Act
                var result = await categoryService.ExistsById(1);

                // Assert
                Assert.IsTrue(result);
            }
        }

        [Test]
        public async Task GetAll_ReturnsAllCategories()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<BookDbContext>()
                .UseInMemoryDatabase(databaseName: "GetAll_Test_Database")
                .Options;

            using (var context = new BookDbContext(options))
            {
                context.Categories.AddRange(new List<Category>
                {
                    new Category { Id = 1, Name = "Category 1" },
                    new Category { Id = 2, Name = "Category 2" },
                    new Category { Id = 3, Name = "Category 3" }
                });
                context.SaveChanges();
            }

            using (var context = new BookDbContext(options))
            {
                var categoryService = new CategoryService(context);

                // Act
                var result = await categoryService.GetAll();

                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(3, result.Count());
                Assert.IsTrue(result.All(c => c.Id > 0 && !string.IsNullOrEmpty(c.Name)));
            }
        }
    }
}
