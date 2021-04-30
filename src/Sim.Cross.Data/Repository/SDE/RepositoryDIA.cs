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
    public class RepositoryDIA : RepositoryBase<DIA>, IRepositoryDIA
    {
        public RepositoryDIA(ApplicationContext dbContext)
            :base(dbContext)
        {

        }
        public IEnumerable<DIA> GetAtivos()
        {
            return _db.DIA.Where(c => c.Situacao == "ATIVO");
        }

        public IEnumerable<DIA> GetBaixados()
        {
            return _db.DIA.Where(c => c.Situacao == "BAIXADO");
        }

        public IEnumerable<DIA> GetByAtividade(string atividade)
        {
            return _db.DIA.Where(c => c.Ambulante.Atividade.Contains(atividade));
        }

        public IEnumerable<DIA> GetByAuxiliar(string nome)
        {
            return _db.DIA.Where(c => c.Ambulante.Pessoas.ToList()[0].Nome.Contains(nome));
        }

        public IEnumerable<DIA> GetByTitular(string nome)
        {
            return _db.DIA.Where(c => c.Ambulante.Pessoas.ToList()[0].Nome.Contains(nome));
        }

        public IEnumerable<DIA> GetVencidos()
        {
            return _db.DIA.Where(c => c.Validade < DateTime.Now.Date);
        }
    }
}
