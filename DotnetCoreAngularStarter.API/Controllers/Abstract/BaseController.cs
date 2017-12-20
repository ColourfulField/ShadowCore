using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShadowBox.Mapper.Abstract;

namespace DotnetCoreAngularStarter.API.Controllers.Abstract
{
    //[Authorize]
    public abstract class BaseController : Controller
    {
        protected readonly ILogger _logger;
        protected readonly IMapper _mapper;

        protected BaseController(ILogger logger = null, IMapper mapper = null)
        {
            _logger = logger;
            _mapper = mapper;
        }

        protected void NoContentResponse()
        {
            Response.StatusCode = 204;
        }

        protected IActionResult FormattedResponse(object response)
        {
            return Json(response);
        }
    }
}
