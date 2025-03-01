using Blog.Application.Exceptions;
using Blog.Domain.Entities.PostAggregate;
using Blog.Domain.Interfaces.Persistence;
using Blog.Domain.Interfaces.Repositories;
using MediatR;

namespace Blog.Application.Commands.UpdatePost
{
    public class UpdatePostCommandHandler(
        IPostRepository postRepository, 
        IUnitOfWork unitOfWork) : IRequestHandler<UpdatePostCommand>
    {
        private readonly IPostRepository _postRepository = postRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task Handle(UpdatePostCommand request, CancellationToken cancellationToken)
        {
            var post = await _postRepository.GetByIdAsync(request.PostId);
            if(post == null)
            {
                throw new NotFoundException(nameof(Post), nameof(Post.PostId), request.PostId);
            }

            post.Edit(request.Title, request.Introduction, request.Content);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
