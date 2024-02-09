using System.Security.Principal;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SampleSmartLockApp.Application.Enums;
using SampleSmartLockApp.Application.Interfaces;
using SampleSmartLockApp.Application.Interfaces.Repositories;
using SampleSmartLockApp.Application.Wrappers;
using SampleSmartLockApp.Domain.Entities;
using SampleSmartLockApp.Infrastructure.Contexts;

namespace SampleSmartLockApp.Application.Features.AccessPermissions.Commands.Create
{
    public class GrantLockAccessToUserCommandHandler(IAccessPermissionRepositoryAsync accessPermissionRepository, ILockRepositoryAsync lockRepository, UserManager<ApplicationUser> userManager) : IRequestHandler<GrantLockAccessToUserCommand, ApiResponse<GrantLockAccessToUserViewModel>>
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly ILockRepositoryAsync _lockRepository = lockRepository;
        private readonly IAccessPermissionRepositoryAsync _accessPermissionRepository = accessPermissionRepository;

        public async Task<ApiResponse<GrantLockAccessToUserViewModel>> Handle(GrantLockAccessToUserCommand command, CancellationToken cancellationToken)
        {
            var userExists = await CheckUserExists(command.UserId);
            if (!userExists)
                return ApiResponse<GrantLockAccessToUserViewModel>.Fail("User with provided UserId not found.");

            var lockExists = await CheckLockExists(command.LockId);
            if (!lockExists)
                return ApiResponse<GrantLockAccessToUserViewModel>.Fail("Lock not found.");

            var (userPermissionExists, validUntil) = await CheckUserHasPermission(command.UserId, command.LockId);
            if (userPermissionExists)
                return ApiResponse<GrantLockAccessToUserViewModel>.Fail($"User has already permission for access and it is still valid by {validUntil}.");

            var result = await _accessPermissionRepository.AddAsync(new AccessPermission { UserId = command.UserId, LockId = command.LockId, ValidUntil = command.ValidUntil });

            return ApiResponse<GrantLockAccessToUserViewModel>.Success(new GrantLockAccessToUserViewModel(result.Id, result.UserId, result.LockId, result.ValidUntil));
        }

        private async Task<(bool, DateTimeOffset?)> CheckUserHasPermission(Guid userId, Guid lockId)
        {
            var permission = await _accessPermissionRepository.GetLockAccessOfUser(userId, lockId);
            if (permission is null) return (false, permission?.ValidUntil);
            return (true, permission.ValidUntil);
        }

        private async Task<bool> CheckLockExists(Guid lockId)
        {
            var _lock = await _lockRepository.GetByIdAsync(lockId);
            if (_lock is null) return false;
            return true;
        }

        private async Task<bool> CheckUserExists(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null) return false;
            return true;
        }
    }
}