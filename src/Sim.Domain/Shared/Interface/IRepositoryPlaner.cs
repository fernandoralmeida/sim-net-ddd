using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Domain.Shared.Interface
{
    using Entity;
    using Domain.Interface;
    public interface IRepositoryPlaner : IRepositoryBase<Planner>
    {
        IEnumerable<Planner> GetByData(DateTime? data);

        Task<IEnumerable<Planner>> GetMyPlanner(DateTime? datai, DateTime? dataf, string username);
    }
}
