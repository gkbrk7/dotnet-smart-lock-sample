using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using SampleSmartLockApp.Application.Interfaces.Repositories;
using SampleSmartLockApp.Application.Wrappers;

namespace SampleSmartLockApp.Application.Features.Locks.Queries.GetAll
{
    public class GetLockByIdQueryHandler(ILockRepositoryAsync lockRepository) : IRequestHandler<GetLockByIdQuery, ApiResponse<GetLockByIdViewModel>>
    {
        private readonly ILockRepositoryAsync _lockRepository = lockRepository;

        public async Task<ApiResponse<GetLockByIdViewModel>> Handle(GetLockByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _lockRepository.GetByLockIdWithOfficeAsync(request.Id);

            if (result is null)
                return ApiResponse<GetLockByIdViewModel>.Fail($"Lock with id: {request.Id} not found!");

            return ApiResponse<GetLockByIdViewModel>.Success(
                new GetLockByIdViewModel(result.Id, result.OfficeId, result.Name, result.Office!.Name)
            );
        }

    }
}