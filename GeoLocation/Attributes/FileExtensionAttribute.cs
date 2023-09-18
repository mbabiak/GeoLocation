using System.ComponentModel.DataAnnotations;

namespace GeoLocation.Attributes
{
    public class FileExtensionAttribute : ValidationAttribute
    {
        private readonly string _allowedExtension;

        public FileExtensionAttribute(string allowedExtension)
        {
            _allowedExtension = allowedExtension;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;

            if (file != null)
            {
                var extension = Path.GetExtension(file.FileName).ToLower();

                if (extension != _allowedExtension.ToLower())
                {
                    return new ValidationResult($"Only files with the {_allowedExtension} extension are allowed.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
