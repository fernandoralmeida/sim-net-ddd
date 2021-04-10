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
    public class AppServiceSetor: AppServiceBase<Setor>, IAppServiceSetor
    {
        private readonly IServiceSetor _setor;
        public AppServiceSetor(IServiceSetor setor)
            :base(setor)
        {
            _setor = setor;
        }

        public IEnumerable<Setor> GetByOwner(string secretaria)
        {
            return _setor.GetByOwner(secretaria);
        }
    }
}
