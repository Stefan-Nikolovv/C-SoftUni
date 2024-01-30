using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Task = TaskBoardApp.Data.Models.Task;


namespace TaskBoardApp.Data.Configuration
{
    internal class TaskEntityConfiguration : IEntityTypeConfiguration<Task>
    {
        public void Configure(EntityTypeBuilder<Task> builder)
        {
            builder
                 .HasOne(t => t.Board)
                 .WithMany(b => b.Tasks)
                 .HasForeignKey(t => t.BoardId)
                 .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasData(this.GenerateTasks());
        }

        private ICollection<Task> GenerateTasks()
        {
            ICollection<Task> tasks = new HashSet<Task>()
            {
                new Task()
                {
                    Title = "Improve CSS styles",
                    Description = "Implement better styling for all public pages",
                    CreatedOn = DateTime.UtcNow.AddDays(-200),
                    OwnerId = "1581772a-fbe7-465d-96cc-32c5f2d9365e",
                    BoardId = 1
                },
                new Task()
                {
                    Title = "Android Client App",
                    Description = "Create Android client App for the RESTful TaskBoard service",
                    CreatedOn = DateTime.UtcNow.AddMonths(-5),
                    OwnerId = "b671e676-ad8b-47aa-bc1e-a50f9bac4966",
                    BoardId = 1
                },
                new Task()
                {
                    Title = "Desktop Client App",
                    Description = "Create Desktop client App for the RESTful TaskBoard service",
                    CreatedOn = DateTime.UtcNow.AddMonths(-1),
                    OwnerId = "b671e676-ad8b-47aa-bc1e-a50f9bac4966",
                    BoardId = 2
                },
                new Task()
                {
                    Title = "Create Tasks",
                    Description = "Implement [Create Task] page for adding tasks",
                    CreatedOn = DateTime.UtcNow.AddYears(-1),
                    OwnerId = "b671e676-ad8b-47aa-bc1e-a50f9bac4966",
                    BoardId = 3
                }
            };

            return tasks;
        }
    }
}
