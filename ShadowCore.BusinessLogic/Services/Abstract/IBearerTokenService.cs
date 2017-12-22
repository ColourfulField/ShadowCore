using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using ShadowCore.Common.Models.Authentication;
using ShadowTools.AutomaticDI.Interfaces;

namespace ShadowCore.BusinessLogic.Services.Abstract
{
    public interface IBearerTokenService
    {
        SecurityKey GenerateSingingKey(string secret = null);
        Task<AuthToken> GenerateToken();
        Task<AuthToken> RenewToken(string refreshToken);
        Task<AuthorizationTokens> RenewTokens(string refreshToken);
    }
}
