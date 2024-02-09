using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace SampleSmartLockApp.Application.Features.Offices.Commands.Create
{
    public class CreateOfficeCommandValidator : AbstractValidator<CreateOfficeCommand>
    {
        public CreateOfficeCommandValidator()
        {
            RuleFor(r => r.Name)
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");
        }
    }
}