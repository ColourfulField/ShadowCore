using Microsoft.EntityFrameworkCore;
using ShadowCore.Models.EntityFramework.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShadowCore.DAL.EntityFramework.Abstract;
using ShadowCore.Common;
using ShadowBox.AutomaticDI;

namespace ShadowCore.DAL.EntityFramework.Repository
{
    [Feature(DependencyInjectionFeatureNames.EntityFramework)]
    public class BaseRepository<T> : IRepository<T>
              where T : class
    {
        protected readonly IShadowCoreDbContext _db;

        public BaseRepository(IShadowCoreDbContext context)
        {
            _db = context;
        }

        public IQueryable<T> GetAll()
        {
            return _db.Set<T>().AsQueryable();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _db.Set<T>().FindAsync(id);
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _db.Set<T>().FindAsync(id);
        }

        public void Add(T entity)
        {
            _db.Set<T>().Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _db.Set<T>().AddRange(entities);
        }

        public void Update(T entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
        }

        public void UpdateRange(IEnumerable<T> entities)
        {
            _db.Set<T>().UpdateRange(entities);
        }

        public void Remove(T entity)
        {
            _db.Set<T>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _db.Set<T>().RemoveRange(entities);
        }

        public void Attach(T entity)
        {
            _db.Set<T>().Attach(entity);
        }

        public void AttachRange(IEnumerable<T> entities)
        {
            _db.Set<T>().AttachRange(entities);
        }
    }
}
