using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Application.SDE.Interface
{
    using Domain.SDE.Entity;
    using Application.Interface;
    public interface IAppServiceEmpregos : IAppServiceBase<Empregos>
    {
        Task<IEnumerable<Empregos>> GetAllEmpregosAsync();
        Task<IEnumerable<Empregos>> GetAllEmpregosAsync(string cnpj);
        Task<IEnumerable<Empregos>> GetByIdAsync(Guid id);
    }
}
