using System;
using DotnetCoreAngularStarter.Common.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using DotnetCoreAngularStarter.Models.EntityFramework.Abstract;
using DotnetCoreAngularStarter.Models.EntityFramework.Domain;
using DotnetCoreAngularStarter.Models.EntityFramework.Extensions;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DotnetCoreAngularStarter.Models.EntityFramework
{
    public partial class DotnetCoreAngularStarterDbContext: DbContext, IDotnetCoreAngularStarterDbContext
    {
        #region Constructor, configuration and seeding

        private readonly DatabaseOptions _databaseOptions;
        public DotnetCoreAngularStarterDbContext(IOptions<DatabaseOptions>  databaseOptions)
        {
            _databaseOptions = databaseOptions.Value;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_databaseOptions.SqlConnectionString);
        }

        public void SeedDatabase()
        {
            if (this.AllMigrationsApplied())
            {
                this.Seed();
            }
        }

        #endregion

        public DbSet<Note> Notes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure Code First to ignore Pluralizing Table Name convention 
            // If you keep this convention then the generated tables will have pluralized names. 
            modelBuilder.RemovePluralizingTableNameConvention();
        }
    }

    public class DotnetCoreAngularStarterDbContextFactory : IDesignTimeDbContextFactory<DotnetCoreAngularStarterDbContext>
    {
        public DotnetCoreAngularStarterDbContext CreateDbContext(string[] options)
        {
            //Get the project base directory (AppContext.BaseDirectory return assembly folder, i.e. "bin/Debug/netcoreapp...")
            string basePath = AppContext.BaseDirectory.Substring(0, AppContext.BaseDirectory.LastIndexOf("bin", StringComparison.InvariantCulture));
            string envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{envName}.json", true)
                .Build();

            var connectionString = configuration.GetSection("DatabaseOptions:SqlConnectionString").Value;
            IOptions<DatabaseOptions> dbOptions = new OptionsWrapper<DatabaseOptions>(new DatabaseOptions { SqlConnectionString = connectionString });

            var builder = new DbContextOptionsBuilder<DotnetCoreAngularStarterDbContext>();
            builder.UseSqlServer(connectionString);
            return new DotnetCoreAngularStarterDbContext(dbOptions);
        }
    }
}

