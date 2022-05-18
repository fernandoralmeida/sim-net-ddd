using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

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

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty(SupportsGet = true)]
        public int Mes { get; set; }

        [BindProperty(SupportsGet = true)]
        public int Ano { get; set; }

        public IndexModel(IAppServiceAtendimento appAtendimento,
            IAppServiceSetor appsetores)
        {
            _appAtendimento = appAtendimento;
            _appSetores = appsetores;  
        }

        private async Task LoadAsync()
        {

            if(Mes == 0)
            {
                var periodo = new DateTime(Ano, 1, 1);

                var l_all_atendimentos = await _appAtendimento.ByAll(periodo);

                Input.Add(l_all_atendimentos);

                var setores = _appSetores.List();

                foreach (Setor s in setores)
                {
                    var setor = await _appAtendimento.BySetor(s.Nome, periodo);
                    if (setor.Count() > 1)
                        Input.Add(setor);
                }
            }
            else
            {
                var p = new DateTime(Ano, Mes, 1);

                var l_all_atendimentos = await _appAtendimento.ByAllMonth(p);

                Input.Add(l_all_atendimentos);

                var setores = _appSetores.List();

                foreach (Setor s in setores)
                {
                    var setor = await _appAtendimento.BySetorMonth(s.Nome, p);
                    if (setor.Count() > 1)
                        Input.Add(setor);
                }
            }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Ano = DateTime.Now.Year;
            Mes = DateTime.Now.Month;
            await LoadAsync();
            return Page();
        }

        public JsonResult OnGetPreview(string id, string id2, string mth)
        {
            JsonResult rjson;

            if (Mes == 0)
            {
                var periodo = new DateTime(Ano, 1, 1);
                var user_atendimentos = Task.Run(() => _appAtendimento.ByUserName(id, periodo));
                user_atendimentos.Wait();
                rjson = new JsonResult(user_atendimentos.Result);
            }
            else
            {
                var p = new DateTime(Ano, Mes, 1);
                var user_atendimentos = Task.Run(() => _appAtendimento.ByUserNameMonth(id, id2, p));
                user_atendimentos.Wait();
                rjson = new JsonResult(user_atendimentos.Result);
            }

            return rjson;
        }

        public JsonResult OnGetServicoPreview(string id, string mth)
        {
            JsonResult rjson;

            if (Mes == 0)
            {
                var periodo = new DateTime(Ano, 1, 1);
                var user_atendimentos = Task.Run(() => _appAtendimento.ByServicos(id, periodo));
                user_atendimentos.Wait();
                rjson = new JsonResult(user_atendimentos.Result);
            }
            else
            {
                var periodo = new DateTime(Ano, Mes, 1);
                var user_atendimentos = Task.Run(() => _appAtendimento.ByServicosMonth(id, periodo));
                user_atendimentos.Wait();
                rjson = new JsonResult(user_atendimentos.Result);
            }

            return rjson;
        }

        public JsonResult OnGetCanalPreview(string id, string id2, string mth)
        {
            JsonResult rjson;

            if (Mes == 0)
            {
                var periodo = new DateTime(Ano, 1, 1);
                var user_atendimentos = Task.Run(() => _appAtendimento.ByCanal(id, id2, periodo));
                user_atendimentos.Wait();
                rjson = new JsonResult(user_atendimentos.Result);
            }
            else
            {
                var periodo = new DateTime(Ano, Ano, 1);
                var user_atendimentos = Task.Run(() => _appAtendimento.ByCanalMonth(id, id2, periodo));
                user_atendimentos.Wait();
                rjson = new JsonResult(user_atendimentos.Result);
            }

            return rjson;
        }

        public async Task<IActionResult> OnPostMonth()
        {
            await LoadAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostYear()
        {
            await LoadAsync();
            return Page();
        }
    }
}
