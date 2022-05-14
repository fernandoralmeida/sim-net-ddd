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
            var t = _db.Emprego.Include(e => e.Empresa).ToListAsync();
            await t;
            return t.Result;
        }

        public async Task<IEnumerable<Empregos>> GetAllEmpregosAsync(string cnpj)
        {
            var t = Task.Run(() => _db.Emprego.Include(e => e.Empresa).Where(s => s.Empresa.CNPJ == cnpj));
            await t;
            return t.Result;
        }
    }
}
