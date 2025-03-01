using Blog.Domain.Entities.PostAggregate;
using Blog.Domain.Interfaces.Persistence;
using Blog.Domain.Interfaces.Repositories;
using MediatR;

namespace Blog.Application.Commands.CreatePost
{
    public class CreatePostCommandHandler(
        IPostRepository postRepository, 
        IUnitOfWork unitOfWork) : IRequestHandler<CreatePostCommand>
    {
        private readonly IPostRepository _postRepository = postRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            var post = new Post(request.PostId, request.Title, request.Introduction, request.Content);

            await _postRepository.AddAsync(post);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
