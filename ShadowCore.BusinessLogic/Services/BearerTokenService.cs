using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ShadowCore.BusinessLogic.Services.Abstract;
using ShadowCore.Common.Models.Authentication;
using ShadowCore.Common.Options;
using ShadowCore.DAL.EntityFramework.Abstract;
using ShadowCore.DAL.EntityFramework.Abstract.Identity;
using ShadowCore.Models.DTO;
using ShadowCore.Models.EntityFramework.Domain;
using ShadowTools.Utilities.Exceptions;
using ShadowTools.Utilities.Helpers;

namespace ShadowCore.BusinessLogic.Services
{
    /// <summary>
    /// Manages access and refresh tokens for bearer OAuth authorization
    /// </summary>
    public class BearerTokenService : BaseService, IBearerTokenService
    {
        private readonly BearerAuthenticationOptions _authenticationOptions;
        private readonly IUserManager _userManager;

        public BearerTokenService(
            IOptions<AuthenticationOptions> authenticationOptionsAccessor,
            IUserManager userManager,
            IUnitOfWork unitOfWork
            ) : base(unitOfWork)
        {
            _authenticationOptions = authenticationOptionsAccessor.Value.Bearer;
            _userManager = userManager;
        }

        /// <summary>
        /// Creates signing key for access tokens.
        /// </summary>
        /// <param name="secret">secret password which is used to encode signing key</param>
        /// <returns>Security key, which is used to sign tokens</returns>
        public SecurityKey GenerateSingingKey(string secret = null)
        {
            secret = secret ?? _authenticationOptions.Secret;
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));
        }

        /// <summary>
        /// Generates access token with specific user claims
        /// </summary>
        /// <param name="userDto">DTO model for user whose claims are added to a token</param>
        /// <returns>Access token</returns>
        public async Task<AuthToken> GenerateAccessToken(UserDTO userDto)
        {
            var user = await _userManager.FindByEmailAsync(userDto.Email);
            return await GenerateAccessToken(user);
        }

        /// <summary>
        /// Generates both access and refresh tokens
        /// </summary>
        /// <param name="userDto">DTO model for user whose claims are added to a token</param>
        /// <returns>Access and refresh tokens</returns>
        public async Task<AuthorizationTokens> GenerateTokens(UserDTO userDto)
        {
            var user = await _userManager.FindByEmailAsync(userDto.Email);

            var accessToken = await GenerateAccessToken(user);
            var refreshToken = await GenerateRefreshToken(user);

            return new AuthorizationTokens {AccessToken = accessToken, RefreshToken = refreshToken};
        }

        /// <summary>
        /// Uses old access token to issue a new one
        /// </summary>
        /// <param name="accessToken">Old access token</param>
        /// <returns>New access token</returns>
        public AuthToken ProlongAccessToken(AuthToken accessToken)
        {
            var oldToken = new JwtSecurityTokenHandler().ReadJwtToken(accessToken.Token);
            return GenerateAccessToken(oldToken);
        }

        /// <summary>
        /// Uses refresh token to issue new access token
        /// </summary>
        /// <param name="refreshTokenId">Refresh token, which was issued during original sign in</param>
        /// <returns>New access token</returns>
        public async Task<AuthToken> RenewAccessToken(string refreshTokenId)
        {
            var refreshToken = await GetRefreshToken(refreshTokenId);

            if (!ValidateRefreshToken(refreshToken))
            {
                throw new TokenValidationException("Refresh token is invalid.");
            }

            return await GenerateAccessToken(new User());
        }

        /// <summary>
        /// Uses refresh token to issue new access and refresh tokens
        /// </summary>
        /// <param name="refreshTokenId">Refresh token, which was issued during original sign</param>
        /// <returns>New access and refresh tokens</returns>
        public async Task<AuthorizationTokens> RenewTokens(string refreshTokenId)
        {
            var refreshToken = await GetRefreshToken(refreshTokenId);

            if (!ValidateRefreshToken(refreshToken))
            {
                throw new TokenValidationException("Refresh token is invalid."); 
            }
            
            var newRefreshToken = await GenerateRefreshToken(refreshToken.User);
            var accessToken = await GenerateAccessToken(refreshToken.User);
            return new AuthorizationTokens { AccessToken = accessToken, RefreshToken = newRefreshToken};
        }

        /// <summary>
        /// Generates access token with specific user claims
        /// </summary>
        /// <param name="user">User whose claims are added to a token</param>
        /// <returns>Access token</returns>
        private async Task<AuthToken> GenerateAccessToken(User user)
        {
            //TODO user claims from real DB
            //var userClaims = GetUserClaims(user, _tokenOptions.Issuer, _tokenOptions.Audience);
            //userClaims.AddRange(await _userManager.GetClaimsAsync(user).ConfigureAwait(false));
            //var identity = new ClaimsIdentity(new GenericIdentity(user.Email), userClaims);

            var securityToken = new JwtSecurityToken(_authenticationOptions.Issuer,
                                                     _authenticationOptions.Audience,
                                                     notBefore: DateTime.UtcNow,
                                                     claims: null, //claims,
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

        /// <summary>
        /// Generates access token with new lifetime based on information from old access token
        /// </summary>
        /// <param name="oldToken">Old (but still valid) access token</param>
        /// <returns>New access token</returns>
        private AuthToken GenerateAccessToken(JwtSecurityToken oldToken)
        {
            var securityToken = new JwtSecurityToken(oldToken.Issuer,
                                                     oldToken.Audiences.FirstOrDefault(),
                                                     notBefore: DateTime.UtcNow,
                                                     claims: oldToken.Claims,
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

        /// <summary>
        /// Generates refresh token and persists it to database
        /// </summary>
        /// <param name="user">User for whom a token is generated</param>
        /// <returns>Refresh token</returns>
        private async Task<AuthToken> GenerateRefreshToken(User user)
        {
            //TODO think about hashing and persistence security
            var refreshToken = new RefreshToken
                               {
                                   Id = CryptographicHelpers.GetHash(Guid.NewGuid().ToString()),
                                   ClientApp = "",
                                   ValidFrom = DateTime.UtcNow,
                                   ValidTo = DateTime.UtcNow.Add(TimeSpan.FromMinutes(_authenticationOptions.RefreshTokenLifetime)),
                                   UserId = user.Id
                               };

            var refreshTokenRepository = UnitOfWork.Repository<RefreshToken>();

            refreshTokenRepository.RemoveRange(refreshTokenRepository.GetAll().Where(x => x.UserId == user.Id));
            refreshTokenRepository.Add(refreshToken);
            await UnitOfWork.SaveChangesAsync();

            return new AuthToken
                   {
                       Issuer = _authenticationOptions.Issuer,
                       ValidTo = refreshToken.ValidTo,
                       Token = refreshToken.Id
                   };
        }

        /// <summary>
        /// Retrieves RefreshToken entity from database for given refreshTokenId
        /// </summary>
        /// <param name="refreshTokenId">Hashed refresh token Id</param>
        /// <returns>RefreshToken entity</returns>
        private async Task<RefreshToken> GetRefreshToken(string refreshTokenId)
        {
            return await UnitOfWork.Repository<RefreshToken>()
                                   .GetAll()
                                   .Include(x => x.User)
                                   .FirstOrDefaultAsync(x => x.Id == refreshTokenId);
        }

        /// <summary>
        /// Checks for refresh token validity
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        private bool ValidateRefreshToken(RefreshToken refreshToken)
        {
            return refreshToken != null && refreshToken.ValidTo > DateTime.UtcNow;
        }
    }
}
