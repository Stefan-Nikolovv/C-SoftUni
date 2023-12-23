using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using TaskBoardApp.Common;
using TaskBoardApp.Web.ViewModels.Board;

namespace TaskBoardApp.Web.ViewModels.Task
{
    public class TaskFormModel
    {
        public string Id { get; set; } = null!;
        [Required]
        [StringLength(EntityValidationConstants.TaskBoard.TaskMaxTitle,
            MinimumLength = EntityValidationConstants.TaskBoard.TaskMinTitle,
            ErrorMessage = "Title should be at least {2} char long")]
        public string Title { get; set; }
        [Required]
        [StringLength(EntityValidationConstants.TaskBoard.TaskMaxDescription,
            MinimumLength = EntityValidationConstants.TaskBoard.TaskMinDescription, 
            ErrorMessage = "Description should be at least {2} char long")]
        public string Description { get; set; }
        [Display(Name = "Board")]
        public int BoardId { get; set; }
        public IEnumerable<BoardSelectVieModel>? Board { get; set; }
    }
}
