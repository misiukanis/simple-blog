using Blog.Domain.Enums;

namespace Blog.Application.Queries.GetPostById
{
    public class PostWithCommentsDTO
    {
        public Guid PostId { get; set; }

        public string Title { get; set; } = default!;

        public string Introduction { get; set; } = default!;

        public string Content { get; set; } = default!;

        public DateTime CreationDate { get; set; }

        public DateTime ModificationDate { get; set; }

        public IEnumerable<CommentDTO> Comments { get; set; } = new List<CommentDTO>();


        public class CommentDTO
        {
            public Guid CommentId { get; set; }

            public Guid PostId { get; set; }

            public string AuthorName { get; set; } = default!;

            public string Content { get; set; } = default!;

            public CommentStatus CommentStatus { get; set; }

            public DateTime CreationDate { get; set; }
        }
    }
}
