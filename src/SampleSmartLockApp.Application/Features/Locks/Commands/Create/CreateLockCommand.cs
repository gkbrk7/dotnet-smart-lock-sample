using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using SampleSmartLockApp.Application.Wrappers;

namespace SampleSmartLockApp.Application.Features.Locks.Commands.Create
{
    public record CreateLockCommand(string Name, Guid OfficeId) : IRequest<ApiResponse<Guid>>;
}