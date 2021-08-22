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
    public class ServiceStatusAtendimento : ServiceBase<StatusAtendimento>, IServiceStatusAtendimento
    {
        private readonly IRepositoryStatusAtendimento _statusatendimento;
        public ServiceStatusAtendimento(IRepositoryStatusAtendimento repositoryStatusAtendimento)
            : base(repositoryStatusAtendimento)
        {
            _statusatendimento = repositoryStatusAtendimento;
        }

        public async Task<IEnumerable<StatusAtendimento>> ListByUser(string username)
        {
            var t = await _statusatendimento.ListByUser(username);
            return t;
        }
    }
}
