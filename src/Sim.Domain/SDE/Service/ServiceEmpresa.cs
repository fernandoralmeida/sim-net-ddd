﻿using System;
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

        public async Task<IEnumerable<BiEmpresas>> BiEmpresasAsync(string municipio, string situacao)
        {

            var t = await _repositoryEmpresa.ListByMunicipioAsync(municipio);

            var r_empresas = new List<BiEmpresas>();

            try
            {

                var _situcao = new List<string>();
                var _emp = new List<string>();
                var _atv = new List<string>();
                var _srv = new List<string>();

                var bi_empresas = new BiEmpresas();

                int cnae = 0;

                foreach (BaseReceitaFederal at in t)
                {
                    if (at.Estabelecimento.SituacaoCadastral == situacao)
                    {

                        _atv.Add(string.Format("{0} - {1}", at.AtividadePrincipal.Codigo, at.AtividadePrincipal.Descricao));
                        _emp.Add("Empresas");

                        cnae = Convert.ToInt32(at.AtividadePrincipal.Codigo.Remove(2,5));

                        var subclasse = new CnaeSubclasse();

                        if (cnae >= 1 && cnae <= 3)
                            _srv.Add("Agropecuária");
                            
                        else if (cnae >= 45 && cnae <= 47)
                            _srv.Add("Comércio");

                        else if (cnae >= 05 & cnae <= 09 || cnae >= 10 && cnae <= 33)
                            _srv.Add("Indústria");

                        else if (cnae >= 41 & cnae <= 43)
                            _srv.Add("Construção");

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
                            _srv.Add("Serviços");
                    }

                    _situcao.Add(at.Estabelecimento.SituacaoCadastral);

                }
                
                bi_empresas.Empresas = new KeyValuePair<string, int>("Estatísticas de Empresas", _emp.Count);

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

                var c_srv = from x in _srv
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

                //Estrutura do CNAE com Seção, Divisao, Grupo, Classe e Subclasse
                /*
                var secao_A = new CnaeSecao();
                

                foreach (var a in bi_empresas.ListaAtividades)
                {

                    int s = Convert.ToInt32(a.Key.Remove(2, (a.Key.Length - 2)));

                    if (s >= 1 && s <= 3 )
                    {
                        var d = s;
                        if (d == 1)
                        {
                            var n_divisao = new CnaeDivisao();
                            var n_grupo = new CnaeGrupo();
                            var n_listgrupo = new List<CnaeGrupo>();
                            var n_classe = new CnaeClasse();
                            var n_listclasse = new List<CnaeClasse>();
                            var n_listsubclasse = new List<CnaeSubclasse>();
                            var g = a.Key.Remove(3, a.Key.Length - 3);
                            switch (g)
                            {
                                case "011":
                                    
                                    if(!n_grupo.Grupo.Key.Any())
                                        n_grupo.Grupo = new KeyValuePair<string, int>("01.1 Produção de lavouras temporárias", a.Value);

                                    var c = a.Key.Remove(5, a.Key.Length - 5);
                                    switch(c)
                                    {
                                        case "01113":
                                            
                                            if (!n_classe.Classe.Key.Any())
                                                n_classe.Classe = new KeyValuePair<string, int>("01.11-3 Cultivo de cereais", a.Value);

                                            var n_subclasse = new CnaeSubclasse();
                                            n_subclasse.Subclasse = new KeyValuePair<string, int>(a.Key, a.Value);
                                            n_listsubclasse.Add(n_subclasse);
                                            break;
                                    }
                                    
                                    break;
                                case "012":
                                    break;
                                case "013":
                                    break;
                                case "014":
                                    break;
                                case "015":
                                    break;
                                case "016":
                                    break;
                                case "017":
                                    break;
                            }
                        }
                    }
                }
                */


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
