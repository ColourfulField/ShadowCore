using System;
using System.Threading;
using System.Threading.Tasks;
using DotnetCoreAngularStarter.DAL.Abstract;
using DotnetCoreAngularStarter.Models.EntityFramework.Abstract;

namespace DotnetCoreAngularStarter.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDotnetCoreAngularStarterDbContext _db;

        public UnitOfWork(IDotnetCoreAngularStarterDbContext db)
        {
            _db = db;
        }

        public IRepository<T> Repository<T>() where T : class
        {
            var repositoryType = typeof(RepositoryEntityFramework<>);
            var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _db);
            return (RepositoryEntityFramework<T>)repositoryInstance;
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
