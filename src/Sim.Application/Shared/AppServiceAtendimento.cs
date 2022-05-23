using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Application.Shared
{
    using Domain.Shared.Entity;
    using Domain.Shared.Interface;
    using Domain.BI;
    using Interface;
    public class AppServiceAtendimento : AppServiceBase<Atendimento>, IAppServiceAtendimento
    {
        private readonly IServiceAtendimento _atendimento;
        public AppServiceAtendimento(IServiceAtendimento atendimento)
            : base(atendimento)
        {
            _atendimento = atendimento;
        }

        public IEnumerable<Atendimento> AtendimentoAtivo(string userid)
        {
            return _atendimento.AtendimentoAtivo(userid);
        }

        public IEnumerable<Atendimento> AtendimentosCancelados(string userid)
        {
            return _atendimento.AtendimentosCancelados(userid);
        }

        public Atendimento GetAtendimento(Guid id)
        {
            return _atendimento.GetAtendimento(id);
        }

        public IEnumerable<Atendimento> GetByCanal(string canal)
        {
            return _atendimento.GetByCanal(canal);
        }

        public IEnumerable<Atendimento> GetByDate(DateTime? dateTime)
        {
            return _atendimento.GetByDate(dateTime);
        }

        public IEnumerable<Atendimento> GetByEmpresa(string cnpj)
        {
            return _atendimento.GetByEmpresa(cnpj);
        }

        public IEnumerable<Atendimento> GetByPessoa(string cpf)
        {
            return _atendimento.GetByPessoa(cpf);
        }

        public IEnumerable<Atendimento> GetByServicos(string servicos)
        {
            return _atendimento.GetByServicos(servicos);
        }

        public IEnumerable<Atendimento> GetBySetor(string setor)
        {
            return _atendimento.GetBySetor(setor);
        }

        public IEnumerable<Atendimento> ListAll()
        {
            return _atendimento.ListAll();
        }

        public IEnumerable<Atendimento> ListByPeriodo(DateTime? dataI, DateTime? dataF)
        {
            return _atendimento.ListByPeriodo(dataI, dataF);
        }

        public IEnumerable<Atendimento> MeusAtendimentos(string userid, DateTime? date)
        {
            return _atendimento.MeusAtendimentos(userid, date);
        }

        public IEnumerable<Atendimento> MeusAtendimentosRae(string userid)
        {
            return _atendimento.MeusAtendimentosRae(userid);
        }

        public IEnumerable<Atendimento> ListarRaeLancados(string userid)
        {
            return _atendimento.ListarRaeLancados(MeusAtendimentosRae(userid));
        }

        public IEnumerable<Atendimento> ListarRaeNaoLancados(string userid)
        {
            return _atendimento.ListarRaeNaoLancados(MeusAtendimentosRae(userid));
        }

        public async Task<IEnumerable<Atendimento>> GetByUserName(string username)
        {
            return await _atendimento.GetByUserName(username);
        }

        public async Task<IEnumerable<Atendimento>> ListByMonth(DateTime? month)
        {
            return await _atendimento.ListByMonth(month);
        }

        public async Task<IEnumerable<Atendimento>> GetByUserNamePeriodo(string username, DateTime? date)
        {
            return await _atendimento.GetByUserNamePeriodo(username, date);
        }

        public async Task<IEnumerable<Atendimento>> ListAtendimentosAtivos()
        {
            return await _atendimento.ListAtendimentosAtivos();
        }

        public async Task<IEnumerable<Atendimento>> ListByParam(List<object> lparam)
        {
            return await _atendimento.ListByParam(lparam);
        }

        /** BI **/
        public Task<BiAtendimentos> BI_Atendimentos(DateTime periodo)
        {
            return _atendimento.BI_Atendimentos(periodo);
        }

        public Task<BiAtendimentos> BI_Atendimentos_AppUser(DateTime periodo)
        {
            return _atendimento.BI_Atendimentos_AppUser(periodo);
        }

        public Task<BiAtendimentos> BI_Atendimentos_Setor(DateTime periodo, string setor)
        {
            return _atendimento.BI_Atendimentos_Setor(periodo, setor);
        }
    }
}
