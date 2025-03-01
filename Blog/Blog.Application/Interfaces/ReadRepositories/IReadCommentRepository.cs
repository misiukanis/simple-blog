using Blog.Application.DTOs;

namespace Blog.Application.Interfaces.ReadRepositories
{
    public interface IReadCommentRepository
    {
        Task<CommentDTO?> GetByIdAsync(Guid id);
        Task<IEnumerable<CommentDTO>> GetAllAsync();
    }
}
