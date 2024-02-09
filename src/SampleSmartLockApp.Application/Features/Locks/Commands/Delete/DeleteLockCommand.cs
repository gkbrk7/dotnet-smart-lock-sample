using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using SampleSmartLockApp.Application.Wrappers;

namespace SampleSmartLockApp.Application.Features.Locks.Commands.Delete
{
    public record DeleteLockCommand(Guid Id) : IRequest<ApiResponse<Guid>>
    {

    }
}