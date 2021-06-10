using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Domain.Cnpj.Interface
{
    public interface IBase<TEntity> where TEntity : class
    {
        void Dispose();
        Task<IEnumerable<TEntity>> ListAll();
    }
}
