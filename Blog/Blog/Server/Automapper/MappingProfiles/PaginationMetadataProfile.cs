using AutoMapper;
using Blog.Common.Pagination;
using Blog.Shared.DTOs;
using Blog.Shared.Pagination;

namespace Blog.Application.Automapper.MappingProfiles
{
    public class PaginationMetadataProfile : Profile
    {
        public PaginationMetadataProfile()
        {
            CreateMap<PaginatedList<PostDTO>, PaginationMetadata>()
                .ForMember(d => d.CurrentPage, opt => opt.MapFrom(src => src.CurrentPage))
                .ForMember(d => d.ItemsCountPerPage, opt => opt.MapFrom(src => src.ItemsCountPerPage))
                .ForMember(d => d.TotalItemsCount, opt => opt.MapFrom(src => src.TotalItemsCount))
                .ForMember(d => d.TotalPagesCount, opt => opt.MapFrom(src => src.TotalPagesCount))
                .ForMember(d => d.HasPreviousPage, opt => opt.MapFrom(src => src.HasPreviousPage))
                .ForMember(d => d.HasNextPage, opt => opt.MapFrom(src => src.HasNextPage));

        }
    }
}
