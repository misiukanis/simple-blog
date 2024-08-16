using System.ComponentModel.DataAnnotations;

namespace Blog.Shared.DTOs
{
    public class CreateCommentDTO
    {
        [Required]
        [StringLength(20)]
        public string Author { get; set; } = default!;

        [Required]
        [StringLength(100000)]
        public string Content { get; set; } = default!;
    }
}
