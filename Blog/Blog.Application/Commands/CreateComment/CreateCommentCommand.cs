using MediatR;

namespace Blog.Application.Commands.CreateComment
{
    public class CreateCommentCommand(
        Guid postId, 
        Guid commentId,
        string authorName,
        string authorEmail,
        string content) : IRequest
    {
        public Guid PostId { get; } = postId;
        public Guid CommentId { get; } = commentId;
        public string AuthorName { get; } = authorName;
        public string AuthorEmail { get; } = authorEmail;
        public string Content { get; } = content;
    }
}
