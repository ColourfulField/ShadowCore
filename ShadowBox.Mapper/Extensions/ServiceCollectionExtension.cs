using Microsoft.Extensions.DependencyInjection;
using ShadowBox.Mapper.Exceptions;

namespace ShadowBox.Mapper.Extensions
{
    public static class ServiceCollectionExtension
    {
        /// <summary>
        /// Configure ShadowBox mapper
        /// </summary> 
        /// <param name="maxDepth">Max depth for automapping</param>
        public static void ConfigureMapper(this IServiceCollection services, int maxDepth)
        {
            if (maxDepth < 0)
            {
                throw new DepthException("maxDepth must be higher than zero.");
            }
            AutoMapService.DefaultMaxDepth = maxDepth;
        }
    }
}
