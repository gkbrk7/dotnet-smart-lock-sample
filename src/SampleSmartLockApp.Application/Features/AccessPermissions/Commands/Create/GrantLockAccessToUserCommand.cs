using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using SampleSmartLockApp.Application.Parameters;
using SampleSmartLockApp.Application.Wrappers;

namespace SampleSmartLockApp.Application.Features.AccessPermissions.Commands.Create
{
    public record GrantLockAccessToUserCommand(Guid UserId, Guid LockId, DateTimeOffset? ValidUntil) : IRequest<ApiResponse<GrantLockAccessToUserViewModel>>;
}