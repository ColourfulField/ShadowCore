using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShadowCore.Models.EntityFramework;

namespace ShadowCore.DI
{
    public static class Registrations
    {
        public static void AddIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, Role>()
                    .AddEntityFrameworkStores<ShadowCoreDbContext>()
                    .AddDefaultTokenProviders()
                    .AddTokenProvider<DataProtectorTokenProvider<User>>("Default")
                    .AddUserManager<CustomUserManager>()
                    .AddRoleManager<CustomRoleManager>()
                    .AddUserStore<CustomUserStore>()
                    .AddUserValidator<CustomUserValidator>()
                    .AddRoleStore<RoleStore<Role, ShadowCoreDbContext, Guid>>()
                    .AddSignInManager<CustomSignInManager>();
        }
    }
}
