using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SampleSmartLockApp.Infrastructure.Contexts;
using Microsoft.AspNetCore.Identity;
using SampleSmartLockApp.Application.Interfaces;
using SampleSmartLockApp.Infrastructure.Repositories;
using SampleSmartLockApp.Application.Interfaces.Repositories;

namespace SampleSmartLockApp.Infrastructure
{
    public static class ServiceRegistrations
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            services
                .AddIdentityApiEndpoints<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddScoped(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
            services.AddScoped<IOfficeRepositoryAsync, OfficeRepositoryAsync>();
            services.AddScoped<ILockRepositoryAsync, LockRepositoryAsync>();
            services.AddScoped<IAccessPermissionRepositoryAsync, AccessPermissionRepositoryAsync>();
            services.AddScoped<IAccessPermissionHistoryRepositoryAsync, AccessPermissionHistoryRepositoryAsync>();
        }
    }
}