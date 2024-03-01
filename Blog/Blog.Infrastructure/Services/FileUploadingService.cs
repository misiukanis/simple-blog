using Blog.Application.Services.Interfaces;
using Blog.Shared.DTOs;
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
            string fullPathToSaveImage = Path.Combine(fileDTO.WebRootPath, _appSettings.UploadedImagesDirectory);

            using (MagickImage image = new MagickImage(fileDTO.File))
            {
                (int scaledImageWidth, int scaledImageHeight) scaledImageDimensions = GetScaledImageDimensions(image.Width, image.Height);

                image.Resize(scaledImageDimensions.scaledImageWidth, scaledImageDimensions.scaledImageHeight);
                image.Quality = ImageQuality;
                await image.WriteAsync(Path.Combine(fullPathToSaveImage, fileDTO.NewFileName));
            }
        }

        private (int scaledImageWidth, int scaledImageHeight) GetScaledImageDimensions(int imageWidth, int imageHeight)
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

                int newImageWidth = (int)Math.Round(imageWidth * factor, MidpointRounding.AwayFromZero); 
                int newImageHeight = (int)Math.Round(imageHeight * factor, MidpointRounding.AwayFromZero);  

                return (newImageWidth, newImageHeight);
            }

            return (imageWidth, imageHeight);
        }
    }
}
