using BookLibrary.Data;
using BookLibrary.Data.Models;
using BookLibrary.Services.Data.Interfaces;
using BookLibrary.Web.ViewModels.Author;
using BookLibrary.Web.ViewModels.Book;
using BookLibrary.Web.ViewModels.Book.Enums;
using BookLibraty.Services.Data.Models.Book;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.IO;
using static System.Net.Mime.MediaTypeNames;


namespace BookLibrary.Services.Data
{
    public class BookService : IBookService
    {
        private readonly BookDbContext dbContext;

        public BookService(BookDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<string> CreateAndReturnIdAsync(BookFormModel model, string agentId, string fileName)
        {
            Book modelForm = new Book()
            {
                Title = model.Title,
                Publisher = model.Publisher,
                Price = model.BookPrice,
                Image = fileName,
                Description = model.Description,
                Pages = model.Pages,
                Language = model.Language,
                CategoryId = model.CategoryId,
                AuthorId = Guid.Parse(agentId),

            };
          
         
        
            await this.dbContext.Books.AddAsync(modelForm);
            await dbContext.SaveChangesAsync();

            return modelForm.Id.ToString();
        }

        public async Task EditBookByIdAsync(string id, BookFormModel model, string f)
        {
            Book bookForm = await
                this.dbContext
                .Books
                .Where(h => h.isActive)
                .FirstAsync(h => h.Id.ToString() == id);

            if (bookForm != null)
            {
                bookForm.Image = f;
                bookForm.Title = model.Title;
                bookForm.Description = model.Description;
                bookForm.Language = model.Language;
                bookForm.Pages = model.Pages;
                bookForm.Price = model.BookPrice;
                bookForm.CategoryId = model.CategoryId;


            }

            await dbContext.SaveChangesAsync();
        }

    

        public async Task<bool> ExistByIdAsync(string bookId)
        {
            bool searchedBook = await this.dbContext.Books.Where(h => h.isActive).AnyAsync(x => x.Id.ToString() == bookId);
            return searchedBook; 
        }

        public async Task<AllBooksFilteredAndPagedServiceModel> GetAllBooksFilteredAndPaged(AllBooksQueryModel queryModel)
        {
            IQueryable<Book> booksQuery = dbContext.Books.AsQueryable();

            if(!string.IsNullOrWhiteSpace(queryModel.Category))
            {
                booksQuery = booksQuery
                    .Where(c => c.Category.Name == queryModel.Category);
            }


            if (!string.IsNullOrWhiteSpace(queryModel.SearchString))
            {
                string wildCard = $"%{queryModel.SearchString.ToLower()}%";
                booksQuery = booksQuery
                   .Where(h => EF.Functions.Like(h.Title, wildCard) ||
                               EF.Functions.Like(h.Publisher, wildCard) ||
                               EF.Functions.Like(h.Description, wildCard));
            }

            booksQuery = queryModel.BookSorting switch
            {
                BookSorting.Newest => booksQuery.OrderBy(h => h.CreatedOn),
                BookSorting.Oldest => booksQuery.OrderByDescending(h => h.CreatedOn),
                BookSorting.PriceAscending => booksQuery.OrderBy(h => h.Price),
                BookSorting.PriceDescending => booksQuery.OrderByDescending(h => h.Price),
                _ => booksQuery.OrderBy(h => h.LikerId != null)
                .ThenByDescending(h => h.CreatedOn)
            };

            IEnumerable<BookAllViewModel> allBooks = await booksQuery
                                                      .Where(h => h.isActive)
                                                      .Skip((queryModel.CurrentPage - 1) * queryModel.BookPerPage)
                                                      .Take(queryModel.BookPerPage)
                                                      .Select(h => new BookAllViewModel()
                                                      {
                                                          Id = h.Id.ToString(),
                                                          Title = h.Title,
                                                          Image = h.Image,
                                                          Price = h.Price,
                                                          Publisher = h.Publisher
                                                      })
                                                      .ToArrayAsync();
            int totalBooks = booksQuery.Count();

            return new AllBooksFilteredAndPagedServiceModel()
            {
                TotalBooksCount = totalBooks,
                Books = allBooks
            };
        }

        public async Task GetBookByIdAndDelete(string id)
        {
            Book book = await this.dbContext.Books.Where(h => h.isActive).FirstAsync(h => h.Id.ToString() == id);

            book.isActive = false;
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<BookDetailsViewModel> GetBookDetailsAsync(string bookId)
        {
            Book book = await this.dbContext
                .Books
                .Include(h => h.Category)
                .Include(h => h.Author)
                .ThenInclude(a => a.User)
               .Where(h => h.isActive)
               .FirstAsync(h => h.Id.ToString() == bookId);


            return new BookDetailsViewModel()
            {
                Id = book.Id.ToString(),
                Title = book.Title,
                Image = book.Image,
                Price = book.Price,
                Pages = book.Pages,
                Publisher = book.Publisher,
                CreatedOn = book.CreatedOn,
                Language = book.Language,
                Description = book.Description,
                Category = book.Category.Name,
                authorInfoForBook = new AuhtorInfoForBook()
                {
                    FirstName = book.Author.User.FirstName,
                    LastName = book.Author.User.LastName,
                    Email = book.Author.User.Email,
                    PhoneNumber = book.Author.PhoneNumber,
                }
            };
        }

        public async Task<BookFormModel> GetBookForEditByIdAsync(string houseId)
        {
            Book book = await this.dbContext
                 .Books
                 .Include(h => h.Category)
                .Where(h => h.isActive)
                .FirstAsync(h => h.Id.ToString() == houseId);



            BookFormModel booktoEdit = new BookFormModel()
            {
                Title = book.Title,
                Language = book.Language,
                ExistingImage = book.Image,
                Description = book.Description,
                Pages = book.Pages,
                BookPrice = book.Price,
                Publisher = book.Publisher,
                CategoryId = book.CategoryId,
               
            };
          
            


            return booktoEdit;
           
        }

        public async Task<bool> isAuthorWithIdOwnerOfHouseWithIdAsync(string houseId, string ownerId)
        {
            Book book =
                await this.dbContext
                .Books
                .Where(h => h.isActive)
                .FirstAsync(h => h.Id.ToString() == houseId);

            return book.AuthorId.ToString() == ownerId;
        }

        public async Task<bool> IsLikedAsync(string bookId)
        {
            Book book = await dbContext
               .Books
               .FirstAsync(h => h.Id.ToString() == bookId);

            return book.LikerId.HasValue;
        }

        public async Task<bool> IsLikedByUserWithIdAsync(string bookId, string userId)
        {
            Book book = await this.dbContext.Books.FirstAsync(h => h.Id.ToString() == bookId);
            return book.LikerId.HasValue && book.LikerId.ToString() == userId;
        }

        public async Task LikeBookAsync(string bookId, string userId)
        {
            Book book = await dbContext
                .Books
                .FirstAsync(h => h.Id.ToString() == bookId);

            book.LikerId = Guid.Parse(userId);

            await dbContext.SaveChangesAsync();
        }

        public async Task UnLikeBookAsync(string bookId)
        {
            Book book = await dbContext
               .Books
               .FirstAsync(h => h.Id.ToString() == bookId);
            book.LikerId = null;

            await dbContext.SaveChangesAsync();
        }
    }
 
}
