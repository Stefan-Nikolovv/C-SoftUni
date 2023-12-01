using System.ComponentModel.DataAnnotations;
using Footballers.Common;

namespace Footballers.Data.Models
{
    public class Coach
    {
        public Coach()
        {
            this.Footballers = new List<Footballer>();
        }
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(ValidationConstants.FootballerCoachMaxLength)]
        public string Name { get; set; }
        [Required]
        public string Nationality { get; set; }
        public virtual ICollection<Footballer> Footballers { get; set; }
    }
}