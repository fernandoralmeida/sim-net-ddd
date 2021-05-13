using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Application.Shared.Interface
{
    using Domain.Shared.Entity;
    using Application.Interface;
    public interface IAppServiceAtendimento : IAppServiceBase<Atendimento>
    {
        IEnumerable<Atendimento> GetByPessoa(string cpf);
        IEnumerable<Atendimento> GetByEmpresa(string cnpj);
        IEnumerable<Atendimento> GetBySetor(string setor);
        IEnumerable<Atendimento> GetByCanal(string canal);
        IEnumerable<Atendimento> GetByServicos(string servicos);
        IEnumerable<Atendimento> GetByDate(DateTime? dateTime);
        IEnumerable<Atendimento> MeusAtendimentos(string userid, DateTime? date);
        IEnumerable<Atendimento> AtendimentoAtivo(string userid);
        IEnumerable<Atendimento> ListByPeriodo(DateTime? dataI, DateTime? dataF);
        IEnumerable<Atendimento> ListAll();
    }
}
