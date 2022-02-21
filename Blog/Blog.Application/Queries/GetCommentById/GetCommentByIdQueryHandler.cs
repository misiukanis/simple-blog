using AutoMapper;
using Blog.Shared.DTOs;
using Dapper;
using MediatR;
using System.Data;

namespace Blog.Application.Queries.GetCommentById
{
    public class GetCommentByIdQueryHandler : IRequestHandler<GetCommentByIdQuery, CommentDTO>
    {
        private readonly IDbConnection _dbConnection;
        private readonly IMapper _mapper;

        public GetCommentByIdQueryHandler(IDbConnection dbConnection, IMapper mapper)
        {
            _dbConnection = dbConnection;
            _mapper = mapper;
        }

        public async Task<CommentDTO> Handle(GetCommentByIdQuery request, CancellationToken cancellationToken)
        {
            var commentDTO = await _dbConnection.QuerySingleOrDefaultAsync<CommentDTO>(@"
                        SELECT 
                            CommentId,
                            PostId,
                            Author,
                            Content,
                            CommentStatusId AS CommentStatus,
                            CreationDate
                        FROM Comments
                        WHERE PostId = @PostId AND CommentId = @CommentId", 
                        new { PostId = request.PostId, CommentId = request.CommentId });

            return commentDTO;
        }
    }

}
