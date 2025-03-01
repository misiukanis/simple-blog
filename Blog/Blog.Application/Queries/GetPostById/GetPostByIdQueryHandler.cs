using Blog.Application.DTOs;
using Blog.Application.Interfaces.ReadRepositories;
using MediatR;

namespace Blog.Application.Queries.GetPostById
{
    public class GetPostByIdQueryHandler(IReadPostRepository readPostRepository) : IRequestHandler<GetPostByIdQuery, PostWithCommentsDTO?>
    {
        private readonly IReadPostRepository _readPostRepository = readPostRepository;

        public async Task<PostWithCommentsDTO?> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
        {
            return await _readPostRepository.GetWithCommentsByIdAsync(request.PostId);
        }
    }
}
