using Blog.Domain.Core;
using Blog.Domain.Enums;
using Blog.Domain.Events;
using Blog.Domain.Exceptions;
using Blog.Domain.ValueObjects;

namespace Blog.Domain.Entities.PostAggregate
{
    public class Comment : Entity
    {
        public Guid CommentId { get; private set; }

        public Author Author { get; private set; }

        public string Content { get; private set; }

        public DateTime CreationDate { get; private set; }

        public DateTime ModificationDate { get; private set; }

        public CommentStatus CommentStatus { get; private set; }


        private Comment() // for Entity Framework
        {
        }

        public Comment(Guid commentId, Author author, string content)
        {
            CommentId = commentId;
            Author = author;
            Content = content;
            CommentStatus = CommentStatus.New;
            CreationDate = DateTime.UtcNow;
            ModificationDate = DateTime.UtcNow; 
        }


        public void ChangeStatus(CommentStatus commentStatus)
        {
            if (CommentStatus == commentStatus)
            {
                throw new DomainException("The status cannot be changed to the same one");
            }

            if (commentStatus == CommentStatus.New)
            {
                throw new DomainException("The status cannot be changed to New");
            }

            CommentStatus = commentStatus;

            AddDomainEvent(new CommentStatusChangedEvent(this));
        }
    }
}
