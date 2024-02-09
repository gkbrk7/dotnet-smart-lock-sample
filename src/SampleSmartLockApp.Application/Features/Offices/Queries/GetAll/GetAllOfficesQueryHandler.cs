using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using SampleSmartLockApp.Application.Interfaces.Repositories;
using SampleSmartLockApp.Application.Wrappers;

namespace SampleSmartLockApp.Application.Features.Offices.Queries.GetAll
{
    public class GetAllOfficesQueryHandler(IOfficeRepositoryAsync officeRepository) : IRequestHandler<GetAllOfficesQuery, ApiPagedResponse<IEnumerable<GetAllOfficesViewModel>>>
    {
        private readonly IOfficeRepositoryAsync _officeRepository = officeRepository;

        public async Task<ApiPagedResponse<IEnumerable<GetAllOfficesViewModel>>> Handle(GetAllOfficesQuery request, CancellationToken cancellationToken)
        {
            var result = await _officeRepository.GetAllPaginatedAsync(request.PageNumber, request.PageSize);
            if (result is null)
                return ApiPagedResponse<IEnumerable<GetAllOfficesViewModel>>.PagedFail("No records found!", request.PageNumber, request.PageSize);

            return ApiPagedResponse<IEnumerable<GetAllOfficesViewModel>>.PagedSuccess(
                result.Select(o => new GetAllOfficesViewModel(o.Id, o.Name)).ToList(),
                request.PageNumber,
                request.PageSize
            );
        }
    }
}