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
    public class RepositoryAtendimento : RepositoryBase<Atendimento>, IRepositoryAtendimento
    {
        public RepositoryAtendimento(ApplicationContext dbContext)
            :base(dbContext)
        {   }

        public IEnumerable<Atendimento> AtendimentoAtivo(string userid)
        {
            var ativo = _db.Atendimento
                .Include(p => p.Pessoa)
                .Include(e => e.Empresa)
                .Where(s=>s.Owner_AppUser_Id == userid && s.Status=="ATIVO");

            return ativo;
        }

        public IEnumerable<Atendimento> GetByCanal(string canal)
        {
            return _db.Atendimento.Where(u => u.Canal.Contains(canal));
        }

        public IEnumerable<Atendimento> GetByDate(DateTime? dateTime)
        {
            return _db.Atendimento.Where(u => u.Data == dateTime);
        }

        public IEnumerable<Atendimento> GetByEmpresa(string cnpj)
        {
            return _db.Atendimento.Where(u => u.Empresa.CNPJ == cnpj);
        }

        public IEnumerable<Atendimento> GetByPessoa(string cpf)
        {
            return _db.Atendimento.Where(u => u.Pessoa.CPF == cpf);
        }

        public IEnumerable<Atendimento> GetByServicos(string servicos)
        {
            return _db.Atendimento.Where(u => u.Servicos == servicos);
        }

        public IEnumerable<Atendimento> GetBySetor(string setor)
        {
            return _db.Atendimento.Where(u => u.Setor == setor);
        }

        public IEnumerable<Atendimento> MeusAtendimentos(string userid, DateTime? date)
        {
            var lista = _db.Atendimento
                .Include(u => u.Pessoa)
                .Include(e => e.Empresa)
                .Where(a => a.Owner_AppUser_Id == userid && a.Data == date);

            return lista;
        }
    }
}
