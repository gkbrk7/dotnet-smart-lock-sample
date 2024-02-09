using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SampleSmartLockApp.Application.Enums;

namespace SampleSmartLockApp.Domain.Settings
{
    public class LockAccessOptions
    {
        public IEnumerable<UserRoles> PrivilegedRoles { get; set; } = [];
    }
}