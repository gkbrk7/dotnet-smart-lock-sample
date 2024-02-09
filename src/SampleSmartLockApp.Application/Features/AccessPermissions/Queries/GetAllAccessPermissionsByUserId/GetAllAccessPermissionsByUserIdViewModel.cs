using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SampleSmartLockApp.Application.Parameters;
using SampleSmartLockApp.Domain.Entities;

namespace SampleSmartLockApp.Application.Features.AccessPermissions.Queries.GetAllAccessPermissionsByUserId
{
    public record GetAllAccessPermissionsByUserIdViewModel(Guid UserId, IEnumerable<GetAllAccessPermissionByUserIdDetailsViewModel> AccessPermissions);
    public record GetAllAccessPermissionByUserIdDetailsViewModel(Guid Id, Guid LockId, string LockName, DateTimeOffset? ValidUntil);
}