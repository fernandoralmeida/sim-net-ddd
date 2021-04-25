using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Domain.Interface
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        void Add(TEntity obj);
        void AddRange(IEnumerable<TEntity> obj);
        void Update(TEntity obj);
        void UpdateRange(IEnumerable<TEntity> obj);
        void Remove(TEntity obj);
        void RemoveRange(IEnumerable<TEntity> obj);
        TEntity GetById(Guid id);
        IEnumerable<TEntity> List();
        void Dispose();
    }
}
