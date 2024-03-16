using BookLibrary.Web.ViewModels.Book;


namespace BookLibraty.Services.Data.Models.Book
{
    public class AllBooksFilteredAndPagedServiceModel
    {
        public AllBooksFilteredAndPagedServiceModel()
        {
            this.Books = new HashSet<BookAllViewModel>();
        }
        public int TotalBooksCount { get; set; }
        public IEnumerable<BookAllViewModel> Books { get; set; }
    }
}
