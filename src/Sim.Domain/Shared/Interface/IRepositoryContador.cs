using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Sim.Domain.Shared.Interface
{
    using Sim.Domain.Interface;
    using Sim.Domain.Shared.Entity;
    public interface IRepositoryContador : IRepositoryBase<Contador>
    {
        Task<string> GetProtocoloAsync(string appuserid, string moduloname);
    }
}
