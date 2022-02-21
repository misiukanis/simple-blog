using Blog.Common.Pagination;
using Blog.Shared.DTOs;
using MediatR;

namespace Blog.Application.Queries.GetPaginatedPosts
{
    public class GetPaginatedPostsQuery : IRequest<PaginatedList<PostDTO>>
    {
        public int Page { get; }
        public int ItemsCountPerPage { get; }
        public string SearchTerm { get; }

        public GetPaginatedPostsQuery(int page, int itemsCountPerPage, string searchTerm)
        {
            Page = page;
            ItemsCountPerPage = itemsCountPerPage;
            SearchTerm = searchTerm;
        }
    }
}
