using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Cross.Data.Repository.Shared
{
    using Sim.Domain.Shared.Entity;
    using Sim.Domain.Shared.Interface;
    using Context;
    public class RepositoryEvento : RepositoryBase<Evento>, IRepositoryEvento
    {
        public RepositoryEvento(ApplicationContext dbContext)
            :base(dbContext)
        {

        }

        public IEnumerable<Evento> GetByNome(string nome)
        {
            return _db.Evento.Where(u => u.Nome.Contains(nome)).OrderBy(d => d.Data).ThenByDescending(d => d.Data);
        }

        public IEnumerable<Evento> GetByOwner(string setor)
        {
            return _db.Evento.Where(u => u.Owner.Contains(setor));
        }

        public int LastCodigo()
        {
            return _db.Evento.Select(c => c.Codigo).FirstOrDefault();
        }
    }
}
