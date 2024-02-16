using Homies.Areas.Contracts;
using Homies.Data;
using Homies.Data.Models;
using Homies.Models;
using Microsoft.EntityFrameworkCore;

namespace Homies.Services
{
    public class EventService : IEventService
    {
        private readonly HomiesDbContext dbContext;
        public EventService(HomiesDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddEventAsync(string organizerId, AddEventViewModel model)
        {
            Event currEvent = new Event()
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                Start = model.Start,
                End = model.End,
                TypeId = model.TypeId,
                OrganiserId = organizerId,
            };
            dbContext.Events.AddAsync(currEvent);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<AllEventsViewModel>> GetAllEventsAsync()
        {
            var allEventsViewModel = await this.dbContext.Events
                .Select(e => new AllEventsViewModel()
                {
                    Id = e.Id,
                    Name = e.Name,
                    Organiser = e.Organiser.UserName,
                    Start = e.Start,
                    Type = e.Type.Name
                }).ToListAsync();

            return allEventsViewModel;
        }

        public async Task<AddEventViewModel> GetEvenetsToAddedWithTypesAsync()
        {
           var types = await this.dbContext.Types
              .Select(t => new AllViewTypes() 
              { Id = t.Id, Name = t.Name})
              .ToListAsync();

            var model = new AddEventViewModel()
            {
                Types = types
            };
            return model;

        }
    }
}
