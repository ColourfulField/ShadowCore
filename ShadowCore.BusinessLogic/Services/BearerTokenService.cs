using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using ShadowCore.BusinessLogic.Services.Abstract;
using Microsoft.IdentityModel.Tokens;

namespace ShadowCore.BusinessLogic.Services
{
    public class BearerTokenService : BaseService, IBearerTokenService
    {
        private readonly string _secret;

        public BearerTokenService()
        {
            _secret = "ololo kek cheburek trolllllll";
        }

        public SecurityKey GenerateSingingKey(string secret = null)
        {
            secret = secret ?? _secret;
            //TODO replace with normal key
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));
        }

        public string GenerateToken()
        {
            //var userClaims = GetUserClaims(user, _tokenOptions.Issuer, _tokenOptions.Audience);
            //userClaims.AddRange(await _userManager.GetClaimsAsync(user).ConfigureAwait(false));
            //var identity = new ClaimsIdentity(new GenericIdentity(user.Email), userClaims);

            var securityToken = new JwtSecurityToken(
                                                     "", //_tokenOptions.Issuer,
                                                     "", //_tokenOptions.Audience,
                                                     notBefore: DateTime.UtcNow,
                                                     claims: null, //identity.Claims,
                                                     expires: DateTime.Now.Add(TimeSpan.FromMinutes(Convert.ToInt32(1))),
                                                     signingCredentials: new SigningCredentials(GenerateSingingKey(), SecurityAlgorithms.HmacSha256));

            var encodedSecurityToken = new JwtSecurityTokenHandler().WriteToken(securityToken);

            return encodedSecurityToken;
        }
    }
}
