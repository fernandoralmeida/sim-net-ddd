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
    public class ServiceEmpresa : ServiceBase<Empresa>, IServiceEmpresa
    {
        private readonly IRepositoryEmpresa _repositoryEmpresa;

        public ServiceEmpresa(IRepositoryEmpresa repositoryEmpresa)
            :base(repositoryEmpresa)
        {
            _repositoryEmpresa = repositoryEmpresa;
        }
        public IEnumerable<Empresa> ConsultaByCNAE(string cnae)
        {
            return _repositoryEmpresa.ConsultaByCNAE(cnae);
        }

        public IEnumerable<Empresa> ConsultaByCNPJ(string cnpj)
        {
            return _repositoryEmpresa.ConsultaByCNPJ(cnpj);
        }

        public IEnumerable<Empresa> ConsultaByRazaoSocial(string name)
        {
            return _repositoryEmpresa.ConsultaByRazaoSocial(name);
        }
    }
}
