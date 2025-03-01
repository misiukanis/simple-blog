using Blog.Domain.Entities.PostAggregate;

namespace Blog.Domain.Interfaces.Repositories
{
    public interface IPostRepository
    {
        Task<Post?> GetByIdAsync(Guid id);
        Task<Post?> GetByCommentIdAsync(Guid commentId);
        Task AddAsync(Post post);
    }
}
