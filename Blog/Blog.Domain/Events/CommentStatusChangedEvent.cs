using Blog.Domain.Core;
using Blog.Domain.Entities.PostAggregate;

namespace Blog.Domain.Events
{
    public class CommentStatusChangedEvent(Comment comment) : DomainEvent
    {
        public Comment Comment { get; } = comment;
    }
}
