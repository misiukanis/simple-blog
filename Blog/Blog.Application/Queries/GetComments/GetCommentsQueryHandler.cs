﻿using Blog.Application.Providers.Interfaces;
using Dapper;
using MediatR;

namespace Blog.Application.Queries.GetComments
{
    public class GetCommentsQueryHandler(IDbConnectionFactory dbConnectionFactory) : IRequestHandler<GetCommentsQuery, IEnumerable<CommentDTO>>
    {
        private readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;

        public async Task<IEnumerable<CommentDTO>> Handle(GetCommentsQuery request, CancellationToken cancellationToken)
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
    }
}
