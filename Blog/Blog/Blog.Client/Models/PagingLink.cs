namespace Blog.Client.Models
{
    public class PagingLink(int page, bool enabled, bool active, string text)
    {
        public int Page { get; set; } = page;
        public bool Enabled { get; set; } = enabled;
        public bool Active { get; set; } = active;
        public string Text { get; set; } = text;
    }
}
