using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SampleSmartLockApp.Application.Enums;
using SampleSmartLockApp.Infrastructure.Contexts;

namespace SampleSmartLockApp.Infrastructure.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var roleNames = new[] { UserRoles.Employee.ToString(), UserRoles.OfficeManager.ToString(), UserRoles.Director.ToString(), UserRoles.Administrator.ToString() };

            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                    await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }
    }
}