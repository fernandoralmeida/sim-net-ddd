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
    public class AppServiceTipo: AppServiceBase<Tipo>, IAppServiceTipo
    {
        private readonly IServiceTipo _tipo;
        public AppServiceTipo(IServiceTipo tipo)
            :base(tipo)
        {
            _tipo = tipo;
        }

        public IEnumerable<Tipo> GetByOwner(string owner)
        {
            return _tipo.GetByOwner(owner);
        }
    }
}
