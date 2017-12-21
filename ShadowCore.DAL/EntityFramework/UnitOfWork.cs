using System;
using System.Threading;
using System.Threading.Tasks;
using ShadowCore.DAL.EntityFramework.Abstract;
using ShadowCore.DAL.EntityFramework.Repository;
using ShadowCore.Common;
using ShadowCore.Models.EntityFramework.Abstract;
using ShadowBox.AutomaticDI;

namespace ShadowCore.DAL.EntityFramework
{
    [Feature(DependencyInjectionFeatureNames.EntityFramework)]
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IShadowCoreDbContext _db;

        public UnitOfWork(IShadowCoreDbContext db)
        {
            _db = db;
        }

        public IRepository<T> Repository<T>() where T : class
        {
            var repositoryType = typeof(BaseRepository<>);
            var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _db);
            return (BaseRepository<T>)repositoryInstance;
        }

        public void EnsureDatabaseSeeded()
        {
            _db.EnsureDatabaseSeeded();
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
