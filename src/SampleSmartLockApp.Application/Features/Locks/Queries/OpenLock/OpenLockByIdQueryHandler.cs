using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Options;
using SampleSmartLockApp.Application.Enums;
using SampleSmartLockApp.Application.Features.AccessPermissionHistories.Commands;
using SampleSmartLockApp.Application.Features.AccessPermissions.Commands.Create;
using SampleSmartLockApp.Application.Interfaces;
using SampleSmartLockApp.Application.Interfaces.Repositories;
using SampleSmartLockApp.Application.Wrappers;
using SampleSmartLockApp.Domain.Entities;
using SampleSmartLockApp.Domain.Settings;

namespace SampleSmartLockApp.Application.Features.Locks.Queries.OpenLock
{
    public class OpenLockByIdQueryHandler(IAuthenticatedUserService authenticatedUserService, IAccessPermissionRepositoryAsync accessPermissionRepository, IMediator mediator, ILockRepositoryAsync lockRepository, IOptions<LockAccessOptions> lockAccessOptions) : IRequestHandler<OpenLockByIdQuery, ApiResponse<string>>
    {
        private readonly IAuthenticatedUserService _authenticatedUserService = authenticatedUserService;
        private readonly IAccessPermissionRepositoryAsync _accessPermissionRepository = accessPermissionRepository;
        private readonly IMediator _mediator = mediator;
        private readonly ILockRepositoryAsync _lockRepository = lockRepository;
        private readonly IEnumerable<UserRoles> privilegedRoles = lockAccessOptions.Value.PrivilegedRoles;

        public async Task<ApiResponse<string>> Handle(OpenLockByIdQuery request, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(_authenticatedUserService.UserId);
            var userRoles = _authenticatedUserService.Roles;

            var lockExists = await CheckLockExists(request);
            if (!lockExists)
                return ApiResponse<string>.Fail("Lock not found.");

            var (userHasPermission, privileged, permission) = await CheckUserHasPermission(request, userId, userRoles);
            if (privileged)
            {
                var message = userHasPermission ? "The lock has been opened successfully." : "The lock has been opened without permission due to privileged accesss.";
                var command = new CreateAccessPermissionsHistoryCommand(userId, request.LockId, DateTimeOffset.UtcNow, true, message);

                await _mediator.Send(command, cancellationToken);
                return ApiResponse<string>.Success(command.Message!);
            }

            if (!userHasPermission || permission is null)
            {
                var command = new CreateAccessPermissionsHistoryCommand(userId, request.LockId, DateTimeOffset.UtcNow, false, $"User has no permission to the lock to perform this action.");

                await _mediator.Send(command, cancellationToken);
                return ApiResponse<string>.Fail(command.Message!);
            }

            if (permission.ValidUntil is null)
            {
                var command = new CreateAccessPermissionsHistoryCommand(userId, request.LockId, DateTimeOffset.UtcNow, true, $"The lock has been opened successfully.");

                await _mediator.Send(command, cancellationToken);
                return ApiResponse<string>.Success(command.Message!);
            }

            if (DateTimeOffset.UtcNow.Subtract(permission.ValidUntil.Value).Seconds < 0)
            {
                var command = new CreateAccessPermissionsHistoryCommand(userId, request.LockId, DateTimeOffset.UtcNow, true, $"The lock has been opened but user is allowed to perform this action till {permission.ValidUntil.Value.UtcDateTime}.");

                await _mediator.Send(command, cancellationToken);
                return ApiResponse<string>.Success(command.Message!);
            }

            await _accessPermissionRepository.DeleteAsync(permission);

            var commandForCreateHistory = new CreateAccessPermissionsHistoryCommand(
                    userId,
                    request.LockId,
                    DateTimeOffset.UtcNow,
                    false,
                    $"User has not been allowed to perform this action due to expired access permission."
                );
            await _mediator.Send(commandForCreateHistory, cancellationToken);

            return ApiResponse<string>.Success(commandForCreateHistory.Message!);
        }

        private async Task<(bool, bool, AccessPermission?)> CheckUserHasPermission(OpenLockByIdQuery request, Guid userId, IEnumerable<UserRoles> userRoles)
        {
            var accessPermission = await _accessPermissionRepository.GetLockAccessOfUser(userId, request.LockId);
            if (userRoles.Any(privilegedRoles.Contains))
                return (false, true, accessPermission);

            if (accessPermission is null) return (false, false, accessPermission);
            return (true, false, accessPermission);
        }

        private async Task<bool> CheckLockExists(OpenLockByIdQuery request)
        {
            var _lock = await _lockRepository.GetByIdAsync(request.LockId);
            if (_lock is null) return false;
            return true;
        }
    }
}