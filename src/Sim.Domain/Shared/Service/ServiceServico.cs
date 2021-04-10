using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Domain.Shared.Service
{
    using Entity;
    using Domain.Service;
    using Interface;
    public class ServiceServico : ServiceBase<Servico>, IServiceServico
    {
        private readonly IRepositoryServico _servico;
        public ServiceServico(IRepositoryServico repositoryServico)
            :base(repositoryServico)
        {
            _servico = repositoryServico;
        }

        public IEnumerable<Servico> GetByOwner(string setor)
        {
            return _servico.GetByOwner(setor);
        }
    }
}
