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

        }
    }
}