using Library.Contracts;
using Library.Data;
using Library.Data.Models;
using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Services
{
    public class BookService : IBookService
    {
        private readonly LibraryDbContext dbContext;

        public BookService(LibraryDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddBookAsync(AddBookViewModel book)
        {
            Book bookToAdd = new Book();
            bookToAdd.Title = book.Title;
            bookToAdd.Description = book.Description;
            bookToAdd.Author = book.Author;
            bookToAdd.CategoryId = book.CategoryId;
            bookToAdd.Rating = decimal.Parse(book.Rating);
           
             dbContext.Books.AddAsync(bookToAdd);
            await dbContext.SaveChangesAsync();
            
        }

        public async Task AddToUserColletionAsync(string Id, AllMineViewBooks book)
        {
            bool alreadyAdded = await dbContext
                 .IdentityUsers.AnyAsync(x => x.CollectorId == Id && x.BookId == book.Id);

            if (alreadyAdded)
            {
                throw new InvalidOperationException("Book Already added to collection!");
            }
            var userBook = new IdentityUserBook()
            {
                CollectorId = Id,
                BookId = book.Id,
            };
            await dbContext.IdentityUsers.AddAsync(userBook);
            await dbContext.SaveChangesAsync(); 
        }

        public async Task<IEnumerable<AllMineViewBooks>> GellAllMineBooksAsync(string userId)
        {
            var allMineBooks = await this.dbContext.IdentityUsers
                               .Where(c => c.CollectorId == userId)
                               .Select(books => new AllMineViewBooks(){
                                       Id = books.Book.Id,
                                       Title = books.Book.Title,
                                       Author = books.Book.Author,
                                       Description = books.Book.Description,
                                       ImageUrl = books.Book.ImageUrl,
                                       Category = books.Book.Category.Name
                               })
                               .ToListAsync();
            return allMineBooks;
        }

        public async Task<IEnumerable<AllBookViewModel>> GetAllBooksAsync()
        {
           var allBooks = await this.dbContext.Books
                .Select(book => new AllBookViewModel()
                {
                    Id = book.Id,
                    Title = book.Title,
                    ImageUrl = book.ImageUrl,
                    Category = book.Category.Name,
                    Rating = book.Rating,
                })
                .ToListAsync();
            return allBooks;
        }

        public async Task<AddBookViewModel> GetAddBookViewModelWIthCategories()
        {
           var categories =  await dbContext.Categories
                .Select(c => new CategoryViewModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                }).ToListAsync();

            var model = new AddBookViewModel()
            {
                Categories = categories
            };
            return model;
        }

        public async Task<AllMineViewBooks> GetBookByIdAsync(int id)
        {
            var book = await this.dbContext.Books
                    .Where(book => book.Id == id)
                    .Select(book => new AllMineViewBooks()
                    {
                        Id = book.Id,
                        Title = book.Title,
                        Author = book.Author,
                        Description = book.Description,
                        ImageUrl = book.ImageUrl,
                        Category = book.Category.Name,
                    }).FirstOrDefaultAsync();
            return book;
        }

        public async Task RemoveBookFromUsersCollectionAsync(string userId, AllMineViewBooks book)
        {
            bool alreadyAdded = await dbContext
                  .IdentityUsers.AnyAsync(x => x.CollectorId == userId && x.BookId == book.Id);
            if (alreadyAdded)
            {
                var removedBook = await this.dbContext.IdentityUsers
                  .FirstOrDefaultAsync(x => x.CollectorId == userId && x.BookId == book.Id);

                dbContext.IdentityUsers.Remove(removedBook);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<AddBookViewModel?> GetBookForEditByIdAsync(int id)
        {
            var categories = await this.dbContext.Categories
                  .Select(c => new CategoryViewModel()
                  {
                      Id = c.Id,
                      Name = c.Name,
                  }).ToListAsync();

                return await this.dbContext.Books.
                Where(x => x.Id == id)
                .Select(x => new AddBookViewModel()
                {
                    Id = x.Id,
                    Title = x.Title,
                    Author = x.Author,
                    CategoryId  = x.CategoryId,
                    ImageUrl = x.ImageUrl,
                    Description = x.Description,
                    Categories = categories
                })
                .FirstOrDefaultAsync();

        }

        public async Task AddEdittedBookAsync(int id, AddBookViewModel model)
        {
            var book = await this.dbContext.Books.FindAsync(id);

            if(book != null)
            {
                book.Title = model.Title;
                book.Author = model.Author;
                book.CategoryId = model.CategoryId;
                book.ImageUrl = model.ImageUrl;
                book.Description = model.Description;
                book.Rating = decimal.Parse(model.Rating);
            }
            await dbContext.SaveChangesAsync();
        }
    }
}
