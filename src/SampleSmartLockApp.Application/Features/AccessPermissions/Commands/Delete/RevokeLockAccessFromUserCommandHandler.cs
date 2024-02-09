using System.Security.Principal;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SampleSmartLockApp.Application.Enums;
using SampleSmartLockApp.Application.Interfaces;
using SampleSmartLockApp.Application.Interfaces.Repositories;
using SampleSmartLockApp.Application.Wrappers;
using SampleSmartLockApp.Domain.Entities;
using SampleSmartLockApp.Infrastructure.Contexts;

namespace SampleSmartLockApp.Application.Features.AccessPermissions.Commands.Delete
{
    public class RevokeLockAccessFromUserCommandHandler(IAccessPermissionRepositoryAsync accessPermissionRepository) : IRequestHandler<RevokeLockAccessFromUserCommand, ApiResponse<RevokeLockAccessFromUserViewModel>>
    {
        private readonly IAccessPermissionRepositoryAsync _accessPermissionRepository = accessPermissionRepository;

        public async Task<ApiResponse<RevokeLockAccessFromUserViewModel>> Handle(RevokeLockAccessFromUserCommand command, CancellationToken cancellationToken)
        {
            var permission = await _accessPermissionRepository.GetLockAccessOfUser(command.UserId, command.LockId);
            if (permission is null)
                return ApiResponse<RevokeLockAccessFromUserViewModel>.Fail("Access permission not found!");

            await _accessPermissionRepository.DeleteAsync(permission);

            return ApiResponse<RevokeLockAccessFromUserViewModel>.Success(new RevokeLockAccessFromUserViewModel(permission.Id, permission.UserId, permission.LockId, permission.ValidUntil));
        }
    }
}