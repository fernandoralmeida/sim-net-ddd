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
    public class AppServiceParceiro : AppServiceBase<Parceiro>, IAppServiceParceiro
    {
        private readonly IServiceParceiro _parceiro;
        public AppServiceParceiro(IServiceParceiro parceiro)
            : base(parceiro)
        {
            _parceiro = parceiro;
        }

        public async Task<IEnumerable<Parceiro>> ListParceiros(string owner)
        {
           return await _parceiro.ListParceiros(owner);
        }
    }
}
