using AutoMapper;
using Blog.Common.Pagination;
using Blog.Domain.Entities.PostAggregate;
using Blog.Shared.DTOs;
using Dapper;
using MediatR;
using System.Data;

namespace Blog.Application.Queries.GetPaginatedPosts
{
    public class GetPaginatedPostsQueryHandler : IRequestHandler<GetPaginatedPostsQuery, PaginatedList<PostDTO>>
    {
        private readonly IDbConnection _dbConnection;
        private readonly IMapper _mapper;

        public GetPaginatedPostsQueryHandler(IDbConnection dbConnection, IMapper mapper)
        {
            _dbConnection = dbConnection;
            _mapper = mapper;
        }

        public async Task<PaginatedList<PostDTO>> Handle(GetPaginatedPostsQuery request, CancellationToken cancellationToken)
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

            IEnumerable<PostDTO> postsDTO = null;
            int totalItemsCount = 0;

            using (var multi = await _dbConnection.QueryMultipleAsync(sql,
                new
                {
                    SearchTerm = "%" + request.SearchTerm + "%",
                    CommentStatusId = (int)CommentStatus.Accepted,
                    Offset = (request.Page - 1) * request.ItemsCountPerPage,
                    PageSize = request.ItemsCountPerPage
                }))
            {
                postsDTO = multi.Read<PostDTO>().ToList();
                totalItemsCount = multi.ReadFirst<int>();
            }

            var totalPagesCount = (int)Math.Ceiling(totalItemsCount / (double)request.ItemsCountPerPage);
            var paginatedPostsDTO = new PaginatedList<PostDTO>(postsDTO, request.Page, request.ItemsCountPerPage, totalItemsCount, totalPagesCount);

            return paginatedPostsDTO;
        }
    }

}
