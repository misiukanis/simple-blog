using MediatR;

namespace Blog.Application.Queries.GetComments
{
    public class GetCommentsQuery : IRequest<IEnumerable<CommentDTO>>
    {
    }
}
