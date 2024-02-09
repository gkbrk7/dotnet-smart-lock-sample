using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SampleSmartLockApp.Application.Enums;
using SampleSmartLockApp.Application.Wrappers;

namespace SampleSmartLockApp.Application.Features.Users.GetAllUserRoles.Queries
{
    public class GetAllUserRolesQueryHandler(RoleManager<IdentityRole> roleManager) : IRequestHandler<GetAllUserRolesQuery, ApiResponse<IEnumerable<GetAllUserRolesViewModel>>>
    {
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;

        public async Task<ApiResponse<IEnumerable<GetAllUserRolesViewModel>>> Handle(GetAllUserRolesQuery request, CancellationToken cancellationToken)
        {
            var roles = await _roleManager.Roles
                .Where(r => r.Name == UserRoles.Administrator.ToString())
                .Select(r => new GetAllUserRolesViewModel(r.Id, r.Name!))
                .ToListAsync(cancellationToken: cancellationToken);

            return ApiResponse<IEnumerable<GetAllUserRolesViewModel>>.Success(roles);
        }
    }
}