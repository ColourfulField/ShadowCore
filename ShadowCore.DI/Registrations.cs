using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using ShadowCore.DAL.EntityFramework.Abstract.Identity;
using ShadowCore.DAL.EntityFramework.Identity;
using ShadowCore.Models.EntityFramework;
using ShadowCore.Models.EntityFramework.Domain;

namespace ShadowCore.DI
{
    public static class Registrations
    {
        public static void AddIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, Role>()
                    .AddEntityFrameworkStores<ShadowCoreDbContext>()
                    .AddDefaultTokenProviders()
                    .AddTokenProvider<DataProtectorTokenProvider<User>>("Default");
                   
            services.AddScoped<IUserManager, ApplicationUserManager>();
            services.AddScoped<IRoleManager, ApplicationRoleManager>();
            services.AddScoped<IUserStore, ApplicationUserStore>();
            services.AddScoped<IUserValidator, ApplicationUserValidator>();
            services.AddScoped<IRoleStore, ApplicationRoleStore>();
            services.AddScoped<ISignInManager, ApplicationSignInManager>();
        }
    }
}
