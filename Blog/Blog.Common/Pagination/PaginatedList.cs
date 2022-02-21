using Microsoft.EntityFrameworkCore;

namespace Blog.Common.Pagination
{
    public class PaginatedList<T> : List<T>
    {
        public int CurrentPage { get; private set; } 
        public int ItemsCountPerPage { get; private set; } 
        public int TotalItemsCount { get; private set; } 
        public int TotalPagesCount { get; private set; } 

        public bool HasPreviousPage
        {
            get
            {
                return (CurrentPage > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (CurrentPage < TotalPagesCount);
            }
        }

        public PaginatedList(IEnumerable<T> source, int currentPage, int itemsCountPerPage, int totalItemsCount, int totalPagesCount)
        {
            CurrentPage = currentPage;
            ItemsCountPerPage = itemsCountPerPage;
            TotalItemsCount = totalItemsCount;
            TotalPagesCount = totalPagesCount;

            AddRange(source);
        }

        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int currentPage, int itemsCountPerPage)
        {
            var totalItemsCount = await source.CountAsync();

            if (totalItemsCount == 0)
            {
                return new PaginatedList<T>(new List<T>(), 0, itemsCountPerPage, 0, 0);
            }

            itemsCountPerPage = Math.Abs(itemsCountPerPage); 
            var totalPagesCount = (int)Math.Ceiling(totalItemsCount / (double)itemsCountPerPage); 
            var pageIndex = Math.Min(Math.Max(1, currentPage), totalPagesCount);
            var items = await source.Skip((pageIndex - 1) * itemsCountPerPage).Take(itemsCountPerPage).ToListAsync();

            return new PaginatedList<T>(items, pageIndex, itemsCountPerPage, totalItemsCount, totalPagesCount);
        }
    }
}
