using AutoMapper;
using Blog.Domain.Entities.PostAggregate;
using Blog.Shared.DTOs;

namespace Blog.Application.Automapper.MappingProfiles
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<Comment, PostWithCommentsDTO.CommentDTO>();
            CreateMap<Comment, CommentDTO>();

        }
    }
}
