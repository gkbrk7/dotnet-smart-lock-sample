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
    public class OfficeRepositoryAsync(ApplicationDbContext dbContext) : GenericRepositoryAsync<Office>(dbContext), IOfficeRepositoryAsync
    {
        private readonly DbSet<Office> offices = dbContext.Set<Office>();
    }
}