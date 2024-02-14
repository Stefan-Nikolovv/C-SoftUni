using Library.Data.Models;
using Library.Models;

namespace Library.Contracts
{
    public interface IBookService
    {
        Task<IEnumerable<AllBookViewModel>> GetAllBooksAsync();
        Task<IEnumerable<AllMineViewBooks>> GellAllMineBooksAsync(string userId);
        Task AddToUserColletionAsync(string Id, AllMineViewBooks book);
        Task<AllMineViewBooks> GetBookByIdAsync(int id);
        Task RemoveBookFromUsersCollectionAsync(string userId, AllMineViewBooks book);
        Task<AddBookViewModel> GetAddBookViewModelWIthCategories();
        Task AddBookAsync(AddBookViewModel book);
        Task<AddBookViewModel?> GetBookForEditByIdAsync(int id);
        Task AddEdittedBookAsync(int id, AddBookViewModel model);
    }
}
