using Blog.Resources;
using System.ComponentModel.DataAnnotations;

namespace Blog.Attributes
{
    public class MaxFileSizeAttribute(int maxBytes) : ValidationAttribute
    {
        private readonly int _maxBytes = maxBytes;

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;

            if (file == null)
            {
                return new ValidationResult(ValidationErrors.EmptyFile);
            }

            if (file.Length > _maxBytes)
            {
                return new ValidationResult(string.Format(ValidationErrors.TooBigFile, _maxBytes + " B"));
            }

            return ValidationResult.Success;
        }
    }
}
