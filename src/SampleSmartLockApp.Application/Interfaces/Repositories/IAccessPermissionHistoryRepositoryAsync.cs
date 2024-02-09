using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SampleSmartLockApp.Application.Wrappers;
using SampleSmartLockApp.Domain.Entities;

namespace SampleSmartLockApp.Application.Interfaces.Repositories
{
    public interface IAccessPermissionHistoryRepositoryAsync : IGenericRepositoryAsync<AccessPermissionHistory>
    {
    }
}