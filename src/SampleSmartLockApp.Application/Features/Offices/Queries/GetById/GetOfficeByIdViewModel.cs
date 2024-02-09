using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SampleSmartLockApp.Application.Parameters;

namespace SampleSmartLockApp.Application.Features.Offices.Queries.GetAll
{
    public record GetOfficeByIdViewModel(Guid Id, string Name);
}