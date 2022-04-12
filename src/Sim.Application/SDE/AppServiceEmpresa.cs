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
    using Sim.Domain.Cnpj.Entity;
    using Domain.BI;

    public class AppServiceEmpresa : AppServiceBase<Empresas>, IAppServiceEmpresa
    {
        private readonly IServiceEmpresa _empresa;

        public AppServiceEmpresa(IServiceEmpresa empresa)
            :base(empresa)
        {
            _empresa = empresa;
        }

        public IEnumerable<Empresas> ConsultaByCNPJ(string cnpj)
        {
            return _empresa.ConsultaByCNPJ(cnpj);
        }

        public IEnumerable<Empresas> ConsultaByRazaoSocial(string name)
        {
            return _empresa.ConsultaByRazaoSocial(name);
        }

        public Task<IEnumerable<BiEmpresas>> BiEmpresasAsync(string municipio, string situacao, string ano, string mes)
        {
            return _empresa.BiEmpresasAsync(municipio, situacao, ano, mes);
        }

        public async Task<BaseReceitaFederal> GetCnpjAsync(string cnpj)
        {
            return await _empresa.GetCnpjAsync(cnpj);
        }

        public async Task<IEnumerable<BaseReceitaFederal>> ListAsync()
        {
            return await _empresa.ListAsync();
        }

        public async Task<IEnumerable<BaseReceitaFederal>> ListByBairroAsync(string bairro, string atividade, string municipio, string situacao)
        {
            return await _empresa.ListByBairroAsync(bairro, atividade, municipio, situacao);
        }

        public async Task<IEnumerable<BaseReceitaFederal>> ListByCNAEAsync(string atividade, string municipio)
        {
            return await _empresa.ListByCNAEAsync(atividade, municipio);
        }

        public async Task<IEnumerable<BaseReceitaFederal>> ListByCNPJBaseAsync(string cnpj)
        {
            return await _empresa.ListByCNPJBaseAsync(cnpj);
        }

        public async Task<IEnumerable<BaseReceitaFederal>> ListByLogradouroAsync(string logradouro, string atividade, string municipio, string situacao)
        {
            return await _empresa.ListByLogradouroAsync(logradouro, atividade, municipio, situacao);
        }

        public async Task<IEnumerable<BaseReceitaFederal>> ListByMunicipioAsync(string municipio)
        {
            return await _empresa.ListByMunicipioAsync(municipio);
        }

        public async Task<IEnumerable<BaseReceitaFederal>> ListByOptanteSimplesAsync(string municipio)
        {
            return await _empresa.ListByOptanteSimplesAsync(municipio);
        }

        public async Task<IEnumerable<BaseReceitaFederal>> ListBySociosAsync(string nomesocio)
        {
            return await _empresa.ListBySociosAsync(nomesocio);
        }

        public async Task<IEnumerable<Municipio>> ListMinicipios()
        {
            return await _empresa.ListMinicipios();
        }

        public async Task<IEnumerable<BaseReceitaFederal>> ListTop10()
        {
            return await _empresa.ListTop10();
        }

        public async Task<IEnumerable<Empresas>> ListTop20()
        {
            return await _empresa.ListTop20();
        }

        public async Task<IEnumerable<Municipio>> MicroRegiaoJahu()
        {
            return await _empresa.MicroRegiaoJahu();
        }

        public async Task<IEnumerable<BaseReceitaFederal>> ListForBIAsync(string municipio, string situacao, string ano, string mes)
        {
            return await _empresa.ListForBIAsync(municipio, situacao, ano, mes);
        }

        public async Task<IEnumerable<BiCnae>> ListBICnae(string municipio)
        {
            return await _empresa.ListBICnae(municipio);
        }

        public async Task<IEnumerable<BaseReceitaFederal>> ListForBICnaeAsync(string municipio)
        {
            return await _empresa.ListForBICnaeAsync(municipio);
        }

        public async Task<IEnumerable<KeyValuePair<string, string>>> ListforCnaeJsonAsync(string cnae, string municipio, string situacao)
        {
            return await _empresa.ListforCnaeJsonAsync(cnae, municipio, situacao);
        }

        public async Task<IEnumerable<BaseReceitaFederal>> ListByParam(List<object> lparam)
        {
            return await _empresa.ListByParam(lparam);
        }
    }
}
