using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace SampleSmartLockApp.Application.Features.Users.Commands
{
    public class AddUserToRoleCommandValidator : AbstractValidator<AddUserToRoleCommand>
    {
        public AddUserToRoleCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(x => x.RoleId)
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");
        }
    }
}