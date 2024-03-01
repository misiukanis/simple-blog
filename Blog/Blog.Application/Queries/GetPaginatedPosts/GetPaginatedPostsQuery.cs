using Blog.Shared.DTOs;
using MediatR;
using X.PagedList;

namespace Blog.Application.Queries.GetPaginatedPosts
{
    public class GetPaginatedPostsQuery(
        int page, 
        int itemsCountPerPage, 
        string? searchTerm) : IRequest<IPagedList<PostDTO>>
    {
        public int Page { get; } = page;
        public int ItemsCountPerPage { get; } = itemsCountPerPage;
        public string? SearchTerm { get; } = searchTerm;
    }
}
