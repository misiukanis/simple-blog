using AutoMapper;
using Blog.Application.Automapper.Converters;
using Blog.Common.Pagination;
using Blog.Domain.Entities.PostAggregate;
using Blog.Shared.DTOs;

namespace Blog.Application.Automapper.MappingProfiles
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<Post, PostDTO>();
            CreateMap<Post, PostWithCommentsDTO>();

            CreateMap<PaginatedList<Post>, PaginatedList<PostDTO>>()
                .ConvertUsing<PagedListConverter<Post, PostDTO>>();
        }
    }
}
