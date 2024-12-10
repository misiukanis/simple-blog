using MediatR;

namespace Blog.Application.Commands.DeletePost
{
    public class DeletePostCommand(Guid postId) : IRequest
    {
        public Guid PostId { get; } = postId;
    }
}
