using BookLibrary.Web.ViewModels.Book;
using BookLibrary.Web.ViewModels.Home;
using BookLibraty.Services.Data.Models.Book;


namespace BookLibrary.Services.Data.Interfaces
{
    public interface IBookService
    {
        Task<AllBooksFilteredAndPagedServiceModel> GetAllBooksFilteredAndPaged(AllBooksQueryModel queryModel);
        Task<string> CreateAndReturnIdAsync(BookFormModel model, string agentId , string fileName);
        Task<bool> ExistByIdAsync(string bookId);
        Task<bool> isAuthorWithIdOwnerOfHouseWithIdAsync(string houseId, string ownerId);
        Task<BookFormModel> GetBookForEditByIdAsync(string houseId );
        Task EditBookByIdAsync(string id, BookFormModel model , string fileName);
        Task<BookDetailsViewModel> GetBookDetailsAsync(string bookId);
        Task GetBookByIdAndDelete(string id);   
        Task<bool> IsLikedAsync(string bookId);
        Task LikeBookAsync(string bookId, string userId);
        Task UnLikeBookAsync(string bookId);
        Task<bool> IsLikedByUserWithIdAsync(string bookId, string userId);
        Task<IEnumerable<BookAllViewModel>> AllByAuthorIdAsync(string authorId);
        Task<IEnumerable<BookAllViewModel>> AllByUserIdAsync(string userId);
        Task<IEnumerable<BookAllViewModel>> AllLickedBooksAsync(string userId);
        Task<IEnumerable<IndexViewModel>> LastThreeBooksAsync();
    }
}
