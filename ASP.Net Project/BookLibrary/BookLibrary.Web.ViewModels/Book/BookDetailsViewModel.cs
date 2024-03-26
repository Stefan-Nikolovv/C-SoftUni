using BookLibrary.Web.ViewModels.Author;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.Web.ViewModels.Book
{
    public class BookDetailsViewModel 
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Language { get; set; }
        public string Pages { get; set; }
        public string Publisher { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public bool IsLiked { get; set; }
        public AuhtorInfoForBook authorInfoForBook { get; set; }
    }
}
