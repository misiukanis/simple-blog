using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Blog.Shared.DTOs
{
    public class CreateCommentDTO
    {
        [Required]
        [StringLength(20)]
        [Display(Name = "Name")]
        public string AuthorName { get; set; } = default!;

        [Required]
        [StringLength(100)]
        [EmailAddress]
        [Display(Name = "Email")]
        public string AuthorEmail { get; set; } = default!;

        [Required]
        [StringLength(100000)]
        public string Content { get; set; } = default!;
    }
}
