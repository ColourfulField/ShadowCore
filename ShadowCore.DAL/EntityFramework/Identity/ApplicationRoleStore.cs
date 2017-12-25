using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ShadowCore.DAL.EntityFramework.Abstract.Identity;
using ShadowCore.Models.EntityFramework;
using ShadowCore.Models.EntityFramework.Domain;

namespace ShadowCore.DAL.EntityFramework.Identity
{
    public class ApplicationRoleStore: RoleStore<Role, ShadowCoreDbContext, Guid>, IRoleStore
    {
        public ApplicationRoleStore(ShadowCoreDbContext context): base(context)
        {
            
        }
    }
}
