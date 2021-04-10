using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Domain.Shared.Service
{
    using Entity;
    using Domain.Service;
    using Interface;
    public class ServicePlaner : ServiceBase<Planer>, IServicePlaner
    {
        private readonly IRepositoryPlaner _planer;
        public ServicePlaner(IRepositoryPlaner repositoryPlaner)
            :base(repositoryPlaner)
        { _planer = repositoryPlaner; }
        public IEnumerable<Planer> GetByData(DateTime? data)
        {
            return _planer.GetByData(data);
        }
    }
}
