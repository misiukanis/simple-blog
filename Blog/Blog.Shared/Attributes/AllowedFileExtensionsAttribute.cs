using Blog.Resources;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Blog.Shared.Attributes
{
    public class AllowedFileExtensionsAttribute(string fileExtensions) : ValidationAttribute
    {
        private readonly IEnumerable<string> _allowedExtensions = fileExtensions.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            IFormFile? file = value as IFormFile;

            if (file == null)
            {
                return new ValidationResult(ValidationErrors.EmptyFile);
            }

            string fileName = file.FileName;
            if (!_allowedExtensions.Any(fileName.EndsWith))
            {
                return new ValidationResult(string.Format(ValidationErrors.NotAllowedFileExtension, string.Join(", ", _allowedExtensions)));
            }

            return ValidationResult.Success;
        }
    }
}
