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

        public async Task AddEventToUserByIdAsync( string userId , int eventId)
        {

           var model = await this.dbContext.Events
                            .Where(x => x.Id == eventId)
                            .Include(x => x.EventsParticipants)
                            .FirstOrDefaultAsync();
            if (model == null)
            {
                return;
            }

           model.EventsParticipants.Add(new EventParticipant()
            {
                EventId = eventId,
                HelperId = userId
            });

            await dbContext.SaveChangesAsync();

        }

        public async Task<DetailsEventViewModel> GetDetailsEventByIdAsync(int eventId)
        {
            
           var model =  await this.dbContext.Events
                          .Where(x => x.Id == eventId)
                          .Select(x => new DetailsEventViewModel()
                          {
                              Id = x.Id,
                              Name= x.Name,
                              Description = x.Description,
                              Start = x.Start,
                              End = x.End,
                              CreatedOn = x.CreatedOn,
                              Organiser = x.Organiser.UserName,
                              Type = x.Type.Name
                          }).FirstOrDefaultAsync();

           
            return model;
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

        public async Task<bool> GetEventIfExists(int eventId)
        {
            return await this.dbContext.Events
                        .AnyAsync(x => x.Id == eventId);

        }

        public async Task<bool> GetEventIsJoinedByTheUserAsync(string userId, int eventId)
        {
            
            bool isJoined = await this.dbContext.Events
                            .Where(x => x.Id == eventId)
                            .AnyAsync(x => x.EventsParticipants.Any( x=> x.HelperId == userId));

            return isJoined;
        }

        public async Task<IEnumerable<AllEventsViewModel>> GetJoinedEventsByIdAsync(string userId)
        {
            var allEventsViewModel = await this.dbContext.Events
                .Include(x => x.EventsParticipants)
                .Where(x => x.EventsParticipants.Any(x => x.HelperId == userId))
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

        public async Task RemoveEventToUserByIdAsync(string userId, int eventId)
        {
            var model = await this.dbContext.Events
                           .Where(x => x.Id == eventId)
                           .Include(x => x.EventsParticipants)
                           .FirstOrDefaultAsync();
            if (model == null)
            {
                return;
            }
           

            var serchedParticipant = model.EventsParticipants.FirstOrDefault( x => x.HelperId == userId);

            if(serchedParticipant == null) {
                return;
            }
            model.EventsParticipants.Remove(serchedParticipant);


            await dbContext.SaveChangesAsync();
        }

        public async Task<EditEventViewModel> GetEventForEditByIdAsync(int eventId)
        {
            var edit = await this.dbContext.Events
                        .Where(x => x.Id == eventId)
                        .Select(x => new EditEventViewModel()
                        {
                            Id = x.Id,
                            Name = x.Name,
                            Description = x.Description,
                            End = x.End.ToString(),
                            Start = x.Start.ToString(),
                            TypeId = x.TypeId,
                        }).FirstOrDefaultAsync();

            var types = await this.dbContext.Types
               .Select(t => new AllViewTypes()
               { Id = t.Id, Name = t.Name })
               .ToListAsync();

            edit.Types = types;

            return edit;

        }



        public async Task EditEventAsync(EditEventViewModel model)
        {
            var editPost = await this.dbContext.Events
                       .FindAsync(model.Id);

            if(editPost != null)
            {
                editPost.Name = model.Name;
               editPost.Description = model.Description;
               editPost.Start.ToString() = model.Start.ToString();
                editPost.End = DateTime.Parse(model.End) ;
                 editPost.TypeId = model.TypeId;
            }
          
            await dbContext.SaveChangesAsync();
                        
        }
    }
}
