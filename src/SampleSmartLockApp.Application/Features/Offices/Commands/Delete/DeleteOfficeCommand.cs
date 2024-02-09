using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using SampleSmartLockApp.Application.Wrappers;

namespace SampleSmartLockApp.Application.Features.Offices.Commands.Delete
{
    public record DeleteOfficeCommand(Guid Id) : IRequest<ApiResponse<Guid>>
    {

    }
}