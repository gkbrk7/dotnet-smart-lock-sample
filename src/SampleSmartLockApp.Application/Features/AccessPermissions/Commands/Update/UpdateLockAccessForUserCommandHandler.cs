using System.Security.Principal;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SampleSmartLockApp.Application.Enums;
using SampleSmartLockApp.Application.Interfaces;
using SampleSmartLockApp.Application.Interfaces.Repositories;
using SampleSmartLockApp.Application.Wrappers;
using SampleSmartLockApp.Domain.Entities;
using SampleSmartLockApp.Infrastructure.Contexts;

namespace SampleSmartLockApp.Application.Features.AccessPermissions.Commands.Update
{
    public class UpdateLockAccessForUserCommandHandler(IAccessPermissionRepositoryAsync accessPermissionRepository) : IRequestHandler<UpdateLockAccessForUserCommand, ApiResponse<UpdateLockAccessForUserViewModel>>
    {
        private readonly IAccessPermissionRepositoryAsync _accessPermissionRepository = accessPermissionRepository;

        public async Task<ApiResponse<UpdateLockAccessForUserViewModel>> Handle(UpdateLockAccessForUserCommand command, CancellationToken cancellationToken)
        {
            var permission = await _accessPermissionRepository.GetLockAccessOfUser(command.UserId, command.LockId);

            if (permission is null)
                return ApiResponse<UpdateLockAccessForUserViewModel>.Fail("Access permission not found!");

            permission.UserId = command.UserId;
            permission.LockId = command.LockId;
            permission.ValidUntil = command.ValidUntil;

            await _accessPermissionRepository.UpdateAsync(permission);

            return ApiResponse<UpdateLockAccessForUserViewModel>.Success(new UpdateLockAccessForUserViewModel(permission.Id, permission.UserId, permission.LockId, permission.ValidUntil));
        }
    }
}