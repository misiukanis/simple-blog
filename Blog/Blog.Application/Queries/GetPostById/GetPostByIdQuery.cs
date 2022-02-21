using Blog.Shared.DTOs;
using MediatR;

namespace Blog.Application.Queries.GetPostById
{
    public class GetPostByIdQuery : IRequest<PostWithCommentsDTO>
    {
        public Guid PostId { get; }

        public GetPostByIdQuery(Guid postId)
        {
            PostId = postId;
        }
    }
}
