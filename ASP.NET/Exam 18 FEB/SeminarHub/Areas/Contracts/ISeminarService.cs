using SeminarHub.Models;

namespace SeminarHub.Areas.Contracts
{
    public interface ISeminarService
    {
       
        //Task<IEnumerable<AllViewCategories>> GetAllCategories();
        Task AddSeminarAsync(AddSeminarViewModel model, string userId);
        Task<IEnumerable<AllSeminarlViewModel>> GetAllSerminarsAsync();
        Task<AddSeminarViewModel> GetSeminarCategoriesToAddedWithTypesAsync();
        Task<bool> GetSeminarIsJoinedByTheUserAsync(string userId, int seminarId);
        Task<bool> GetSeminarIfExists(int seminarId);
        Task AddSeminarToUserByIdAsync(string userId, int seminarId);
        Task RemoveSeminarOfUserColletionByIdAsync(string userId, int seminarId);
        Task<IEnumerable<AllSeminarJoinedViewModel>> GetAllSeminarsJoinedByUserIdAsync(string userId);
        Task<EditSeminarViewModel> GetSeminarForEditByIdAsync(int Id);

        Task EditSeminarAsync(EditSeminarViewModel model);
        Task<DetailSeminarViewModel> GetDetailsFoeSeminarByIdAsync(int seminarId);
        Task DeleteSeminarByIdAsync(int Id);

        Task<bool> GetIsUserIsCreatorOfSeminarByIdAsync(string userId, int seminarId);
    }
}
