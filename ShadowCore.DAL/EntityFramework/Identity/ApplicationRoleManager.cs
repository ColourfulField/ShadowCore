using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using ShadowCore.DAL.EntityFramework.Abstract.Identity;
using ShadowCore.Models.EntityFramework.Domain;

namespace ShadowCore.DAL.EntityFramework.Identity
{
    public class ApplicationRoleManager: RoleManager<Role>, IRoleManager
    {
        public ApplicationRoleManager(
            IRoleStore<Role> store,
            IEnumerable<IRoleValidator<Role>> roleValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            ILogger<RoleManager<Role>> logger)
            : base(store,
                  roleValidators,
                  keyNormalizer,
                  errors,
                  logger)
        {
        }
    }
}
