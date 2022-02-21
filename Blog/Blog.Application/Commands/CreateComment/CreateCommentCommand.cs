using MediatR;

namespace Blog.Application.Commands.CreateComment
{
    public class CreateCommentCommand : IRequest
    {
        public Guid PostId { get; }
        public Guid CommentId { get; }        
        public string Author { get; }
        public string Content { get; }

        public CreateCommentCommand(Guid postId, Guid commentId, string author, string content)
        {
            PostId = postId;
            CommentId = commentId;            
            Author = author;
            Content = content;
        }
    }
}
