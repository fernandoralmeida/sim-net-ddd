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
    public class RepositoryInscricao : RepositoryBase<Inscricao>, IRepositoryInscricao
    {
        public RepositoryInscricao(ApplicationContext applicationContext)
            :base(applicationContext)
        {

        }

        public IEnumerable<Inscricao> GetByEvento(string evento)
        {
            return _db.Inscricao.Where(u=>u.Evento_Id > 0);
        }

        public IEnumerable<Inscricao> GetByParticipante(string nome)
        {
            return _db.Inscricao.Where(u => u.Evento_Id > 0);
        }

        public IEnumerable<Inscricao> GetByTipo(string evento)
        {
            return _db.Inscricao.Where(u => u.Tipo == evento);
        }
    }
}
