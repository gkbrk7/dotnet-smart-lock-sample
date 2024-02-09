using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using SampleSmartLockApp.Application.Interfaces.Repositories;
using SampleSmartLockApp.Application.Wrappers;

namespace SampleSmartLockApp.Application.Features.AccessPermissionHistories.Queries
{
    public class GetAllAccessPermissionsHistoryQueryHandler(IAccessPermissionHistoryRepositoryAsync accessPermissionHistoryRepository) : IRequestHandler<GetAllAccessPermissionsHistoryQuery, ApiPagedResponse<IEnumerable<GetAllAccessPermissionsHistoryViewModel>>>
    {
        private readonly IAccessPermissionHistoryRepositoryAsync _accessPermissionHistoryRepository = accessPermissionHistoryRepository;

        public async Task<ApiPagedResponse<IEnumerable<GetAllAccessPermissionsHistoryViewModel>>> Handle(GetAllAccessPermissionsHistoryQuery request, CancellationToken cancellationToken)
        {
            var accessPermissionHistories = await _accessPermissionHistoryRepository.GetAllPaginatedAsync(request.PageNumber, request.PageSize);

            if (accessPermissionHistories is null)
                return ApiPagedResponse<IEnumerable<GetAllAccessPermissionsHistoryViewModel>>.PagedFail("No records found!", request.PageNumber, request.PageSize);

            var result = accessPermissionHistories
                .Select(aph => new GetAllAccessPermissionsHistoryViewModel(aph.UserId, aph.LockId, aph.Timestamp, aph.IsConfirmed, aph.Message))
                .ToList();

            return ApiPagedResponse<IEnumerable<GetAllAccessPermissionsHistoryViewModel>>.PagedSuccess(result, request.PageNumber, request.PageSize);
        }
    }
}