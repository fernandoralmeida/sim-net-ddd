using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Application.SDE.Interface
{
    using Domain.SDE.Entity;
    using Application.Interface;
    public interface IAppServiceEmpresa : IAppServiceBase<Empresa>
    {
        IEnumerable<Empresa> ConsultaByCNPJ(string _cnpj);
        IEnumerable<Empresa> ConsultaByCNAE(string _cnae);
        IEnumerable<Empresa> ConsultaByRazaoSocial(string _name);

        IEnumerable<Empresa> ListEmpresasQsa(Guid id);
        Task<IEnumerable<Empresa>> UltimasFormalizacoes();
    }
}
