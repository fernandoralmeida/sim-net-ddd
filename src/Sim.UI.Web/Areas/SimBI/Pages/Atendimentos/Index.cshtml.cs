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
    using Sim.Domain.BI;
    using System;


    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IAppServiceAtendimento _appAtendimento;
        private readonly IAppServiceSetor _appSetores;

        [BindProperty(SupportsGet = true)]
        public IEnumerable<KeyValuePair<string, int>> Sebrae { get; set; }

        [BindProperty(SupportsGet = true)]
        public BiAtendimentos Atendimentos_List { get; set; }

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
            var nperiodo = new DateTime(Ano, 1, 1);
            Atendimentos_List  = await _appAtendimento.BI_Atendimentos(nperiodo);
            Sebrae = await _appAtendimento.BI_Atendimentos_Setor(nperiodo, "Sebrae Aqui");
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Ano = DateTime.Now.Year;
            Mes = DateTime.Now.Month;
            await LoadAsync();
            return Page();
        }

        public JsonResult OnGetPreview(string id, string id2, string mth, int y)
        {
            JsonResult rjson;

            if (mth == "0")
            {
                var periodo = new DateTime(y, 1, 1);
                var user_atendimentos = _appAtendimento.ByUserName(id, periodo);
                user_atendimentos.Wait();
                rjson = new JsonResult(user_atendimentos.Result);
            }
            else
            {
                var p = new DateTime(y, Convert.ToInt32(mth), 1);
                var user_atendimentos = _appAtendimento.ByUserNameMonth(id, id2, p);
                user_atendimentos.Wait();
                rjson = new JsonResult(user_atendimentos.Result);
            }

            return rjson;
        }

        public JsonResult OnGetServicoPreview(string id, string mth, int y)
        {
            JsonResult rjson;
                        

            if (mth == "0")
            {
                var periodo = new DateTime(y, 1, 1);
                var user_atendimentos =  _appAtendimento.ByServicos(id, periodo);
                user_atendimentos.Wait();
                rjson = new JsonResult(user_atendimentos.Result);                
            }
            else
            {
                var periodo = new DateTime(y, Convert.ToInt32(mth), 1);
                var user_atendimentos = _appAtendimento.ByServicosMonth(id, periodo);
                user_atendimentos.Wait();
                rjson = new JsonResult(user_atendimentos.Result);
            }

            return rjson;
        }

        public JsonResult OnGetCanalPreview(string id, string id2,  string mth, int y)
        {
            JsonResult rjson;            

            if (mth == "0")
            {                
                var periodo = new DateTime(y, 1, 1);
                var user_atendimentos = _appAtendimento.ByCanal(id, id2, periodo);
                user_atendimentos.Wait();
                rjson = new JsonResult(user_atendimentos.Result);
            }
            else
            {
                var periodo = new DateTime(y, Convert.ToInt32(mth), 1);
                var user_atendimentos = _appAtendimento.ByCanalMonth(id, id2, periodo);
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
