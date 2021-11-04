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
    public class ServiceParceiros : ServiceBase<Parceiro>, IServiceParceiro
    {
        private readonly IRepositoryParceiro _repositoryParceiro;

        public ServiceParceiros(IRepositoryParceiro repositoryParceiro)
            : base(repositoryParceiro)
        {
            _repositoryParceiro = repositoryParceiro;
        }

        public async Task<IEnumerable<Parceiro>> ListParceiros(string owner)
        {
            return await _repositoryParceiro.ListParceiros(owner);
        }
    }
}
