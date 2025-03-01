using Blog.Application.DTOs;
using Blog.Application.Interfaces.ReadRepositories;
using Blog.Infrastructure.Providers.Interfaces;
using Dapper;

namespace Blog.Infrastructure.ReadRepositories
{
    public class ReadCommentRepository(IDbConnectionFactory dbConnectionFactory) : IReadCommentRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;

        public async Task<IEnumerable<CommentDTO>> GetAllAsync()
        {
            using (var dbConnection = _dbConnectionFactory.CreateConnection())
            {
                var commentsDTO = await dbConnection.QueryAsync<CommentDTO>(@"
                        SELECT 
                            CommentId,
                            PostId,
                            AuthorName,
                            AuthorEmail,
                            Content,
                            CommentStatusId AS CommentStatus,
                            CreationDate
                        FROM Comments
                        ORDER BY CreationDate DESC");

                return commentsDTO;
            }
        }

        public async Task<CommentDTO?> GetByIdAsync(Guid id)
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
                        new { CommentId = id });

                return commentDTO;
            }
        }
    }
}
