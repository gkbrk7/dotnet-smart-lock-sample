using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using SampleSmartLockApp.Application.Enums;
using SampleSmartLockApp.Application.Interfaces;
using SampleSmartLockApp.Application.Interfaces.Repositories;
using SampleSmartLockApp.Application.Wrappers;
using SampleSmartLockApp.Domain.Entities;


namespace SampleSmartLockApp.Application.Features.AccessPermissions.Queries.GetAllAccessPermissions
{
    public class GetAllAccessPermissionsQueryHandler(IAccessPermissionRepositoryAsync accessPermissionRepository) : IRequestHandler<GetAllAccessPermissionsQuery, ApiPagedResponse<GetAllAccessPermissionsViewModel>>
    {
        private readonly IAccessPermissionRepositoryAsync _accessPermissionRepository = accessPermissionRepository;

        public async Task<ApiPagedResponse<GetAllAccessPermissionsViewModel>> Handle(GetAllAccessPermissionsQuery request, CancellationToken cancellationToken)
        {
            var result = await _accessPermissionRepository.GetAllPaginatedFilteredAsync(request.PageNumber, request.PageSize);

            if (!result.Any())
                return ApiPagedResponse<GetAllAccessPermissionsViewModel>.PagedFail($"No records found.", request.PageNumber, request.PageSize);

            var model = ToViewModel(result);

            return ApiPagedResponse<GetAllAccessPermissionsViewModel>.PagedSuccess(model, request.PageNumber, request.PageSize);
        }

        private static GetAllAccessPermissionsViewModel ToViewModel(IEnumerable<AccessPermission> result)
        {
            var details = result
                .Select(r =>
                    new GetAllAccessPermissionDetailsViewModel(r.Id, r.UserId, r.LockId, r.Lock!.Name, r.ValidUntil))
                .ToList();

            return new GetAllAccessPermissionsViewModel(details);
        }
    }
}