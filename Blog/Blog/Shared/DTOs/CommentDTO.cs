using Blog.Shared.Enums;

namespace Blog.Shared.DTOs
{
    public class CommentDTO
    {
        public Guid CommentId { get; set; }

        public Guid PostId { get; set; }

        public string Author { get; set; }

        public string Content { get; set; }

        public CommentStatus CommentStatus { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
