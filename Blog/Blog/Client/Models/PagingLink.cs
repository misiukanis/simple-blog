namespace Blog.Client.Models
{
    public class PagingLink
    {
        public int Page { get; set; }
        public bool Enabled { get; set; }
        public bool Active { get; set; }
        public string Text { get; set; }
        

        public PagingLink(int page, bool enabled, bool active, string text)
        {
            Page = page;
            Enabled = enabled;
            Active = active;
            Text = text;
        }
    }
}
