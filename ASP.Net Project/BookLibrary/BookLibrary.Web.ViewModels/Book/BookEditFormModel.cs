using BookLibrary.Web.ViewModels.Category;
using Microsoft.AspNetCore.Http;

using System.ComponentModel.DataAnnotations;

using static BookLibrary.Common.EntityValidationsConstants.Book;
namespace BookLibrary.Web.ViewModels.Book
{
    public class BookEditFormModel : UploadImageViewModel
    {


        public int Id { get; set; }
      
        public string? ExistingImage { get; set; }
    }
}
