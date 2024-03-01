using Blog.Application.Services.Interfaces;
using MediatR;

namespace Blog.Application.Commands.UploadImage
{
    public class UploadImageCommandHandler(IFileUploadingService fileUploadingService) : IRequestHandler<UploadImageCommand>
    {
        private readonly IFileUploadingService _fileUploadingService = fileUploadingService;

        public async Task Handle(UploadImageCommand request, CancellationToken cancellationToken)
        {
            await _fileUploadingService.UploadImageAsync(request.FileDTO);
        }
    }
}
