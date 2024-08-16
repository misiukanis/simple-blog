using Blog.Application.Providers.Interfaces;
using Blog.Shared.DTOs;
using Blog.Shared.Enums;
using Dapper;
using MediatR;

namespace Blog.Application.Queries.GetPostById
{
    public class GetPostByIdQueryHandler(IDbConnectionFactory dbConnectionFactory) : IRequestHandler<GetPostByIdQuery, PostWithCommentsDTO?>
    {
        private readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;

        public async Task<PostWithCommentsDTO?> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
        {
            var sql = @"
                SELECT 
	                PostId,
	                Title,
	                Introduction,
	                Content,
	                CreationDate, 
	                ModificationDate
                FROM Posts
                WHERE PostId = @PostId AND IsRemoved = 0;

                SELECT 
	                CommentId,
                    PostId,
	                Author,
	                Content,
                    CommentStatusId AS CommentStatus,
	                CreationDate
                FROM Comments
                WHERE 
	                PostId = @PostId
	                AND CommentStatusId = @CommentStatusId
                ORDER BY CreationDate DESC;";

            PostWithCommentsDTO? postWithCommentsDTO = null;

            using (var dbConnection = _dbConnectionFactory.CreateConnection())
            {
                using (var multi = await dbConnection.QueryMultipleAsync(sql,
                    new
                    {
                        PostId = request.PostId,
                        CommentStatusId = (int)CommentStatus.Accepted
                    }))
                {
                    postWithCommentsDTO = multi.Read<PostWithCommentsDTO>().SingleOrDefault();
                    if (postWithCommentsDTO != null)
                    {
                        postWithCommentsDTO.Comments = multi.Read<PostWithCommentsDTO.CommentDTO>().ToList();
                    }
                }
            }

            return postWithCommentsDTO;
        }
    }
}
