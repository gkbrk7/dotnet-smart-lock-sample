using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using SampleSmartLockApp.Application.Interfaces.Repositories;
using SampleSmartLockApp.Domain.Entities;
using SampleSmartLockApp.Infrastructure.Contexts;

namespace SampleSmartLockApp.Infrastructure.Repositories
{
    public class AccessPermissionRepositoryAsync(ApplicationDbContext dbContext) : GenericRepositoryAsync<AccessPermission>(dbContext), IAccessPermissionRepositoryAsync
    {
        private readonly DbSet<AccessPermission> accessPermissions = dbContext.Set<AccessPermission>();

        public async Task<IEnumerable<AccessPermission>> GetAllPaginatedFilteredAsync(int pageNumber, int pageSize, Expression<Func<AccessPermission, bool>>? filter)
        {
            var query = accessPermissions
                    .Include(ap => ap.Lock)
                    .Where(ap => ap.ValidUntil == null || ap.ValidUntil > DateTimeOffset.UtcNow);

            if (filter is not null)
                query = query
                    .Where(filter)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize);
            else
                query = query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize);

            return await query.AsNoTracking()
            .ToListAsync();
        }

        public async Task<AccessPermission?> GetLockAccessOfUser(Guid userId, Guid lockId)
        {
            return await accessPermissions
                .Include(ap => ap.Lock)
                .Where(ap => ap.UserId == userId &&
                    ap.LockId == lockId)
                .Where(ap => ap.ValidUntil == null || ap.ValidUntil > DateTimeOffset.UtcNow)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }
    }
}