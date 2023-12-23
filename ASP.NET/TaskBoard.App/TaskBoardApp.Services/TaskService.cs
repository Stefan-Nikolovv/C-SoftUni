using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskBoardApp.Data;
using TaskBoardApp.Services.Contracts;
using TaskBoardApp.Web.ViewModels.Board;
using TaskBoardApp.Web.ViewModels.Task;



namespace TaskBoardApp.Services
{
    
   
   
    public class TaskService : ITaskService
    {
        private readonly TaskBoardDbContext dbContext;
        public TaskService(TaskBoardDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task AddAsync(string userId, TaskFormModel model)
        {
            Data.Models.Task task = new Data.Models.Task()
            {
                Title = model.Title,
                Description = model.Description,
                BoardId = model.BoardId,
                CreatedOn = DateTime.UtcNow,
                OwnerId = userId,
            };
            await this.dbContext.Tasks.AddAsync(task);
            await this.dbContext.SaveChangesAsync();


        }

        public async Task DeleteFormByIdAsync(string id)
        {
            var task = await this.dbContext
              .Tasks.
              FirstAsync(x => x.Id.ToString() == id);
            this.dbContext.Tasks.Remove(task);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task EditFormByIdAsync(string id, TaskFormModel model)
        {
            var task = await this.dbContext
                .Tasks.
                FirstAsync(x => x.Id.ToString() == id);

            task.Title = model.Title;
            task.Description = model.Description;
            task.BoardId = model.BoardId;


            await this.dbContext.SaveChangesAsync();
        }

        public async Task<TaskCreateViewModel> GetByIdAsync(string id)
        {
            TaskCreateViewModel task = await this.dbContext.
                Tasks.
                Select(x => new TaskCreateViewModel()
                {
                    Id = x.Id.ToString(),
                    Title = x.Title,
                    Description = x.Description,
                    Owner = x.Owner.UserName,
                    CreatedOn = x.CreatedOn.ToString("f"),
                    Board = x.Board.Name
                }).FirstAsync(x => x.Id == id);


            
            return task;
        }

        public async Task<TaskFormModel> GetFormByIdAsync(string id)
        {
            TaskFormModel task = await this.dbContext
                .Tasks.Select(x => new TaskFormModel()
                {
                    Id = x.Id.ToString(),
                    Title = x.Title,
                    Description = x.Description,
                    BoardId = x.BoardId,
                    
                }).FirstAsync(x => x.Id == id);
           
            return task;
        }
    }
}
