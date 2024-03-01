﻿using Blog.Shared.DTOs;
using Blog.Shared.Enums;
using Dapper;
using MediatR;
using System.Data;
using X.PagedList;

namespace Blog.Application.Queries.GetPaginatedPosts
{
    public class GetPaginatedPostsQueryHandler(IDbConnection dbConnection) : IRequestHandler<GetPaginatedPostsQuery, IPagedList<PostDTO>>
    {
        private readonly IDbConnection _dbConnection = dbConnection;

        public async Task<IPagedList<PostDTO>> Handle(GetPaginatedPostsQuery request, CancellationToken cancellationToken)
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

            var paginatedPostsDTO = new StaticPagedList<PostDTO>(postsDTO, request.Page, request.ItemsCountPerPage, totalItemsCount);
            return paginatedPostsDTO;
        }
    }
}
