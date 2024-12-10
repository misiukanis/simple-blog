namespace Blog.Models.Pagination
{
    public class PaginationResponse<T> where T : class
    {
        public List<T> Items { get; set; } = default!;
        public PaginationMetadata Metadata { get; set; } = default!;
    }
}
