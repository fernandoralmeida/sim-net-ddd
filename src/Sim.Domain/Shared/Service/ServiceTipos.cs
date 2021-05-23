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

    public class ServiceTipos : ServiceBase<Tipo>, IServiceTipo
    {
        private readonly IRepositoryTipo _repositoryTipo;

        public ServiceTipos(IRepositoryTipo repositoryTipo)
            :base(repositoryTipo)
        {
            _repositoryTipo = repositoryTipo;
        }

        public IEnumerable<Tipo> GetByOwner(string owner)
        {
            return _repositoryTipo.GetByOwner(owner);
        }

        public int LastCodigo()
        {
            return 0;
        }
    }
}
