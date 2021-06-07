using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Cross.Data.Repository.SDE
{
    using Sim.Domain.SDE.Entity;
    using Sim.Domain.SDE.Interface;
    using Context;
    public class RepositoryQsa : RepositoryBase<QSA>, IRepositoryQSA
    {
        public RepositoryQsa(ApplicationContext dbContext)
            :base(dbContext)
        {

        }

        public IEnumerable<QSA> GetBySocio(string nome)
        {
            return null;// _db.QSA.Where(c => c.Nome.Contains(nome)).OrderBy(c => c.Nome);
        }
    }
}
