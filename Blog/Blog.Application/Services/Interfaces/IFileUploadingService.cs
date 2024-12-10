using Blog.Application.Commands.UploadImage;

namespace Blog.Application.Services.Interfaces
{
    public interface IFileUploadingService
    {
        Task UploadImageAsync(FileDTO fileDTO);
    }
}
