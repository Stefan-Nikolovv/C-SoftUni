using System.ComponentModel.DataAnnotations;

namespace Homies.Models
{
    public class EditEventViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 5)]
        public string Name { get; set; }
        [Required]
        [StringLength(150, MinimumLength = 15)]
        public string Description { get; set; }
        [Required]
       
        public string Start { get; set; }
        [Required]
       
        public string End { get; set; }
        public int TypeId { get; set; }
        [Required]
        public List<AllViewTypes> Types { get; set; }
    }
}
