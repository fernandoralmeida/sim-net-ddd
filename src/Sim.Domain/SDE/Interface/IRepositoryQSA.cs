using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Domain.SDE.Interface
{
    using Entity;
    using Domain.Interface;
    public interface IRepositoryQSA : IRepositoryBase<QSA>
    {
        IEnumerable<QSA> GetBySocio(string nome);

        IEnumerable<QSA> GetByCPFSocio(string cpf);
    }
}
