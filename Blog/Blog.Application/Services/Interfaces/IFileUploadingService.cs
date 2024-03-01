using Blog.Shared.DTOs;

namespace Blog.Application.Services.Interfaces
{
    public interface IFileUploadingService
    {
        Task UploadImageAsync(FileDTO fileDTO);
    }
}
