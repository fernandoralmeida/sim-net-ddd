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
    public class ServiceSetor: ServiceBase<Setor>, IServiceSetor
    {
        private readonly IRepositorySetor _setor;
        public ServiceSetor(IRepositorySetor repositorySetor)
            :base(repositorySetor)
        {
            _setor = repositorySetor;
        }

        public IEnumerable<Setor> GetByOwner(string secretaria)
        {
            return _setor.GetByOwner(secretaria);
        }
    }
}
