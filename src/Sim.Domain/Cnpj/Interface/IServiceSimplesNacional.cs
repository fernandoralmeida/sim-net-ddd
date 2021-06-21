using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Domain.Cnpj.Interface
{
    public interface IServiceSimplesNacional<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> OptanteSimplesNacional (IEnumerable<TEntity> obj);
        Task<IEnumerable<TEntity>> OptanteMEI(IEnumerable<TEntity> obj);
        Task<IEnumerable<TEntity>> OptanteSimplesNacionalNaoMEI(IEnumerable<TEntity> obj);
        Task<IEnumerable<TEntity>> ExclusaoSimplesNacional(IEnumerable<TEntity> obj);
        Task<IEnumerable<TEntity>> ExclusaoSimplesNacionalMEI(IEnumerable<TEntity> obj);
    }
}
