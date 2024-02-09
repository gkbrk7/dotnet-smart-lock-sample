using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using SampleSmartLockApp.Application.Wrappers;

namespace SampleSmartLockApp.Application.Features.Locks.Queries.OpenLock
{
    public record OpenLockByIdQuery(Guid LockId) : IRequest<ApiResponse<string>>;
}