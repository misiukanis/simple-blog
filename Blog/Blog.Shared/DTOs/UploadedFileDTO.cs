namespace Blog.Shared.DTOs
{
    public record UploadedFileDTO
    {
        public string FileName { get; set; } = default!;
        public string FilePath { get; set; } = default!;
    }
}
