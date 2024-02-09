using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SampleSmartLockApp.Application.Interfaces.Repositories;
using SampleSmartLockApp.Application.Wrappers;
using SampleSmartLockApp.Domain.Entities;

namespace SampleSmartLockApp.Application.Features.Locks.Commands.Create
{
    public class CreateLockCommandHandler(ILockRepositoryAsync lockRepository, IOfficeRepositoryAsync officeRepository) : IRequestHandler<CreateLockCommand, ApiResponse<Guid>>
    {
        private readonly ILockRepositoryAsync _lockRepository = lockRepository;
        private readonly IOfficeRepositoryAsync _officeRepository = officeRepository;

        public async Task<ApiResponse<Guid>> Handle(CreateLockCommand command, CancellationToken cancellationToken)
        {
            var office = await _officeRepository.GetByIdAsync(command.OfficeId);
            if (office is null) return ApiResponse<Guid>.Fail("Office not found.");

            var entity = new Lock { Name = command.Name, OfficeId = office.Id };
            try
            {
                var _lock = await _lockRepository.AddAsync(entity);
                return ApiResponse<Guid>.Success(_lock.Id);
            }
            catch (Exception)
            {
                return ApiResponse<Guid>.Fail("Lock has already been created.");
            }
        }
    }
}