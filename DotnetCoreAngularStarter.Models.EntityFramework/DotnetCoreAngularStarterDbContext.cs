using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using DotnetCoreAngularStarter.Infrastructure.Options;
using DotnetCoreAngularStarter.Models.EntityFramework.Abstract;
using DotnetCoreAngularStarter.Models.EntityFramework.Domain;

namespace DotnetCoreAngularStarter.Models.EntityFramework
{
    public partial class DotnetCoreAngularStarterDbContext: DbContext, IDotnetCoreAngularStarterDbContext
    {
        #region Constructor and configuration

        private readonly DatabaseOptions _databaseOptinos;
        public DotnetCoreAngularStarterDbContext(IOptions<DatabaseOptions>  databaseOptions)
        {
            _databaseOptinos = databaseOptions.Value;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_databaseOptinos.ConnectionString);
        }

        #endregion

        public DbSet<Note> Notes { get; set; }

    }
}

