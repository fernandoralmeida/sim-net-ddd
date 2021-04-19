using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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
            return _db.Inscricao.Where(u => u.Numero.ToString() == evento);
        }

        public IEnumerable<Inscricao> GetByParticipante(string nome)
        {
            var query =
               from evento in _db.Evento
               from pessoa in _db.Pessoa
               join inscricao in _db.Inscricao on evento.Id equals inscricao.Evento_Id
               where pessoa.Nome == nome
               select new { Inscricao = inscricao, Evento = evento, Pessoa = pessoa };

            var lista = new List<Inscricao>() { };

            foreach(var i in query)
            {               
                lista.Add(i.Inscricao);
            }


            return lista; //_db.Inscricao.Select(r => r);
        }

        public IEnumerable<Inscricao> GetByTipo(string evento)
        {
            return _db.Inscricao.Select(r => r);
        }
    }
}
