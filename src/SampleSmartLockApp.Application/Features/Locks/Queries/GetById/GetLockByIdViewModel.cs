using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SampleSmartLockApp.Application.Parameters;

namespace SampleSmartLockApp.Application.Features.Locks.Queries.GetAll
{
    public record GetLockByIdViewModel(Guid Id, Guid OfficeId, string Name, string OfficeName);
}