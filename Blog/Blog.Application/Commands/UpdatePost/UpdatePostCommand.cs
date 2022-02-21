using MediatR;

namespace Blog.Application.Commands.UpdatePost
{
    public class UpdatePostCommand : IRequest
    {
        public Guid PostId { get; }
        public string Title { get; }
        public string Introduction { get; }
        public string Content { get; }

        public UpdatePostCommand(Guid postId, string title, string introduction, string content)
        {
            PostId = postId;
            Title = title;
            Introduction = introduction;
            Content = content;
        }
    }
}
