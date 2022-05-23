using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Application.Shared.Interface
{
    using Domain.Shared.Entity;
    using Application.Interface;
    using Domain.BI;
    public interface IAppServiceAtendimento : IAppServiceBase<Atendimento>
    {
        IEnumerable<Atendimento> GetByPessoa(string cpf);
        IEnumerable<Atendimento> GetByEmpresa(string cnpj);
        IEnumerable<Atendimento> GetBySetor(string setor);
        IEnumerable<Atendimento> GetByCanal(string canal);
        IEnumerable<Atendimento> GetByServicos(string servicos);
        IEnumerable<Atendimento> GetByDate(DateTime? dateTime);
        IEnumerable<Atendimento> MeusAtendimentos(string userid, DateTime? date);
        IEnumerable<Atendimento> MeusAtendimentosRae(string userid);
        IEnumerable<Atendimento> AtendimentoAtivo(string userid);
        IEnumerable<Atendimento> AtendimentosCancelados(string userid);
        IEnumerable<Atendimento> ListByPeriodo(DateTime? dataI, DateTime? dataF);
        Task<IEnumerable<Atendimento>> ListByMonth(DateTime? month);
        IEnumerable<Atendimento> ListAll();
        Atendimento GetAtendimento(Guid id);
        Task<IEnumerable<Atendimento>> GetByUserName(string username);
        Task<IEnumerable<Atendimento>> GetByUserNamePeriodo(string username, DateTime? date);
        IEnumerable<Atendimento> ListarRaeLancados(string userid);
        IEnumerable<Atendimento> ListarRaeNaoLancados(string userid);
        Task<IEnumerable<Atendimento>> ListAtendimentosAtivos();
        Task<IEnumerable<Atendimento>> ListByParam(List<object> lparam);

        /** BI **/
        Task<BiAtendimentos> BI_Atendimentos(DateTime periodo);
        Task<BiAtendimentos> BI_Atendimentos_Setor(DateTime periodo,string setor);
        Task<BiAtendimentos> BI_Atendimentos_AppUser(DateTime periodo);
    }
}
