using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using MediatR;
using SampleSmartLockApp.Application.Exceptions;
using SampleSmartLockApp.Application.Interfaces.Repositories;
using SampleSmartLockApp.Application.Wrappers;

namespace SampleSmartLockApp.Application.Features.Locks.Commands.Delete
{
    public class DeleteLockCommandHandler(ILockRepositoryAsync lockRepository) : IRequestHandler<DeleteLockCommand, ApiResponse<Guid>>
    {
        private readonly ILockRepositoryAsync _lockRepository = lockRepository;

        public async Task<ApiResponse<Guid>> Handle(DeleteLockCommand command, CancellationToken cancellationToken)
        {
            var entity = await _lockRepository.GetByIdAsync(command.Id);
            if (entity is null)
                return ApiResponse<Guid>.Fail($"Lock not found.");

            await _lockRepository.DeleteAsync(entity);
            return ApiResponse<Guid>.Success(command.Id, "Lock successfully deleted.");
        }
    }
}