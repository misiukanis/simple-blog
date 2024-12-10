using Blog.Application.Providers.Interfaces;
using Blog.Application.Queries.GetComments;
using Dapper;
using MediatR;

namespace Blog.Application.Queries.GetCommentById
{
    public class GetCommentByIdQueryHandler(IDbConnectionFactory dbConnectionFactory) : IRequestHandler<GetCommentByIdQuery, CommentDTO?>
    {
        private readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;

        public async Task<CommentDTO?> Handle(GetCommentByIdQuery request, CancellationToken cancellationToken)
        {
            using (var dbConnection = _dbConnectionFactory.CreateConnection())
            {
                var commentDTO = await dbConnection.QuerySingleOrDefaultAsync<CommentDTO>(@"
                        SELECT 
                            CommentId,
                            PostId,
                            AuthorName,
                            AuthorEmail,
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
}
