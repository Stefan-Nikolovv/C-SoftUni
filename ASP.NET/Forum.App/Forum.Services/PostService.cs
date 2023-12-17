using Forum.Data;
using Forum.Data.Models;
using Forum.Services.Contracts;
using Forum.ViewModels.Post;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Services
{
    public class PostService : IPostService
    {
        private readonly ForumDbContext dbContext;

        public PostService(ForumDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task addPostAsync(PostFormModel model)
        {
           Post post = new Post()
           {
               Title = model.Title,
               Content = model.Content,
           };

         await   this.dbContext.Posts.AddAsync(post);
         await  this.dbContext.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(string id)
        {
            Post postToDelete = await this.dbContext.Posts.FirstAsync(x => x.Id.ToString() == id);
            this.dbContext.Posts.Remove(postToDelete);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task EditByIdAsync(string id, PostFormModel post)
        {
            Post edintPost = await dbContext.Posts.FirstAsync(x => x.Id.ToString() == id);
            edintPost.Title = post.Title;
            edintPost.Content = post.Content;
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<PostFormModel> EditPostModelAsync(string id)
        {
            Post current = await dbContext.Posts.FirstAsync(p => p.Id.ToString() == id);
            return new PostFormModel()
            {
                Title = current.Title,
                Content = current.Content,
            };
        }

        public async Task<IEnumerable<PostListViewModel>> ListAllAsync()
        {
            IEnumerable<PostListViewModel> allPosts = await dbContext.Posts
                .Select(p => new PostListViewModel()
                {
                    Id = p.Id.ToString(),
                    Title = p.Title,
                    Content = p.Content,
                }).ToArrayAsync();
             return allPosts;
        }
    }
}
