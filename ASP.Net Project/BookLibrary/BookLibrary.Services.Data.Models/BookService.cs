using BookLibrary.Data;
using BookLibrary.Data.Models;
using BookLibrary.Services.Data.Interfaces;
using BookLibrary.Web.ViewModels.Book;
using BookLibrary.Web.ViewModels.Book.Enums;
using BookLibraty.Services.Data.Models.Book;
using Microsoft.EntityFrameworkCore;


namespace BookLibrary.Services.Data
{
    public class BookService : IBookService
    {
        private readonly BookDbContext dbContext;

        public BookService(BookDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<string> CreateAndReturnIdAsync(BookFormModel model, string agentId)
        {
            Book modelForm = new Book()
            {
                Title = model.Title,
                Publisher = model.Publisher,
                Price = model.BookPrice,
                Description = model.Description,
                FileName = model.Image.FileName,
                Pages = model.Pages,
                Language = model.Language,
                CategoryId = model.CategoryId,
                AuthorId = Guid.Parse(agentId),

            };
            using (var memoryStream = new MemoryStream())
            {
                await model.Image.CopyToAsync(memoryStream);
                modelForm.Image = memoryStream.ToArray();
            }
         
        
            await this.dbContext.Books.AddAsync(modelForm);
            await dbContext.SaveChangesAsync();

            return modelForm.Id.ToString();
        }

        public async Task<bool> ExistByIdAsync(string bookId)
        {
            bool searchedBook = await this.dbContext.Books.Where(h => h.isActive).AnyAsync(x => x.Id.ToString() == bookId);
            return searchedBook; throw new NotImplementedException();
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
                                                          Image = Convert.ToBase64String(h.Image),
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

        public Task<bool> isAuthorWithIdOwnerOfHouseWithIdAsync(string houseId, string ownerId)
        {
            throw new NotImplementedException();
        }
    }
}
