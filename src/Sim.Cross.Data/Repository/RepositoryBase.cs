using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Sim.Cross.Data.Repository
{
    using Context;
    using Sim.Domain.Interface;
    public class RepositoryBase<TEntity> : IDisposable, IRepositoryBase<TEntity> where TEntity : class
    {
        protected readonly ApplicationContext _db;

        public RepositoryBase(ApplicationContext dbcontext)
        {
            _db = dbcontext;
        }

        public void Add(TEntity obj)
        {
            _db.Set<TEntity>().Add(obj);
            _db.SaveChanges();
        }

        public void AddRange(IEnumerable<TEntity> obj)
        {
            _db.Set<TEntity>().AddRange(obj);
            _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public IEnumerable<TEntity> List()
        {
            return _db.Set<TEntity>().ToList();
        }

        public TEntity GetById(Guid id)
        {
            return _db.Set<TEntity>().Find(id);
        }

        public void Remove(TEntity obj)
        {
            _db.Set<TEntity>().Remove(obj);
            _db.SaveChanges();
        }

        public void RemoveRange(IEnumerable<TEntity> obj)
        {
            _db.Set<TEntity>().RemoveRange(obj);
            _db.SaveChanges();
        }

        public void Update(TEntity obj)
        {
            _db.Entry(obj).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void UpdateRange(IEnumerable<TEntity> obj)
        {
            _db.Entry(obj).State = EntityState.Modified;
            _db.SaveChanges();
        }
    }
}
