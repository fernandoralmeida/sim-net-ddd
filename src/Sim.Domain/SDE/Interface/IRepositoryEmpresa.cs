using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Domain.SDE.Interface
{
    using Entity;
    using Cnpj.Entity;
    using Domain.Interface;
    public interface IRepositoryEmpresa : IRepositoryBase<Empresas>
    {
        IEnumerable<Empresas> ConsultaByCNPJ(string cnpj);
        IEnumerable<Empresas> ConsultaByRazaoSocial(string name);
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
        Task<IEnumerable<BaseReceitaFederal>> ListForBIAsync(string municipio, string situacao, string ano, string mes);
        Task<IEnumerable<BaseReceitaFederal>> ListForBICnaeAsync(string municipio);
        Task<IEnumerable<Municipio>> ListMinicipios();
        Task<IEnumerable<Empresas>> ListByParam(List<object> lparam);

    }
}
