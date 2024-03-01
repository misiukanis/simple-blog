using Blog.Shared.DTOs;
using Dapper;
using MediatR;
using System.Data;

namespace Blog.Application.Queries.GetComments
{
    public class GetCommentsQueryHandler(IDbConnection dbConnection) : IRequestHandler<GetCommentsQuery, IEnumerable<CommentDTO>>
    {
        private readonly IDbConnection _dbConnection = dbConnection;

        public async Task<IEnumerable<CommentDTO>> Handle(GetCommentsQuery request, CancellationToken cancellationToken)
        {
            var commentsDTO = await _dbConnection.QueryAsync<CommentDTO>(@"
                        SELECT 
                            CommentId,
                            PostId,
                            Author,
                            Content,
                            CommentStatusId AS CommentStatus,
                            CreationDate
                        FROM Comments
                        ORDER BY CreationDate DESC");
            
            return commentsDTO;
        }
    }
}
