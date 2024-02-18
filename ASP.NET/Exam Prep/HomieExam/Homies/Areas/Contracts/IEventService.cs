using Homies.Models;

namespace Homies.Areas.Contracts
{
    public interface IEventService
    {
        Task<IEnumerable<AllEventsViewModel>> GetAllEventsAsync();
        Task<AddEventViewModel> GetEvenetsToAddedWithTypesAsync();
        Task AddEventAsync(string organizerId,AddEventViewModel model);
        Task<IEnumerable<AllEventsViewModel>> GetJoinedEventsByIdAsync(string userId);
        Task<bool> GetEventIsJoinedByTheUserAsync(string userId, int eventId);
        Task<bool> GetEventIfExists(int eventId);
        Task AddEventToUserByIdAsync( string userId, int evetnId);
        Task RemoveEventToUserByIdAsync(string userId, int evetnId);

        Task<DetailsEventViewModel> GetDetailsEventByIdAsync(int eventId);

        Task<EditEventViewModel> GetEventForEditByIdAsync(int eventId);

        Task EditEventAsync(EditEventViewModel model);
    }
}
