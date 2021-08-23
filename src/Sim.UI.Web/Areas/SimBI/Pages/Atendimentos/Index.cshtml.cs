using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

namespace Sim.UI.Web.Areas.SimBI.Pages.Atendimentos
{

    using Sim.Application.Shared.Interface;
    using Sim.Domain.Shared.Entity;
    using System;

    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IAppServiceAtendimento _appAtendimento;

        [BindProperty(SupportsGet = true)]
        public InputModelIndex Input { get; set; }

        [BindProperty(SupportsGet = true)]
        public InputModelIndex ListaPAT { get; set; }

        [BindProperty(SupportsGet = true)]
        public InputModelIndex ListaBPP { get; set; }

        [BindProperty(SupportsGet = true)]
        public InputModelIndex ListaSA { get; set; }

        [BindProperty(SupportsGet = true)]
        public InputModelIndex ListaSE { get; set; }

        public class InputModelIndex
        {
            public List<KeyValuePair<string, int>> Ano { get; set; }
            public List<KeyValuePair<string, int>> Atendimentos { get; set; }
            public List<KeyValuePair<string, int>> Servicos { get; set; }
            public List<KeyValuePair<string, int>> Setor { get; set; }
            public List<KeyValuePair<string, int>> Canal { get; set; }
            public List<KeyValuePair<string, int>> Operador { get; set; }
        }

        [TempData]
        public string StatusMessage { get; set; }

        public IndexModel(IAppServiceAtendimento appAtendimento)
        {
            _appAtendimento = appAtendimento;

        }

        private async Task LoadAsync()
        {
            var t = Task.Run(() => _appAtendimento.ListAll());
            await t;

            Input = new();
            Input.Ano = new List<KeyValuePair<string, int>>();
            Input.Atendimentos = new List<KeyValuePair<string, int>>();
            Input.Servicos = new List<KeyValuePair<string, int>>();
            Input.Setor = new List<KeyValuePair<string, int>>();
            Input.Canal = new List<KeyValuePair<string, int>>();
            Input.Operador = new List<KeyValuePair<string, int>>();

            try
            {
                List<string> _ano = new List<string>();
                List<string> _atendimento = new List<string>();
                List<string> _servico = new List<string>();
                List<string> _setor = new List<string>();
                List<string> _canal = new List<string>();
                List<string> _operador = new List<string>();

                foreach (Atendimento at in t.Result)
                {
                    _atendimento.Add("Atendimentos");

                    _ano.Add(at.Data.Value.Year.ToString());

                    if (at.Servicos != null)
                    {
                        string[] words = at.Servicos.ToString().Split(new Char[] { ';', ',' });

                        foreach (string sv in words)
                        {
                            if (sv != null && sv != string.Empty)
                                _servico.Add(sv);
                        }
                    }

                    if (at.Canal != null)
                        _canal.Add(at.Canal);

                    if (at.Setor != null)
                        _setor.Add(at.Setor);

                    _operador.Add(at.Owner_AppUser_Id);
                }

                var c_atendimento = from x in _atendimento
                                    group x by x into g
                                    let count = g.Count()
                                    orderby count descending
                                    select new { Value = g.Key, Count = count };

                foreach (var x in c_atendimento)
                {
                    Input.Atendimentos.Add(new KeyValuePair<string, int>(x.Value, x.Count));
                }

                var c_ano = from x in _ano
                            group x by x into g
                            let count = g.Count()
                            orderby count descending
                            select new { Value = g.Key, Count = count };

                foreach (var x in c_ano)
                {
                    Input.Ano.Add(new KeyValuePair<string, int>(x.Value, x.Count));
                }

                Input.Servicos.Add(new KeyValuePair<string, int>("Serviços", _servico.Count()));
            }
            catch { }            
        }

        private async Task LoadPATAsync()
        {
            var t = Task.Run(() => _appAtendimento.GetBySetor("PAT"));
            await t;

            ListaPAT = new();
            ListaPAT.Ano = new List<KeyValuePair<string, int>>();
            ListaPAT.Atendimentos = new List<KeyValuePair<string, int>>();
            ListaPAT.Servicos = new List<KeyValuePair<string, int>>();
            ListaPAT.Setor = new List<KeyValuePair<string, int>>();
            ListaPAT.Canal = new List<KeyValuePair<string, int>>();
            ListaPAT.Operador = new List<KeyValuePair<string, int>>();

            try
            {
                List<string> _ano = new List<string>();
                List<string> _atendimento = new List<string>();
                List<string> _servico = new List<string>();
                List<string> _setor = new List<string>();
                List<string> _canal = new List<string>();
                List<string> _operador = new List<string>();

                foreach (Atendimento at in t.Result)
                {
                    _atendimento.Add("Atendimentos");

                    _ano.Add(at.Data.Value.Year.ToString());

                    if (at.Servicos != null)
                    {
                        string[] words = at.Servicos.ToString().Split(new Char[] { ';', ',' });

                        foreach (string sv in words)
                        {
                            if (sv != null && sv != string.Empty)
                                _servico.Add(sv);
                        }
                    }

                    if (at.Canal != null)
                        _canal.Add(at.Canal);

                    if (at.Setor != null)
                        _setor.Add(at.Setor);

                    _operador.Add(at.Owner_AppUser_Id);
                }

                var c_atendimento = from x in _atendimento
                                    group x by x into g
                                    let count = g.Count()
                                    orderby count descending
                                    select new { Value = g.Key, Count = count };

                foreach (var x in c_atendimento)
                {
                    ListaPAT.Atendimentos.Add(new KeyValuePair<string, int>(x.Value, x.Count));
                }

                var c_ano = from x in _ano
                            group x by x into g
                            let count = g.Count()
                            orderby count descending
                            select new { Value = g.Key, Count = count };

                foreach (var x in c_ano)
                {
                    ListaPAT.Ano.Add(new KeyValuePair<string, int>(x.Value, x.Count));
                }

                var c_tipo = from x in _servico
                             group x by x into g
                             let count = g.Count()
                             orderby count descending
                             select new { Value = g.Key, Count = count };

                foreach (var x in c_tipo)
                {
                    ListaPAT.Servicos.Add(new KeyValuePair<string, int>(x.Value, x.Count));
                }

                var c_origem = from x in _setor
                               group x by x into g
                               let count = g.Count()
                               orderby count descending
                               select new { Value = g.Key, Count = count };

                foreach (var x in c_origem)
                {
                    ListaPAT.Setor.Add(new KeyValuePair<string, int>(x.Value, x.Count));
                }

                var c_operador = from x in _operador
                                 group x by x into g
                                 let count = g.Count()
                                 orderby count descending
                                 select new { Value = g.Key, Count = count };

                foreach (var x in c_operador)
                {
                    ListaPAT.Operador.Add(new KeyValuePair<string, int>(x.Value, x.Count));
                }

                var c_canal = from x in _canal
                              group x by x into g
                              let count = g.Count()
                              orderby count descending
                              select new { Value = g.Key, Count = count };

                foreach (var x in c_canal)
                {
                    ListaPAT.Canal.Add(new KeyValuePair<string, int>(x.Value, x.Count));
                }

            }
            catch { }
        }

        private async Task LoadBPPAsync()
        {
            var t = Task.Run(() => _appAtendimento.GetBySetor("Banco do Povo"));
            await t;

            ListaBPP = new();
            ListaBPP.Ano = new List<KeyValuePair<string, int>>();
            ListaBPP.Atendimentos = new List<KeyValuePair<string, int>>();
            ListaBPP.Servicos = new List<KeyValuePair<string, int>>();
            ListaBPP.Setor = new List<KeyValuePair<string, int>>();
            ListaBPP.Canal = new List<KeyValuePair<string, int>>();
            ListaBPP.Operador = new List<KeyValuePair<string, int>>();

            try
            {
                List<string> _ano = new List<string>();
                List<string> _atendimento = new List<string>();
                List<string> _servico = new List<string>();
                List<string> _setor = new List<string>();
                List<string> _canal = new List<string>();
                List<string> _operador = new List<string>();

                foreach (Atendimento at in t.Result)
                {
                    _atendimento.Add("Atendimentos");

                    _ano.Add(at.Data.Value.Year.ToString());

                    if (at.Servicos != null)
                    {
                        string[] words = at.Servicos.ToString().Split(new Char[] { ';', ',' });

                        foreach (string sv in words)
                        {
                            if (sv != null && sv != string.Empty)
                                _servico.Add(sv);
                        }
                    }

                    if (at.Canal != null)
                        _canal.Add(at.Canal);

                    if (at.Setor != null)
                        _setor.Add(at.Setor);

                    _operador.Add(at.Owner_AppUser_Id);
                }

                var c_atendimento = from x in _atendimento
                                    group x by x into g
                                    let count = g.Count()
                                    orderby count descending
                                    select new { Value = g.Key, Count = count };

                foreach (var x in c_atendimento)
                {
                    ListaBPP.Atendimentos.Add(new KeyValuePair<string, int>(x.Value, x.Count));
                }

                var c_ano = from x in _ano
                            group x by x into g
                            let count = g.Count()
                            orderby count descending
                            select new { Value = g.Key, Count = count };

                foreach (var x in c_ano)
                {
                    ListaBPP.Ano.Add(new KeyValuePair<string, int>(x.Value, x.Count));
                }

                var c_tipo = from x in _servico
                             group x by x into g
                             let count = g.Count()
                             orderby count descending
                             select new { Value = g.Key, Count = count };

                foreach (var x in c_tipo)
                {
                    ListaBPP.Servicos.Add(new KeyValuePair<string, int>(x.Value, x.Count));
                }

                var c_origem = from x in _setor
                               group x by x into g
                               let count = g.Count()
                               orderby count descending
                               select new { Value = g.Key, Count = count };

                foreach (var x in c_origem)
                {
                    ListaBPP.Setor.Add(new KeyValuePair<string, int>(x.Value, x.Count));
                }

                var c_operador = from x in _operador
                                 group x by x into g
                                 let count = g.Count()
                                 orderby count descending
                                 select new { Value = g.Key, Count = count };

                foreach (var x in c_operador)
                {
                    ListaBPP.Operador.Add(new KeyValuePair<string, int>(x.Value, x.Count));
                }

                var c_canal = from x in _canal
                              group x by x into g
                              let count = g.Count()
                              orderby count descending
                              select new { Value = g.Key, Count = count };

                foreach (var x in c_canal)
                {
                    ListaBPP.Canal.Add(new KeyValuePair<string, int>(x.Value, x.Count));
                }

            }
            catch { }
        }

        private async Task LoadSAAsync()
        {
            var t = Task.Run(() => _appAtendimento.GetBySetor("Sebrae Aqui"));
            await t;

            ListaSA = new();
            ListaSA.Ano = new List<KeyValuePair<string, int>>();
            ListaSA.Atendimentos = new List<KeyValuePair<string, int>>();
            ListaSA.Servicos = new List<KeyValuePair<string, int>>();
            ListaSA.Setor = new List<KeyValuePair<string, int>>();
            ListaSA.Canal = new List<KeyValuePair<string, int>>();
            ListaSA.Operador = new List<KeyValuePair<string, int>>();

            try
            {
                List<string> _ano = new List<string>();
                List<string> _atendimento = new List<string>();
                List<string> _servico = new List<string>();
                List<string> _setor = new List<string>();
                List<string> _canal = new List<string>();
                List<string> _operador = new List<string>();

                foreach (Atendimento at in t.Result)
                {
                    _atendimento.Add("Atendimentos");

                    _ano.Add(at.Data.Value.Year.ToString());

                    if (at.Servicos != null)
                    {
                        string[] words = at.Servicos.ToString().Split(new Char[] { ';', ',' });

                        foreach (string sv in words)
                        {
                            if (sv != null && sv != string.Empty)
                                _servico.Add(sv);
                        }
                    }

                    if (at.Canal != null)
                        _canal.Add(at.Canal);

                    if (at.Setor != null)
                        _setor.Add(at.Setor);

                    _operador.Add(at.Owner_AppUser_Id);
                }

                var c_atendimento = from x in _atendimento
                                    group x by x into g
                                    let count = g.Count()
                                    orderby count descending
                                    select new { Value = g.Key, Count = count };

                foreach (var x in c_atendimento)
                {
                    ListaSA.Atendimentos.Add(new KeyValuePair<string, int>(x.Value, x.Count));
                }

                var c_ano = from x in _ano
                            group x by x into g
                            let count = g.Count()
                            orderby count descending
                            select new { Value = g.Key, Count = count };

                foreach (var x in c_ano)
                {
                    ListaSA.Ano.Add(new KeyValuePair<string, int>(x.Value, x.Count));
                }

                var c_tipo = from x in _servico
                             group x by x into g
                             let count = g.Count()
                             orderby count descending
                             select new { Value = g.Key, Count = count };

                foreach (var x in c_tipo)
                {
                    ListaSA.Servicos.Add(new KeyValuePair<string, int>(x.Value, x.Count));
                }

                var c_origem = from x in _setor
                               group x by x into g
                               let count = g.Count()
                               orderby count descending
                               select new { Value = g.Key, Count = count };

                foreach (var x in c_origem)
                {
                    ListaSA.Setor.Add(new KeyValuePair<string, int>(x.Value, x.Count));
                }

                var c_operador = from x in _operador
                                 group x by x into g
                                 let count = g.Count()
                                 orderby count descending
                                 select new { Value = g.Key, Count = count };

                foreach (var x in c_operador)
                {
                    ListaSA.Operador.Add(new KeyValuePair<string, int>(x.Value, x.Count));
                }

                var c_canal = from x in _canal
                              group x by x into g
                              let count = g.Count()
                              orderby count descending
                              select new { Value = g.Key, Count = count };

                foreach (var x in c_canal)
                {
                    ListaSA.Canal.Add(new KeyValuePair<string, int>(x.Value, x.Count));
                }

            }
            catch { }
        }


        private async Task LoadSEAsync()
        {
            var t = Task.Run(() => _appAtendimento.GetBySetor("Sala do Empreendedor"));
            await t;

            ListaSE = new();
            ListaSE.Ano = new List<KeyValuePair<string, int>>();
            ListaSE.Atendimentos = new List<KeyValuePair<string, int>>();
            ListaSE.Servicos = new List<KeyValuePair<string, int>>();
            ListaSE.Setor = new List<KeyValuePair<string, int>>();
            ListaSE.Canal = new List<KeyValuePair<string, int>>();
            ListaSE.Operador = new List<KeyValuePair<string, int>>();

            try
            {
                List<string> _ano = new List<string>();
                List<string> _atendimento = new List<string>();
                List<string> _servico = new List<string>();
                List<string> _setor = new List<string>();
                List<string> _canal = new List<string>();
                List<string> _operador = new List<string>();

                foreach (Atendimento at in t.Result)
                {
                    _atendimento.Add("Atendimentos");

                    _ano.Add(at.Data.Value.Year.ToString());

                    if (at.Servicos != null)
                    {
                        string[] words = at.Servicos.ToString().Split(new Char[] { ';', ',' });

                        foreach (string sv in words)
                        {
                            if (sv != null && sv != string.Empty)
                                _servico.Add(sv);
                        }
                    }

                    if (at.Canal != null)
                        _canal.Add(at.Canal);

                    if (at.Setor != null)
                        _setor.Add(at.Setor);

                    _operador.Add(at.Owner_AppUser_Id);
                }

                var c_atendimento = from x in _atendimento
                                    group x by x into g
                                    let count = g.Count()
                                    orderby count descending
                                    select new { Value = g.Key, Count = count };

                foreach (var x in c_atendimento)
                {
                    ListaSE.Atendimentos.Add(new KeyValuePair<string, int>(x.Value, x.Count));
                }

                var c_ano = from x in _ano
                            group x by x into g
                            let count = g.Count()
                            orderby count descending
                            select new { Value = g.Key, Count = count };

                foreach (var x in c_ano)
                {
                    ListaSE.Ano.Add(new KeyValuePair<string, int>(x.Value, x.Count));
                }

                var c_tipo = from x in _servico
                             group x by x into g
                             let count = g.Count()
                             orderby count descending
                             select new { Value = g.Key, Count = count };

                foreach (var x in c_tipo)
                {
                    ListaSE.Servicos.Add(new KeyValuePair<string, int>(x.Value, x.Count));
                }

                var c_origem = from x in _setor
                               group x by x into g
                               let count = g.Count()
                               orderby count descending
                               select new { Value = g.Key, Count = count };

                foreach (var x in c_origem)
                {
                    ListaSE.Setor.Add(new KeyValuePair<string, int>(x.Value, x.Count));
                }

                var c_operador = from x in _operador
                                 group x by x into g
                                 let count = g.Count()
                                 orderby count descending
                                 select new { Value = g.Key, Count = count };

                foreach (var x in c_operador)
                {
                    ListaSE.Operador.Add(new KeyValuePair<string, int>(x.Value, x.Count));
                }

                var c_canal = from x in _canal
                              group x by x into g
                              let count = g.Count()
                              orderby count descending
                              select new { Value = g.Key, Count = count };

                foreach (var x in c_canal)
                {
                    ListaSE.Canal.Add(new KeyValuePair<string, int>(x.Value, x.Count));
                }

            }
            catch { }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await LoadAsync();
            await LoadPATAsync();
            await LoadBPPAsync();
            await LoadSAAsync();
            await LoadSEAsync();
            return Page();
        }
    }
}
