namespace Blog.Shared.DTOs
{
    public class PostDTO
    {
        public Guid PostId { get; set; }

        public string Title { get; set; }

        public string Introduction { get; set; }

        public string Content { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime ModificationDate { get; set; }

        public int CommentsCount { get; set; }
    }
}
