using System.ComponentModel.DataAnnotations;

namespace Blog.Shared.DTOs
{
    public class CreatePostDTO
    {
        [Required]
        [StringLength(100)]
        public string Title { get; set; } = default!;

        [Required]
        [StringLength(100000)]
        public string Introduction { get; set; } = default!;

        [Required]
        [StringLength(10000000)]
        public string Content { get; set; } = default!;
    }
}
