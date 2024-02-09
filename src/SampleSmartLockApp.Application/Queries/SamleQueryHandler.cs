using MediatR;
using Microsoft.AspNetCore.Identity;
using SampleSmartLockApp.Application.Interfaces;
using SampleSmartLockApp.Application.Wrappers;
using SampleSmartLockApp.Domain.Entities;
using SampleSmartLockApp.Infrastructure.Contexts;

namespace SampleSmartLockApp.Application.Queries
{
    public class SamleQueryHandler : IRequestHandler<SampleQuery, ApiResponse<ApplicationUser>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuthenticatedUserService _authenticatedUserService;

        public SamleQueryHandler(UserManager<ApplicationUser> userManager, IAuthenticatedUserService authenticatedUserService)
        {
            _authenticatedUserService = authenticatedUserService;
            _userManager = userManager;
        }
        public async Task<ApiResponse<ApplicationUser>> Handle(SampleQuery request, CancellationToken cancellationToken)
        {
            var userId = _authenticatedUserService.UserId;

            if (userId is null)
                return ApiResponse<ApplicationUser>.Fail("User Id Not Found.");

            var user = await _userManager.FindByIdAsync(userId);

            if (user is null)
                return ApiResponse<ApplicationUser>.Fail("User Not Found.");

            return ApiResponse<ApplicationUser>.Success(user);
        }
    }
}