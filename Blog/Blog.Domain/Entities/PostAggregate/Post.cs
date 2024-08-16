using Blog.Domain.Core;
using Blog.Domain.Enums;
using Blog.Domain.Events;

namespace Blog.Domain.Entities.PostAggregate
{
    public class Post : Entity, IAggregateRoot
    {
        public Guid PostId { get; private set; }

        public string Title { get; private set; }

        public string Introduction { get; private set; }

        public string Content { get; private set; }

        public bool IsRemoved { get; private set; }

        public DateTime CreationDate { get; private set; }

        public DateTime ModificationDate { get; private set; }        

        public List<Comment> Comments { get; private set; }        

        public int CommentsCount => Comments.Count;


        private Post() // for Entity Framework
        {
            Comments = new List<Comment>();
        }

        public Post(Guid postId, string title, string introduction, string content)
        {
            PostId = postId;
            Title = title;
            Introduction = introduction;
            Content = content;
            IsRemoved = false;
            CreationDate = DateTime.UtcNow;
            ModificationDate = DateTime.UtcNow;
            Comments = new List<Comment>();
        }


        public void Edit(string title, string introduction, string content)
        {
            Title = title;
            Introduction = introduction;
            Content = content;
            ModificationDate = DateTime.UtcNow;
        }

        public void Remove()
        {
            IsRemoved = true;
        }

        public void AddComment(Guid commentId, string author, string content)
        {
            var comment = new Comment(commentId, author, content);
            Comments.Add(comment);

            AddDomainEvent(new CommentAddedEvent(comment));
        }

        public void ChangeCommentStatus(Guid commentId, CommentStatus commentStatus)
        {
            var comment = Comments.Single(x => x.CommentId == commentId);
            comment.ChangeStatus(commentStatus);
        }
    }
}
