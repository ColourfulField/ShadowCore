using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ShadowCore.DAL.EntityFramework.Abstract.Identity;
using ShadowCore.Models.EntityFramework.Domain;

namespace ShadowCore.DAL.EntityFramework.Identity
{
    public class ApplicationUserManager : UserManager<User>, IUserManager
    {
        public ApplicationUserManager(
            ApplicationUserStore store,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<User> passwordHasher,
            IEnumerable<IUserValidator<User>> userValidators,
            IEnumerable<IPasswordValidator<User>> passwordValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            IServiceProvider services,
            ILogger<UserManager<User>> logger)
            : base(
                   store,
                   optionsAccessor,
                   passwordHasher,
                   userValidators.OfType<ApplicationUserValidator>(),
                   passwordValidators,
                   keyNormalizer,
                   errors,
                   services,
                   logger)
        {
        }
    }
}
