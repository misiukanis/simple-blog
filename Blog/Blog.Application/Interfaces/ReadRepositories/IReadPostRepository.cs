using Blog.Application.DTOs;
using X.PagedList;

namespace Blog.Application.Interfaces.ReadRepositories
{
    public interface IReadPostRepository
    {
        Task<PostWithCommentsDTO?> GetWithCommentsByIdAsync(Guid id);
        Task<IPagedList<PostDTO>> GetPaginatedAsync(int page, int itemsCountPerPage, string? searchTerm);
    }
}
