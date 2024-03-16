using BookLibrary.Web.ViewModels.Book.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.Web.ViewModels.Book
{
    public class AllBooksQueryModel
    {
        public AllBooksQueryModel()
        {
            CurrentPage = 1;
            BookPerPage = 3;
            Categories = new HashSet<string>();
            Books = new HashSet<BookAllViewModel>();
        }
        public string? Category { get; set; }
        [Display(Name = "Search by word")]
        public string? SearchString { get; set; }
        [Display(Name = "Sort Book by")]
        public BookSorting BookSorting { get; set; }

        public int CurrentPage { get; set; }

        public int BookPerPage { get; set; }
        public int TotalBooks { get; set; }

        public IEnumerable<string> Categories { get; set; }

        public IEnumerable<BookAllViewModel> Books { get; set; }
    }
}
