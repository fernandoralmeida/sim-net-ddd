using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Application.SDE
{
    using Interface;
    using Domain.SDE.Entity;
    using Domain.SDE.Interface;
    public class AppServicePessoa : AppServiceBase<Pessoa>, IAppServicePessoa
    {
        private readonly IServicePessoa _pessoa;

        public AppServicePessoa(IServicePessoa pessoa):base(pessoa)
        {
            _pessoa = pessoa;
        }
        public IEnumerable<Pessoa> ConsultaByCPF(string cpf)
        {
            return _pessoa.ConsultaByCPF(cpf);
        }

        public IEnumerable<Pessoa> ConsultaByNome(string nome)
        {
            return _pessoa.ConsultaByNome(nome);
        }

        public IEnumerable<Pessoa> ConsultarPessoaByNameOrCPF(string cpf, string nome)
        {
            return _pessoa.ConsultarPessoaByNameOrCPF(cpf, nome);
        }
    }
}
