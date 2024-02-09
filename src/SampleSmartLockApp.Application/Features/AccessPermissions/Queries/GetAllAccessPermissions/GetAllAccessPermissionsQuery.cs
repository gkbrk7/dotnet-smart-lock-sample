using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using SampleSmartLockApp.Application.Features.AccessPermissionHistories.Queries;
using SampleSmartLockApp.Application.Parameters;
using SampleSmartLockApp.Application.Wrappers;

namespace SampleSmartLockApp.Application.Features.AccessPermissions.Queries.GetAllAccessPermissions
{
    public record GetAllAccessPermissionsQuery(int PageNumber, int PageSize) : IRequest<ApiPagedResponse<GetAllAccessPermissionsViewModel>>;
}