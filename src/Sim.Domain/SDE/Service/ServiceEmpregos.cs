using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Domain.SDE.Service
{
    using Entity;
    using Interface;
    using Domain.Service;
    using Sim.Domain.Interface;

    public class ServiceEmpregos : ServiceBase<Empregos>, IServiceEmpregos
    {
        private readonly IRepositoryEmpregos repositoryEmpregos;
        public ServiceEmpregos(IRepositoryEmpregos repositoryBase) : base(repositoryBase)
        {
            repositoryEmpregos = repositoryBase;
        }

        public Task<IEnumerable<Empregos>> GetAllEmpregosAsync()
        {
            return repositoryEmpregos.GetAllEmpregosAsync();
        }

        public Task<IEnumerable<Empregos>> GetAllEmpregosAsync(string cnpj)
        {
            return repositoryEmpregos.GetAllEmpregosAsync(cnpj);
        }
    }
}
