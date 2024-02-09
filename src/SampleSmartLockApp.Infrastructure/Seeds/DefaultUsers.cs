using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SampleSmartLockApp.Application.Enums;
using SampleSmartLockApp.Infrastructure.Contexts;

namespace SampleSmartLockApp.Infrastructure.Seeds
{
    public static class DefaultUsers
    {
        public static async Task SeedEmployeeAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var employee = new ApplicationUser
            {
                UserName = "employee@company.com",
                Email = "employee@company.com",
                EmailConfirmed = true
            };

            var user = await userManager.FindByEmailAsync(employee.Email);
            if (user == null)
            {
                await userManager.CreateAsync(employee, "P@$$W0rd123");
                await userManager.AddToRoleAsync(employee, UserRoles.Employee.ToString());
            }
        }

        public static async Task SeedDirectorAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var director = new ApplicationUser
            {
                UserName = "director@company.com",
                Email = "director@company.com",
                EmailConfirmed = true
            };

            var user = await userManager.FindByEmailAsync(director.Email);
            if (user == null)
            {
                await userManager.CreateAsync(director, "P@$$W0rd123");
                await userManager.AddToRoleAsync(director, UserRoles.Director.ToString());
            }
        }

        public static async Task SeedAdministratorAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var admin = new ApplicationUser
            {
                UserName = "admin@company.com",
                Email = "admin@company.com",
                EmailConfirmed = true
            };

            var user = await userManager.FindByEmailAsync(admin.Email);
            if (user == null)
            {
                await userManager.CreateAsync(admin, "P@$$W0rd123");
                await userManager.AddToRoleAsync(admin, UserRoles.Administrator.ToString());
            }
        }
    }
}