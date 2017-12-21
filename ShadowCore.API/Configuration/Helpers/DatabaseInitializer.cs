using System;
using ShadowCore.BusinessLogic.Services.Abstract;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ShadowCore.API.Configuration.Helpers
{
    /// <summary>
    /// Contains methods for database seeding
    /// </summary>
    internal static class DatabaseInitializer
    {
        /// <summary>
        /// Calls database seeding methods in services
        /// </summary>
        /// <param name="serviceScope">Retrieves service provider, which accesses registered services</param>
        internal static void SeedDatabases(IServiceScope serviceScope)
        {
            using (serviceScope)
            {
                var serviceProvider = serviceScope.ServiceProvider;

                try
                {
                    var databaseSeederService = serviceProvider.GetService<IDatabaseSeederService>();
                    databaseSeederService.EnsureDatabasesSeeded();
                }
                catch (Exception ex)
                {
                    var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred seeding the DB.");
                }
            }
        }
    }
}
