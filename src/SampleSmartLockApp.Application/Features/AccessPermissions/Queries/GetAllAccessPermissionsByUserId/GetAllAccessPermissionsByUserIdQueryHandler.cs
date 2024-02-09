using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using SampleSmartLockApp.Application.Enums;
using SampleSmartLockApp.Application.Interfaces;
using SampleSmartLockApp.Application.Interfaces.Repositories;
using SampleSmartLockApp.Application.Wrappers;

namespace SampleSmartLockApp.Application.Features.AccessPermissions.Queries.GetAllAccessPermissionsByUserId
{
    public class GetAllAccessPermissionsByUserIdQueryHandler(IAccessPermissionRepositoryAsync accessPermissionRepository) : IRequestHandler<GetAllAccessPermissionsByUserIdQuery, ApiPagedResponse<GetAllAccessPermissionsByUserIdViewModel>>
    {
        private readonly IAccessPermissionRepositoryAsync _accessPermissionRepository = accessPermissionRepository;

        public async Task<ApiPagedResponse<GetAllAccessPermissionsByUserIdViewModel>> Handle(GetAllAccessPermissionsByUserIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _accessPermissionRepository.GetAllPaginatedFilteredAsync(request.PageNumber, request.PageSize, r => r.UserId == request.UserId);

            if (!result.Any())
                return ApiPagedResponse<GetAllAccessPermissionsByUserIdViewModel>.PagedFail($"No records found.", request.PageNumber, request.PageSize);

            var userAccessPermissions = result
                .Select(r => new GetAllAccessPermissionByUserIdDetailsViewModel(r.Id, r.LockId, r.Lock!.Name, r.ValidUntil))
                .ToList();

            var model = new GetAllAccessPermissionsByUserIdViewModel(request.UserId, userAccessPermissions);

            return ApiPagedResponse<GetAllAccessPermissionsByUserIdViewModel>.PagedSuccess(model, request.PageNumber, request.PageSize);
        }
    }
}