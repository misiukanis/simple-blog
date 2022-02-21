using AutoMapper;
using Blog.Common.Pagination;

namespace Blog.Application.Automapper.Converters
{
    public class PagedListConverter<TSource, TDestination> : ITypeConverter<PaginatedList<TSource>, PaginatedList<TDestination>>
    {
        public PaginatedList<TDestination> Convert(PaginatedList<TSource> source, PaginatedList<TDestination> destination, ResolutionContext context)
        {
            IEnumerable<TDestination> mappedModels = source.Select(x => context.Mapper.Map<TSource, TDestination>(x));

            return new PaginatedList<TDestination>(mappedModels, source.CurrentPage, source.ItemsCountPerPage, source.TotalItemsCount, source.TotalPagesCount);
        }
    }
}
