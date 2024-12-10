using System.ComponentModel.DataAnnotations;

namespace Blog.Models.Emails
{
    public class SendEmailRequest
    {
        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; } = default!;

        [Required]
        [StringLength(100)]
        public string Subject { get; set; } = default!;

        [Required]
        [StringLength(10000)]
        public string Message { get; set; } = default!;
    }
}
