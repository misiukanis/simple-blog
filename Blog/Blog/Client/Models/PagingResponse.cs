using Blog.Shared.Pagination;

namespace Blog.Client.Models
{
    public class PagingResponse<T> where T : class
    {
        public List<T> Items { get; set; }
        public PaginationMetadata MetaData { get; set; }
    }
}
