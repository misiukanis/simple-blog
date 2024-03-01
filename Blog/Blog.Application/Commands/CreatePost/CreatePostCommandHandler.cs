using Blog.Domain.Core;
using Blog.Domain.Entities.PostAggregate;
using Blog.Domain.Repositories.Interfaces;
using MediatR;

namespace Blog.Application.Commands.CreatePost
{
    public class CreatePostCommandHandler(
        IPostsRepository postsRepository, 
        IUnitOfWork unitOfWork) : IRequestHandler<CreatePostCommand>
    {
        private readonly IPostsRepository _postsRepository = postsRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            var post = new Post(request.PostId, request.Title, request.Introduction, request.Content);

            await _postsRepository.AddAsync(post);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
