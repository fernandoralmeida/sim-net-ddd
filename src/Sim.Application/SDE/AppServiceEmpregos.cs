using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Application.SDE
{
    using Interface;
    using Domain.SDE.Entity;
    using Domain.SDE.Interface;
    public class AppServiceEmpregos : AppServiceBase<Empregos>, IAppServiceEmpregos
    {
        private readonly IServiceEmpregos _db;
        public AppServiceEmpregos(IServiceEmpregos serviceEmpregos) : base(serviceEmpregos) { _db = serviceEmpregos; }
        public Task<IEnumerable<Empregos>> GetAllEmpregosAsync()
        {
            return _db.GetAllEmpregosAsync();
        }

        public Task<IEnumerable<Empregos>> GetAllEmpregosAsync(string cnpj)
        {
            return _db.GetAllEmpregosAsync(cnpj);
        }

        public Task<IEnumerable<Empregos>> GetByIdAsync(Guid id)
        {
            return _db.GetByIdAsync(id);
        }
    }
}
