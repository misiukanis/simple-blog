using Blog.Shared.DTOs;
using Blog.Shared.Enums;
using Dapper;
using MediatR;
using System.Data;

namespace Blog.Application.Queries.GetPostById
{
    public class GetPostByIdQueryHandler(IDbConnection dbConnection) : IRequestHandler<GetPostByIdQuery, PostWithCommentsDTO?>
    {
        private readonly IDbConnection _dbConnection = dbConnection;

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
            
            using (var multi = await _dbConnection.QueryMultipleAsync(sql, 
                new { 
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

            return postWithCommentsDTO;
        }
    }
}
