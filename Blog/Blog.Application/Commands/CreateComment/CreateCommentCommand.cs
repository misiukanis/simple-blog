using MediatR;

namespace Blog.Application.Commands.CreateComment
{
    public class CreateCommentCommand(
        Guid postId, 
        Guid commentId, 
        string author, 
        string content) : IRequest
    {
        public Guid PostId { get; } = postId;
        public Guid CommentId { get; } = commentId;
        public string Author { get; } = author;
        public string Content { get; } = content;
    }
}
