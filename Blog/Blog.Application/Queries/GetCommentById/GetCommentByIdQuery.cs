using Blog.Shared.DTOs;
using MediatR;

namespace Blog.Application.Queries.GetCommentById
{
    public class GetCommentByIdQuery(Guid commentId) : IRequest<CommentDTO?>
    {
        public Guid CommentId { get; } = commentId;
    }
}
