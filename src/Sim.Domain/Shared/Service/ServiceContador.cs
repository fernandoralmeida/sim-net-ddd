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
    public class ServiceContador : ServiceBase<Contador>, IServiceContador
    {
        private readonly IRepositoryContador _contador;
        public ServiceContador(IRepositoryContador repositorycontador)
            :base(repositorycontador)
        { _contador = repositorycontador; }

        public Task<string> GetProtocoloAsync(string appuserid, string moduloname)
        {
            return _contador.GetProtocoloAsync(appuserid, moduloname);
        }
    }
}
