using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Domain.Cnpj.Service
{
    using Entity;
    using Cnpj.Interface;

    public class ServiceMunicipios : IServiceMunicipios<Municipio>
    {

        private readonly IBase<Municipio> _municipios;
        public ServiceMunicipios(IBase<Municipio> municipios) 
        {
            _municipios = municipios;
        }
        public void Dispose()
        {
            _municipios.Dispose();
        }

        public async Task<IEnumerable<Municipio>> ListAll()
        {
            return await _municipios.ListAll();
        }

        public async Task<IEnumerable<Municipio>> MicroRegiaoJahu()
        {
            var q = await ListAll();
            var t = q.Where(s => s.MicroRegiaoJahu(s));
            return t;
        }
    }
}
