using System.ComponentModel.DataAnnotations;
using TaskBoardApp.Common;

namespace TaskBoardApp.Data.Models
{
    public class Board
    {
        public Board()
        {
            this.Tasks = new HashSet<Task>();
        }
        public int Id { get; set; }
        [Required]
        [MaxLength(EntityValidationConstants.Board.BoardMaxName)]
        public string Name { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
    }
}