using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace SampleSmartLockApp.Application.Features.AccessPermissions.Commands.Update
{
    public class UpdateLockAccessForUserCommandValidator : AbstractValidator<UpdateLockAccessForUserCommand>
    {
        public UpdateLockAccessForUserCommandValidator()
        {
            RuleFor(r => r.LockId)
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(r => r.UserId)
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(r => r.ValidUntil)
                .GreaterThan(DateTimeOffset.UtcNow)
                .When(r => r.ValidUntil.HasValue);
        }
    }
}