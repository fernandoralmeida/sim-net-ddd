using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Application
{
    using Interface;
    using Domain.Interface;
    public class AppServiceBase<TEntity> : IDisposable, IAppServiceBase<TEntity> where TEntity : class
    {
        private readonly IServiceBase<TEntity> _serviceBase;
        public AppServiceBase(IServiceBase<TEntity> serviceBase)
        {
            _serviceBase = serviceBase;
        }

        public void Add(TEntity obj)
        {
            _serviceBase.Add(obj);
        }

        public void AddRange(IEnumerable<TEntity> obj)
        {
            _serviceBase.AddRange(obj);
        }

        public void Dispose()
        {
            _serviceBase.Dispose();
            GC.SuppressFinalize(this);
        }

        public IEnumerable<TEntity> List()
        {
            return _serviceBase.List();
        }

        public TEntity GetById(Guid id)
        {
            return _serviceBase.GetById(id);
        }

        public void Remove(TEntity obj)
        {
            _serviceBase.Remove(obj);
        }

        public void RemoveRange(IEnumerable<TEntity> obj)
        {
            _serviceBase.RemoveRange(obj);
        }

        public void Update(TEntity obj)
        {
            _serviceBase.Update(obj);
        }

        public void UpdateRange(IEnumerable<TEntity> obj)
        {
            _serviceBase.UpdateRange(obj);
        }
    }
}
