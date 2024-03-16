using BookLibrary.Web.ViewModels.Book;
using BookLibraty.Services.Data.Models.Book;


namespace BookLibrary.Services.Data.Interfaces
{
    public interface IBookService
    {
        Task<AllBooksFilteredAndPagedServiceModel> GetAllBooksFilteredAndPaged(AllBooksQueryModel queryModel);
    }
}
