using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SampleSmartLockApp.Application.Enums;

namespace SampleSmartLockApp.Application.Interfaces
{
    public interface IAuthenticatedUserService
    {
        public string UserId { get; }
        public string? Name { get; }
        public IEnumerable<UserRoles> Roles { get; }

    }
}