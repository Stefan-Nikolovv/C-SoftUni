using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class AllBookViewModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 10)]
        
        public string Title { get; set; }
        [Required]

        public string ImageUrl { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Author { get; set; }
        public decimal Rating { get; set; }
        public string Category { get; set; }
    }
}
