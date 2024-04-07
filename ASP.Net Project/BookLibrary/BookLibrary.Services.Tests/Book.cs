using BookLibrary.Data;
using BookLibrary.Data.Models;
using BookLibrary.Services.Data;
using BookLibrary.Web.ViewModels.Book;
using Microsoft.EntityFrameworkCore;
using static BookLibrary.Services.Tests.DatabaseSeeder;
namespace BookLibrary.Services.Tests
{
    namespace BookLibrary.Tests.Services
    {
        [TestFixture]
        public class BookServiceTests
        {
            private DbContextOptions<BookDbContext> _options;
            private BookDbContext dbContext;
            private AuthorService authorService;
            [OneTimeSetUp]
            public void OneTimeSetUp()
            {
                this._options = new DbContextOptionsBuilder<BookDbContext>()
                    .UseInMemoryDatabase("BookLibraryInMemory" + Guid.NewGuid().ToString())
                    .Options;
                this.dbContext = new BookDbContext(this._options, false);

                this.dbContext.Database.EnsureCreated();
                SeedDatabase(this.dbContext);

                this.authorService = new AuthorService(this.dbContext);
            }

            [Test]
            public async Task AllByAuthorIdAsync_ReturnsAllBooksByAuthorId()
            {
                // Arrange
                using (var context = new BookDbContext(_options))
                {
                    context.Books.AddRange(
                       this.GenerateBooks()
                    );
                    context.SaveChanges();
                }

                using (var context = new BookDbContext(_options))
                {
                    var bookService = new BookService(context);

                    // Act
                    var result = await bookService.AllByAuthorIdAsync(AuthorUser.Id.ToString());

                    // Assert
                    Assert.IsNotNull(result);
                    Assert.AreEqual(2, result.Count());
                    Assert.IsTrue(result.All(b => b.Id == "1" || b.Id == "2"));
                }
            }

            [Test]
            public async Task AllByUserIdAsync_ReturnsAllBooksByUserId()
            {
                // Arrange
                using (var context = new BookDbContext(_options))
                {
                    context.Books.AddRange(
                     this.GenerateBooks()
                    );
                    context.SaveChanges();
                }

                using (var context = new BookDbContext(_options))
                {
                    var bookService = new BookService(context);

                    // Act
                    var result = await bookService.AllByUserIdAsync(DatabaseSeeder.AppTestUser.Id.ToString());

                    // Assert
                    Assert.IsNotNull(result);
                    Assert.AreEqual(2, result.Count());
                    Assert.IsTrue(result.All(b => b.Id == "1" || b.Id == "2"));
                }
            }

            [Test]
            public async Task AllLickedBooksAsync_ReturnsAllBooksLikedByUserId()
            {
                // Arrange
                var userId = Guid.NewGuid().ToString();
                using (var context = new BookDbContext(_options))
                {
                    context.Books.AddRange(
                       this.GenerateBooks()
                    );
                    context.SaveChanges();
                }

                using (var context = new BookDbContext(_options))
                {
                    var bookService = new BookService(context);

                    // Act
                    var result = await bookService.AllLickedBooksAsync(userId);

                    // Assert
                    Assert.IsNotNull(result);
                    Assert.AreEqual(1, result.Count());
                    Assert.AreEqual("2", result.First().Id);
                }
            }

            // Add tests for other methods...

            [Test]
            public async Task CreateAndReturnIdAsync_CreatesAndReturnsBookId()
            {
                // Arrange
                using (var context = new BookDbContext(_options))
                {
                    var bookService = new BookService(context);
                    var model = new BookFormModel
                    {
                        Title = "New Book",
                        Publisher = "Publisher",
                        BookPrice = 10,
                        Description = "Description",
                        Pages = "100",
                        Language = "English",
                        CategoryId = 1
                    };
                    var authorId = "authorUserId";
                    var fileName = "image.jpg";

                    // Act
                    var result = await bookService.CreateAndReturnIdAsync(model, authorId, fileName);

                    // Assert
                    Assert.IsNotNull(result);
                    Assert.AreNotEqual("", result);
                }
            }

            [Test]
            public async Task EditBookByIdAsync_EditsBookWithGivenId()
            {
                // Arrange
                using (var context = new BookDbContext(_options))
                {
                    var book = this.GenerateBooks()[0];
                    context.Books.Add(book);
                    context.SaveChanges();
                }

                using (var context = new BookDbContext(_options))
                {
                    var bookService = new BookService(context);
                    var model = new BookFormModel
                    {
                        Title = "Edited Book",
                        Publisher = "Edited Publisher",
                        BookPrice = 20,
                        Description = "Edited Description",
                        Pages = "200",
                        Language = "Edited English",
                        CategoryId = 2
                    };
                    var fileName = "edited_image.jpg";

                    // Act
                    await bookService.EditBookByIdAsync("1", model, fileName);
                }

                using (var context = new BookDbContext(_options))
                {
                    // Assert
                    var editedBook = await context.Books.FindAsync(1);
                    Assert.IsNotNull(editedBook);
                    Assert.AreEqual("Edited Book", editedBook.Title);
                    Assert.AreEqual("Edited Publisher", editedBook.Publisher);
                    Assert.AreEqual(20, editedBook.Price);
                    Assert.AreEqual("Edited Description", editedBook.Description);
                    Assert.AreEqual("Edited English", editedBook.Language);
                    Assert.AreEqual(2, editedBook.CategoryId);
                }
            }

            private Book[] GenerateBooks()
            {
                ICollection<Book> books = new HashSet<Book>();

                Book book;

                book = new Book()
                {
                    Id = Guid.NewGuid(),
                    Title = "The First Seven",
                    Description = "Book created about the story of Bulgarian mountain climber. ",
                    Price = 2100.00M,
                    Pages = "245",
                    Publisher = "VackonPrime",
                    Language = "Bulgarian",
                    CategoryId = 3,
                    AuthorId = Guid.Parse(DatabaseSeeder.AuthorUser.Id.ToString()), //AuthorId,
                    LikerId = Guid.Parse(DatabaseSeeder.AppTestUser.Id.ToString()), //UserID
                    CreatedOn = DateTime.UtcNow,
                    Image = "2a46eb9a-6e55-42d3-8827-8059333c4b53_The First Seven.jpg"

                };
                books.Add(book);
                book = new Book()
                {
                    Id = Guid.NewGuid(),
                    Title = "Man's Search for Meaning",
                    Description = "Everything can be taken from a man but one thing: the last of the human freedoms—to choose one’s attitude in any given set of circumstances, to choose",
                    Price = 2100.00M,
                    Pages = "233",
                    Publisher = "amazonPrime",
                    Language = "German",
                    CategoryId = 2,
                    AuthorId = Guid.Parse(DatabaseSeeder.AuthorUser.Id.ToString()), //AuthorId,
                    LikerId = Guid.Parse(DatabaseSeeder.AppTestUser.Id.ToString()), //UserID
                    CreatedOn = DateTime.UtcNow,
                    Image = "107aba19-e84b-41e9-95bc-fefb811b9576_Man's Search for Meaning.jpg"

                };
                books.Add(book);
                book = new Book()
                {
                    Id = Guid.NewGuid(),
                    Title = "Beyond Possible",
                    Description = "Is 14 Peaks a book? Beyond Possible: One Man, Fourteen Peaks, and the Mountaineering Achievement of a Lifetime by ",
                    Price = 2100.00M,
                    Pages = "245",
                    Publisher = "NetFlixPrime",
                    Language = "English",
                    CategoryId = 4,
                    AuthorId = Guid.Parse("430E99FB-5A76-4235-9B39-83B13B17BB58"), //AuthorId,
                    LikerId = Guid.Parse("794405D2-C399-41D9-9A60-C4E2E7CB1B67"), //UserID
                    CreatedOn = DateTime.UtcNow,
                    Image = "8ca6ff06-ac52-47bc-b625-7f9355953c69_Beyond Possible.jpg"

                };
                books.Add(book);
                return books.ToArray();
            }
        }
    }

}