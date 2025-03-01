using Blog.Application.DTOs;
using Blog.Application.Interfaces.ReadRepositories;
using MediatR;

namespace Blog.Application.Queries.GetCommentById
{
    public class GetCommentByIdQueryHandler(IReadCommentRepository readCommentRepository) : IRequestHandler<GetCommentByIdQuery, CommentDTO?>
    {
        private readonly IReadCommentRepository _readCommentRepository = readCommentRepository;

        public async Task<CommentDTO?> Handle(GetCommentByIdQuery request, CancellationToken cancellationToken)
        {
            return await _readCommentRepository.GetByIdAsync(request.CommentId);
        }
    }
}
