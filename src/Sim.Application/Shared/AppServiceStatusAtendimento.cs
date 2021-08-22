using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Application.Shared
{
    using Domain.Shared.Entity;
    using Domain.Shared.Interface;
    using Interface;
    public class AppServiceStatusAtendimento: AppServiceBase<StatusAtendimento>, IAppServiceStatusAtendimento
    {
        private readonly IServiceStatusAtendimento _statusatendimento;

        public AppServiceStatusAtendimento(IServiceStatusAtendimento statusatendimento)
            : base(statusatendimento)
        {
            _statusatendimento = statusatendimento;
        }

        public async Task<IEnumerable<StatusAtendimento>> ListByUser(string username)
        {
            var t = await _statusatendimento.ListByUser(username);
            return t;
        }
    }
}
