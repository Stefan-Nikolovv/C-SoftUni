using BookLibrary.Web.ViewModels.Category;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static BookLibrary.Common.EntityValidationsConstants.Book;

namespace BookLibrary.Web.ViewModels.Book
{
    public class BookFormModel
    {
        public BookFormModel()
        {
            this.Categories = new HashSet<BookSelectCategoryFromModel>();
        }
        [Required]
        [StringLength(TitleNameMaxLength, MinimumLength = TitleNameMinLength)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(PublisherMax, MinimumLength = PublisherMin)]
        public string Publisher { get; set; } = null!;

        [Required]
        [StringLength(LanguageMax, MinimumLength = LanguageMin)]
        public string Language { get; set; } = null!;
        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
        public string Description { get; set; } = null!;

        [Required]
       
        [Display(Name = "Image Link")]
        public IFormFile Image { get; set; } = null!;

        [Range(typeof(decimal), BookPriceMin, BookPriceMax)]
        [Display(Name = "Book Price")]
        public decimal BookPrice { get; set; }

         [StringLength(BookPagesMax)]
        [Display(Name = "Book Pages")]
        public string Pages { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public IEnumerable<BookSelectCategoryFromModel> Categories { get; set; }
    }
}
