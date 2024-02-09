using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using SampleSmartLockApp.Application.Parameters;
using SampleSmartLockApp.Application.Wrappers;

namespace SampleSmartLockApp.Application.Features.AccessPermissions.Queries.GetAllAccessPermissionsByUserId
{
    public record GetAllAccessPermissionsByUserIdQuery(Guid UserId, int PageNumber, int PageSize) : IRequest<ApiPagedResponse<GetAllAccessPermissionsByUserIdViewModel>>;
}