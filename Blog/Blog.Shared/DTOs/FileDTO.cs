namespace Blog.Shared.DTOs
{
    public class FileDTO
    {
        public byte[] File { get; set; } = default!;
        public string OriginalFileName { get; set; } = default!;
        public string NewFileName { get; set; } = default!;
        public string ContentType { get; set; } = default!;
        public long ContentLength { get; set; }
        public string WebRootPath { get; set; } = default!;
    }
}
