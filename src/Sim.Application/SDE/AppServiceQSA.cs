using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Application.SDE
{
    using Interface;
    using Domain.SDE.Entity;
    using Domain.SDE.Interface;
    public class AppServiceQSA : AppServiceBase<QSA>, IAppServiceQSA
    {
        private readonly IServiceQSA _qsa;
        public AppServiceQSA(IServiceQSA qsa)
            :base(qsa)
        {
            _qsa = qsa;
        }

        public IEnumerable<QSA> GetByCPFSocio(string cpf)
        {
            return _qsa.GetByCPFSocio(cpf);
        }

        public IEnumerable<QSA> GetBySocio(string nome)
        {
            return _qsa.GetBySocio(nome);
        }
    }
}
