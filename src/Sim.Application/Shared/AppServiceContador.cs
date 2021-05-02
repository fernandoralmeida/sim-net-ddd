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
    public class AppServiceContador : AppServiceBase<Contador>, IAppServiceContador
    {
        private readonly IServiceContador _contador;
        public AppServiceContador(IServiceContador contador)
            :base(contador)
        { _contador = contador; }

        public Task<string> GetProtocoloAsync(string appuserid, string moduloname)
        {
            return _contador.GetProtocoloAsync(appuserid, moduloname);
        }
    }
}
