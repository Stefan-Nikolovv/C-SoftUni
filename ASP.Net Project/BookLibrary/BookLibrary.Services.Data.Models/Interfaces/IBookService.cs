using BookLibrary.Web.ViewModels.Book;
using BookLibraty.Services.Data.Models.Book;


namespace BookLibrary.Services.Data.Interfaces
{
    public interface IBookService
    {
        Task<AllBooksFilteredAndPagedServiceModel> GetAllBooksFilteredAndPaged(AllBooksQueryModel queryModel);
        Task<string> CreateAndReturnIdAsync(BookFormModel model, string agentId);
        Task<bool> ExistByIdAsync(string bookId);
        Task<bool> isAuthorWithIdOwnerOfHouseWithIdAsync(string houseId, string ownerId);
    }
}
