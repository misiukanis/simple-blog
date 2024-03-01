using Blog.Shared.DTOs;
using Dapper;
using MediatR;
using System.Data;

namespace Blog.Application.Queries.GetCommentById
{
    public class GetCommentByIdQueryHandler(IDbConnection dbConnection) : IRequestHandler<GetCommentByIdQuery, CommentDTO?>
    {
        private readonly IDbConnection _dbConnection = dbConnection;

        public async Task<CommentDTO?> Handle(GetCommentByIdQuery request, CancellationToken cancellationToken)
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
                        WHERE CommentId = @CommentId", 
                        new { CommentId = request.CommentId });

            return commentDTO;
        }
    }
}
