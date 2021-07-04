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
    public class RepositoryMunicipios: IBase<Municipio>
    {
        private readonly RFBContext db;

        public RepositoryMunicipios(RFBContext context)
        {
            db = context;
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public async Task<IEnumerable<Municipio>> ListAll()
        {
            return await db.Municipios.OrderBy(s => s.Descricao).ToListAsync();
        }

    }
}
