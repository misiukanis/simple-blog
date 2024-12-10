using Blog.Attributes;

namespace Blog.Models.Files
{
    public class UploadImageRequest
    {
        [MaxFileSize(1048576)] // 1 MB
        [AllowedFileExtensions("jpg,png,gif,jpeg")]
        public IFormFile File { get; set; } = default!;
    }
}
