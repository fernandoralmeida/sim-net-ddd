using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Domain.SDE.Interface
{
    using Entity;
    using Domain.Interface;
    public interface IServiceEmpregos : IServiceBase<Empregos>
    {
        Task<IEnumerable<Empregos>> GetAllEmpregosAsync();
        Task<IEnumerable<Empregos>> GetAllEmpregosAsync(string cnpj);
    }
}
