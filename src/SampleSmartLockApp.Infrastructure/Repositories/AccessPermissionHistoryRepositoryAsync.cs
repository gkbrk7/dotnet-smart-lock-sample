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
    public class AccessPermissionHistoryRepositoryAsync(ApplicationDbContext dbContext) : GenericRepositoryAsync<AccessPermissionHistory>(dbContext), IAccessPermissionHistoryRepositoryAsync
    {
        private readonly DbSet<AccessPermissionHistory> accessPermissionHistories = dbContext.Set<AccessPermissionHistory>();
    }
}