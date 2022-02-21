using Blog.Domain.Entities.PostAggregate;
using Blog.Domain.Repositories.Interfaces;
using Blog.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Repositories
{
    public class PostsRepository : IPostsRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public PostsRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Post> GetByIdAsync(Guid id)
        {
            var post = await _applicationDbContext.Posts
                .Include(x => x.Comments)
                .SingleOrDefaultAsync(x => x.PostId == id);

            return post;
        }

        public async Task AddAsync(Post post)
        {
            await _applicationDbContext.Posts.AddAsync(post);
        }
    }
}
