namespace Blog.Shared.DTOs
{
    public class PostDTO
    {
        public Guid PostId { get; set; }

        public string Title { get; set; } = default!;

        public string Introduction { get; set; } = default!;

        public string Content { get; set; } = default!;

        public DateTime CreationDate { get; set; }

        public DateTime ModificationDate { get; set; }

        public int CommentsCount { get; set; }
    }
}
