using Blog.Application.Commands.UploadImage;
using Blog.Shared.DTOs;
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
        public async Task<ActionResult<UploadedFileDTO>> UploadImage([FromForm] UploadImageDTO imageDTO)
        {
            var fileDTO = new FileDTO
            {
                OriginalFileName = imageDTO.File.FileName,
                NewFileName = imageDTO.File.FileName.TrustedFileName(),
                ContentType = imageDTO.File.ContentType,
                ContentLength = imageDTO.File.Length,
                WebRootPath = _environment.WebRootPath
            };

            using (var memoryStream = new MemoryStream())
            {
                imageDTO.File.CopyTo(memoryStream);
                fileDTO.File = memoryStream.ToArray();
            }

            var imageCommand = new UploadImageCommand(fileDTO);
            await _mediator.Send(imageCommand);

            var resourcePath = new Uri($"{Request.Scheme}://{Request.Host}/{_appSettings.UploadedImagesDirectory}/{fileDTO.NewFileName}");

            var uploadedFileDTO = new UploadedFileDTO
            {
                FileName = fileDTO.NewFileName,
                FilePath = $"{_appSettings.UploadedImagesDirectory}/{fileDTO.NewFileName}"
            };

            return new CreatedResult(resourcePath, uploadedFileDTO);
        }

    }
}
