using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Validations
{
    public class ContentTypeValidator : ValidationAttribute
    {
        private readonly string[] _validContentType;
        private readonly string[] imageContentType = new string[] { "image/jpeg", "image/png", "image/gif" };

        public ContentTypeValidator(string[] validContentType)
        {
            _validContentType = validContentType;
        }
        public ContentTypeValidator(ContentTypeGroup contentTypeGroup)
        {
            switch (contentTypeGroup)
            {
                case ContentTypeGroup.Image:
                    _validContentType = imageContentType;
                    break;
            }
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }
            IFormFile formFile = value as IFormFile;
            if (formFile == null)
            {
                return ValidationResult.Success;
            }
            if (!_validContentType.Contains(formFile.ContentType))
            {
                return new ValidationResult($"Content-Type should be one of the following {string.Join(" ",_validContentType)}");
            }
            return ValidationResult.Success;
        }

    }
    public enum ContentTypeGroup
    {
        Image 
    }
}
