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
    using Sim.Domain.BI;

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

        public async Task<IEnumerable<BiEmpresas>> BiEmpresasAsync(string municipio, string situacao, string ano, string mes)
        {

            var t = await _repositoryEmpresa.ListByMunicipioAsync(municipio);

            var r_empresas = new List<BiEmpresas>();

            try
            {

                var _situcao = new List<string>();
                var _emp = new List<string>();
                var _atv = new List<string>();
                var _setores = new List<string>();
                var _temp = new List<string>();
                var _porte = new List<string>();
                var _osn = new List<string>();
                var _mei = new List<string>();

                var _servico = new List<string>();
                var _comercio = new List<string>();
                var _industria = new List<string>();
                var _agro = new List<string>();
                var _construcao = new List<string>();

                var _formalizacao = new List<string>();
                var _baixada = new List<string>();

                var bi_empresas = new BiEmpresas();

                int cnae = 0;

                foreach (BaseReceitaFederal at in t)
                {
                    string[] data = at.Estabelecimento.DataInicioAtividade.ToString().Split(new char[] { '-' });


                    if(mes == "99")
                    {

                        if (data[0] == ano)
                        {
                            if (at.Estabelecimento.SituacaoCadastral == "Baixada")
                                _baixada.Add("B");
                            else if (at.Estabelecimento.SituacaoCadastral == "Ativa")
                                _formalizacao.Add("F");
                        }

                        if (at.Estabelecimento.SituacaoCadastral == situacao)
                        {

                            _porte.Add(at.Empresa.PorteEmpresa);

                            if (at.SimplesNacional != null)
                            {
                                switch(at.SimplesNacional.OpcaoSimples)
                                {
                                    case "Sim":
                                        _osn.Add(at.SimplesNacional.OpcaoSimples);
                                        break;
                                }
                                switch (at.SimplesNacional.OpcaoMEI)
                                {
                                    case "Sim":
                                        _mei.Add(at.SimplesNacional.OpcaoMEI);
                                        break;
                                }
                            }

                            _atv.Add(string.Format("{0} - {1}", at.AtividadePrincipal.Codigo, at.AtividadePrincipal.Descricao));
                            _emp.Add("E");

                            cnae = Convert.ToInt32(at.AtividadePrincipal.Codigo.Remove(2, 5));

                            if (cnae >= 1 && cnae <= 3)
                            {
                                _setores.Add("Agropecuária");
                                _agro.Add(string.Format("{0} - {1}", at.AtividadePrincipal.Codigo, at.AtividadePrincipal.Descricao));
                            }

                            else if (cnae >= 45 && cnae <= 47)
                            {
                                _setores.Add("Comércio");
                                _comercio.Add(string.Format("{0} - {1}", at.AtividadePrincipal.Codigo, at.AtividadePrincipal.Descricao));
                            }
                            else if (cnae >= 05 & cnae <= 09 || cnae >= 10 && cnae <= 33)
                            {
                                _setores.Add("Indústria");
                                _industria.Add(string.Format("{0} - {1}", at.AtividadePrincipal.Codigo, at.AtividadePrincipal.Descricao));
                            }
                            else if (cnae >= 41 & cnae <= 43)
                            {
                                _setores.Add("Construção");
                                _construcao.Add(string.Format("{0} - {1}", at.AtividadePrincipal.Codigo, at.AtividadePrincipal.Descricao));
                            }
                            else if (cnae == 35 || (cnae >= 36 && cnae <= 39)
                                || (cnae >= 49 && cnae <= 53)
                                || (cnae >= 55 && cnae <= 56)
                                || (cnae >= 58 && cnae <= 63)
                                || (cnae >= 64 && cnae <= 66)
                                || (cnae == 68)
                                || (cnae >= 69 && cnae <= 75)
                                || (cnae >= 77 && cnae <= 82)
                                || (cnae == 85)
                                || (cnae >= 86 && cnae <= 88)
                                || (cnae >= 86 && cnae <= 88)
                                || (cnae >= 90 && cnae <= 93)
                                || (cnae >= 94 && cnae <= 96)
                                || (cnae == 97)
                                || (cnae == 99))
                            {
                                _setores.Add("Serviços");
                                _servico.Add(string.Format("{0} - {1}", at.AtividadePrincipal.Codigo, at.AtividadePrincipal.Descricao));
                            }
                        }

                        _temp.Add("TE");
                        _situcao.Add(at.Estabelecimento.SituacaoCadastral);


                    }

                    else if(mes == "00" )
                    {

                        if (data[0] == ano)
                        {
                            if (at.Estabelecimento.SituacaoCadastral == "Baixada")
                                _baixada.Add("B");
                            else if (at.Estabelecimento.SituacaoCadastral == "Ativa")
                                _formalizacao.Add("F");


                            if (at.Estabelecimento.SituacaoCadastral == situacao)
                            {
                                _porte.Add(at.Empresa.PorteEmpresa);

                                if (at.SimplesNacional != null)
                                {
                                    switch (at.SimplesNacional.OpcaoSimples)
                                    {
                                        case "Sim":
                                            _osn.Add(at.SimplesNacional.OpcaoSimples);
                                            break;
                                    }
                                    switch (at.SimplesNacional.OpcaoMEI)
                                    {
                                        case "Sim":
                                            _mei.Add(at.SimplesNacional.OpcaoMEI);
                                            break;
                                    }
                                }

                                _atv.Add(string.Format("{0} - {1}", at.AtividadePrincipal.Codigo, at.AtividadePrincipal.Descricao));
                                _emp.Add("E");

                                cnae = Convert.ToInt32(at.AtividadePrincipal.Codigo.Remove(2, 5));

                                if (cnae >= 1 && cnae <= 3)
                                {
                                    _setores.Add("Agropecuária");
                                    _agro.Add(string.Format("{0} - {1}", at.AtividadePrincipal.Codigo, at.AtividadePrincipal.Descricao));
                                }

                                else if (cnae >= 45 && cnae <= 47)
                                {
                                    _setores.Add("Comércio");
                                    _comercio.Add(string.Format("{0} - {1}", at.AtividadePrincipal.Codigo, at.AtividadePrincipal.Descricao));
                                }
                                else if (cnae >= 05 & cnae <= 09 || cnae >= 10 && cnae <= 33)
                                {
                                    _setores.Add("Indústria");
                                    _industria.Add(string.Format("{0} - {1}", at.AtividadePrincipal.Codigo, at.AtividadePrincipal.Descricao));
                                }
                                else if (cnae >= 41 & cnae <= 43)
                                {
                                    _setores.Add("Construção");
                                    _construcao.Add(string.Format("{0} - {1}", at.AtividadePrincipal.Codigo, at.AtividadePrincipal.Descricao));
                                }
                                else if (cnae == 35 || (cnae >= 36 && cnae <= 39)
                                    || (cnae >= 49 && cnae <= 53)
                                    || (cnae >= 55 && cnae <= 56)
                                    || (cnae >= 58 && cnae <= 63)
                                    || (cnae >= 64 && cnae <= 66)
                                    || (cnae == 68)
                                    || (cnae >= 69 && cnae <= 75)
                                    || (cnae >= 77 && cnae <= 82)
                                    || (cnae == 85)
                                    || (cnae >= 86 && cnae <= 88)
                                    || (cnae >= 86 && cnae <= 88)
                                    || (cnae >= 90 && cnae <= 93)
                                    || (cnae >= 94 && cnae <= 96)
                                    || (cnae == 97)
                                    || (cnae == 99))
                                {
                                    _setores.Add("Serviços");
                                    _servico.Add(string.Format("{0} - {1}", at.AtividadePrincipal.Codigo, at.AtividadePrincipal.Descricao));
                                }
                            }

                            _temp.Add("TE");
                            _situcao.Add(at.Estabelecimento.SituacaoCadastral);
                        }
                    }

                    else
                    {

                        if (data[0] == ano && data[1] == mes)
                        {
                            if (at.Estabelecimento.SituacaoCadastral == "Baixada")
                                _baixada.Add("B");
                            else if (at.Estabelecimento.SituacaoCadastral == "Ativa")
                                _formalizacao.Add("F");

                            if (at.Estabelecimento.SituacaoCadastral == situacao)
                            {
                                _porte.Add(at.Empresa.PorteEmpresa);

                                if (at.SimplesNacional != null)
                                {
                                    switch (at.SimplesNacional.OpcaoSimples)
                                    {
                                        case "Sim":
                                            _osn.Add(at.SimplesNacional.OpcaoSimples);
                                            break;
                                    }
                                    switch (at.SimplesNacional.OpcaoMEI)
                                    {
                                        case "Sim":
                                            _mei.Add(at.SimplesNacional.OpcaoMEI);
                                            break;
                                    }
                                }

                                _atv.Add(string.Format("{0} - {1}", at.AtividadePrincipal.Codigo, at.AtividadePrincipal.Descricao));
                                _emp.Add("E");

                                cnae = Convert.ToInt32(at.AtividadePrincipal.Codigo.Remove(2, 5));

                                if (cnae >= 1 && cnae <= 3)
                                {
                                    _setores.Add("Agropecuária");
                                    _agro.Add(string.Format("{0} - {1}", at.AtividadePrincipal.Codigo, at.AtividadePrincipal.Descricao));
                                }

                                else if (cnae >= 45 && cnae <= 47)
                                {
                                    _setores.Add("Comércio");
                                    _comercio.Add(string.Format("{0} - {1}", at.AtividadePrincipal.Codigo, at.AtividadePrincipal.Descricao));
                                }
                                else if (cnae >= 05 & cnae <= 09 || cnae >= 10 && cnae <= 33)
                                {
                                    _setores.Add("Indústria");
                                    _industria.Add(string.Format("{0} - {1}", at.AtividadePrincipal.Codigo, at.AtividadePrincipal.Descricao));
                                }
                                else if (cnae >= 41 & cnae <= 43)
                                {
                                    _setores.Add("Construção");
                                    _construcao.Add(string.Format("{0} - {1}", at.AtividadePrincipal.Codigo, at.AtividadePrincipal.Descricao));
                                }
                                else if (cnae == 35 || (cnae >= 36 && cnae <= 39)
                                    || (cnae >= 49 && cnae <= 53)
                                    || (cnae >= 55 && cnae <= 56)
                                    || (cnae >= 58 && cnae <= 63)
                                    || (cnae >= 64 && cnae <= 66)
                                    || (cnae == 68)
                                    || (cnae >= 69 && cnae <= 75)
                                    || (cnae >= 77 && cnae <= 82)
                                    || (cnae == 85)
                                    || (cnae >= 86 && cnae <= 88)
                                    || (cnae >= 86 && cnae <= 88)
                                    || (cnae >= 90 && cnae <= 93)
                                    || (cnae >= 94 && cnae <= 96)
                                    || (cnae == 97)
                                    || (cnae == 99))
                                {
                                    _setores.Add("Serviços");
                                    _servico.Add(string.Format("{0} - {1}", at.AtividadePrincipal.Codigo, at.AtividadePrincipal.Descricao));
                                }
                            }

                            _temp.Add("TE");
                            _situcao.Add(at.Estabelecimento.SituacaoCadastral);
                        }
                    }

                }
                
                bi_empresas.EmpresasAtivas = new KeyValuePair<string, int>("Estatísticas", _emp.Count);
                bi_empresas.TotalEmpresas = new KeyValuePair<string, int>("Total Empresas", _temp.Count);
                bi_empresas.Formalizacoes = new KeyValuePair<string, int>("Empresas Formalizadas", _formalizacao.Count);
                bi_empresas.Baixas = new KeyValuePair<string, int>("Empresas Baixadas", _baixada.Count);
                bi_empresas.SimplesNacional = new KeyValuePair<string, int>("Optante Simples Nacional", _osn.Count);
                bi_empresas.OptanteMEI = new KeyValuePair<string, int>("Optante MEI", _mei.Count);

                var c_atv = from x in _atv
                             group x by x into g
                             let count = g.Count()
                             orderby count descending
                             select new { Value = g.Key, Count = count };

                var n_atv = new List<KeyValuePair<string, int>>();

                foreach (var x in c_atv)
                {
                    n_atv.Add(new KeyValuePair<string, int>(x.Value, x.Count));                    
                }
                
                bi_empresas.ListaAtividades = n_atv;                

                var c_stc = from x in _situcao
                            group x by x into g
                            let count = g.Count()
                            orderby count descending
                            select new { Value = g.Key, Count = count };

                var n_stc = new List<KeyValuePair<string, int>>();
                foreach (var x in c_stc)
                {
                    n_stc.Add(new KeyValuePair<string, int>(x.Value, x.Count));
                }

                bi_empresas.ListaSituacao = n_stc;

                var c_srv = from x in _setores
                            group x by x into g
                            let count = g.Count()
                            orderby count descending
                            select new { Value = g.Key, Count = count };

                var n_srv = new List<KeyValuePair<string, int>>();
                foreach (var x in c_srv)
                {
                    n_srv.Add(new KeyValuePair<string, int>(x.Value, x.Count));
                }
                bi_empresas.ListaSetores = n_srv;


                // Atividades Serviços
                var c_servico = from x in _servico
                            group x by x into g
                            let count = g.Count()
                            orderby count descending
                            select new { Value = g.Key, Count = count };

                var l_servico = new List<KeyValuePair<string, int>>();
                foreach (var x in c_servico)
                {
                    l_servico.Add(new KeyValuePair<string, int>(x.Value, x.Count));
                }
                bi_empresas.Servicos = l_servico;

                // Atividades Comércio
                var c_comercio = from x in _comercio
                                group x by x into g
                                let count = g.Count()
                                orderby count descending
                                select new { Value = g.Key, Count = count };

                var l_comercio = new List<KeyValuePair<string, int>>();
                foreach (var x in c_comercio)
                {
                    l_comercio.Add(new KeyValuePair<string, int>(x.Value, x.Count));
                }
                bi_empresas.Comercio = l_comercio;

                // Atividades Industria
                var c_industria = from x in _industria
                                 group x by x into g
                                 let count = g.Count()
                                 orderby count descending
                                 select new { Value = g.Key, Count = count };

                var l_industria = new List<KeyValuePair<string, int>>();
                foreach (var x in c_industria)
                {
                    l_industria.Add(new KeyValuePair<string, int>(x.Value, x.Count));
                }
                bi_empresas.Indistria = l_industria;

                // Atividades Industria
                var c_agro = from x in _agro
                                  group x by x into g
                                  let count = g.Count()
                                  orderby count descending
                                  select new { Value = g.Key, Count = count };

                var l_agro = new List<KeyValuePair<string, int>>();
                foreach (var x in c_agro)
                {
                    l_agro.Add(new KeyValuePair<string, int>(x.Value, x.Count));
                }
                bi_empresas.Agro = l_agro;

                // Atividades Construção
                var c_construcao = from x in _construcao
                             group x by x into g
                             let count = g.Count()
                             orderby count descending
                             select new { Value = g.Key, Count = count };

                var l_constucao = new List<KeyValuePair<string, int>>();
                foreach (var x in c_construcao)
                {
                    l_constucao.Add(new KeyValuePair<string, int>(x.Value, x.Count));
                }
                bi_empresas.Construcao = l_constucao;

                // Porte
                var c_porte = from x in _porte
                                   group x by x into g
                                   let count = g.Count()
                                   orderby count descending
                                   select new { Value = g.Key, Count = count };

                var l_porte = new List<KeyValuePair<string, int>>();
                foreach (var x in c_porte)
                {
                    l_porte.Add(new KeyValuePair<string, int>(x.Value, x.Count));
                }
                bi_empresas.Porte = l_porte;

                r_empresas.Add(bi_empresas);

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
