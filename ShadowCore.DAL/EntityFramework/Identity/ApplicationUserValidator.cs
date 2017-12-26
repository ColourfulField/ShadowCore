using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ShadowCore.DAL.EntityFramework.Abstract.Identity;
using ShadowCore.Models.EntityFramework.Domain;

namespace ShadowCore.DAL.EntityFramework.Identity
{
    public class ApplicationUserValidator : UserValidator<User>, IUserValidator
    {
        private readonly IUserManager _userManager;

        public ApplicationUserValidator(IUserManager userManager): base()
        {
            _userManager = userManager;
        }

        public async Task<bool> ValidateAsync(User user)
        {
            // Identity does not follow dependency inversion, so we need to 
            if (_userManager is UserManager<User> userManager)
            {
                var result = await base.ValidateAsync(userManager, user);
                return result.Succeeded;
            }
            return false;
        }
    }
}
