using Blog.Shared.DTOs;
using MediatR;

namespace Blog.Application.Queries.GetPostById
{
    public class GetPostByIdQuery(Guid postId) : IRequest<PostWithCommentsDTO?>
    {
        public Guid PostId { get; } = postId;
    }
}
