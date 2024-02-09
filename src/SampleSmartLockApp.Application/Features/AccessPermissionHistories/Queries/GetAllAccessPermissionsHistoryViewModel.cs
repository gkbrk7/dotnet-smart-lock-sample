using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SampleSmartLockApp.Application.Parameters;

namespace SampleSmartLockApp.Application.Features.AccessPermissionHistories.Queries
{
    public record GetAllAccessPermissionsHistoryViewModel(Guid UserId, Guid LockId, DateTimeOffset Timestamp, bool IsConfirmed, string? Message)
    {

    }
}