using System.ComponentModel.DataAnnotations;

namespace Blog.Models.Pagination
{
    public class PaginationRequest
    {
        [Range(1, int.MaxValue)]
        public int Page { get; set; } = 1;

        [Range(1, int.MaxValue)]
        public int ItemsCountPerPage { get; set; } = 5;
    }
}
