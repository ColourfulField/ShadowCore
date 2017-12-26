using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ShadowCore.DAL.EntityFramework.Abstract.Identity;
using ShadowCore.Models.EntityFramework.Domain;

namespace ShadowCore.DAL.EntityFramework.Identity
{
    public class ApplicationSignInManager : SignInManager<User>, ISignInManager
    {
        public ApplicationSignInManager(
            UserManager<User> userManager,
            IHttpContextAccessor contextAccessor,
            IUserClaimsPrincipalFactory<User> claimsFactory,
            IOptions<IdentityOptions> optionsAccessor,
            ILogger<SignInManager<User>> logger,
            Microsoft.AspNetCore.Authentication.IAuthenticationSchemeProvider schemes)
            : base(userManager,
                   contextAccessor,
                   claimsFactory,
                   optionsAccessor,
                   logger,
                   schemes)
        {
        }
    }
}
