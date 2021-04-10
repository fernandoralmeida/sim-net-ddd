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
    public class AppServiceCanal : AppServiceBase<Canal>, IAppServiceCanal
    {
        private readonly IServiceCanal _canal;
        public AppServiceCanal(IServiceCanal canal)
            :base(canal)
        { _canal = canal; }
        public IEnumerable<Canal> GetByOwner(string setor)
        {
            return _canal.GetByOwner(setor);
        }
    }
}
