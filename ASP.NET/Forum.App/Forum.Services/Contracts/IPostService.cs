

using Forum.ViewModels.Post;

namespace Forum.Services.Contracts
{
    public interface IPostService
    {
        Task<IEnumerable<PostListViewModel>> ListAllAsync();
        Task addPostAsync(PostFormModel model);

        Task<PostFormModel> EditPostModelAsync(string id);
        Task EditByIdAsync(string id, PostFormModel post);
        Task DeleteByIdAsync(string id);
    } 
}
