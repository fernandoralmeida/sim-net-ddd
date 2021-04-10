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
    public class ServiceSecretaria : ServiceBase<Secretaria>, IServiceSecretaria
    {
        private readonly IRepositorySecretaria _secretaria;

        public ServiceSecretaria(IRepositorySecretaria repositorySecretaria)
            :base(repositorySecretaria)
        {
            _secretaria = repositorySecretaria;
        }

        public IEnumerable<Secretaria> GetByOwner(string setor)
        {
            return _secretaria.GetByOwner(setor);
        }
    }
}
