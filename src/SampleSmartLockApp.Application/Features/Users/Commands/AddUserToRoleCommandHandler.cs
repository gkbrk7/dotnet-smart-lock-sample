using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SampleSmartLockApp.Application.Enums;
using SampleSmartLockApp.Application.Wrappers;
using SampleSmartLockApp.Infrastructure.Contexts;

namespace SampleSmartLockApp.Application.Features.Users.Commands
{
    public class AddUserToRoleCommandHandler(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager) : IRequestHandler<AddUserToRoleCommand, ApiResponse<Guid>>
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;

        public async Task<ApiResponse<Guid>> Handle(AddUserToRoleCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user is null)
                return ApiResponse<Guid>.Fail("User not found.");

            var role = await _roleManager.FindByIdAsync(request.RoleId.ToString());
            if (role is null)
                return ApiResponse<Guid>.Fail("Role not found.");

            var result = await _userManager.AddToRoleAsync(user, role.Name!);
            if (!result.Succeeded)
                return ApiResponse<Guid>.Fail(result.Errors.First().Description);

            return ApiResponse<Guid>.Success(new Guid(request.UserId), "User added to role successfully.");
        }
    }
}