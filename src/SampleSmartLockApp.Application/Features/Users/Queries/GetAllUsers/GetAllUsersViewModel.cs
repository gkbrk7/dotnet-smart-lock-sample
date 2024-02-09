using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleSmartLockApp.Application.Features.Users.Queries.GetAllUsers
{
    public record GetAllUsersViewModel(string UserId, string? UserName, string? Email);
}