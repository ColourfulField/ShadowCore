using System.Threading;
using System.Threading.Tasks;
using ShadowBox.AutomaticDI.Interfaces;

namespace ShadowCore.DAL.EntityFramework.Abstract
{
    public interface IUnitOfWork : IScopedLifetime
    {
        IRepository<T> Repository<T>() where T : class;
        void EnsureDatabaseSeeded();
        int SaveChanges();
        int SaveChanges(bool acceptAllChangesOnSuccess);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken));
    }
}
