using System;
using DotnetCoreAngularStarter.BusinessLogic.Services.Abstract;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DotnetCoreAngularStarter.API.Initialization
{
    internal static class DatabaseInitializer
    {
        internal static void SeedDatabases(IServiceScope serviceScope)
        {
            using (serviceScope)
            {
                var serviceProvider = serviceScope.ServiceProvider;

                try
                {
                    var databaseSeederService = serviceProvider.GetService<IDatabaseSeederService>();
                    databaseSeederService.SeedDatabases();
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
