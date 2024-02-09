using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using SampleSmartLockApp.Application.Parameters;
using SampleSmartLockApp.Application.Wrappers;

namespace SampleSmartLockApp.Application.Features.Offices.Queries.GetAll
{
    public record GetAllOfficesQuery(int PageNumber, int PageSize) : IRequest<ApiPagedResponse<IEnumerable<GetAllOfficesViewModel>>>;
}