using System.Threading.Tasks;
using ShadowCore.API.Controllers.Abstract;
using ShadowCore.BusinessLogic.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShadowCore.Common.Models.Authentication;
using ShadowCore.Models.DTO;
using ShadowCore.Models.VM.User;
using ShadowTools.Mapper.Abstract;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShadowCore.API.Controllers
{
    public class AuthorizationController : BaseController
    {
        private readonly IBearerTokenService _tokenService;
        private readonly IUserService _userService;

        public AuthorizationController(IBearerTokenService tokenService, IUserService userService, IMapper mapper) : base(mapper: mapper)
        {
            _tokenService = tokenService;
            _userService = userService;
        }

        /// <summary>
        /// Creates access token for provided user
        /// </summary>
        /// <param name="userLoginModel">User credentials</param>
        /// <returns>access token</returns>
        [AllowAnonymous]
        [HttpPost("token")]
        public async Task<IActionResult> GenerateToken([FromBody] UserLoginVM userLoginModel)
        {
            var userDto = await Mapper.Map<UserLoginVM, UserDTO>(userLoginModel);
            if (await _userService.CheckPassword(userDto))
            {
                return FormattedResponse(await _tokenService.GenerateAccessToken(userDto));
            }

            return Unauthorized();
        }

        /// <summary>
        /// Creates acess and refresh tokens for provided user
        /// </summary>
        /// <param name="userLoginModel">User credentials</param>
        /// <returns>Access and refresh tokens</returns>
        [AllowAnonymous]
        [HttpPost("tokens")]
        public async Task<IActionResult> GenerateTokens([FromBody] UserLoginVM userLoginModel)
        {
            var userDto = await Mapper.Map<UserLoginVM, UserDTO>(userLoginModel);
            if (await _userService.CheckPassword(userDto))
            {
                return FormattedResponse(await _tokenService.GenerateTokens(userDto));
            }

            return Unauthorized();
        }

        /// <summary>
        /// Returns new access token, using previously received refresh token
        /// </summary>
        /// <param name="refreshToken">Refresh token</param>
        /// <returns>Access token</returns>
        [HttpPost("renew-token")]
        public async Task<AuthToken> RenewToken([FromBody]string refreshToken)
        {
            return await _tokenService.RenewAccessToken(refreshToken);
        }

        /// <summary>
        /// Returns new access and refresh tokens, using previously received refresh token
        /// </summary>
        /// <param name="refreshToken">Refresh token</param>
        /// <returns>Access and refresh tokens</returns>
        [HttpPost("renew-tokens")]
        public async Task<AuthorizationTokens> RenewTokens([FromBody]string refreshToken)
        {
            return await _tokenService.RenewTokens(refreshToken);
        }
    }
}
