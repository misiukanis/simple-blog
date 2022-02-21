using Blog.Shared.DTOs;
using MediatR;

namespace Blog.Application.Queries.GetPaginatedPosts
{
    public class GetCommentsQuery : IRequest<IEnumerable<CommentDTO>>
    {
        public GetCommentsQuery()
        {
        }
    }
}
