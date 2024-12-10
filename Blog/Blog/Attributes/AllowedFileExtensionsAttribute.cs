using Blog.Resources;
using System.ComponentModel.DataAnnotations;

namespace Blog.Attributes
{
    public class AllowedFileExtensionsAttribute(string fileExtensions) : ValidationAttribute
    {
        private readonly IEnumerable<string> _allowedExtensions = fileExtensions.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;

            if (file == null)
            {
                return new ValidationResult(ValidationErrors.EmptyFile);
            }

            var fileName = file.FileName;
            if (!_allowedExtensions.Any(fileName.EndsWith))
            {
                return new ValidationResult(string.Format(ValidationErrors.NotAllowedFileExtension, string.Join(", ", _allowedExtensions)));
            }

            return ValidationResult.Success;
        }
    }
}
