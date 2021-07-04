using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Sim.Cross.Data.Repository.Cnpj
{
    using Context;
    using Sim.Domain.Cnpj.Entity;
    using Sim.Domain.Cnpj.Interface;
    public class RepositoryCNAEs : IBase<CNAE>
    {
        private readonly RFBContext db;

        public RepositoryCNAEs(RFBContext context)
        {
            db = context;
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public async Task<IEnumerable<CNAE>> ListAll()
        {
            return await db.CNAEs.OrderBy(s => s.Codigo).ToListAsync();
        }
    }
}
