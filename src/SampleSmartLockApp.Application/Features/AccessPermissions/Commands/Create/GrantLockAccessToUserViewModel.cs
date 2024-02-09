using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SampleSmartLockApp.Application.Parameters;
using SampleSmartLockApp.Domain.Entities;

namespace SampleSmartLockApp.Application.Features.AccessPermissions.Commands.Create
{
    public record GrantLockAccessToUserViewModel(Guid Id, Guid UserId, Guid LockId, DateTimeOffset? ValidUntil = null);
}