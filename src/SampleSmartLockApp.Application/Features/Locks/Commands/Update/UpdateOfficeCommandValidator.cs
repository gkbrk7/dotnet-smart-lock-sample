using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace SampleSmartLockApp.Application.Features.Locks.Commands.Update
{
    public class UpdateLockCommandValidator : AbstractValidator<UpdateLockCommand>
    {
        public UpdateLockCommandValidator()
        {
            RuleFor(r => r.Name)
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");
        }
    }
}