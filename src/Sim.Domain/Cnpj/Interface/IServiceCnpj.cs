using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Domain.Cnpj.Interface
{
    public interface IServiceCnpj<TEntity> where TEntity : class
    {

        Task<IEnumerable<TEntity>> EmpresasNulas(IEnumerable<TEntity> obj);
        Task<IEnumerable<TEntity>> EmpresasAtivas(IEnumerable<TEntity> obj);
        Task<IEnumerable<TEntity>> EmpresasSuspensas(IEnumerable<TEntity> obj);
        Task<IEnumerable<TEntity>> EmpresasInaptas(IEnumerable<TEntity> obj);
        Task<IEnumerable<TEntity>> EmpresasBaixadas(IEnumerable<TEntity> obj);
    }
}
