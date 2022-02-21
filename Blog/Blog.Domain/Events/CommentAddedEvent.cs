using Blog.Domain.Core;
using Blog.Domain.Entities.PostAggregate;

namespace Blog.Domain.Events
{
    public class CommentAddedEvent : DomainEvent
    {
        public Comment Comment { get; }

        public CommentAddedEvent(Comment comment)
        {
            Comment = comment;
        }
    }
}
