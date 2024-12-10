using Blog.Domain.Enums;
using MediatR;

namespace Blog.Application.Commands.ChangeCommentStatus
{
    public class ChangeCommentStatusCommand(
        Guid commentId, 
        CommentStatus commentStatus) : IRequest
    {
        public Guid CommentId { get; } = commentId;
        public CommentStatus CommentStatus { get; } = commentStatus;
    }
}
