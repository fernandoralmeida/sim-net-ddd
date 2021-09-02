using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Domain.Shared.Interface
{
    using Entity;
    using Domain.Interface;
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
        IEnumerable<Atendimento> ListAll();
        Atendimento GetAtendimento(Guid id);
        Task<IEnumerable<Atendimento>> GetByUserName(string username);
        IEnumerable<Atendimento> ListarRaeLancados(IEnumerable<Atendimento> atendimentos);
        IEnumerable<Atendimento> ListarRaeNaoLancados(IEnumerable<Atendimento> atendimentos);
        Task<IEnumerable<KeyValuePair<string, int>>> BySetor(string setor);
        Task<IEnumerable<KeyValuePair<string, int>>> ByAll();
        Task<IEnumerable<KeyValuePair<string, int>>> ByUserName(string username);
        Task<IEnumerable<KeyValuePair<string, int>>> ByServicos(string servico);
        Task<IEnumerable<KeyValuePair<string, int>>> ByCanal(string canal, string setor);
    }
}
