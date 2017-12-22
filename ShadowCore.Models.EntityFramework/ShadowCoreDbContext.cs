using System;
using ShadowCore.Common.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ShadowCore.Models.EntityFramework.Abstract;
using ShadowCore.Models.EntityFramework.Domain;
using ShadowCore.Models.EntityFramework.Extensions;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ShadowCore.Models.EntityFramework
{
    public partial class ShadowCoreDbContext: DbContext, IShadowCoreDbContext
    {
        #region Constructor, configuration and seeding

        private readonly DatabaseOptions _databaseOptions;
        public ShadowCoreDbContext(IOptions<DatabaseOptions>  databaseOptionsAccessor)
        {
            _databaseOptions = databaseOptionsAccessor.Value;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_databaseOptions.SqlConnectionString);
        }

        public void EnsureDatabaseSeeded()
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
            if (_databaseOptions.PluralizeColumnNames)
            {
                modelBuilder.UseTableNamePluralization();
            }
            if (_databaseOptions.LogTableGeneration.GenerateLogTables)
            {
                modelBuilder.GenerateLogTables();
            }

            modelBuilder.Entity<Note>()
                        .Property(t => t.NoteId);
        }
    }

    public class ShadowCoreDbContextFactory : IDesignTimeDbContextFactory<ShadowCoreDbContext>
    {
        public ShadowCoreDbContext CreateDbContext(string[] options)
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
            IOptions<DatabaseOptions> databaseOptionsAccessor = new OptionsWrapper<DatabaseOptions>(new DatabaseOptions { SqlConnectionString = connectionString });

            var builder = new DbContextOptionsBuilder<ShadowCoreDbContext>();
            builder.UseSqlServer(connectionString);
            return new ShadowCoreDbContext(databaseOptionsAccessor);
        }
    }
}

