using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Domain.Shared.Interface
{
    using Entity;
    using Domain.Interface;
    using Domain.BI;
    public interface IServiceAtendimento : IServiceBase<Atendimento>
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
        IEnumerable<Atendimento> ListarRaeLancados(IEnumerable<Atendimento> atendimentos);
        IEnumerable<Atendimento> ListarRaeNaoLancados(IEnumerable<Atendimento> atendimentos);
        Task<IEnumerable<KeyValuePair<string, int>>> BySetor(string setor, DateTime periodo);
        Task<IEnumerable<KeyValuePair<string, int>>> BySetorMonth(string setor, DateTime periodo);
        Task<IEnumerable<KeyValuePair<string, int>>> ByAll(DateTime periodo);
        Task<IEnumerable<KeyValuePair<string, int>>> ByAllMonth(DateTime periodo);
        Task<IEnumerable<KeyValuePair<string, int>>> ByUserName(string username, DateTime periodo);
        Task<IEnumerable<KeyValuePair<string, int>>> ByUserNameMonth(string username, string setor, DateTime periodo);
        Task<IEnumerable<KeyValuePair<string, int>>> ByServicos(string servico, DateTime periodo);
        Task<IEnumerable<KeyValuePair<string, int>>> ByServicosMonth(string servico, DateTime periodo);
        Task<IEnumerable<KeyValuePair<string, int>>> ByCanal(string canal, string setor, DateTime periodo);
        Task<IEnumerable<KeyValuePair<string, int>>> ByCanalMonth(string canal, string setor, DateTime periodo);
        Task<IEnumerable<Atendimento>> ListAtendimentosAtivos();
        Task<IEnumerable<Atendimento>> ListByParam(List<object> lparam);

        /** BI **/
        Task<BiAtendimentos> BI_Atendimentos(DateTime periodo);
        Task<IEnumerable<KeyValuePair<string, int>>> BI_Atendimentos_Setor(DateTime periodo, string setor);
        Task<IEnumerable<KeyValuePair<string, int>>> BI_Atendimentos_AppUser(DateTime periodo);
    }
}
