﻿using AutoMapper;
using Blog.Application.DTOs;
using Blog.Models.Pagination;
using X.PagedList;

namespace Blog.Automapper.MappingProfiles
{
    public class PaginationMetadataProfile : Profile
    {
        public PaginationMetadataProfile()
        {
            CreateMap<IPagedList<PostDTO>, PaginationMetadata>()
                .ForMember(d => d.CurrentPage, opt => opt.MapFrom(src => src.PageNumber))
                .ForMember(d => d.ItemsCountPerPage, opt => opt.MapFrom(src => src.PageSize))
                .ForMember(d => d.TotalItemsCount, opt => opt.MapFrom(src => src.TotalItemCount))
                .ForMember(d => d.TotalPagesCount, opt => opt.MapFrom(src => src.PageCount))
                .ForMember(d => d.HasPreviousPage, opt => opt.MapFrom(src => src.HasPreviousPage))
                .ForMember(d => d.HasNextPage, opt => opt.MapFrom(src => src.HasNextPage));
        }
    }
}