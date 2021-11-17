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
    using Sim.Domain.Cnpj.Entity;

    public class ServiceEmpresa : ServiceBase<Empresas>, IServiceEmpresa
    {
        private readonly IRepositoryEmpresa _repositoryEmpresa;

        public ServiceEmpresa(IRepositoryEmpresa repositoryEmpresa)
            :base(repositoryEmpresa)
        {
            _repositoryEmpresa = repositoryEmpresa;
        }

        public IEnumerable<Empresas> ConsultaByCNPJ(string cnpj)
        {
            return _repositoryEmpresa.ConsultaByCNPJ(cnpj);
        }

        public IEnumerable<Empresas> ConsultaByRazaoSocial(string name)
        {
            return _repositoryEmpresa.ConsultaByRazaoSocial(name);
        }

        public async Task<IEnumerable<KeyValuePair<string, int>>> EmpresasByMunicipioAsync(string municipio, string situacao)
        {

            var t = await _repositoryEmpresa.ListByMunicipioAsync(municipio);
            var r_empresas = new List<KeyValuePair<string, int>>();
            try
            {
                //List<string> _ano = new List<string>();
                //var _canal = new List<string>();
                var _situcao = new List<string>();
                var _emp = new List<string>();
                var _atv = new List<string>();

                foreach (BaseReceitaFederal at in t)
                {
                    if (at.Estabelecimento.SituacaoCadastral == situacao)
                    {

                        _atv.Add(at.AtividadePrincipal.Descricao);
                        _emp.Add("Empresas");
                    }

                    _situcao.Add(at.Estabelecimento.SituacaoCadastral);
                }

                r_empresas.Add(new KeyValuePair<string, int>("Empresas", _emp.Count()));

                var c_atv = from x in _atv
                             group x by x into g
                             let count = g.Count()
                             orderby count descending
                             select new { Value = g.Key, Count = count };

                foreach (var x in c_atv)
                {
                    r_empresas.Add(new KeyValuePair<string, int>(x.Value, x.Count));
                }

            }
            catch { }

            return r_empresas;

        }

        public async Task<BaseReceitaFederal> GetCnpjAsync(string cnpj)
        {
            return await _repositoryEmpresa.GetCnpjAsync(cnpj);
        }

        public async Task<IEnumerable<BaseReceitaFederal>> ListAsync()
        {
            return await _repositoryEmpresa.ListAsync();
        }

        public async Task<IEnumerable<BaseReceitaFederal>> ListByBairroAsync(string bairro, string atividade, string municipio, string situacao)
        {
            return await _repositoryEmpresa.ListByBairroAsync(bairro, atividade, municipio, situacao);
        }

        public async Task<IEnumerable<BaseReceitaFederal>> ListByCNAEAsync(string atividade, string municipio)
        {
            return await _repositoryEmpresa.ListByCNAEAsync(atividade, municipio);
        }

        public async Task<IEnumerable<BaseReceitaFederal>> ListByCNPJBaseAsync(string cnpj)
        {
            return await _repositoryEmpresa.ListByCNPJBaseAsync(cnpj);
        }

        public async Task<IEnumerable<BaseReceitaFederal>> ListByLogradouroAsync(string logradouro, string atividade, string municipio, string situacao)
        {
            return await _repositoryEmpresa.ListByLogradouroAsync(logradouro, atividade, municipio, situacao);
        }

        public async Task<IEnumerable<BaseReceitaFederal>> ListByMunicipioAsync(string municipio)
        {
            return await _repositoryEmpresa.ListByMunicipioAsync(municipio);
        }

        public async Task<IEnumerable<BaseReceitaFederal>> ListByOptanteSimplesAsync(string municipio)
        {
            return await _repositoryEmpresa.ListByOptanteSimplesAsync(municipio);
        }

        public async Task<IEnumerable<BaseReceitaFederal>> ListBySociosAsync(string nomesocio)
        {
            return await _repositoryEmpresa.ListBySociosAsync(nomesocio);
        }

        public async Task<IEnumerable<Municipio>> ListMinicipios()
        {
            return await _repositoryEmpresa.ListMinicipios();
        }

        public async Task<IEnumerable<BaseReceitaFederal>> ListTop10()
        {
            return await _repositoryEmpresa.ListTop10();
        }

        public async Task<IEnumerable<Empresas>> ListTop20()
        {
            return await _repositoryEmpresa.ListTop20();
        }

        public async Task<IEnumerable<Municipio>> MicroRegiaoJahu()
        {
            var q = await ListMinicipios();
            var t = q.Where(s => s.MicroRegiaoJahu(s));
            return t;
        }
    }
}
