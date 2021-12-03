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

        // ** FOR BI ** //
        public async Task<IEnumerable<BaseReceitaFederal>> ListForBIAsync(string municipio, string situacao, string ano, string mes)
        {
            return await _repositoryEmpresa.ListForBIAsync(municipio, situacao, ano, mes);
        }

        public async Task<IEnumerable<BiEmpresas>> BiEmpresasAsync(string municipio, string situacao, string ano, string mes)
        {

            var t = await _repositoryEmpresa.ListByMunicipioAsync(municipio);

            var r_empresas = new List<BiEmpresas>();

            var tks = Task.Run(() => {

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
                    var _var_mes = new List<string>();

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

                        if (at.AtividadePrincipal.Codigo != "9492800") //Excluir Servioços políticos
                        {
                            string[] data = at.Estabelecimento.DataInicioAtividade.ToString().Split(new char[] { '-' });

                            if (mes == "99")
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

                            else if (mes == "00")
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

                                        switch (data[1])
                                        {
                                            case "01":
                                                _var_mes.Add("01Janeiro");
                                                break;
                                            case "02":
                                                _var_mes.Add("02Fevereiro");
                                                break;
                                            case "03":
                                                _var_mes.Add("03Março");
                                                break;
                                            case "04":
                                                _var_mes.Add("04Abril");
                                                break;
                                            case "05":
                                                _var_mes.Add("05Maio");
                                                break;
                                            case "06":
                                                _var_mes.Add("06Junho");
                                                break;
                                            case "07":
                                                _var_mes.Add("07Julho");
                                                break;
                                            case "08":
                                                _var_mes.Add("08Agosto");
                                                break;
                                            case "09":
                                                _var_mes.Add("09Setembro");
                                                break;
                                            case "10":
                                                _var_mes.Add("10Outubro");
                                                break;
                                            case "11":
                                                _var_mes.Add("11Novembro");
                                                break;
                                            case "12":
                                                _var_mes.Add("12Dezembro");
                                                break;
                                        }

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
                    }

                    bi_empresas.EmpresasAtivas = new KeyValuePair<string, int>("Estatísticas", _emp.Count);
                    bi_empresas.TotalEmpresas = new KeyValuePair<string, int>("Total Empresas", _temp.Count);
                    bi_empresas.Formalizacoes = new KeyValuePair<string, int>("Empresas Formalizadas", _formalizacao.Count);
                    bi_empresas.Baixas = new KeyValuePair<string, int>("Empresas Baixadas", _baixada.Count);
                    bi_empresas.SimplesNacional = new KeyValuePair<string, int>("Simples Nacional", _osn.Count);
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

                    // Varicao Mes a Mes
                    var c_var_mes = from x in _var_mes
                                    group x by x into g
                                    let count = g.Count()
                                    orderby g.Key ascending
                                    select new { Value = g.Key.Remove(0, 2), Count = count };

                    var l_var_mes = new List<KeyValuePair<string, int>>();
                    foreach (var x in c_var_mes)
                    {
                        l_var_mes.Add(new KeyValuePair<string, int>(x.Value, x.Count));
                    }
                    bi_empresas.ListaMensal = l_var_mes;

                    r_empresas.Add(bi_empresas);

                }
                catch { }

            });

            await tks;

            return r_empresas;

        }

        public async Task<IEnumerable<BiCnae>> ListBICnae(string municipio)
        {

            var l_full_cnae = new List<BiCnae>();

            var secao = new KeyValuePair<string, int>();

            var l_secao = new BiCnae() { ListaSecao = new() };

            var emp = await _repositoryEmpresa.ListForBICnaeAsync(municipio);            

            var t = Task.Run(() => {

                var subclasse = new List<string>();

                foreach (BaseReceitaFederal e in emp)
                {
                    if(e.Estabelecimento.SituacaoCadastral == "Ativa")
                        subclasse.Add(string.Format("{0} - {1}", e.AtividadePrincipal.Codigo, e.AtividadePrincipal.Descricao));
                }

                var s_subclasse = from x in subclasse
                                group x by x into g
                                let count = g.Count()
                                orderby g.Key ascending
                                select new { Value = g.Key, Count = count };

                var sec_count = 0;
                var sub_sec_count = 0;

                //agro
                var agro = new CnaeSecao() { ListaClasse = new(), Secao = new() };
                for (int i = 1;i <= 3; i++)
                {                    
                    var n_cnae = new CnaeClasse() { ListaSubClasse = new(), Classe = new() };
                    sec_count = 0;
                    sub_sec_count = 0;
                    foreach (var s in s_subclasse)
                    {
                        string[] cd = s.Value.Split(" - ");

                        int sec = Convert.ToInt32(cd[0].Remove(2, 5));
                        
                        if (sec >= 01 && sec <= 03)
                        {
                            sec_count += s.Count;
                            secao = new KeyValuePair<string, int>("01...03 AGRICULTURA, PECUÁRIA, PRODUÇÃO FLORESTAL, PESCA E AQÜICULTURA", sec_count);
                            
                            if (i == 01 && sec == 01)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("01 AGRICULTURA, PECUÁRIA E SERVIÇOS RELACIONADOS", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            if (i == 02 && sec == 02)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("02 PRODUÇÃO FLORESTAL", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            if (i == 03 && sec == 03)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("03 PESCA E AQÜICULTURA", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                        }                        
                    }
                    if(n_cnae.ListaSubClasse.Any())
                    {
                        agro.ListaClasse.Add(n_cnae);
                        agro.Secao = secao;
                    }                    
                }
                if(agro.ListaClasse.Any())
                    l_secao.ListaSecao.Add(agro);

                //Ind Ext
                var indextrativa = new CnaeSecao() { ListaClasse = new(), Secao = new() };
                for (int i = 5; i <= 9; i++)
                {
                    var n_cnae = new CnaeClasse() { ListaSubClasse = new(), Classe = new() };
                    sec_count = 0;
                    sub_sec_count = 0;
                    foreach (var s in s_subclasse)
                    {
                        string[] cd = s.Value.Split(" - ");

                        int sec = Convert.ToInt32(cd[0].Remove(2, 5));

                        if (sec >= 05 && sec <= 09)
                        {
                            sec_count += s.Count;
                            secao = new KeyValuePair<string, int>("05...09 INDÚSTRIAS EXTRATIVAS", sec_count);

                            if (i == 05 && sec == 05)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("05 EXTRAÇÃO DE CARVÃO MINERAL", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            else if (i == 06 && sec == 06)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("06 EXTRAÇÃO DE PETRÓLEO E GÁS NATURAL", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            else if (i == 07 && sec == 07)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("07 EXTRAÇÃO DE MINERAIS METÁLICOS", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            else if (i == 08 && sec == 08)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("08 EXTRAÇÃO DE MINERAIS NÃO-METÁLICOS", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            else if (i == 09 && sec == 09)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("09 ATIVIDADES DE APOIO À EXTRAÇÃO DE MINERAIS", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                        }
                    }
                    if(n_cnae.ListaSubClasse.Any())
                    {
                        indextrativa.ListaClasse.Add(n_cnae);
                        indextrativa.Secao = secao;
                    }                  
                }
                if(indextrativa.ListaClasse.Any())
                    l_secao.ListaSecao.Add(indextrativa);

                //Ind Transf
                var indetransf = new CnaeSecao() { ListaClasse = new(), Secao = new() };
                for (int i = 10; i <= 33; i++)
                {
                    var n_cnae = new CnaeClasse() { ListaSubClasse = new(), Classe = new() };
                    sec_count = 0;
                    sub_sec_count = 0;
                    foreach (var s in s_subclasse)
                    {
                        string[] cd = s.Value.Split(" - ");

                        int sec = Convert.ToInt32(cd[0].Remove(2, 5));

                        if (sec >= 10 && sec <= 33)
                        {
                            sec_count += s.Count;
                            secao = new KeyValuePair<string, int>("10...33 INDÚSTRIAS DE TRANSFORMAÇÃO", sec_count);

                            if (i == 10 && sec == 10)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("10 FABRICAÇÃO DE PRODUTOS ALIMENTÍCIOS", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            else if (i == 11 && sec == 11)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("11 FABRICAÇÃO DE BEBIDAS", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            else if (i == 12 && sec == 12)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("12 FABRICAÇÃO DE PRODUTOS DO FUMO", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            else if (i == 13 && sec == 13)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("13 FABRICAÇÃO DE PRODUTOS TÊXTEIS", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            else if (i == 14 && sec == 14)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("14 CONFECÇÃO DE ARTIGOS DO VESTUÁRIO E ACESSÓRIOS", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            else if (i == 15 && sec == 15)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("15 PREPARAÇÃO DE COUROS E FABRICAÇÃO DE ARTEFATOS DE COURO, ARTIGOS PARA VIAGEM E CALÇADOS", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            else if (i == 16 && sec == 16)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("16 FABRICAÇÃO DE PRODUTOS DE MADEIRA", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            else if (i == 17 && sec == 17)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("17 FABRICAÇÃO DE CELULOSE, PAPEL E PRODUTOS DE PAPEL", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            else if (i == 18 && sec == 18)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("18 IMPRESSÃO E REPRODUÇÃO DE GRAVAÇÕES", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            else if (i == 19 && sec == 19)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("19 FABRICAÇÃO DE COQUE, DE PRODUTOS DERIVADOS DO PETRÓLEO E DE BIOCOMBUSTÍVEIS", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            else if (i == 20 && sec == 20)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("20 FABRICAÇÃO DE PRODUTOS QUÍMICOS", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            else if (i == 21 && sec == 21)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("21 FABRICAÇÃO DE PRODUTOS FARMOQUÍMICOS E FARMACÊUTICOS", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            else if (i == 22 && sec == 22)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("22 FABRICAÇÃO DE PRODUTOS DE BORRACHA E DE MATERIAL PLÁSTICO", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            else if (i == 23 && sec == 23)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("23 FABRICAÇÃO DE PRODUTOS DE MINERAIS NÃO-METÁLICOS", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            else if (i == 24 && sec == 24)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("24 METALURGIA", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            else if (i == 25 && sec == 25)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("25 FABRICAÇÃO DE PRODUTOS DE METAL, EXCETO MÁQUINAS E EQUIPAMENTOS", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            else if (i == 26 && sec == 26)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("26 FABRICAÇÃO DE EQUIPAMENTOS DE INFORMÁTICA, PRODUTOS ELETRÔNICOS E ÓPTICOS", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            else if (i == 27 && sec == 27)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("27 FABRICAÇÃO DE MÁQUINAS, APARELHOS E MATERIAIS ELÉTRICOS", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            else if (i == 28 && sec == 28)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("28 FABRICAÇÃO DE MÁQUINAS E EQUIPAMENTOS", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            else if (i == 29 && sec == 29)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("29 FABRICAÇÃO DE VEÍCULOS AUTOMOTORES, REBOQUES E CARROCERIAS", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            else if (i == 30 && sec == 30)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("30 FABRICAÇÃO DE OUTROS EQUIPAMENTOS DE TRANSPORTE, EXCETO VEÍCULOS AUTOMOTORES", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            else if (i == 31 && sec == 31)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("31 FABRICAÇÃO DE MÓVEIS", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            else if (i == 32 && sec == 32)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("32 FABRICAÇÃO DE PRODUTOS DIVERSOS", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            else if (i == 33 && sec == 33)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("33 MANUTENÇÃO, REPARAÇÃO E INSTALAÇÃO DE MÁQUINAS E EQUIPAMENTOS", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                        }
                    }
                    if (n_cnae.ListaSubClasse.Any())
                    {
                        indetransf.ListaClasse.Add(n_cnae);
                        indetransf.Secao = secao;
                    }
                }
                if (indetransf.ListaClasse.Any())
                    l_secao.ListaSecao.Add(indetransf);



                l_full_cnae.Add(l_secao);
            });
            
            await t;

            return l_full_cnae;
        }

        public async Task<IEnumerable<BaseReceitaFederal>> ListForBICnaeAsync(string municipio)
        {
            return await _repositoryEmpresa.ListForBICnaeAsync(municipio);
        }
    }
}
