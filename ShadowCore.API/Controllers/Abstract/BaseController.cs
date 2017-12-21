using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShadowTools.Mapper.Abstract;

namespace ShadowCore.API.Controllers.Abstract
{
    /// <summary>
    /// Base class for API controllers. Contains API and default Route attributes along with [Authorize] attribute.
    /// Also contains common Controller functionality
    /// </summary>
    //[Authorize]
    [ApiVersion("1")]
    [Route("api/v1/[controller]")]
    public abstract class BaseController : Controller
    {
        protected readonly ILogger Logger;
        protected readonly IMapper Mapper;

        /// <summary>
        /// Base constructor, which initializes Logger and Mapper fields
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        protected BaseController(ILogger logger = null, IMapper mapper = null)
        {
            Logger = logger;
            Mapper = mapper;
        }

        /// <summary>
        /// Sets Response code to "No Content"
        /// </summary>
        protected void NoContentResponse()
        {
            Response.StatusCode = 204;
        }

        /// <summary>
        /// Abstraction for response formatting
        /// </summary>
        /// <typeparam name="T">Response Type</typeparam>
        /// <param name="response">Response object</param>
        /// <returns></returns>
        protected IActionResult FormattedResponse<T>(T response)
        {
            return Json(response);
        }
    }
}
