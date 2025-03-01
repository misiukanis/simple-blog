using Blog.Application.DTOs;
using Blog.Application.Interfaces.ReadRepositories;
using Blog.Domain.Enums;
using Blog.Infrastructure.Providers.Interfaces;
using Dapper;
using X.PagedList;

namespace Blog.Infrastructure.ReadRepositories
{
    public class ReadPostRepository(IDbConnectionFactory dbConnectionFactory) : IReadPostRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;

        public async Task<IPagedList<PostDTO>> GetPaginatedAsync(int page, int itemsCountPerPage, string? searchTerm)
        {
            var sql = @"
                        SELECT 
                            PostId,
		                    Title,
		                    Introduction,
		                    Content,
		                    CreationDate, 
		                    ModificationDate,
	                        (SELECT Count(*) FROM Comments AS comments WHERE comments.PostId = posts.PostId AND comments.CommentStatusId = @CommentStatusId) AS CommentsCount
                        FROM Posts AS posts
                        WHERE (Title LIKE @SearchTerm OR Introduction LIKE @SearchTerm OR Content LIKE @SearchTerm) AND IsRemoved = 0
                        ORDER BY CreationDate DESC
                        OFFSET @Offset ROWS
                        FETCH NEXT @PageSize ROWS ONLY;

                        SELECT 
                            Count(*)
                        FROM Posts AS posts
                        WHERE (Title LIKE @SearchTerm OR Introduction LIKE @SearchTerm OR Content LIKE @SearchTerm) AND IsRemoved = 0;";

            IEnumerable<PostDTO>? postsDTO = null;
            int totalItemsCount = 0;

            using (var dbConnection = _dbConnectionFactory.CreateConnection())
            {
                using (var multi = await dbConnection.QueryMultipleAsync(sql,
                    new
                    {
                        SearchTerm = "%" + searchTerm + "%",
                        CommentStatusId = (int)CommentStatus.Accepted,
                        Offset = (page - 1) * itemsCountPerPage,
                        PageSize = itemsCountPerPage
                    }))
                {
                    postsDTO = multi.Read<PostDTO>().ToList();
                    totalItemsCount = multi.ReadFirst<int>();
                }
            }
            var paginatedPostsDTO = new StaticPagedList<PostDTO>(postsDTO, page, itemsCountPerPage, totalItemsCount);
            return paginatedPostsDTO;
        }

        public async Task<PostWithCommentsDTO?> GetWithCommentsByIdAsync(Guid id)
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
	                AuthorName,
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
                        PostId = id,
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
