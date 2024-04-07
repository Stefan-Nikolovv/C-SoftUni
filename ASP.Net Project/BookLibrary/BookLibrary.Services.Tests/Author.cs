namespace BookLibrary.Services.Tests
{
    using NUnit.Framework;
    using Moq;
    using System.Threading.Tasks;
    using BookLibrary.Services;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;

 
        [TestFixture]
        public class AuthorServiceTests
        {
        private DbContextOptions<BookLibraryDbContext> dbOptions;
        private BookLibraryDbContext dbContext;

        private IAuthorService agentService;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            this.dbOptions = new DbContextOptionsBuilder<BookLibraryDbContext>()
                .UseInMemoryDatabase("BookLibraryInMemory" + Guid.NewGuid().ToString())
                .Options;
            this.dbContext = new BookLibraryDbContext(this.dbOptions, false);

            this.dbContext.Database.EnsureCreated();
            SeedDatabase(this.dbContext);

            this.agentService = new AuthorService(this.dbContext);
        }
        [Test]
            public async Task AuthorExistsByUserId_ReturnsTrueIfAuthorExists()
            {
                // Arrange
                var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: "AuthorExistsByUserIdTest")
                    .Options;

                using (var context = new ApplicationDbContext(options))
                {
                    var author = new Author { UserId = "userId" }; // Provide appropriate user id
                    context.Authors.Add(author);
                    context.SaveChanges();
                }

                using (var context = new ApplicationDbContext(options))
                {
                    var authorService = new AuthorService(context);

                    // Act
                    var result = await authorService.AuthorExistsByUserId("userId");

                    // Assert
                    Assert.IsTrue(result);
                }
            }

            [Test]
            public async Task AuthorExistsByUserId_ReturnsFalseIfAuthorDoesNotExist()
            {
                // Arrange
                var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: "AuthorExistsByUserIdTest")
                    .Options;

                using (var context = new ApplicationDbContext(options))
                {
                    var authorService = new AuthorService(context);

                    // Act
                    var result = await authorService.AuthorExistsByUserId("userId");

                    // Assert
                    Assert.IsFalse(result);
                }
            }

            // Add similar tests for other methods...

            [Test]
            public async Task UserHasLikesAsync_ReturnsTrueIfUserHasLikes()
            {
                // Arrange
                var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: "UserHasLikesAsyncTest")
                    .Options;

                using (var context = new ApplicationDbContext(options))
                {
                    var user = new ApplicationUser { Id = "userId" }; // Provide appropriate user id
                    user.LikedBooks.Add(new Book()); // Add a liked book
                    context.Users.Add(user);
                    context.SaveChanges();
                }

                using (var context = new ApplicationDbContext(options))
                {
                    var authorService = new AuthorService(context);

                    // Act
                    var result = await authorService.UserHasLikesAsync("userId");

                    // Assert
                    Assert.IsTrue(result);
                }
            }

            [Test]
            public async Task UserHasLikesAsync_ReturnsFalseIfUserDoesNotHaveLikes()
            {
                // Arrange
                var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: "UserHasLikesAsyncTest")
                    .Options;

                using (var context = new ApplicationDbContext(options))
                {
                    var authorService = new AuthorService(context);

                    // Act
                    var result = await authorService.UserHasLikesAsync("userId");

                    // Assert
                    Assert.IsFalse(result);
                }
            }
        }
    
}