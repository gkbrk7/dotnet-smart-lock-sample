using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using MediatR;
using SampleSmartLockApp.Application.Exceptions;
using SampleSmartLockApp.Application.Interfaces.Repositories;
using SampleSmartLockApp.Application.Wrappers;

namespace SampleSmartLockApp.Application.Features.Offices.Commands.Update
{
    public class UpdateOfficeCommandHandler(IOfficeRepositoryAsync officeRepository) : IRequestHandler<UpdateOfficeCommand, ApiResponse<Guid>>
    {
        private readonly IOfficeRepositoryAsync _officeRepository = officeRepository;

        public async Task<ApiResponse<Guid>> Handle(UpdateOfficeCommand command, CancellationToken cancellationToken)
        {
            var entity = await _officeRepository.GetByIdAsync(command.Id);
            if (entity is null)
                return ApiResponse<Guid>.Fail($"Office Not Found.");

            entity.Name = command.Name;
            await _officeRepository.UpdateAsync(entity);
            return ApiResponse<Guid>.Success(command.Id, "Office Successfully Updated.");
        }
    }
}