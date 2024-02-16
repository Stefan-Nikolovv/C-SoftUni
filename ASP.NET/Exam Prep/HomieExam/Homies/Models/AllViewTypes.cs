using System.ComponentModel.DataAnnotations;

namespace Homies.Models
{
    public class AllViewTypes
    {
        [Required]
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(15, MinimumLength = 5)]
        public string Name { get; set; }
    }
}
