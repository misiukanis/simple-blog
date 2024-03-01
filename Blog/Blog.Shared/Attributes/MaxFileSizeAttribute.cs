using Blog.Resources;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Blog.Shared.Attributes
{
    public class MaxFileSizeAttribute(int maxBytes) : ValidationAttribute
    {
        private readonly int _maxBytes = maxBytes;

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            IFormFile? file = value as IFormFile;

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
