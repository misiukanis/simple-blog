using AutoMapper;
using Blog.Shared.DTOs;
using Dapper;
using MediatR;
using System.Data;

namespace Blog.Application.Queries.GetPaginatedPosts
{
    public class GetCommentsQueryHandler : IRequestHandler<GetCommentsQuery, IEnumerable<CommentDTO>>
    {
        private readonly IDbConnection _dbConnection;
        private readonly IMapper _mapper;

        public GetCommentsQueryHandler(IDbConnection dbConnection, IMapper mapper)
        {
            _dbConnection = dbConnection;
            _mapper = mapper;
        }

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
                        ORDER BY CreationDate desc");

            return commentsDTO;
        }
    }

}
