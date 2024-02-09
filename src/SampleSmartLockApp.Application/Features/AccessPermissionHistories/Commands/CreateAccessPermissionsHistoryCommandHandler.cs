using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using SampleSmartLockApp.Application.Interfaces.Repositories;
using SampleSmartLockApp.Application.Wrappers;
using SampleSmartLockApp.Domain.Entities;

namespace SampleSmartLockApp.Application.Features.AccessPermissionHistories.Commands
{
    public class CreateAccessPermissionsHistoryCommandHandler(IAccessPermissionHistoryRepositoryAsync accessPermissionHistoryRepository) : IRequestHandler<CreateAccessPermissionsHistoryCommand, ApiResponse<CreateAccessPermissionsHistoryViewModel>>
    {
        private readonly IAccessPermissionHistoryRepositoryAsync _accessPermissionHistoryRepository = accessPermissionHistoryRepository;

        public async Task<ApiResponse<CreateAccessPermissionsHistoryViewModel>> Handle(CreateAccessPermissionsHistoryCommand command, CancellationToken cancellationToken)
        {
            var entity = new AccessPermissionHistory
            {
                UserId = command.UserId,
                LockId = command.LockId,
                Timestamp = command.Timestamp,
                IsConfirmed = command.IsConfirmed,
                Message = command.Message
            };

            var accessPermission = await _accessPermissionHistoryRepository.AddAsync(entity);

            var result = new CreateAccessPermissionsHistoryViewModel(accessPermission.Id, accessPermission.UserId, accessPermission.LockId, accessPermission.Timestamp, accessPermission.IsConfirmed, accessPermission.Message);

            return ApiResponse<CreateAccessPermissionsHistoryViewModel>.Success(result);
        }
    }
}