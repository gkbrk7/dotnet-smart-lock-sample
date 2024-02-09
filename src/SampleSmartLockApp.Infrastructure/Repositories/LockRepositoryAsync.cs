using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SampleSmartLockApp.Application.Interfaces.Repositories;
using SampleSmartLockApp.Domain.Entities;
using SampleSmartLockApp.Infrastructure.Contexts;

namespace SampleSmartLockApp.Infrastructure.Repositories
{
    public class LockRepositoryAsync(ApplicationDbContext dbContext) : GenericRepositoryAsync<Lock>(dbContext), ILockRepositoryAsync
    {
        private readonly DbSet<Lock> locks = dbContext.Set<Lock>();

        public async Task<IEnumerable<Lock>> GetAllWithOfficePaginatedAsync(int pageNumber, int pageSize)
        {
            return await locks
                .Include(l => l.Office)
                .AsNoTracking()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Lock?> GetByLockIdWithOfficeAsync(Guid id)
        {
            return await locks
                .Include(l => l.Office)
                .AsNoTracking()
                .FirstOrDefaultAsync(l => l.Id == id);
        }
    }
}