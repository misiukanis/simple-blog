namespace Blog.Shared.Settings
{
    public record ApplicationSettings
    {
        public required string UploadedImagesDirectory { get; init; }
    }
}
