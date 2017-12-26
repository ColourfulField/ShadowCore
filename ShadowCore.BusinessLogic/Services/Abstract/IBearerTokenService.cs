using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using ShadowCore.Common.Models.Authentication;
using ShadowCore.Models.DTO;
using ShadowTools.AutomaticDI.Interfaces;

namespace ShadowCore.BusinessLogic.Services.Abstract
{
    public interface IBearerTokenService : IScopedLifetime
    {
        SecurityKey GenerateSingingKey(string secret = null);
        Task<AuthToken> GenerateAccessToken(UserDTO userDto);
        Task<AuthorizationTokens> GenerateTokens(UserDTO userDto);
        Task<AuthToken> RenewAccessToken(string refreshToken);
        Task<AuthorizationTokens> RenewTokens(string refreshToken);
    }
}
