using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Application.Shared.Service
{
    using Domain.Shared.Entity;
    using Domain.Shared.Interface;
    using Interface;
    public class AppServiceServico : AppServiceBase<Servico>, IAppServiceServico
    {
        private readonly IServiceServico _servico;
        public AppServiceServico(IServiceServico servico)
            :base(servico)
        {
            _servico = servico;
        }

        public IEnumerable<Servico> GetByOwner(string setor)
        {
            return _servico.GetByOwner(setor);
        }
    }
}
