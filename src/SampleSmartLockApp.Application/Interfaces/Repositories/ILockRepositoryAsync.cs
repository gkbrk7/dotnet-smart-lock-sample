using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SampleSmartLockApp.Domain.Entities;

namespace SampleSmartLockApp.Application.Interfaces.Repositories
{
    public interface ILockRepositoryAsync : IGenericRepositoryAsync<Lock>
    {
        Task<Lock?> GetByLockIdWithOfficeAsync(Guid id);
        Task<IEnumerable<Lock>> GetAllWithOfficePaginatedAsync(int pageNumber, int pageSize);
    }
}