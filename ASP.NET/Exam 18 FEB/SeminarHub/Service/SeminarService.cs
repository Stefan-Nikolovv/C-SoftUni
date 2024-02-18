using Microsoft.CodeAnalysis.Differencing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SeminarHub.Areas.Contracts;
using SeminarHub.Data;
using SeminarHub.Data.Models;
using SeminarHub.Models;

namespace SeminarHub.Service
{
    public class SeminarService : ISeminarService
    {
        private readonly SeminarHubDbContext dbContext;

        public SeminarService(SeminarHubDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddSeminarAsync(AddSeminarViewModel model, string userId)
        {
            Seminar seminar = new Seminar()
            {
                Topic = model.Topic,
                Lecturer = model.Lecturer,
                Details = model.Details,
                DateAndTime = DateTime.Parse(model.DateAndTime),
                Duration = (int)model.Duration,
                CategoryId = model.CategoryId,
                
                OrganizerId = userId,
            };
            dbContext.Seminars.Add(seminar);
            await dbContext.SaveChangesAsync();
        }

        public async Task AddSeminarToUserByIdAsync(string userId, int seminarId)
        {
            var model = await this.dbContext.Seminars
                            .Where(s => s.Id == seminarId)
                            .Include(sp => sp.SeminarsParticipants)
                            .FirstOrDefaultAsync();
            if(model == null)
            {
                return;
            }

            model.SeminarsParticipants.Add(new SeminarParticipant()
            {
                SeminarId = seminarId,
                ParticipantId = userId,
            });
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteSeminarByIdAsync(int Id)
        {
            var seminarToDelete = await this.dbContext.Seminars
            .Include(s => s.SeminarsParticipants)
            .FirstOrDefaultAsync(s => s.Id == Id);
            this.dbContext.SeminarParticipants.RemoveRange(seminarToDelete.SeminarsParticipants);

            // Remove the Seminar from the table
            dbContext.Seminars.Remove(seminarToDelete);
            await dbContext.SaveChangesAsync();

            
    }

        public async Task EditSeminarAsync(EditSeminarViewModel model)
        {
           var serachedPost = await this.dbContext.Seminars.FindAsync(model.Id);

            if(serachedPost != null)
            {
                serachedPost.Id = model.Id;
                serachedPost.Topic = model.Topic;
                serachedPost.Lecturer = model.Lecturer;
                serachedPost.Details = model.Details;
                serachedPost.DateAndTime = DateTime.Parse(model.DateAndTime);
                serachedPost.Duration = model.Duration;
                serachedPost.CategoryId = model.CategoryId;
            };
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<AllSeminarJoinedViewModel>> GetAllSeminarsJoinedByUserIdAsync(string userId)
        {
            var allSeminarsJoind = await this.dbContext.Seminars
                                       .Include(sp => sp.SeminarsParticipants)
                                       .Where(sp => sp.SeminarsParticipants.Any(u => u.ParticipantId == userId))
                                       .Select(m => new AllSeminarJoinedViewModel()
                                       {
                                           Id = m.Id,
                                           Topic = m.Topic,
                                           DateAndTime = m.DateAndTime.ToString(),
                                           Lecturer = m.Lecturer,
                                           Organizer = m.Organizer.UserName
                                       })
                                       .ToListAsync();
            return allSeminarsJoind;
        }

        public async Task<IEnumerable<AllSeminarlViewModel>> GetAllSerminarsAsync()
        {
            var allSeminars = await this.dbContext.Seminars
                                .Select(s => new AllSeminarlViewModel()
                                {
                                    Id = s.Id,
                                    Topic = s.Topic,
                                    Lecturer = s.Lecturer,
                                    DateAndTime = s.DateAndTime.ToString(),
                                    
                                    Organizer = s.Organizer.UserName
                                })
                                .ToListAsync();
            return allSeminars;
        }

        public async Task<DetailSeminarViewModel> GetDetailsFoeSeminarByIdAsync(int seminarId)
        {

            var serachedSeminar = await this.dbContext.Seminars
                                        .Where(x => x.Id == seminarId)
                                        .Select(m => new DetailSeminarViewModel()
                                        {
                                            Id = m.Id,
                                            Topic = m.Topic,
                                            Lecturer = m.Lecturer,
                                            DateAndTime = m.DateAndTime.ToString(),
                                            Organizer = m.Organizer.UserName,
                                            Details = m.Details,
                                            Duration = m.Duration,
                                            Category = m.Category.Name
                                        })
                                        .FirstOrDefaultAsync();

           
            return serachedSeminar;
        }

        public async Task<bool> GetIsUserIsCreatorOfSeminarByIdAsync(string userId, int seminarId)
        {
           var seminar =  await this.dbContext.Seminars
                .Include(s => s.Organizer)
                .FirstOrDefaultAsync(s => s.Id == seminarId);

            if(seminar == null)
            {
                return false;
            }
            return seminar.OrganizerId == userId;
        }

        public async Task<AddSeminarViewModel> GetSeminarCategoriesToAddedWithTypesAsync()
        {
            var categories = await this.dbContext.Categories
               .Select(t => new AllViewCategories()
               { Id = t.Id, Name = t.Name })
               .ToListAsync();

            var model = new AddSeminarViewModel()
            {
                Categories = categories
            };
            return model;
        }

        public async Task<EditSeminarViewModel> GetSeminarForEditByIdAsync(int Id)
        {
            
            var postToEdit = await this.dbContext.Seminars
                                    .Where(m => m.Id == Id)
                                    .Select(m => new EditSeminarViewModel() 
                                    { 
                                        Id = m.Id,
                                        Topic = m.Topic,
                                        Lecturer = m.Lecturer,
                                        Details = m.Details,
                                        DateAndTime= m.DateAndTime.ToString(),
                                        Duration = m.Duration,
                                        CategoryId = m.CategoryId,
                                    }).FirstOrDefaultAsync();

            var categories = await this.dbContext.Categories
              .Select(t => new AllViewCategories()
              { Id = t.Id, Name = t.Name })
              .ToListAsync();

            postToEdit.Categories = categories;

            return postToEdit;

        }

        public async Task<bool> GetSeminarIfExists(int seminarId)
        {
            return await this.dbContext.Seminars
                       .AnyAsync(x => x.Id == seminarId);
        }

        public async Task<bool> GetSeminarIsJoinedByTheUserAsync(string userId, int seminarId)
        {
            bool isJoined = await this.dbContext.Seminars
                            .Where(x => x.Id == seminarId)
                            .AnyAsync(x => x.SeminarsParticipants.Any(p => p.ParticipantId == userId));

            return isJoined;
        }

        public async Task RemoveSeminarOfUserColletionByIdAsync(string userId, int seminarId)
        {
            var model = await this.dbContext.Seminars
                              .Where(s => s.Id == seminarId)
                              .Include(sp => sp.SeminarsParticipants)
                              .FirstOrDefaultAsync();
            if (model == null)
            {
                return;
            }

            var serachedSeminar = model.SeminarsParticipants.FirstOrDefault(u => u.ParticipantId == userId);

            if(serachedSeminar == null)
            {
                return;
            }
            model.SeminarsParticipants.Remove(serachedSeminar);

            await dbContext.SaveChangesAsync();
        }
    }
}


