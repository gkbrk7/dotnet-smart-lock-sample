using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleSmartLockApp.Application.Features.AccessPermissionHistories.Commands
{
    public record CreateAccessPermissionsHistoryViewModel(Guid Id, Guid UserId, Guid LockId, DateTimeOffset Timestamp, bool IsConfirmed, string? Message);
}