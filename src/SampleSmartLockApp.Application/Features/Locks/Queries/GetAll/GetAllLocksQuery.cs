using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using SampleSmartLockApp.Application.Wrappers;

namespace SampleSmartLockApp.Application.Features.Locks.Queries.GetAll
{
    public record GetAllLocksQuery(int PageNumber, int PageSize) : IRequest<ApiPagedResponse<IEnumerable<GetAllLocksViewModel>>>;
}