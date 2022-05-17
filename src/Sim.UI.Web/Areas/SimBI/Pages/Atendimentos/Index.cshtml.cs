using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
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

        [TempData]
        public string StatusMessage { get; set; }

        [TempData]
        public DateTime Periodo { get; set; }

        [TempData]
        public string Mes { get; set; }

        [TempData]
        public int Ano { get; set; }

        [TempData]
        public string ButtonPressed { get; set; }

        public IndexModel(IAppServiceAtendimento appAtendimento,
            IAppServiceSetor appsetores)
        {
            _appAtendimento = appAtendimento;
            _appSetores = appsetores;            
        }

        private async Task LoadAsyncMonth(DateTime date)
        {
            var l_all_atendimentos = await _appAtendimento.ByAllMonth(date);

            Input.Add(l_all_atendimentos);

            var setores = _appSetores.List();

            foreach (Setor s in setores)
            {
                var setor = await _appAtendimento.BySetorMonth(s.Nome, date);
                if (setor.Count() > 1)
                    Input.Add(setor);
            }
        }
        private async Task LoadAsync(DateTime date, string month, int ano)
        {
            ButtonPressed = month;

            GetPeriod(month);

            if(ButtonPressed == "Year")
            {
                var l_all_atendimentos = await _appAtendimento.ByAll(date);

                Input.Add(l_all_atendimentos);

                var setores = _appSetores.List();

                foreach (Setor s in setores)
                {
                    var setor = await _appAtendimento.BySetor(s.Nome, date);
                    if (setor.Count() > 1)
                        Input.Add(setor);
                }
            }
            else
                await LoadAsyncMonth(Periodo);
        }

        public async Task<IActionResult> OnGetAsync(string id, int y)
        {
            if (y != 0)
            {
                Ano = DateTime.Now.Year;
            }
            else
                Ano = y;

            Periodo = DateTime.Now; 
            await LoadAsync(Periodo, Functions.DateTimeExtensions.ToShortMonthName(DateTime.Now), Ano);
            return Page();
        }

        public JsonResult OnGetPreview(string id, string id2, string mth)
        {
            JsonResult rjson;
            Periodo = DateTime.Now;
            ButtonPressed = mth;
            GetPeriod(mth);

            if (ButtonPressed == "Year")
            {                
                var user_atendimentos = Task.Run(() => _appAtendimento.ByUserName(id, Periodo));
                user_atendimentos.Wait();
                rjson = new JsonResult(user_atendimentos.Result);
            }
            else
            {
                var user_atendimentos = Task.Run(() => _appAtendimento.ByUserNameMonth(id, id2, Periodo));
                user_atendimentos.Wait();
                rjson = new JsonResult(user_atendimentos.Result);
            }

            return rjson;
        }

        public JsonResult OnGetServicoPreview(string id, string mth)
        {
            JsonResult rjson;
            Periodo = DateTime.Now;
            ButtonPressed = mth;
            GetPeriod(mth);

            if (ButtonPressed == "Year")
            {
                var user_atendimentos = Task.Run(() => _appAtendimento.ByServicos(id, Periodo));
                user_atendimentos.Wait();
                rjson = new JsonResult(user_atendimentos.Result);
            }
            else
            {
                var user_atendimentos = Task.Run(() => _appAtendimento.ByServicosMonth(id, Periodo));
                user_atendimentos.Wait();
                rjson = new JsonResult(user_atendimentos.Result);
            }

            return rjson;
        }

        public JsonResult OnGetCanalPreview(string id, string id2, string mth)
        {
            JsonResult rjson;
            Periodo = DateTime.Now;
            ButtonPressed = mth;
            GetPeriod(mth);

            if (ButtonPressed == "Year")
            {
                var user_atendimentos = Task.Run(() => _appAtendimento.ByCanal(id, id2, Periodo));
                user_atendimentos.Wait();
                rjson = new JsonResult(user_atendimentos.Result);
            }
            else
            {
                var user_atendimentos = Task.Run(() => _appAtendimento.ByCanalMonth(id, id2, Periodo));
                user_atendimentos.Wait();
                rjson = new JsonResult(user_atendimentos.Result);
            }

            return rjson;
        }

        public async Task<IActionResult> OnPostMonth(string id, int y)
        {
            await LoadAsync(Periodo, id, y);
            return Page();
        }

        public async Task<IActionResult> OnPostYear(string id, int y)
        {
            Periodo = DateTime.Now;
            await LoadAsync(Periodo, id, y);
            return Page();
        }

        private void GetPeriod(string param)
        {
            switch (param)
            {
                case "jan":
                    Periodo = new DateTime(Periodo.Year, 01, 01);
                    break;

                case "fev":
                    Periodo = new DateTime(Periodo.Year, 02, 01);
                    break;

                case "mar":
                    Periodo = new DateTime(Periodo.Year, 03, 01);
                    break;

                case "abr":
                    Periodo = new DateTime(Periodo.Year, 04, 01);
                    break;

                case "mai":
                    Periodo = new DateTime(Periodo.Year, 05, 01);
                    break;

                case "jun":
                    Periodo = new DateTime(Periodo.Year, 06, 01);
                    break;

                case "jul":
                    Periodo = new DateTime(Periodo.Year, 07, 01);
                    break;

                case "ago":
                    Periodo = new DateTime(Periodo.Year, 08, 01);
                    break;

                case "set":
                    Periodo = new DateTime(Periodo.Year, 09, 01);
                    break;

                case "out":
                    Periodo = new DateTime(Periodo.Year, 10, 01);
                    break;

                case "nov":
                    Periodo = new DateTime(Periodo.Year, 11, 01);
                    break;

                case "dez":
                    Periodo = new DateTime(Periodo.Year, 12, 01);
                    break;

                default:
                    ButtonPressed = "Year";
                    break;
            }
        }
    }
}
