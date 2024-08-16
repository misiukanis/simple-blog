namespace Blog.Shared.Metadata
{
    public class PaginationMetadata
    {
        public int CurrentPage { get; set; }
        public int ItemsCountPerPage { get; set; }
        public int TotalItemsCount { get; set; }
        public int TotalPagesCount { get; set; }
        public bool HasPreviousPage { get; set; }
        public bool HasNextPage { get; set; }
    }
}
