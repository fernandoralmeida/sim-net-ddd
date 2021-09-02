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
            return atendimentos.Where(a => a.RaeLancados(a));
        }

        public IEnumerable<Atendimento> ListarRaeNaoLancados(IEnumerable<Atendimento> atendimentos)
        {
            return atendimentos.Where(a => a.RaeNaoLancados(a));
        }

        public async Task<IEnumerable<Atendimento>> GetByUserName(string username)
        {
            return await _atendimento.GetByUserName(username);
        }

        public async Task<IEnumerable<KeyValuePair<string, int>>> BySetor(string setor)
        {
            var list = new List<Atendimento>();

            if (setor == "Sala do Empreendedor")
            {
                var se =  _atendimento.GetBySetor("Sala do Empreendedor");

                var ee = _atendimento.GetBySetor("Espaço do Empreendedor");
                               

                foreach (Atendimento at1 in se)
                {
                    list.Add(at1);
                }

                foreach (Atendimento at2 in ee)
                {
                    list.Add(at2);
                }
            }
            else
            {
                var st = _atendimento.GetBySetor(setor);

                foreach (Atendimento at3 in st)
                {
                    list.Add(at3);
                }
            }

            var r_setores = new List<KeyValuePair<string, int>>();

            var t = Task.Run(() => {

                try
                {
                    var _servico = new List<string>();
                    var _canal = new List<string>();
                    var _operador = new List<string>();
                    var _setor = new List<string>();

                    foreach (Atendimento at in list)
                    {
                        if (at.Setor == "Espaço do Empreendedor")
                            at.Setor = "Sala do Empreendedor";

                                if (at.Servicos != null)
                                {
                                    string[] words = at.Servicos.ToString().Split(new char[] { ';', ',' });

                                    foreach (string sv in words)
                                    {
                                        if (sv != null && sv != string.Empty)
                                            _servico.Add("Servico-" + sv);
                                    }
                                }

                                if (at.Canal != null)
                                    _canal.Add("Canal-" + at.Canal);

                                _setor.Add(at.Setor);

                                _operador.Add("User-" + at.Owner_AppUser_Id);  
                        
                    }

                    var c_setor = from x in _setor
                                  group x by x into g
                                  let count = g.Count()
                                  orderby count descending
                                  select new { Value = g.Key, Count = count };

                    foreach (var x in c_setor)
                    {
                        r_setores.Add(new KeyValuePair<string, int>(x.Value, x.Count));
                    }
                                        
                    var c_servicos = from x in _servico
                                        group x by x into g
                                        let count = g.Count()
                                        orderby count descending
                                        select new { Value = g.Key, Count = count };

                    foreach (var x in c_servicos)
                    {
                        r_setores.Add(new KeyValuePair<string, int>(x.Value, x.Count));
                    }

                    r_setores.Add(new KeyValuePair<string, int>("Servico-Total de Serviços", _servico.Count()));

                    var c_canais = from x in _canal
                                     group x by x into g
                                     let count = g.Count()
                                     orderby count descending
                                     select new { Value = g.Key, Count = count };

                    foreach (var x in c_canais)
                    {
                        r_setores.Add(new KeyValuePair<string, int>(x.Value, x.Count));
                    }

                    var c_operador = from x in _operador
                                     group x by x into g
                                     let count = g.Count()
                                     orderby count descending
                                     select new { Value = g.Key, Count = count };

                    foreach (var x in c_operador)
                    {
                        r_setores.Add(new KeyValuePair<string, int>(x.Value, x.Count));
                    }

                    
                }
                catch { }

            });
            await t;

            return r_setores;
        }

        public async Task<IEnumerable<KeyValuePair<string, int>>> ByAll()
        {
            var list = _atendimento.ListAll();
            
            var r_all = new List<KeyValuePair<string, int>>();

            var t = Task.Run(() => {

                try
                {
                    var _servico = new List<string>();
                    var _canal = new List<string>();
                    var _operador = new List<string>();
                    var _setor = new List<string>();

                    foreach (Atendimento at in list)
                    {
                        if (at.Setor == "Espaço do Empreendedor")
                            at.Setor = "Sala do Empreendedor";

                        if (at.Servicos != null)
                        {
                            string[] words = at.Servicos.ToString().Split(new char[] { ';', ',' });

                            foreach (string sv in words)
                            {
                                if (sv != null && sv != string.Empty)
                                    _servico.Add("Servico-" + sv);
                            }
                        }

                        if (at.Canal != null)
                            _canal.Add("Canal-" + at.Canal);

                        _setor.Add("Geral");

                        _operador.Add("User-" + at.Owner_AppUser_Id);

                    }

                    var c_setor = from x in _setor
                                  group x by x into g
                                  let count = g.Count()
                                  orderby count descending
                                  select new { Value = g.Key, Count = count };

                    foreach (var x in c_setor)
                    {
                        r_all.Add(new KeyValuePair<string, int>(x.Value, x.Count));
                    }

                    var c_servicos = from x in _servico
                                     group x by x into g
                                     let count = g.Count()
                                     orderby count descending
                                     select new { Value = g.Key, Count = count };

                    foreach (var x in c_servicos)
                    {
                        r_all.Add(new KeyValuePair<string, int>(x.Value, x.Count));
                    }

                    r_all.Add(new KeyValuePair<string, int>("Servico-Total de Serviços", _servico.Count()));

                    var c_canais = from x in _canal
                                   group x by x into g
                                   let count = g.Count()
                                   orderby count descending
                                   select new { Value = g.Key, Count = count };

                    foreach (var x in c_canais)
                    {
                        r_all.Add(new KeyValuePair<string, int>(x.Value, x.Count));
                    }

                    var c_operador = from x in _operador
                                     group x by x into g
                                     let count = g.Count()
                                     orderby count descending
                                     select new { Value = g.Key, Count = count };

                    foreach (var x in c_operador)
                    {
                        r_all.Add(new KeyValuePair<string, int>(x.Value, x.Count));
                    }


                }
                catch { }

            });
            await t;

            return r_all;
        }

        public async Task<IEnumerable<KeyValuePair<string, int>>> ByUserName(string username)
        {
            var t = Task.Run(() => _atendimento.GetByUserName(username));
            await t;
            var r_user = new List<KeyValuePair<string, int>>();
            try
            {
                //List<string> _ano = new List<string>();
                var _atendimento = new List<string>();
                var _servico = new List<string>();

                foreach (Atendimento at in t.Result)
                {
                    _atendimento.Add(at.Owner_AppUser_Id);

                    //_ano.Add(at.Data.Value.Year.ToString());

                    if (at.Servicos != null)
                    {
                        string[] words = at.Servicos.ToString().Split(new char[] { ';', ',' });

                        foreach (string sv in words)
                        {
                            if (sv != null && sv != string.Empty)
                                _servico.Add(sv);
                        }
                    }

                }

                r_user.Add(new KeyValuePair<string, int>(username, _atendimento.Count()));

                var c_servico = from x in _servico
                             group x by x into g
                             let count = g.Count()
                             orderby count descending
                             select new { Value = g.Key, Count = count };

                foreach (var x in c_servico)
                {
                    r_user.Add(new KeyValuePair<string, int>(x.Value, x.Count));
                }

                r_user.Add(new KeyValuePair<string, int>("Total de Serviços", _servico.Count()));

            }
            catch { }

            return r_user;
        }

        public async Task<IEnumerable<KeyValuePair<string, int>>> ByServicos(string servico)
        {
            var t = Task.Run(() => _atendimento.GetByServicos(servico));
            await t;
            var r_servico = new List<KeyValuePair<string, int>>();
            try
            {
                //List<string> _ano = new List<string>();
                //var _canal = new List<string>();
                var _user = new List<string>();

                foreach (Atendimento at in t.Result)
                {
                    _user.Add(at.Owner_AppUser_Id);

                    //_ano.Add(at.Data.Value.Year.ToString());
                }

                r_servico.Add(new KeyValuePair<string, int>(servico, t.Result.Count()));

                var c_user = from x in _user
                             group x by x into g
                                let count = g.Count()
                                orderby count descending
                                select new { Value = g.Key, Count = count };

                foreach (var x in c_user)
                {
                    r_servico.Add(new KeyValuePair<string, int>(x.Value, x.Count));
                }

            }
            catch { }

            return r_servico;
        }

        public async Task<IEnumerable<KeyValuePair<string, int>>> ByCanal(string canal, string setor)
        {
            
            var t = Task.Run(() => _atendimento.GetByCanal(canal));
            await t;
            var r_servico = new List<KeyValuePair<string, int>>();
            try
            {

                var _servico = new List<string>();

                foreach (Atendimento at in t.Result)
                {
                    if (at.Setor == "Espaço do Empreendedor")
                        at.Setor = "Sala do Empreendedor";

                    if (at.Setor == setor || setor == "Geral")
                        if (at.Servicos != null)
                        {
                            string[] words = at.Servicos.ToString().Split(new char[] { ';', ',' });

                            foreach (string sv in words)
                            {
                                if (sv != null && sv != string.Empty)
                                    _servico.Add(sv);
                            }
                        }
                }
                                
                var c_servico = from x in _servico
                                group x by x into g
                              let count = g.Count()
                              orderby count descending
                              select new { Value = g.Key, Count = count };

                foreach (var x in c_servico)
                {
                    r_servico.Add(new KeyValuePair<string, int>(x.Value, x.Count));
                }

                r_servico.Add(new KeyValuePair<string, int>("Total de Serviços " + canal, _servico.Count()));
            }
            catch { }

            return r_servico;
        }
    }
}
