using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SampleSmartLockApp.Application.Parameters;
using SampleSmartLockApp.Domain.Entities;

namespace SampleSmartLockApp.Application.Features.AccessPermissions.Queries.GetAllAccessPermissions
{
    public record GetAllAccessPermissionsViewModel(IEnumerable<GetAllAccessPermissionDetailsViewModel> AccessPermissions);
    public record GetAllAccessPermissionDetailsViewModel(Guid Id, Guid UserId, Guid LockId, string LockName, DateTimeOffset? ValidUntil);
}