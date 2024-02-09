using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SampleSmartLockApp.Application.Interfaces.Repositories;
using SampleSmartLockApp.Application.Wrappers;
using SampleSmartLockApp.Domain.Entities;

namespace SampleSmartLockApp.Application.Features.Offices.Commands.Create
{
    public class CreateOfficeCommandHandler(IOfficeRepositoryAsync officeRepository) : IRequestHandler<CreateOfficeCommand, ApiResponse<Guid>>
    {
        private readonly IOfficeRepositoryAsync _officeRepository = officeRepository;

        public async Task<ApiResponse<Guid>> Handle(CreateOfficeCommand command, CancellationToken cancellationToken)
        {
            var entity = new Office { Name = command.Name };
            var office = await _officeRepository.AddAsync(entity);
            return ApiResponse<Guid>.Success(office.Id);
        }
    }
}