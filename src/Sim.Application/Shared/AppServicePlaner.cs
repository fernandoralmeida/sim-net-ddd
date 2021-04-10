using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Application.Shared.Service
{
    using Domain.Shared.Entity;
    using Domain.Shared.Interface;
    using Interface;
    public class AppServicePlaner : AppServiceBase<Planer>, IAppServicePlaner
    {
        private readonly IServicePlaner _planer;
        public AppServicePlaner(IServicePlaner planer)
            :base(planer)
        { _planer = planer; }
        public IEnumerable<Planer> GetByData(DateTime? data)
        {
            return _planer.GetByData(data);
        }
    }
}
