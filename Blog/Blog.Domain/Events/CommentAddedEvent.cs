using Blog.Domain.Core;
using Blog.Domain.Entities.PostAggregate;

namespace Blog.Domain.Events
{
    public class CommentAddedEvent(Comment comment) : DomainEvent
    {
        public Comment Comment { get; } = comment;
    }
}
