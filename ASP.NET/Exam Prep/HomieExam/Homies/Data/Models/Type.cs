using System.ComponentModel.DataAnnotations;

namespace Homies.Data.Models
{
    public class Type
    {
        public Type()
        {
            this.Events = new List<Event>();
        }
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(15)]
        public string Name { get; set; }
        public List<Event> Events { get; set; }
    }
}
