using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskBoardApp.Web.ViewModels.Task;

namespace TaskBoardApp.Services.Contracts
{
    public interface ITaskService
    {
        Task AddAsync(string userId, TaskFormModel model);
        Task<TaskCreateViewModel> GetByIdAsync(string id);
        Task<TaskFormModel> GetFormByIdAsync(string id);
        Task EditFormByIdAsync(string id, TaskFormModel model);
        Task DeleteFormByIdAsync(string id);
    }
}