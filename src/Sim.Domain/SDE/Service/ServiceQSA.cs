using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Domain.SDE.Service
{
    using Entity;
    using Interface;
    using Domain.Service;
    public class ServiceQSA : ServiceBase<QSA>, IServiceQSA
    {
        private readonly IRepositoryQSA _qsa;
        public ServiceQSA(IRepositoryQSA repositoryQSA)
            :base(repositoryQSA)
        {
            _qsa = repositoryQSA;
        }


        public IEnumerable<QSA> GetBySocio(string nome)
        {
            return _qsa.GetBySocio(nome);
        }
    }
}
