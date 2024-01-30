using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskBoardApp.Web.ViewModels.Task
{
    public class TaskCreateViewModel : TaskViewModel
    {
        public string CreatedOn { get; set; }
        public string Board { get; set;}
    }
}
