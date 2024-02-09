using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using SampleSmartLockApp.Application.Wrappers;

namespace SampleSmartLockApp.Application.Features.Users.Queries.GetAllUsers
{
    public record GetAllUsersQuery(int PageNumber, int PageSize) : IRequest<ApiPagedResponse<IEnumerable<GetAllUsersViewModel>>>;
}