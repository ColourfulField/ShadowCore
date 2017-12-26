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

        private readonly SqlOptions _sqlOptions;
        public ShadowCoreDbContext(IOptions<SqlOptions>  databaseOptionsAccessor)
        {
            _sqlOptions = databaseOptionsAccessor.Value;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_sqlOptions.SqlConnectionString);
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
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (_sqlOptions.PluralizeColumnNames)
            {
                modelBuilder.UseTableNamePluralization();
            }
            if (_sqlOptions.LogTableGeneration.GenerateLogTables)
            {
                modelBuilder.GenerateLogTables();
            }

            modelBuilder.Entity<Note>()
                        .HasOne(x => x.ParentNote)
                        .WithMany(x => x.ChildNotes)
                        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RefreshToken>()
                        .HasOne(x => x.User);
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

            var sqlOptions = configuration.GetSection("DatabaseOptions:SqlOptions").Get<SqlOptions>();
            IOptions<SqlOptions> databaseOptionsAccessor = new OptionsWrapper<SqlOptions>(sqlOptions);

            var builder = new DbContextOptionsBuilder<ShadowCoreDbContext>();
            builder.UseSqlServer(sqlOptions.SqlConnectionString);
            return new ShadowCoreDbContext(databaseOptionsAccessor);
        }
    }
}

