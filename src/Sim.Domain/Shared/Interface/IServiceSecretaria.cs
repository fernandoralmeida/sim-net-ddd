using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Domain.Shared.Interface
{
    using Entity;
    using Domain.Interface;
    public interface IServiceSecretaria : IServiceBase<Secretaria>
    {
        IEnumerable<Secretaria> GetByOwner(string setor);
    }
}
