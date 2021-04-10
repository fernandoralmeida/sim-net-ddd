using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Domain.SDE.Interface
{
    using SDE.Entity;
    using Domain.Interface;
    public interface IServicePessoa : IServiceBase<Pessoa>
    {
        IEnumerable<Pessoa> ConsultaByNome(string nome);
        IEnumerable<Pessoa> ConsultaByCPF(string cpf);
        IEnumerable<Pessoa> ConsultarPessoaByNameOrCPF(string _cpf, string nome);
    }
}
