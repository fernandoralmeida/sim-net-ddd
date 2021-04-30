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
    class RepositoryAmbulante : RepositoryBase<Ambulante>, IRepositoryAmbulante
    {
        public RepositoryAmbulante(ApplicationContext dbContext)
            :base(dbContext)
        {

        }

        public IEnumerable<Ambulante> GetByAtividade(string atividade)
        {
            return _db.Ambulante.Where(c => c.Atividade.Contains(atividade));
        }

        public IEnumerable<Ambulante> GetByAuxiliar(string nome)
        {  
            return _db.Ambulante.Where(u => u.Pessoas.ToList()[0].Nome == nome).ToList();
        }

        public IEnumerable<Ambulante> GetByTitular(string nome)
        {
            return _db.Ambulante.Where(u => u.Pessoas.ToList()[0].Nome == nome).ToList();
        }
    }
}
