using Blog.Application.Exceptions;
using Blog.Domain.Core;
using Blog.Domain.Entities.PostAggregate;
using Blog.Domain.Enums;
using Blog.Domain.Repositories.Interfaces;
using MediatR;

namespace Blog.Application.Commands.ChangeCommentStatus
{
    public class ChangeCommentStatusCommandHandler(
        IPostsRepository postsRepository, 
        IUnitOfWork unitOfWork) : IRequestHandler<ChangeCommentStatusCommand>
    {
        private readonly IPostsRepository _postsRepository = postsRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task Handle(ChangeCommentStatusCommand request, CancellationToken cancellationToken)
        {
            var post = await _postsRepository.GetByCommentIdAsync(request.CommentId);
            if (post == null)
            {
                throw new NotFoundException(nameof(Post), nameof(Comment.CommentId), request.CommentId);
            }

            post.ChangeCommentStatus(request.CommentId, (CommentStatus)request.CommentStatus);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
