using Library.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class AddBookViewModel
    {
        public AddBookViewModel()
        {
            this.Categories = new List<CategoryViewModel>();
        }
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 10)]

        public string Title { get; set; }
        [Required(AllowEmptyStrings = false)]

        public string ImageUrl { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Author { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Description { get; set; }
        [Required]
        public string Rating { get; set; }
        [Range(0, int.MaxValue)]
        public int CategoryId { get; set; }
        public IEnumerable<CategoryViewModel> Categories { get; set; } 
    }
}
