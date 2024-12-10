namespace Blog.Shared.Settings
{
    public class EmailSettings
    {
        public required string EmailAddress { get; init; }
        public required string EmailPassword { get; init; }
        public required string EmailHost { get; init; }
        public required int EmailPort { get; init; }
    }
}
