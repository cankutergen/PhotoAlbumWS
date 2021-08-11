using FluentValidation;
using Microservices.Albums.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservices.Albums.Business.ValidationRules.FluentValidaton
{
    public class AlbumValidator : AbstractValidator<Album>
    {
        public AlbumValidator()
        {
            RuleFor(x => x.Title)
                .NotNull()
                .NotEmpty();

            // Id must be exist and have a valid Id
            RuleFor(x => x.UserId)
                .NotNull()
                .GreaterThan(0);
        }
    }
}
