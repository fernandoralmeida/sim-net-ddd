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
    public class ServiceCanal : ServiceBase<Canal>, IServiceCanal
    {
        private readonly IRepositoryCanal _canal;
        public ServiceCanal(IRepositoryCanal repositoryCanal)
            :base(repositoryCanal)
        { _canal = repositoryCanal; }
        public IEnumerable<Canal> GetByOwner(string setor)
        {
            return _canal.GetByOwner(setor);
        }
    }
}
