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
        private readonly IAppServiceSetor _appSetores;

        [BindProperty(SupportsGet = true)]
        public List<IEnumerable<KeyValuePair<string, int>>> Input { get; set; }

        [BindProperty(SupportsGet = true)]
        public InputModelIndex ListaPAT { get; set; }

        [BindProperty(SupportsGet = true)]
        public InputModelIndex ListaBPP { get; set; }

        [BindProperty(SupportsGet = true)]
        public InputModelIndex ListaSA { get; set; }

        [BindProperty(SupportsGet = true)]
        public InputModelIndex ListaSE { get; set; }

        [BindProperty(SupportsGet = true)]
        public InputModelIndex ListaOperador { get; set; }

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

        public IndexModel(IAppServiceAtendimento appAtendimento,
            IAppServiceSetor appsetores)
        {
            _appAtendimento = appAtendimento;
            _appSetores = appsetores;
        }

        private async Task LoadAsync()
        {
            var t = _appSetores.List();

            foreach (Setor s in t)
            {
                var setor = await _appAtendimento.BySetor(s.Nome);
                if (setor.Count() > 1)
                    Input.Add(setor);
            }
        }

        private async Task LoadOperadorAsync()
        {
            var t = Task.Run(() => _appAtendimento.GetByUserName(User.Identity.Name));
            await t;

            ListaOperador = new();
            ListaOperador.Ano = new List<KeyValuePair<string, int>>();
            ListaOperador.Atendimentos = new List<KeyValuePair<string, int>>();
            ListaOperador.Servicos = new List<KeyValuePair<string, int>>();

            try
            {
                List<string> _ano = new List<string>();
                List<string> _atendimento = new List<string>();
                List<string> _servico = new List<string>();

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

                }

                var c_atendimento = from x in _atendimento
                                    group x by x into g
                                    let count = g.Count()
                                    orderby count descending
                                    select new { Value = g.Key, Count = count };

                foreach (var x in c_atendimento)
                {
                    ListaOperador.Atendimentos.Add(new KeyValuePair<string, int>(x.Value, x.Count));
                }

                var c_ano = from x in _ano
                            group x by x into g
                            let count = g.Count()
                            orderby count descending
                            select new { Value = g.Key, Count = count };

                foreach (var x in c_ano)
                {
                    ListaOperador.Ano.Add(new KeyValuePair<string, int>(x.Value, x.Count));
                }

                var c_tipo = from x in _servico
                             group x by x into g
                             let count = g.Count()
                             orderby count descending
                             select new { Value = g.Key, Count = count };

                foreach (var x in c_tipo)
                {
                    ListaOperador.Servicos.Add(new KeyValuePair<string, int>(x.Value, x.Count));
                }

            }
            catch { }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await LoadAsync();
            await LoadOperadorAsync();
            return Page();
        }
    }
}
