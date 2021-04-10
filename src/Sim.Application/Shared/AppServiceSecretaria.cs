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
    public class AppServiceSecretaria : AppServiceBase<Secretaria>, IAppServiceSecretaria
    {
        private readonly IServiceSecretaria _secretaria;

        public AppServiceSecretaria(IServiceSecretaria secretaria)
            :base(secretaria)
        {
            _secretaria = secretaria;
        }

        public IEnumerable<Secretaria> GetByOwner(string setor)
        {
            return _secretaria.GetByOwner(setor);
        }
    }
}
