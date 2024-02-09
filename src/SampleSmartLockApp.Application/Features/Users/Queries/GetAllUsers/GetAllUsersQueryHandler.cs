using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SampleSmartLockApp.Application.Wrappers;
using SampleSmartLockApp.Infrastructure.Contexts;

namespace SampleSmartLockApp.Application.Features.Users.Queries.GetAllUsers
{
    public class GetAllUsersQueryHandler(UserManager<ApplicationUser> userManager) : IRequestHandler<GetAllUsersQuery, ApiPagedResponse<IEnumerable<GetAllUsersViewModel>>>
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;

        public async Task<ApiPagedResponse<IEnumerable<GetAllUsersViewModel>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userManager.Users
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(u => new GetAllUsersViewModel(u.Id, u.UserName, u.Email))
                .AsNoTracking()
                .ToListAsync();

            return ApiPagedResponse<IEnumerable<GetAllUsersViewModel>>.PagedSuccess(users, request.PageNumber, request.PageSize);
        }
    }
}