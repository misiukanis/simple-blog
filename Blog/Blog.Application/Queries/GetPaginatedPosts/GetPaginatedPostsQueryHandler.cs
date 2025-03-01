using Blog.Application.DTOs;
using Blog.Application.Interfaces.ReadRepositories;
using MediatR;
using X.PagedList;

namespace Blog.Application.Queries.GetPaginatedPosts
{
    public class GetPaginatedPostsQueryHandler(IReadPostRepository readPostRepository) : IRequestHandler<GetPaginatedPostsQuery, IPagedList<PostDTO>>
    {
        private readonly IReadPostRepository _readPostRepository = readPostRepository;

        public async Task<IPagedList<PostDTO>> Handle(GetPaginatedPostsQuery request, CancellationToken cancellationToken)
        {
            return await _readPostRepository.GetPaginatedAsync(request.Page, request.ItemsCountPerPage, request.SearchTerm);
        }
    }
}
