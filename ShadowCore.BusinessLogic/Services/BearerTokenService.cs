using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using ShadowCore.BusinessLogic.Services.Abstract;
using Microsoft.IdentityModel.Tokens;
using ShadowCore.Common.Models.Authentication;
using ShadowCore.Common.Options;
using ShadowCore.DAL.EntityFramework.Abstract;

namespace ShadowCore.BusinessLogic.Services
{
    public class BearerTokenService : BaseService, IBearerTokenService
    {
        private readonly BearerAuthenticationOptions _authenticationOptions;

        public BearerTokenService(IOptions<AuthenticationOptions> authenticationOptionsAccessor) : base()
        {
            _authenticationOptions = authenticationOptionsAccessor.Value.Bearer;
        }

        public SecurityKey GenerateSingingKey(string secret = null)
        {
            secret = secret ?? _authenticationOptions.Secret;
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));
        }

        public async Task<AuthToken> GenerateToken()
        {
            //TODO user claims from real DB
            //var userClaims = GetUserClaims(user, _tokenOptions.Issuer, _tokenOptions.Audience);
            //userClaims.AddRange(await _userManager.GetClaimsAsync(user).ConfigureAwait(false));
            //var identity = new ClaimsIdentity(new GenericIdentity(user.Email), userClaims);

            var securityToken = new JwtSecurityToken(_authenticationOptions.Issuer,
                                                     _authenticationOptions.Audience,
                                                     notBefore: DateTime.UtcNow,
                                                     claims: null, //identity.Claims,
                                                     expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(_authenticationOptions.AccessTokenLifetime)),
                                                     signingCredentials: new SigningCredentials(GenerateSingingKey(), SecurityAlgorithms.HmacSha256));

            var encodedToken = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return new AuthToken
                   {
                       Token = encodedToken,
                       Issuer = _authenticationOptions.Issuer,
                       ValidTo = securityToken.ValidTo,
                   };
        }

        public async Task<AuthToken> RenewToken(string refreshToken)
        {
            return await GenerateToken();
        }

        public async Task<AuthorizationTokens> RenewTokens(string refreshToken)
        {
            var newRefreshToken = new AuthToken();
            var accessToken = await GenerateToken();
            return new AuthorizationTokens { AccessToken = accessToken, RefreshToken = newRefreshToken};
        }
    }
}
