using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ShadowCore.DAL.EntityFramework.Abstract.Identity;
using ShadowCore.Models.EntityFramework;
using ShadowCore.Models.EntityFramework.Domain;

namespace ShadowCore.DAL.EntityFramework.Identity
{
    public class ApplicationUserStore : UserStore<User, Role, ShadowCoreDbContext, Guid>, IUserStore
    {
        public ApplicationUserStore(ShadowCoreDbContext context) : base(context)
        {
        }
    }
}
