using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SampleSmartLockApp.Application.Parameters;

namespace SampleSmartLockApp.Application.Features.AccessPermissions.Queries.GetAllAccessPermissionsByUserId
{
    public record GetAllAccessPermissionsByUserIdRequestParameter(Guid UserId) : RequestParameter
    {

    }
}