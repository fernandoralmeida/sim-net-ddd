using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Domain.Shared.Service
{
    using Entity;
    using Domain.Service;
    using Interface;
    public class ServiceAtendimento : ServiceBase<Atendimento>, IServiceAtendimento
    {
        private readonly IRepositoryAtendimento _atendimento;
        public ServiceAtendimento(IRepositoryAtendimento repositoryAtendimento)
            :base(repositoryAtendimento)
        {
            _atendimento = repositoryAtendimento;
        }

        public IEnumerable<Atendimento> AtendimentoAtivo(string userid)
        {
            return _atendimento.AtendimentoAtivo(userid);
        }

        public IEnumerable<Atendimento> AtendimentosCancelados(string userid)
        {
            return _atendimento.AtendimentosCancelados(userid);
        }

        public Atendimento GetAtendimento(Guid id)
        {
            return _atendimento.GetAtendimento(id);
        }

        public IEnumerable<Atendimento> GetByCanal(string canal)
        {
            return _atendimento.GetByCanal(canal);
        }

        public IEnumerable<Atendimento> GetByDate(DateTime? dateTime)
        {
            return _atendimento.GetByDate(dateTime);
        }

        public IEnumerable<Atendimento> GetByEmpresa(string cnpj)
        {
            return _atendimento.GetByEmpresa(cnpj);
        }

        public IEnumerable<Atendimento> GetByPessoa(string cpf)
        {
            return _atendimento.GetByPessoa(cpf);
        }

        public IEnumerable<Atendimento> GetByServicos(string servicos)
        {
            return _atendimento.GetByServicos(servicos);
        }

        public IEnumerable<Atendimento> GetBySetor(string setor)
        {
            return _atendimento.GetBySetor(setor);
        }

        public IEnumerable<Atendimento> ListAll()
        {
            return _atendimento.ListAll();
        }

        public IEnumerable<Atendimento> ListByPeriodo(DateTime? dataI, DateTime? dataF)
        {
            return _atendimento.ListByPeriodo(dataI, dataF);
        }

        public IEnumerable<Atendimento> MeusAtendimentos(string userid, DateTime? date)
        {
            return _atendimento.MeusAtendimentos(userid, date);
        }

        public IEnumerable<Atendimento> MeusAtendimentosRae(string userid)
        {
            return _atendimento.MeusAtendimentosRae(userid);
        }

        public IEnumerable<Atendimento> ListarRaeLancados(IEnumerable<Atendimento> atendimentos)
        {
            return atendimentos.Where(a => a.RaeLancados(a)).OrderByDescending(o => o.Data);
        }

        public IEnumerable<Atendimento> ListarRaeNaoLancados(IEnumerable<Atendimento> atendimentos)
        {
            return atendimentos.Where(a => a.RaeNaoLancados(a));
        }

        public async Task<IEnumerable<Atendimento>> GetByUserName(string username)
        {
            return await _atendimento.GetByUserName(username);
        }

        public async Task<IEnumerable<Atendimento>> ListByMonth(DateTime? month)
        {
            return await _atendimento.ListByMonth(month);
        }

        public async Task<IEnumerable<Atendimento>> GetByUserNamePeriodo(string username, DateTime? date)
        {
            return await _atendimento.GetByUserNamePeriodo(username, date);
        }

        public async Task<IEnumerable<Atendimento>> ListAtendimentosAtivos()
        {
            return await _atendimento.ListAtendimentosAtivos();
        }

        public async Task<IEnumerable<Atendimento>> ListByParam(List<object> lparam)
        {
            return await _atendimento.ListByParam(lparam);
        }

        /** BI **/
        private readonly List<string> _meses = new();
        private readonly List<string> _pessoas_mes = new();
        private readonly List<string> _empresas_mes = new();
        private readonly List<string> _mes_servicos = new();
        private readonly List<string> _pessoas_mes_servicos = new();
        private readonly List<string> _empresas_mes_servicos = new();

        private void ConstruirMeses(Atendimento at_param, string[] serv_param)
        {
            _meses.Add(at_param.Data.Value.ToString("MMM"));

            if (at_param.Empresa != null)
                _empresas_mes.Add(at_param.Data.Value.ToString("MMM") + " Empresas");
            else
                _pessoas_mes.Add(at_param.Data.Value.ToString("MMM") + " Pessoas");

            foreach (string sv in serv_param.Where(s => s.Any()))
            {
                _mes_servicos.Add(at_param.Data.Value.ToString("MMM") + " Servicos");

                if (at_param.Empresa != null)
                    _empresas_mes_servicos.Add(at_param.Data.Value.Month + "Servicos");
                else
                    _pessoas_mes_servicos.Add(at_param.Data.Value.Month + "Servicos");
            }
        }

        private void AppUserMonth(Atendimento at_param, string[] serv_param)
        {
            _meses.Add(at_param.Owner_AppUser_Id);

            if (at_param.Empresa != null)
                _empresas_mes.Add(at_param.Owner_AppUser_Id + " Empresas");
            else
                _pessoas_mes.Add(at_param.Owner_AppUser_Id + " Pessoas");

            foreach (string sv in serv_param.Where(s => s.Any()))
            {
                _mes_servicos.Add(at_param.Owner_AppUser_Id + " Servicos");

                if (at_param.Empresa != null)
                    _empresas_mes_servicos.Add(at_param.Owner_AppUser_Id + "PJ Servicos");
                else
                    _pessoas_mes_servicos.Add(at_param.Owner_AppUser_Id + "PF Servicos");
            }
        }
        public async Task<BI.BiAtendimentos> BI_Atendimentos(DateTime periodo)
        {
            var d1 = new DateTime(periodo.Year, 01, 01);
            var d2 = new DateTime(periodo.Year, 12, 31);

            var list = _atendimento.ListByPeriodo(d1, d2);

            var r_all = new BI.BiAtendimentos();

            var t = Task.Run(() => {

                try
                {
                    _meses.Clear();
                    _pessoas_mes.Clear();
                    _empresas_mes.Clear();

                    _mes_servicos.Clear();
                    _pessoas_mes_servicos.Clear();
                    _empresas_mes_servicos.Clear();

                    var _meses_t = new List<(string Nome, string Atendimento, string Servico)>();

                    foreach (Atendimento at in list.Where(s => s.Servicos != null))
                    {
                        string[] servicos = at.Servicos.ToString().Split(new char[] { ';', ',' });
                        for (int i = 1; i < 13; i++)
                            if (at.Data.Value.Month == i)
                                ConstruirMeses(at, servicos);
                    }                  
           
                    r_all.Cliente = ("Clientes", _meses.Count, _mes_servicos.Count);
                    r_all.ClientePF = ("Pessoas", _pessoas_mes.Count, _pessoas_mes_servicos.Count);
                    r_all.ClientePJ = ("Empresas", _empresas_mes.Count, _empresas_mes_servicos.Count);

                    var mlist = new List<(string Mes, int Atendimentos, int Servicos)>();              

                    foreach (var x in from a in _meses
                                      group a by a into g
                                      let count = g.Count()                                      
                                      select new { Mes = g.Key, Atend = count })
                    {
                        mlist.Add((x.Mes, x.Atend, _mes_servicos.Where(s=>s.Contains(x.Mes)).Count()));
                    }

                    r_all.ListaMensal = mlist;

                    GC.Collect();

                }
                catch { }

            });
            await t;

            return r_all;
        }

        public async Task<BI.BiAtendimentos> BI_Atendimentos_Setor(DateTime periodo, string setor)
        {
            var list = _atendimento.GetBySetor(setor);

            var r_all = new BI.BiAtendimentos();

            var t = Task.Run(() => {

                try
                {
                    _meses.Clear();
                    _pessoas_mes.Clear();
                    _empresas_mes.Clear();

                    _mes_servicos.Clear();
                    _pessoas_mes_servicos.Clear();
                    _empresas_mes_servicos.Clear();

                    var _meses_t = new List<(string Nome, string Atendimento, string Servico)>();

                    foreach (Atendimento at in list.Where(s => s.Servicos != null && s.Data.Value.Year == periodo.Year))
                    {
                        string[] servicos = at.Servicos.ToString().Split(new char[] { ';', ',' });
                        for (int i = 1; i < 13; i++)
                            if (at.Data.Value.Month == i)
                                ConstruirMeses(at, servicos);
                    }

                    r_all.Cliente = (setor, _meses.Count, _mes_servicos.Count);
                    r_all.ClientePF = ("Pessoas", _pessoas_mes.Count, _pessoas_mes_servicos.Count);
                    r_all.ClientePJ = ("Empresas", _empresas_mes.Count, _empresas_mes_servicos.Count);

                    var mlist = new List<(string Mes, int Atendimentos, int Servicos)>();

                    foreach (var x in from a in _meses
                                      group a by a into g
                                      let count = g.Count()
                                      select new { Mes = g.Key, Atend = count })
                    {
                        mlist.Add((x.Mes, x.Atend, _mes_servicos.Where(s => s.Contains(x.Mes)).Count()));
                    }

                    r_all.ListaMensal = mlist;

                    GC.Collect();

                }
                catch { }

            });
            await t;

            return r_all;
        }

        public async Task<BI.BiAtendimentos> BI_Atendimentos_AppUser(DateTime periodo)
        {

            var list = _atendimento.ListByPeriodo(new DateTime(periodo.Year, 01, 01), new DateTime(periodo.Year, 12, 31));

            var r_all = new BI.BiAtendimentos();

            var t = Task.Run(() => {

                try
                {
                    _meses.Clear();
                    _pessoas_mes.Clear();
                    _empresas_mes.Clear();

                    _mes_servicos.Clear();
                    _pessoas_mes_servicos.Clear();
                    _empresas_mes_servicos.Clear();

                    var _meses_t = new List<(string Nome, string Atendimento, string Servico)>();

                    foreach (Atendimento at in list.Where(s => s.Servicos != null && s.Data.Value.Year == periodo.Year))
                    {
                        string[] servicos = at.Servicos.ToString().Split(new char[] { ';', ',' });
                        AppUserMonth(at, servicos);
                    }

                    var mlist = new List<(string Nome, int Atendimentos, int Servicos)>();

                    foreach (var x in from a in _meses
                                      group a by a into g
                                      let count = g.Count()
                                      orderby count descending
                                      select new { AppUser = g.Key, Atend = count })
                    {
                        mlist.Add((x.AppUser, x.Atend, _mes_servicos.Where(s => s.Contains(x.AppUser)).Count()));
                    }

                    r_all.ListaAppUser = mlist;

                    GC.Collect();

                }
                catch { }

            });
            await t;

            return r_all;
        }
    }
}
