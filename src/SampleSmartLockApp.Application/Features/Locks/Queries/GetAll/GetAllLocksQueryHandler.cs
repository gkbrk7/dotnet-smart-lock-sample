using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using SampleSmartLockApp.Application.Interfaces.Repositories;
using SampleSmartLockApp.Application.Wrappers;

namespace SampleSmartLockApp.Application.Features.Locks.Queries.GetAll
{
    public class GetAllLocksQueryHandler(ILockRepositoryAsync lockRepository) : IRequestHandler<GetAllLocksQuery, ApiPagedResponse<IEnumerable<GetAllLocksViewModel>>>
    {
        private readonly ILockRepositoryAsync _lockRepository = lockRepository;

        public async Task<ApiPagedResponse<IEnumerable<GetAllLocksViewModel>>> Handle(GetAllLocksQuery request, CancellationToken cancellationToken)
        {
            var result = await _lockRepository.GetAllWithOfficePaginatedAsync(request.PageNumber, request.PageSize);

            if (!result.Any())
                return ApiPagedResponse<IEnumerable<GetAllLocksViewModel>>.PagedFail($"No records found.", request.PageNumber, request.PageSize);

            return ApiPagedResponse<IEnumerable<GetAllLocksViewModel>>.PagedSuccess(
                result.Select(o => new GetAllLocksViewModel(o.Id, o.OfficeId, o.Name)).ToList(),
                request.PageNumber,
                request.PageSize
            );
        }
    }
}