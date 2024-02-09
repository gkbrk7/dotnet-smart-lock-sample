using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace SampleSmartLockApp.Application.Features.Locks.Commands.Create
{
    public class CreateLockCommandValidator : AbstractValidator<CreateLockCommand>
    {
        public CreateLockCommandValidator()
        {
            RuleFor(r => r.Name)
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(r => r.OfficeId)
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");
        }
    }
}