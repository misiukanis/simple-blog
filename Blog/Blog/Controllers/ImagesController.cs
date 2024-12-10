using Blog.Application.Commands.UploadImage;
using Blog.Models.Files;
using Blog.Shared.Extensions;
using Blog.Shared.Settings;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Blog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ImagesController(
        ISender mediator, 
        IWebHostEnvironment environment, 
        IOptionsSnapshot<ApplicationSettings> appSettings) : ControllerBase
    {
        private readonly ISender _mediator = mediator;
        private readonly IWebHostEnvironment _environment = environment;
        private readonly ApplicationSettings _appSettings = appSettings.Value;        


        #if !DEBUG
        [Authorize] 
        #endif
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<FileResponse>> UploadImage([FromForm] UploadImageRequest imageRequest)
        {
            var fileDTO = MapToFileDTO(imageRequest);

            var imageCommand = new UploadImageCommand(fileDTO);
            await _mediator.Send(imageCommand);

            var filePath = GetFilePath(fileDTO);
            var fileResponse = GetFileResponse(fileDTO);

            return new CreatedResult(filePath, fileResponse);
        }


        private FileDTO MapToFileDTO(UploadImageRequest imageRequest)
        {
            var fileDTO = new FileDTO
            {
                OriginalFileName = imageRequest.File.FileName,
                NewFileName = imageRequest.File.FileName.TrustedFileName(),
                ContentType = imageRequest.File.ContentType,
                ContentLength = imageRequest.File.Length,
                WebRootPath = _environment.WebRootPath
            };

            using (var memoryStream = new MemoryStream())
            {
                imageRequest.File.CopyTo(memoryStream);
                fileDTO.File = memoryStream.ToArray();
            }

            return fileDTO;
        }

        private Uri GetFilePath(FileDTO fileDTO)
        {
            return new Uri($"{Request.Scheme}://{Request.Host}/{_appSettings.UploadedImagesDirectory}/{fileDTO.NewFileName}");
        }

        private FileResponse GetFileResponse(FileDTO fileDTO)
        {
            return new FileResponse
            {
                FileName = fileDTO.NewFileName,
                FilePath = $"/{_appSettings.UploadedImagesDirectory}/{fileDTO.NewFileName}"
            };
        }
    }
}
