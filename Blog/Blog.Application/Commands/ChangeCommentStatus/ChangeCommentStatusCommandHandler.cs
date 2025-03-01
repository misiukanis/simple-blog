using Blog.Application.Exceptions;
using Blog.Domain.Entities.PostAggregate;
using Blog.Domain.Interfaces.Persistence;
using Blog.Domain.Interfaces.Repositories;
using MediatR;

namespace Blog.Application.Commands.ChangeCommentStatus
{
    public class ChangeCommentStatusCommandHandler(
        IPostRepository postRepository, 
        IUnitOfWork unitOfWork) : IRequestHandler<ChangeCommentStatusCommand>
    {
        private readonly IPostRepository _postRepository = postRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task Handle(ChangeCommentStatusCommand request, CancellationToken cancellationToken)
        {
            var post = await _postRepository.GetByCommentIdAsync(request.CommentId);
            if (post == null)
            {
                throw new NotFoundException(nameof(Post), nameof(Comment.CommentId), request.CommentId);
            }

            post.ChangeCommentStatus(request.CommentId, request.CommentStatus);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
