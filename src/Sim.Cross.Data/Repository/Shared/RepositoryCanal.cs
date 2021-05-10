using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Cross.Data.Repository.Shared
{
    using Sim.Domain.Shared.Entity;
    using Sim.Domain.Shared.Interface;
    using Context;
    public class RepositoryCanal : RepositoryBase<Canal>, IRepositoryCanal
    {
        public RepositoryCanal(ApplicationContext dbContext)
            :base(dbContext)
        {

        }

        public IEnumerable<Canal> GetByOwner(string setor)
        {
            return _db.Canal.Where(u => u.Setor.Nome.Contains(setor) || u.Setor.Nome.Contains("Geral"));
        }
    }
}
