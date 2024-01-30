using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskBoardApp.Common;

namespace TaskBoardApp.Data.Models
{
    public class Task
    {
        public Task()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        [Required]
        [MaxLength(EntityValidationConstants.TaskBoard.TaskMaxTitle)]
        public string Title { get; set; }

        [Required]
        [MaxLength(EntityValidationConstants.TaskBoard.TaskMaxDescription)]
        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }
        [ForeignKey(nameof(Board))]
        public int BoardId { get; set; }
        public virtual Board Board { get; set; }
        [Required]
        [ForeignKey(nameof(Owner))]
        public string OwnerId { get; set; }

        public virtual IdentityUser Owner { get; set; }
    }
}
