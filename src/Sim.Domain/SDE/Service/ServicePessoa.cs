using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Domain.SDE.Service
{
    using Entity;
    using Interface;
    using Domain.Service;
    public class ServicePessoa : ServiceBase<Pessoa>, IServicePessoa
    {
        private readonly IRepositoryPessoa _repositoryPessoa;

        public ServicePessoa(IRepositoryPessoa repositoryPessoa):base(repositoryPessoa)
        {
            _repositoryPessoa = repositoryPessoa;
        }
        public IEnumerable<Pessoa> ConsultaByCPF(string cpf)
        {
            return _repositoryPessoa.ConsultaByCPF(cpf);
        }

        public IEnumerable<Pessoa> ConsultaByNome(string nome)
        {
            return _repositoryPessoa.ConsultaByNome(nome);
        }

        public IEnumerable<Pessoa> ConsultarPessoaByNameOrCPF(string cpf, string nome)
        {
            var _cpf = _repositoryPessoa.ConsultaByCPF(cpf).ToList();
            var _nome = _repositoryPessoa.ConsultaByNome(nome).ToList();

            if (_cpf.Count > 0)

                return _cpf;

            else if (_nome.Count > 0)

                return _nome;

            else

                return null;
        }
    }
}
