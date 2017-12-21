using Microsoft.IdentityModel.Tokens;
using ShadowTools.AutomaticDI.Interfaces;

namespace ShadowCore.BusinessLogic.Services.Abstract
{
    public interface IBearerTokenService : ISingletonLifetime
    {
        SecurityKey GenerateSingingKey(string secret = null);
        string GenerateToken();
    }
}
