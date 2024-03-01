using Blog.Shared.DTOs;
using MediatR;

namespace Blog.Application.Commands.UploadImage
{
    public class UploadImageCommand(FileDTO fileDTO) : IRequest
    {
        public FileDTO FileDTO { get; } = fileDTO;
    }
}
