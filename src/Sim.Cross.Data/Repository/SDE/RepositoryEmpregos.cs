using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Sim.Cross.Data.Repository.SDE
{
    using Sim.Domain.SDE.Entity;
    using Sim.Domain.SDE.Interface;
    using Context;
    public class RepositoryEmpregos : RepositoryBase<Empregos>, IRepositoryEmpregos
    {
        public RepositoryEmpregos(ApplicationContext applicationContext) : base(applicationContext) { }

        public async Task<IEnumerable<Empregos>> GetAllEmpregosAsync()
        {
            var t = _db.Emprego.ToListAsync();
            await t;
            return t.Result;
        }

        public Task<IEnumerable<Empregos>> GetAllEmpregosAsync(string cnpj)
        {
            var t = Task.Run(() => _db.Emprego.Where(s => s.Empresa.CNPJ == cnpj));
            return (Task<IEnumerable<Empregos>>)t.Result;
        }
    }
}
