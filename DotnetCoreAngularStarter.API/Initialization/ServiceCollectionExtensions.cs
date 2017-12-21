using System.Collections.Generic;
using DotnetCoreAngularStarter.Common.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Localization;
using ShadowBox.Mapper;
using ShadowBox.Mapper.Exceptions;
using ShadowBox.Utilities.Localization;

namespace DotnetCoreAngularStarter.API.Initialization
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Register all option objects as IOptions<T>
        /// </summary>
        /// <param name="services">Service collection object to configure</param>
        /// <param name="configuration">Configuration object which contains serialized options</param>
        public static void ConfigureAppOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DatabaseOptions>(configuration.GetSection("DatabaseOptions"));
        }

        /// <summary>
        /// Configure ShadowBox mapper
        /// </summary>
        /// <param name="services">Service collection object to configure</param>
        /// <param name="maxDepth">Max depth for automapping</param>
        public static void ConfigureMapper(this IServiceCollection services, int maxDepth = 0)
        {
            if (maxDepth < 0)
            {
                throw new DepthException("maxDepth must be higher than zero.");
            }
            AutoMapService.DefaultMaxDepth = maxDepth;
        }

        /// <summary>
        /// Configures localizer factory options and adds factory it as a singleton to service collection
        /// </summary>
        /// <param name="services">Service collection object to configure</param>
        public static void ConfigureLocalization(this IServiceCollection services)
        {
            services.Configure<ClassLibraryLocalizationOptions>(options =>
                {
                    options.ResourcePaths = new Dictionary<string, string>
                                            {
                                                {
                                                    "DotnetCoreAngularStarter.BusinessLogic",
                                                    "Localization"
                                                },
                                                {
                                                    "DotnetCoreAngularStarter.API",
                                                    "Localization"
                                                }
                                            };
                    options.UseSeparateFolderForEachResource = true;
                });
            services.TryAddSingleton(typeof(IStringLocalizerFactory), typeof(ClassLibraryStringLocalizerFactory));
            services.AddLocalization();
        }
    }
}
