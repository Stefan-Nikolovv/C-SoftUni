namespace BookLibrary.Services.Tests
{
    using NUnit.Framework;
    using Moq;
    using System.Threading.Tasks;
   
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
  
    using static DatabaseSeeder;
 
   
    using global::BookLibrary.Data;
    using global::BookLibrary.Services.Data;

    [TestFixture]
        public class AuthorServiceTests
        {
        private DbContextOptions<BookDbContext> dbOptions;
        private BookDbContext dbContext;

        private AuthorService authorService;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            this.dbOptions = new DbContextOptionsBuilder<BookDbContext>()
                .UseInMemoryDatabase("BookLibraryInMemory" + Guid.NewGuid().ToString())
                .Options;
            this.dbContext = new BookDbContext(this.dbOptions, false);

            this.dbContext.Database.EnsureCreated();
            SeedDatabase(this.dbContext);

            this.authorService = new AuthorService(this.dbContext);
        }
        [Test]
            public async Task AuthorExistsByUserId_ReturnsTrueIfAuthorExists()
            {
                // Arrange
                var options = new DbContextOptionsBuilder<BookDbContext>()
                    .UseInMemoryDatabase(databaseName: "AuthorExistsByUserIdTest")
                    .Options;

                using (var context = new BookDbContext(options))
                {
                    
                    context.Authors.Add(DatabaseSeeder.AuthorUser);
                    context.SaveChanges();
                }

                using (var context = new BookDbContext(options))
                {
                    var authorService = new AuthorService(context);

                    // Act
                    var result = await authorService.AuthorExistsByUserId(DatabaseSeeder.AuthorUser.UserId.ToString());

                    // Assert
                    Assert.IsTrue(result);
                }
            }

            [Test]
            public async Task AuthorExistsByUserId_ReturnsFalseIfAuthorDoesNotExist()
            {
                // Arrange
                var options = new DbContextOptionsBuilder<BookDbContext>()
                    .UseInMemoryDatabase(databaseName: "AuthorExistsByUserIdTest")
                    .Options;

                using (var context = new BookDbContext(options))
                {
                    var authorService = new AuthorService(context);

                    // Act
                    var result = await authorService.AuthorExistsByUserId(AppTestUser.Id.ToString());

                    // Assert
                    Assert.IsFalse(result);
                }
            }

            // Add similar tests for other methods...

            [Test]
            public async Task UserHasLikesAsync_ReturnsTrueIfUserHasLikes()
            {
                // Arrange
                var options = new DbContextOptionsBuilder<BookDbContext>()
                    .UseInMemoryDatabase(databaseName: "UserHasLikesAsyncTest")
                    .Options;

                using (var context = new BookDbContext(options))
                {

                    AppTestUser.LikedBooks.Add(DatabaseSeeder.Book); // Add a liked book
                    context.Users.Add(AppTestUser);
                    context.SaveChanges();
                

              
                    var authorService = new AuthorService(context);

                    // Act
                    var result = await authorService.UserHasLikesAsync(AppTestUser.Id.ToString());

                    // Assert
                    Assert.IsTrue(result);
                }
            }

            [Test]
            public async Task UserHasLikesAsync_ReturnsFalseIfUserDoesNotHaveLikes()
            {
                // Arrange
                var options = new DbContextOptionsBuilder<BookDbContext>()
                    .UseInMemoryDatabase(databaseName: "UserHasLikesAsyncTest")
                    .Options;

                using (var context = new BookDbContext(options))
                {
                    var authorService = new AuthorService(context);

                    // Act
                    var result = await authorService.UserHasLikesAsync(AppTestUser.Id.ToString());

                    // Assert
                    Assert.IsFalse(result);
                }
            }
        }
    
}