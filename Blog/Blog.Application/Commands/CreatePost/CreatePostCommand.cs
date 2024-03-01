using MediatR;

namespace Blog.Application.Commands.CreatePost
{
    public class CreatePostCommand(
        Guid postId, 
        string title, 
        string introduction, 
        string content) : IRequest
    {
        public Guid PostId { get; } = postId;
        public string Title { get; } = title;
        public string Introduction { get; } = introduction;
        public string Content { get; } = content;
    }
}
