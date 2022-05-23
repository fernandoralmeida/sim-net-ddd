using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        private readonly IAppServiceSecretaria _appServiceSecretaria;

        [BindProperty(SupportsGet = true)]
        public Secretaria Sec { get; set; } //Secretaria

        [BindProperty(SupportsGet = true)]
        public IEnumerable<BiAtendimentos> Setores { get; set; }

        [BindProperty(SupportsGet = true)]
        public BiAtendimentos Atendimentos_List { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty(SupportsGet = true)]
        public int Ano { get; set; }

        public SelectList Secretarias { get; set; }

        public IndexModel(IAppServiceAtendimento appAtendimento,
            IAppServiceSecretaria appServiceSecretaria,
            IAppServiceSetor appsetores)
        {
            _appAtendimento = appAtendimento;
            _appServiceSecretaria = appServiceSecretaria;
            _appSetores = appsetores;  
        }

        private async Task LoadSecretaria()
        {
            var s = Task.Run(() => _appServiceSecretaria.List());
            await s;
            if (s.Result != null)
                Secretarias = new SelectList(s.Result, nameof(Sec.Nome), nameof(Sec.Nome), null);

            Sec.Nome = Secretarias.SingleOrDefault().Text;
        }

        private async Task LoadAsync()
        {            
            var nperiodo = new DateTime(Ano, 1, 1);
            Atendimentos_List  = await _appAtendimento.BI_Atendimentos(nperiodo);
            var _list = new List<BiAtendimentos>();
            var setores = _appSetores.GetByOwner(Sec.Nome);
            foreach(var s in setores)
            {
                _list.Add(await _appAtendimento.BI_Atendimentos_Setor(nperiodo, s.Nome));
            }
            Setores = _list;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Ano = DateTime.Now.Year;
            await LoadSecretaria();
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
            await LoadSecretaria();
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
