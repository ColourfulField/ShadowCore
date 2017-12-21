using System;
using System.Threading;
using System.Threading.Tasks;
using DotnetCoreAngularStarter.DAL.EntityFramework.Abstract;
using DotnetCoreAngularStarter.DAL.EntityFramework.Repository;
using DotnetCoreAngularStarter.Common;
using DotnetCoreAngularStarter.Models.EntityFramework.Abstract;
using ShadowBox.AutomaticDI;

namespace DotnetCoreAngularStarter.DAL.EntityFramework
{
    [Feature(DependencyInjectionFeatureNames.EntityFramework)]
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDotnetCoreAngularStarterDbContext _db;

        public UnitOfWork(IDotnetCoreAngularStarterDbContext db)
        {
            _db = db;
        }

        public IRepository<T> Repository<T>() where T : class
        {
            var repositoryType = typeof(BaseRepository<>);
            var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _db);
            return (BaseRepository<T>)repositoryInstance;
        }

        public void SeedDatabase()
        {
            _db.SeedDatabase();
        }

        public int SaveChanges()
        {
            return _db.SaveChanges();
        }

        public int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            return _db.SaveChanges(acceptAllChangesOnSuccess);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _db.SaveChangesAsync(cancellationToken);
        }
        public async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _db.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}
