using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using SampleSmartLockApp.Application.Interfaces.Repositories;
using SampleSmartLockApp.Application.Wrappers;

namespace SampleSmartLockApp.Application.Features.Offices.Queries.GetAll
{
    public class GetOfficeByIdQueryHandler(IOfficeRepositoryAsync officeRepository) : IRequestHandler<GetOfficeByIdQuery, ApiResponse<GetOfficeByIdViewModel>>
    {
        private readonly IOfficeRepositoryAsync _officeRepository = officeRepository;

        public async Task<ApiResponse<GetOfficeByIdViewModel>> Handle(GetOfficeByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _officeRepository.GetByIdAsync(request.Id);
            if (result is null)
                return ApiResponse<GetOfficeByIdViewModel>.Fail($"Office with id: {request.Id} not found!");
            return ApiResponse<GetOfficeByIdViewModel>.Success(new GetOfficeByIdViewModel(result.Id, result.Name));
        }

    }
}