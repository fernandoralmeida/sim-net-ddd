using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Application.SDE.Interface
{
    using Domain.SDE.Entity;
    using Domain.Cnpj.Entity;
    using Application.Interface;
    using Domain.BI;
    public interface IAppServiceEmpresa : IAppServiceBase<Empresas>
    {
        IEnumerable<Empresas> ConsultaByCNPJ(string _cnpj);
        IEnumerable<Empresas> ConsultaByRazaoSocial(string _name);
        Task<IEnumerable<Empresas>> ListTop20();

        /** Consultas Especificas **/
        Task<BaseReceitaFederal> GetCnpjAsync(string cnpj);
        Task<IEnumerable<BaseReceitaFederal>> ListTop10();
        Task<IEnumerable<BaseReceitaFederal>> ListAsync();
        Task<IEnumerable<BaseReceitaFederal>> ListByMunicipioAsync(string municipio);
        Task<IEnumerable<BaseReceitaFederal>> ListByOptanteSimplesAsync(string municipio);
        Task<IEnumerable<BaseReceitaFederal>> ListByLogradouroAsync(string logradouro, string atividade, string municipio, string situacao);
        Task<IEnumerable<BaseReceitaFederal>> ListByBairroAsync(string bairro, string atividade, string municipio, string situacao);
        Task<IEnumerable<BaseReceitaFederal>> ListByCNAEAsync(string atividade, string municipio);
        Task<IEnumerable<BaseReceitaFederal>> ListByCNPJBaseAsync(string cnpj);
        Task<IEnumerable<BaseReceitaFederal>> ListBySociosAsync(string nomesocio);
        Task<IEnumerable<Municipio>> ListMinicipios();
        Task<IEnumerable<Municipio>> MicroRegiaoJahu();
        
        /** Relatorio Estatisticos **/
        Task<IEnumerable<BaseReceitaFederal>> ListForBIAsync(string municipio, string situacao, string ano, string mes);
        Task<IEnumerable<BiEmpresas>> BiEmpresasAsync(string municipio, string situacao, string ano, string mes);
        Task<IEnumerable<BaseReceitaFederal>> ListForBICnaeAsync(string municipio);
        Task<IEnumerable<BiCnae>> ListBICnae(string municipio);
        Task<IEnumerable<Empresas>> ListforCnaeJsonAsync(string cnae, string municipio, string situacao);
        Task<IEnumerable<Empresas>> ListByParam(List<object> lparam);
    }
}
