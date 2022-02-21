using Blog.Application.Exceptions;
using Blog.Domain.Core;
using Blog.Domain.Repositories.Interfaces;
using MediatR;

namespace Blog.Application.Commands.ChangeCommentStatus
{
    public class ChangeCommentStatusCommandHandler : IRequestHandler<ChangeCommentStatusCommand>
    {
        private readonly IPostsRepository _postsRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ChangeCommentStatusCommandHandler(IPostsRepository postsRepository, IUnitOfWork unitOfWork)
        {
            _postsRepository = postsRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(ChangeCommentStatusCommand request, CancellationToken cancellationToken)
        {
            var post = await _postsRepository.GetByIdAsync(request.PostId);
            if (post == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.PostAggregate.Post), request.PostId);
            }

            if (!post.Comments.Any(x => x.CommentId == request.CommentId))
            {
                throw new NotFoundException(nameof(Domain.Entities.PostAggregate.Comment), request.CommentId);
            }

            post.ChangeCommentStatus(request.CommentId, (Domain.Entities.PostAggregate.CommentStatus)request.CommentStatus);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
