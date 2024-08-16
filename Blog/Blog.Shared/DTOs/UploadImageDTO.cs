using Blog.Shared.Attributes;
using Microsoft.AspNetCore.Http;

namespace Blog.Shared.DTOs
{
    public class UploadImageDTO
    {
        [MaxFileSize(1048576)] // 1 MB
        [AllowedFileExtensions("jpg,png,gif,jpeg")]
        public IFormFile File { get; set; } = default!;
    }
}
