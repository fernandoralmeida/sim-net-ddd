using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Application.SDE.Interface
{
    using Domain.SDE.Entity;
    using Application.Interface;
    public interface IAppServicePessoa : IAppServiceBase<Pessoa>
    {
        IEnumerable<Pessoa> ConsultaByNome(string nome);
        IEnumerable<Pessoa> ConsultaByCPF(string cpf);
        IEnumerable<Pessoa> ConsultarPessoaByNameOrCPF(string _cpf, string nome);
    }
}
