using System.Diagnostics.CodeAnalysis;
using DotnetCoreAngularStarter.API.Controllers.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DotnetCoreAngularStarter.API.Controllers.V0
{
    /// <summary>
    /// controller for old API version. Used only for API versioning demo, feel free to remove it.
    /// </summary>
    public class ValuesController : BaseControllerV0
    {
        [SuppressMessage("", "CS1591:MissingXmlDocumentation")]
        public ValuesController(ILogger<ValuesController> logger) : base(logger)
        {
        }

        /// <summary>
        /// Old Api method example for Swagger
        /// </summary>
        /// <returns>"Old API" string</returns>
        [HttpGet]
        public string Get()
        {
            return "Old API";
        }
    }
}