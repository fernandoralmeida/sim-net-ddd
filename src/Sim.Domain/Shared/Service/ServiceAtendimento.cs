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

        public async Task<IEnumerable<KeyValuePair<string, int>>> BySetor(string setor, DateTime periodo)
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

                        if (at.Data.Value.Year == periodo.Year)
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

        public async Task<IEnumerable<KeyValuePair<string, int>>> ByAll(DateTime periodo)
        {
            var d1 = new DateTime(periodo.Year, 01, 01);
            var d2 = new DateTime(periodo.Year, 12, 31);

            var list = _atendimento.ListByPeriodo(d1, d2);
            
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

                        if (at.Data.Value.Year == periodo.Year)
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

        public async Task<IEnumerable<KeyValuePair<string, int>>> ByUserName(string username, DateTime periodo)
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
                    if(at.Data.Value.Year == periodo.Year)
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

        public async Task<IEnumerable<KeyValuePair<string, int>>> ByServicos(string servico, DateTime periodo)
        {
            var t = Task.Run(() => _atendimento.GetByServicos(servico));
            await t;
            var r_servico = new List<KeyValuePair<string, int>>();
            try
            {
                //List<string> _ano = new List<string>();
                //var _canal = new List<string>();
                var _servico = new List<string>();
                var _user = new List<string>();

                foreach (Atendimento at in t.Result)
                {
                    if(at.Data.Value.Year == periodo.Year)
                    {
                        if (at.Servicos != null)
                        {
                            string[] words = at.Servicos.ToString().Split(new char[] { ';', ',' });

                            foreach (string sv in words)
                            {
                                if (sv != null && sv != string.Empty)
                                    if (sv == servico)
                                    {
                                        _user.Add(at.Owner_AppUser_Id);
                                        _servico.Add(sv);
                                    }
                            }
                        }

                    }
                    //_ano.Add(at.Data.Value.Year.ToString());

                }

                r_servico.Add(new KeyValuePair<string, int>(servico, _servico.Count()));

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

        public async Task<IEnumerable<KeyValuePair<string, int>>> ByCanal(string canal, string setor, DateTime periodo)
        {
            
            var t = Task.Run(() => _atendimento.GetByCanal(canal));
            await t;
            var r_servico = new List<KeyValuePair<string, int>>();
            try
            {

                var _atendimento = new List<string>();
                var _servico = new List<string>();

                foreach (Atendimento at in t.Result)
                {
                    if(at.Data.Value.Year == periodo.Year)
                    {
                        _atendimento.Add("Atendimentos");

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
                }

                var c_atendimento = from x in _atendimento
                                    group x by x into g
                                let count = g.Count()
                                orderby count descending
                                select new { Value = g.Key, Count = count };

                foreach (var x in c_atendimento)
                {
                    r_servico.Add(new KeyValuePair<string, int>(x.Value, x.Count));
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

        public async Task<IEnumerable<KeyValuePair<string, int>>> BySetorMonth(string setor, DateTime periodo)
        {
            var list = new List<Atendimento>();

            if (setor == "Sala do Empreendedor")
            {
                var se = _atendimento.GetBySetor("Sala do Empreendedor");

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
                        if(at.Data.Value.Year == periodo.Year)
                        {

                            if (at.Data.Value.Month == periodo.Month)
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
                        }

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

        public async Task<IEnumerable<KeyValuePair<string, int>>> ByAllMonth(DateTime periodo)
        {
            var list = await _atendimento.ListByMonth(periodo);

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

                        if(at.Data.Value.Year == periodo.Year)
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

        public async Task<IEnumerable<KeyValuePair<string, int>>> ByUserNameMonth(string username, string setor, DateTime periodo)
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

                    if (at.Data.Value.Month == periodo.Month && at.Data.Value.Year == periodo.Year)
                    {

                        if (at.Setor == "Espaço do Empreendedor")
                            at.Setor = "Sala do Empreendedor";

                        if (at.Setor == setor || setor == "Geral")
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

        public async Task<IEnumerable<KeyValuePair<string, int>>> ByServicosMonth(string servico, DateTime periodo)
        {
            var t = Task.Run(() => _atendimento.GetByServicos(servico));
            await t;
            var r_servico = new List<KeyValuePair<string, int>>();
            try
            {
                //List<string> _ano = new List<string>();
                var _servico = new List<string>();
                var _user = new List<string>();

                foreach (Atendimento at in t.Result)
                {
                    if (at.Data.Value.Month == periodo.Month && at.Data.Value.Year == periodo.Year)
                    {

                        if (at.Servicos != null)
                        {
                            string[] words = at.Servicos.ToString().Split(new char[] { ';', ',' });

                            foreach (string sv in words)
                            {
                                if (sv != null && sv != string.Empty)
                                    if (sv == servico)
                                    {
                                        _user.Add(at.Owner_AppUser_Id);
                                        _servico.Add(sv);
                                    }
                            }
                        }
                    }
                    //_ano.Add(at.Data.Value.Year.ToString());
                }

                r_servico.Add(new KeyValuePair<string, int>(servico, _servico.Count()));

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

        public async Task<IEnumerable<KeyValuePair<string, int>>> ByCanalMonth(string canal, string setor, DateTime periodo)
        {
            var t = Task.Run(() => _atendimento.GetByCanal(canal));
            await t;
            var r_servico = new List<KeyValuePair<string, int>>();
            try
            {
                var _atendimento = new List<string>();
                var _servico = new List<string>();

                foreach (Atendimento at in t.Result)
                {
                    if(at.Data.Value.Year == periodo.Year && at.Data.Value.Month == periodo.Month)
                    {
                        

                        if (at.Setor == "Espaço do Empreendedor")
                            at.Setor = "Sala do Empreendedor";

                        if (at.Setor == setor || setor == "Geral")
                            if (at.Servicos != null)
                            {
                                _atendimento.Add("Atendimentos");

                                string[] words = at.Servicos.ToString().Split(new char[] { ';', ',' });

                                foreach (string sv in words)
                                {
                                    if (sv != null && sv != string.Empty)
                                        _servico.Add(sv);
                                }
                            }
                    }
                }

                var c_atendimento = from x in _atendimento
                                    group x by x into g
                                    let count = g.Count()
                                    orderby count descending
                                    select new { Value = g.Key, Count = count };

                foreach (var x in c_atendimento)
                {
                    r_servico.Add(new KeyValuePair<string, int>(x.Value, x.Count));
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
        public async Task<BI.BiAtendimentos> BI_Atendimentos(DateTime periodo)
        {
            var d1 = new DateTime(periodo.Year, 01, 01);
            var d2 = new DateTime(periodo.Year, 12, 31);

            var list = _atendimento.ListByPeriodo(d1, d2);

            var r_all = new BI.BiAtendimentos();

            var t = Task.Run(() => {

                try
                {
                    var _atendimentos = new List<string>();
                    var _servicos = new List<string>();

                    var _a_pessoas = new List<string>();
                    var _s_pessoas = new List<string>();

                    var _a_empresas = new List<string>();
                    var _s_empresas = new List<string>();

                    foreach (Atendimento at in list)
                    {
                        _atendimentos.Add("Atendimentos");

                        if (at.Empresa != null)
                            _a_empresas.Add("A_Empresas");
                        else
                            _a_pessoas.Add("A_Pessoas");

                        if (at.Servicos != null)
                        {
                            string[] words = at.Servicos.ToString().Split(new char[] { ';', ',' });

                            foreach (string sv in words)
                            {
                                if (sv != null && sv != string.Empty)
                                {
                                    _servicos.Add("Serviços");
                                    if (at.Empresa != null)
                                        _s_empresas.Add("S_Empresas");
                                    else
                                        _s_pessoas.Add("S_Pessoas");
                                }
                            }
                        }
                        #region Meses
                        // mes servicos 
                        /*
                        switch (at.Data.Value.Month)
                        {
                            case 1:
                                if (at.Empresa != null)
                                    _empresas_mes.Add("Empresas-S");
                                else
                                    _pessoas_mes.Add("Pessoas-S");

                                if (at.Servicos != null)
                                {
                                    string[] words = at.Servicos.ToString().Split(new char[] { ';', ',' });

                                    foreach (string sv in words)
                                    {
                                        if (sv != null && sv != string.Empty)
                                        {
                                            _mes_servicos.Add("Serv " + at.Data.Value.ToString("MMM"));
                                            if (at.Empresa != null)
                                                _empresas_mes_servicos.Add("Empresas-" + at.Data.Value.ToString("MMM"));
                                            else
                                                _pessoas_mes_servicos.Add("Pessoas-" + at.Data.Value.ToString("MMM"));
                                        }
                                            
                                    }
                                }
                                break;

                            case 2:
                                if (at.Empresa != null)
                                    _empresas_mes.Add("Empresas");
                                else
                                    _pessoas_mes.Add("Pessoas");

                                if (at.Servicos != null)
                                {
                                    string[] words = at.Servicos.ToString().Split(new char[] { ';', ',' });

                                    foreach (string sv in words)
                                    {
                                        if (sv != null && sv != string.Empty)
                                        {
                                            _mes_servicos.Add("Serv " + at.Data.Value.Date.ToString("MMM"));
                                            if (at.Empresa != null)
                                                _empresas_mes_servicos.Add("Empresas-" + at.Data.Value.ToString("MMM"));
                                            else
                                                _pessoas_mes_servicos.Add("Pessoas-" + at.Data.Value.ToString("MMM"));
                                        }

                                    }
                                }
                                break;

                            case 3:
                                if (at.Empresa != null)
                                    _empresas_mes.Add("Empresas");
                                else
                                    _pessoas_mes.Add("Pessoas");

                                if (at.Servicos != null)
                                {
                                    string[] words = at.Servicos.ToString().Split(new char[] { ';', ',' });

                                    foreach (string sv in words)
                                    {
                                        if (sv != null && sv != string.Empty)
                                        {
                                            _mes_servicos.Add("Serv " + at.Data.Value.Date.ToString("MMM"));
                                            if (at.Empresa != null)
                                                _empresas_mes_servicos.Add("Empresas-" + at.Data.Value.ToString("MMM"));
                                            else
                                                _pessoas_mes_servicos.Add("Pessoas-" + at.Data.Value.ToString("MMM"));
                                        }

                                    }
                                }
                                break;

                            case 4:
                                if (at.Empresa != null)
                                    _empresas_mes.Add("Empresas");
                                else
                                    _pessoas_mes.Add("Pessoas");

                                if (at.Servicos != null)
                                {
                                    string[] words = at.Servicos.ToString().Split(new char[] { ';', ',' });

                                    foreach (string sv in words)
                                    {
                                        if (sv != null && sv != string.Empty)
                                        {
                                            _mes_servicos.Add("Serv " + at.Data.Value.Date.ToString("MMM"));
                                            if (at.Empresa != null)
                                                _empresas_mes_servicos.Add("Empresas-" + at.Data.Value.ToString("MMM"));
                                            else
                                                _pessoas_mes_servicos.Add("Pessoas-" + at.Data.Value.ToString("MMM"));
                                        }

                                    }
                                }
                                break;

                            case 5:
                                if (at.Empresa != null)
                                    _empresas_mes.Add("Empresas");
                                else
                                    _pessoas_mes.Add("Pessoas");

                                if (at.Servicos != null)
                                {
                                    string[] words = at.Servicos.ToString().Split(new char[] { ';', ',' });

                                    foreach (string sv in words)
                                    {
                                        if (sv != null && sv != string.Empty)
                                        {
                                            _mes_servicos.Add("Serv " + at.Data.Value.Date.ToString("MMM"));
                                            if (at.Empresa != null)
                                                _empresas_mes_servicos.Add("Empresas-" + at.Data.Value.ToString("MMM"));
                                            else
                                                _pessoas_mes_servicos.Add("Pessoas-" + at.Data.Value.ToString("MMM"));
                                        }

                                    }
                                }
                                break;

                            case 6:
                                if (at.Empresa != null)
                                    _empresas_mes.Add("Empresas");
                                else
                                    _pessoas_mes.Add("Pessoas");

                                if (at.Servicos != null)
                                {
                                    string[] words = at.Servicos.ToString().Split(new char[] { ';', ',' });

                                    foreach (string sv in words)
                                    {
                                        if (sv != null && sv != string.Empty)
                                        {
                                            _mes_servicos.Add("Serv " + at.Data.Value.Date.ToString("MMM"));
                                            if (at.Empresa != null)
                                                _empresas_mes_servicos.Add("Empresas-" + at.Data.Value.ToString("MMM"));
                                            else
                                                _pessoas_mes_servicos.Add("Pessoas-" + at.Data.Value.ToString("MMM"));
                                        }

                                    }
                                }
                                break;

                            case 7:
                                if (at.Empresa != null)
                                    _empresas_mes.Add("Empresas");
                                else
                                    _pessoas_mes.Add("Pessoas");

                                if (at.Servicos != null)
                                {
                                    string[] words = at.Servicos.ToString().Split(new char[] { ';', ',' });

                                    foreach (string sv in words)
                                    {
                                        if (sv != null && sv != string.Empty)
                                        {
                                            _mes_servicos.Add("Serv " + at.Data.Value.Date.ToString("MMM"));
                                            if (at.Empresa != null)
                                                _empresas_mes_servicos.Add("Empresas-" + at.Data.Value.ToString("MMM"));
                                            else
                                                _pessoas_mes_servicos.Add("Pessoas-" + at.Data.Value.ToString("MMM"));
                                        }

                                    }
                                }
                                break;

                            case 8:
                                if (at.Empresa != null)
                                    _empresas_mes.Add("Empresas");
                                else
                                    _pessoas_mes.Add("Pessoas");

                                if (at.Servicos != null)
                                {
                                    string[] words = at.Servicos.ToString().Split(new char[] { ';', ',' });

                                    foreach (string sv in words)
                                    {
                                        if (sv != null && sv != string.Empty)
                                        {
                                            _mes_servicos.Add("Serv " + at.Data.Value.Date.ToString("MMM"));
                                            if (at.Empresa != null)
                                                _empresas_mes_servicos.Add("Empresas-" + at.Data.Value.ToString("MMM"));
                                            else
                                                _pessoas_mes_servicos.Add("Pessoas-" + at.Data.Value.ToString("MMM"));
                                        }

                                    }
                                }
                                break;

                            case 9:
                                if (at.Empresa != null)
                                    _empresas_mes.Add("Empresas");
                                else
                                    _pessoas_mes.Add("Pessoas");

                                if (at.Servicos != null)
                                {
                                    string[] words = at.Servicos.ToString().Split(new char[] { ';', ',' });

                                    foreach (string sv in words)
                                    {
                                        if (sv != null && sv != string.Empty)
                                        {
                                            _mes_servicos.Add("Serv " + at.Data.Value.Date.ToString("MMM"));
                                            if (at.Empresa != null)
                                                _empresas_mes_servicos.Add("Empresas-" + at.Data.Value.ToString("MMM"));
                                            else
                                                _pessoas_mes_servicos.Add("Pessoas-" + at.Data.Value.ToString("MMM"));
                                        }

                                    }
                                }
                                break;

                            case 10:
                                if (at.Empresa != null)
                                    _empresas_mes.Add("Empresas");
                                else
                                    _pessoas_mes.Add("Pessoas");

                                if (at.Servicos != null)
                                {
                                    string[] words = at.Servicos.ToString().Split(new char[] { ';', ',' });

                                    foreach (string sv in words)
                                    {
                                        if (sv != null && sv != string.Empty)
                                        {
                                            _mes_servicos.Add("Serv " + at.Data.Value.Date.ToString("MMM"));
                                            if (at.Empresa != null)
                                                _empresas_mes_servicos.Add("Empresas-" + at.Data.Value.ToString("MMM"));
                                            else
                                                _pessoas_mes_servicos.Add("Pessoas-" + at.Data.Value.ToString("MMM"));
                                        }

                                    }
                                }
                                break;

                            case 11:
                                if (at.Empresa != null)
                                    _empresas_mes.Add("Empresas");
                                else
                                    _pessoas_mes.Add("Pessoas");

                                if (at.Servicos != null)
                                {
                                    string[] words = at.Servicos.ToString().Split(new char[] { ';', ',' });

                                    foreach (string sv in words)
                                    {
                                        if (sv != null && sv != string.Empty)
                                        {
                                            _mes_servicos.Add("Serv " + at.Data.Value.Date.ToString("MMM"));
                                            if (at.Empresa != null)
                                                _empresas_mes_servicos.Add("Empresas-" + at.Data.Value.ToString("MMM"));
                                            else
                                                _pessoas_mes_servicos.Add("Pessoas-" + at.Data.Value.ToString("MMM"));
                                        }

                                    }
                                }
                                break;

                            case 12:
                                if (at.Empresa != null)
                                    _empresas_mes.Add("Empresas");
                                else
                                    _pessoas_mes.Add("Pessoas");

                                if (at.Servicos != null)
                                {
                                    string[] words = at.Servicos.ToString().Split(new char[] { ';', ',' });

                                    foreach (string sv in words)
                                    {
                                        if (sv != null && sv != string.Empty)
                                        {
                                            _mes_servicos.Add("Serv " + at.Data.Value.Date.ToString("MMM"));
                                            if (at.Empresa != null)
                                                _empresas_mes_servicos.Add("Empresas-" + at.Data.Value.ToString("MMM"));
                                            else
                                                _pessoas_mes_servicos.Add("Pessoas-" + at.Data.Value.ToString("MMM"));
                                        }

                                    }
                                }
                                break;

                            default:
                                break;
                        }
                        */
                        #endregion
                    }

                    foreach (var x in from x in _atendimentos
                                      group x by x into g
                                      let count = g.Count()
                                      //orderby count descending
                                      select new { Value = g.Key, Count = count })
                    {
                        r_all.Antendimentos_Ano = new KeyValuePair<string, int>("Atendimentos", x.Count);
                    }

                    foreach (var x in from x in _servicos
                                      group x by x into g
                                      let count = g.Count()
                                      //orderby count descending
                                      select new { Value = g.Key, Count = count })
                    {
                        r_all.Servicos_Ano = new KeyValuePair<string, int>("Serviços", x.Count);
                    }

                    foreach (var x in from x in _a_pessoas
                                      group x by x into g
                                      let count = g.Count()
                                      orderby count descending
                                      select new { Value = g.Key, Count = count })
                    {
                        r_all.Pessoas_Ano = new KeyValuePair<string, int>("Atendimentos", x.Count);
                    }

                    foreach (var x in from x in _s_pessoas
                                      group x by x into g
                                      let count = g.Count()
                                      orderby count descending
                                      select new { Value = g.Key, Count = count })
                    {
                        r_all.Pessoas_Servicos_Ano = new KeyValuePair<string, int>("Serviços", x.Count);
                    }


                    foreach (var x in from x in _a_empresas
                                      group x by x into g
                                      let count = g.Count()
                                      orderby count descending
                                      select new { Value = g.Key, Count = count })
                    {
                        r_all.Empresas_Ano = new KeyValuePair<string, int>("Atendimentos", x.Count);
                    }

                    foreach (var x in from x in _s_empresas
                                      group x by x into g
                                      let count = g.Count()
                                      orderby count descending
                                      select new { Value = g.Key, Count = count })
                    {
                        r_all.Empresas_Servicos_Ano = new KeyValuePair<string, int>("Serviços", x.Count);
                    }
                }
                catch { }

            });
            await t;

            return r_all;
        }

        public Task<IEnumerable<KeyValuePair<string, int>>> BI_Atendimentos_SA(DateTime periodo)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<KeyValuePair<string, int>>> BI_Atendimentos_SE(DateTime periodo)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<KeyValuePair<string, int>>> BI_Atendimentos_BP(DateTime periodo)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<KeyValuePair<string, int>>> BI_Atendimentos_PT(DateTime periodo)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<KeyValuePair<string, int>>> BI_Atendimentos_EP(DateTime periodo)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<KeyValuePair<string, int>>> BI_Atendimentos_AppUser(DateTime periodo)
        {
            throw new NotImplementedException();
        }
    }
}
