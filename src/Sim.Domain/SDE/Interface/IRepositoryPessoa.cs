using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Domain.SDE.Interface
{
    using SDE.Entity;
    using Domain.Interface;
    public interface IRepositoryPessoa : IRepositoryBase<Pessoa>
    {
        IEnumerable<Pessoa> ConsultaByNome(string nome);
        IEnumerable<Pessoa> ConsultaByCPF(string cpf);
        //IEnumerable<Pessoa> ConsultarPessoaByNameOrCPF(string cpf, string nome);
        IEnumerable<Pessoa> Top10();
    }
}
