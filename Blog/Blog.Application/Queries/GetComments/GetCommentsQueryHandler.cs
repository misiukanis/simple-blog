using Blog.Application.DTOs;
using Blog.Application.Interfaces.ReadRepositories;
using MediatR;

namespace Blog.Application.Queries.GetComments
{
    public class GetCommentsQueryHandler(IReadCommentRepository readCommentRepository) : IRequestHandler<GetCommentsQuery, IEnumerable<CommentDTO>>
    {
        private readonly IReadCommentRepository _readCommentRepository = readCommentRepository;

        public async Task<IEnumerable<CommentDTO>> Handle(GetCommentsQuery request, CancellationToken cancellationToken)
        {
            return await _readCommentRepository.GetAllAsync();
        }
    }
}
