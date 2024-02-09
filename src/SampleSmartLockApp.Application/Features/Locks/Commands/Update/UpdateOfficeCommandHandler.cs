using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using MediatR;
using SampleSmartLockApp.Application.Exceptions;
using SampleSmartLockApp.Application.Interfaces.Repositories;
using SampleSmartLockApp.Application.Wrappers;

namespace SampleSmartLockApp.Application.Features.Locks.Commands.Update
{
    public class UpdateLockCommandHandler(ILockRepositoryAsync lockRepository) : IRequestHandler<UpdateLockCommand, ApiResponse<Guid>>
    {
        private readonly ILockRepositoryAsync _lockRepository = lockRepository;

        public async Task<ApiResponse<Guid>> Handle(UpdateLockCommand command, CancellationToken cancellationToken)
        {
            var entity = await _lockRepository.GetByIdAsync(command.Id);
            if (entity is null)
                return ApiResponse<Guid>.Fail($"Lock not found.");

            await _lockRepository.UpdateAsync(entity);
            return ApiResponse<Guid>.Success(command.Id, "Lock successfully updated.");
        }
    }
}