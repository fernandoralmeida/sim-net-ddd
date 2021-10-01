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
                .Include(s => s.Sebrae)
                .Where(s => s.Owner_AppUser_Id == userid && s.Status == "Ativo");

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
            return _db.Atendimento
                .Include(p => p.Pessoa)
                .Include(e => e.Empresa)
                .Include(s => s.Sebrae)
                .Where(u => u.Empresa.CNPJ == cnpj).OrderBy(d => d.Data).OrderByDescending(o => o.Data);
        }

        public IEnumerable<Atendimento> GetByPessoa(string cpf)
        {
            return _db.Atendimento
                .Include(p => p.Pessoa)
                .Include(e => e.Empresa)
                .Include(s => s.Sebrae)
                .Where(u => u.Pessoa.CPF == cpf).OrderBy(d => d.Data).OrderByDescending(o => o.Data);
        }

        public IEnumerable<Atendimento> GetByServicos(string servicos)
        {
            return _db.Atendimento.Where(u => u.Servicos.Contains(servicos) && u.Status == "Finalizado" && u.Ativo == true);
        }

        public IEnumerable<Atendimento> GetBySetor(string setor)
        {
            return _db.Atendimento.Where(u => u.Setor == setor && u.Status == "Finalizado" && u.Ativo == true);
        }

        public IEnumerable<Atendimento> ListByPeriodo(DateTime? dataI, DateTime? dataF)
        {
            var list = _db.Atendimento
                .Include(p => p.Pessoa)
                .Include(e => e.Empresa)
                .Include(s => s.Sebrae)
                .Where(a => a.Data.Value.Date >= dataI
                && a.Data.Value.Date <= dataF && a.Status == "Finalizado" && a.Ativo == true).OrderBy(o => o.Data);

            return list;
        }

        public IEnumerable<Atendimento> MeusAtendimentos(string userid, DateTime? date)
        {
            var lista = _db.Atendimento
                .Include(u => u.Pessoa)
                .Include(e => e.Empresa)
                .Include(s => s.Sebrae)
                .Where(a => a.Owner_AppUser_Id == userid && a.Data.Value.Date == date.Value.Date && a.Status == "Finalizado" && a.Ativo == true).OrderBy(o => o.Data);

            return lista;
        }

        public IEnumerable<Atendimento> ListAll()
        {
            var lista = _db.Atendimento
                .Include(u => u.Pessoa)
                .Include(e => e.Empresa)
                .Include(s => s.Sebrae)
                .Where(a => a.Ativo == true && a.Status == "Finalizado").OrderBy(a => a.Data).OrderBy(o => o.Data);

            return lista;
        }

        public IEnumerable<Atendimento> AtendimentosCancelados(string userid)
        {
            var ativo = _db.Atendimento
                .Include(p => p.Pessoa)
                .Include(e => e.Empresa)
                .Include(s => s.Sebrae)
                .Where(s => s.Owner_AppUser_Id == userid && s.Status == "Cancelado" && s.Ativo == true).OrderBy(o => o.DataF);

            return ativo;
        }

        public Atendimento GetAtendimento(Guid id)
        {
            var ativo = _db.Atendimento
                .Include(p => p.Pessoa)
                .Include(e => e.Empresa)
                .Include(s => s.Sebrae)
                .Where(i => i.Id == id).FirstOrDefault();

            return ativo;
        }

        public IEnumerable<Atendimento> MeusAtendimentosRae(string userid)
        {
            var lista = _db.Atendimento
                .Include(u => u.Pessoa)
                .Include(e => e.Empresa)
                .Include(s => s.Sebrae)
                .Where(a => a.Owner_AppUser_Id == userid && a.Status=="Finalizado" && a.Ativo == true).OrderBy(o => o.Data);

            return lista;
        }

        public async Task<IEnumerable<Atendimento>> GetByUserName(string username)
        {
            var t = Task.Run(() => _db.Atendimento.Where(u => u.Owner_AppUser_Id == username && u.Status == "Finalizado" && u.Ativo == true));
            await t;
            return t.Result;
        }

        public async Task<IEnumerable<Atendimento>> ListByMonth(DateTime? month)
        {
            var list = Task.Run(() => _db.Atendimento
                .Include(p => p.Pessoa)
                .Include(e => e.Empresa)
                .Include(s => s.Sebrae)
                .Where(a => a.Data.Value.Month == month.Value.Month
                && a.Status == "Finalizado"
                && a.Ativo == true).OrderBy(o => o.Data));
            await list;

            return list.Result;
        }

        public async Task<IEnumerable<Atendimento>> GetByUserNamePeriodo(string username, DateTime? date)
        {
            var t = Task.Run(() => _db.Atendimento
                .Include(p => p.Pessoa)
                .Include(e => e.Empresa)
                .Include(s => s.Sebrae)
                .Where(u => u.Owner_AppUser_Id == username && u.Status == "Finalizado" && u.Ativo == true && u.Data.Value.Date == date.Value.Date));
            await t;
            return t.Result;
        }
    }
}
