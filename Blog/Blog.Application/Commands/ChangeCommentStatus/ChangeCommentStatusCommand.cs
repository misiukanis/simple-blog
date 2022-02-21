using Blog.Shared.Enums;
using MediatR;

namespace Blog.Application.Commands.ChangeCommentStatus
{
    public class ChangeCommentStatusCommand : IRequest
    {
        public Guid PostId { get; }
        public Guid CommentId { get; }        
        public CommentStatus CommentStatus { get; }

        public ChangeCommentStatusCommand(Guid postId, Guid commentId, CommentStatus commentStatus)
        {
            PostId = postId;
            CommentId = commentId;            
            CommentStatus = commentStatus;
        }
    }
}
