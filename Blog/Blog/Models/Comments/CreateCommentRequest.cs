using System.ComponentModel.DataAnnotations;

namespace Blog.Models.Comments
{
    public class CreateCommentRequest
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
