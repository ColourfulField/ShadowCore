using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ShadowCore.DAL.EntityFramework.Abstract.Identity;
using ShadowCore.Models.EntityFramework.Domain;
using ShadowTools.Utilities.Exceptions;

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

        public new async Task CreateAsync(User user, string password)
        {
            var result = await base.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                throw new ValidationException(result.Errors.Select(x => x.Description).AsEnumerable());
            }
        }

        public new async Task<bool> CheckPasswordAsync(User user, string password)
        {
            return await base.CheckPasswordAsync(user, password);
        }

        public new async Task<User> FindByEmailAsync(string email)
        {
            return await base.FindByEmailAsync(email);
        }
    }
}
