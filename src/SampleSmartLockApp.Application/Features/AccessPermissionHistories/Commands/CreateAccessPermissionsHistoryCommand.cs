using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using SampleSmartLockApp.Application.Wrappers;

namespace SampleSmartLockApp.Application.Features.AccessPermissionHistories.Commands
{
    public record CreateAccessPermissionsHistoryCommand(Guid UserId, Guid LockId, DateTimeOffset Timestamp, bool IsConfirmed, string? Message) : IRequest<ApiResponse<CreateAccessPermissionsHistoryViewModel>>;
}