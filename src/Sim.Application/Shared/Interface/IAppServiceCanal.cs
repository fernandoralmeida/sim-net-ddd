using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Application.Shared.Interface
{
    using Domain.Shared.Entity;
    using Application.Interface;
    public interface IAppServiceCanal : IAppServiceBase<Canal>
    {
        IEnumerable<Canal> GetByOwner(string setor);
    }
}
