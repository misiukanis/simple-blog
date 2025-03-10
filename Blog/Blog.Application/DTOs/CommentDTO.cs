﻿using Blog.Domain.Enums;

namespace Blog.Application.DTOs
{
    public class CommentDTO
    {
        public Guid CommentId { get; set; }

        public Guid PostId { get; set; }

        public string AuthorName { get; set; } = default!;

        public string AuthorEmail { get; set; } = default!;

        public string Content { get; set; } = default!;

        public CommentStatus CommentStatus { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
