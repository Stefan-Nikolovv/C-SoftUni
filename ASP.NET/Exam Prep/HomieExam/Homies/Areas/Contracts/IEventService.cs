using Homies.Models;

namespace Homies.Areas.Contracts
{
    public interface IEventService
    {
        Task<IEnumerable<AllEventsViewModel>> GetAllEventsAsync();
        Task<AddEventViewModel> GetEvenetsToAddedWithTypesAsync();
        Task AddEventAsync(string organizerId,AddEventViewModel model);
    }
}
