using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoAlbumWS.Core.CrossCuttingConcerns.FluentValidation
{
    public static class ValidatorTool
    {
        public static void FluentValidate(IValidator validator, object entity)
        {
            var context = new ValidationContext<object>(entity);
            var result = validator.Validate(context);

            if (result.Errors.Count > 0)
            {
                throw new ValidationException(result.Errors);
            }
        }
    }
}
