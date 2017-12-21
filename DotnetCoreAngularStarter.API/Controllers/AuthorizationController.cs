using DotnetCoreAngularStarter.API.Controllers.Abstract;
using DotnetCoreAngularStarter.BusinessLogic.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DotnetCoreAngularStarter.API.Controllers
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
        public string CreateToken()
        {
            return _tokenService.GenerateToken();
        }
    }
}
