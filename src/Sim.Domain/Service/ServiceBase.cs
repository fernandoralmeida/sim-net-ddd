using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Domain.Service
{
    using Interface;
    public class ServiceBase<TEntity> : IDisposable, IServiceBase<TEntity> where TEntity : class
    {
        private readonly IRepositoryBase<TEntity> _repositoryBase;

        public ServiceBase(IRepositoryBase<TEntity> repositoryBase)
        {
            _repositoryBase = repositoryBase;
        }

        public void Add(TEntity obj)
        {
            _repositoryBase.Add(obj);
        }

        public void AddRange(IEnumerable<TEntity> obj)
        {
            _repositoryBase.AddRange(obj);
        }

        public void Dispose()
        {
            _repositoryBase.Dispose();
            GC.SuppressFinalize(this);
        }

        public IEnumerable<TEntity> List()
        {
            return  _repositoryBase.List();
        }

        public TEntity GetById(Guid id)
        {
            return _repositoryBase.GetById(id);
        }

        public void Remove(TEntity obj)
        {
            _repositoryBase.Remove(obj);
        }

        public void RemoveRange(IEnumerable<TEntity> obj)
        {
            _repositoryBase.RemoveRange(obj);
        }

        public void Update(TEntity obj)
        {
            _repositoryBase.Update(obj);
        }

        public void UpdateRange(IEnumerable<TEntity> obj)
        {
            _repositoryBase.UpdateRange(obj);
        }
    }
}
