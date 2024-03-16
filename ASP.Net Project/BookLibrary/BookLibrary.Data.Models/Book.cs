
using System.ComponentModel.DataAnnotations;
using static BookLibrary.Common.EntityValidationsConstants.Book;

namespace BookLibrary.Data.Models
{
    public class Book
    {
        public Book()
        {
            Id = Guid.NewGuid();
        }
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [MaxLength(TitleNameMaxLength)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Price is required")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Pages is required")]
        
        public string Pages { get; set; }

        [Display(Name = "Publisher")]
        [Required(ErrorMessage = "Publisher is required")]
        [MaxLength(PublisherMax)]
        public string Publisher { get; set; }

        [Required(ErrorMessage = "Language is required")]
        [MaxLength(LanguageMax)]
        public string Language { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        [Required]
        public Guid AuthorId { get; set; }
        public virtual Author Author { get; set; }
        public Guid? LikerId { get; set; }
        public ApplicationUser? Liker { get; set; }

        [Display(Name = "Created On")]
        [DataType(DataType.Date)]
        public DateTime CreatedOn { get; set; }
        public string FileName { get; set; } = null!;

        public byte[] Image { get; set; }

        public bool isActive { get; set; }
    }
}
