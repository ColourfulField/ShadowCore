using System.Collections.Generic;
using System.Threading.Tasks;
using ShadowCore.API.Controllers.Abstract;
using ShadowCore.BusinessLogic.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShadowCore.Common.Models.Authentication;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShadowCore.API.Controllers
{
    [AllowAnonymous]
    [Route("token")]
    public class AuthorizationController : BaseController
    {
        private readonly IBearerTokenService _tokenService;

        public AuthorizationController(IBearerTokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost]
        public async Task<AuthToken> GenerateToken()
        {
            return await _tokenService.GenerateToken();
        }

        [HttpPost("refresh-token")]
        public async Task<AuthToken> RenewToken([FromBody]string refreshToken)
        {
            return await _tokenService.RenewToken(refreshToken);
        }

        [HttpPost("refresh-both-tokens")]
        public async Task<AuthorizationTokens> RenewTokens([FromBody]string refreshToken)
        {
            return await _tokenService.RenewTokens(refreshToken);
        }
    }
}
