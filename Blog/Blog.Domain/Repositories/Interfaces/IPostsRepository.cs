using Blog.Domain.Entities.PostAggregate;

namespace Blog.Domain.Repositories.Interfaces
{
    public interface IPostsRepository
    {
        Task<Post> GetByIdAsync(Guid id);
        Task AddAsync(Post post);
    }
}
