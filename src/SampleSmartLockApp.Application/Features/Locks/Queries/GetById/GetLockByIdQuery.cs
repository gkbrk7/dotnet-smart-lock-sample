using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using SampleSmartLockApp.Application.Wrappers;

namespace SampleSmartLockApp.Application.Features.Locks.Queries.GetAll
{
    public record GetLockByIdQuery(Guid Id) : IRequest<ApiResponse<GetLockByIdViewModel>>;
}