using Blog.Shared.DTOs;
using MediatR;

namespace Blog.Application.Queries.GetCommentById
{
    public class GetCommentByIdQuery : IRequest<CommentDTO>
    {
        public Guid PostId { get; }
        public Guid CommentId { get; }

        public GetCommentByIdQuery(Guid postId, Guid commentId)
        {
            PostId = postId;
            CommentId = commentId;
        }
    }
}
