using System;
using DotnetCoreAngularStarter.Common.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using DotnetCoreAngularStarter.Models.EntityFramework.Abstract;
using DotnetCoreAngularStarter.Models.EntityFramework.Domain;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DotnetCoreAngularStarter.Models.EntityFramework
{
    public partial class DotnetCoreAngularStarterDbContext: DbContext, IDotnetCoreAngularStarterDbContext
    {
        #region Constructor and configuration

        private readonly DatabaseOptions _databaseOptions;
        public DotnetCoreAngularStarterDbContext(IOptions<DatabaseOptions>  databaseOptions)
        {
            _databaseOptions = databaseOptions.Value;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_databaseOptions.ConnectionString);
        }

        #endregion

        public DbSet<Note> Notes { get; set; }

    }

    public class DotnetCoreAngularStarterDbContextFactory : IDesignTimeDbContextFactory<DotnetCoreAngularStarterDbContext>
    {
        public DotnetCoreAngularStarterDbContext CreateDbContext(string[] options)
        {

            var builder = new DbContextOptionsBuilder<DotnetCoreAngularStarterDbContext>();
            builder.UseSqlServer(_databaseOptinos.Value.ConnectionString);
            return new DotnetCoreAngularStarterDbContext(_databaseOptinos);
        }
    }
}

