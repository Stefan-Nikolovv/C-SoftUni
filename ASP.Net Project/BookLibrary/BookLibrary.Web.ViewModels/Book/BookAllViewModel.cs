using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.Web.ViewModels.Book
{
    public class BookAllViewModel
    {
        public string Id { get; set; }
        public string Title { get; set;}
        public string Image { get; set; }
        public string Price { get; set; }
        public string Publisher { get; set; }
    }
}
