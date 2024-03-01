using Blog.Shared.Metadata;

namespace Blog.Client.Models
{
    public class PagingResponse<T> where T : class
    {
        public List<T> Items { get; set; } = default!;
        public PaginationMetadata MetaData { get; set; } = default!;
    }
}
