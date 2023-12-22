using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P02_FootballBetting.Data.Models
{
    public class Position
    {
        [Key]
        public int PostitionId { get; set; }
        [Required]
        [StringLength(Constants.PositionNameLength)]
        public string Name { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
