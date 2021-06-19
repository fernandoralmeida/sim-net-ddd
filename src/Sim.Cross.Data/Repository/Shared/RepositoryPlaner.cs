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
    public class RepositoryPlaner : RepositoryBase<Planner>, IRepositoryPlaner
    {
        public RepositoryPlaner(ApplicationContext dbContext)
            :base(dbContext)
        {

        }

        public IEnumerable<Planner> GetByData(DateTime? data)
        {
            return _db.Planner.Where(u => u.Data == data);
        }
    }
}
