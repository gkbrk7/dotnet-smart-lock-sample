using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SampleSmartLockApp.Domain.Entities;

namespace SampleSmartLockApp.Application.Interfaces.Repositories
{
    public interface IAccessPermissionRepositoryAsync : IGenericRepositoryAsync<AccessPermission>
    {
        Task<IEnumerable<AccessPermission>> GetAllPaginatedFilteredAsync(int pageNumber, int pageSize, Expression<Func<AccessPermission, bool>>? filter = null);
        Task<AccessPermission?> GetLockAccessOfUser(Guid userId, Guid lockId);
    }
}