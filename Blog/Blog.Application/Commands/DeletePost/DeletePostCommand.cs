using MediatR;

namespace Blog.Application.Commands.DeletePost
{
    public class DeletePostCommand : IRequest
    {
        public Guid PostId { get; }

        public DeletePostCommand(Guid postId)
        {
            PostId = postId;
        }
    }
}
