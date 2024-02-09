using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using SampleSmartLockApp.Application.Wrappers;

namespace SampleSmartLockApp.Application.Features.Locks.Commands.Update
{
    public record UpdateLockCommand(Guid Id, string Name) : IRequest<ApiResponse<Guid>>
    {

    }
}