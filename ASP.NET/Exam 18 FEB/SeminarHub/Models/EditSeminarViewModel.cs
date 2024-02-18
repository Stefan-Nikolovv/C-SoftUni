using System.ComponentModel.DataAnnotations;

namespace SeminarHub.Models
{
    public class EditSeminarViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Topic { get; set; }
        [Required]
        [StringLength(60, MinimumLength = 5)]
        public string Lecturer { get; set; }
        [Required]
        [StringLength(500, MinimumLength = 10)]
        public string Details { get; set; }
        [Required(ErrorMessage = "Date and Time are required")]
        public string DateAndTime { get; set; } = string.Empty;
        [Range(30, 180)]
        public int? Duration { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public List<AllViewCategories> Categories { get; set; } = new List<AllViewCategories>();
    }
}
