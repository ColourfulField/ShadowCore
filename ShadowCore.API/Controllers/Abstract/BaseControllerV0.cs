using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ShadowCore.API.Controllers.Abstract
{
    /// <summary>
    /// Base class for API controllers. Used only for API versioning demo, feel free to remove it.
    /// </summary>
    [ApiVersion("0")]
    [Route("api/v0/[controller]")]
    public abstract class BaseControllerV0 : Controller
    {
        protected readonly ILogger Logger;


        [SuppressMessage("", "CS1591:MissingXmlDocumentation")]
        protected BaseControllerV0(ILogger logger = null)
        {
            Logger = logger;
        }

    }
}
