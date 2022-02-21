using Blog.Domain.Core;
using Blog.Domain.Entities.PostAggregate;

namespace Blog.Domain.Events
{
    public class CommentStatusChangedEvent : DomainEvent
    {
        public Comment Comment { get; }

        public CommentStatusChangedEvent(Comment comment)
        {
            Comment = comment;
        }
    }
}
