using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SampleSmartLockApp.Application.Enums;
using SampleSmartLockApp.Application.Wrappers;

namespace SampleSmartLockApp.Application.Features.Users.GetAllUserRoles.Queries
{
    public record GetAllUserRolesQuery : IRequest<ApiResponse<IEnumerable<GetAllUserRolesViewModel>>>
    {

    }
}