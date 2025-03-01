using Blog.Application.Commands.UploadImage;

namespace Blog.Application.Interfaces.Services
{
    public interface IFileUploadingService
    {
        Task UploadImageAsync(FileDTO fileDTO);
    }
}
