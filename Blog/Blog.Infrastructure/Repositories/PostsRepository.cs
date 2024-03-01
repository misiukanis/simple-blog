using Blog.Domain.Entities.PostAggregate;
using Blog.Domain.Repositories.Interfaces;
using Blog.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Repositories
{
    public class PostsRepository(ApplicationDbContext applicationDbContext) : IPostsRepository
    {
        private readonly ApplicationDbContext _applicationDbContext = applicationDbContext;

        public async Task<Post> GetByIdAsync(Guid id)
        {
            var post = await _applicationDbContext.Posts
                .Include(x => x.Comments)
                .FirstOrDefaultAsync(x => x.PostId == id);

            return post;
        }

        public async Task<Post> GetByCommentIdAsync(Guid commentId)
        {
            var post = await _applicationDbContext.Posts
                .Include(x => x.Comments)
                .Where(x => x.Comments.Any(y => y.CommentId == commentId))
                .FirstOrDefaultAsync();

            return post;
        }

        public async Task AddAsync(Post post)
        {
            await _applicationDbContext.Posts.AddAsync(post);
        }
    }
}
