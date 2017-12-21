using Microsoft.IdentityModel.Tokens;
using ShadowBox.AutomaticDI.Interfaces;

namespace DotnetCoreAngularStarter.BusinessLogic.Services.Abstract
{
    public interface IBearerTokenService : ISingletonLifetime
    {
        SecurityKey GenerateSingingKey(string secret = null);
        string GenerateToken();
    }
}
