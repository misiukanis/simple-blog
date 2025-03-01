using Blog.Application.Commands.UploadImage;
using Blog.Application.Interfaces.Services;
using Blog.Shared.Settings;
using ImageMagick;
using Microsoft.Extensions.Options;

namespace Blog.Infrastructure.Services
{
    public class FileUploadingService(IOptionsSnapshot<ApplicationSettings> appSettings) : IFileUploadingService
    {
        private const int MaxImageWidthAndHeight = 500;
        private const int ImageQuality = 75;

        private readonly ApplicationSettings _appSettings = appSettings.Value;

        public async Task UploadImageAsync(FileDTO fileDTO)
        {
            var fullPathToSaveImage = Path.Combine(fileDTO.WebRootPath, _appSettings.UploadedImagesDirectory);

            using (var image = new MagickImage(fileDTO.File))
            {
                (uint scaledImageWidth, uint scaledImageHeight) = GetScaledImageDimensions(image.Width, image.Height);

                image.Resize(scaledImageWidth, scaledImageHeight);
                image.Quality = ImageQuality;
                await image.WriteAsync(Path.Combine(fullPathToSaveImage, fileDTO.NewFileName));
            }
        }

        private (uint scaledImageWidth, uint scaledImageHeight) GetScaledImageDimensions(uint imageWidth, uint imageHeight)
        {
            if (imageWidth > MaxImageWidthAndHeight || imageHeight > MaxImageWidthAndHeight)
            {
                double factor;

                if (imageWidth > imageHeight)
                {
                    factor = MaxImageWidthAndHeight / (double)imageWidth;
                }
                else
                {
                    factor = MaxImageWidthAndHeight / (double)imageHeight;
                }

                var newImageWidth = (uint)Math.Round(imageWidth * factor, MidpointRounding.AwayFromZero); 
                var newImageHeight = (uint)Math.Round(imageHeight * factor, MidpointRounding.AwayFromZero);   

                return (newImageWidth, newImageHeight);
            }

            return (imageWidth, imageHeight);
        }
    }
}
