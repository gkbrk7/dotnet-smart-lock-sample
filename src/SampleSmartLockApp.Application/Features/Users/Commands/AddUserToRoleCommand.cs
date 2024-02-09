using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleSmartLockApp.Application.Features.Users.Commands
{
    public record AddUserToRoleCommand(string UserId, string RoleId);
}