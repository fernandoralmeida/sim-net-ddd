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
    public class AppServiceEmpresa : AppServiceBase<Empresa>, IAppServiceEmpresa
    {
        private readonly IServiceEmpresa _empresa;

        public AppServiceEmpresa(IServiceEmpresa empresa)
            :base(empresa)
        {
            _empresa = empresa;
        }

        public IEnumerable<Empresa> ConsultaByCNAE(string cnae)
        {
            return _empresa.ConsultaByCNAE(cnae);
        }

        public IEnumerable<Empresa> ConsultaByCNPJ(string cnpj)
        {
            return _empresa.ConsultaByCNPJ(cnpj);
        }

        public IEnumerable<Empresa> ConsultaByRazaoSocial(string name)
        {
            return _empresa.ConsultaByRazaoSocial(name);
        }

        public IEnumerable<Empresa> ListEmpresasQsa(Guid id)
        {
            return _empresa.ListEmpresasQsa(id);
        }

        public async Task<IEnumerable<Empresa>> UltimasFormalizacoes()
        {
            return await _empresa.UltimasFormalizacoes();
        }
    }
}
