using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Application.SDE.Interface
{
    using Domain.SDE.Entity;
    using Application.Interface;
   public interface IAppServiceQSA : IAppServiceBase<QSA>
    {
        IEnumerable<QSA> GetBySocio(string nome);
    }
}
